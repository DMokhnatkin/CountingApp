using Microsoft.EntityFrameworkCore;

namespace CountingApp.Server.DbModels
{
    public class CountingAppDbContext : DbContext
    {
        public DbSet<TransactionDbModel> TransactionDbModels { get; set; }

        public CountingAppDbContext(DbContextOptions<CountingAppDbContext> options) : base(options)
        {
        }
    }
}
