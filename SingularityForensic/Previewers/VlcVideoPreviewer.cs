using SingularityForensic.Previewers.ViewModels;
using SingularityForensic.Previewers.Models;
using SingularityForensic.Contracts.Previewers;

namespace SingularityForensic.Previewers {
    /// <summary>
    /// Vlc视频播放器;
    /// </summary>
    public abstract class VideoPreviewer<TVideoPreviewerModel> : IPreviewer 
        where TVideoPreviewerModel : IVideoPreviewerModel<IPlayer> {
        public virtual object DataContext => null;
        
        public abstract object UIObject { get; }

        //~VideoPreviewer() {
        //    Dispose();
        //}
        public abstract TVideoPreviewerModel VideoPreviewerModel { get; }
        
        public virtual void Dispose() => VideoPreviewerModel.Dispose();
    }

    
    public class VlcVideoPreviewer : VideoPreviewer<VlcVideoPreviewerViewModel> {
        public VlcVideoPreviewer(string videoFileName) {
            this.FileName = videoFileName;
        }

        public string FileName { get; }

        private Views.VideoPreviewer _previewer;
        public override object UIObject {
            get {
                if(_previewer == null) {
                    _previewer = new Views.VideoPreviewer {
                        ScreenPanelContent = previewerPanel,
                        DataContext = VideoPreviewerModel
                    };
                }
                return _previewer;
            }
        }

        private Views.VlcVideoPreviewer previewerPanel = new Views.VlcVideoPreviewer();

        /// <summary>
        /// Vlc视图模型;
        /// </summary>
        private VlcVideoPreviewerViewModel _videoPreviewerModel;
        public override VlcVideoPreviewerViewModel VideoPreviewerModel {
            get {
                if(_videoPreviewerModel == null) {
                    var _player = new VlcPlayer(previewerPanel.Player) {
                        FileName = FileName
                    };
                    _videoPreviewerModel = new VlcVideoPreviewerViewModel(_player);
                }
                return _videoPreviewerModel;
            }
        }


    }
}
