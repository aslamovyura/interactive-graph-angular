
using Application.CQRS.Queries.Get;
using ServerSideApp.Application.DTO;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CQRS.Queries
{
    public class GetSaleQueryTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handle_GivenValidId_ReturnsSaleDTO()
        {
            // Arrange
            var sale = new SaleDTO
            {
                Id = 2,
                Date = new DateTime(2020, 01, 01),
                Amount = 1200,
            };

            var query = new GetSaleQuery { Id = 2 };

            // Act
            var handler = new GetSaleQuery.GetSaleQueryHandler(Context, Mapper);
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<SaleDTO>();
            result.ShouldNotBeNull();

            result.Id.ShouldBe(sale.Id);
            result.Date.ShouldBe(sale.Date);
            result.Amount.ShouldBe(sale.Amount);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ReturnsNull()
        {
            // Arrange
            var query = new GetSaleQuery { Id = 99 };

            // Act
            var handler = new GetSaleQuery.GetSaleQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeNull();
        }
    }
}