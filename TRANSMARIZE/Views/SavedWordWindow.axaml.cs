using Avalonia.Controls;
using System.ComponentModel;
using TRANSMARIZE.Model;
using TRANSMARIZE.ViewModels;

namespace TRANSMARIZE.Views
{
    public partial class SavedWordWindow : Window
    {
        public SavedWordWindow()
        {
            InitializeComponent();
            DataContext = new SaveWordViewModel(new ListWordViewModel());
        }
        private void Window_Activated_1(object? sender, System.EventArgs e)
        {
            // Mỗi lần Window nhận Focus thì goi GetWord để load lại danh sách
            SaveWordViewModel viewModel = (SaveWordViewModel)DataContext;
            ListWordViewModel listView = viewModel.Content as ListWordViewModel;
            if (listView != null) {
                listView.GetWords();
            }
        }

        // Khi item được chọn có thay đổi tức là người dùng đã chọn gì đó
/*        private void ListBox_SelectionChanged_1(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
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
        }*/

        private void HideButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ExitButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
