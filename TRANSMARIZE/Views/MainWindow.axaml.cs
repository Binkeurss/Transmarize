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
    public bool isRunning;
    public Button ThoatButton;
    public  MainWindow()
    {
        InitializeComponent();
        toggleButton = this.Find<Button>("ToggleButton");
        toggleButton.Click += ToggleButtonClick;
        ThoatButton = this.Find<Button>("ExitButton");
        ThoatButton.Click += ExitButton_Click;
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

    public void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        //Environment.Exit(0);
        this.Close();
    }

    public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Hide();
    }
}
