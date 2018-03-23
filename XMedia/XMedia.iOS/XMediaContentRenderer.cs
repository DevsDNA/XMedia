using Xamarin.Forms;
using XMedia.iOS.Services;

namespace XMedia.iOS
{
    public class XMediaContentRenderer
    {
        public static void Init()
        {
            DependencyService.Register<MediaFileSearchService>();
        }
    }
}