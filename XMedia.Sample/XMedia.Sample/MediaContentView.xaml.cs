 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XMedia.Sample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MediaContentView : ContentPage
	{
		public MediaContentView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            var bindingContext = new MediaContentViewModel();
            BindingContext = bindingContext;

            MediaContent.ImagesCompleteSelectionCommand = bindingContext.CompleteCommand;
		}
	}
}
