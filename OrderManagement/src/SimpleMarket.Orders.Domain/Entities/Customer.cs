﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleMarket.Orders.Domain.Entities;

[Table("Customers")]
public class Customer
{
    [Key]
    public Guid Id { get; set; }
    
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
}