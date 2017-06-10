using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimeDIrector.Client.ViewModels
{
	class RelayCommand : ICommand
	{
		private Action<object> _execute;
		private Predicate<object> _canExecute;
		private event EventHandler CanExecuteChangedInternal;

		public RelayCommand(Action<object> execute)
			: this(execute, DefaultCanExecute)
		{

		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			_execute = execute;
			_canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
				CanExecuteChangedInternal += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
				CanExecuteChangedInternal -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute != null && _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			if (CanExecute(parameter))
				_execute(parameter);
		}

		public void OnCanExecuteChanged()
		{
			EventHandler handler = CanExecuteChangedInternal;
			if (handler != null)
			{
				handler.Invoke(this, EventArgs.Empty);
			}
		}

		public void Destroy()
		{
			_canExecute = _ => false;
			_execute = _ => { };
		}

		private static bool DefaultCanExecute(object parameter)
		{
			return true;
		}
	}
}
