namespace SimpleMarket.Customers.Api.Models;

public class CustomersListDto
{
    public CustomersListDto(List<CustomerDto> customers, int totalCount)
    {
        Customers = customers;
        TotalCount = totalCount;
    }

    public List<CustomerDto> Customers { get; set; }
    public int TotalCount { get; set; }
}