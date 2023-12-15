using Avalonia.Controls;
using System.Threading.Tasks;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Views
{
    public partial class FeaturesView : UserControl
    {
        public FeaturesView()
        {
            InitializeComponent();
        }
        private async void CopySourceButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Delay(1000);
            var topLevel = TopLevel.GetTopLevel(this);
            topLevel.Clipboard.SetTextAsync(SourceText.Text);
        }

        private async void CopyTransedButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Delay(1000);
            var topLevel = TopLevel.GetTopLevel(this);
            topLevel.Clipboard.SetTextAsync(OutputText.Text);
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
