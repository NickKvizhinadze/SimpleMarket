using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMarket.Orders.Domain.Entities
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int City { get; set; }
        [Required]
        [MaxLength(500)]
        public string StreetAddress { get; set; }
        [Required]
        [MaxLength(50)]
        public string ZipCode { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }

    }
}
