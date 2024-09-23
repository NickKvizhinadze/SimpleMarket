using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMarket.Catalog.Api.Domain;

[Table("Customers")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string Title { get;  set; }
    
    [Required]
    public required string Description { get; set; }
    
    public decimal Price { get; set; }
    
    
    public int BrandId { get; set; }
    
    public int CategoryId { get; set; }
    
    
    #region Navigation Properties
    [ForeignKey("BrandId")]
    public Brand Brand { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    #endregion
}