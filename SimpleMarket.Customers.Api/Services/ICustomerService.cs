using DotNetHelpers.Models;
using SimpleMarket.Customers.Api.Models;

namespace SimpleMarket.Customers.Api.Services;

public interface ICustomerService
{
    Task<Result<CustomersListDto>> GetCustomers(Paging paging, CancellationToken cancellationToken);
    Task<Result<CustomerDetailsDto>> GetCustomer(Guid id, CancellationToken cancellationToken);
    Task<Result<Guid>> CreateCustomer(CustomerCreateDto model, CancellationToken cancellationToken);
    Task<Result<Guid>> UpdateCustomer(Guid id, CustomerUpdateDto model, CancellationToken cancellationToken);
    Task<Result> DeleteCustomer(Guid id, CancellationToken cancellationToken);
}