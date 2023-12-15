using Avalonia.Controls;
using System.Threading.Tasks;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Views
{
    public partial class NaviFeaturesWindow : Window
    {
        public NaviFeaturesWindow()
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
    }
}
