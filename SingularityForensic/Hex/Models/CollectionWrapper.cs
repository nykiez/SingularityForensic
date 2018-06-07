using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hex.Models {
    class CollectionWrapper<T1, T2> : ICollection<T1> {
        public CollectionWrapper(Func<T1,T2> getT2,ICollection<T2> t2Collection) {
            this._typeConverter = getT2;
            this._t2Collection = t2Collection;
        }
        private Func<T1, T2> _typeConverter;
        private ICollection<T2> _t2Collection;
        private Dictionary<T1,T2> _list = new Dictionary<T1,T2>();
        public int Count => _list.Count;

        public bool IsReadOnly => _t2Collection.IsReadOnly;

        public void Add(T1 item) {
            var t2Item = _typeConverter(item);
            _list.Add(item,t2Item);
            _t2Collection.Add(t2Item);
        }

        public void Clear() {
            _list.Clear();
            _t2Collection.Clear();
        }

        public bool Contains(T1 item) => _list.ContainsKey(item);

        public void CopyTo(T1[] array, int arrayIndex) {
            throw new NotImplementedException();
            //_list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T1> GetEnumerator() {
            foreach (var item in _list) {
                yield return item.Key;
            }
        }

        public bool Remove(T1 item) {
            _list.Remove(item);
            return _t2Collection.Remove(_list[item]);
        }

        IEnumerator IEnumerable.GetEnumerator() =>_list.GetEnumerator();
    }
}
