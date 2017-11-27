using CDFCUIContracts.Abstracts;
using Prism.Mvvm;
using Singularity.UI.AdbViewer.Contracts;
using Singularity.UI.AdbViewer.Resources;
using Singularity.UI.Info.Contracts;
using Singularity.UI.Info.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.AdbViewer.ViewModels {
    //主查看器所需的TabModel拓展;
    public abstract class InfoMainTabModel : BindableBase ,ITabModel {
        public string Header { get; set; }
        //默认图标所在位置;
        public Uri Icon { get; set; }
        //高亮图标所在位置;
        public Uri ActiveIcon { get; set; }
    }

    //列表视图;
    [Export]
    public class InfoListViewTabModel : InfoMainTabModel {
        public InfoListViewTabModel() {
            Icon = IconSources.BtnListViewIcon;
            ActiveIcon = IconSources.BtnListViewActiveIcon;
            Header = FindResourceString("ListView");
        }

        //信息类型;
        private MInfoType _infoType;
        public MInfoType InfoType {
            get => _infoType;
            set => SetProperty(ref _infoType, value);
        }
        
        //展示项;
        public ObservableCollection<InfoModel> InfoModels { get; set; } = new ObservableCollection<InfoModel>();

        //选中项;
        private InfoModel _selectedInfoModel;
        public InfoModel SelectedInfoModel {
            get => _selectedInfoModel;
            set => SetProperty(ref _selectedInfoModel, value);
        }

        private Type _rowType;
        public Type RowType {
            get => _rowType;
            set => SetProperty(ref _rowType, value);
        }
    }

    //对话视图;
    [Export]
    public class TalkTabViewModel : InfoMainTabModel {
        public TalkTabViewModel() {
            Icon = IconSources.BtnTalkViewIcon;
            ActiveIcon = IconSources.BtnTalkViewActiveIcon;
            Header = FindResourceString("TalkingView");
        }

        public TalkViewModel<ITalkLog> TalkViewModel { get; } = new TalkViewModel<ITalkLog>();

        //public ObservableCollection<ITalkLog> TalkLogs { get; set; } = new ObservableCollection<ITalkLog>();
    }
}
