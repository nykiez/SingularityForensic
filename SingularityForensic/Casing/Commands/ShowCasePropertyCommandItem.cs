using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Casing.Commands {
    /// <summary>
    /// 显示案件信息命令;
    /// </summary>
    public class ShowCasePropertyCommandItem:CommandItem {
        public ShowCasePropertyCommandItem(ICase cs) {
            this._cs = cs ?? throw new ArgumentNullException(nameof(cs));
            this.Initialize();
        }

        private ICase _cs;
        private void Initialize() {
            this.CommandName = LanguageService.FindResourceString(Constants.ShowCaseProperty);
            this.Command = new DelegateCommand(
                () => {
                    ServiceProvider.Current?.GetInstance<ICaseDialogService>()?.ShowCaseProperty(_cs);
                }
            );
            this.Sort = 256;
        }
    }
}
