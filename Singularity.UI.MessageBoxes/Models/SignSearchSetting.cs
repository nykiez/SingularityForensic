namespace Singularity.UI.MessageBoxes.Models {
    public class SignSearchSetting {
        public int MaxSize { get; set; }
        public byte[] KeyWord { get; set; }

        public bool AlignToSec { get; set; } = true;
        public int SectorSize { get; set; } = 512;
        public int SecStartLBA { get; set; }

        public string FileExtension { get; set; }

    }
}
