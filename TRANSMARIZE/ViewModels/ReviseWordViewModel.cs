using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.ViewModels
{
	public partial class ReviseWordViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string learnContent = "abstract";
		[ObservableProperty]
		private string learnPhonetic = "nan";
		[ObservableProperty]
		private string date = string.Empty;
		public ReviseWordViewModel() { }
		public ReviseWordViewModel(SavedWord learnWord)
		{
			LearnContent = learnWord.Content;
			LearnPhonetic = learnWord.Phonetic;
			Date = learnWord.Date.ToShortDateString();
		}
	}
}