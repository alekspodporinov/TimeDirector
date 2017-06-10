using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeDIrector.Client.ViewModels
{
	public class NotifyViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void SetField<T>(ref T field, T value, string propertyName, Action<T> callback = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
				return;

			field = value;
			OnPropertyChanged(propertyName);

			callback?.Invoke(value);
		}

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void Invoke(Action action)
		{
			if (Application.Current == null)
				return;

			Application.Current.Dispatcher.Invoke(action);
		}

		protected virtual void GoUrl(string url)
		{
			var uri = new Uri(url).ToString();
			Task.Factory.StartNew(() => Process.Start(uri.Replace("\"", "\\\"")));
		}
	}
}
