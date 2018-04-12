namespace XMedia.Samples
{
    using ReactiveUI;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class XMediaPage
	{
		public XMediaPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel= new XMediaPageViewModel();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.WhenActivated(d => 
            {
                d(this.Bind(ViewModel, vm => vm.SelectedItems, v => v.XMediaControl.FilesSelected));
            });
        }
    }
}