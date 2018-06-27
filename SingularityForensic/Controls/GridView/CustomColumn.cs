using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SingularityForensic.Controls.GridView {
    public class CustomColumn {
        public DataTemplate CellTemplate { get; set; }
        public bool ShowDistinctFilters { get; set; }
        public object Header { get; set; }
        
    }

    /// <summary>
    /// 自定义列(数据列);
    /// </summary>
    public class CustomDataColumn:CustomColumn {
        public Binding Binding { get; set; }
        public Type ColumnDataType { get; set; }
    }
}
