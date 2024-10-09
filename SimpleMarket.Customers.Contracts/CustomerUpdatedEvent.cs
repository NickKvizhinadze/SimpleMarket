namespace SimpleMarket.Customers.Contracts;

public class CustomerUpdatedEvent
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PersonalNumber { get; set; }
    public required string PhoneNumber { get; set; }
}