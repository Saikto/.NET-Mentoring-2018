using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIoC;
using MyIoCTests.TestClasses;

namespace MyIoCTests
{
    [TestClass]
    public class ContainerTests
    {
        private Container _container;

        [TestInitialize]
        public void TestInitialize()
        {
            _container = new Container();
        }

        [TestMethod]
        public void AddAssembly_ImportConstructor_CreateInstance_Test()
        {
            _container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerBll = (CustomerBLL)_container.CreateInstance(typeof(CustomerBLL));

            Assert.IsNotNull(customerBll);
            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void AddAssembly_ImportConstructor_GenericCreateInstance_Test()
        {
            _container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerBll = _container.CreateInstance<CustomerBLL>();

            Assert.IsNotNull(customerBll);
            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void AddType_ImportConstructor_CreateInstance_Test()
        {
            _container.AddType(typeof(Logger));
            _container.AddType(typeof(CustomerBLL));
            _container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBll = (CustomerBLL)_container.CreateInstance(typeof(CustomerBLL));

            Assert.IsNotNull(customerBll);
            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void AddType_ImportConstructor_GenericCreateInstance_Test()
        {
            _container.AddType(typeof(Logger));
            _container.AddType(typeof(CustomerBLL));
            _container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBll = _container.CreateInstance<CustomerBLL>();

            Assert.IsNotNull(customerBll);
            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL));
        }

        [TestMethod]
        public void AddAssembly_ImportProperties_CreateInstance_Test()
        {
            _container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerBll = (CustomerBLL2)_container.CreateInstance(typeof(CustomerBLL2));

            Assert.IsNotNull(customerBll);
            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL2));

            Assert.IsNotNull(customerBll.CustomerDAL);
            Assert.IsTrue(customerBll.CustomerDAL.GetType() == typeof(CustomerDAL));

            Assert.IsNotNull(customerBll.Logger);
            Assert.IsTrue(customerBll.Logger.GetType() == typeof(Logger));
        }

        [TestMethod]
        public void AddAssembly_ImportProperties_GenericCreateInstance_Test()
        {
            _container.AddAssembly(Assembly.GetExecutingAssembly());

            var customerBll = _container.CreateInstance<CustomerBLL2>();

            Assert.IsNotNull(customerBll);
            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL2));

            Assert.IsNotNull(customerBll.CustomerDAL);
            Assert.IsTrue(customerBll.CustomerDAL.GetType() == typeof(CustomerDAL));

            Assert.IsNotNull(customerBll.Logger);
            Assert.IsTrue(customerBll.Logger.GetType() == typeof(Logger));
        }

        [TestMethod]
        public void AddType_ImportProperties_CreateInstance_Test()
        {
            _container.AddType(typeof(Logger));
            _container.AddType(typeof(CustomerBLL2));
            _container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBll = (CustomerBLL2)_container.CreateInstance(typeof(CustomerBLL2));

            Assert.IsNotNull(customerBll);
            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL2));

            Assert.IsNotNull(customerBll.CustomerDAL);
            Assert.IsTrue(customerBll.CustomerDAL.GetType() == typeof(CustomerDAL));

            Assert.IsNotNull(customerBll.Logger);
            Assert.IsTrue(customerBll.Logger.GetType() == typeof(Logger));
        }

        [TestMethod]
        public void AddType_ImportProperties_GenericCreateInstance_Test()
        {
            _container.AddType(typeof(Logger));
            _container.AddType(typeof(CustomerBLL2));
            _container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBll = _container.CreateInstance<CustomerBLL2>();

            Assert.IsNotNull(customerBll);
            Assert.IsTrue(customerBll.GetType() == typeof(CustomerBLL2));

            Assert.IsNotNull(customerBll.CustomerDAL);
            Assert.IsTrue(customerBll.CustomerDAL.GetType() == typeof(CustomerDAL));

            Assert.IsNotNull(customerBll.Logger);
            Assert.IsTrue(customerBll.Logger.GetType() == typeof(Logger));
        }
    }
}
