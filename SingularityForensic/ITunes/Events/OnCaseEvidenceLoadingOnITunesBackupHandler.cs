using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using static SingularityForensic.ITunes.Constants;

namespace SingularityForensic.ITunes.Events {
    ///  <summary>
    /// 加载案件文件若为ITunes备份文件夹,则响应文件夹解析;
    /// </summary>
    /// <param name="tuple"></param>
    /// </summary>
    [Export(typeof(ICaseEvidenceLoadingEventHandler))]
    class OnCaseEvidenceLoadingOnITunesBackupHandler : ICaseEvidenceLoadingEventHandler {
        public int Sort => 0;

        public bool IsEnabled => true;

        public void Handle((ICaseEvidence csEvidence, IProgressReporter reporter) tuple) {
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
                var manager = IOSBackUpParser.DoParse(backUpPath);
                FileSystemService.Current.MountFile(manager.Directory, csEvidence.XElem);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }
    }
}
