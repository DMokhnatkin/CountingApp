using System.Collections.Generic;

namespace CountingApp.Models.TransactionsAggregator
{
    public class Aggregator
    {
        public AggregatorState Aggregate(IEnumerable<IAggregatable> aggregatables)
        {
            var result = new AggregatorState();
            foreach (var aggregatable in aggregatables)
            {
                aggregatable.Aggregate(result);
            }
            return result;
        }
    }
}
