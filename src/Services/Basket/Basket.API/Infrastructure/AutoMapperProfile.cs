using Basket.API.Basket.DeleteBasket;
using Basket.API.Basket.GetBasket;
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {


        CreateMap<GetBasketResult, GetBasketResponse>()
            .ForCtorParam(nameof(GetBasketResponse.Cart), opt => opt.MapFrom(src => src.Cart));

        CreateMap<ShoppingCart, GetBasketResult>()
            .ForCtorParam(nameof(GetBasketResult.Cart), opt => opt.MapFrom(src => src));


        CreateMap<StoreBasketRequest, StoreBasketCommand>()
            .ForCtorParam(nameof(StoreBasketCommand.Cart), opt => opt.MapFrom(src => src.Cart));

        CreateMap<StoreBasketResult, StoreBasketResponse>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

        CreateMap<DeleteBasketResult, DeleteBasketResponse>()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

    }
}