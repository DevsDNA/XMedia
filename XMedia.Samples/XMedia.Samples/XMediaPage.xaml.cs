using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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