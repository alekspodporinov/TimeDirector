﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeDIrector.Client.ViewModels;

namespace TimeDIrector.Client {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	partial class MainWindow : Window {
		MainWindowViewModel _viewModel;
		
		public MainWindow(MainWindowViewModel viewModel) {
			DataContext = _viewModel = viewModel;
			InitializeComponent();
			Hide();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Hide();
		}
	}
}
