using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IFileSystemService))]
    class FileSystemServiceImpl : IFileSystemService {
        IEnumerable<IStreamParsingProvider> _parsingProviders;

        public void Initialize() {
            this._parsingProviders = ServiceProvider.GetAllInstances<IStreamParsingProvider>().OrderBy(p => p.Order).ToArray();
        }
        
        private List<IMountedUnit> _enumFiles = new List<IMountedUnit>();

        public IEnumerable<IMountedUnit> MountedUnits => _enumFiles.Select(p => p);
        
        public IFile MountStream(Stream stream,string name,XElement xElem, IProgressReporter reporter) {
            IFile file = null;

            foreach (var provider in _parsingProviders) {
                try {
                    if (!provider.CheckIsValidStream(stream)) {
                        continue;   
                    }
                    file = provider.ParseStream(stream, name, xElem, reporter);

                    if(file != null) {
                        break;
                    }
                }
                catch(Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                }   
            }

            if(file == null) {
                file = ServiceProvider.Current?.GetInstance<IUnknownDeviceParsingProvider>()?.
                    ParseStream(stream, name, xElem);
            }

            if (file != null) {
                _enumFiles.Add(new MountedUnit { File = file, XElem = xElem });
                return file;
            }

            return file;
        }

        /// <summary>
        /// 卸载文件;
        /// </summary>
        /// <param name="file"></param>
        public void UnMountFile(IFile file) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            var tuples = _enumFiles.Where(p => p.File == file).ToArray();
            foreach (var tuple in tuples) {
                try {
                    if(tuple.File is IDisposable disOb) {
                        disOb.Dispose();
                    }
                }
                catch(Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
                finally {
                    _enumFiles.Remove(tuple);
                }
            }
        }

        /// <summary>
        /// 挂载现有文件文件;
        /// </summary>
        /// <param name="file"></param>
        public void MountFile(IFile file,XElement xElem) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            _enumFiles.Add(new MountedUnit {
                File = file,
                XElem = xElem
            });
        }

        
    }

    class MountedUnit : IMountedUnit {
        public IFile File { get; internal set; }

        public XElement XElem { get; internal set; }
    }

}
