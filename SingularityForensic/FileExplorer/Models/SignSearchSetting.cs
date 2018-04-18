using SingularityForensic.Contracts.FileExplorer;

namespace SingularityForensic.FileExplorer.Models {
    public class SignSearchSetting: ICustomSignSearchSetting {
        public int MaxSize { get; set; }
        public byte[] KeyWord { get; set; }

        public bool AlignToSec { get; set; } = true;
        public int SectorSize { get; set; } = 512;
        public int SecStartLBA { get; set; }

        public string FileExtension { get; set; }

    }
}
