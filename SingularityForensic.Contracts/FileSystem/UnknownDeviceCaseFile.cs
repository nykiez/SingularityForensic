using CDFC.Parse.DeviceObjects;
using SingularityForensic.Contracts.Case;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SingularityForensic.Contracts.FileSystem {
    //public class UnknownDeviceCaseFile : DeviceCaseFile<UnKnownDevice> {
    //    /// <summary>
    //    /// 安卓镜像设备总文件夹;
    //    /// </summary>
    //    public const string UnknownDeviceClassFolder = "UnknownDevices";
    //    public UnknownDeviceCaseFile(UnKnownDevice device, XElement xElem) : base(device, xElem) {

    //    }
    //    public UnknownDeviceCaseFile(UnKnownDevice device, string interLabel, DateTime dateAdded) :
    //        base(device, nameof(UnKnownDevice), device.Name, interLabel, dateAdded) {

    //    }

    //    public override IEnumerable<CaseEvidence> InnerCaseFiles => null;

    //    protected override string GetBasePath() => $"{UnknownDeviceClassFolder}{Guid.NewGuid().ToString("N")}/{Name}";
    //}

}
