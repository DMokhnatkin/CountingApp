using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CountingApp.Data.Mappers;
using CountingApp.Data.Repositories.People;
using CountingApp.Models;

namespace CountingApp.ViewModels
{
    public class SelectPeoplePageViewModel : BaseViewModel
    {
        private readonly IPeopleRepository _peopleRepository;

        public SelectPeoplePageViewModel()
        {
            _peopleRepository = new HttpPeopleRepository();
        }

        public const string ApplyMessage = "Apply";

        #region Observable

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

        #endregion

        public async Task LoadPeopleListAsync()
        {
            OccupyIsBusy();
            var people = (await _peopleRepository.GetAvailablePeopleAsync())?.Select(x => x.Unmap()) ?? new Person[0];
            People = new ObservableCollection<SelectPersonViewModel>(people.Select(x => new SelectPersonViewModel(x)));
            ReleaseIsBusy();
        }

        /// <summary>
        /// Установить выбранных людей
        /// </summary>
        public void SetSelected(Person[] people)
        {
            var selectedDict =
                new Dictionary<string, Person>(people.Select(x => new KeyValuePair<string, Person>(x.Id, x)));
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

        /// <summary>
        /// Установить людей, которых нельзя выбрать
        /// </summary>
        public void SetUnActive(string[] peopleIds)
        {
            var peopleIdsSet = new HashSet<string>(peopleIds);

            foreach (var personViewModel in People)
            {
                if (peopleIdsSet.Contains(personViewModel.PersonModel.Id))
                    personViewModel.IsActive = false;
            }
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

            private bool _isActive = true;
            public bool IsActive
            {
                get => _isActive;
                set => SetProperty(ref _isActive, value);
            }
        }
    }


}
