using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using System.ComponentModel.Composition;
using System.IO;
using static SingularityForensic.ITunes.Constants;

namespace SingularityForensic.ITunes {
    [Export]
    public class ITunesBackUpService {
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
            }, Path.GetDirectoryName(backUpPath), backUpPath);
            
            csEvidence[ITunesBackUpDir_Path] = Path.GetFullPath(backUpPath);

            CaseService.Current.CurrentCase.AddNewCaseEvidence(csEvidence);

            CaseService.Current.CurrentCase.LoadCaseEvidence(csEvidence);
        }

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

        
    }
}
