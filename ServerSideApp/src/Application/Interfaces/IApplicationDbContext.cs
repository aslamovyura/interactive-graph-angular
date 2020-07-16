using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    interface IApplicationDbContext
    {
        /// <summary>
        /// Sales.
        /// </summary>
        DbSet<Sale> Sales { get; set; }

        /// <summary>
        /// Save changes in application context.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Operation result.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}