using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.IO;
using System.Web;
using System.Xml.Linq;
using ClosedXML.Excel;

namespace NorthwindHandler
{
    //    /Report?customer=ERNSH&dateFrom=1996-07-01&take=10&skip=0

    public class NorthwindHandler: IHttpHandler
    {
        public bool IsReusable => true;

        private const string XlsxType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private const string XmlType1 = "text/xml";
        private const string XmlType2 = "application/xml";

        public void ProcessRequest(HttpContext context)
        {
            string customerValue = null;
            string dateFrom = null;
            string dateTo = null;
            string take = null;
            string skip = null;

            if (context.Request.HttpMethod == "GET")
            {
                customerValue = context.Request.QueryString["customer"];
                dateFrom = context.Request.QueryString["dateFrom"];
                dateTo = context.Request.QueryString["dateTo"];
                take = context.Request.QueryString["take"];
                skip = context.Request.QueryString["skip"];
            }
            else if (context.Request.HttpMethod == "POST")
            {
                string requestBody = GetRequestBody(context);

                Dictionary<string, string> parametersDict = new Dictionary<string, string>();
                var parametersArray = requestBody.Split('&');
                foreach (var parameterString in parametersArray)
                {
                    parametersDict.Add(parameterString.Split('=')[0], parameterString.Split('=')[1]);
                }

                parametersDict.TryGetValue("customer", out customerValue);
                parametersDict.TryGetValue("dateFrom", out dateFrom);
                parametersDict.TryGetValue("dateTo", out dateTo);
                parametersDict.TryGetValue("take", out take);
                parametersDict.TryGetValue("skip", out skip);
            }

            if (dateFrom != null && dateTo != null)
            {
                context.Response.StatusCode = 400;
                context.Response.StatusDescription = "dateFrom and dateTo parameters can't be used together. Please change request.";
                return;
            }

            List<Orders> orders = new List<Orders>();
            try
            {
                orders = OrdersFromDb(customerValue, dateFrom, dateTo, take, skip);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 400;
                context.Response.StatusDescription = ex.Message;
                return;
            }

            var acceptHeader = context.Request.Headers["Accept"];
            if (acceptHeader != null && (acceptHeader.Contains(XmlType1) || acceptHeader.Contains(XmlType2)))
            {
                context.Response.Write(GenerateXmlFromOrders(orders));
                context.Response.ContentType = XmlType1;
            }
            else if (acceptHeader == null || acceptHeader.Contains(XlsxType) || !acceptHeader.Contains(XmlType1) ||
                !acceptHeader.Contains(XmlType2))
            {
                context.Response.BinaryWrite(GenerateXlsxFromOrders(orders));
                context.Response.ContentType = XlsxType;
            }
        }

        private List<Orders> OrdersFromDb(string customer, string dateFrom, string dateTo, string take, string skip)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
            DataContext db = new DataContext(connectionString);
            Table<Orders> orders = db.GetTable<Orders>();

            List<Orders> result = orders.ToList();

            if (customer != null)
            {
                result = result.Where(o => o.CustomerID == customer).ToList();
            }

            if (dateFrom != null)
            {
                try
                {
                    result = result.Where(o => o.OrderDate >= DateTime.Parse(dateFrom)).ToList();
                }
                catch (FormatException)
                {
                    throw new Exception("Date string was not in appropriate format.");
                }
            }

            if (dateTo != null)
            {
                try
                {
                    result = result.Where(o => o.OrderDate <= DateTime.Parse(dateTo)).ToList();
                }
                catch (FormatException)
                {
                    throw new Exception("Date string was not in appropriate format.");
                }
            }

            if (skip != null)
            {
                try
                {
                    result = result.Skip(int.Parse(skip)).ToList();
                }
                catch (FormatException)
                {
                    throw new Exception("Skip parameter was not integer value.");
                }
            }

            if (take != null)
            {
                try
                {
                    result = result.Take(int.Parse(take)).ToList();
                }
                catch (FormatException)
                {
                    throw new Exception("Take parameter was not integer value.");
                }
            }

            return result.OrderBy(o => o.CustomerID).ToList();
        }

        private XDocument GenerateXmlFromOrders(List<Orders> orders)
        {
            XDocument document = new XDocument();
            XElement rootElement = new XElement("orders");
            foreach (var order in orders)
            {
                XElement orderElement = new XElement("order");
                XElement customerID = new XElement("CustomerID", order.CustomerID);
                XElement orderDate = new XElement("OrderDate", order.OrderDate);
                XElement shipName = new XElement("ShipName", order.ShipName);
                XElement shipCountry = new XElement("ShipCountry", order.ShipCountry);
                orderElement.Add(customerID, orderDate, shipName, shipCountry);
                rootElement.Add(orderElement);
            }
            document.Add(rootElement);
            return document;
        }

        private byte[] GenerateXlsxFromOrders(List<Orders> orders)
        {
            var result = new XLWorkbook();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Orders");
                worksheet.Columns().AdjustToContents();
                worksheet.Cell("A1").Value = "CustomerID";
                worksheet.Cell("B1").Value = "OrderDate";
                worksheet.Cell("C1").Value = "ShipName";
                worksheet.Cell("D1").Value = "ShipCountry";
                var columnNames = worksheet.Range("A1:D1");
                columnNames.Style.Fill.BackgroundColor = XLColor.AshGrey;

                for(int row = 2; row <= orders.Count + 1; row++)
                {
                    foreach (var cell in Enumerable.Range(1, 5))
                    {
                        worksheet.Cell(row, 1).Value = orders[row - 2].CustomerID;
                        worksheet.Cell(row, 2).Value = orders[row - 2].OrderDate;
                        worksheet.Cell(row, 3).Value = orders[row - 2].ShipName;
                        worksheet.Cell(row, 4).Value = orders[row - 2].ShipCountry;
                    }
                }

                worksheet.Columns().AdjustToContents();
                result = workbook;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                result.SaveAs(stream);
                return stream.ToArray();
            }
        }

        private static string GetRequestBody(HttpContext context)
        {
            var bodyStream = new StreamReader(context.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            return bodyText;
        }
    }
}