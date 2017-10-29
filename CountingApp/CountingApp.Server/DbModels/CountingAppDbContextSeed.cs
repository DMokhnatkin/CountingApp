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
                UserId = "b70166adce518a511db4d44fb0bb158644032a52ba7a129647a5ecdc4b71f496",
                SerializedData = "test",
                TransactionId = new Guid("28150EB5-803D-4611-B397-AC89220A75BB")
            };
            if (!context.TransactionDbModels.Any(x => x.TransactionId == t.TransactionId))
            {
                context.TransactionDbModels.Add(t);
                context.SaveChanges();
            }
        }
    }
}
