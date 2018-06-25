using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Controls;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.FileExplorer {
    public interface IFolderBrowserDataContext:IUIObjectProvider,IExtensible {
        IFolderBrowserViewModel FolderBrowserViewModel { get; }
        IStackGrid<IUIObjectProvider> StackGrid { get; }
    }
}
