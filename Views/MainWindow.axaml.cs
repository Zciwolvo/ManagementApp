using Avalonia.Controls;
using ManagementApp.ViewModels;
using Avalonia.ReactiveUI;

namespace ManagementApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}