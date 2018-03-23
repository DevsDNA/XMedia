using Xamarin.Forms;

namespace XMedia.Model
{
    public class MediaFileSelector : BindableObject
    {
        public XMediaFile Media { get; private set; }

        public MediaFileSelector(XMediaFile mediaFile)
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
