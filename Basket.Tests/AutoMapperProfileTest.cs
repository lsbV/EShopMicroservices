using AutoMapper;
using Basket.API.Basket.GetBasket;
using Basket.API.Infrastructure;
using Basket.API.Models;

namespace Basket.Tests
{
    public class AutoMapperProfileTest
    {
        private readonly Mapper _mapper;
        public AutoMapperProfileTest()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = new Mapper(configuration);

        }

    }
}
