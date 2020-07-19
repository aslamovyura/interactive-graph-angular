using System;
using System.ComponentModel.DataAnnotations;

namespace ServerSideApp.Application.DTO
{
    /// <summary>
    /// Define data transfer object (DTO) of Sale entity.
    /// </summary>
    public class SaleDTO
    {
        /// <summary>
        /// Sale identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sale date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Sale amount, $.
        /// </summary>
        public double Amount { get; set; }
    }
}