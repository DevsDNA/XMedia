using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XMedia.Model
{
    public class MediaFile
    {
        public string Id { get; set; }

        //public byte[] Data { get; set; }

        public ImageSource Data { get; set; }

        public string DateAdded { get; set; }

        public string MediaType { get; set; }

        public string MimeType { get; set; }

        public string FileName { get; set; }
    }
}
