﻿using CoreGraphics;
using Foundation;
using Photos;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IEnumerable<XMediaFile> GetMediaFiles()
        {
            var images = new List<XMediaFile>();
            PHImageManager imageManager = PHImageManager.DefaultManager;

            var requestOptions = new PHImageRequestOptions()
            {
                Synchronous = true   
            };

            var results = PrepareResults();
            
            for(int i = 0; i < results.Count; i ++)
            {
                var asset = results.ObjectAt(i) as PHAsset;
                
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
                         images.Add(new XMediaFile()
                         {
                             Data = new MemoryStream(rawBytes),                             
                             DateAdded = asset.CreationDate.ToDateTime().ToShortDate(),
                             MediaType = asset.MediaType.ToString()

                         });
                     }
                     
                     
                 });                
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