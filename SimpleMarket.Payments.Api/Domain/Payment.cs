using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMarket.Payments.Api.Domain;

[Table("Payments")]
public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public Guid CorrelationId { get; set; }

    public Guid CustomerId { get; set; }

    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}