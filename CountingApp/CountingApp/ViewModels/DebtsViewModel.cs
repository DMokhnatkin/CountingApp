﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CountingApp.Data.Mappers;
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

            OccupyIsBusy();
            var debts = await _debtsCalculationService.CalculateDebts();
            Debts = new ObservableCollection<DebtViewModel>();
            foreach (var debt in debts)
            {
                var who = await _peopleRepository.GetAsync(new [] { debt.FreeloaderId });
                var whom = await _peopleRepository.GetAsync(new [] { debt.ContributorId });

                Debts.Add(new DebtViewModel
                {
                    Who = who[0].Unmap(),
                    Whom = whom[0].Unmap(),
                    AmountRub = debt.AmountRub
                });
            }
            //Debts = new ObservableCollection<PersonDebtsViewModel>();
            //foreach (var personDebts in debts.GroupBy(x => x.ContributorId))
            //{
            //    var person = await _peopleRepository.GetAsync(personDebts.Key);
            //    Debts.AddAsync(new PersonDebtsViewModel
            //    {
            //        Who = person,
            //        Debts = new ObservableCollection<Debt>(personDebts)
            //    });
            //}
            ReleaseIsBusy();
        }

        #region Observable

        //private ObservableCollection<PersonDebtsViewModel> _debts;
        //public ObservableCollection<PersonDebtsViewModel> Debts
        //{
        //    get=> _debts;
        //    set => SetProperty(ref _debts, value);
        //}

        private ObservableCollection<DebtViewModel> _debts;
        public ObservableCollection<DebtViewModel> Debts
        {
            get => _debts;
            set => SetProperty(ref _debts, value);
        }

        #endregion

        //public class PersonDebtsViewModel : BaseViewModel
        //{
        //    #region Observable

        //    private Person _who;
        //    public Person Who
        //    {
        //        get =>_who;
        //        set =>SetProperty(ref _who, value); 
        //    }

        //    public ObservableCollection<Debt> Debts { get; set; }

        //    #endregion
        //}

        public class DebtViewModel : BaseViewModel
        {
            #region Observable

            private Person _who;
            public Person Who
            {
                get => _who;
                set => SetProperty(ref _who, value);
            }

            private Person _whom;
            public Person Whom
            {
                get => _whom;
                set => SetProperty(ref _whom, value);
            }

            public decimal AmountRub { get; set; }

            #endregion
        }
    }


}
