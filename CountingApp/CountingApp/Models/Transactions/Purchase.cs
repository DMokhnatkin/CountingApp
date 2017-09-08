using System;
using System.Linq;
using CountingApp.Models.TransactionsAggregator;

namespace CountingApp.Models.Transactions
{
    public class Purchase : Transaction, IAggregatable
    {
        /// <summary>
        /// Перечень взносов
        /// </summary>
        public Contribution[] Contributions { get; set; }

        /// <summary>
        /// Все люди, которые участвовали в покупке
        /// </summary>
        public Person[] People { get; set; }

        public override decimal TotalAmountRub => Contributions.Select(x => x.AmountRub).Sum();

        public decimal TotalContribution => Contributions.Sum(x => x.AmountRub);

        public Person[] ExtractFreeloaders()
        {
            var contributions = Contributions.ToDictionary(key => key.PersonId, val => val);

            return People.Where(x => !contributions.ContainsKey(x.Id)).ToArray();
        }

        public void Aggregate(IAggregatorState state)
        {
            if (state is IDebtsAggregatorState debtsState)
            {
                foreach (var contribution in Contributions)
                {
                    var eachPersonDebtRub = contribution.AmountRub / People.Length;
                    foreach (var person in People)
                    {
                        if (person.Id == contribution.PersonId)
                            continue;
                        debtsState.IncreaseDebt(person.Id, contribution.PersonId, eachPersonDebtRub);
                    }
                }
                return;
            }
            throw new ArgumentException($"Can't aggregate transaction {Id}. {nameof(state)} doesn't implement {nameof(IDebtsAggregatorState)}");
        }
    }
}
