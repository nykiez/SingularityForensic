using Microsoft.Practices.ServiceLocation;
using Singularity.UI.Case.Contracts;
using Singularity.UI.FileSystem.Interfaces;
using System;
using System.Linq;

namespace Singularity.UI.FileSystem.Helpers {
    public static class FileSystemHelper {
        public static IFileSystemServiceProvider GetFileSystemServiceProvider(ICaseFile caseFile)  {
            var providers = ServiceLocator.Current.GetAllInstances<IFileSystemServiceProvider>();
            try {
                return providers.FirstOrDefault(p => p.CheckIsValid(caseFile));
            }
            catch(Exception ex) {
                EventLogger.Logger.WriteCallerLine(ex.Message);
                return null;
            }
        }
    }
}
