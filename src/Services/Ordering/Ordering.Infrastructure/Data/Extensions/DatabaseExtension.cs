using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DatabaseExtension
    {
        public static async Task InitialiseDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();
            await context.Database.MigrateAsync();
            await SeedAsync(context);
        }
        private static async Task SeedAsync(OrderingDbContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync(GetPreconfiguredCustomers());
                await context.SaveChangesAsync();
            }

            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(GetPreconfiguredProducts());
                await context.SaveChangesAsync();
            }

            if (!await context.Orders.AnyAsync())
            {
                await context.Orders.AddRangeAsync(GetPreconfiguredOrders());
                await context.SaveChangesAsync();
            }



        }

        private static IEnumerable<Customer> GetPreconfiguredCustomers()
        {
            return new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("0B442A7E-33F2-4F2B-9A94-9FCD433AF808")),"John","john@mail.com" ),
                Customer.Create(CustomerId.Of(new Guid("98446BF3-ECD7-4F98-8090-3E2BEF46CE5D")),"Sam","Sam@mail.com"),
                Customer.Create(CustomerId.Of(new Guid("34BC5A08-5EB0-4E26-824E-5EECFEF5DE1C")),"Tom","Tom@mail.com"),
                Customer.Create(CustomerId.Of(new Guid("B977B420-F77D-4174-8F58-44AE38B08E67")),"Alice","Alice@mail.com"),

            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("2BACA16F-2320-4D06-AF30-3507C94F93F7")),"Product1", 100),
                Product.Create(ProductId.Of(new Guid("16626F49-0CE0-4820-A92B-C43ABCD4B35E")),"Product2", 200),
                Product.Create(ProductId.Of(new Guid("BEB028AA-60D2-4696-A23B-C7BE47E3CC72")),"Product3", 300),
                Product.Create(ProductId.Of(new Guid("29CF1FC2-0A3A-428C-B11A-DC1D38A999FB")),"Product4", 400),
            };
        }


        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            List<Order> orders =
            [
                Order.Create(
                    OrderId.Of(new Guid("CF0D16D7-F0C4-43E7-9EA3-CAF9E785F86A")),
                    CustomerId.Of(new Guid("0B442A7E-33F2-4F2B-9A94-9FCD433AF808")),
                    OrderName.Of("q1w2e"),
                    Address.Of("John", "Doe", "ShippingStreet1", "ShippingCity1", "ShippingState1", "ShippingCountry1", "12345"),
                    Address.Of("John", "Doe", "BillingStreet1", "BillingCity1", "BillingState1", "BillingCountry1", "12345"),
                    OrderStatus.Pending,
                    Payment.Of("1111222233334444", "Name1", DateTime.UtcNow + new TimeSpan(1000, 0, 0, 0), "123",
                        123)),
            ];
            orders[0].AddOrderItem(ProductId.Of(new Guid("2BACA16F-2320-4D06-AF30-3507C94F93F7")), 100, 1);
            orders[0].AddOrderItem(ProductId.Of(new Guid("16626F49-0CE0-4820-A92B-C43ABCD4B35E")), 200, 2);
            orders[0].AddOrderItem(ProductId.Of(new Guid("BEB028AA-60D2-4696-A23B-C7BE47E3CC72")), 300, 3);

            return orders;
        }


    }


}
