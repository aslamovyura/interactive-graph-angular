using ServerSideApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSideApp.Infrastructure.Persistence
{
    /// <summary>
    /// Define class to seed application context.
    /// </summary>
    public class ApplicationDbContextSeeder
    {
        const int START_YEAR = 2010;
        const int END_YEAR = 2020;

        const int SALES_PER_DAY_MIN = 0;
        const int SALES_PER_DAY_MAX = 5;

        const int SALE_AMOUNT_MIN = 100;
        const int SALE_AMOUNT_MAX = 50000;

        /// <summary>
        /// Fill database with test data.
        /// </summary>
        /// <param name="context">Application DB context.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            if (context.Sales.Count() == 0)
            {
                var sales = GenerateInitialSales();
                await context.Sales.AddRangeAsync(sales);
                await context.SaveChangesAsync();
            }
        }

        private static ICollection<Sale> GenerateInitialSales()
        {
            var sales = new List<Sale>();
            var rand = new Random();
            for (int year = START_YEAR; year < END_YEAR; year++)
            {
                for (int month = 1; month <= 12; month ++)
                {
                    var daysInMonth = DateTime.DaysInMonth(year, month);
                    for (int day = 1; day <= daysInMonth; day ++)
                    {
                        var salesOnDay = rand.Next(SALES_PER_DAY_MIN, SALES_PER_DAY_MAX);
                        for (int count = 0; count < salesOnDay; count ++)
                        {
                            var sale = new Sale
                            {
                                Date = new DateTime(year, month, day),
                                Amount = rand.Next(SALE_AMOUNT_MIN, SALE_AMOUNT_MAX),
                            };
                            sales.Add(sale);
                        }
                    }
                }
            }

            return sales;
        }
    }
}