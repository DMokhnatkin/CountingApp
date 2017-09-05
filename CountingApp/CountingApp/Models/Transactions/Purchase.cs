using System.Linq;

namespace CountingApp.Models.Transactions
{
    public class Purchase : Transaction
    {
        /// <summary>
        /// Перечень взносов
        /// </summary>
        public Contribution[] Contributions { get; set; }

        /// <summary>
        /// Все люди, которые участвовали в покупке
        /// </summary>
        public Person[] People { get; set; }

        public override decimal TotalAmountRub => Contributions.Select(x => x.Amount).Sum();

        public Person[] ExtractFreeloaders()
        {
            var contributions = Contributions.ToDictionary(key => key.PersonId, val => val);

            return People.Where(x => !contributions.ContainsKey(x.Id)).ToArray();
        }
    }
}
