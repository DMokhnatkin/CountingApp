﻿using CountingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPersonPage : ContentPage
    {
        public SelectPersonPage()
        {
            InitializeComponent();
        }

        public SelectPersonPage(SelectPersonViewModel vm)
            : this()
        {
            BindingContext = vm;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is SelectPersonViewModel vm)
                vm.Navigation = Navigation;
        }
    }
}