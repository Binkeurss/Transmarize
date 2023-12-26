using Avalonia.Controls;
using TRANSMARIZE.Model;
using TRANSMARIZE.ViewModels;

namespace TRANSMARIZE.Views
{
    public partial class ListWordView : UserControl
    {
        public ListWordView()
        {
            InitializeComponent();
        }
        private void ListBox_SelectionChanged_1(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            SavedWord selectedWord = listBox.SelectedItem as SavedWord;
            // Nếu từ được chọn ko có giá trị thì dừng lại
            if (selectedWord is null)
            {
                return;
            }
            // Nếu có giá trị thì mở WordWindow để hiện thị nghĩa của từ
            SavedWord finalWord = selectedWord;
            WordWindow wordWindow = new WordWindow();
            wordWindow.DataContext = new WordWindowViewModel(finalWord);
            wordWindow.Show();
        }
    }
}
