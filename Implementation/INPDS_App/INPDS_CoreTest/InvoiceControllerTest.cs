using System;
using System.Data.Entity;
using INPDS_Core.Controller;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPDS_CoreTest
{
    //TODO Implement tests
    [TestClass]
    public class InvoiceControllerTest
    {
        private static Order o;

        [TestMethod]
        public void RegisterInvoiceTestTrue()
        {
            Invoice testInvoice = new Invoice(o, 1000);
            InvoiceController invoiceController = new InvoiceController();
            var vysledek = invoiceController.RegisterInvoice(testInvoice);
            Assert.IsTrue(vysledek.IsValid);
            DeleteTestInovice(testInvoice);
        }

        [TestMethod]
        public void RegisterInvoiceTestZeroPrice()
        {
            Invoice testInvoice = new Invoice(o, 0);
            InvoiceController invoiceController = new InvoiceController();

            Assert.IsTrue(invoiceController.RegisterInvoice(testInvoice).IsValid);
            DeleteTestInovice(testInvoice);
        }

        [TestMethod]
        public void RegisterInvoiceTestPrice()
        {
            Invoice testInvoice = new Invoice(o, -10);
            InvoiceController invoiceController = new InvoiceController();

            Assert.IsFalse(invoiceController.RegisterInvoice(testInvoice).IsValid);
        }


        #region Private method

        private static User GetCustomer()
        {
            IUserController userController = new UserController();
            userController.Login("customer", "pass");
            var customer = userController.LoggedUser;
            return customer;
        }

        private void DeleteTestInovice(Invoice i)
        {
            using (var context = new ReturnFreightContext())
            {
                context.Invoices.Attach(i);
                context.Invoices.Remove(i);
                context.TrySaveChanges();
            }
        }

        [ClassInitialize()]
        public static void Initialize(TestContext con)
        {
            var order = new Order(GetCustomer(), new DateTime(2017, 6, 3), "testOrder", new DateTime(2017, 5, 5), "to");
            using (var context = new ReturnFreightContext())
            {
                context.Users.Attach(order.Customer);
                context.Orders.Add(order);
                context.TrySaveChanges();
            }
            o = order;
        }

        [ClassCleanup()]
        public static void DeleteTestOrder()
        {
            if (o != null)
            {
                using (var context = new ReturnFreightContext())
                {
                    context.Orders.Attach(o);
                    context.Orders.Remove(o);
                    context.TrySaveChanges();
                }
            }
        }

        #endregion
    }
}