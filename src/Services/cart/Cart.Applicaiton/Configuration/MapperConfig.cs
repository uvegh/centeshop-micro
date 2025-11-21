using AutoMapper;
using Cart.Domain.Entities;



namespace Catalog.API.Configuration;

public class MapperConfig:Profile
{

    public MapperConfig()
    {
        CreateMap<CartDto, CartEntity>().ReverseMap();
        CreateMap<CartItemDto, CartItem>().ReverseMap();

    }
}
