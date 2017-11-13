using CountingApp.Data.Repositories.People;
using CountingApp.Models;
using CountingApp.ViewModels.EntryDialogs;
using CountingApp.Views.EntryDialogs;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CountingApp.ViewModels
{
    public class SelectPersonViewModel : BaseViewModel
    {
        private readonly IPeopleRepository _peopleRepository;

        public const string SelectedMessage = "Selected";

        public SelectPersonViewModel()
        {
#if DEBUGUI
            _peopleRepository = new PeopleRepository();
#else
            _peopleRepository = new HttpPeopleRepository();
#endif
        }


        //by name and value like: contributionAmount : 100 rubs
        public Dictionary<string, object> AdditionalParameters { get; } = new Dictionary<string, object>();
        public INavigation Navigation { get; set; }

        public Person SelectedPerson { get; private set; }
#region Observable

        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        private Command<Person> _selectPerson;
        public Command<Person> SelectPerson
        {
            get
            {
                return _selectPerson ?? (_selectPerson = new Command<Person>(
                    async (person) =>
                    {
                        await Navigation.PushPopupAsync(new EntryDialog(
                            new EntryDialogViewModel(async (text) => {
                                SelectedPerson = person;
                                decimal amount;
                                if (!decimal.TryParse(text, out amount))
                                    amount = 0;
                                AdditionalParameters.Add("amount", amount);
                                MessagingCenter.Send(this, SelectedMessage);
                                await Navigation.PopAsync(true);
                            })
                            {
                                Title = "Сколько заплатил?",
                                Placeholder = "0 рублей",
                                KeyboardType = Keyboard.Numeric
                            }));

                    }));
            }
        }

#endregion

        public async Task LoadPeopleListAsync()
        {
            OccupyIsBusy();
            var peopleDtos = await _peopleRepository.GetAvailablePeopleAsync() ?? new Core.Dto.PersonDto[0];
            People = new ObservableCollection<Person>(peopleDtos.Select(p => new Person(p.Id, p.DisplayName)));
            ReleaseIsBusy();
        }

    }
}
