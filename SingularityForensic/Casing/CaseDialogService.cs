using SingularityForensic.Casing.MessageBoxes;
using SingularityForensic.Contracts.Casing;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing {
    [Export(typeof(ICaseDialogService))]
    public class CaseDialogService : ICaseDialogService {
        public ICase CreateCase() {
            return CreateCaseMessageBox.Show();
        }
    }
}
