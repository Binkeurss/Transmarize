using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TRANSMARIZE.Model;
using System.Threading.Tasks;

namespace TRANSMARIZE.ViewModels
{
    public partial class SaveWordViewModel : ViewModelBase
    {
        public SavedWord learnWord;
        public int i = 0;
        public List<SavedWord> wordList = new List<SavedWord>();

        [ObservableProperty]
        ViewModelBase content = new ListWordViewModel();
        public SaveWordViewModel() { }
        public SaveWordViewModel(ViewModelBase view)
        {
            Content = view;
        }

        [RelayCommand]
        public async void OpenFlashCard()
        {
            //wordList = await App.WordBookDatabase.GetWordsAtCurrDay();
            wordList = await App.WordBookDatabase.GetWordsAsync();
            i = 0;
            if (i < wordList.Count())
            {
                learnWord = wordList[i++];
                Content = new ReviseWordViewModel(learnWord);
            }
        }

        public void NaviRevise()
        {
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
        public void UpdateDayAndNavi0()
        {
            wordList.Add(learnWord);
            NaviRevise();
        }

        [RelayCommand]
        public void UpdateDayAndNavi1()
        {
            App.WordBookDatabase.UpdateWordAtDay(learnWord, learnWord.Date.AddDays(1));
            NaviRevise();
        }

        [RelayCommand]
        public void UpdateDayAndNavi4()
        {
            App.WordBookDatabase.UpdateWordAtDay(learnWord, learnWord.Date.AddDays(4));
            NaviRevise();
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
    }
}