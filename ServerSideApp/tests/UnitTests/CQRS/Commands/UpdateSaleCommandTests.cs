using ServerSideApp.Application.CQRS.Commans.Update;
using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Exceptions;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CQRS.Commands
{
    public class UpdateSaleCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handler_GivenValidData_ShouldUpdateSale()
        {
            // Arrange
            var sale = new SaleDTO
            {
                Id = 2,
                Date = new DateTime(2019, 01, 01),
                Amount = 600,
            };

            var command = new UpdateSaleCommand { Model = sale };

            // Act
            var handler = new UpdateSaleCommand.UpdateSaleCommandHandler(Context);
            await handler.Handle(command, CancellationToken.None);

            var entity = Context.Sales.Find(sale.Id);

            // Assert
            entity.ShouldNotBeNull();

            entity.Date.ShouldBe(command.Model.Date);
            entity.Amount.ShouldBe(command.Model.Amount);
        }

        [Fact]
        public async Task Handle_GivenInvalidSaleData_ThrowsException()
        {
            // Arrange
            var sale = new SaleDTO
            {
                Id = 99,
                Date = new DateTime(2019, 01, 01),
                Amount = 200,
            };

            var command = new UpdateSaleCommand { Model = sale };

            // Act
            var handler = new UpdateSaleCommand.UpdateSaleCommandHandler(Context);

            // Assert
            await Should.ThrowAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}