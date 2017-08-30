using CountingApp.Models;

namespace CountingApp.ViewModels
{
    public class ContributionItemViewModel : BaseViewModel
    {
        private readonly Contribution _model;

        public ContributionItemViewModel(Contribution model)
        {
            _model = model;
        }

        #region Properties

        public string PersonDisplayName => _model.PersonId.DisplayName;

        public decimal Amount => _model.Amount;

        #endregion
    }
}
