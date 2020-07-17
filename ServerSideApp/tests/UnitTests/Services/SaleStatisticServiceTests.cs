using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using ServerSideApp.Application.CQRS.Queries.Get;
using ServerSideApp.Application.Enums;
using ServerSideApp.Infrastructure.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnitTests.Controllers;
using Xunit;

namespace UnitTests.Services
{
    public class SaleStatisticServiceTests : ConstrollerTestFixture
    {
        [Fact]
        public void GetSalesStatistic_WithIncorrectEndDate_Returs_ArgumentOutOfRangeException()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<SaleStatisticService>>();

            var service = new SaleStatisticService(loggerMock.Object, mediatorMock.Object);

            var timeUnit = TimeUnit.Quarter;
            var startDate = DateTime.Parse("2020-01-01");
            var endDate = DateTime.Parse("2010-01-01");

            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => service.GetSalesStatistic(timeUnit, startDate, endDate).GetAwaiter().GetResult());
        }

        [Theory]
        [InlineData(TimeUnit.Quarter)]
        [InlineData(TimeUnit.Month)]
        [InlineData(TimeUnit.Week)]
        [InlineData(TimeUnit.Day)]
        public void GetSalesStatistic_WithValidParameters_Returs_SalesStatistic(TimeUnit timeUnit)
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SaleStatisticService>>();

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<GetSalesByDateQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetSales()));

            var service = new SaleStatisticService(loggerMock.Object, mediatorMock.Object);

            var startDate = DateTime.Parse("2010-01-01");
            var endDate = DateTime.Parse("2022-01-01");

            // Act
            var result = service.GetSalesStatistic(timeUnit, startDate, endDate).GetAwaiter().GetResult();

            // Assert
            result.ShouldNotBeNull();
            result.StartDate.ShouldNotBeNull();
            result.StartDate.ShouldBeOfType(typeof(DateTime));
            result.EndDate.ShouldNotBeNull();
            result.EndDate.ShouldBeOfType(typeof(DateTime));
            result.SalesDate.ShouldNotBeNull();
            result.SalesDate.ShouldBeOfType(typeof(List<DateTime>));
            result.SalesNumber.ShouldNotBeNull();
            result.SalesNumber.ShouldBeOfType(typeof(List<int>));
            result.SalesSum.ShouldNotBeNull();
            result.SalesSum.ShouldBeOfType(typeof(List<double>));
        }
    }
}