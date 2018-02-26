using Prism.Mvvm;

namespace Singularity.UI.Info.ViewModels {
    public class InfoBasicViewModel : BindableBase {
        /// <summary>
        /// 前台显示的基本信息;
        /// </summary>
        private string _basicText;
        public string BasicText {
            get => _basicText;
            set => SetProperty(ref _basicText, value);
        }
    }
}
