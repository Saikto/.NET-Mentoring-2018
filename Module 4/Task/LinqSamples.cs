// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

        [Category("Tasks")]
        [Title("Task 1")]
        [Description("Displays list of customer's name who have total order sum more that X.")]
        public void Linq001()
        {
            decimal valueX = 65000;

            var customers = dataSource.Customers.Where(x => x.Orders.Sum(t => t.Total) > valueX);

            ObjectDumper.Write($"Customers with total orders sum > {valueX}: {Environment.NewLine}");
            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.CompanyName);
            }

            valueX = 1000;
            ObjectDumper.Write($"Customers with total orders sum > {valueX}: {Environment.NewLine}");
            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.CompanyName);
            }
        }

        [Category("Tasks")]
        [Title("Task 2")]
        [Description("Displays list of customers and for each one of them list of suppliers that are situated in the same country and city.")]
        public void Linq002()
        {
            var customersAndSuppliers = dataSource.Customers
                .Select(c => new
                {
                    Customer = c,
                    Suppliers = dataSource.Suppliers.Where(s => s.City == c.City && s.Country == c.Country)
                });

            ObjectDumper.Write($"Customers and their suppliers:{Environment.NewLine}");

            foreach (var customer in customersAndSuppliers)
            {
                var suppliers = customer.Suppliers.Select(s => s.SupplierName).ToArray();
                ObjectDumper.Write($"Customer name: {customer.Customer.CompanyName}");
                ObjectDumper.Write($"    Suppliers: { string.Join(", ",suppliers)} {Environment.NewLine}");
            }
        }

        [Category("Tasks")]
        [Title("Task 2 with grouping")]
        [Description("Displays list of customers and for each one of them list of suppliers that are situated in the same country and city.")]
        public void Linq002Grouping()
        {
            ObjectDumper.Write($"Customers and their suppliers:{Environment.NewLine}");

            var customersAndSuppliersGrouping = dataSource.Customers.GroupJoin(
                dataSource.Suppliers,
                c => new { c.City, c.Country },
                s => new { s.City, s.Country },
                (c, s) => new { Customer = c, Suppliers = s });

            foreach (var customer in customersAndSuppliersGrouping)
            {
                var suppliers = customer.Suppliers.Select(s => s.SupplierName).ToArray();
                ObjectDumper.Write($"Customer name: {customer.Customer.CompanyName}");
                ObjectDumper.Write($"    Suppliers: { string.Join(", ", suppliers)} {Environment.NewLine}");
            }
        }

        [Category("Tasks")]
        [Title("Task 3")]
        [Description("Displays list of customers who have at least one order with total greater than 12000.")]
        public void Linq003()
        {
            decimal valueX = 12000;

            var customers = dataSource.Customers.Where(x => x.Orders.Any(t => t.Total > valueX));

            ObjectDumper.Write($"Customers with at least one order cost > {valueX}: {Environment.NewLine}");
            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer.CompanyName);
            }
        }

        [Category("Tasks")]
        [Title("Task 4")]
        [Description("Displays list of customers and date when they did their first order.")]
        public void Linq004()
        {
            var customersWithJoinDate = dataSource.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new
                {
                    Customer = c,
                    JoinDate = c.Orders.Min(o => o.OrderDate)
                });

            ObjectDumper.Write($"Customers and join date:{Environment.NewLine}");

            foreach (var customer in customersWithJoinDate)
            {
                ObjectDumper.Write($"Customer name: {customer.Customer.CompanyName}");
                ObjectDumper.Write($"    Joined: {customer.JoinDate:MM.yyyy} {Environment.NewLine}");
            }
        }

        [Category("Tasks")]
        [Title("Task 5")]
        [Description("Displays list of customers and date when they did their first order. Sorted ascending by year, then by month, then by descending total orders sum and then by ascending customer's name.")]
        public void Linq005()
        {
            var customersWithJoinDate = dataSource.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new
                {
                    Customer = c,
                    JoinDate = c.Orders.Min(o => o.OrderDate)
                })
                .OrderBy(c => c.JoinDate.Year)
                .ThenBy(c => c.JoinDate.Month)
                .ThenByDescending(c => c.Customer.Orders.Sum(o => o.Total))
                .ThenBy(c => c.Customer.CompanyName);

            ObjectDumper.Write($"Customers and join date:{Environment.NewLine}");

            foreach (var customer in customersWithJoinDate)
            {
                ObjectDumper.Write($"Customer name: {customer.Customer.CompanyName}");
                ObjectDumper.Write($"    Joined: {customer.JoinDate:MM.yyyy} {Environment.NewLine}");
            }
        }

        [Category("Tasks")]
        [Title("Task 6")]
        [Description("Displays list of customers with non digit postal code or without region or without phone operator code.")]
        public void Linq006()
        {
            Regex postalPattern = new Regex(@"\d");
            Regex phonePattern = new Regex(@"^(.\d*).*");

            var customersWithNotValidFields = dataSource.Customers
                .Where(c => c.PostalCode == null || !postalPattern.IsMatch(c.PostalCode) || c.Region == null || !phonePattern.IsMatch(c.Phone));

            ObjectDumper.Write($"Customers with non digit postal code or without region or without phone operator code:{Environment.NewLine}");

            foreach (var customer in customersWithNotValidFields)
            {
                ObjectDumper.Write($"Customer name: {customer.CompanyName}");
            }
        }

        [Category("Tasks")]
        [Title("Task 7")]
        [Description("Displays list of products that are groupped by categories and by their existence in stock. Products ordered by their price.")]
        public void Linq007()
        {
            var finalGrouping = dataSource.Products
                .GroupBy(p => p.Category)
                .Select(c => new
                {
                    Category = c.Key,
                    groupInStock = c.GroupBy(s => s.UnitsInStock > 0)
                    .Select(m => new
                    {
                        inStock = m.Key,
                        Products = m.OrderBy(u => u.UnitPrice)
                    })
                    
                });

            foreach (var productCategories in finalGrouping)
            {
                ObjectDumper.Write($"{Environment.NewLine}Category: {productCategories.Category}");
                foreach (var productInStock in productCategories.groupInStock)
                {
                    ObjectDumper.Write($"   Is in stock: {productInStock.inStock}");
                    foreach (var product in productInStock.Products)
                    {
                        ObjectDumper.Write($"       Product: {product.ProductName}, price for one unit: {product.UnitPrice}");
                    }
                }
            }
        }

        [Category("Tasks")]
        [Title("Task 8")]
        [Description("Displays list of products seperated into 3 groups by their price: low cost, mid cost and high cost.")]
        public void Linq008()
        {
            var maxUnitPrice = dataSource.Products.Max(p => p.UnitPrice);

            var midBorder = maxUnitPrice / 3;
            var highBorder = maxUnitPrice * 2 / 3;

            var productGroupsLowAndMidHigh = dataSource.Products.GroupBy(p => p.UnitPrice < midBorder).ToList();
            var productGroupsMidAndHigh = productGroupsLowAndMidHigh.Last().GroupBy(p => p.UnitPrice > highBorder).ToList();

            List<IGrouping<bool, Product>> result = new List<IGrouping<bool, Product>>();

            result.Add(productGroupsLowAndMidHigh.First());
            result.Add(productGroupsMidAndHigh.First());
            result.Add(productGroupsMidAndHigh.Last());

            for (int i = 0; i < result.Count; i++)
            {
                if (i == 0) ObjectDumper.Write("Low cost products:");
                if (i == 1) ObjectDumper.Write("Mid cost products:");
                if (i == 2) ObjectDumper.Write("Hihg cost products:");

                foreach (var product in result[i])
                {
                    ObjectDumper.Write($"Product: {product.ProductName}, price for one unit: {product.UnitPrice}");
                }
                ObjectDumper.Write(Environment.NewLine);
            }
        }

        [Category("Tasks")]
        [Title("Task 9")]
        [Description("Displays average cliets order total and average clients order count for each city.")]
        public void Linq009()
        {
            var cityInfo = dataSource.Customers
                .GroupBy(c => c.City)
                .Select(c => new
                {
                    City = c.Key,
                    Valuability = c.Average(b => b.Orders.Sum(o => o.Total)),
                    Intensity = c.Average(b => b.Orders.Count())
                });

            foreach (var city in cityInfo)
            {
                ObjectDumper.Write($"City: {city.City}");
                ObjectDumper.Write($"   Valuability: {city.Valuability}");
                ObjectDumper.Write($"   Intensity: {city.Intensity}");
                ObjectDumper.Write(Environment.NewLine);
            }
        }

        [Category("Tasks")]
        [Title("Task 10")]
        [Description("Displays customer's orders statictics by months, by years and by months in years.")]
        public void Linq010()
        {
            var customerInfo = dataSource.Customers.Select(c => new
            {
                c.CompanyName,
                byMonth = c.Orders
                    .GroupBy(o => o.OrderDate.Month)
                    .Select(g => new { g.Key, OrdersCount = g.Count() }),

                byYear = c.Orders
                    .GroupBy(o => o.OrderDate.Year)
                    .Select(g => new { g.Key, OrdersCount = g.Count() }),

                byYearAndMonth = c.Orders
                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                    .Select(g => new { g.Key.Year, g.Key.Month, OrdersCount = g.Count() })
            });

            foreach (var customer in customerInfo)
            {
                ObjectDumper.Write($"Customer: {customer.CompanyName}");

                ObjectDumper.Write($"   Statistics by month:");
                foreach (var record in customer.byMonth)
                {
                    ObjectDumper.Write($"       Month: {record.Key}, Orders count: {record.OrdersCount}");
                }

                ObjectDumper.Write($"   Statistics by year:");
                foreach (var record in customer.byYear)
                {
                    ObjectDumper.Write($"       Year: {record.Key}, Orders count: {record.OrdersCount}");
                }

                ObjectDumper.Write($"   Statistics by year and month:");
                foreach (var record in customer.byYearAndMonth)
                {
                    ObjectDumper.Write($"       Year: {record.Year}, Month: {record.Month}, Orders count: {record.OrdersCount}");
                }
                ObjectDumper.Write(Environment.NewLine);
            }
        }
    }
}
