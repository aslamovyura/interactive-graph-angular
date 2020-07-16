using System;

namespace Domain.Entities
{
    /// <summary>
    /// Single sale entity.
    /// </summary>
    public class Sale
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