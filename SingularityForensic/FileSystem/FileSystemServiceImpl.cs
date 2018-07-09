using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
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

        public IFile GetFile(string path) {
            if (string.IsNullOrEmpty(path)) {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.Replace('/',Contracts.FileSystem.Constants.Path_SplitChar);
            var pathParams = path.Split(Contracts.FileSystem.Constants.Path_SplitChar);
            if (pathParams.Length == 0) {
                throw new ArgumentException($"Invalid {nameof(path)}:{path}");
            }

            var mountedUnit = MountedUnits.FirstOrDefault(p => p.XElem.GetXElemValue(Contracts.Common.Constants.EvidenceGUID) == pathParams[0]);
            
            if(mountedUnit == null) {
                return null;
            }

            if (pathParams.Length == 1) {
                return mountedUnit.File;
            }

            if (!(mountedUnit.File is IHaveFileCollection haveFileCollection)) {
                return null;
            }
            //在集合内部使用集合名替代案件文件GUID;
            var pathArray = pathParams.ToArray();
            pathArray[0] = haveFileCollection.Name;
            return haveFileCollection.GetFileByUrlArgs(pathArray);
        }

        public string GetPath(IFile file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            var mountUnit = GetOwnMountUnit(file);
            if (mountUnit == null) {
                return null;
            }
            var root = mountUnit.File;
            //若查找的文件为根文件,则直接返回跟文件的GUID;
            if (root == file) {
                return mountUnit.XElem.GetXElemValue(Contracts.Common.Constants.EvidenceGUID);
            }

            if(root is IHaveFileCollection haveFileCollection) {
                var args = haveFileCollection.GetUrlArgsByFile(file);
                if(args == null || args.Length == 0) {
                    return null;
                }
                args[0] = mountUnit.XElem.GetXElemValue(Contracts.Common.Constants.EvidenceGUID);

                var sb = new StringBuilder();
                var argIndex = 0;
                foreach (var arg in args) {
                    if(argIndex == 0) {
                        sb.Append(arg);
                    }
                    else {
                        sb.Append($"{Contracts.FileSystem.Constants.Path_SplitChar}{arg}");
                    }

                    argIndex++;
                }
                return sb.ToString();
            }

            return null;
        }

        public IMountedUnit GetOwnMountUnit(IFile file) {
            if (file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            var root = file;
            while (root.Parent != null) {
                root = root.Parent;
            }

            var mountUnit = MountedUnits.FirstOrDefault(p => p.File == root);
            return mountUnit;
        }
    }

    class MountedUnit : IMountedUnit {
        public IFile File { get; internal set; }

        public XElement XElem { get; internal set; }
    }

}
