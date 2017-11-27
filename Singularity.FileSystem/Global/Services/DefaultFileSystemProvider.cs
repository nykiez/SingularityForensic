using CDFC.Parse.Contracts;
using CDFC.Util;
using Singularity.UI.Case.Contracts;
using Singularity.UI.FileSystem.Interfaces;
using Singularity.UI.FileSystem.Models;
using System;

namespace Singularity.UI.FileSystem.Global.Services {

    public class DefaultFileSystemProvider : GenericStaticInstance<DefaultFileSystemProvider>, IFileSystemServiceProvider {
        public IStreamFileParser StreamFileParser => throw new NotImplementedException();

        public void AddNewCaseFile(IFile file, string interLabel) {
            throw new NotImplementedException();
        }

        public bool CheckIsValid(ICaseFile file) {
            if (file is UnknownDeviceCaseFile) {
                return true;
            }
            else {
                return false;
            }
        }

        public TService GetService<TService>() {
            return (TService)GetService(typeof(TService));
        }

        public object GetService(Type ServiceType) {
            if (ServiceType == typeof(IRowBuilder)) {
                return DefaultRowBuilder.StaticInstance;
            }
            return null;
        }
    }

    public class DefaultRowBuilder : GenericStaticInstance<DefaultRowBuilder>, IRowBuilder {
        public IFileRow BuildRow(IFile file) {
            return new FileRow<IFile>(file);
        }
    }
}
