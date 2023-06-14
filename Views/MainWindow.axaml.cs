using Avalonia.Controls;
using ManagementApp.ViewModels;
using Avalonia.ReactiveUI;
using Avalonia;

namespace ManagementApp.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel viewModel;
        public MainWindow()
        {
            viewModel = new MainWindowViewModel();
            InitializeComponent();
            DataContext = viewModel;

        }

    }
}