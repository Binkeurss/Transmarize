using Avalonia.Controls;

namespace TRANSMARIZE.Views
{
    public partial class PopWindow : Window
    {
        public PopWindow()
        {
            InitializeComponent();
        }
        private void TranslateButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TranslateWindow translate = new TranslateWindow();
            // Gán DataContext của PopWindow cho TransWindow
            translate.DataContext = this.DataContext;
            translate.Show();
            this.Close();
        }
        private void SummarizeButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SumWindow sumWindow = new SumWindow();
            // Gán DataContext của PopWindow cho SumWindow
            sumWindow.DataContext = this.DataContext;
            sumWindow.Show();
            this.Close();
        }
        public void Window_Deactivated(object? sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
