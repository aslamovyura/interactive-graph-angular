using Application.CQRS.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ServerSideApp.Application.CQRS.Commans.Create;
using ServerSideApp.Application.CQRS.Commans.Delete;
using ServerSideApp.Application.CQRS.Commans.Update;
using ServerSideApp.Application.CQRS.Queries.Get;
using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Enums;
using ServerSideApp.Application.Interfaces;
using ServerSideApp.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Controllers
{
    public class SalesControllerTests : ConstrollerTestFixture
    {
        [Fact]
        public void GetSales_Returns_CollectionOfSalesDTO()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<GetSalesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetSales()));

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);

            // Act
            var result = controller.GetSales().GetAwaiter().GetResult();

            // Assert
            Assert.IsType<List<SaleDTO>>(result);
        }

        [Fact]
        public void GetSale_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<GetSalesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetSales()));

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            controller.ModelState.AddModelError("Id", "InvalidId");

            var id = 1;

            // Act
            var result = controller.GetSale(id).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetSale_WithValidModelAndValidId_Returns_SaleDTO()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<GetSaleQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetSale()));

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var id = 1;

            // Act
            var result = controller.GetSale(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<SaleDTO>(okObjectResult.Value);
        }

        [Fact]
        public void GetSensor_WithInvalidId_Returns_NotFoundResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<GetSaleQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetNullSale()));

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var id = 99;

            // Act
            var result = controller.GetSale(id).GetAwaiter().GetResult();

            // Assert
            var noContentResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void RegisterNewSale_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);

            controller.ModelState.AddModelError("Error", "Model Error");
            var saleDTO = new SaleDTO();

            // Act
            var result = controller.RegisterNewSale(saleDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void RegisterNewSale_WithValidExistingModel_Returns_ConflictResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<CreateSaleCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var saleDTO = new SaleDTO();

            // Act
            var result = controller.RegisterNewSale(saleDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public void RegisterNewSale_WithValidModel_Returns_CreatedAtActionResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<CreateSaleCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(It.IsAny<int>()));

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var saleDTO = new SaleDTO();

            // Act
            var result = controller.RegisterNewSale(saleDTO).GetAwaiter().GetResult();

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsAssignableFrom<SaleDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void UpdateSale_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);

            controller.ModelState.AddModelError("Error", "Model Error");
            var saleDTO = new SaleDTO();

            // Act
            var result = controller.UpdateSale(saleDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateSale_WithInvalidModelId_Returns_BadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);

            controller.ModelState.AddModelError("Error", "Model Error");
            var saleDTO = new SaleDTO { Id = -99 };

            // Act
            var result = controller.UpdateSale(saleDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateSale_WithNonexistingId_Returns_ConflictResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<UpdateSaleCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var saleDTO = new SaleDTO { Id = 99 };

            // Act
            var result = controller.UpdateSale(saleDTO).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public void UpdateSale_WithValidModel_Returns_OkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<UpdateSaleCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(It.IsAny<Unit>()));

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var saleDTO = new SaleDTO { Id = 1 };

            // Act
            var result = controller.UpdateSale(saleDTO).GetAwaiter().GetResult();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<SaleDTO>(okResult.Value);
        }

        [Fact]
        public void DeleteSale_WithInvalidModelId_Returns_NotFoundResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<DeleteSaleCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception());

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var id = -99;

            // Act
            var result = controller.DeleteSale(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void DeleteSale_Returns_OkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator
                .Send(It.IsAny<DeleteSaleCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(It.IsAny<Unit>()));

            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var id = 1;

            // Act
            var result = controller.DeleteSale(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<int>(okObjectResult.Value);
        }

        [Fact]
        public void GetSalesStatistic_WithMissingQueryParameter_Returns_BadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var scale = TimeUnit.Quarter;
            var startDate = DateTime.Parse("2019-01-01");

            // Act
            var result = controller.GetSalesStatistic(scale, startDate, null).GetAwaiter().GetResult();

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<string>(badRequestObjectResult.Value);
        }

        [Fact]
        public void GetSalesStatistic_WithIncorrectEndDateParameter_Returns_BadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<SalesController>>();
            var salesServiceMock = new Mock<ISaleStatisticService>();

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var scale = TimeUnit.Quarter;
            var startDate = DateTime.Parse("2019-01-01");
            var endDate = DateTime.Parse("2018-01-01");

            // Act
            var result = controller.GetSalesStatistic(scale, startDate, endDate).GetAwaiter().GetResult();

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<string>(badRequestObjectResult.Value);
        }

        [Fact]
        public void GetSalesStatistic_WithValidQuery_Returns_OkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<SalesController>>();

            var salesServiceMock = new Mock<ISaleStatisticService>();
            salesServiceMock.Setup(service => service
                .GetSalesStatistic(It.IsAny<TimeUnit>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(Task.FromResult(GetSaleStatistic()));

            var controller = new SalesController(loggerMock.Object, mediatorMock.Object, salesServiceMock.Object);
            var scale = TimeUnit.Quarter;
            var startDate = DateTime.Parse("2019-01-01");
            var endDate = DateTime.Parse("2020-01-01");

            // Act
            var result = controller.GetSalesStatistic(scale, startDate, endDate).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<SaleStatisticDTO>(okObjectResult.Value);
        }
    }
}