using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Shell.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing.Events.Shell {
    /// <summary>
    /// 若案件打开时关闭窗体,则询问是否关闭;
    /// </summary>
    [Export(typeof(IShellClosingEventHandler))]
    public class OnShellClosingOnCaseClosingHandler : IShellClosingEventHandler {
        public int Sort => 2;

        public bool IsEnabled => true;

        public void Handle(ShellClosingEventArgs args) {
            if(args == null) {
                return;
            }

            if(CaseService.Current.CurrentCase != null) {
                var res = MsgBoxService.Show(LanguageService.FindResourceString(Constants.MsgText_ConfirmToCloseCurrentCase), MessageBoxButton.YesNo);
                if(res != MessageBoxResult.Yes) {
                    args.CancelEventArgs.Cancel = true;
                }
                else {
                    CaseService.Current.CloseCurrentCase();
                }
                args.Handled = true;
            }
        }
    }
}
