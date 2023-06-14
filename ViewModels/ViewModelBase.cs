using Avalonia;
using ReactiveUI;
using System.Windows.Input;

namespace ManagementApp.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public class ButtonItem
        {
            public string Content { get; set; }
            public Thickness Margin { get; set; }
            public int Tag { get; set; }
            public ICommand Command { get; set; }
            public int CommandParameter { get; set; }
        }
    }
}