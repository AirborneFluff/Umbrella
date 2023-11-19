using API.Data.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<NewProductComponentDto, ProductComponent>();
    }
}