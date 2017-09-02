using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CountingApp.Data.Repositories.People;
using CountingApp.Models;

namespace CountingApp.ViewModels
{
    public class SelectPeoplePageViewModel : BaseViewModel
    {
        private readonly IPeopleRepository _peopleRepository;

        public SelectPeoplePageViewModel()
        {
            _peopleRepository = new PeopleRepository();
        }

        public const string ApplyMessage = "Apply";

        private ObservableCollection<SelectPersonViewModel> _people;
        public ObservableCollection<SelectPersonViewModel> People
        {
            get => _people;
            set
            {
                _people = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadPeopleListAsync()
        {
            IsBusy = true;
            var people = await _peopleRepository.GetAvailablePeopleAsync() ?? new Person[0];
            People = new ObservableCollection<SelectPersonViewModel>(people.Select(x => new SelectPersonViewModel(x)));
            IsBusy = false;
        }

        public void SetSelected(Person[] people)
        {
            var selectedDict =
                new Dictionary<Guid, Person>(people.Select(x => new KeyValuePair<Guid, Person>(x.Id, x)));
            foreach (var personViewModel in People)
            {
                if (selectedDict.ContainsKey(personViewModel.PersonModel.Id))
                    personViewModel.IsSelected = true;
            }
        }

        public Person[] GetSelected()
        {
            return People.Where(x => x.IsSelected).Select(x => x.PersonModel).ToArray();
        }

        public class SelectPersonViewModel : BaseViewModel
        {
            public Person PersonModel { get; }

            public SelectPersonViewModel(Person person)
            {
                PersonModel = person;
                OnPropertyChanged(nameof(DisplayName));
            }

            public string DisplayName => PersonModel.DisplayName;

            private bool _isSelected;
            public bool IsSelected
            {
                get => _isSelected;
                set
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }
    }


}
