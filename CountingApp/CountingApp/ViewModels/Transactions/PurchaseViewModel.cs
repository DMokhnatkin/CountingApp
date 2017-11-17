using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CountingApp.Models;
using CountingApp.Models.Transactions;
using CountingApp.Data.Repositories.People;
using Xamarin.Forms;
using CountingApp.Views;
using CountingApp.Helpers;

namespace CountingApp.ViewModels.Transactions
{
    public class PurchaseViewModel : BaseViewModel
    {
        /// <summary>
        /// Ид транзакции (Guid.Empty если мы создаем новую)
        /// </summary>
        private readonly Guid _transactionId;
        private readonly Purchase _model;

        //todo refactor, done to use same veiwmodel for subviews 
        public PurchaseViewModel ViewModel => this;

        public INavigation Navigation { get; set; }

        public PurchaseViewModel()
        {
            _model = new Purchase();
            Contributions = new ObservableCollection<Contribution>();
            Freeloaders = new ObservableCollection<Person>();

        }


        public PurchaseViewModel(Purchase model)
        {
            _model = model;
            Contributions = new ObservableCollection<Contribution>(model.Contributions);
            Freeloaders = new ObservableCollection<Person>(model.ExtractFreeloaders());

            Description = model.Description;

            _transactionId = model.Id;
        }

        #region Observable

        private string _description = "Покупка";
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }


        private ObservableCollection<Contribution> _contributions;
        public ObservableCollection<Contribution> Contributions
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

        private Command<Contribution> _removeContribution;
        public Command<Contribution> RemoveContribution
        {
            get
            {
                return _removeContribution ??
                    (_removeContribution = new Command<Contribution>(
                       (contribution) => Contributions.Remove(contribution)
                     ));
            }
        }

        private Command<Person> _removeFreeloader;
        public Command<Person> RemoveFreeloader
        {
            get
            {
                return _removeFreeloader ??
                    (_removeFreeloader = new Command<Person>(
                       (person) => Freeloaders.Remove(person)
                     ));
            }
        }

        private Command _addContributor;
        public Command AddContributor
        {
            get
            {
                return _addContributor ??
                    (_addContributor = new Command(
                        (p) => AddContributorImpl()
                    ));
            }
        }

        private Command _addFreeloader;
        public Command AddFreeloader
        {
            get
            {
                return _addFreeloader ??
                    (_addFreeloader = new Command(
                        (p) => AddFreeloaderImpl()
                    ));
            }
        }

        #endregion

        public Purchase GetModel()
        {
            var people = Freeloaders.ToList();
            people.AddRange(Contributions.Select(c => c.Person));//TODO
            return new Purchase
            {
                Id = _transactionId,
                Contributions = Contributions.ToArray(),
                People = people.ToArray(),//ContributionVM.Contributions+ FrealodersVM.FreeLoaders
                Description = Description,
                Timestamp = DateTime.Now
            };
        }

        private async void AddContributorImpl()
        {
            var selectPersonViewModel = new SelectPersonViewModel();
            await selectPersonViewModel.LoadPeopleListAsync();
            var selectPersonPage = new SelectPersonPage(selectPersonViewModel);

            MessagingCenter.Subscribe<SelectPersonViewModel>(this, SelectPersonViewModel.SelectedMessage, selectPersonVM =>
            {
                Contributions.Add(new Contribution
                {
                    Person = selectPersonVM.SelectedPerson,
                    AmountRub = (decimal)selectPersonVM.AdditionalParameters["amount"]
                });

                // Не забываем если человек был назначен в спонсоры, его нужно убрать их нахлебников
                Freeloaders = new ObservableCollection<Person>(Freeloaders.Where(x => x.Id == selectPersonVM.SelectedPerson.Id));

                MessagingCenter.Unsubscribe<SelectPersonViewModel>(this, SelectPersonViewModel.SelectedMessage);
            });
            await Navigation.PushAsync(selectPersonPage);
        }


        private async void AddFreeloaderImpl()
        {
            var selectPeopleViewModel = new SelectPeoplePageViewModel();
#pragma warning disable 4014
            selectPeopleViewModel
                .LoadPeopleListAsync()
                .ContinueWith(task =>
                {
                    selectPeopleViewModel.SetSelected(Freeloaders.ToArray());
                    selectPeopleViewModel.SetUnActive(Contributions.Select(x => x.Person.Id).ToArray());
                });
#pragma warning restore 4014
            var selectPeoplePage = new SelectPeoplePage(selectPeopleViewModel);

            MessagingCenter.Subscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage, selectPeoplePageViewModel =>
            {
                Freeloaders = new ObservableCollection<Person>(selectPeoplePageViewModel.GetSelected());

                MessagingCenter.Unsubscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage);
            });
            await Navigation?.PushAsync(selectPeoplePage);
        }
    }
}
