using ServerSideApp.Application.DTO;
using ServerSideApp.Application.Enums;
using System;
using System.Threading.Tasks;

namespace ServerSideApp.Application.Interfaces
{
    /// <summary>
    /// Define interface of service to calculate sales statistic.
    /// </summary>
    public interface ISaleStatisticService
    {
        /// <summary>
        /// Calculate sales statistic.
        /// </summary>
        /// <param name="timeUnit">Time unit for statistic calculation.</param>
        /// <param name="startDate">Start date for statistic calculation.</param>
        /// <param name="endDate">End date for statistic calculation.</param>
        /// <returns>Sales statistic.</returns>
        Task<SaleStatisticDTO> GetSalesStatistic(TimeUnit timeUnit, DateTime startDate, DateTime endDate);
    }
}