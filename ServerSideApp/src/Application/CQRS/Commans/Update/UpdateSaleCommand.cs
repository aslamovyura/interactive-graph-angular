using MediatR;
using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Exceptions;
using ServerSideApp.Application.Interfaces;
using ServerSideApp.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSideApp.Application.CQRS.Commans.Update
{
    /// <summary>
    /// Update sale.
    /// </summary>
    public class UpdateSaleCommand : IRequest
    {
        /// <summary>
        /// Sale data transfer object (DTO).
        /// </summary>
        public SaleDTO Model { get; set; }

        /// <summary>
        /// Update sale.
        /// </summary>
        public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand>
        {
            private readonly IApplicationDbContext _context;

            /// <summary>
            /// Constructor of update parameters.
            /// </summary>
            /// <param name="context">Application context.</param>
            /// <exception cref="ArgumentNullException"></exception>
            public UpdateSaleCommandHandler(IApplicationDbContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            /// <summary>
            /// Update sale.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns>Void value.</returns>
            /// <exception cref="ArgumentNullException"></exception>
            public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                var sale = _context.Sales.Where(s => s.Id == request.Model.Id).SingleOrDefault();
                if (sale == null)
                {
                    throw new NotFoundException(nameof(Sale), request.Model.Id);
                }

                sale.Date = request.Model.Date;
                sale.Amount = request.Model.Amount;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}