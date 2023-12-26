using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace TRANSMARIZE.ViewModels
{
	public partial class RevealWordViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string learnWord;
		public RevealWordViewModel() { }
		public RevealWordViewModel(string word)
		{
			LearnWord = word;
		}
	}
}