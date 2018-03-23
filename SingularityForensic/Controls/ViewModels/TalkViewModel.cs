using SingularityForensic.Contracts.Info;
using System.Collections.ObjectModel;

namespace SingularityForensic.Controls.Info.ViewModels {
    /// <summary>
    /// 对话视图模型;
    /// </summary>
    public class TalkViewModel<TTalkLog> where TTalkLog:ITalkLog {
        public ObservableCollection<TTalkLog> TalkLogs { get; set; } = new ObservableCollection<TTalkLog>();
        
    }
}
