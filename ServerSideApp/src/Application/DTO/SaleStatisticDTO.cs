using ServerSideApp.Application.Enums;
using ServerSideApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ServerSideApp.Application.DTO
{
    /// <summary>
    /// Define data transfer object (DTO) for sales statistic.
    /// </summary>
    public class SaleStatisticDTO
    {
        /// <summary>
        /// Start date for statistic calculation.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date for statistic calculation.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Center point (date) of time interval for statistic calculation.
        /// </summary>
        public ICollection<DateTime> SalesDate { get; set; }

        /// <summary>
        /// Total sales number.
        /// </summary>
        public ICollection<int> SalesNumber { get; set; }

        /// <summary>
        /// Total sales sum, $.
        /// </summary>
        public ICollection<double> SalesSum { get; set; }

        /// <summary>
        /// Constructor of SaleStatistic DTO.
        /// </summary>
        public SaleStatisticDTO()
        {
            SalesDate = new List<DateTime>();
            SalesNumber = new List<int>();
            SalesSum = new List<double>();
        }
    }
}