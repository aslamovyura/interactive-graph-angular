using FluentValidation;

namespace ServerSideApp.Application.CQRS.Commans.Update
{
    /// <summary>
    /// Define class to validate update sale command.
    /// </summary>
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        /// <summary>
        /// Constructor of validator for update command.
        /// </summary>
        public UpdateSaleCommandValidator()
        {
            RuleFor(sale => sale.Model.Date).NotEmpty();
            RuleFor(sale => sale.Model.Amount).NotEmpty().GreaterThan(0);
        }
    }
}