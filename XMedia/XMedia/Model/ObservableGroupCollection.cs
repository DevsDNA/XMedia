using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XMedia.Model
{
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
