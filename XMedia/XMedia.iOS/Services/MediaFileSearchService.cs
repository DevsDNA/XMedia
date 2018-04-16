[assembly: Xamarin.Forms.Dependency(typeof(XMedia.iOS.Services.MediaFileSearchService))]
namespace XMedia.iOS.Services
{
    using CoreGraphics;
    using Foundation;
    using Photos;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms.Platform.iOS;
    using XMedia.Model;
    using XMedia.Services;
    using XMedia.Utils;

    [Preserve(AllMembers = true)]
    public class MediaFileSearchService : IMediaFileSearchService
    {
        public MediaFileSearchService()
        {
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
        }

        public async Task<IEnumerable<XMediaFile>> GetMediaFiles()
        {
            var images = new List<XMediaFile>();
            var permission = await PHPhotoLibrary.RequestAuthorizationAsync();

            if(permission == PHAuthorizationStatus.Authorized)
            {
                
                PHImageManager imageManager = PHImageManager.DefaultManager;

                var requestOptions = new PHImageRequestOptions()
                {
                    Synchronous = true
                };

                var results = PrepareResults();

                if (results == null || results.Count == 0)
                {
                    return images;
                }

                for (int i = 0; i < results.Count; i++)
                {
                    var asset = results.ObjectAt(i) as PHAsset;

                    imageManager.RequestImageForAsset(asset, new CGSize(100, 100), PHImageContentMode.AspectFill, requestOptions, (image, info) =>
                    {

                        var funcBytes = new Func<byte[]>(() =>
                        {
                            byte[] rawBytes = null;
                            using (NSData imageData = image.AsJPEG())
                            {
                                rawBytes = new Byte[imageData.Length];
                                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, rawBytes, 0, (int)imageData.Length);                                
                            }

                            return rawBytes;                            
                        });


                        images.Add(new XMediaFile()
                        {
                            Data = funcBytes,
                            ThumbData = funcBytes,
                            DateAdded = asset.CreationDate.ToDateTime().ToShortDate(),
                            MediaType = asset.MediaType.ToString()

                        });



                    });
                }

                return images;
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