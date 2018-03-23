namespace SingularityForensic.Controls.Models.Filtering {
    //大小匹配模型;
    public class FilterSizeModel : IFilterModel {
        public long? MaxSize { get; set; }
        public long? MinSize { get; set; }

        //两个条件的并行关系;
        public TwoConditionRule? Condition { get; set; }
        
        public string MaxUnitSize { get; set; }
        public string MinUnitSize { get; set; }

        public bool IsEnabled { get; set; }
    }

    //两个条件的并行关系;
    public enum TwoConditionRule {
        MinAndMax,
        MinOnly,
        MaxOnly,
        MinOrMax
    }
}
