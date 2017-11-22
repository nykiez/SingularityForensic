using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.MessageBoxes.Models.Filtering {
    public class FilterStringModel:IFilterModel {
        public bool IsEnabled { get; set; }

        public string[] Keys { get; set; }
        public bool MatchCase { get; set; }

        public StringMatchWay MatchWay { get; set; }
        
    }

    public enum StringMatchWay {
        FullMatch,
        AnyKey
    }
}
