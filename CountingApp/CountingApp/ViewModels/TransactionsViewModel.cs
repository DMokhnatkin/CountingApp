using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CountingApp.Data.Mappers;
using CountingApp.Data.Repositories.Transactions;
using CountingApp.Models;
using CountingApp.ViewModels.Transactions;
using Xamarin.Forms;

namespace CountingApp.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private readonly ITransactionsRepository _transactionsRepository;

        private ObservableCollection<TransactionListItemViewModel> _transactions;
        public ObservableCollection<TransactionListItemViewModel> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        public void CreateTransaction(Transaction transaction)
        {
            Transactions.Add(new TransactionListItemViewModel(transaction));
            _transactionsRepository.Add(transaction.Map());
        }

        public async Task<bool> ModifyTransaction(Transaction transaction)
        {
            var vm = Transactions.FirstOrDefault(x => x.Model.Id == transaction.Id);
            if (vm == null)
                return false;

            vm.ChangeModel(transaction);
            return await _transactionsRepository.Modify(transaction.Map());
        }

        public TransactionsViewModel(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;

            Transactions = new ObservableCollection<TransactionListItemViewModel>();
            _transactionsRepository = DependencyService.Get<ITransactionsRepository>();

#pragma warning disable 4014
            LoadTransactions();
#pragma warning restore 4014
        }

        private async Task LoadTransactions()
        {
            IsBusy = true;
            var transactions = await _transactionsRepository.GetAllAsync();
            Transactions = new ObservableCollection<TransactionListItemViewModel>(transactions.Select(x => new TransactionListItemViewModel(x.Unmap())));
            IsBusy = false;
        }
    }
}
