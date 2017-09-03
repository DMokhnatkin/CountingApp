using System;
using System.Collections.ObjectModel;
using System.Linq;
using CountingApp.Data.Repositories.People;
using CountingApp.Models;
using CountingApp.Models.Transactions;

namespace CountingApp.ViewModels.Transactions
{
    public class PurchaseViewModel : BaseViewModel
    {
        public PurchaseViewModel()
        {
            Contributions = new ObservableCollection<ContributionViewModel>();
            Freeloaders = new ObservableCollection<Person>();
        }

        public PurchaseViewModel(Purchase model)
        {
            var contributions =
                from personModel in model.People
                join contribution in model.Contributions on personModel.Id equals contribution.PersonId
                select new ContributionViewModel(personModel) {Amount = contribution.Amount};

            var freeloaders =
                model.People.Where(x => model.Contributions.SingleOrDefault(y => y.PersonId == x.Id) == null).ToArray();

            Contributions = new ObservableCollection<ContributionViewModel>(contributions);
            Freeloaders = new ObservableCollection<Person>(freeloaders);
        }

        public void Load()
        {
            new PeopleRepository().GetAvailablePeopleAsync().ContinueWith(task =>
            {
                Contributions.Add(new ContributionViewModel(task.Result[0]));

                Freeloaders.Add(task.Result[1]);
                Freeloaders.Add(task.Result[2]);
                Freeloaders.Add(task.Result[3]);
                Freeloaders.Add(task.Result[4]);
            });
        }

        private ObservableCollection<ContributionViewModel> _contributions;
        public ObservableCollection<ContributionViewModel> Contributions
        {
            get => _contributions;
            set
            {
                _contributions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Person> _freeloaders;
        public ObservableCollection<Person> Freeloaders
        {
            get => _freeloaders;
            set
            {
                _freeloaders = value;
                OnPropertyChanged();
            }
        }

        public Purchase GetModel()
        {
            return new Purchase
            {
                Contributions = Contributions.Select(x => new Contribution{ Amount = x.Amount, PersonId = x.Model.Id }).ToArray(),
                People = Contributions.Select(x => x.Model).Concat(Freeloaders).ToArray(),
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
