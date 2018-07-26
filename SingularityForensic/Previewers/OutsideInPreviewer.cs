using SingularityForensic.Contracts.Previewers;

namespace SingularityForensic.Controls.Previewers {
    public class OutsideInPreviewer : IPreviewer {
        /// <summary>
        /// OutSideIn-Tech预览器;
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="targetFileName"></param>
        public OutsideInPreviewer(string targetFileName) {
            FileName = targetFileName;
        }
        private Views.OutsideInPreviewer _outSideInPreviewer;
        private Views.OutsideInPreviewer OutSideInPreviewer => _outSideInPreviewer ?? (_outSideInPreviewer = new Views.OutsideInPreviewer());
        public object UIObject => OutSideInPreviewer;

        private string fileName;
        public string FileName {
            get {
                return fileName;
            }
            set {
                fileName = value;
                //(View as Views.OutsideInPreviewer)?.CloseFile();
                OutSideInPreviewer?.OpenFile(fileName);
            }
        }
        
        public void Dispose() {
            (UIObject as Views.OutsideInPreviewer).CloseFile();
        }
    }
}
    
