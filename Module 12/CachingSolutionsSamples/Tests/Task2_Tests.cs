using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Caching.Cache;
using CachingSolutionsSamples.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Tests
{
    [TestClass]
    public class Task2_Tests
    {
        private readonly string _categoriesPrefix = "Cache_Categories";
        private readonly string _customersPrefix = "Cache_Customers";
        private readonly string _suppliersPrefix = "Cache_Suppliers";

        [TestMethod]
        public void CategoriesMemoryCache()
        {
            EntitiesManager<Category> entitiesManager = new EntitiesManager<Category>(new MemoryCache<IEnumerable<Category>>(_categoriesPrefix));

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void CategoriesRedisCache()
        {
            EntitiesManager<Category> entitiesManager = new EntitiesManager<Category>(new RedisCache<IEnumerable<Category>>("localhost", _categoriesPrefix));

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void CustomersMemoryCache()
        {
            EntitiesManager<Customer> entitiesManager = new EntitiesManager<Customer>(new MemoryCache<IEnumerable<Customer>>(_customersPrefix));

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void CustomersRedisCache()
        {
            EntitiesManager<Customer> entitiesManager = new EntitiesManager<Customer>(new RedisCache<IEnumerable<Customer>>("localhost", _customersPrefix));

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void SuppliersMemoryCache()
        {
            EntitiesManager<Supplier> entitiesManager = new EntitiesManager<Supplier>(new MemoryCache<IEnumerable<Supplier>>(_suppliersPrefix));

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void SuppliersRedisCache()
        {
            EntitiesManager<Supplier> entitiesManager = new EntitiesManager<Supplier>(new RedisCache<IEnumerable<Supplier>>("localhost", _suppliersPrefix));

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void SqlMonitorsTest()
        {
            MemoryEntitiesManager<Supplier> entitiesManager = new MemoryEntitiesManager<Supplier>(new MemoryCache<IEnumerable<Supplier>>(_suppliersPrefix),
                "select [SupplierID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax] from [dbo].[Suppliers]");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(entitiesManager.GetEntities().Count());
                Thread.Sleep(1000);
            }
        }


    }
}