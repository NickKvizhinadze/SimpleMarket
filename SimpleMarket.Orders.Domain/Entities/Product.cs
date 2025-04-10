using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMarket.Orders.Domain.Entities;

[Table("Products")]
public class Product
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Title { get;  set; }
    
    public decimal Price { get; set; }
}