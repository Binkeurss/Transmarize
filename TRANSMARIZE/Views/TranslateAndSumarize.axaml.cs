using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia;
using System;


namespace TRANSMARIZE.Views
{
    public partial class TranslateAndSumarize : Window
    {
        public TranslateAndSumarize()
        {
            InitializeComponent();
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
}
