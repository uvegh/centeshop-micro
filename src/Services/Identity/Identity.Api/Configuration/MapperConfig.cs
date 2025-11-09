
using Identity.Api.DTOs;
using Identity.Domain.Entities;


namespace Identity.API.Configuration;

public class MapperConfig:Profile
{

    public MapperConfig()
    {
        CreateMap<LoginRequestDto, ApiUser>().ReverseMap();

        CreateMap<LoginRequestDto, ApiUser>().ReverseMap();

    }
}
