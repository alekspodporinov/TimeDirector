using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using TimeDIrector.Client.Autofac;
using TimeDIrector.Client.ViewModels;

namespace TimeDIrector.Client {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : System.Windows.Application//TODO:Сделать таймер на исчезновение окна
	{
		private NotifyIcon _notifyIcon;
		private bool _isExit;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			var viewModel = Injection.Resolve<MainWindowViewModel>();
			MainWindow = new MainWindow(viewModel);
			
			MainWindow.Closing += MainWindowClosing;

			_notifyIcon = new NotifyIcon();
			_notifyIcon.DoubleClick += (s, args) => ShowMainWindow();
			_notifyIcon.Icon = new Icon(typeof(App), "Hamzasaleem-Stock-Dashboard.ico");
			_notifyIcon.Visible = true;

			CreateContextMenu();
		}

		private void CreateContextMenu()
		{
			_notifyIcon.ContextMenuStrip = new ContextMenuStrip();
			_notifyIcon.ContextMenuStrip.Items.Add("Time Director...").Click += (s, e) => ShowMainWindow();
			_notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
		}

		private void ExitApplication()
		{
			_isExit = true;
			MainWindow.Close();
			_notifyIcon.Dispose();
			_notifyIcon = null;
		}

		private void ShowMainWindow()
		{
			SetWindowToBottomRightOfScreen();
			MainWindow.Show();
		}

		private void SetWindowToBottomRightOfScreen()
		{
			MainWindow.Left = SystemParameters.WorkArea.Width - MainWindow.Width - 15;
			MainWindow.Top = SystemParameters.WorkArea.Height - MainWindow.Height - 40;
		}

		private void MainWindowClosing(object sender, CancelEventArgs e)
		{
			if (!_isExit)
			{
				e.Cancel = true;
				MainWindow.Hide();
			}
			Environment.Exit(0);
		}
	}
}
