using ServerSideApp.Application.CQRS.Queries.Get;
using ServerSideApp.Application.DTO;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.CQRS.Queries
{
    public class GetSalessQueryTests : BaseTestsFixture
    {
        [Fact]
        public async Task Handler_ReturnsSalesDTOCollection()
        {
            // Arrange
            var query = new GetSalesQuery();

            // Act
            var handler = new GetSalesQuery.GetSalesQueryHandler(Context, Mapper);

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<List<SaleDTO>>();
            result.ShouldNotBeNull();
        }
    }
}