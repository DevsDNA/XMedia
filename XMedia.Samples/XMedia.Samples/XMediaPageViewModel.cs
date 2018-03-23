using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XMedia.Model;

namespace XMedia.Samples
{
    public class XMediaPageViewModel
    {
        public ICommand CompleteImagesCommand
        {
            get
            {
                return new Command<List<XMediaFile>>((mediaFiles) =>
                {
                    //Do things with MediaFiles.
                });
            }
        }
    }
}
