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
	    private object _changeIsBusyLock = new object();
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

	    private readonly Queue<Task> _occupyTasks = new Queue<Task>();
	    private PropertyChangedEventHandler _busyChangedEventHandler;

        /// <summary>
        /// Пытается установить флаг IsBusy. Если флаг уже установлен, ожидает.
        /// </summary>
        /// <returns></returns>
	    public Task OccupyIsBusy()
        {
            lock (_changeIsBusyLock)
            {
                if (_busyChangedEventHandler == null)
                    _busyChangedEventHandler = (sender, args) =>
                    {
                        var nextTask = _occupyTasks.Dequeue();
                        nextTask.Start();
                    };

                var task = new Task(() => IsBusy = true);
                _occupyTasks.Enqueue(task);
                if (!IsBusy)
                    _busyChangedEventHandler.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
                return task;
            }
	    }

	    public void ReleaseIsBusy()
	    {
            IsBusy = false;
        }
    }
}

