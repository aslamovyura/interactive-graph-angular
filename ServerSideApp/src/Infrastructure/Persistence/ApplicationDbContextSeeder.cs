using ServerSideApp.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSideApp.Infrastructure.Persistence
{
    /// <summary>
    /// Define class to seed application context.
    /// </summary>
    public class ApplicationDbContextSeeder
    {
        /// <summary>
        /// Fill database with test data.
        /// </summary>
        /// <param name="context">Application DB context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            // Add first test sale.
            var sale1 = new Sale
            {
                Date = DateTime.Parse("2020-01-01 10:00:00"),
                Amount = 1000,
            };

            var sale1Exist = context.Sales.FirstOrDefault(s => s.Date == sale1.Date && s.Amount == sale1.Amount);
            if (sale1Exist == null)
            {
                context.Sales.Add(sale1);
            }

            // Add second test sale.
            var sale2 = new Sale
            {
                Date = DateTime.Parse("2020-02-01 10:00:00"),
                Amount = 2500,
            };

            var sale2Exist = context.Sales.FirstOrDefault(s => s.Date == sale2.Date && s.Amount == sale2.Amount);
            if (sale2Exist == null)
            {
                context.Sales.Add(sale2);
            }

            await context.SaveChangesAsync();
        }
    }
}