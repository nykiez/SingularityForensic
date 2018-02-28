﻿using System.Collections.Generic;

namespace CDFC.Parse.Modules.DeviceObjects {
    public class AndroidDiskMgr {
        public AndroidEFIInfo EFIInfo { get;set; }
        public AndroidMbrInfo MBRInfo { get;set; }
        public List<TabPartInfo> PartTabInfos { get; set; } = new List<TabPartInfo>();
    }
}