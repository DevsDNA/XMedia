using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XMedia.Model;
using XMedia.Services;

namespace XMedia
{
    [Preserve(AllMembers = true)]
    public partial class XMediaContent : ContentView
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

        private static readonly BindableProperty BarColorProperty =
            BindableProperty.Create(
                nameof(BarColor),
                typeof(Color),
                typeof(XMediaContent),
                Color.Blue);

        public Color BarColor
        {
            get => (Color)GetValue(BarColorProperty);
            set => SetValue(BarColorProperty, value);
        }

        public static readonly BindableProperty ImagesCompleteSelectionCommandProperty =
            BindableProperty.Create(
                nameof(ImagesCompleteSelectionCommand),
                typeof(ICommand),
                typeof(XMediaContent),
                default(ICommand));

        public ICommand ImagesCompleteSelectionCommand
        {
            get => (ICommand)GetValue(ImagesCompleteSelectionCommandProperty);
            set => SetValue(ImagesCompleteSelectionCommandProperty, value);
        }
                
        public string SelectedItems
        {
            get => $"{imagesSelected.Count} {SelectedItemText}";
        }

		public XMediaContent()
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

        public ICommand DoneCommand
        {
            get
            {
                return new Command(() =>
                {
                    ImagesCompleteSelectionCommand?.Execute(imagesSelected.Select(x => x.Media).ToList());
                });
            }
        }

        protected override async void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            var mediaFileSearchService = DependencyService.Get<IMediaFileSearchService>();

            var mediaFiles = (await mediaFileSearchService.GetMediaFiles()).Select(x => new MediaFileSelector(x));
           
            MediaFiles = new ObservableCollection<Grouping<DateTime, MediaFileSelector>>(mediaFiles
                                                    .OrderByDescending(x => x.Media.DateAdded)
                                                    .GroupBy(x => x.Media.DateAdded)
                                                    .Select(x => new Grouping<DateTime, MediaFileSelector>(x.Key, x)));

            OnPropertyChanged(nameof(MediaFiles));
        }        
    }
}
