using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.ViewModels
{
    public partial class SaveWordViewModel : ViewModelBase
    {
        // Danh sách các từ hiển thị trong WordBook
        [ObservableProperty]
        private List<SavedWord> wordList = new List<SavedWord>();

        // Biến selectedWord dùng để lưu từ mà người dùng chọn trong danh sách
        [ObservableProperty]
        private SavedWord selectedWord;

        public SaveWordViewModel()
        {
            // Mỗi lần WordBook được mở thì cập nhật danh sách 
            GetWords();
        }

        [RelayCommand]
        public async void GetWords()
        {
            WordList = await App.WordBookDatabase.GetWordsAsync();
        }
    }
}