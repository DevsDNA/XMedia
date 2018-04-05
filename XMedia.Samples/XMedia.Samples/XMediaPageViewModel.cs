namespace XMedia.Samples
{
    using ReactiveUI;
    using System.Collections.ObjectModel;
    using XMedia.Model;

    public class XMediaPageViewModel : ReactiveObject
    {
        private ObservableCollection<XMediaFile> selectedItems = new ObservableCollection<XMediaFile>();
        
        public XMediaPageViewModel()
        {

        }

        public ObservableCollection<XMediaFile> SelectedItems
        {
            get => selectedItems;
            set => this.RaiseAndSetIfChanged(ref selectedItems, value);
        }

    }
}
