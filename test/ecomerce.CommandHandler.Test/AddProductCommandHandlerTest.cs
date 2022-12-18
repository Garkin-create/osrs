using ecomerce.Application.Products.Command.AddProduct;
using ecomerce.Common.Exceptions;
using ecomerce.Persistance.CommandHandlers.Products;
using ecomerce.Persistance.Db;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ecomerce.CommandHandler.Test
{
    public class AddProductCommandHandlerTest
    {
        [Fact]
        public async Task Should_ThrowException_When_InputIsNull()
        {
            var dbContext = new Mock<CleanArchWriteDbContext>();
            var logger = new Mock<ILogger<AddProductCommandHandler>>();

            var commandHandler = new AddProductCommandHandler(dbContext.Object, logger.Object);

            var request = new Mock<AddProductCommand>();

            //await Assert.ThrowsAsync<InvalidNullInputException>(() => commandHandler.Handle(request.Object, CancellationToken.None));
            await Assert.ThrowsAsync<InvalidNullInputException>(() => commandHandler.Handle(null, CancellationToken.None));
        }
    }
}