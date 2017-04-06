using System;
using System.Linq;
using INPDS_Core.Controller;
using INPDS_Core.DTO;
using INPDS_Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPDS_CoreTest
{
    //TODO Implement tests
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public void RegisterOrderTrue()
        {
            var customer = GetCustomer();
            IOrderController con = new OrderController();
            Order order = new Order(customer, new DateTime(2017, 6, 3), "from", new DateTime(2017, 5, 5), "to");


            Assert.IsTrue(con.RegisterOrder(order).IsValid);

        }

        [TestMethod]
        public void RegisterOrderTestPickUpDate()
        {
            var customer = GetCustomer();

            IOrderController con = new OrderController();
            Order order = new Order(customer, new DateTime(2017, 6, 3), "from", new DateTime(2017, 7, 5), "to");

            Assert.IsFalse(con.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullCustomer()
        {
            IOrderController con = new OrderController();
            Order order = new Order(null, new DateTime(2017, 6, 3), "from", new DateTime(2017, 5, 5), "to");

            Assert.IsFalse(con.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullFrom()
        {
            var customer = GetCustomer();

            IOrderController con = new OrderController();
            Order order = new Order(customer, new DateTime(2017, 6, 3), null, new DateTime(2017, 5, 5), "to");

            Assert.IsFalse(con.RegisterOrder(order).IsValid);
        }


        [TestMethod]
        public void RegisterOrderTestNullTo()
        {
            var customer = GetCustomer();

            IOrderController con = new OrderController();
            Order order = new Order(customer, new DateTime(2017, 6, 3), "from", new DateTime(2017, 5, 5), null);

            Assert.IsFalse(con.RegisterOrder(order).IsValid);
        }

        [TestMethod]
        public void RegisterOrderTestNullFromAndTo()
        {
            var customer = GetCustomer();

            IOrderController con = new OrderController();
            Order order = new Order(customer, new DateTime(2017, 6, 3), null, new DateTime(2017, 5, 5), null);

            Assert.AreEqual(1, con.RegisterOrder(order).GetMessages.Count());
        }

        [TestMethod]
        public void RegisterOrderTestPickUpAndDeliveryDate()
        {
            var customer = GetCustomer();

            IOrderController con = new OrderController();
            Order order = new Order(customer, new DateTime(2016, 6, 3), "from", new DateTime(2016, 5, 5), "to");

            Assert.AreEqual(2, con.RegisterOrder(order).GetMessages.Count());
        }

        [TestMethod]
        public void RegisterOrderTestPickUpAndDeliveryDateCustomerFromTo()
        {
            IOrderController con = new OrderController();
            Order order = new Order(null, new DateTime(2016, 6, 3), null, new DateTime(2016, 5, 5), null);

            Assert.AreEqual(4, con.RegisterOrder(order).GetMessages.Count());
        }

        #region Private method

        private User GetCustomer()
        {
            IUserController userController = new UserController();
            userController.Login("user", "pass");
            User customer = userController.LoggedUser;
            return customer;
        }

        #endregion


    }
}