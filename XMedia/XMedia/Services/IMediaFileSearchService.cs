using System.Collections.Generic;
using System.Threading.Tasks;
using XMedia.Model;

namespace XMedia.Services
{
    public interface IMediaFileSearchService
    {
        Task<IEnumerable<XMediaFile>> GetMediaFiles(int limitImages = 0);
    }
}
