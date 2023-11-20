using API.Data.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Helpers;

public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<NewProductComponentDto, ProductComponent>();
        CreateMap<IdentityUser, IdentityUserDto>();
    }
}