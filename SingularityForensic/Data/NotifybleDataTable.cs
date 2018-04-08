using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Data {
    public class NotifybleDataTable : DataTable, INotifyCollectionChanged {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        

        protected override void OnTableClearing(DataTableClearEventArgs e) {
            base.OnTableClearing(e);

            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                ((IListSource)this).GetList()));
        }

    }
}
