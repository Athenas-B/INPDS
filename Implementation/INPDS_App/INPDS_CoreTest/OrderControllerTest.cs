using System;
using System.Linq;
using INPDS_Core.Controller;
using INPDS_Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPDS_CoreTest
{
    //TODO Implement tests
    [TestClass]
    public class OrderControllerTest
    {
        private readonly IOrderController _orderController = new OrderController();

        [TestMethod]
        public void RegisterOrderTrue()
        {
            var customer = GetCustomer();
            var order = new Order(customer, new DateTime(2017, 6, 3), "from", new DateTime(2017, 5, 5), "to");
            Assert.IsTrue(_orderController.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestPickUpDate()
        {
            var customer = GetCustomer();
            var order = new Order(customer, new DateTime(2017, 6, 3), "from", new DateTime(2017, 7, 5), "to");
            Assert.IsFalse(_orderController.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullCustomer()
        {
            var order = new Order(null, new DateTime(2017, 6, 3), "from", new DateTime(2017, 5, 5), "to");
            Assert.IsFalse(_orderController.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullFrom()
        {
            var customer = GetCustomer();
            var order = new Order(customer, new DateTime(2017, 6, 3), null, new DateTime(2017, 5, 5), "to");
            Assert.IsFalse(_orderController.RegisterOrder(order).IsValid);
        }


        [TestMethod]
        public void RegisterOrderTestNullTo()
        {
            var customer = GetCustomer();
            var order = new Order(customer, new DateTime(2017, 6, 3), "from", new DateTime(2017, 5, 5), null);
            Assert.IsFalse(_orderController.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullFromAndTo()
        {
            var customer = GetCustomer();
            var order = new Order(customer, new DateTime(2017, 6, 3), null, new DateTime(2017, 5, 5), null);
            Assert.AreEqual(1, _orderController.RegisterOrder(order).GetMessages.Count());
        }

        [TestMethod]
        public void RegisterOrderTestPickUpAndDeliveryDate()
        {
            var customer = GetCustomer();
            var order = new Order(customer, new DateTime(2016, 6, 3), "from", new DateTime(2016, 5, 5), "to");
            Assert.AreEqual(2, _orderController.RegisterOrder(order).GetMessages.Count());
        }

        [TestMethod]
        public void RegisterOrderTestPickUpAndDeliveryDateCustomerFromTo()
        {
            var order = new Order(null, new DateTime(2016, 6, 3), null, new DateTime(2016, 5, 5), null);
            Assert.AreEqual(4, _orderController.RegisterOrder(order).GetMessages.Count());
        }

        [TestMethod]
        public void RegisterOrderNotPersistedUser()
        {
            var order = new Order(new User("newuser", "password", UserRole.Planner), DateTime.Now.AddDays(2), "a",
                DateTime.Now.AddDays(1), "b");
            _orderController.RegisterOrder(order);
        }

        #region Private method

        private User GetCustomer()
        {
            IUserController userController = new UserController();
            userController.Login("customer", "pass");
            var customer = userController.LoggedUser;
            return customer;
        }

        #endregion
    }
}