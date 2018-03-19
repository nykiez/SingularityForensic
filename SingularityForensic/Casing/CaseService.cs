using System;
using EventLogger;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using static SingularityForensic.Casing.Constants;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.Casing.Events;
using SingularityForensic.Contracts.Shell;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Document;
using System.ComponentModel;
using System.Linq;

namespace SingularityForensic.Casing{
    [Export(typeof(ICaseService))]
    public class CaseService:ICaseService {
#pragma warning disable 0169
        [Import]
        private Lazy<INodeService> nodeService;

        [Import]
        private Lazy<IDocumentTabService> documentService;

#pragma warning restore 0169

        /// <summary>
        /// 当前案件;
        /// </summary>
        public Case CurrentCase {
            get => _currentCase;
            private set => _currentCase = value;
        }
        private Case _currentCase;

        /// <summary>
        /// 创建一个空案件,注意,并不加载;
        /// </summary>
        /// <returns></returns>
        public Case CreateNewCase() {
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
        public void LoadCase(Case cs) {
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
                var reporter = new ProgressReporter();
                reporter.DoubleProgressReported += (sender, e) => {
                    msg.ReportProgress(e.totalPer, e.detailPer, e.desc, e.detail);
                };

                //证据项元素;
                var evidenceElems = cs.XDoc.Root.Elements(CaseEvidence.RootElemName);

                foreach (var elem in evidenceElems) {
                    try {
                        var csEvidence = new CaseEvidence(elem);
                        
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
        
        public void ShowCaseFileProperty(CaseEvidence csFile) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 打开现有案件文件;
        /// </summary>
        public void OpenExistingCase() {
            //若已经存在打开的案件;
            if (CurrentCase != null) {
                //询问是否关闭;
                if (
                    MsgBoxService.Current.Show(
                    $"{LanguageService.FindResourceString(ConfirmToCloseAndOpen)}",
                    MessageBoxButton.YesNo
                    ) != MessageBoxResult.Yes) {
                    return;
                }
            }

            var filter =
                $"({LanguageService.FindResourceString(SupportedCaseFileType)})|*.sfproj |" +
                $"({LanguageService.FindResourceString("AllFiles")})| *.* ";

            var fileName = DialogService.Current.OpenFile(filter);

            if (string.IsNullOrEmpty(fileName)) {
                return;
            }
            
            LoadCase(fileName);  
        }
    }
}
