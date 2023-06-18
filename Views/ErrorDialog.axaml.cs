using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ManagementApp.Views;

public partial class ErrorDialog : Window
{

    public enum MessageBoxButtons
    {
        Ok,
    }

    public enum MessageBoxResult
    {
        Ok,
    }
    public ErrorDialog()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static Task<MessageBoxResult> Show(Window parent,  string title, string text, MessageBoxButtons buttons)
    {
        var msgbox = new ErrorDialog()
        {
            Title = title
        };
        msgbox.FindControl<TextBlock>("Text").Text = text;
        var buttonPanel = msgbox.FindControl<StackPanel>("Buttons");

        var res = MessageBoxResult.Ok;

        void AddButton(string caption, MessageBoxResult r, bool def = false)
        {
            var btn = new Button { Content = caption };
            btn.Click += (_, __) => {
                res = r;
                msgbox.Close();
            };
            buttonPanel.Children.Add(btn);
            if (def)
                res = r;
        }

        AddButton("Ok", MessageBoxResult.Ok, true);

        var tcs = new TaskCompletionSource<MessageBoxResult>();
        msgbox.Closed += delegate { tcs.TrySetResult(res); };
        if (parent != null)
            msgbox.ShowDialog(parent);
        else msgbox.Show();
        return tcs.Task;
    }
}