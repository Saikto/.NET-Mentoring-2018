using System;
using System.IO;
using System.Runtime.Serialization;
using Caching.Interfaces;
using StackExchange.Redis;

namespace Caching.Cache
{
    public class RedisCache<T> : ICache<T>
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly string _prefix;

        private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(T));

        public RedisCache(string hostName, string prefix)
        {
            _prefix = prefix;
            ConfigurationOptions options = new ConfigurationOptions()
            {
                AbortOnConnectFail = false,
                EndPoints = { hostName }
            };
            _redisConnection = ConnectionMultiplexer.Connect(options);
        }

        public T Get(string key)
        {
            IDatabase db = _redisConnection.GetDatabase();
            byte[] s = db.StringGet(_prefix + key);
            if (s == null)
            {
                return default(T);
            }

            return (T)_serializer.ReadObject(new MemoryStream(s));
        }

        public void Set(string key, T value, DateTimeOffset expirationDate)
        {
            IDatabase db = _redisConnection.GetDatabase();
            string redisKey = _prefix + key;

            if (value == null)
            {
                db.StringSet(redisKey, RedisValue.Null);
            }
            else
            {
                MemoryStream stream = new MemoryStream();
                _serializer.WriteObject(stream, value);
                db.StringSet(redisKey, stream.ToArray(), expirationDate - DateTimeOffset.Now);
            }
        }
    }
}