using API.Data.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Helpers;

public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<IdentityUser, IdentityUserDto>();
    }
}