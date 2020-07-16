using AutoMapper;
using MediatR;
using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Interfaces;
using ServerSideApp.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSideApp.Application.CQRS.Commans.Create
{
    /// <summary>
    /// Define command to create new sale.
    /// </summary>
    public class CreateSaleCommand : IRequest<int>
    {
        /// <summary>
        /// Sale DTO.
        /// </summary>
        public SaleDTO Model { get; set; }

        public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Constructor with parameters.
            /// </summary>
            /// <param name="context">Application context.</param>
            /// <param name="mapper">Mapper.</param>
            /// <exception cref="ArgumentNullException"></exception>
            public CreateSaleCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <summary>
            /// Create new sale.
            /// </summary>
            /// <param name="request">Request.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>Identifier of new sale.</returns>
            public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                var entity = _mapper.Map<Sale>(request.Model);

                _context.Sales.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}