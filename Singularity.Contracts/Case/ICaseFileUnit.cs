using CDFCUIContracts.Models;

namespace Singularity.Contracts.Case {
    public interface ICaseEvidenceUnit<out TEvidence>:ITreeUnit {
        TEvidence Evidence { get; }
    }
}
