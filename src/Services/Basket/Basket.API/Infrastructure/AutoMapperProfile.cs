using Basket.API.Basket.DeleteBasket;
using Basket.API.Basket.GetBasket;
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ShoppingCartItem, BasketItemResponse>()
                //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                //.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                //.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ProductPictureUrl))
                //.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
                .ConstructUsing(src => new BasketItemResponse(src.Quantity,
                    src.ProductName,
                    src.Price,
                    src.Color,
                    src.ProductPictureUrl
                ));

            CreateMap<GetBasketResult, GetBasketResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Cart.UserName))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Cart.Items))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Cart.TotalPrice));

            CreateMap<StoreBasketRequest, StoreBasketCommand>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<StoreBasketResult, StoreBasketResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));

            CreateMap<DeleteBasketResult, DeleteBasketResponse>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
        }
    }
}
