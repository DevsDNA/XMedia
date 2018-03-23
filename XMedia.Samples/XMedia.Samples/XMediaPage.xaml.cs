
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XMedia.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class XMediaPage : ContentPage
	{
		public XMediaPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new XMediaPageViewModel();
		}
	}
}