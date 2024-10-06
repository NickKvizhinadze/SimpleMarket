using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMarket.Orders.Api.Domain;

[Table("Orders")]
public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid ShippingAddressId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    [Column(TypeName = "Money")]
    public decimal Amount { get; set; }
    [Column(TypeName = "Money")]
    public decimal ShippingAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderState State { get; set; }

    [ForeignKey("ShippingAddressId")]
    public virtual Address ShippingAddress { get; set; }
    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; }


    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public decimal DiscountedAmount { get; set; }
}
