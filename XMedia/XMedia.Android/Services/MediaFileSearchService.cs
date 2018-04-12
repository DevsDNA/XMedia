[assembly: Xamarin.Forms.Dependency(typeof(XMedia.Droid.Services.MediaFileSearchService))]
namespace XMedia.Droid.Services
{
    using Android.Graphics;
    using Android.Media;
    using Android.Provider;   
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Internals;
    using XMedia.Model;
    using XMedia.Services;
    using XMedia.Utils;

    [Preserve(AllMembers = true)]
    public class MediaFileSearchService : IMediaFileSearchService
    {
        public MediaFileSearchService()
        {
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init(true);
        }

        public async Task<IEnumerable<XMediaFile>> GetMediaFiles()
        {
            var mediaFiles = new List<XMediaFile>();

            string[] projection =
            {
                MediaStore.Files.FileColumns.Id,
                MediaStore.Files.FileColumns.Data,
                MediaStore.Files.FileColumns.DateAdded,
                MediaStore.Files.FileColumns.MediaType,
                MediaStore.Files.FileColumns.MimeType,
                MediaStore.Files.FileColumns.Title,                
            };

            
            string selection = $"{MediaStore.Files.FileColumns.MediaType} = {Android.Provider.MediaType.Image} OR {MediaStore.Files.FileColumns.MediaType} = {Android.Provider.MediaType.Video}";

            var queryUri = MediaStore.Files.GetContentUri("external");
            
            var cursor = Forms.Context.ContentResolver.Query(queryUri, projection, string.Empty, null, $"{MediaStore.Files.FileColumns.DateAdded} DESC");

            var columnNames = cursor.GetColumnNames();

            while (cursor.MoveToNext())
            {
                var id = cursor.GetLong(0);
                var fileName = cursor.GetString(1);
                var data = GetFile(fileName);
                var thumbData = GetThumbFile(fileName);                                

                if(data != null)
                {
                    mediaFiles.Add(new XMediaFile()
                    {
                        Id = id.ToString(),
                        Data = data,
                        ThumbData = thumbData,
                        //Check out https://developer.android.com/reference/android/provider/MediaStore.MediaColumns.html#DATE_ADDED
                        DateAdded = new DateTime(1970,1,1).AddSeconds(cursor.GetLong(2)).ToShortDate(),
                        MediaType = cursor.GetString(3),
                        MimeType = cursor.GetString(4),
                        FileName = cursor.GetString(5)
                    });
                }                
            }

            return mediaFiles.Where(x => !string.IsNullOrWhiteSpace(x.MimeType)).Where(x => x.MimeType.Contains("image/jpeg") || x.MimeType.Contains("image/png"));
        }
                
        private Func<byte[]> GetFile(string path)
        {
            return new Func<byte[]>(() =>
            {
                try
                {
                    var bytes = File.ReadAllBytes(path);
                    if (bytes.Length > 0)
                    {
                        return bytes;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            });                        
        }

        private Func<byte[]> GetThumbFile(string path)
        {
            return new Func<byte[]>(() =>
            {                
                var bitmap = ThumbnailUtils.ExtractThumbnail(BitmapFactory.DecodeFile(path), 100,100);
                using (var stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    var rawImage = stream.ToArray();
                    bitmap.Recycle();
                    return rawImage;
                }                    
            });            
        }        
    }
}