using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Imaging {
    class ImgMountUnit : IImgMountUnit {
        public ImgMountUnit(IImgMounter imgMounter,ICaseEvidence caseEvidence) {
            this.ImgMounter = imgMounter??throw new ArgumentNullException(nameof(imgMounter));
            this.CaseEvidence = caseEvidence ?? throw new ArgumentNullException(nameof(caseEvidence));
        }

        public IImgMounter ImgMounter { get; internal set; }

        public ICaseEvidence CaseEvidence { get; internal set; }
    }
}
