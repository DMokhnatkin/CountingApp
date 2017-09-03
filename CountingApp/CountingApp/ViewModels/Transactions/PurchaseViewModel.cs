using System.Collections.ObjectModel;
using System.Linq;
using CountingApp.Data.Repositories.People;
using CountingApp.Models;
using CountingApp.Models.Transactions;
using Xamarin.Forms;

namespace CountingApp.ViewModels.Transactions
{
    public class PurchaseViewModel : BaseViewModel
    {
        public PurchaseViewModel()
        {
            Contributors = new ObservableCollection<ContributionViewModel>();
            Freeloaders = new ObservableCollection<Person>();
        }

        public void Load()
        {
            new PeopleRepository().GetAvailablePeopleAsync().ContinueWith(task =>
            {
                Contributors.Add(new ContributionViewModel(task.Result[0]));

                Freeloaders.Add(task.Result[1]);
                Freeloaders.Add(task.Result[2]);
                Freeloaders.Add(task.Result[3]);
                Freeloaders.Add(task.Result[4]);
            });
        }

        private ObservableCollection<ContributionViewModel> _contributors;
        public ObservableCollection<ContributionViewModel> Contributors
        {
            get => _contributors;
            set
            {
                _contributors = value;
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
                Contributions = Contributors.Select(x => new Contribution{ Amount = x.Amount, PersonId = x.Model.Id }).ToArray(),
                People = Contributors.Select(x => x.Model).Concat(Freeloaders).ToArray()
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
