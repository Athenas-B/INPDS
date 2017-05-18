using System;
using INPDS_Core.Controller;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;

namespace INPDS_CoreTest
{
    public static class TestUtils
    {
        private static User _customer;

        public static DateTime Tomorrow
        {
            get { return DateTime.Now.AddDays(1); }
        }

        public static DateTime NextWeek
        {
            get { return DateTime.Now.AddDays(7); }
        }

        public static User Customer
        {
            get
            {
                if (_customer == null)
                {
                    IUserController userController = UserController.Instance;
                    userController.Login("customer", "pass");
                    _customer = userController.LoggedUser;
                }
                return _customer;
            }
        }

        public static void DeleteTestInovice(Invoice invoice)
        {
            using (var context = new ReturnFreightContext())
            {
                context.Invoices.Attach(invoice);
                context.Invoices.Remove(invoice);
                context.TrySaveChanges();
            }
        }

        public static Order CreateTestOrder(DateTime deadline, DateTime pickupDate, string from = "from", string to = "to")
        {
            var order = new Order(Customer, deadline, from, pickupDate, to);
            using (var context = new ReturnFreightContext())
            {
                context.Users.Attach(order.Customer);
                context.Orders.Add(order);
                context.TrySaveChanges();
            }
            return order;
        }

        public static Order CreateTestOrder(string from = "from", string to = "to")
        {
            return CreateTestOrder(NextWeek, Tomorrow);
        }

        public static void DeleteTestOrder(Order o)
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
    }
}