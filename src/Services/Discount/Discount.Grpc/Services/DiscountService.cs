namespace Discount.Grpc.Services;

public class DiscountService(ICouponRepository repository, IMapper mapper, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await repository.GetCoupon(request.ProductName);
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        }
        logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
        var couponModel = mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = mapper.Map<Coupon>(request);
        await repository.CreateCoupon(coupon);
        logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);
        var couponModel = mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = mapper.Map<Coupon>(request);
        await repository.UpdateCoupon(coupon);
        logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);
        var couponModel = mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleted = await repository.DeleteCoupon(request.ProductName);
        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };
        return response;
    }

}