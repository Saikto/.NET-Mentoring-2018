using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Northwind_EntityFramework.Tasks
{
    [TestClass]
    public class UnitTest1
    {
        private NorthwindDB _northwind;

        [TestInitialize]
        public void TestInitialize()
        {
            _northwind = new NorthwindDB();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _northwind.Dispose();
        }

        [TestMethod]
        public void OrdersThatContainProductsOfCategory()
        {
            int catId = 1;
            var result = _northwind.Orders
                .Include(o => o.Order_Details
                    .Select(od => od.Product))
                .Include(o => o.Customer)
                .Where(o => o.Order_Details
                    .Any(od => od.Product.CategoryID == catId))
                .Select(o => new
                {
                    o.Customer.ContactName,
                    Order_Details = o.Order_Details.Select(od => new
                    {
                        od.Product.ProductName,
                        od.OrderID,
                        od.Discount,
                        od.Quantity,
                        od.UnitPrice,
                        od.ProductID
                    })
                }).ToList();

            foreach (var row in result)
            {
                Console.WriteLine(
                    $"Customer: {row.ContactName}\n     Products: {string.Join(", ", row.Order_Details.Select(od => od.ProductName))}");
            }
        }
    }
}