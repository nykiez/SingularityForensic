using System.Windows.Media.Imaging;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IHavePreviewSource {
        BitmapImage PreviewSource { get; }
    }

}
