using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia;

namespace TRANSMARIZE.Views;

public partial class MainWindow : Window
{
    public Button toggleButton;
    public bool isRunning;
    public  MainWindow()
    {
        InitializeComponent();
        toggleButton = this.Find<Button>("ToggleButton");
        toggleButton.Click += ToggleButtonClick;
    }

    private void ToggleButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        isRunning = !isRunning;
        UpdateButtonLabel();
    }

    private void UpdateButtonLabel()
    {
        toggleButton.Content = isRunning ? "STOP" : "START";
    }
}
