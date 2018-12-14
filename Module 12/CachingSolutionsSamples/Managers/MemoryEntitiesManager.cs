using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using Caching.Cache;
using NorthwindLibrary;

namespace CachingSolutionsSamples.Managers
{
    public class MemoryEntitiesManager<T> where T : class
    {
        private readonly MemoryCache<IEnumerable<T>> _cache;
        private readonly string _monitorCommand;

        public MemoryEntitiesManager(MemoryCache<IEnumerable<T>> cache, string monitorCommand)
        {
            _cache = cache;
            _monitorCommand = monitorCommand;
        }

        public IEnumerable<T> GetEntities()
        {
            Console.WriteLine("Get Entities");
            string user = Thread.CurrentPrincipal.Identity.Name;
            IEnumerable<T> entities = _cache.Get(user);

            if (entities == null)
            {
                Console.WriteLine("From no cache storage");
                string connectionString;
                using (Northwind dbContext = new Northwind())
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    entities = dbContext.Set<T>().ToList();
                    connectionString = dbContext.Database.Connection.ConnectionString;
                }

                SqlDependency.Start(connectionString);
                _cache.Set(user, entities, GetCachePolicy(_monitorCommand, connectionString));
            }
            return entities;
        }

        private CacheItemPolicy GetCachePolicy(string monitorCommand, string connectionString)
        {
            return new CacheItemPolicy
            {
                ChangeMonitors = { GetMonitor(monitorCommand, connectionString) }
            };
        }

        private SqlChangeMonitor GetMonitor(string query, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlChangeMonitor monitor = new SqlChangeMonitor(new SqlDependency(command));
                command.ExecuteNonQuery();
                return monitor;
            }
        }
    }
}