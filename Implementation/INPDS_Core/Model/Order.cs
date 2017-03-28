using System;

namespace INPDS_Core.Model
{
    public class Order
    {
        public Order(User customer, DateTime deliveryDeadline, string @from, int id, DateTime pickupDate, string to)
        {
            Customer = customer;
            DeliveryDeadline = deliveryDeadline;
            From = @from;
            Id = id;
            PickupDate = pickupDate;
            To = to;
        }

        public User Customer { get; set; }
        public DateTime DeliveryDeadline { get; set; }
        public string From { get; set; }
        public int Id { get; set; }
        public DateTime PickupDate { get; set; }
        public string To { get; set; }
    }
}