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

        private List<MediaFileSelector> imagesSelected;

        private string selectedItemText;

        public string SelectedItemText
        {
            get
            {
                if(string.IsNullOrEmpty(selectedItemText))
                {
                    SelectedItemText = "Selected item(s)";
                }

                return selectedItemText;
            }
            set => selectedItemText = value;
        }

        public Color BarColor { get; set; }
        
        
        public string SelectedItems
        {
            get => $"{imagesSelected.Count} {SelectedItemText}";
        }

		public MainPage()
		{
			InitializeComponent();
            imagesSelected = new List<MediaFileSelector>();
            BarColor = Color.Blue;
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

                    if(mediaFileSelector.Selected)
                    {
                        imagesSelected.Add(mediaFileSelector);
                    }
                    else
                    {
                        imagesSelected.Remove(mediaFileSelector);
                    }

                    OnPropertyChanged(nameof(SelectedItems));
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
