using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XMedia.Model;
using XMedia.Services;

namespace XMedia
{
    public partial class MainPage : ContentPage
	{
        private List<MediaFile> mediaFiles;

        public List<MediaFile> MediaFiles
        {
            get => mediaFiles;
            set => mediaFiles = value;
        }

		public MainPage()
		{
			InitializeComponent();
            BindingContext = this;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MediaFiles = DependencyService.Get<IMediaFileSearchService>().GetMediaFiles().ToList();
            OnPropertyChanged(nameof(MediaFiles));
        }
    }
}
