using System;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;

namespace INPDS_InitialData
{
    internal class Program
    {
        private static User _customer;

        private static void Main(string[] args)
        {
            Console.WriteLine("Loading initial data...");
            Database.SetInitializer(new DropCreateDatabaseAlways<ReturnFreightContext>());
            using (var context = new ReturnFreightContext())
            {
                Console.WriteLine("Initializing users...");
                InitializeUsers(context);
                Console.WriteLine("Done initializing users.");
                Console.WriteLine("Initializing dummy orders...");
                InitializeOrders(context);
                Console.WriteLine("Done initializing dummy orders.");
                Console.WriteLine("Saving to DB...");
                context.SaveChanges();
            }
            Console.WriteLine("Done loading initial data.");
        }

        private static void InitializeUsers(ReturnFreightContext context)
        {
            using (SHA512 sha = new SHA512Managed())
            {
                _customer = CreateUser("customer", sha, "pass", UserRole.Customer);
                context.Users.Add(_customer);
                context.Users.Add(CreateUser("accountant", sha, "pass", UserRole.Accountant));
                context.Users.Add(CreateUser("planner", sha, "pass", UserRole.Planner));
            }
        }

        private static void InitializeOrders(ReturnFreightContext context)
        {
            context.Orders.Add(new Order
            {
                Customer = _customer,
                DeliveryDeadline = DateTime.Now.AddDays(30),
                PickupDate = DateTime.Now.AddDays(7),
                From = "Pardubice",
                To = "Brno"
            });

            context.Orders.Add(new Order
            {
                Customer = _customer,
                DeliveryDeadline = DateTime.Now.AddDays(60),
                PickupDate = DateTime.Now.AddDays(35),
                From = "Brno",
                To = "Pardubice"
            });

            context.Orders.Add(new Order
            {
                Customer = _customer,
                DeliveryDeadline = DateTime.Now.AddDays(60),
                PickupDate = DateTime.Now.AddDays(32),
                From = "Hradec Králové",
                To = "Pardubice"
            });
        }

        private static User CreateUser(string userName, SHA512 sha, string pass, UserRole role)
        {
            var encoding = Encoding.UTF8;
            var entity = new User(userName, encoding.GetString(sha.ComputeHash(encoding.GetBytes(pass))), role);
            return entity;
        }
    }
}