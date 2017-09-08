using System.Linq;
using System.Threading.Tasks;
using CountingApp.Data.Mappers;
using CountingApp.Data.Repositories.Transactions;
using CountingApp.Models;
using CountingApp.Models.TransactionsAggregator;
using Xamarin.Forms;

namespace CountingApp.Services
{
    public class DebtsCalculationService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public DebtsCalculationService()
        {
            _transactionsRepository = DependencyService.Get<ITransactionsRepository>();
        }

        public async Task<Debt[]> CalculateDebts()
        {
            var transactions = await _transactionsRepository.GetAllAsync();
            var aggregator = new Aggregator();
            var result = aggregator.Aggregate(transactions.Select(x => x.Unmap()).OfType<IAggregatable>());
            return result.GetAllDebts();
        }
    }
}
