using SingularityForensic.Contracts.Previewers;
using System.Windows;

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
        private Views.OutsideInPreviewer view;
        public object UIObject => view ?? (view = new Views.OutsideInPreviewer());

        private string fileName;
        public string FileName {
            get {
                return fileName;
            }
            set {
                fileName = value;
                //(View as Views.OutsideInPreviewer)?.CloseFile();
                (UIObject as Views.OutsideInPreviewer)?.OpenFile(fileName);
            }
        }
        
        public void Dispose() {
            (UIObject as Views.OutsideInPreviewer).CloseFile();
        }
    }
}
    
