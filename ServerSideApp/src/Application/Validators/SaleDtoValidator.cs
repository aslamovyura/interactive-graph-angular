using FluentValidation;
using ServerSideApp.Application.DTO;
using System;

namespace ServerSideApp.Application.Validators
{
    /// <summary>
    /// Define validator of Sale DTO.
    /// </summary>
    public class SaleDtoValidator: AbstractValidator<SaleDTO>
    {
        /// <summary>
        /// Constructor of SaleDTO validator.
        /// </summary>
        public SaleDtoValidator()
        {
            RuleFor(s => s.Amount).NotNull()
                                  .NotEmpty()
                                  .GreaterThan(0);
            RuleFor(s => s.Date).NotNull()
                                .NotEmpty()
                                .LessThanOrEqualTo(DateTime.Now);
        }
    }
}