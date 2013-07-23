using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace pdok4arcgis {
    /// <summary>
    /// Abstract class for collections.
    /// </summary>
    /// <remarks>
    /// The collection support both sequential and random access objects based on key.
    /// </remarks>
    public abstract class CswObjects :IDictionary, IEnumerable{

        private ArrayList list = new ArrayList();
        private Hashtable map = new Hashtable();

#region ICollection implementation
        
        public int Count {
            get { return map.Count; }
        }

        public bool IsSynchronized {
            get { return map.IsSynchronized; }
        }

        public object SyncRoot {
            get { return map.SyncRoot; }
        }

        public void CopyTo(System.Array array, int index) {
            map.CopyTo(array, index);
        }
#endregion

#region IDictionary implementation
        

        public void Add(object key, object value) {
            if (!map.ContainsKey(key))//ignores duplicate keys
            {
                list.Add(key);
                map.Add(key, value);
            }
        }

        public bool IsFixedSize {
            get { return map.IsFixedSize; }
        }

        public bool IsReadOnly {
            get { return map.IsReadOnly; }
        }

        public ICollection Keys {
            get { return map.Keys; }
        }

        public void Clear() {
            list.Clear();
            map.Clear();
        }

        public bool Contains(object key) {
            return map.Contains(key);
        }

        public bool ContainsKey(object key) {
            return map.ContainsKey(key);
        }

        public IDictionaryEnumerator GetEnumerator() {
            return map.GetEnumerator();
        }

        public void Remove(object key) {
            map.Remove(key);
            list.Remove(key);
        }

        public object this[object key] {
            get { return map[key]; }
            set { map[key] = value; }
        }

        public ICollection Values {
            get { return map.Values; }
        }
#endregion

#region IEnumerable implementation

         IEnumerator IEnumerable.GetEnumerator() {
            return map.GetEnumerator();
        }

#endregion

#region specialized indexer routines

        public object this[string key] {
            get { return map[key]; }
        }

        public object this[int index] {
            get { return map[list[index]]; }
        }
#endregion


    }
}
