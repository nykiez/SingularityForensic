using Singularity.Contracts.FileExplorer;
using System.Windows;

namespace Singularity.UI.Previewers {
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
        public UIElement View => view ?? (view = new Views.OutsideInPreviewer());

        private string fileName;
        public string FileName {
            get {
                return fileName;
            }
            set {
                fileName = value;
                //(View as Views.OutsideInPreviewer)?.CloseFile();
                (View as Views.OutsideInPreviewer)?.OpenFile(fileName);
            }
        }
        public void Dispose() {
            (View as Views.OutsideInPreviewer).CloseFile();
        }
    }
}
    
