namespace CountingApp.Models.TransactionsAggregator
{
    /// <summary>
    /// Все транзакции которые реализуют этот интерфейс можно сложить и получить некоторое значение.
    /// </summary>
    public interface IAggregatable
    {
        void Aggregate(IAggregatorState state);
    }
}
