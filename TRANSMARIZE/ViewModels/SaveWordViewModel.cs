using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.ViewModels
{
    public partial class SaveWordViewModel : ViewModelBase
    {
        public static SavedWord learnWord;
        public static int i = 0;

        [ObservableProperty]
        ViewModelBase content = new ListWordViewModel();

        [RelayCommand]
        public async void NaviRevise()
        {
            List<SavedWord> wordList = await App.WordBookDatabase.GetWordsAsync();
            if (i < wordList.Count())
            {
                learnWord = wordList[i++];
                Content = new ReviseWordViewModel(learnWord);
            }
            else
            {
                Content = new ListWordViewModel();
            }
        }

        [RelayCommand]
        public void NaviReveal()
        {
            Content = new WordTransViewModel(learnWord);
        }
        [RelayCommand]
        public void NaviList()
        {
            Content = new ListWordViewModel();
        }
        public SaveWordViewModel() { }
        public SaveWordViewModel(ViewModelBase view)
        {
            Content = view;
        }
    }
}