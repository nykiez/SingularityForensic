using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Case.Events {
    public class EvidenceLoadingProgressChanged:PubSubEvent<(int totalPro,int detailPro)> {

    }
}
