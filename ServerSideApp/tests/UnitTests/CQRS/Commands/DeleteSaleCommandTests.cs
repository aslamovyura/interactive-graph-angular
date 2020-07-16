using ServerSideApp.Application.CQRS.Commans.Delete;
using ServerSideApp.Application.Exceptions;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CQRS.Commands
{
    public class DeleteSaleCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handler_GivenValidSaleId_ShouldRemoveSale()
        {
            // Arrange
            var validSaleId = 3;

            // Act
            var command = new DeleteSaleCommand { Id = validSaleId };
            var handler = new DeleteSaleCommand.DeleteSaleCommandHandler(Context);
            await handler.Handle(command, CancellationToken.None);

            // Assert
            var entity = Context.Sales.Find(command.Id);
            entity.ShouldBeNull();
        }

        [Fact]
        public void Handler_GivenInvalidSaleId_ThrowsException()
        {
            // Arrange
            var invalidSaleId = 99;

            // Act
            var command = new DeleteSaleCommand { Id = invalidSaleId };
            var handler = new DeleteSaleCommand.DeleteSaleCommandHandler(Context);

            // Assert
            Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}