using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPDS_Core.Model
{
    public class Trip
    {
        public Trip()
        {
            //Empty constructor
        }

        public Trip(Order primaryOrder, Order secondaryOrder = null)
        {
            PrimaryOrder = primaryOrder;
            SecondaryOrder = secondaryOrder;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Order PrimaryOrder { get; set; }

        public Order SecondaryOrder { get; set; }

        public override string ToString()
        {
            return string.Format("PrimaryOrder: {0}, SecondaryOrder: {1}", PrimaryOrder, SecondaryOrder);
        }
    }
}
