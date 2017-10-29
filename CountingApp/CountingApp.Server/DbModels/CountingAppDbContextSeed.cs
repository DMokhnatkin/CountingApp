using System;
using System.Linq;

namespace CountingApp.Server.DbModels
{
    public class CountingAppDbContextSeed
    {
        public void SeedAsync(CountingAppDbContext context)
        {
            var t = new TransactionDbModel
            {
                UserId = "9593a5a0-0058-4a0b-90db-5cb14d9e49d3",
                TransactionData =
                "{ 'Contributions' : { '9593a5a0-0058-4a0b-90db-5cb14d9e49d3' : 100, '9593a5a0-0058-4a0b-90db-5cb14d9e49d4' : 200 }, 'PersonList' : ['9593a5a0-0058-4a0b-90db-5cb14d9e49d3', '9593a5a0-0058-4a0b-90db-5cb14d9e49d4'] }",
                TransactionId = new Guid("28150EB5-803D-4611-B397-AC89220A75BB"),
                Timestamp = new DateTime(2016, 1, 1),
                TotalAmount = 560,
                TransactionType = "purchase",
            };
            if (!context.TransactionDbModels.Any(x => x.TransactionId == t.TransactionId))
            {
                context.TransactionDbModels.Add(t);
                context.SaveChanges();
            }
        }
    }
}
