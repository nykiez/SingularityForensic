using SingularityForensic.Contracts.Document;
using SingularityForensic.Contracts.Common;
using System.Collections.Generic;
using SingularityForensic.Controls.Info.ViewModels;

namespace SingularityForensic.Info.TabModels {

    public class BasicTextTabModel:IDocumentTab {
        public BasicTextTabModel(string pinKind,string text) {
            var vm = new InfoBasicViewModel { BasicText = text };
            //Content = new InfoBasicView() { DataContext = vm };
        }

        public string Title => throw new System.NotImplementedException();

        public List<CommandItem> Commands => throw new System.NotImplementedException();

        public object UIObject => throw new System.NotImplementedException();
    }
}
