using AutoMapper;
using DotNetHelpers.Extentions;
using DotNetHelpers.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleMarket.Customers.Api.Domain;
using SimpleMarket.Customers.Api.Models;
using SimpleMarket.Customers.Api.Infrastructure.Data;
using SimpleMarket.Customers.Contracts;

namespace SimpleMarket.Customers.Api.Services;

public class CustomersService: ICustomerService
{
    private readonly IMapper _mapper;
    private readonly CustomersDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;

    public CustomersService(IMapper mapper, CustomersDbContext dbContext, IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task<Result<CustomerDetailsDto>> GetCustomer(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (customer is null)
                return Result.BadRequestResult()
                    .WithError("Customer not found", "customer_not_found")
                    .WithEmptyData<CustomerDetailsDto>();

            return Result.SuccessResult().WithData(_mapper.Map<CustomerDetailsDto>(customer));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<CustomerDetailsDto>();
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

            return Result.SuccessResult().WithData(
                new CustomersListDto(_mapper.Map<List<CustomerDto>>(customer), count)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<CustomersListDto>();
        }
    }

    public async Task<Result<Guid>> CreateCustomer(CustomerCreateDto model, CancellationToken cancellationToken)
    {
        try
        {
            var customer = _mapper.Map<Customer>(model);

            await _dbContext.Customers.AddAsync(customer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await PublishUpdatedEvent(customer, cancellationToken);
            
            return Result.SuccessResult().WithData(customer.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<Guid>();
        }
    }

    public async Task<Result<Guid>> UpdateCustomer(Guid id, CustomerUpdateDto model,
        CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (customer is null)
                return Result.InternalErrorResult()
                    .WithError("Customer not found", "customer_not_found")
                    .WithEmptyData<Guid>();

            _mapper.Map(model, customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await PublishUpdatedEvent(customer, cancellationToken);
            
            return Result.SuccessResult().WithData(customer.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message)
                .WithEmptyData<Guid>();
        }
    }
    
    public async Task<Result> DeleteCustomer(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

            if (customer is null)
                return Result.InternalErrorResult()
                    .WithError("Customer not found", "customer_not_found");


            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            await PublishDeletedEvent(id, cancellationToken);
            return Result.SuccessResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.InternalErrorResult()
                .WithError(ex.Message);
        }
    }
    
    #region Private Methods
    private Task PublishUpdatedEvent(Customer customer, CancellationToken cancellationToken)
    {
        return _publishEndpoint.Publish(new CustomerUpdatedEvent
        {
            Id = customer.Id,
            Email = customer.Email,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PersonalNumber = customer.PersonalNumber,
            PhoneNumber = customer.PhoneNumber
        }, cancellationToken);
    } 
    
    private Task PublishDeletedEvent(Guid id, CancellationToken cancellationToken)
    {
        return _publishEndpoint.Publish(new CustomerDeletedEvent
        {
            Id = id
        }, cancellationToken);
    }
    #endregion
}