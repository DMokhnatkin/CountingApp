using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CountingApp.Models;
using CountingApp.Models.Transactions;

namespace CountingApp.ViewModels.Transactions
{
    public class PurchaseViewModel : BaseViewModel
    {
        /// <summary>
        /// Ид транзакции (null если мы создаем новую)
        /// </summary>
        private readonly Guid _transactionId;

        public PurchaseViewModel()
        {
            Contributions = new ObservableCollection<ContributionViewModel>();
            Freeloaders = new ObservableCollection<Person>();
        }

        public PurchaseViewModel(Purchase model)
        {
            var contributions = new List<ContributionViewModel>();
            var people = model.People?.ToDictionary(key => key.Id, val => val) ?? new Dictionary<string, Person>();
            foreach (var contribution in model.Contributions ?? new Contribution[0])
            {
                contributions.Add(new ContributionViewModel(people[contribution.PersonId]) { Amount = contribution.AmountRub });
            }

            Contributions = new ObservableCollection<ContributionViewModel>(contributions);
            Freeloaders = new ObservableCollection<Person>(model.ExtractFreeloaders());
            Description = model.Description;

            _transactionId = model.Id;
        }

        #region Observable

        private ObservableCollection<ContributionViewModel> _contributions;
        public ObservableCollection<ContributionViewModel> Contributions
        {
            get => _contributions;
            set => SetProperty(ref _contributions, value);
        }

        private ObservableCollection<Person> _freeloaders;
        public ObservableCollection<Person> Freeloaders
        {
            get => _freeloaders;
            set => SetProperty(ref _freeloaders, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        #endregion

        public Purchase GetModel()
        {
            return new Purchase
            {
                Id = _transactionId,
                Contributions = Contributions.Select(x => new Contribution{ AmountRub = x.Amount, PersonId = x.Model.Id }).ToArray(),
                //People = Contributions.Select(x => x.Model).Concat(Freeloaders).Distinct().ToArray(),
                PersonList = Contributions.Select(x => x.Model).Concat(Freeloaders).Select(x => x.Id).Distinct().ToArray(),
                Description = Description,
                Timestamp = DateTime.Now
            };
        }

        public class ContributionViewModel : BaseViewModel
        {
            public Person Model { get; }

            public ContributionViewModel(Person model)
            {
                Model = model;

                OnPropertyChanged(nameof(DisplayName));
            }

            #region Observable

            public string DisplayName => Model.DisplayName;

            private decimal _amount;
            public decimal Amount
            {
                get => _amount;
                set
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }

            #endregion
        }
    }
}
