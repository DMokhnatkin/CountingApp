using System;
using CountingApp.Dto;

namespace CountingApp.ViewModels.Sessions
{
    class SessionViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private DateTime _createdDateTime;
        public DateTime CreatedDateTime
        {
            get => _createdDateTime;
            set
            {
                _createdDateTime = value;
                RaisePropertyChanged(nameof(CreatedDateTime));
            }
        }

        public SessionViewModel Load(SessionDto dto)
        {
            CreatedDateTime = dto.CreatedDateTime;
            return this;
        }
    }
}