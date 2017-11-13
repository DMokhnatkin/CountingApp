using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CountingApp.ViewModels.EntryDialogs
{
    public class EntryDialogViewModel : BaseViewModel
    {


        #region Observable
        private string _title;
        public new string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder;
            set => SetProperty(ref _placeholder, value);
        }

        private Keyboard _keyboardType;
        public Keyboard KeyboardType
        {
            get => _keyboardType ?? Keyboard.Default;
            set => SetProperty(ref _keyboardType, value);
        }


        #endregion

        public Action<string> OnOkAction {get; private set;}

        public EntryDialogViewModel(Action<string> doOnOk)
        {
            OnOkAction = doOnOk;
        }
    }
}
