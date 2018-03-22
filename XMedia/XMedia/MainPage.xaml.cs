using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using XMedia.Model;
using XMedia.Services;

namespace XMedia
{
    public partial class MainPage : ContentPage
	{
        private ObservableCollection<Grouping<DateTime, MediaFileSelector>> mediaFiles;        

        public ObservableCollection<Grouping<DateTime, MediaFileSelector>> MediaFiles
        {
            get => mediaFiles;
            set => mediaFiles = value;
        }

		public MainPage()
		{
			InitializeComponent();
            BindingContext = this;            
		}
        
        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command<MediaFileSelector>((obj) =>
                {
                    var mediaFileSelector = obj as MediaFileSelector;

                    mediaFileSelector.Selected = !mediaFileSelector.Selected;
                });
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var mediaFiles = DependencyService.Get<IMediaFileSearchService>().GetMediaFiles().Select(x => new MediaFileSelector(x));
            
            MediaFiles = new ObservableCollection<Grouping<DateTime, MediaFileSelector>>(mediaFiles.GroupBy(x => x.Media.DateAdded)
                                                    .Select(x => new Grouping<DateTime, MediaFileSelector>(x.Key, x)));
                                                                                                                     
            OnPropertyChanged(nameof(MediaFiles));            
        }                
    }
}
