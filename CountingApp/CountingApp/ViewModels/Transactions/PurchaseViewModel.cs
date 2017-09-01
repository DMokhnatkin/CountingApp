using System.Collections.ObjectModel;
using CountingApp.Data.Repositories.People;
using CountingApp.Models;
using Xamarin.Forms;

namespace CountingApp.ViewModels.Transactions
{
    public class PurchaseViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        public PurchaseViewModel(INavigation navigation)
        {
            _navigation = navigation;

            Contributors = new ObservableCollection<Person>();
            Freeloaders = new ObservableCollection<Person>();
        }

        public void Load()
        {
            new PeopleRepository().GetAvailablePeopleAsync().ContinueWith(task =>
            {
                Contributors.Add(task.Result[0]);

                Freeloaders.Add(task.Result[1]);
                Freeloaders.Add(task.Result[2]);
                Freeloaders.Add(task.Result[3]);
                Freeloaders.Add(task.Result[4]);
            });
        }

        private ObservableCollection<Person> _contributors;
        public ObservableCollection<Person> Contributors
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
    }
}
