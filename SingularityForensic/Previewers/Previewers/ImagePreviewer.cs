using System;
using System.Windows;
using System.IO;
using SingularityForensic.Contracts.FileExplorer;

namespace SingularityForensic.Previewers {
    public class ImagePreviewer : IPreviewer {
        public ImagePreviewer(Stream stream) {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            this.Stream = stream;
        }
        private Stream stream;
        public Stream Stream {
            get {
                return stream;
            }
            private set {
                stream = value;
                if (stream != null) {
                    (View as Views.ImagePreviewer).LoadStream(stream);
                }
                else {
                    (View as Views.ImagePreviewer).LoadStream(null); 
                }

            }
        }
        private Views.ImagePreviewer view;
        public UIElement View => view ?? (view = new Views.ImagePreviewer());

        public void Dispose() {
            Stream?.Close();
            Stream = null;
        }
    }
}
