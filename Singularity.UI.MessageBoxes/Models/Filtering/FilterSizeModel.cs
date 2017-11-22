using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singularity.UI.MessageBoxes.Models.Filtering {
    //大小匹配模型;
    public class FilterSizeModel {
        public long? MaxSize { get; set; }
        public long? MinSize { get; set; }

        //两个条件的并行关系;
        public TwoConditionRule? Condition { get; set; }
        
        public string MaxUnitSize { get; set; }
        public string MinUnitSize { get; set; }
    }

    //两个条件的并行关系;
    public enum TwoConditionRule {
        MinAndMax,
        MinOnly,
        MaxOnly,
        MinOrMax
    }
}
