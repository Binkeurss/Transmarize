using Avalonia.Controls;
using System.Threading.Tasks;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Views
{
    public partial class FeaturesWindow : Window
    {
        public FeaturesWindow()
        {
            InitializeComponent();
        }
        public async void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Hide();
            await Task.Delay(1000);
            ShareData.currentText = string.Empty;
        }

        public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private async void CopySourceButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Delay(1000);
            Clipboard.SetTextAsync(SourceText.Text);
        }

        private async void CopyTransedButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Delay(1000);
            Clipboard.SetTextAsync(OutputText.Text);
            ShareData.currentText = OutputText.Text;
        }

        private async void Flyout_Opened(object? sender, System.EventArgs e)
        {
            var flyOut = sender as Flyout;
            await Task.Delay(2000);
            flyOut.Hide();
        }
    }
}
