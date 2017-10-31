using System;
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

        public TransactionsViewModel()
        {
            _transactionsRepository = DependencyService.Get<ITransactionsRepository>();

            Transactions = new ObservableCollection<TransactionListItemViewModel>();
        }

        public async Task LoadTransactions()
        {
            OccupyIsBusy();

            var transactions = await _transactionsRepository.GetAllAsync();
            Transactions = new ObservableCollection<TransactionListItemViewModel>(transactions.Select(x => new TransactionListItemViewModel(x.Unmap())));
            ReleaseIsBusy();
        }

        #region Observable

        private ObservableCollection<TransactionListItemViewModel> _transactions;
        public ObservableCollection<TransactionListItemViewModel> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        #endregion

        public async Task CreateTransaction(Transaction transaction)
        {
            Transactions.Add(new TransactionListItemViewModel(transaction));
            await _transactionsRepository.AddAsync(transaction.Map());
        }

        public async Task ModifyTransaction(Transaction transaction)
        {
            var vm = Transactions.FirstOrDefault(x => x.Model.Id == transaction.Id);
            if (vm == null)
                throw new ArgumentException("Transaction doesn't exist");

            vm.ChangeModel(transaction);
            await _transactionsRepository.ModifyAsync(transaction.Map());
        }

        public async Task<bool> RemoveTransaction(Guid id)
        {
            int i;
            for (i = 0; i < Transactions.Count; i++)
            {
                if (Transactions[i].Model.Id == id)
                    break;
            }
            if (i == Transactions.Count)
                return false;
            await _transactionsRepository.RemoveAsync(id);
            Transactions.RemoveAt(i);
            return true;
        }
    }
}
