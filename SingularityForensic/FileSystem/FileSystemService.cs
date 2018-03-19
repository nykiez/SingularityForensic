using CDFCCultures.Helpers;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using fsContracts = SingularityForensic.Contracts.FileSystem;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IFileSystemService))]
    class FileSystemService : IFileSystemService {
        [ImportingConstructor]
        public FileSystemService([ImportMany] IEnumerable<IStreamParsingProvider> streamParsers) {
            this._parsingProvider = streamParsers.OrderBy(p => p.Order);
        }
        IEnumerable<IStreamParsingProvider> _parsingProvider;

        public void Initialize() {
            RegisterEvents();
        }

        public void RegisterEvents() {
            
        }

        ////内部挂载,将镜像加载到案件中;
        //private IFile LoadFromPath(string path,IImgParser streamParser,Func<bool> isCancel) {
        //    if(path == null) {
        //        throw new ArgumentNullException(nameof(path));
        //    }

        //    if(streamParser == null) {
        //        throw new ArgumentNullException(nameof(streamParser));
        //    }

        //    if (!File.Exists(path)) {
        //        throw new FileNotFoundException(nameof(path));
        //    }


        //    return streamParser.ParseStream(
        //                    path,
        //                    tuple => {
        //                        PubEventHelper.GetEvent<EvidenceLoadingProgressChanged>().Publish((tuple.totalPro, tuple.detailPro));
        //                    },
        //                    isCancel);
        //}



        private List<(IHaveFileCollection enumFile, XElement xElem)> _enumFiles = new List<(IHaveFileCollection enumFile, XElement xElem)>();

        public IEnumerable<(IHaveFileCollection enumFile, XElement xElem)> EnumedFiles => _enumFiles.Select(p => p);
        
        public IHaveFileCollection MountStream(Stream stream,string name,XElement xElem, ProgressReporter reporter) {
            foreach (var provider in _parsingProvider) {
                try {
                    if (provider.CheckIsValidStream(stream)) {
                        var blockFile = provider.ParseStream(stream,name,xElem, reporter);
                        if(blockFile != null) {
                            _enumFiles.Add((blockFile,xElem));
                            return blockFile;
                        }
                    }
                }
                catch(Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                }   
            }

            return null;
        }

        //卸载文件;
        public void UnMountFile(IHaveFileCollection file) {
            if(file == null) {
                throw new ArgumentNullException(nameof(file));
            }

            var tuples = _enumFiles.Where(p => p.enumFile == file).ToArray();
            foreach (var tuple in tuples) {
                try {
                    if(tuple.enumFile is IDisposable disOb) {
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
    }
}
