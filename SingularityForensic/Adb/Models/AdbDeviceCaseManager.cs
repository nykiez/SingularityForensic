namespace SingularityForensic.Adb.Models {
    //[Export(typeof(ICaseManager))]
    //[PartCreationPolicy(CreationPolicy.Shared)]
    //public class AdbDeviceCaseManager : ICaseManager {
    //    public int SortOrder => 0;

    //    public string TypeGUID => throw new NotImplementedException();

    //    public void Load(CaseLoadingHanlder loadingHanlder, Func<bool> isCancel) {
    //        //if(ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
    //        //    Logger.WriteLine($"{nameof(AdbDeviceCaseManager)}->{nameof(Load)}:{nameof(ICaseService)}-{nameof(ICaseService.CurrentCase)} can't be null.");
    //        //    return;
    //        //}

    //        //var cs = ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase;
    //        ////找出所有Adb证据文件;
    //        //var adbFilesElems = cs.XDoc.Root.
    //        //    Element(nameof(ICase.CaseEvidences)).
    //        //    Elements("CaseFile").
    //        //    Where(p => p.Attribute(nameof(CaseEvidence.Type))?.Value == AdbDeviceCaseFile.AdbCaseFileType);

    //        ////遍历adb证据文件;寻找容器序列化;
    //        //foreach (var elem in adbFilesElems) {
    //        //    try {
    //        //        var storageFile = $"{ServiceProvider.Current?.GetInstance<ICaseService>().CurrentCase.Path}/{elem.Element(nameof(CaseEvidence.EvidenceGUID)).Value}/{AdbDeviceCaseFile.AdbStorageFile}";
    //        //        var formatter = new BinaryFormatter();
    //        //        using (var fs = File.OpenRead(storageFile)) {
    //        //            var container = formatter.Deserialize(fs) as PhoneFullInfoContainer;
    //        //            if (container == null) {
    //        //                throw new NullReferenceException($"{nameof(container)} can't be null!" +
    //        //                    $"SN : {elem.Element(AdbDeviceCaseFile.AdbSerialNumber).Value}");
    //        //            }
    //        //            var cFile = new AdbDeviceCaseFile(container, elem);
    //        //            ServiceProvider.Current.GetInstance<ICaseService>()?.AddNewCaseFile(cFile);
    //        //            ServiceProvider.Current.GetInstance<AdbViewerService>()?.AddAdbInfoNode(container);
    //        //        }
    //        //    }
    //        //    catch (Exception ex) {
    //        //        Logger.WriteLine($"{nameof(AdbDeviceCaseManager)}->{nameof(Load)}:{ex.Message}");
    //        //        Application.Current.Dispatcher.Invoke(() => {
    //        //            RemainingMessageBox.Tell($"{FindResourceString("FailedToLoadAdbDevice")}:{ex.Message}");
    //        //        });
    //        //    }
    //        //}
    //    }

    //    public void Clear() {

    //    }

    //    public void SetData(CaseEvidence csEvidence, object data) {
    //        if(csEvidence == null) {
    //            throw new ArgumentNullException(nameof(csEvidence));
    //        }

    //        //if(data is adbs)
    //        //if (csEvidence.EvidenceTypeGuids?.Contains(TypeGUID) ?? false) {

    //        //}
    //    }

    //    public void Remove(CaseEvidence csEvidence) {
    //        throw new NotImplementedException();
    //    }

    //    public CaseEvidence CreateEvidence(string name, string interLabel) {
    //        throw new NotImplementedException();
    //    }
    //}
}
