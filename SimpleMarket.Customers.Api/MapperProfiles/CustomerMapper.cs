using AutoMapper;
using SimpleMarket.Customers.Api.Domain;
using SimpleMarket.Customers.Api.Models;

namespace SimpleMarket.Customers.Api.MapperProfiles;

public class CustomerMapper: Profile
{
    public CustomerMapper()
    {
        CreateMap<CustomerCreateDto, Customer>();
        CreateMap<CustomerUpdateDto, Customer>();
        
        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerDetailsDto>();
    }
}