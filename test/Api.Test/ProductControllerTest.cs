using MediatR;
using System.Threading.Tasks;
using Xunit;

namespace OSRS.Api.Test
{
    public class ProductControllerTest
    {
        private readonly IMediator _mediator;

        
        public ProductControllerTest(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Theory]
        [InlineData(1, 20)]
        public async Task GetByIdAsyncTest(int id1, int id2)
        {
            //arrange
            var validId = id1;
            var invalidId = id2;

            //act

            //assert

            //var item = result.Data as ProductQueryModel;
            //Assert.IsType<ProductQueryModel>(item);

            //var productItem = item as ProductQueryModel;
            //Assert.Equal(validId, productItem.ProductId);
        }
    }
}
