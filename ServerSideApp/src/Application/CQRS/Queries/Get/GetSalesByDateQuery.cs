using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSideApp.Application.CQRS.Queries.Get
{
    /// <summary>
    /// Define class to get info on the whole sales.
    /// </summary>
    public class GetSalesByDateQuery : IRequest<IEnumerable<SaleDTO>>
    {
        /// <summary>
        /// Start date for sales selection.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date for sales selection.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Define class to get info on the whole sales in app.
        /// </summary>
        public class GetSalesByDateQueryHandler : IRequestHandler<GetSalesByDateQuery, IEnumerable<SaleDTO>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            /// <summary>
            /// Constructor of query handler.
            /// </summary>
            /// <param name="context">Application context.</param>
            /// <param name="mapper">Automapper.</param>
            public GetSalesByDateQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <summary>
            /// Get sales info.
            /// </summary>
            /// <param name="request">Info request.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>Number of sales DTO.</returns>
            public async Task<IEnumerable<SaleDTO>> Handle(GetSalesByDateQuery request, CancellationToken cancellationToken)
            {
                var entites = await _context.Sales.Where(s => s.Date >= request.StartDate && s.Date <= request.EndDate)
                    .OrderBy(s => s.Date)
                    .ToArrayAsync(cancellationToken);

                var sales = _mapper.Map<IEnumerable<SaleDTO>>(entites);

                return sales;
            }
        }
    }
}