using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Local {
    /// <summary>
    /// 本地文件服务;
    /// </summary>
    [Export]
    public class LocalFileService {
        public void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
            CommonEventHelper.GetEvent<CaseEvidenceLoadingEvent>().SubscribeCheckingSubscribed((Action<(ICaseEvidence csEvidence, IProgressReporter reporter)>)OnCaseEvidenceLoading);

            CommonEventHelper.GetEvent<CaseEvidenceRemovedEvent>().SubscribeCheckingSubscribed((Action<ICaseEvidence>)OnCaseEvidenceRemoved);
            
            CommonEventHelper.GetEvent<CaseUnloadedEvent>().SubscribeCheckingSubscribed((Action<ICase>) OnCaseUnloaded);
        }

        /// <summary>
        /// 当卸载案件时;进行卸载操作;
        /// </summary>
        private void OnCaseUnloaded(ICase cs) {
            if(cs == null) {
                return;
            }

            var fsService = FileSystemService.Current;
            if (fsService == null) {
                LoggerService.Current.WriteCallerLine($"{nameof(fsService)} can't be null.");
                return;
            }

            var localEvidences = cs.CaseEvidences.Where(p => p.EvidenceTypeGuids?.Contains(Constants.EvidenceType_LocalDir)??false).ToArray();

            var localDirMountUnits = fsService.MountedUnits.Where(p => localEvidences.Any(q => q.EvidenceGUID == q.EvidenceGUID)).ToArray();

            //文件系统卸载本地Drive相关的案件文件;
            foreach (var unit in localDirMountUnits) {
                fsService.UnMountFile(unit);
            }
        }

        /// <summary>
        /// 所移除证据项若为本地文件夹,则响应卸载处理;
        /// </summary>
        /// <param name="csEvidence"></param>
        private void OnCaseEvidenceRemoved(ICaseEvidence csEvidence) {
            if (csEvidence == null) {
                return;
            }

            if (!(csEvidence.EvidenceTypeGuids?.Contains(Constants.EvidenceType_LocalDir) ?? false)) {
                return;
            }

            //文件系统卸载文件;
            var units = FileSystemService.Current.MountedUnits.Where(p => p.GUID == csEvidence.EvidenceGUID).ToArray();
            foreach (var unit in units) {
                FileSystemService.Current.UnMountFile(unit);
            }
        }

        /// <summary>
        /// 加载证据项若为本地文件夹,则响应本地文件夹处理;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnCaseEvidenceLoading((ICaseEvidence csEvidence, IProgressReporter reporter) tuple) {
            var csEvidence = tuple.csEvidence;
            var reporter = tuple.reporter;

            if (csEvidence == null) {
                return;
            }
            
            if (!(csEvidence.EvidenceTypeGuids?.Contains(Constants.EvidenceType_LocalDir) ?? false)) {
                return;
            }

            var dirPath = csEvidence[Constants.LocalDir_DirPath];
            if (string.IsNullOrEmpty(dirPath)) {
                LoggerService.WriteCallerLine($"{nameof(dirPath)} can't be null.");
                return;
            }

            try {
                reporter?.ReportProgress(50, LanguageService.FindResourceString(Constants.ProgressWord_LocalDirBeingSearched), string.Empty);
                var directory = LocalDirParser.GetDirectory(dirPath, reporter);
                if(directory == null) {
                    LoggerService.WriteCallerLine($"{nameof(directory)} can't be null.");
                    return;
                }
                FileSystemService.Current.MountFile(directory, csEvidence.EvidenceGUID);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
                throw;
            }
        }

        public void AddLocalDir() {
            if (CaseService.ConfirmCaseLoaded() != true) {
                return;
            }
            var dirPath = DialogService.Current.OpenDirect();
            if (string.IsNullOrEmpty(dirPath)) {
                return;
            }

            AddLocalDir(dirPath);
        }

        public void AddLocalDir(string dirPath) {
            if (CaseService.ConfirmCaseLoaded() != true) {
                return;
            }

            if (dirPath == null) {
                throw new ArgumentNullException(nameof(dirPath));
            }

            var di = new DirectoryInfo(dirPath);
            if (!Directory.Exists(dirPath)) {
                throw new DirectoryNotFoundException($"{dirPath} is not found.");
            }

            var csEvidence = CaseService.Current.CreateNewCaseEvidence(new string[] {
                Constants.EvidenceType_LocalDir
            },di.FullName,dirPath);

            csEvidence[Constants.LocalDir_DirPath] = Path.GetFullPath(dirPath);
            CaseService.Current.CurrentCase.AddNewCaseEvidence(csEvidence);
            CaseService.Current.CurrentCase.LoadCaseEvidence(csEvidence);
        }


    }
}
