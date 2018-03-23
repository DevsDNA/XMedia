using System;
using System.IO;
using Xamarin.Forms;

namespace XMedia.Model
{
    public class MediaFile
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
