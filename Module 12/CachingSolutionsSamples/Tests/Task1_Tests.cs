using System;
using System.Threading;
using Caching.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CachingSolutionsSamples.Tests
{
    [TestClass]
    public class Task1_Tests
    {
        private readonly string _fibonacciPrefix = "_fibonacci";

        [TestMethod]
        public void FibonacciMemoryCache()
        {
            Fibonacci fibonacci = new Fibonacci(new MemoryCache<int>(_fibonacciPrefix));

            for (int i = 1; i < 20; i++)
            {
                Console.WriteLine(fibonacci.ComputeFibonacci(i));
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void FibonacciRedisCache()
        {
            Fibonacci fibonacci = new Fibonacci(new RedisCache<int>("localhost", _fibonacciPrefix));

            for (int i = 1; i < 20; i++)
            {
                Console.WriteLine(fibonacci.ComputeFibonacci(i));
                Thread.Sleep(100);
            }
        }
    }
}