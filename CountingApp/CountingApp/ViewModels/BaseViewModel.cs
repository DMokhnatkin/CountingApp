using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using CountingApp.Helpers;
using CountingApp.Models;

using Xamarin.Forms;

namespace CountingApp.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            private set => SetProperty(ref _isBusy, value);
        }

        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string _title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        readonly SemaphoreSlim _busySemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Пытается установить флаг IsBusy. Если флаг уже установлен, ожидает.
        /// </summary>
        /// <returns></returns>
	    public void OccupyIsBusy()
        {
            _busySemaphore.Wait();
            Device.BeginInvokeOnMainThread(() => IsBusy = true);
        }

        public void ReleaseIsBusy()
        {
            Device.BeginInvokeOnMainThread(() => IsBusy = false);
            _busySemaphore.Release();
        }
    }
}

