using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace TRANSMARIZE.ViewModels
{
	public partial class ReviseWordViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string learnWord = "abstract";
		public ReviseWordViewModel() { }
		public ReviseWordViewModel(string word)
		{
			LearnWord = word;
		}
	}
}