using API.Data.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Helpers;

public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<IdentityUser, IdentityUserDto>();
        
        CreateMap<StockItem, NewStockItemDto>();
        CreateMap<NewStockItemDto, StockItem>();
        CreateMap<UpdateStockItemDto, StockItem>();

        CreateMap<StockSupplierDto, StockSupplier>();
        CreateMap<StockSupplier, OwnedStockSupplier>();

        CreateMap<StockSupplySourceDto, StockSupplySource>();
    }
}