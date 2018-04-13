namespace XMedia.iOS
{
    using Xamarin.Forms;
    using XMedia.iOS.Services;

    public class XMediaContentRenderer
    {
        public static void Init()
        {
            DependencyService.Register<MediaFileSearchService>();
            XCheckBox.iOS.CheckBoxRenderer.Init();
        }
    }
}