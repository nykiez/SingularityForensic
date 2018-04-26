using System;
using System.Windows;
using System.IO;
using SingularityForensic.Contracts.Previewers;

namespace SingularityForensic.Previewers {
    public class ImagePreviewer : IPreviewer {
        public ImagePreviewer(Stream stream) {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            this.BaseStream = stream;
        }

        private Stream _baseStream;
        public Stream BaseStream {
            get {
                return _baseStream;
            }
            private set {
                _baseStream = value;
                if (_baseStream != null) {
                    (View as Views.ImagePreviewer).LoadStream(_baseStream);
                }
                else {
                    (View as Views.ImagePreviewer).LoadStream(null); 
                }

            }
        }

        private Views.ImagePreviewer view;
        public FrameworkElement View => view ?? (view = new Views.ImagePreviewer());

        public void Dispose() {
            BaseStream?.Dispose();
            BaseStream = null;
        }
    }
}
