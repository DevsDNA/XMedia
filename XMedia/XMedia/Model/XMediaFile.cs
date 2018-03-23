using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace XMedia.Model
{
    [Preserve(AllMembers = true)]
    public class XMediaFile
    {
        public string Id { get; set; }
        
        public ImageSource Source
        {
            get => ImageSource.FromStream(() => Data);
        }

        public DateTime DateAdded { get; set; }

        public string MediaType { get; set; }

        public string MimeType { get; set; }

        public string FileName { get; set; }

        public MemoryStream Data { get; set; }

        
    }
}
