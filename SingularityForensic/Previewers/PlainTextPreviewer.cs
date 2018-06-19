using SingularityForensic.Contracts.Previewers;
using System;
using System.IO;
using System.Text;
using views = SingularityForensic.Previewers.Views;
namespace SingularityForensic.Previewers {
    public class PlainTextPreviewer : IPreviewer {
        public PlainTextPreviewer(Stream stream) {
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
                    var sr = new StreamReader(_baseStream,Encoding.UTF8);
                    var txt= sr.ReadToEnd();
                    (UIObject as views.PlainTextPreviewer).LoadString(txt);
                }
                else {
                    (UIObject as views.PlainTextPreviewer).Clear();
                }
                
            }
        }

        private views.PlainTextPreviewer view;
        public object UIObject => view ?? (view = new views.PlainTextPreviewer());
        
        public void Dispose() {
            BaseStream?.Close();
            BaseStream = null;
        }
    }
}
