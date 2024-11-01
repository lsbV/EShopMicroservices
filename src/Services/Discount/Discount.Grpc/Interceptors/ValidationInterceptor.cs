using FluentValidation;
using Grpc.Core.Interceptors;

namespace Discount.Grpc.Interceptors;

public class ValidationInterceptor(IServiceProvider serviceProvider)
    : Interceptor
{
    public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var validator = serviceProvider.GetService<IValidator<TRequest>>();
        if (validator is null) return base.UnaryServerHandler(request, context, continuation);

        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, validationResult.ToString()));
        }
        return base.UnaryServerHandler(request, context, continuation);
    }
}