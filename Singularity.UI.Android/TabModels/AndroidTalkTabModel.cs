using Singularity.Contracts.Info;
using Singularity.Contracts.TabControl;
using Singularity.UI.Controls.Views;
using Singularity.UI.Info.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Singularity.UI.Info.Android.TabModels {
    //对话视图Tab模型;
    public class AndroidTalkTabModel<TTalklog>:ExtTabModel<IEnumerable<TTalklog>> where TTalklog:ITalkLog {
        public AndroidTalkTabModel(string infoPin, IEnumerable<TTalklog> talkLogs):base(talkLogs,infoPin) {
            TalkViewModel = new TalkViewModel<TTalklog> {
                TalkLogs = new ObservableCollection<TTalklog>(talkLogs)
            };
            Content = new TalkView { DataContext = TalkViewModel };
        }
        public TalkViewModel<TTalklog> TalkViewModel { get; }
        
    }
}
