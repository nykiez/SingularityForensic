using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Common.Commands;
using System;

namespace SingularityForensic.Casing.Commands {
    /// <summary>
    /// 打开案件所在路径的命令;
    /// </summary>
    public class OpenCasePathCommandItem:CommandItem {

        public OpenCasePathCommandItem(ICase cs) {
            this._cs = cs ?? throw new ArgumentNullException(nameof(cs));

            this.Initialize();
        }

        private ICase _cs;

        private void Initialize() {
            this.Command = new OpenPathCommand(_cs.Path);
            this.CommandName = LanguageService.FindResourceString(Constants.OpenCasePathFolder);
            this.Sort = 128;
        }

        
    }
}
