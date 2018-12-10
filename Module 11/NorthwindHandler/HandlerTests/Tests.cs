using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Xml.Linq;
using ClosedXML.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HandlerTests
{
    [TestClass]
    public class Tests
    {
        private readonly SqlConnection _connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString);
        private readonly Uri _baseUri = new Uri("http://localhost:51123");
        private const string Path = "/Report";

        [TestInitialize]
        public void TestInitialize()
        {
            _connection.Open();
            string query0 = "ALTER TABLE Orders NOCHECK CONSTRAINT ALL";
            string query1 = "INSERT INTO Orders (CustomerID, OrderDate, ShipName, ShipCountry) VALUES ('AAAAA', '2018-10-12', 'SomeShipName', 'Belarus')";
            string query2 = "INSERT INTO Orders (CustomerID, OrderDate, ShipName, ShipCountry) VALUES ('BBBBB', '2018-09-12', 'SomeShipName', 'Belarus')";
            string query3 = "INSERT INTO Orders (CustomerID, OrderDate, ShipName, ShipCountry) VALUES ('CCCCC', '2018-08-12', 'SomeShipName', 'Belarus')";
            SqlCommand command0 = new SqlCommand(query0, _connection);
            SqlCommand command1 = new SqlCommand(query1, _connection);
            SqlCommand command2 = new SqlCommand(query2, _connection);
            SqlCommand command3 = new SqlCommand(query3, _connection);
            command0.ExecuteNonQuery();
            command1.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            command3.ExecuteNonQuery();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            string query0 = "DELETE FROM Orders WHERE CustomerID IN ('AAAAA', 'BBBBB', 'CCCCC')";
            string query1 = "ALTER TABLE Categories CHECK CONSTRAINT ALL";
            SqlCommand command0 = new SqlCommand(query0, _connection);
            SqlCommand command1 = new SqlCommand(query1, _connection);
            command0.ExecuteNonQuery();
            command1.ExecuteNonQuery();
            _connection.Close();
        }

        [TestMethod]
        public void UrlEncodedRequest_Customer_DateTo_Xml()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("customer", "AAAAA"),
                new KeyValuePair<string, string>("dateTo", "2018-12-10"),
            };
            var content = new FormUrlEncodedContent(pairs);
            var client = new HttpClient
            {
                BaseAddress = _baseUri,
            };
            client.DefaultRequestHeaders.Add("Accept", "text/xml");
            var response = client.PostAsync(Path, content).Result;
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            Assert.IsTrue(response.Content.Headers.ContentType.ToString().Contains("text/xml"));

            XDocument document = XDocument.Parse(response.Content.ReadAsStringAsync().Result);
            XElement ordersElement = document.Root;

            Assert.AreEqual(1, ordersElement.DescendantsAndSelf("order").Count());
            Assert.AreEqual(ordersElement.DescendantsAndSelf("order").DescendantsAndSelf("CustomerID").First().Value, "AAAAA");
        }

        [TestMethod]
        public void UrlParams_DateFrom_Take_Skip_Xml()
        {
            string pathParams = "/Report?&dateFrom=2018-12-08&take=3&skip=1";
            var client = new HttpClient
            {
                BaseAddress = _baseUri,
            };
            client.DefaultRequestHeaders.Add("Accept", "text/xml");
            var response = client.GetAsync(pathParams).Result;
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            Assert.IsTrue(response.Content.Headers.ContentType.ToString().Contains("text/xml"));

            XDocument document = XDocument.Parse(response.Content.ReadAsStringAsync().Result);
            XElement ordersElement = document.Root;

            Assert.AreEqual(2, ordersElement.DescendantsAndSelf("order").Count());
            Assert.AreEqual(ordersElement.DescendantsAndSelf("order").DescendantsAndSelf("CustomerID").First().Value, "BBBBB");
            Assert.AreEqual(ordersElement.DescendantsAndSelf("order").DescendantsAndSelf("CustomerID").Last().Value, "CCCCC");
        }

        [TestMethod]
        public void UrlParams_DateFrom_Take_Skip_Xlsx_Implicitly()
        {
            string pathParams = "/Report?&dateFrom=2018-12-08&take=3&skip=2";
            var client = new HttpClient
            {
                BaseAddress = _baseUri,
            };
            var response = client.GetAsync(pathParams).Result;
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            Assert.IsTrue(response.Content.Headers.ContentType.ToString().Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

            using (var workbook = new XLWorkbook(response.Content.ReadAsStreamAsync().Result))
            {
                Assert.IsTrue(workbook.Worksheets.Contains("Orders"));
                var worksheet = workbook.Worksheet("Orders");
                Assert.IsTrue(worksheet.Cell(1, 1).Value.ToString() == "CustomerID");
                Assert.IsTrue(worksheet.Cell(1, 2).Value.ToString() == "OrderDate");
                Assert.IsTrue(worksheet.Cell(1, 3).Value.ToString() == "ShipName");
                Assert.IsTrue(worksheet.Cell(1, 4).Value.ToString() == "ShipCountry");

                Assert.IsTrue(worksheet.Cell(2, 1).Value.ToString() == "CCCCC");
                Assert.IsTrue(worksheet.Cell(2, 2).Value.ToString() == "08.12.2018 0:00:00");
                Assert.IsTrue(worksheet.Cell(2, 3).Value.ToString() == "SomeShipName");
                Assert.IsTrue(worksheet.Cell(2, 4).Value.ToString() == "Belarus");
            }
        }

        [TestMethod]
        public void UrlParams_DateFrom_Take_Skip_Xlsx_Explicitly()
        {
            string pathParams = "/Report?&dateFrom=2018-12-08&take=3&skip=2";
            var client = new HttpClient
            {
                BaseAddress = _baseUri,
            };
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            var response = client.GetAsync(pathParams).Result;
            if (!response.IsSuccessStatusCode)
            {
                Assert.Fail();
            }

            Assert.IsTrue(response.Content.Headers.ContentType.ToString().Contains("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

            using (var workbook = new XLWorkbook(response.Content.ReadAsStreamAsync().Result))
            {
                Assert.IsTrue(workbook.Worksheets.Contains("Orders"));
                var worksheet = workbook.Worksheet("Orders");
                Assert.IsTrue(worksheet.Cell(1, 1).Value.ToString() == "CustomerID");
                Assert.IsTrue(worksheet.Cell(1, 2).Value.ToString() == "OrderDate");
                Assert.IsTrue(worksheet.Cell(1, 3).Value.ToString() == "ShipName");
                Assert.IsTrue(worksheet.Cell(1, 4).Value.ToString() == "ShipCountry");

                Assert.IsTrue(worksheet.Cell(2, 1).Value.ToString() == "CCCCC");
                Assert.IsTrue(worksheet.Cell(2, 2).Value.ToString() == "08.12.2018 0:00:00");
                Assert.IsTrue(worksheet.Cell(2, 3).Value.ToString() == "SomeShipName");
                Assert.IsTrue(worksheet.Cell(2, 4).Value.ToString() == "Belarus");
            }
        }

        [TestMethod]
        public void UrlParams_DateFrom_DateTo()
        {
            string pathParams = "/Report?&dateFrom=2018-12-08&dateTo=2018-12-10";
            var client = new HttpClient
            {
                BaseAddress = _baseUri,
            };
            var response = client.GetAsync(pathParams).Result;
            if (response.StatusCode != HttpStatusCode.BadRequest)
            {
                Assert.Fail();
            }
        }
    }
}
