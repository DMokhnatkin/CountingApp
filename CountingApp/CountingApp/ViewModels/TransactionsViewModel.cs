using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CountingApp.Data.Mappers;
using CountingApp.Data.Repositories.Transactions;
using CountingApp.Models;
using CountingApp.Services;
using CountingApp.ViewModels.Transactions;
using Xamarin.Forms;

namespace CountingApp.ViewModels
{
    public class TransactionsViewModel : BaseViewModel, ILoadableViewModel
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsViewModel()
        {
            _transactionsRepository = DependencyService.Get<ITransactionsRepository>();

            Transactions = new ObservableCollection<TransactionListItemViewModel>();
        }

        #region Observable

        private ObservableCollection<TransactionListItemViewModel> _transactions;
        public ObservableCollection<TransactionListItemViewModel> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        #endregion

        /// <summary>
        /// Reload one transaction from the server.
        /// If transaction was in the list before it will be updated or added to the list otherwise.
        /// </summary>
        public async Task ReloadTransaction(Guid id)
        {
            OccupyIsBusy();
            try
            {
                var dto = await _transactionsRepository.GetAsync(id);
                var t = Transactions.SingleOrDefault(x => x.Model.Id == id);
                if (t != null)
                    t.ChangeModel(dto.Unmap());
                else
                    Transactions.Add(new TransactionListItemViewModel(dto.Unmap()));
            }
            catch (Exception e)
            {
                Debug.Fail(e.Message);
            }
            ReleaseIsBusy();
        }

        /// <summary>
        /// Reload all transactions. (clear list and create anew with data from server)
        /// </summary>
        /// <returns></returns>
        public async Task ReloadTransactions()
        {
            OccupyIsBusy();

            var transactions = await _transactionsRepository.GetAllAsync();
            Transactions = new ObservableCollection<TransactionListItemViewModel>(transactions.Select(x => new TransactionListItemViewModel(x.Unmap())));
            ReleaseIsBusy();
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

        public async Task Load()
        {
            // TODO: It is not place for authorization
            var authService = ApplicationIocContainer.CurrentContainer.Resolve<IAuthService>();
            if (!authService.IsAuthenticated)
                authService.Login();
            await ReloadTransactions();
        }
    }
}
