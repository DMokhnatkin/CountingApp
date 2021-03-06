﻿using CountingApp.Models;
using CountingApp.ViewModels;
using CountingApp.ViewModels.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views.Transactions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FreeloadersPage : ContentPage
    {
        public FreeloadersPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // TODO: remove
            // Нам не нужно выделение в ListView
            if (sender is ListView listView)
            {
                listView.SelectedItem = null;
            }
        }
    }
}