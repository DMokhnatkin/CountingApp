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
    public partial class CreateTransactionPage : TabbedPage
    {
        public CreateTransactionPage ()
        {
            InitializeComponent();
        }
    }
}