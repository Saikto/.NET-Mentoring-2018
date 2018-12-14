using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Caching.Interfaces;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Managers
{
    public class EntitiesManager<T> where T : class
    {
        private readonly ICache<IEnumerable<T>> _cache;

        public EntitiesManager(ICache<IEnumerable<T>> cache)
        {
            _cache = cache;
        }

        public IEnumerable<T> GetEntities()
        {
            Console.WriteLine("Get Entities");
            string user = Thread.CurrentPrincipal.Identity.Name;
            IEnumerable<T> entities = _cache.Get(user);

            if (entities == null)
            {
                Console.WriteLine("From no cache storage");
                using (Northwind dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                }

                _cache.Set(user, entities, DateTimeOffset.Now.AddDays(1));
            }
            return entities;
        }
    }
}