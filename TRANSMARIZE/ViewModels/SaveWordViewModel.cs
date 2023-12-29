using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TRANSMARIZE.Model;
using System.Threading.Tasks;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using TRANSMARIZE.Views;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Models;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace TRANSMARIZE.ViewModels
{
    public partial class SaveWordViewModel : ViewModelBase
    {
        public SavedWord currWord;
        public int i = 0;
        public List<SavedWord> cardList = new List<SavedWord>();

        [ObservableProperty]
        ViewModelBase content = new ListWordViewModel();
        public SaveWordViewModel() { }
        public SaveWordViewModel(ViewModelBase view)
        {
            Content = view;
        }

/*        public async void OpenFlashCard()
        {
            //wordList = await App.WordBookDatabase.GetWordsAtCurrDay();
            cardList = await App.WordBookDatabase.GetWordsAsync();
            i = 0;
            if (i < cardList.Count())
            {
                learnWord = cardList[i++];
                Content = new ReviseWordViewModel(learnWord);
            }
        }*/

        [RelayCommand]
        public async void ReviseButton()
        {
            //cardList = await App.WordBookDatabase.GetWordsAsync();
            cardList = await App.WordBookDatabase.GetWordsAtCurrDay();

            var readyCustomBox = MessageBoxManager.GetMessageBoxCustom(
                new MessageBoxCustomParams
                {
                    ButtonDefinitions = new List<ButtonDefinition>
                    {
                        new ButtonDefinition { Name = "Let's go!", },
                        new ButtonDefinition { Name = "I'm lazy", }
                    },
                    ContentHeader = "NOTIFICATION",  
                    ContentMessage = $"You have {cardList.Count()} words left to learn today",

                    FontFamily = new Avalonia.Media.FontFamily("avares://TRANSMARIZE/Assets/Fonts#Viga"),
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    ShowInCenter = true,
                    Icon = MsBox.Avalonia.Enums.Icon.Info,
                });

            var freeCustomBox = MessageBoxManager.GetMessageBoxCustom(
                new MessageBoxCustomParams
                {
                    ButtonDefinitions = new List<ButtonDefinition>
                    {
                        new ButtonDefinition { Name = "Yeah!", }
                    },
                    ContentHeader = "GOOD NEWS!",
                    ContentMessage = "There's nothing for you to learn today",

                    FontFamily = new Avalonia.Media.FontFamily("avares://TRANSMARIZE/Assets/Fonts#Viga"),
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    ShowInCenter = true,
                    Icon = MsBox.Avalonia.Enums.Icon.Success,
                });

            if (cardList.Count() == 0)
            {
                var freeResult = await freeCustomBox.ShowAsPopupAsync(App.SavedWordWindow);
                return;
            }

            var readyResult = await readyCustomBox.ShowAsPopupAsync(App.SavedWordWindow);
            if (readyResult == "Let's go!")
            {
                i = 0;
                if (i < cardList.Count())
                {
                    currWord = cardList[i++];
                    Content = new ReviseWordViewModel(currWord);
                }
            }
        }

        public void NextWord()
        {
            if (i < cardList.Count())
            {
                currWord = cardList[i++];
                Content = new ReviseWordViewModel(currWord);
            }
            else
            {
                var completeCustomBox = MessageBoxManager.GetMessageBoxCustom(
                    new MessageBoxCustomParams
                    {
                        ButtonDefinitions = new List<ButtonDefinition>
                        {
                            new ButtonDefinition { Name = "Yeah!", }
                        },
                        ContentHeader = "CONGRATULATION!",
                        ContentMessage = "You finished today's revision",

                        FontFamily = new Avalonia.Media.FontFamily("avares://TRANSMARIZE/Assets/Fonts#Viga"),
                        SizeToContent = SizeToContent.WidthAndHeight,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        ShowInCenter = true,
                        Icon = MsBox.Avalonia.Enums.Icon.Success,
                    });

                Content = new ListWordViewModel();
                var result = completeCustomBox.ShowAsPopupAsync(App.SavedWordWindow);
            }
        }

        [RelayCommand]
        public void UpdateDayAndNext0()
        {
            cardList.Add(currWord);
            NextWord();
        }

        [RelayCommand]
        public void UpdateDayAndNext1()
        {
            App.WordBookDatabase.UpdateWordAtDay(currWord, currWord.Date.AddDays(1));
            NextWord();
        }

        [RelayCommand]
        public void UpdateDayAndNext4()
        {
            App.WordBookDatabase.UpdateWordAtDay(currWord, currWord.Date.AddDays(4));
            NextWord();
        }

        [RelayCommand]
        public void RevealWord()
        {
            Content = new WordTransViewModel(currWord);
        }

        [RelayCommand]
        public void CloseFlashCard()
        {
            Content = new ListWordViewModel();
        }
    }
}