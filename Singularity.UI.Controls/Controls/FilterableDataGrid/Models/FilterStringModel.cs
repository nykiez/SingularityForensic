using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.Controls.Controls.FilterableDataGrid.Models {
    public interface IFilterModel {
        bool IsEnabled { get; }
    }

    public class FilterStringModel:IFilterModel {
        public string[] Keys { get; set; }
        public bool MatchCase { get; set; }

        public StringMatchWay MatchWay { get; set; }

        public bool IsEnabled { get; set; }
    }

    public enum StringMatchWay {
        FullMatch,
        AnyKey
    }
}
