﻿using CDFC.Parse.Abstracts;
using CDFC.Parse.Android.DeviceObjects;
using Singularity.Contracts.Case;
using Singularity.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Singularity.Android.Models {
    //安卓设备案件文件;
    public class AndroidDeviceCaseEvidence : DeviceCaseFile<AndroidDevice> {
        /// <summary>
        /// 安卓镜像设备总文件夹;
        /// </summary>
        public const string AndDeviceClassFolder = "AndroidDevices";
        /// <summary>
        /// /// 根据设备创建创建案件文件(针对案件中现存的案件文件);
        /// </summary>
        /// <param name="device"></param>
        /// <param name="xElem"></param>
        public AndroidDeviceCaseEvidence(AndroidDevice device, XElement xElem) : base(device, xElem) {
            //加入子案件文件;
            var elements = xElem.Elements(RootElemName);
            foreach (var elem in elements) {
                try {
                    if (device.Children[int.Parse(elem.Element(nameof(PartitionCaseFile.PartitionID)).Value)]
                        is Partition part) {
                        _children.Add(new PartitionCaseFile(part, elem));
                    }
                }
                catch {

                }
            }

        }

        /// <summary>
        /// 根据设备创建创建案件文件(针对新加入的案件文件);
        /// </summary>
        /// <param name="device"></param>
        /// <param name="interLabel"></param>
        /// <param name="dateAdded"></param>
        public AndroidDeviceCaseEvidence(AndroidDevice device, string interLabel, DateTime dateAdded) :
            base(device, nameof(AndroidDevice), device.Name, interLabel, dateAdded) {
            //加入子案件文件;
            var partID = 0;
            
            device.Children.ForEach((Action<CDFC.Parse.Contracts.IFile>)(p => {
                if (p is Partition part) {
                    var pFile = new PartitionCaseFile(part, $"{interLabel}-{part.Name}", dateAdded, partID++);
                    XElem.Add(pFile.XElem);
                    //.Add(pFile.Data);
                    _children.Add(pFile);
                }
            }));

        }

        private List<ICaseEvidence> _children = new List<ICaseEvidence>();
        public override IEnumerable<ICaseEvidence> InnerCaseFiles => _children?.Select(p => p);

        protected override string GetBasePath() => $"{AndDeviceClassFolder}/{Guid.NewGuid().ToString("N")}-{Name}";
    }
}