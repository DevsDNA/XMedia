using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;

namespace XMedia.Model
{
    [Preserve(AllMembers = true)]
    public class Grouping<K, V> : ObservableCollection<V>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<V> items)
        {
            Key = key;
            foreach(var item in items)
            {
                Items.Add(item);
            }
        }        
        
    }
}
