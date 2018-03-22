using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XMedia.Model;

namespace XMedia.Sample
{
    public class MediaContentViewModel 
    {
        public ICommand CompleteCommand
        {
            get
            {
                return new Command<List<MediaFile>>((mediaFiles) =>
                {
                    
                });                
            }
        }
    }
}
