using Microsoft.EntityFrameworkCore;
using ServerSideApp.Application.Interfaces;
using ServerSideApp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSideApp.Infrastructure.Persistence
{
    /// <summary>
    /// Define application context to manage entities.
    /// </summary>
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        /// <summary>
        /// Sales table.
        /// </summary>
        public DbSet<Sale> Sales{ get; set; }

        /// <summary>
        /// Constructor of application DB context.
        /// </summary>
        /// <param name="options">Application DB context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Configure DB on model creating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Add entities configuration here.
        }
    }
}