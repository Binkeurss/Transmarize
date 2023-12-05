using Avalonia.Controls;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Views
{
    public partial class WordWindow : Window
    {
        public WordWindow()
        {
            InitializeComponent();
        }

        public void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //Environment.Exit(0);
            this.Close();
        }
        private void Window_Deactivated(object? sender, System.EventArgs e)
        {
            this.Close();
        }

        public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
