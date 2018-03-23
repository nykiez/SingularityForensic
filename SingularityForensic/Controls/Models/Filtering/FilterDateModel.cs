using System;

namespace SingularityForensic.Controls.Models.Filtering {
    /// <summary>
    /// 过滤时间模型;
    /// </summary>
    public class FilterDateModel : IFilterModel {
        public DateTime? MinTime { get; set; }

        public DateTime? MaxTime { get; set; }

        
        //两个条件的并行关系;
        public TwoConditionRule? Condition { get; set; }

        public bool IsEnabled { get; set; }
    }
}
