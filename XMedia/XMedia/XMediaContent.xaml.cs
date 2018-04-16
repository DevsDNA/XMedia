namespace XMedia
{
    
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;
    using XMedia.Model;
    using XMedia.Services;

    [Preserve(AllMembers = true)]
    public partial class XMediaContent : ContentView
	{
        private ObservableCollection<Grouping<DateTime, MediaFileSelector>> mediaFiles;
        private List<MediaFileSelector> imagesSelected;        

        public static readonly BindableProperty SelectedColorProperty =
                                                BindableProperty.Create(
                                                nameof(SelectedColor),
                                                typeof(Color),
                                                typeof(XMediaContent),
                                                Color.Transparent);

        public static readonly BindableProperty FilesSelectedProperty =
                                                BindableProperty.Create(
                                                nameof(FilesSelected),
                                                typeof(ObservableCollection<XMediaFile>),
                                                typeof(XMediaContent),
                                                new ObservableCollection<XMediaFile>(), BindingMode.TwoWay);

        public XMediaContent()
        {
            InitializeComponent();

            imagesSelected = new List<MediaFileSelector>();
            SelectedColor = Color.Blue;
            BindingContext = this;            
        }

        public ObservableCollection<Grouping<DateTime, MediaFileSelector>> MediaFiles
        {
            get => mediaFiles;
            set => mediaFiles = value;
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public ObservableCollection<XMediaFile> FilesSelected
        {
            get => (ObservableCollection<XMediaFile>)GetValue(FilesSelectedProperty);
            set => SetValue(FilesSelectedProperty, value);
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
                        FilesSelected.Add(mediaFileSelector.Media);
                    }
                    else
                    {
                        imagesSelected.Remove(mediaFileSelector);
                        FilesSelected.Remove(mediaFileSelector.Media);

                    }
                });
            }
        }

        public bool MediaLoading
        {
            get => !(MediaFiles?.Count > 0);
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
            OnPropertyChanged(nameof(MediaLoading));
        }                                        
    }
}
