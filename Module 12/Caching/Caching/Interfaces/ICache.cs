using System;

namespace Caching.Interfaces
{
    public interface ICache<T>
    {
        T Get(string key);
        void Set(string key, T value, DateTimeOffset expirationDate);
    }
}
