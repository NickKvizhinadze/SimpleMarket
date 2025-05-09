using AutoMapper;
using SimpleMarket.Catalog.Api.Domain;
using SimpleMarket.Catalog.Api.Models;

namespace SimpleMarket.Catalog.Api.Mappers;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<Product, ProductDetailsDto>();
        CreateMap<Product, ProductDto>();
    }
}