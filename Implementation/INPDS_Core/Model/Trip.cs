using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [DisplayName("Zakázka cesty")]
        [Required]
        public Order PrimaryOrder { get; set; }

        [DisplayName("Zakázka zpáteční cesty")]
        public Order SecondaryOrder { get; set; }

        public override string ToString()
        {
            return string.Format("Zakázka cesty: {0}, Zakázka zpáteční cesty: {1}", PrimaryOrder, SecondaryOrder);
        }
    }
}
