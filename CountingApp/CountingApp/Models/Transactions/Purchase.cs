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
    }
}
