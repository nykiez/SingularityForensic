using Prism.Mvvm;
using Singularity.UI.AdbViewer.Contracts;
using Singularity.UI.AdbViewer.Helpers;
using Singularity.UI.Info.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace Singularity.UI.AdbViewer.ViewModels {
    //查看器主视图;
    [Export]
    public class InfoMainViewModel:BindableBase {
        public ObservableCollection<InfoMainTabModel> TabModels { get; set; } = new ObservableCollection<InfoMainTabModel>();

        //选定的Tab;
        private InfoMainTabModel _selectedTabModel;
        public InfoMainTabModel SelectedTabModel {
            get => _selectedTabModel;
            set => SetProperty(ref _selectedTabModel, value);
        }

        //列表视图;
        //[Import]
        private InfoListViewTabModel _infoListViewTabModel;
        public InfoListViewTabModel InfoListViewTabModel {
            get {
                if(_infoListViewTabModel == null) {
                    _infoListViewTabModel = new InfoListViewTabModel();
                    if (IsListViewRequired && !TabModels.Contains(_infoListViewTabModel)) {
                        TabModels.Add(_infoListViewTabModel);
                        SelectedTabModel = _infoListViewTabModel;
                    }
                }
                
                return _infoListViewTabModel;
            }
        }
        //是否需要显示列表视图;
        public bool IsListViewRequired { get; private set; } = true;

        ////对话视图;
        //[Import]
        //private TalkViewTabModel _talkViewTabModel {
        //    set {
        //        TalkViewTabModel = value;
        //        if (IsTalkViewRequired && !TabModels.Contains(TalkViewTabModel)) {
        //            TabModels.Add(TalkViewTabModel);
        //        }
        //    }
        //}

        private TalkTabViewModel _talkViewTabModel;
        public TalkTabViewModel TalkViewTabModel {
            get {
                if(_talkViewTabModel == null) {
                    _talkViewTabModel = new TalkTabViewModel();
                    if(IsTalkViewRequired && !TabModels.Contains(_talkViewTabModel)) {
                        TabModels.Add(_talkViewTabModel);
                    }
                }
                return _talkViewTabModel;
            }
        }

        //是否需要显示对话视图;
        public bool IsTalkViewRequired { get; private set; } = true;

        /// <summary>
        /// 加载信息集合;
        /// </summary>
        /// <param name="tuple"></param>
        public void LoadInfoes((IEnumerable<InfoModel> infoes,MInfoType infoType) tuple,bool isTalkViewVisible = false) {
            TalkViewTabModel.TalkViewModel.TalkLogs.Clear();

            InfoListViewTabModel.RowType = tuple.infoes.FirstOrDefault() != null ? tuple.infoes.ElementAt(0).GetType() : null;
            InfoListViewTabModel.InfoModels.Clear();
            InfoListViewTabModel.InfoType = tuple.infoType;

            if (isTalkViewVisible && MInfoTypeHelper.IsTalkLog(tuple.infoType)) {
                if (!TabModels.Contains(TalkViewTabModel)) {
                    TabModels.Add(TalkViewTabModel);
                }
                IsTalkViewRequired = true;
                foreach (var item in tuple.infoes) {
                    TalkViewTabModel.TalkViewModel.TalkLogs.Add(item as ITalkLog);
                }
            }
            else {
                TabModels.Remove(TalkViewTabModel);
                SelectedTabModel = InfoListViewTabModel;
                IsTalkViewRequired = false;
            }
            foreach (var info in tuple.infoes) {
                InfoListViewTabModel.InfoModels.Add(info);
            }
        }

        
    }
}
