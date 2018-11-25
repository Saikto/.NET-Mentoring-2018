using System;
using System.Linq;
using LinqToDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Northwind_Linq2Db.Tasks
{
    [TestClass]
    public class Task1
    {
        private NorthwindConnection _northwind;

        [TestInitialize]
        public void TestInitialize()
        {
            _northwind = new NorthwindConnection("Northwind");
        }

        [TestCleanup]
        public void CleanUp()
        {
            _northwind.Dispose();
        }

        [TestMethod]
        public void ProductsList_With_CategoryAndSupplier()
        {
            foreach (var product in _northwind.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier).ToList())
            {
                Console.WriteLine($"Product name: {product.ProductName}; \n     Category: {product.Category?.CategoryName}; Supplier: {product.Supplier?.ContactName}");
            }
        }

        [TestMethod]
        public void EmployeesList_With_Region()
        {
            var resultQuery = from employee in _northwind.Employees
                        join employeeT in _northwind.EmployeeTerritories on employee.EmployeeId equals employeeT.EmployeeId into j1
                        from a in j1.DefaultIfEmpty()
                        join territory in _northwind.Territories on a.TerritoryId equals territory.TerritoryId into j2
                        from b in j2.DefaultIfEmpty()
                        join region in _northwind.Regions on b.RegionId equals region.RegionId into j3
                        from c in j3.DefaultIfEmpty()
                        select new { employee.FirstName, employee.LastName, Region = c };
            resultQuery = resultQuery.Distinct();

            foreach (var record in resultQuery.ToList())
            {
                Console.WriteLine($"Employee: {record.FirstName} {record.LastName}; Region: {record.Region?.RegionDescription}");
            }
        }

        [TestMethod]
        public void EmployeesCount_By_Regions()
        {
            var query = from region in _northwind.Regions
                        join territory in _northwind.Territories on region.RegionId equals territory.RegionId into j1
                        from a in j1.DefaultIfEmpty()
                        join employeeT in _northwind.EmployeeTerritories on a.TerritoryId equals employeeT.TerritoryId into j2
                        from b in j2.DefaultIfEmpty()
                        join employee in _northwind.Employees on b.EmployeeId equals employee.EmployeeId into j3
                        from c in j3.DefaultIfEmpty()
                        select new { Region = region, c.EmployeeId };

            var result = from row in query.Distinct()
                         group row by row.Region into ger
                         select new { RegionDescription = ger.Key.RegionDescription, EmployeesCount = ger.Count(e => e.EmployeeId != 0) };

            foreach (var record in result.ToList())
            {
                Console.WriteLine($"Region: {record.RegionDescription}; Employees count: {record.EmployeesCount}");
            }
        }

        [TestMethod]
        public void EmployeesToShippers_Using_Orders()
        {
            var query = (from employee in _northwind.Employees
                         join order in _northwind.Orders on employee.EmployeeId equals order.EmployeeId into j1
                         from a in j1.DefaultIfEmpty()
                         join shipper in _northwind.Shippers on a.ShipperId equals shipper.ShipperId into j2
                         from b in j2.DefaultIfEmpty()
                         select new { employee.EmployeeId, employee.FirstName, employee.LastName, b.CompanyName })
                .Distinct()
                .OrderBy(t => t.EmployeeId);

            foreach (var record in query.ToList())
            {
                Console.WriteLine($"Employee: {record.FirstName} {record.LastName}\n    Shipper: {record.CompanyName}");
            }
        }
    }
}
