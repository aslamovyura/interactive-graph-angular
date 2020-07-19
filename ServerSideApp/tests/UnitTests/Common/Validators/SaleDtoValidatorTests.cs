using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Validators;
using Shouldly;
using System;
using Xunit;

namespace UnitTests.Common.Validators
{
    public class UpdateTodoListCommandValidatorTests
    {
        [Fact]
        public void Validator_WhenSaleDtoParametersAreValid_Returns_Valid()
        {
            // Arrange
            var saleDTO = new SaleDTO
            {
                Date = new DateTime(2010, 01, 01),
                Amount = 100,
            };

            var validator = new SaleDtoValidator();

            // Act
            var result = validator.Validate(saleDTO);

            // Assert
            result.IsValid.ShouldBe(true);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(0)]
        public void Validator_WhenSaleAmountIsNegativeOrZero_Returns_NotValid(double amout)
        {
            // Arrange
            var saleDTO = new SaleDTO
            {
                Date = new DateTime(2010, 01, 01),
                Amount = amout,
            };

            var validator = new SaleDtoValidator();

            // Act
            var result = validator.Validate(saleDTO);

            // Assert
            result.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Validator_WhenSaleAmountIsMissing_Returns_NotValid()
        {
            // Arrange
            var saleDTO = new SaleDTO
            {
                Date = new DateTime(2010, 01, 01),
            };

            var validator = new SaleDtoValidator();

            // Act
            var result = validator.Validate(saleDTO);

            // Assert
            result.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Validator_WhenSaleDateIsMissing_Returns_NotValid()
        {
            // Arrange
            var saleDTO = new SaleDTO
            {
                Amount = 100,
            };

            var validator = new SaleDtoValidator();

            // Act
            var result = validator.Validate(saleDTO);

            // Assert
            result.IsValid.ShouldBe(false);
        }

        [Fact]
        public void Validator_WhenSaleDateIsAheadOfPresentTime_Returns_NotValid()
        {
            // Arrange
            var saleDTO = new SaleDTO
            {
                Date = DateTime.Now.AddYears(10),
                Amount = 100,
            };

            var validator = new SaleDtoValidator();

            // Act
            var result = validator.Validate(saleDTO);

            // Assert
            result.IsValid.ShouldBe(false);
        }
    }
}