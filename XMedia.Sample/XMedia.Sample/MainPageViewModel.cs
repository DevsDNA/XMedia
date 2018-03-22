using System.Windows.Input;
using Xamarin.Forms;

namespace XMedia.Sample
{
    public class MainPageViewModel
    {
        public ICommand CommandOpen
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage.Navigation.PushAsync(new NavigationPage(new XMediaPage()));

                    await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MediaContentView()));
                    
                });
            }
        }
    }
}
