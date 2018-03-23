using System;
using System.Collections.Generic;
using System.Text;
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
                return new Command<List<MediaFile>>((mediaFiles) =>
                {
                    //Do things with MediaFiles.
                });
            }
        }
    }
}
