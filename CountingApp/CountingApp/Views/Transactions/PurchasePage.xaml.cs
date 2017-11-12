using CountingApp.Controls;
using CountingApp.Helpers;
using CountingApp.ViewModels.EntryDialogs;
using CountingApp.ViewModels.Transactions;
using CountingApp.Views.EntryDialogs;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views.Transactions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchasePage : TabbedPage
    {
        public const string DoneMessage = "PurchaseDone";
        public readonly PurchaseViewModel ViewModel;
        
        public PurchasePage()
        {
            AddBarItems();
            InitializeComponent();
            BindingContext = ViewModel = new PurchaseViewModel();
        }

        public PurchasePage(PurchaseViewModel vm)
        {
            AddBarItems();
            InitializeComponent();
            BindingContext = ViewModel = vm;
            Title = vm?.Description;
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is PurchaseViewModel vm)
                vm.Navigation = Navigation;
        }

        private void AddBarItems()
        {
            ToolbarItems.Add(new ToolbarItem("Edit", "ic_edit_white_24dp.png", async () =>
            {
                await Navigation.PushPopupAsync(new EntryDialog(
                            new EntryDialogViewModel(async (text) => {
                                if (!string.IsNullOrEmpty(text))
                                    Title = text;
                            })
                            {
                                Title = "Введите название покупки",
                                Placeholder = "Например, \"Чипсы\""
                            }));
            }));
            ToolbarItems.Add(new ToolbarItem("Done", "done.png", () => { }));

        }
    }
}