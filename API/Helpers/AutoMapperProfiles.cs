using API.Data.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, AppUserDto>();
        CreateMap<NewUserDto, AppUser>();
        
        CreateMap<StockItem, NewStockItemDto>();
        CreateMap<NewStockItemDto, StockItem>();
        CreateMap<UpdateStockItemDto, StockItem>();

        CreateMap<StockSupplierDto, StockSupplier>();
        CreateMap<StockSupplier, OwnedStockSupplier>();

        CreateMap<StockSupplySourceDto, StockSupplySource>();

        CreateMap<NewPriceBreakDto, PriceBreak>();
    }
}