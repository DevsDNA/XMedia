using System.Collections.Generic;
using Android.Provider;
using Xamarin.Forms;
using XMedia.Model;
using XMedia.Services;

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

            string selection = $"{MediaStore.Files.FileColumns.MediaType} = {MediaStore.Files.FileColumns.MediaTypeImage} OR {MediaStore.Files.FileColumns.MediaType} = {MediaStore.Files.FileColumns.MediaTypeVideo}";

            var queryUri = MediaStore.Files.GetContentUri("external");
            
            var cursor = Forms.Context.ContentResolver.Query(queryUri, projection, selection, null, $"{MediaStore.Files.FileColumns.DateAdded} DESC");

            var columnNames = cursor.GetColumnNames();

            while (cursor.MoveToNext())
            {
                mediaFiles.Add(new MediaFile() { FileName = cursor.GetString(5) });
            }

            return mediaFiles;
        }
    }
}