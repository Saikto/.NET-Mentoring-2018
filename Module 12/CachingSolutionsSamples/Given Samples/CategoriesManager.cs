using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public class CategoriesManager
	{
		private ICategoriesCache cache;

		public CategoriesManager(ICategoriesCache cache)
		{
			this.cache = cache;
		}

		public IEnumerable<Category> GetCategories()
		{
			Console.WriteLine("Get Categories");

            string user = Thread.CurrentPrincipal.Identity.Name;
            IEnumerable<Category> categories = cache.Get(user);

			if (categories == null)
			{
				Console.WriteLine("From DB");

				using (Northwind dbContext = new Northwind())
				{
					dbContext.Configuration.LazyLoadingEnabled = false;
					dbContext.Configuration.ProxyCreationEnabled = false;
					categories = dbContext.Categories.ToList();
					cache.Set(user, categories);
				}
			}

			return categories;
		}
	}
}
