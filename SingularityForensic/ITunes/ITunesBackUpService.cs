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
using static SingularityForensic.ITunes.Constants;

namespace SingularityForensic.ITunes {
    [Export]
    public partial class ITunesBackUpService {
        public void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
            PubEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Subscribe(OnCaseEvidenceLoading);
            
        }

        /// <summary>
        /// 添加ITunes备份文件夹;
        /// </summary>
        public void AddITunesBackUpDir() {
            if (CaseService.ConfirmCaseLoaded() != true) {
                return;
            }

            string backUpPath = null;

            while (true) {
                backUpPath = DialogService.Current.OpenDirect();

                if (string.IsNullOrEmpty(backUpPath)) {
                    return;
                }

                if (WordsIScn(backUpPath) == true) {
                    MsgBoxService.Show(LanguageService.FindResourceString(Constants.InvalidItunesBPath));
                    continue;
                }

                break;
            }

            AddITunesBackUpDir(backUpPath);
        }

        public void AddITunesBackUpDir(string backUpPath) {
            var di = new DirectoryInfo(backUpPath);
            if (!di.Exists) {
                throw new DirectoryNotFoundException($"{backUpPath}");
            }

            var csEvidence = CaseService.Current.CreateNewCaseEvidence(new string[] {
                EvidenceType_ITunesBackUpDir
            }, di.Name, backUpPath);

            csEvidence[ITunesBackUpDir_Path] = Path.GetFullPath(backUpPath);

            CaseService.Current.CurrentCase.AddNewCaseEvidence(csEvidence);

            CaseService.Current.CurrentCase.LoadCaseEvidence(csEvidence);
        }
        
        private List<ITunesBackUpManager> _managers = new List<ITunesBackUpManager>();
        public IEnumerable<ITunesBackUpManager> Managers => _managers.Select(p => p);

        /// <summary>
        /// 判断是否含有非ASCII码;
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static bool WordsIScn(string str) {
            if (str == null) {
                return false;
            }
            var sa = str.ToCharArray();
            foreach (var ch in sa) {
                if (ch > 127) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 加载案件文件若为ITunes备份文件夹,则响应镜像解析;
        /// </summary>
        /// <param name="tuple"></param>
        private void OnCaseEvidenceLoading((ICaseEvidence csEvidence, IProgressReporter reporter) tuple) {
            var csEvidence = tuple.csEvidence;
            var reporter = tuple.reporter;

            if (csEvidence == null) {
                return;
            }

            if (!(csEvidence.EvidenceTypeGuids?.Contains(EvidenceType_ITunesBackUpDir) ?? false)) {
                return;
            }
            var backUpPath = csEvidence[ITunesBackUpDir_Path];
            if (string.IsNullOrEmpty(backUpPath)) {
                LoggerService.WriteCallerLine($"{nameof(backUpPath)} can't be null.");
                return;
            }

            try {
                reporter.ReportProgress(50, LanguageService.FindResourceString(Constants.ProgressWord_ITunesBeingParsed), string.Empty);
                var manager = IOSBackUpParser.DoParse(backUpPath);
                if(manager == null) {
                    return;
                }

                FileSystemService.Current.MountFile(manager.Directory, csEvidence.XElem);
                _managers.Add(manager);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

        }
    }
}
