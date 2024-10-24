using AutoMapper;
using Basket.API.Basket.GetBasket;
using Basket.API.Infrastructure;
using Basket.API.Models;

namespace Basket.Tests
{
    public class AutoMapperProfileTest
    {
        private readonly IMapper _mapper;
        public AutoMapperProfileTest()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = new Mapper(configuration);

        }
        [Fact]
        public void TestMapShoppingCartItemIntoBasketItemResponse()
        {
            var shoppingCart = new ShoppingCartItem()
            {
                Color = "Red",
                Price = 11,
                ProductId = Guid.NewGuid(),
                ProductName = "product",
                ProductPictureUrl = "url.png",
                Quantity = 1
            };


            var result = _mapper.Map<BasketItemResponse>(shoppingCart);

            Assert.Equal(result.ProductName, shoppingCart.ProductName);
            Assert.Equal(result.Price, shoppingCart.Price);
            Assert.Equal(result.Color, shoppingCart.Color);
            Assert.Equal(result.ImageUrl, shoppingCart.ProductPictureUrl);
            Assert.Equal(result.Quantity, shoppingCart.Quantity);
        }

        [Fact]
        public void TestMapGetBasketResultToGetBasketResponse()
        {
            var getBasketResult = new GetBasketResult(
                new ShoppingCart(userName: "user")
                {
                    Items =
                [
                    new ShoppingCartItem()
                    {
                        Color = "Red",
                        Price = 11,
                        ProductId = Guid.NewGuid(),
                        ProductName = "product",
                        ProductPictureUrl = "url.png",
                        Quantity = 1
                    }
                ]
                });
            var result = _mapper.Map<GetBasketResponse>(getBasketResult);
            Assert.Equal(result.UserName, getBasketResult.Cart.UserName);
            Assert.Equal(result.TotalPrice, getBasketResult.Cart.TotalPrice);
            Assert.Equal(result.Items.Count, getBasketResult.Cart.Items.Count);
        }

    }
}
