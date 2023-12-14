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
                Task.Delay(50); // thử delay một chút để xem có hết dựt ko
                wordWindow.Show();
            }
            else
            {
                FeaturesWindowViewModel viewmodel = this.DataContext as FeaturesWindowViewModel;
                viewmodel.SetFeatureType("Translate");
                App.FeaturesWindow.DataContext = viewmodel;
                App.FeaturesWindow.Show();
                if (App.FeaturesWindow.IsActive == false)
                {
                    App.FeaturesWindow.Topmost = true;
                    App.FeaturesWindow.Activate();
                    App.FeaturesWindow.Topmost = false;
                }
            }
            Task.Delay(25);
            this.Close();
        }
        private void SummarizeButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            FeaturesWindowViewModel viewmodel = this.DataContext as FeaturesWindowViewModel;
            viewmodel.SetFeatureType("Summarize");
            App.FeaturesWindow.DataContext = viewmodel; 
            App.FeaturesWindow.Show();
            if (App.FeaturesWindow.IsActive == false)
            {
                App.FeaturesWindow.Topmost = true;
                App.FeaturesWindow.Activate();
                App.FeaturesWindow.Topmost = false;
            }
        }
        private void ExplainButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Gán DataContext
            FeaturesWindowViewModel viewmodel = this.DataContext as FeaturesWindowViewModel;
            viewmodel.SetFeatureType("Explain");
            App.FeaturesWindow.DataContext = viewmodel;
            App.FeaturesWindow.Show();
            if (App.FeaturesWindow.IsActive == false)
            {
                App.FeaturesWindow.Topmost = true;
                App.FeaturesWindow.Activate();
                App.FeaturesWindow.Topmost = false;
            }
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
