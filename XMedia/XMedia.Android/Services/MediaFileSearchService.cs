using System.Collections.Generic;
using Android.Provider;
using Xamarin.Forms;
using XMedia.Model;
using XMedia.Services;
using System.Linq;
using System.IO;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(XMedia.Droid.Services.MediaFileSearchService))]
namespace XMedia.Droid.Services
{
    public class MediaFileSearchService : IMediaFileSearchService
    {
        public IEnumerable<MediaFile> GetMediaFiles()
        {
            var mediaFiles = new List<MediaFile>();

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
                    mediaFiles.Add(new MediaFile()
                    {
                        Id = cursor.GetString(0),
                        Data = data,
                        DateAdded = cursor.GetString(2),
                        MediaType = cursor.GetString(3),
                        MimeType = cursor.GetString(4),
                        FileName = cursor.GetString(5)
                    });
                }                
            }

            return mediaFiles.Where(x => !string.IsNullOrWhiteSpace(x.MimeType)).Where(x => x.MimeType.Contains("image/jpeg") || x.MimeType.Contains("image/png"));
        }

        private ImageSource GetFile(string path)
        {
            try
            {
                return ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(path)));                
            }
            catch
            {
                return null;
            }
            
        }

    }
}