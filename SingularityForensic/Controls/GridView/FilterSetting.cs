using System.Collections.Generic;
using Telerik.Windows.Data;

namespace SingularityForensic.Controls.GridView {
    public class FilterDescriptorProxy {
        public FilterOperator Operator { get; set; }
        public object Value { get; set; }
        public bool IsCaseSensitive { get; set; }
    }

    //过滤设定单项;
    public class FilterSetting {
        public string ColumnUniqueName { get; set; }
        public List<object> SelectedDistinctValues { get; } = new List<object>();
        public FilterDescriptorProxy Filter1 { get; set; }
        public FilterCompositionLogicalOperator FieldFilterLogicalOperator { get; set; }
        public FilterDescriptorProxy Filter2 { get; set; }
    }
}
