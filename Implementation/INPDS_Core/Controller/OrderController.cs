using System;
using INPDS_Core.DataAccess;
using INPDS_Core.Model;

namespace INPDS_Core.Controller
{
    public class OrderController : IOrderController
    {
        public void RegisterOrder(Order order)
        {
            if (IsValid(order))
            {
                using (var context = new ReturnFreightContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
        }

        private static bool IsValid(Order order)
        {
            if (order.Customer == null)
            {
                return false;
            }
            if (DateTime.Now > order.DeliveryDeadline || DateTime.Now > order.PickupDate || order.PickupDate > order.DeliveryDeadline)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(order.From) || string.IsNullOrWhiteSpace(order.To))
            {
                return false;
            }
            return true;
        }
    }
}