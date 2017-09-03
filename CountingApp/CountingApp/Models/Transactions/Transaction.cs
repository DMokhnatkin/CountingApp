using System;

namespace CountingApp.Models
{
    /// <summary>
    /// Базовый класс для всех операций
    /// </summary>
    public abstract class Transaction
    {
        /// <summary>
        /// Время проведения операции
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Сумма транзакции
        /// </summary>
        public decimal TotalPriceRub { get; set; }

        public abstract decimal TotalAmount { get; }
    }
}
