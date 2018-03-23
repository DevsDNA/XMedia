using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Photos;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XMedia.Model;
using XMedia.Services;
using XMedia.Utils;

[assembly: Xamarin.Forms.Dependency(typeof(XMedia.iOS.Services.MediaFileSearchService))]
namespace XMedia.iOS.Services
{
    public class MediaFileSearchService : IMediaFileSearchService
    {
        public IEnumerable<MediaFile> GetMediaFiles()
        {
            var images = new List<MediaFile>();
            PHImageManager imageManager = PHImageManager.DefaultManager;

            var requestOptions = new PHImageRequestOptions()
            {
                Synchronous = true   
            };

            var results = PrepareResults();
            
            for(int i = 0; i < results.Count; i ++)
            {
                var asset = results.ObjectAt(i) as PHAsset;

                //There this second option
                //imageManager.RequestImageData

                imageManager.RequestImageForAsset(asset, new CGSize(100, 100), PHImageContentMode.AspectFill, requestOptions, (image, info) =>
                 {
                     byte[] rawBytes = null;
                     using (NSData imageData = image.AsJPEG())
                     {
                         rawBytes = new Byte[imageData.Length];
                         System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, rawBytes, 0, (int)imageData.Length);
                     }

                     System.Diagnostics.Debug.WriteLine(info);

                     if (rawBytes != null)
                     {
                         images.Add(new MediaFile()
                         {
                             Data = ImageSource.FromStream(() => new MemoryStream(rawBytes)),
                             //FileName = (info[new NSString("PHImageFileURLKey")] as NSUrl).LastPathComponent,
                             DateAdded = asset.CreationDate.ToDateTime().ToShortDate(),
                             MediaType = asset.MediaType.ToString()

                         });
                     }
                     
                     
                 });

                
                
                /*
                imageManager.RequestImageData(asset, requestOptions, (data, dataUti, orientation, info) =>
                {
                    byte[] rawBytes = new byte[data.Length];

                    System.Runtime.InteropServices.Marshal.Copy(data.Bytes, rawBytes, 0, (int)data.Length);

                    System.Diagnostics.Debug.WriteLine(info);
                    System.Diagnostics.Debug.WriteLine(dataUti);

                    images.Add(new MediaFile()
                    {
                        Data = ImageSource.FromStream(() => new MemoryStream(rawBytes)),
                        FileName = (info[new NSString("PHImageFileURLKey")] as NSUrl).LastPathComponent,
                        DateAdded = asset.CreationDate.ToDateTime(),
                        MediaType = asset.MediaType.ToString()
                    });
                });                
                */
            }

            return images;
        }

        private PHFetchResult PrepareResults()
        {            
            var fetchOptions = new PHFetchOptions()
            {
                SortDescriptors = new NSSortDescriptor[] { new NSSortDescriptor("creationDate", true) }
            };

            return PHAsset.FetchAssets(PHAssetMediaType.Image, fetchOptions);
        }
    }
}