using Singularity.Contracts.Info;
using System.Collections.ObjectModel;

namespace Singularity.UI.Info.ViewModels {
    /// <summary>
    /// 对话视图模型;
    /// </summary>
    public class TalkViewModel<TTalkLog> where TTalkLog:ITalkLog {
        public ObservableCollection<TTalkLog> TalkLogs { get; set; } = new ObservableCollection<TTalkLog>();
        
    }
}
