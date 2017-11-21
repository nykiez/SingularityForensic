namespace Singularity.UI.Controls.Models.Filtering {
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
