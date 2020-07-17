using ServerSideApp.Application.CQRS.Queries.Get;
using ServerSideApp.Application.DTO;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CQRS.Queries
{
    public class GetSalesByDateQueryTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handler_ReturnsSalesDTOCollection()
        {
            // Arrange
            var startDate = DateTime.Parse("2018-01-01");
            var endDate = DateTime.Parse("2021-01-01");
            var query = new GetSalesByDateQuery { StartDate = startDate, EndDate = endDate };

            // Act
            var handler = new GetSalesByDateQuery.GetSalesByDateQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            (result as List<SaleDTO>).Count.ShouldBeGreaterThanOrEqualTo(4);
            result.ShouldBeOfType<List<SaleDTO>>();
            result.ShouldNotBeNull();
            
        }
    }
}