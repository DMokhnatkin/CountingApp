using System.Collections.ObjectModel;
using System.Linq;
using CountingApp.Models;

namespace CountingApp.ViewModels
{
    public class PurchaseItemViewModel : BaseViewModel
    {
        private Purchase _model;

        public PurchaseItemViewModel(Purchase model)
        {
            _model = model;

            Contributions = new ObservableCollection<ContributionItemViewModel>(_model.Contributions?.Select(x => new ContributionItemViewModel(x)) ?? new ContributionItemViewModel[0]);
            Freeloaders = new ObservableCollection<string>(_model.Contributions?.Select(x => x.PersonId.DisplayName) ?? new string[0]);
        }

        #region Properties

        private ObservableCollection<ContributionItemViewModel> _contributions;
        public ObservableCollection<ContributionItemViewModel> Contributions
        {
            get => _contributions;
            set => SetProperty(ref _contributions, value);
        }

        private ObservableCollection<string> _freeloaders;
        public ObservableCollection<string> Freeloaders
        {
            get => _freeloaders;
            set => SetProperty(ref _freeloaders, value);
        }

        #endregion
    }
}
