using System;
using EventLogger;
using System.ComponentModel.Composition;
using static SingularityForensic.Casing.Constants;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SingularityForensic.Casing {
    [Export(typeof(ICaseService))]
    public class CaseService:ICaseService {
        /// <summary>
        /// 当前案件;
        /// </summary>
        public ICase CurrentCase {
            get => _currentCase;
            private set => _currentCase = value;
        }

        public IEnumerable<ICase> RecentCases => Enumerable.Empty<ICase>();

        private ICase _currentCase;

        /// <summary>
        /// 创建一个空案件;
        /// </summary>
        /// <returns></returns>
        /// <param name="load">是否加载</param>
        public ICase CreateNewCase() {
            if (CurrentCase != null) {
                if(MsgBoxService.Current?.Show(
                    ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("ConfirmToCloseAndCreate"),
                   LanguageService.Current?.FindResourceString("Tip"), MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
                    return null;
                }
            }

            var sCase = CsDialogService.Current?.CreateCase();
            if (sCase != null) {
                try {
                    sCase.Save();
                }
                catch (Exception ex) {
                    LoggerService.Current?.WriteCallerLine(ex.Message);
                    MsgBoxService.Show($"{ex.Message}");
                }
            }
            
            return sCase;
        }
        
        /// <summary>
        /// 从指定路径加载案件;
        /// </summary>
        /// <param name="csPath"></param>
        public void LoadCase(string csPath) {
            if (string.IsNullOrEmpty(csPath)) {
                throw new ArgumentNullException(nameof(csPath));
            }

            try {
                var cs = Case.LoadFrom(csPath);
                if (cs == null) {
                    MsgBoxService.Show(LanguageService.FindResourceString(CaseConstructingFailed));
                }

                LoadCase(cs);
            }
            catch(Exception ex) {
                Logger.WriteCallerLine(ex.Message);
                MsgBoxService.ShowError(ex.Message);
            }
            
            
        }

        /// <summary>
        /// 加载案件;
        /// </summary>
        /// <param name="cFile"></param>
        public void LoadCase(ICase cs) {
            if(cs == null) {
                throw new ArgumentNullException(nameof(cs));
            }
            
            //关闭尚未关闭的案件;
            if (CurrentCase != null) {
                CloseCurrentCase();
            }

            //若当前案件仍不为空,则关闭案件未完成,需中断加载案件;
            if(CurrentCase != null) {
                return;
            }

            //发布案件加载中事件;
            PubEventHelper.GetEvent<CaseLoadingEvent>().Publish(cs);
            CurrentCase = cs;
            //发布已经加载案件的事件;
            PubEventHelper.GetEvent<CaseLoadedEvent>().Publish(CurrentCase);
            
            //从文档中加载证据项;
            var msg = ServiceProvider.Current.GetInstance<IDialogService>()?.CreateDoubleLoadingDialog();
            msg.Title = LanguageService.Current?.FindResourceString(LoadingCase);

            if (msg == null) {
                LoggerService.Current?.WriteCallerLine($"{nameof(IDialogService)} can't be null.");
                return;
            }
            
            msg.DoWork += delegate {
                //构建进度回调器;
                var reporter = ProgessReporterFactory.CreateNew();
                reporter.DoubleProgressReported += (sender, e) => {
                    msg.ReportProgress(e.totalPer, e.detailPer, e.desc, e.detail);
                };

                //证据项元素;
                var evidenceElems = cs.XDoc.Root.Elements(Contracts.Casing.Constants.CaseEvidenceRootElemName);

                foreach (var elem in evidenceElems) {
                    try {
                        var csEvidence = LoadCaseEvidenceFromXElem(elem);
                        
                        //加载证据项;
                        CurrentCase.LoadCaseEvidence(csEvidence,reporter);
                        
                    }
                    catch(Exception ex) {
                        Logger.WriteCallerLine(ex.Message);
                        ThreadInvoker.UIInvoke(() => {
                            MsgBoxService.ShowError(ex.Message);
                        });
                    }
                }
            };

            msg.ShowDialog();
        }

        /// <summary>
        /// 确认案件是否加载,若无,则将会询问后创建一个案件;
        /// </summary>
        /// <returns></returns>
        public bool ConfirmCaseLoaded() {
            if (CurrentCase == null) {
                if (MsgBoxService.Current.Show(
                    LanguageService.Current?.FindResourceString("ConfirmToCreateNewCase"), 
                    MessageBoxButton.YesNo) != MessageBoxResult.Yes) {
                    return false;
                }
                else {
                    var cs = CreateNewCase();
                    LoadCase(cs);
                }
            }

            return CurrentCase != null;
        }

        //关闭案件;
        public void CloseCurrentCase() {
            if(CurrentCase == null) {
                return;
            }

            var arg = new CancelEventArgs();
            PubEventHelper.GetEvent<CaseUnloadingEvent>().Publish(arg);
            if (arg.Cancel) {
                return;
            }

            CurrentCase.Save();
            CurrentCase = null;
            
            PubEventHelper.Publish<CaseUnloadedEvent>();
            
        }
        
        public void ShowCaseFileProperty(ICaseEvidence csFile) {
            throw new NotImplementedException();
        }

        public ICaseEvidence LoadCaseEvidenceFromXElem(XElement xelem) {
            if(xelem == null) {
                throw new ArgumentNullException(nameof(xelem));
            }
            return new CaseEvidence(xelem);
        }

        public ICaseEvidence CreateNewCaseEvidence(string[] typeGuids, string name, string interLabel) {
            return new CaseEvidence(typeGuids, name, interLabel);
        }
    }
}
