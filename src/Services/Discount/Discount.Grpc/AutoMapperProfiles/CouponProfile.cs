using AutoMapper;

namespace Discount.Grpc.AutoMapperProfiles;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();

        CreateMap<Coupon, CreateDiscountRequest>().ReverseMap();

        CreateMap<UpdateDiscountRequest, Coupon>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Coupon.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Coupon.ProductName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Coupon.Description))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Coupon.Amount));

        CreateMap<Coupon, DeleteDiscountRequest>().ReverseMap();
    }
}