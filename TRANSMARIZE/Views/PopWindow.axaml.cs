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
            TranslateAndSumarize translate = new TranslateAndSumarize();
            translate.Show();
            this.Close();
        }
    }
}
