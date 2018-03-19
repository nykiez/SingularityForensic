using CDFC.Parse.Contracts;
using System;
using CDFC.Util;
using System.ComponentModel.Composition;
using CDFC.Parse.Modules.DeviceObjects;
using Microsoft.Practices.ServiceLocation;
using SingularityForensic.Contracts.FileSystem;
using CDFC.Parse.Abstracts;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Android.FileSystem.Models;
using SingularityForensic.Contracts.FileExplorer;
using System.Collections.Generic;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Android.FileSystem.Services {
    [Export(typeof(ICaseEvidenceServiceProvider))]
    public class AndroidDeviceCaseEvidenceServiceProvider : EmptyServiceProvider<AndroidDeviceCaseEvidenceServiceProvider>
        , ICaseEvidenceServiceProvider {
        public IImgParser StreamFileParser => AndroidDeviceStreamParser.StaticInstance;

        public void AddNewCaseFile(IFilefile, string interLabel) {
            //if (file is AndroidDevice adDevice) {
            //    ServiceProvider.Current.GetInstance<ICaseService>()?.AddNewCaseFile(
            //        new CaseEvidence(adDevice, interLabel, DateTime.Now));
            //}

        }

        public bool CheckIsValid(CaseEvidence file) => file.Data is AndroidDevice;

        public override object GetInstance(Type serviceType) {
            if (serviceType == typeof(IFileDetailInfoProvider)) {
                return Ext4NodeDetailProvider.StaticInstance;
            }
            return null;
        }

        public override object GetInstance(Type serviceType, string key) {
            throw new NotImplementedException();
        }


        //public static readonly AndroidDeviceFileExplorerServiceProvider Ins = AndroidDeviceFileExplorerServiceProvider.StaticInstance;
    }

}
