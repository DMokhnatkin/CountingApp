using System;

namespace CountingApp.Models
{
    /// <summary>
    /// Базовый класс для всех операций
    /// </summary>
    public abstract class Transaction
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Время проведения операции
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Сумма транзакции
        /// </summary>
        public abstract decimal TotalAmountRub { get; }
    }
}
