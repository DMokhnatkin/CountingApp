using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CountingApp.Data.Repositories.People;
using CountingApp.Models;
using CountingApp.Services;
using Xamarin.Forms;

namespace CountingApp.ViewModels
{
    public class DebtsViewModel : BaseViewModel
    {
        private readonly DebtsCalculationService _debtsCalculationService;
        private readonly IPeopleRepository _peopleRepository;

        public DebtsViewModel()
        {
            _debtsCalculationService = DependencyService.Get<DebtsCalculationService>();
            _peopleRepository = DependencyService.Get<IPeopleRepository>();
        }

        public async Task CalculateDebts()
        {
            if (_debtsCalculationService == null || _peopleRepository == null)
                return;

            await OccupyIsBusy();
            var debts = await _debtsCalculationService.CalculateDebts();
            Debts = new ObservableCollection<PersonDebtsViewModel>();
            foreach (var personDebts in debts.GroupBy(x => x.ContributorId))
            {
                var person = await _peopleRepository.GetAsync(personDebts.Key);
                Debts.Add(new PersonDebtsViewModel
                {
                    Who = person,
                    Debts = new ObservableCollection<Debt>(personDebts)
                });
            }
            ReleaseIsBusy();
        }

        #region Observable

        private ObservableCollection<PersonDebtsViewModel> _debts;
        public ObservableCollection<PersonDebtsViewModel> Debts
        {
            get=> _debts;
            set => SetProperty(ref _debts, value);
        }

        #endregion

        public class PersonDebtsViewModel : BaseViewModel
        {
            #region Observable

            private Person _who;
            public Person Who
            {
                get =>_who;
                set =>SetProperty(ref _who, value); 
            }

            public ObservableCollection<Debt> Debts { get; set; }

            #endregion
        }
    }


}
