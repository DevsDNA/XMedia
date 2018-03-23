using Android.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using XMedia.Model;
using XMedia.Services;
using XMedia.Utils;

[assembly: Xamarin.Forms.Dependency(typeof(XMedia.Droid.Services.MediaFileSearchService))]
namespace XMedia.Droid.Services
{
    public class MediaFileSearchService : IMediaFileSearchService
    {
        public IEnumerable<XMediaFile> GetMediaFiles()
        {
            var mediaFiles = new List<XMediaFile>();

            string[] projection =
            {
                MediaStore.Files.FileColumns.Id,
                MediaStore.Files.FileColumns.Data,
                MediaStore.Files.FileColumns.DateAdded,
                MediaStore.Files.FileColumns.MediaType,
                MediaStore.Files.FileColumns.MimeType,
                MediaStore.Files.FileColumns.Title
            };

            

            string selection = $"{MediaStore.Files.FileColumns.MediaType} = {Android.Provider.MediaType.Image} OR {MediaStore.Files.FileColumns.MediaType} = {Android.Provider.MediaType.Video}";

            var queryUri = MediaStore.Files.GetContentUri("external");

            //var cursor = Forms.Context.ContentResolver.Query(queryUri, projection, selection, null, $"{MediaStore.Files.FileColumns.DateAdded} DESC");
            var cursor = Forms.Context.ContentResolver.Query(queryUri, projection, string.Empty, null, $"{MediaStore.Files.FileColumns.DateAdded} DESC");

            var columnNames = cursor.GetColumnNames();

            while (cursor.MoveToNext())
            {
                var data = GetFile(cursor.GetString(1));

                if(data != null)
                {
                    mediaFiles.Add(new XMediaFile()
                    {
                        Id = cursor.GetString(0),
                        Data = data,
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

        
        

        private MemoryStream GetFile(string path)
        {
            try
            {
                var bytes = File.ReadAllBytes(path);
                if (bytes.Length > 0)
                {
                    return new MemoryStream(bytes);
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
            
        }

    }
}