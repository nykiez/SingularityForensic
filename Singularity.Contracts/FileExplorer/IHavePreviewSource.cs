using System.Windows.Media.Imaging;

namespace Singularity.Contracts.FileExplorer {
    public interface IHavePreviewSource {
        BitmapImage PreviewSource { get; }
    }

}
