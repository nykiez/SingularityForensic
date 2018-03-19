using CDFCUIContracts.Models;

namespace SingularityForensic.Contracts.Casing {
    public interface ICaseEvidenceUnit<out TEvidence>:ITreeUnit {
        TEvidence Evidence { get; }
    }
}
