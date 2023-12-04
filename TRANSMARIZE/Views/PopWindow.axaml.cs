using Avalonia.Controls;

namespace TRANSMARIZE.Views
{
    public partial class PopWindow : Window
    {
        public PopWindow()
        {
            InitializeComponent();
        }

        public void Window_Deactivated(object? sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Button_Translate(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TranslateWindow translate = new TranslateWindow();
            translate.Show();
            this.Close();
        }
    }
}
