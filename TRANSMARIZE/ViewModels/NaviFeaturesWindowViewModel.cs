using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using TRANSMARIZE.ViewModels;

namespace TRANSMARIZE.ViewModels
{
	public partial class NaviFeaturesWindowViewModel : ViewModelBase
	{
		[ObservableProperty]
		public ViewModelBase content;
		public NaviFeaturesWindowViewModel() { }

    }
}