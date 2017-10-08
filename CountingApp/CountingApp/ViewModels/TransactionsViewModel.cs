using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using CountingApp.Data.Mappers;
using CountingApp.Data.Repositories.Transactions;
using CountingApp.Models;
using CountingApp.Services;
using CountingApp.ViewModels.Transactions;
using IdentityModel.Client;
using Xamarin.Auth;
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

#pragma warning disable 4014
            LoadTransactions();
#pragma warning restore 4014
        }

        private async Task LoadTransactions()
        {
            await OccupyIsBusy();

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
            await _transactionsRepository.Add(transaction.Map());
        }

        public async Task<bool> ModifyTransaction(Transaction transaction)
        {
            var vm = Transactions.FirstOrDefault(x => x.Model.Id == transaction.Id);
            if (vm == null)
                return false;

            vm.ChangeModel(transaction);
            return await _transactionsRepository.Modify(transaction.Map());
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
            var res = await _transactionsRepository.Remove(id);
            if (!res)
                return false;
            Transactions.RemoveAt(i);
            return true;
        }
    }
}
