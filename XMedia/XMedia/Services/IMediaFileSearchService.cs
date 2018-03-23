using System.Collections.Generic;
using XMedia.Model;

namespace XMedia.Services
{
    public interface IMediaFileSearchService
    {
        IEnumerable<XMediaFile> GetMediaFiles();
    }
}
