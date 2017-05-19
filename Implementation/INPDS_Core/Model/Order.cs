using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INPDS_Core.Model
{
    public class Order
    {
        public Order()
        {
            //Empty constructor
        }

        public Order(User customer, DateTime deliveryDeadline, string @from, DateTime pickupDate, string to)
        {
            Customer = customer;
            DeliveryDeadline = deliveryDeadline;
            From = @from;
            PickupDate = pickupDate;
            To = to;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public User Customer { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DeliveryDeadline { get; set; }
        public override string ToString()
        {
            return string.Format("Customer: {0}, From: {2}, To: {4}, PickupDate: {3}, DeliveryDeadline: {1}", Customer,
                DeliveryDeadline, From, PickupDate, To);
        }
    }
}