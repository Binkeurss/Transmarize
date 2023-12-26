using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.ViewModels
{
    public partial class SaveWordViewModel : ViewModelBase
    {
        public static string learnWord;
        public static int i = 0;

        [ObservableProperty]
        ViewModelBase content = new ListWordViewModel();

        [RelayCommand]
        public async void NaviRevise()
        {
            List<SavedWord> wordList = await App.WordBookDatabase.GetWordsAsync();
            learnWord = wordList[i++].Content;
            Content = new ReviseWordViewModel(learnWord);
        }

        [RelayCommand]
        public void NaviReveal()
        {
            Content = new RevealWordViewModel(learnWord);
        }
        [RelayCommand]
        public void NaviList()
        {
            Content = new ListWordViewModel();
        }
        public SaveWordViewModel() 
        {
        }
        public SaveWordViewModel(ViewModelBase view)
        {
            Content = view;
        }

        /*        // Danh sách các từ hiển thị trong WordBook
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

                [RelayCommand]
                private async void ClearWords()
                {
                    var r = await App.WordBookDatabase.ClearTaskAsync();
                    GetWords();
                }*/
    }
}