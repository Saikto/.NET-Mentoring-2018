using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind_Linq2Db.Entities;

namespace Northwind_Linq2Db.Tasks
{
    [TestClass]
    public class Task2
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
        public void AddEmployee_With_Territories()
        {
            Employee newEmployee = new Employee { FirstName = "SomeName", LastName = "SomeLastName" };
            try
            {
                _northwind.BeginTransaction();
                newEmployee.EmployeeId = Convert.ToInt32(_northwind.InsertWithIdentity(newEmployee));
                _northwind.Territories.Where(t => t.RegionId == 1)
                    .Insert(_northwind.EmployeeTerritories, t => new EmployeeTerritory { EmployeeId = newEmployee.EmployeeId, TerritoryId = t.TerritoryId });
                _northwind.CommitTransaction();
            }
            catch
            {
                _northwind.RollbackTransaction();
            }
        }

        [TestMethod]
        public void ChangeProductsCategory()
        {
            int affectedRows = _northwind.Products.Update(p => p.CategoryId == 3, pr => new Product
            {
                CategoryId = 2
            });

            Console.WriteLine(affectedRows);
        }

        [TestMethod]
        public void InsertListProducts_With_SuppliersAndCategories()
        {
            var products = new List<Product>
            {
                new Product
                {
                    ProductName = "SomeProduct",
                    Category = new Category {CategoryName = "Produce"},
                    Supplier = new Supplier {CompanyName = "SomeCompany"}
                },
                new Product
                {
                    ProductName = "SomeProduct2",
                    Category = new Category {CategoryName = "SomeCategory"},
                    Supplier = new Supplier {CompanyName = "SomeCompany"}
                }
            };

            try
            {
                _northwind.BeginTransaction();
                foreach (var product in products)
                {
                    var category = _northwind.Categories.FirstOrDefault(c => c.CategoryName == product.Category.CategoryName);
                    var supplier = _northwind.Suppliers.FirstOrDefault(s => s.CompanyName == product.Supplier.CompanyName);

                    if (category != null)
                    {
                        product.CategoryId = category.CategoryId;
                    }
                    else
                    {
                        product.CategoryId = Convert.ToInt32(_northwind.InsertWithIdentity(new Category
                        { 
                                CategoryName = product.Category.CategoryName
                        }));
                    }

                    
                    if (supplier != null)
                    {
                        product.SupplierId = supplier.SupplierId;
                    }
                    else
                    {
                        product.SupplierId = Convert.ToInt32(_northwind.InsertWithIdentity(new Supplier
                        {
                                CompanyName = product.Supplier.CompanyName
                        }));
                    }
                }

                _northwind.BulkCopy(products);
                _northwind.CommitTransaction();
            }
            catch
            {
                _northwind.RollbackTransaction();
            }
        }

        [TestMethod]
        public void ReplaceProduct_In_NotShippedOrders()
        {
            int prIdToReplace = 42;
            int prIdReplacement = 14;
            var affectedRows = _northwind.OrderDetails.LoadWith(od => od.Order).LoadWith(od => od.Product)
                .Where(od => od.Order.ShippedDate == null && od.ProductId == prIdToReplace).Update(
                    od => new OrderDetail
                    {
                        ProductId = prIdReplacement
                    });

            Console.WriteLine(affectedRows);
        }

    }
}
