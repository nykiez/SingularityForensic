using Singularity.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Windows;
using views = Singularity.Previewers.Views;
namespace Singularity.Previewers {
    public class PlainTextPreviewer : IPreviewer {
        public PlainTextPreviewer(Stream stream) {
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
                    var sr = new StreamReader(stream,Encoding.UTF8);
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

        public void Dispose() {
            Stream?.Close();
            Stream = null;
        }
    }
}
