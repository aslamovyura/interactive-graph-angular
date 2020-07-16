using MediatR;
using ServerSideApp.Application.Exceptions;
using ServerSideApp.Application.Interfaces;
using ServerSideApp.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSideApp.Application.CQRS.Commans.Delete
{
    public class DeleteSaleCommand : IRequest
    {
        /// <summary>
        /// Sale Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Define command to delete sale.
        /// </summary>
        public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand>
        {
            private readonly IApplicationDbContext _context;

            /// <summary>
            /// Constructor of delete command.
            /// </summary>
            /// <param name="context">Application context.</param>
            /// <exception cref="ArgumentNullException"></exception>
            public DeleteSaleCommandHandler(IApplicationDbContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            /// <summary>
            /// Delete sale from DB.
            /// </summary>
            /// <param name="request">Command to delete sale.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                var author = _context.Sales.Where(a => a.Id == request.Id).SingleOrDefault();

                if (author == null)
                {
                    throw new NotFoundException(nameof(Sale), request.Id);
                }

                _context.Sales.Remove(author);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}