using SingularityForensic.Contracts.Previewers;
using System;
using System.IO;
using System.Text;
using System.Windows;
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
                    (View as views.PlainTextPreviewer).LoadString(txt);
                }
                else {
                    (View as views.PlainTextPreviewer).Clear();
                }
                
            }
        }

        private views.PlainTextPreviewer view;
        public UIElement View => view ?? (view = new views.PlainTextPreviewer());

        FrameworkElement IPreviewer.View => throw new NotImplementedException();

        public void Dispose() {
            BaseStream?.Close();
            BaseStream = null;
        }
    }
}
