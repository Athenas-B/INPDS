using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INPDS_Core.Model
{
    public class Invoice
    {
        public Invoice()
        {
            //Empty constructor
        }

        public Invoice(Order order, double price)
        {
            Order = order;
            Price = price;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Order Order { get; set; }
        public double Price { get; set; }
    }
}
