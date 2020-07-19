using FluentValidation;

namespace ServerSideApp.Application.CQRS.Commans.Create
{
    /// <summary>
    /// Define class to validate create sale command.
    /// </summary>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        /// <summary>
        /// Constructor of command validator.
        /// </summary>
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.Model.Date).NotEmpty();
            RuleFor(sale => sale.Model.Amount).GreaterThan(0).NotEmpty();
        }
    }
}