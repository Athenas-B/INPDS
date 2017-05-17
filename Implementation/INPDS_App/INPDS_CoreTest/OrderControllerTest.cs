using System;
using System.Linq;
using INPDS_Core.Controller;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPDS_CoreTest
{
    [TestClass]
    public class OrderControllerTest
    {
        private readonly IOrderController _orderController = new OrderController();

        [TestMethod]
        public void RegisterOrderTrue()
        {
            var customer = TestUtils.Customer;
            var order = new Order(customer, TestUtils.NextWeek, "from", TestUtils.Tomorrow, "to");
            Assert.IsTrue(_orderController.RegisterOrder(order).IsValid);
            TestUtils.DeleteTestOrder(order);
        }

        [TestMethod]
        public void RegisterOrderTestPickUpDate()
        {
            var customer = TestUtils.Customer;
            var order = new Order(customer, TestUtils.NextWeek, "from", DateTime.Now.AddDays(-1), "to");
            Assert.IsFalse(_orderController.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullCustomer()
        {
            var order = new Order(null, TestUtils.NextWeek, "from", TestUtils.Tomorrow, "to");
            Assert.IsFalse(_orderController.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullFrom()
        {
            var customer = TestUtils.Customer;
            var order = new Order(customer, TestUtils.NextWeek, null, TestUtils.Tomorrow, "to");
            Assert.IsFalse(_orderController.RegisterOrder(order).IsValid);
        }


        [TestMethod]
        public void RegisterOrderTestNullTo()
        {
            var customer = TestUtils.Customer;
            var order = new Order(customer, TestUtils.NextWeek, "from", TestUtils.Tomorrow, null);
            Assert.IsFalse(_orderController.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullFromAndTo()
        {
            var customer = TestUtils.Customer;
            var order = new Order(customer, TestUtils.NextWeek, null, TestUtils.Tomorrow, null);
            var messages = _orderController.RegisterOrder(order).GetMessages;
            Assert.AreEqual(1, messages.Count());
        }

        [TestMethod]
        public void RegisterOrderTestPickUpAndDeliveryDate()
        {
            var customer = TestUtils.Customer;
            var order = new Order(customer, DateTime.Now.AddDays(-1), "from", DateTime.Now.AddDays(-10), "to");
            Assert.AreEqual(2, _orderController.RegisterOrder(order).GetMessages.Count());
        }

        [TestMethod]
        public void RegisterOrderTestPickUpAndDeliveryDateCustomerFromTo()
        {
            var order = new Order(null, DateTime.Now.AddDays(-1), null, DateTime.Now.AddDays(-10), null);
            Assert.AreEqual(4, _orderController.RegisterOrder(order).GetMessages.Count());
        }

        [TestMethod]
        public void RegisterOrderNotPersistedUser()
        {
            var order = new Order(new User("newuser", "password", UserRole.Planner), DateTime.Now.AddDays(2), "a",
                DateTime.Now.AddDays(1), "b");
            _orderController.RegisterOrder(order);
            using (var context = new ReturnFreightContext())
            {
                var count = context.Users.Count(user => user.UserName == "newuser" && user.UserRole == UserRole.Planner);
                Assert.AreEqual(0, count);
            }
        }
    }
}