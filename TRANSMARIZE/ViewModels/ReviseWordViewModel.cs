using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using TRANSMARIZE.Model;
using PexelsDotNetSDK;
using PexelsDotNetSDK.Api;
using System.Linq;
using TRANSMARIZE.Services;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

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

		private string urlImage = string.Empty;
		[ObservableProperty]
		private Bitmap imageFromWebsite;
		[ObservableProperty]
		private bool isComplete = false;

        public ReviseWordViewModel() { }
		public ReviseWordViewModel(SavedWord learnWord)
		{
			LearnContent = learnWord.Content;
			LearnPhonetic = learnWord.Phonetic;
			Date = learnWord.Date.ToShortDateString();
			SearchImage();
		}

        public async void SearchImage()
        {
            var pexelsClient = new PexelsClient("o2f83GqfSjOliNYQKk2qn23C6QEcDiHIGaKmJkQWtogxMK3zwaMunxfk");
			Task<PexelsDotNetSDK.Models.PhotoPage> task = pexelsClient.SearchPhotosAsync(LearnContent);
            var result = await task;
			if (task.IsCompleted == true)
			{
				IsComplete = true;
			}
			List<PexelsDotNetSDK.Models.Photo> photos = result.photos.ToList();
			if (photos.Count == 0)
			{
				ImageFromWebsite = ImageHelper.LoadFromResource(new Uri("avares://TRANSMARIZE/Assets/just-kidding.png"));
                return;
			}
			foreach (var photo in photos)
			{
				if (photo.source.landscape != null)
				{
					urlImage = photo.source.landscape;
					break;
				}
			}
			ImageFromWebsite = await ImageHelper.LoadFromWeb(new Uri(urlImage));
        }
    }
}