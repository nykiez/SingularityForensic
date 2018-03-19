using SingularityForensic.Contracts.Casing;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Dos {
    //[Export(typeof(ICaseManager)), Export]
    //public class DosCaseManager : ICaseManager {
    //    public int SortOrder => throw new NotImplementedException();

    //    public string TypeGUID => Constants.DosDeviceEvidence;

    //    public void Clear() {
    //        throw new NotImplementedException();
    //    }

    //    public CaseEvidence CreateEvidence(string name, string interLabel) {
    //        return new CaseEvidence(new string[]{
    //        Contracts.FileSystem.Constants.ImgCaseEvidence,
    //        Constants.DosDeviceEvidence },
    //        name,
    //        interLabel);
    //    }

    //    public void Load(CaseLoadingHanlder loadingHanlder, Func<bool> isCancel) {
    //        throw new NotImplementedException();
    //    }

    //    public void Remove(CaseEvidence csEvidence) {
    //        throw new NotImplementedException();
    //    }

    //    public void SetData(CaseEvidence csEvidence, object data) {
    //        if(csEvidence == null) {
    //            throw new ArgumentNullException(nameof(csEvidence));
    //        }

    //        if (!(csEvidence.EvidenceTypeGuids?.Contains(Constants.DosDeviceEvidence)??false)) {
    //            throw new InvalidOperationException();
    //        }

    //        //csEvidence.SetData(data, Constants.DosDeviceEvidenceKey);
    //    }
    //}
}
