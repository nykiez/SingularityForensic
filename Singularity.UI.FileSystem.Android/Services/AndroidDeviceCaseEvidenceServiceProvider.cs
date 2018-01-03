﻿using CDFC.Parse.Contracts;
using System;
using CDFC.Util;
using System.ComponentModel.Composition;
using CDFC.Parse.Android.DeviceObjects;
using Microsoft.Practices.ServiceLocation;
using Singularity.Contracts.FileSystem;
using CDFC.Parse.Abstracts;
using Singularity.Contracts.Case;
using Singularity.Android.Models;
using Singularity.Contracts.FileExplorer;
using System.Collections.Generic;
using Singularity.Contracts.Common;

namespace Singularity.Android.Services {
    [Export(typeof(ICaseEvidenceServiceProvider))]
    public class AndroidDeviceCaseEvidenceServiceProvider : EmptyServiceProvider<AndroidDeviceCaseEvidenceServiceProvider>
        ,ICaseEvidenceServiceProvider {
        public IStreamFileParser StreamFileParser => AndroidDeviceStreamParser.StaticInstance;

        public void AddNewCaseFile(IFile file, string interLabel) {
            if(file is AndroidDevice adDevice) {
                ServiceLocator.Current.GetInstance<ICaseService>()?.AddNewCaseFile(
                    new AndroidDeviceCaseEvidence(adDevice, interLabel, DateTime.Now));
            }
            
        }

        public bool CheckIsValid(ICaseEvidence file) {
            if (file is AndroidDeviceCaseEvidence) {
                return true;
            }
            else if(file is PartitionCaseFile pCFile) {
                if(pCFile.Partition is AndroidPartition) {
                    return true;
                }
                else if(pCFile.Partition is AndroidUnknownParititon) {
                    return true;
                }
            }
            return false;
        }
        
        public override object GetInstance(Type serviceType) {
            if(serviceType == typeof(IFileDetailInfoProvider)) {
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