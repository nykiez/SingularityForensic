using SingularityForensic.Contracts.Info;
using SingularityForensic.Contracts.TabControl;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SingularityForensic.Controls.Info.ViewModels;
using SingularityForensic.Controls.Views;

namespace SingularityForensic.Android.Info.TabModels {
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
