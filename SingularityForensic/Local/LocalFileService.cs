using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
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
    class LocalFileService {
        public void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
            PubEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Subscribe(OnCaseEvidenceLoading);

            //PubEventHelper.GetEvent<CaseEvidenceRemovedEvent>().Subscribe(OnCaseEvidenceRemoved);

            //PubEventHelper.GetEvent<CaseUnloadedEvent>().Subscribe(OnCaseUnloaded);
        }

        /// <summary>
        /// 加载案件文件若为本地文件夹,则响应本地文件夹处理;
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

                var manager = new LocalDirectoryManager {
                    Directory = directory
                };
                
                _localDirectoryManagers.Add(manager);
            }
            catch(Exception ex) {

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
            if (dirPath == null) {
                throw new ArgumentNullException(nameof(dirPath));
            }

            var di = new DirectoryInfo(dirPath);
            if (Directory.Exists(dirPath)) {
                throw new DirectoryNotFoundException($"{dirPath} is not found.");
            }

            var csEvidence = CaseService.Current.CreateNewCaseEvidence(new string[] {
                Constants.EvidenceType_LocalDir
            },di.FullName,dirPath);

            csEvidence[Constants.LocalDir_DirPath] = Path.GetFullPath(dirPath);
            CaseService.Current.CurrentCase.AddNewCaseEvidence(csEvidence);
            CaseService.Current.CurrentCase.LoadCaseEvidence(csEvidence);
        }

        public IEnumerable<LocalDirectoryManager> DirectoryManagers { get; }
        private List<LocalDirectoryManager> _localDirectoryManagers = new List<LocalDirectoryManager>();

    }
}
