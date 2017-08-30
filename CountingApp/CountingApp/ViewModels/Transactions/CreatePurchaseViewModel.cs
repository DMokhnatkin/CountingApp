using System;
using System.Collections.ObjectModel;
using CountingApp.Models;

namespace CountingApp.ViewModels.Transactions
{
    public class CreatePurchaseViewModel : BaseViewModel
    {
        public ObservableCollection<Person> Contributors { get; set; }

        public CreatePurchaseViewModel()
        {
            Contributors = new ObservableCollection<Person>
            {
                new Person {DisplayName = "Test1", Id = new Guid()},
                new Person {DisplayName = "Test2", Id = new Guid()}
            };
        }
    }
}
