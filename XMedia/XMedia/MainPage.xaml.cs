using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using XMedia.Model;
using XMedia.Services;

namespace XMedia
{
    public partial class MainPage : ContentPage
	{
        private ObservableCollection<Grouping<DateTime, MediaFile>> mediaFiles;

        public ObservableCollection<Grouping<DateTime, MediaFile>> MediaFiles
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

            var mediaFiles = DependencyService.Get<IMediaFileSearchService>().GetMediaFiles();
            
            MediaFiles = new ObservableCollection<Grouping<DateTime, MediaFile>>(mediaFiles.GroupBy(x => x.DateAdded)
                                                    .Select(x => new Grouping<DateTime, MediaFile>(x.Key, x)));

            /*
            MediaFiles = new ObservableCollection<Grouping<DateTime, MediaFile>>(mediaFiles.GroupBy(x => x.DateAdded)
                                   .Select(x => new Grouping<DateTime, MediaFile>(x)));
                                   //.Where(x => x.Count > 0));
                                   */
                                   
                                                                      
            OnPropertyChanged(nameof(MediaFiles));
        }
    }
}
