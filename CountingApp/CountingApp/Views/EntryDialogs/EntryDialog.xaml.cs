using CountingApp.ViewModels.EntryDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views.EntryDialogs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryDialog : PopupPage
    {
        EntryDialogViewModel _vm;
        public EntryDialog()
        {
            BindingContext = new EntryDialogViewModel((text) => { });
            InitializeComponent();
        }

        public EntryDialog(EntryDialogViewModel vm)
        {
            _vm = vm;
            BindingContext = vm;
            InitializeComponent();
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        private void OnOk(object sender, EventArgs e)
        {
            _vm.OnOkAction(EntryField.Text);
            PopupNavigation.PopAsync();
        }

    }
}