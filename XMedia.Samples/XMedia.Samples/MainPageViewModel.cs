using System.Windows.Input;
using Xamarin.Forms;

namespace XMedia.Samples
{
    public class MainPageViewModel
    {
        public ICommand CommandOpen
        {
            get
            {
                return new Command(() =>
                {
                    App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new XMediaPage()));
                });
            }
        }
    }
}
