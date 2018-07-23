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
            CommonEventHelper.GetEvent<CaseEvidenceLoadingEvent>().Subscribe(OnCaseEvidenceLoading);

            CommonEventHelper.GetEvent<CaseEvidenceRemovedEvent>().Subscribe(OnCaseEvidenceRemoved);

            CommonEventHelper.GetEvent<CaseUnloadedEvent>().Subscribe(OnCaseUnloaded);
        }

      
        /// <summary>
        /// 添加ITunes备份文件夹;
        /// </summary>
        public void AddITunesBackUpDir() {
            if (CaseService.ConfirmCaseLoaded() != true) {
                return;
            }

            string backUpPath = null;

            //由于暂时不能处理包含了非ASCII的路径;在输入纯ASCII码前，不能进行
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
            if(backUpPath == null) {
                throw new ArgumentNullException(nameof(backUpPath));
            }

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
        /// 加载案件文件若为ITunes备份文件夹,则ITunes备份处理;
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
                reporter?.ReportProgress(50, LanguageService.FindResourceString(Constants.ProgressWord_ITunesBeingParsed), string.Empty);
                var manager = IOSBackUpParser.DoParse(backUpPath);
                if(manager == null) {
                    return;
                }

                //文件系统挂载;
                FileSystemService.Current.MountFile(manager.Directory,csEvidence.EvidenceGUID);
                //添加实例;
                _managers.Add(manager);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }

        }

        /// <summary>
        /// 移除案件文件为ITunes备份文件夹时,进行卸载操作;
        /// </summary>
        /// <param name="csEvidence"></param>
        private void OnCaseEvidenceRemoved(ICaseEvidence csEvidence) {
            if(csEvidence == null) {
                return;
            }

            if (!(csEvidence.EvidenceTypeGuids?.Contains(EvidenceType_ITunesBackUpDir) ?? false)) {
                return;
            }

            try {
                var dir = FileSystemService.Current.GetFile(csEvidence.EvidenceGUID) as IDirectory;
                var manager = Managers.FirstOrDefault(p => p.Directory == dir);
                if (manager == null) {
                    return;
                }
                //从文件系统中卸载;
                FileSystemService.Current.UnMountFile(dir);
                _managers.Remove(manager);
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
        }

        /// <summary>
        /// 案件文件被卸载时,进行卸载操作;
        /// </summary>
        private void OnCaseUnloaded(ICase cs) {
            if(cs == null) {
                return;
            }

            try {
                foreach (var manager in Managers) {
                    FileSystemService.Current.UnMountFile(manager.Directory);
                }

                _managers.Clear();
            }
            catch(Exception ex) {
                LoggerService.WriteException(ex);
            }
        }

    }
}
