using Avalonia.Controls;
using System.Threading.Tasks;
using TRANSMARIZE.Model;
using TRANSMARIZE.ViewModels;

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
            int numWord = CountWord(ShareData.transText);
            if (numWord == 1)
            {
                WordWindow wordWindow = new WordWindow();
                wordWindow.DataContext = new WordWindowViewModel();
                wordWindow.Show();
            }
            else
            {
                //TranslateWindow translate = new TranslateWindow();
                // Gán DataContext của PopWindow cho TransWindow
                if (App.TranslateWindow.IsVisible == true)
                {
                    App.TranslateWindow.Close();
                }
                App.TranslateWindow.Show();
                App.TranslateWindow.DataContext = this.DataContext;
                App.TranslateWindow.Activate();
            }
            Task.Delay(25);
            this.Close();
        }
        private void SummarizeButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (App.SumWindow.IsVisible == true)
            {
                App.SumWindow.Close();
            }
            App.SumWindow.Show();
            // Gán DataContext của PopWindow cho SumWindow
            App.SumWindow.DataContext = this.DataContext;
            App.SumWindow.Activate();
            this.Close();
        }
        private int CountWord(string inputString)
        {
            int wordCount = 0;
            bool isPreviousCharSpace = true;

            foreach (char character in inputString)
            {
                if (character == ' ')
                {
                    isPreviousCharSpace = true;
                }
                else if (isPreviousCharSpace)
                {
                    wordCount++;
                    isPreviousCharSpace = false;
                }
            }
            return wordCount;
        }
        public void Window_Deactivated(object? sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
