using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace XMedia.Model
{
    [Preserve(AllMembers = true)]
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
