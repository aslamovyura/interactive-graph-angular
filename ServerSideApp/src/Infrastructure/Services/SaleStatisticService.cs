using MediatR;
using Microsoft.Extensions.Logging;
using ServerSideApp.Application.CQRS.Queries.Get;
using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Enums;
using ServerSideApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSideApp.Infrastructure.Services
{
    /// <summary>
    /// Define service to calculate sale statistic.
    /// </summary>
    public class SaleStatisticService : ISaleStatisticService
    {
        private ILogger<SaleStatisticService> _logger;
        private IMediator _mediator;

        /// <summary>
        /// Construcor of service to calculate sales statistic.
        /// </summary>
        /// <param name="logger">Logging service.</param>
        /// <param name="mediator">Mediator service.</param>
        public SaleStatisticService(ILogger<SaleStatisticService> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<SaleStatisticDTO> GetSalesStatistic(TimeUnit timeUnit, DateTime startDate, DateTime endDate)
        {
            if (endDate <= startDate)
            {
                throw new ArgumentOutOfRangeException(nameof(endDate));
            }

            var salesQuery = new GetSalesByDateQuery { StartDate = startDate, EndDate = endDate };
            var sales = await _mediator.Send(salesQuery);

            var saleStatistic = timeUnit switch
            {
                TimeUnit.Quarter => CalculateQuarterStatistic(sales, startDate, endDate),
                TimeUnit.Month => CalculateMonthStatistic(sales, startDate, endDate),
                TimeUnit.Week => CalculateWeekStatistic(sales, startDate, endDate),
                TimeUnit.Day => CalculateDayStatistic(sales, startDate, endDate),
                _ => null,
            };
            return saleStatistic;

        }

        private SaleStatisticDTO CalculateQuarterStatistic(IEnumerable<SaleDTO> sales, DateTime startDate, DateTime endDate)
        {
            var saleStatistic = new SaleStatisticDTO { StartDate = startDate, EndDate = endDate };

            var salesByYearsQuery = from sale in sales
                                    group sale by sale.Date.Year into newGroup
                                    orderby newGroup.Key
                                    select newGroup;

            foreach (var yearGroup in salesByYearsQuery)
            {
                // Current year & quarter start monthes.
                var year = yearGroup.Key;
                var months = new int[] { 1, 4, 7, 10 };

                // Group data by Quarter and calculate statistic.
                var quarterSalesStatistic = yearGroup.GroupBy(item => ((item.Date.Month - 1) / 3))
                        .Select(x => new
                        {
                            Quarter = x.Key,
                            SalesNumber = x.Aggregate(0, (total, next) => total + 1),
                            SalesSum = x.Sum(item => item.Amount),
                        });

                // Add quarters without data to calculate statistic.
                var fullQuarterSalesStatistic = (from quarter in Enumerable.Range(0, 4)
                                                 join statistic in quarterSalesStatistic on quarter equals statistic.Quarter into grouping
                                                 from statistic in grouping.DefaultIfEmpty()
                                                 select new
                                                 {
                                                     Quarter = quarter,
                                                     StartDate = new DateTime(year, months[quarter], 1),
                                                     SalesNumber = statistic?.SalesNumber ?? 0,
                                                     SalesSum = statistic?.SalesSum ?? 0
                                                 }).ToList();

                // Filtering statistic data outside the specified time interval.
                foreach (var statistic in fullQuarterSalesStatistic)
                {
                    var qEndDate = statistic.StartDate.AddMonths(3);
                    var qStartDate = statistic.StartDate;
                    if (qEndDate < startDate || qStartDate > endDate)
                    {
                        continue;
                    }

                    saleStatistic.SalesNumber.Add(statistic.SalesNumber);
                    saleStatistic.SalesSum.Add(statistic.SalesSum);
                    saleStatistic.SalesDate.Add(statistic.StartDate);
                }
            }

            return saleStatistic;
        }

        private SaleStatisticDTO CalculateMonthStatistic(IEnumerable<SaleDTO> sales, DateTime startDate, DateTime endDate)
        {
            var saleStatistic = new SaleStatisticDTO { StartDate = startDate, EndDate = endDate };

            var salesByYearsQuery = from sale in sales
                                    group sale by sale.Date.Year into newGroup
                                    orderby newGroup.Key
                                    select newGroup;

            foreach (var yearGroup in salesByYearsQuery)
            {
                // Current year.
                var year = yearGroup.Key;

                // Group data by Month and calculate statistic.
                var monthSalesStatistic = yearGroup.GroupBy(item => item.Date.Month - 1)
                        .Select(x => new
                        {
                            Month = x.Key,
                            SalesNumber = x.Aggregate(0, (total, next) => total + 1),
                            SalesSum = x.Sum(item => item.Amount),
                        });

                // Add months without data to calculate statistic.
                var fullMonthSalesStatistic = (from month in Enumerable.Range(0, 12)
                                                 join statistic in monthSalesStatistic on month equals statistic.Month into grouping
                                                 from statistic in grouping.DefaultIfEmpty()
                                                 select new
                                                 {
                                                     Month = month,
                                                     StartDate = new DateTime(year, month + 1, 1),
                                                     SalesNumber = statistic?.SalesNumber ?? 0,
                                                     SalesSum = statistic?.SalesSum ?? 0
                                                 }).ToList();

                // Filtering statistic data outside the specified time interval.
                foreach (var statistic in fullMonthSalesStatistic)
                {
                    var mEndDate = statistic.StartDate.AddMonths(1);
                    var mStartDate = statistic.StartDate;
                    if (mEndDate < startDate || mStartDate > endDate)
                    {
                        continue;
                    }

                    saleStatistic.SalesNumber.Add(statistic.SalesNumber);
                    saleStatistic.SalesSum.Add(statistic.SalesSum);
                    saleStatistic.SalesDate.Add(statistic.StartDate);
                }
            }

            return saleStatistic;
        }

        private SaleStatisticDTO CalculateWeekStatistic(IEnumerable<SaleDTO> sales, DateTime startDate, DateTime endDate)
        {

            Func<DateTime, int> weekProjector =
                d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                     d,
                     CalendarWeekRule.FirstFourDayWeek,
                     DayOfWeek.Monday);
                     //DayOfWeek.Sunday);

            var saleStatistic = new SaleStatisticDTO { StartDate = startDate, EndDate = endDate };

            var salesByYearsQuery = from sale in sales
                                    group sale by sale.Date.Year into newGroup
                                    orderby newGroup.Key
                                    select newGroup;

            foreach (var yearGroup in salesByYearsQuery)
            {
                // Current year.
                var year = yearGroup.Key;

                // Calculate parameters to convert week number to date.
                var jan1 = new DateTime(year, 1, 1);
                int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;
                var firstThursday = jan1.AddDays(daysOffset);
                var isFullFirstWeek = weekProjector(firstThursday) == 1;

                // Group data by Week and calculate statistic.
                var weekSalesStatistic = yearGroup
                    .GroupBy(item => weekProjector(item.Date) - 1)
                    .OrderBy(item => item.Key)
                    .Select(x => new
                    {
                        Week = x.Key,
                        SalesNumber = x.Aggregate(0, (total, next) => total + 1),
                        SalesSum = x.Sum(item => item.Amount),
                    });

                var weeksNumber = weekProjector(new DateTime(year, 12, 31));

                // Add weeks without data to calculate statistic.
                var fullWeekSalesStatistic = (from week in Enumerable.Range(0, weeksNumber)
                                               join statistic in weekSalesStatistic on week equals statistic.Week into grouping
                                               from statistic in grouping.DefaultIfEmpty()
                                               select new
                                               {
                                                   Week = week,
                                                   StartDate = firstThursday.AddDays((isFullFirstWeek ? week : week+1)* 7).AddDays(-3),
                                                   SalesNumber = statistic?.SalesNumber ?? 0,
                                                   SalesSum = statistic?.SalesSum ?? 0
                                               }).ToList();

                // Remove statistic data outside the specified time interval.
                foreach (var statistic in fullWeekSalesStatistic)
                {
                    var wEndDate = statistic.StartDate.AddDays(7);
                    var wStartDate = statistic.StartDate;
                    if (wEndDate < startDate || wStartDate > endDate)
                    {
                        continue;
                    }

                    // Add new statisctics or Combine statistics for weeks at the border of years.
                    var weekExist = saleStatistic.SalesDate.Any(d => d == wStartDate);
                    if (weekExist)
                    {
                        var index = saleStatistic.SalesDate.Select((d, i) => new { Index = i, SalesDate = d })
                            .Where(item => item.SalesDate == wStartDate)
                            .Single().Index;

                        // Update statistic collection.
                        var salesNumber = saleStatistic.SalesNumber.ElementAt(index);
                        saleStatistic.SalesNumber.Remove(salesNumber);
                        saleStatistic.SalesNumber.Add(salesNumber + statistic.SalesNumber);

                        var salesSum = saleStatistic.SalesSum.ElementAt(index);
                        saleStatistic.SalesSum.Remove(salesSum);
                        saleStatistic.SalesSum.Add(salesSum + statistic.SalesSum);

                        var salesDate = saleStatistic.SalesDate.ElementAt(index);
                        saleStatistic.SalesDate.Remove(salesDate);
                        saleStatistic.SalesDate.Add(salesDate);
                    }
                    else
                    {
                        saleStatistic.SalesNumber.Add(statistic.SalesNumber);
                        saleStatistic.SalesSum.Add(statistic.SalesSum);
                        saleStatistic.SalesDate.Add(statistic.StartDate);
                    }
                }
            }

            return saleStatistic;
        }

        private SaleStatisticDTO CalculateDayStatistic(IEnumerable<SaleDTO> sales, DateTime startDate, DateTime endDate)
        {
            var saleStatistic = new SaleStatisticDTO { StartDate = startDate, EndDate = endDate };

            var salesByYearsQuery = from sale in sales
                                    group sale by sale.Date.Year into newGroup
                                    orderby newGroup.Key
                                    select newGroup;

            foreach (var yearGroup in salesByYearsQuery)
            {
                // Current year.
                var year = yearGroup.Key;
                var jan1 = new DateTime(year, 1, 1);

                // Group data by Day and calculate statistic.
                var daySalesStatistic = yearGroup.GroupBy(item => item.Date.Day - 1)
                        .Select(x => new
                        {
                            Day = x.Key,
                            SalesNumber = x.Aggregate(0, (total, next) => total + 1),
                            SalesSum = x.Sum(item => item.Amount),
                        });

                var daysNumber = DateTime.IsLeapYear(year) ? 366 : 365;

                // Add days without data to calculate statistic.
                var fullDaySalesStatistic = (from day in Enumerable.Range(0, daysNumber)
                                               join statistic in daySalesStatistic on day equals statistic.Day into grouping
                                               from statistic in grouping.DefaultIfEmpty()
                                               select new
                                               {
                                                   Day = day,
                                                   StartDate = jan1.AddDays(day),
                                                   SalesNumber = statistic?.SalesNumber ?? 0,
                                                   SalesSum = statistic?.SalesSum ?? 0
                                               }).ToList();

                // Filtering statistic data outside the specified time interval.
                foreach (var statistic in fullDaySalesStatistic)
                {
                    var dEndDate = statistic.StartDate.AddDays(1);
                    var dStartDate = statistic.StartDate;
                    if (dEndDate < startDate || dStartDate > endDate)
                    {
                        continue;
                    }

                    saleStatistic.SalesNumber.Add(statistic.SalesNumber);
                    saleStatistic.SalesSum.Add(statistic.SalesSum);
                    saleStatistic.SalesDate.Add(statistic.StartDate);
                }
            }

            return saleStatistic;
        }
    }
}