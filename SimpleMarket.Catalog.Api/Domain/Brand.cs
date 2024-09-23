using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMarket.Catalog.Api.Domain;

[Table("Brands")]
public class Brand
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Title { get; set; }
    
    
    #region Navigation Properties
    public ICollection<Product>? Products { get; set; }
    #endregion
}