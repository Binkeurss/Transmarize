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
                TranslateWindow translate = new TranslateWindow();
                // Gán DataContext của PopWindow cho TransWindow
                translate.DataContext = this.DataContext;
                translate.Show();
            }
            Task.Delay(25);
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
