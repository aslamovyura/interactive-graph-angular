using ServerSideApp.Application.DTO;
using System;
using System.Collections.Generic;

namespace UnitTests.Controllers
{
    /// <summary>
    /// Define base functions for controllers testing.
    /// </summary>
    public class ConstrollerTestFixture
    {
        /// <summary>
        /// Generate collection of SaleDTO.
        /// </summary>
        /// <returns>Collection of SaleDTO.</returns>
        public IEnumerable<SaleDTO> GetSales()
        {
            return new List<SaleDTO>()
            {
                new SaleDTO()
                {
                    Id = 1,
                    Date = DateTime.Parse("2020-01-01"),
                    Amount = 1000,
                },

                new SaleDTO()
                {
                    Id = 2,
                    Date = DateTime.Parse("2020-02-01"),
                    Amount = 2500,
                },
            };
        }

        /// <summary>
        /// Generate single SaleDTO.
        /// </summary>
        /// <returns>SaleDTO.</returns>
        public SaleDTO GetSale()
        {
            return new SaleDTO()
            {
                Id = 1,
                Date = DateTime.Parse("2020-01-01"),
                Amount = 1000,
            };
        }

        /// <summary>
        /// Get nullable SaleDTO.
        /// </summary>
        /// <returns>Null object.</returns>
        public SaleDTO GetNullSale() => null;
    }
}