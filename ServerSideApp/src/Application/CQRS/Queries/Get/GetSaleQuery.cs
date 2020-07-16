using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries.Get
{
    /// <summary>
    /// Defice class to get sale info.
    /// </summary>
    public class GetSaleQuery : IRequest<SaleDTO>
    {
        /// <summary>
        /// Sale Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Handler of the sale queries.
        /// </summary>
        public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, SaleDTO>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Constructor of query handler.
            /// </summary>
            /// <param name="context">Application context.</param>
            /// <param name="mapper">Model mapper.</param>
            public GetSaleQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <summary>
            /// Get info on single sale.
            /// </summary>
            /// <param name="request">Request.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>Sale object.</returns>
            public async Task<SaleDTO> Handle(GetSaleQuery request, CancellationToken cancellationToken)
            {
                request = request ?? throw new ArgumentNullException(nameof(request));

                var entity = await _context.Sales.Where(a => a.Id == request.Id).SingleOrDefaultAsync();
                var sale = _mapper.Map<SaleDTO>(entity);

                return sale;
            }
        }
    }
}