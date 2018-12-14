using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
	[TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void MemoryCache()
		{
            CategoriesManager categoryManager = new CategoriesManager(new CategoriesMemoryCache());

			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void RedisCache()
		{
            CategoriesManager categoryManager = new CategoriesManager(new CategoriesRedisCache("localhost"));

			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine(categoryManager.GetCategories().Count());
				Thread.Sleep(100);
			}
		}
	}
}
