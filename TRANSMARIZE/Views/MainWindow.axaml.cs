using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia;
using System;

namespace TRANSMARIZE.Views;

public partial class MainWindow : Window
{
    public Button toggleButton;
    public bool isRunning = false;
    public Button ThoatButton;
    public  MainWindow()
    {
        InitializeComponent();
    }

    private void ToggleButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Button button = (Button)sender;
        isRunning = !isRunning;
        button.Content = isRunning ? "STOP" : "START";
    }

    public void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        //Environment.Exit(0);
        this.Close();
    }

    public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }
}
