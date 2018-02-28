using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using CDFCCultures.Helpers;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Case;
using SingularityForensic.Contracts.Case.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileSystem {
    [Export(typeof(IFileSystemService))]
    public class FileSystemService : IFileSystemService {
        public FileSystemService() {
            Initialize();
        }
        public void Initialize() {
            RegisterEvents();
        }

        public void RegisterEvents() {
            PubEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Subscribe(csFile => {
                if(csFile == null) {
                    return;
                }

                if (!(csFile.EvidenceTypeGuids?.Contains(Contracts.FileSystem.Constants.ImgCaseEvidence) ?? false)) {
                    return;
                }

                var path = csFile[Contracts.FileSystem.Constants.ImgPath];
                if (string.IsNullOrEmpty(path)) {
                    return;
                }

                //提取所有流Parsers;
                var parsers = ServiceProvider.Current?.GetAllInstances<IImgParser>().OrderBy(p => p.SortNum);
                var streamParser = parsers.FirstOrDefault(p => csFile.EvidenceTypeGuids?.Contains(p.CaseManager.TypeGUID) ?? false);
                if (streamParser != null) {
                    var file = LoadFromPath(path, streamParser ,null);
                    streamParser.CaseManager.SetData(csFile, file);
                }
            });
        }

        //挂载新镜像文件到案件中;
        public IFile MountImg(string path) {
            if(path == null) {
                throw new ArgumentNullException(nameof(path));
            }

            try {
                if (!File.Exists(path)) {
                    throw new FileNotFoundException($"{nameof(path)}:{path} doesn't exist.");
                }
            }
            catch(Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                throw;
            }


            CaseEvidence csFile = null;

            var msgInfo = string.Empty;
            
            IFile file = null;
            IImgParser streamParser = null;

            var dialog = ServiceProvider.Current?.GetInstance<IDialogService>()?.CreateDoubleLoadingDialog();
            if (dialog == null) {
                LoggerService.WriteCallerLine($"{nameof(dialog)} can't be null.");
                return null;
            }

            dialog.Title = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AddingImg");

            //提取所有流Parsers;
            var parsers = ServiceProvider.Current?.GetAllInstances<IImgParser>().OrderBy(p => p.SortNum);
            if (parsers == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(parsers)} can't be null.");
                return null;
            }
            
            
            dialog.DoWork += delegate {
                try {
                    streamParser = parsers.FirstOrDefault(p => p.CheckIsValid(path));
                    //先行建立案件文件,详细加载将在案件文件加载中事件中进行;
                    var csService = ServiceProvider.Current?.GetInstance<ICaseService>();
                    if (csService == null) {
                        return;
                    }

                    var evidence = streamParser.CaseManager.CreateEvidence(
                        IOPathHelper.GetFileNameFromUrl(path),
                        path);

                    evidence[Contracts.FileSystem.Constants.ImgPath] = path;

                    //解析将在案件加载事件中进行,以便加载案件时镜像和添加镜像时解析动作一致;
                    var stoken = PubEventHelper.GetEvent<EvidenceLoadingProgressChanged>().Subscribe(tuple => {
                        dialog.ReportProgress(
                            tuple.totalPro,
                            tuple.detailPro,
                            LanguageService.Current?.FindResourceString("Parsing"),
                            LanguageService.Current?.FindResourceString("Parsing")
                        );
                    });

                    csService.AddNewCaseFile(evidence);

                    PubEventHelper.GetEvent<EvidenceLoadingProgressChanged>().Unsubscribe(stoken);
                }
                catch (IOException ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                    msgInfo = ex.Message;
                }
                catch (Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                    msgInfo = ex.Message;
                }
                
            };
            
            dialog.ShowDialog();

            ServiceProvider.Current?.GetInstance<IShellService>()?.ChangeLoadState(false);

            if (ServiceProvider.Current?.GetInstance<ICaseService>()?.CurrentCase == null) {
                LoggerService.Current?.WriteCallerLine("null case entity!");
                return null;
            }
            
            try {
                

            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                MsgBoxService.Current?.Show(ex.Message);
            }

            return file;
        }

        //内部挂载,将镜像加载到案件中;
        private IFile LoadFromPath(string path,IImgParser streamParser,Func<bool> isCancel) {
            if(path == null) {
                throw new ArgumentNullException(nameof(path));
            }

            if(streamParser == null) {
                throw new ArgumentNullException(nameof(streamParser));
            }

            if (!File.Exists(path)) {
                throw new FileNotFoundException(nameof(path));
            }

            
            return streamParser.ParseStream(
                            path,
                            tuple => {
                                PubEventHelper.GetEvent<EvidenceLoadingProgressChanged>().Publish((tuple.totalPro, tuple.detailPro));
                            },
                            isCancel);
        }
        
        public FSFile OpenFile(string fileName) {
            if (string.IsNullOrEmpty(fileName)) {
                return null;
            }

            var csService = ServiceProvider.Current.GetInstance<ICaseService>();
            if (csService?.CurrentCase?.CaseEvidences == null) {
                return null;
            }

            fileName = fileName.Replace('\\', '/');

            var args = fileName.Split('/');
            if (args.Count() <= 1) {
                EventLogger.Logger.WriteCallerLine($"Invalid args count");
                return null;
            }

            foreach (var evi in csService.CurrentCase.CaseEvidences) {
                if (evi.Data is Device device) {
                    if (evi.EvidenceGUID == args.FirstOrDefault()) {
                        IFile file = null;
                        if ((file = device.GetFileByUrl(fileName.Substring(fileName.IndexOf('/') + 1))) != null) {
                            return new FSFile(file);
                        }
                    }
                }
            }

            return null;
        }
    }
}
