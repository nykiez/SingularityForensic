using CDFC.Parse.Contracts;
using Singularity.UI.FileSystem.Interfaces;
using System;
using Singularity.UI.Case.Contracts;
using Singularity.UI.FileSystem.Models;
using Singularity.UI.FileSystem.Android.Models;
using CDFC.Util;
using System.ComponentModel.Composition;
using CDFC.Parse.Android.DeviceObjects;
using Microsoft.Practices.ServiceLocation;
using Singularity.UI.Case.Global.Services;

namespace Singularity.UI.FileSystem.Android.Global.Services {
    [Export(typeof(IFileSystemServiceProvider))]
    public class AndroidDeviceFileSystemServiceProvider : IFileSystemServiceProvider {
        public IStreamFileParser StreamFileParser => AndroidDeviceStreamParser.StaticInstance;

        public void AddNewCaseFile(IFile file, string interLabel) {
            if(file is AndroidDevice adDevice) {
                ServiceLocator.Current.GetInstance<ICaseService>()?.AddNewCaseFile(new AndroidDeviceCaseFile(adDevice, interLabel, DateTime.Now));
            }
            
        }

        public bool CheckIsValid(ICaseFile file) => file is AndroidDeviceCaseFile;

        public object GetService(Type serviceType) {
            if(serviceType == typeof(IRowBuilder)) {
                return AndroidDeviceRowBuilder.StaticInstance;
            }
            return null;
        }

        public TService GetService<TService>() {
            var service = GetService(typeof(TService));
            if (service != null) {
                return (TService)service;
            }
            return default(TService);
        }
    }

     

    public class AndroidDeviceRowBuilder : GenericStaticInstance<AndroidDeviceRowBuilder>, IRowBuilder {
        public IFileRow BuildRow(IFile file) {
            return new AndroidFileRow<IFile>(file);
        }
    }
}
