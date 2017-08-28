using System.Collections.ObjectModel;
using System.Linq;
using CountingApp.Dto;

namespace CountingApp.ViewModels.Sessions
{
    class SessionsViewModel : ViewModelBase
    {
        private ObservableCollection<SessionViewModel> _sessionCollection;
        public ObservableCollection<SessionViewModel> SessionCollection
        {
            get => _sessionCollection;
            set
            {
                _sessionCollection = value;
                RaisePropertyChanged(nameof(SessionCollection));
            }
        }

        public void Load(SessionCollectionDto dto)
        {
            SessionCollection = new ObservableCollection<SessionViewModel>(dto.Sessions.Select(x => new SessionViewModel().Load(x)));
        }
    }
}