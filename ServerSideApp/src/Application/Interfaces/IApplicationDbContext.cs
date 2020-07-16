using Microsoft.EntityFrameworkCore;
using ServerSideApp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSideApp.Application.Interfaces
{
    /// <summary>
    /// Define interface for application DB context.
    /// </summary>
    public interface IApplicationDbContext
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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}