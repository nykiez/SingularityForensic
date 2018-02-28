using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SingularityForensic.Previewers.Views {
    /// <summary>
    /// Interaction logic for ImagePreviewer.xaml
    /// </summary>
    public partial class ImagePreviewer : UserControl {
        public ImagePreviewer() {
            InitializeComponent();
        }
        private static long maxImgSize = 10485760;
        public void LoadStream(Stream stream) {
            if (stream?.Length <= maxImgSize) {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                try {
                    using (var ms = new MemoryStream(buffer)) {
                        var bitMapImage2 = new BitmapImage();
                        bitMapImage2.BeginInit();
                        bitMapImage2.CacheOption = BitmapCacheOption.OnLoad;
                        bitMapImage2.StreamSource = ms;
                        bitMapImage2.DecodePixelWidth = 400;
                        bitMapImage2.EndInit();
                        bitMapImage2.Freeze();
                        mainImg.Source = bitMapImage2;
                    }
                }
                catch {

                }
            }
            else {
                mainImg.Source = null;
            }
            
        }
    }
}
