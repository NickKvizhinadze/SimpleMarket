using System.ComponentModel.DataAnnotations;
using SimpleMarket.Customers.Api.Domain;

namespace SimpleMarket.Customers.Api.Models;

public class CustomerCreateDto
{
    [Required]
    public required string Email { get; set; }
    
    public string? Avatar { get; set; }
 
    [Required]
    public required string FirstName { get; set; }
 
    [Required]
    public required string LastName { get; set; }
 
    [Required]
    public required string PersonalNumber { get; set; }
 
    [Required]
    public required string PhoneNumber { get; set; }

    public DateTime BirthDate { get; set; }

    public Gender Gender { get; set; }
}