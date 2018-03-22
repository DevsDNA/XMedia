using Xamarin.Forms;

namespace XMedia.Model
{
    public class MediaFileSelector : BindableObject
    {
        public MediaFile Media { get; private set; }

        public MediaFileSelector(MediaFile mediaFile)
        {
            Media = mediaFile;
        }

        private bool selected;

        public bool Selected
        {
            get => selected;
            set
            {
                selected = value;
                
                OnPropertyChanged();
            }
        }
    }
}
