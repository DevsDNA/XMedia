
using System;
using System.Collections.Generic;
using System.Text;
using XMedia.Model;

namespace XMedia.Services
{
    public interface IMediaFileSearchService
    {
        IEnumerable<MediaFile> GetMediaFiles();
    }
}
