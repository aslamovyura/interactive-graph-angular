using ServerSideApp.Application.CQRS.Commans.Create;
using ServerSideApp.Application.DTO;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CQRS.Commands
{
    public class CreateSaleCommandTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handler_ShouldAddSale()
        {
            // Arrange
            var sale = new SaleDTO
            {
                Id = 5,
                Date = new DateTime(2020, 03, 01),
                Amount = 1000,
            };

            var command = new CreateSaleCommand { Model = sale };

            // Act
            var handler = new CreateSaleCommand.CreateSaleCommandHandler(Context, Mapper);
            await handler.Handle(command, CancellationToken.None);
            var entity = Context.Sales.Find(sale.Id);

            // Assert
            entity.ShouldNotBeNull();

            entity.Id.ShouldBe(command.Model.Id);
            entity.Date.ShouldBe(command.Model.Date);
            entity.Amount.ShouldBe(command.Model.Amount);
        }
    }
}