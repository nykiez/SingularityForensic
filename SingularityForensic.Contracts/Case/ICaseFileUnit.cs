using CDFCUIContracts.Models;

namespace SingularityForensic.Contracts.Case {
    public interface ICaseEvidenceUnit<out TEvidence>:ITreeUnit {
        TEvidence Evidence { get; }
    }
}
