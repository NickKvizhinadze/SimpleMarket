using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMarket.Customers.Api.Domain;

[Table("Customers")]
public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string? Avatar { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string PersonalNumber { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public DateTime BirthDate { get; set; }

    [Required]
    public Gender Gender { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
}