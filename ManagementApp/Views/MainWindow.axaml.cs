using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ManagementApp.ViewModels;
using System;


namespace ManagementApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
            InitializeComponent();
            
        }

    }
}