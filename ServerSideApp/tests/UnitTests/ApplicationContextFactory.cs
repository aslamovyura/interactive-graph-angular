using Microsoft.EntityFrameworkCore;
using ServerSideApp.Domain.Entities;
using ServerSideApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    /// <summary>
    /// Factory to fill datababe with seed test data.
    /// </summary>
    public static class ApplicationContextFactory
    {
        /// <summary>
        /// Create database.
        /// </summary>
        /// <returns>Context of sample database.</returns>
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        /// <summary>
        /// Seed database with sample data.
        /// </summary>
        /// <param name="context">Context of sample database.</param>
        public static void SeedSampleData(ApplicationDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            var comments = new List<Sale>
            {
                new Sale
                {
                    Id = 1,
                    Date = new DateTime(2020, 01, 01),
                    Amount = 1000,
                },

                new Sale
                {
                    Id = 2,
                    Date = new DateTime(2020, 01, 01),
                    Amount = 1200,
                },

                new Sale
                {
                    Id = 3,
                    Date = new DateTime(2020, 02, 01),
                    Amount = 2500,
                },

                new Sale
                {
                    Id = 4,
                    Date = new DateTime(2020, 03, 01),
                    Amount = 500,
                },
            };

            context.Sales.AddRange(comments);
            context.SaveChanges();
        }

        /// <summary>
        /// Destroy database.
        /// </summary>
        /// <param name="context">Context of sample database.</param>
        public static void Destroy(ApplicationDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}