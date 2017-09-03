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

        public override decimal TotalAmount => Contributions.Select(x => x.Amount).Sum();
    }
}
