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
                AiFeature("Translate");
            }
            // Task.Delay(25);
        }
        private async void SummarizeButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AiFeature("Summarize");
        }
        private void ExplainButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AiFeature("Explain");
        }
        private void AiFeature(string feature)
        {
            // mainVM là Datacontext của NaviFeaturesWindow
            NaviFeaturesWindowViewModel mainVM = new NaviFeaturesWindowViewModel();
            // featureVM là ViewModel của FeaturesView, được truyền lại từ MainWindow thông qua PopWindow
            FeaturesViewModel featureVM = this.DataContext as FeaturesViewModel;
            featureVM.SetFeatureType(feature);

            // Content là  1 thuộc tính kiểu ViewModelBase, là nội dung sẽ hiển thị lên Window
            mainVM.Content = featureVM;
            // ViewLocator sẽ tìm View ứng với kiểu của Content, tức FeaturesView
            // Khi đó Window sẽ hiện thị UI ứng với View tương ứng của Content bấy giờ
            App.NaviFeaturesWindow.DataContext = mainVM;

            App.NaviFeaturesWindow.Show();
            if (App.NaviFeaturesWindow.IsActive == false)
            {
                App.NaviFeaturesWindow.Topmost = true;
                App.NaviFeaturesWindow.Activate();
                App.NaviFeaturesWindow.Topmost = false;
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
