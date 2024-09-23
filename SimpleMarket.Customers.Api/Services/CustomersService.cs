using AutoMapper;
using DotNetHelpers.Models;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Customers.Api.Domain;
using SimpleMarket.Customers.Api.Models;
using SimpleMarket.Customers.Api.Infrastructure.Data;

namespace SimpleMarket.Customers.Api.Services;

public class CustomersService: ICustomerService
{
    private readonly IMapper _mapper;
    private readonly CustomersDbContext _dbContext;

    public CustomersService(IMapper mapper, CustomersDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<Result<CustomerDetailsDto>> GetCustomer(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (customer is null)
                return Result.Error<CustomerDetailsDto>("customer_not_found", "Customer not found");

            return Result.Success(_mapper.Map<CustomerDetailsDto>(customer));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error<CustomerDetailsDto>(ex.Message);
        }
    }

    public async Task<Result<CustomersListDto>> GetCustomers(Paging paging, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _dbContext.Customers
                .Skip(paging.PerPage * (paging.CurrentPage - 1))
                .Take(paging.CurrentPage)
                .ToListAsync(cancellationToken);

            var count = await _dbContext.Customers.CountAsync(cancellationToken);

            return Result.Success(
                new CustomersListDto(_mapper.Map<List<CustomerDto>>(customer), count)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error<CustomersListDto>(ex.Message);
        }
    }

    public async Task<Result<Guid>> CreateCustomer(CustomerCreateDto model, CancellationToken cancellationToken)
    {
        try
        {
            var customer = _mapper.Map<Customer>(model);

            await _dbContext.Customers.AddAsync(customer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(customer.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error<Guid>(ex.Message);
        }
    }

    public async Task<Result<Guid>> UpdateCustomer(Guid id, CustomerUpdateDto model,
        CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (customer is null)
                return Result.Error<Guid>("customer_not_found", "Customer not found");

            _mapper.Map(model, customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(customer.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error<Guid>(ex.Message);
        }
    }
    
    public async Task<Result> DeleteCustomers(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (customer is null)
                return Result.Error("customer_not_found", "Customer not found");


            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error(ex.Message);
        }
    }
}