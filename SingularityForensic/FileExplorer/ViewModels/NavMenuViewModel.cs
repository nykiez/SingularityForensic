using Prism.Mvvm;
using SingularityForensic.Contracts.FileExplorer.Models;
using SingularityForensic.Contracts.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer.ViewModels {
    class NavMenuViewModel:BindableBase, INavMenuViewModel {
        public NavMenuViewModel() {

#if DEBUG
            //_rootNavNode = NavNodeFactory.CreateNew();
            //_rootNavNode.Name = "dada";
            //for (int i = 0; i < 10; i++) {
            //    var node = NavNodeFactory.CreateNew();
            //    node.Name = $"child{i}";
            //    _rootNavNode.Children.Add(node);
            //}
#endif
        }
        
        private string _selectedPath;
        public string SelectedPath {
            get => _selectedPath;
            set {
                _selectedPath = value;
                RaisePropertyChanged(nameof(InternalSelectedPath));
            }
        }
        /// <summary>
        /// 为避免联动过程中可能出现的相互通知导致性能下降,内部绑定节点与外部节点将使用不同的属性;
        /// </summary>
        public string InternalSelectedPath {
            get => _selectedPath;
            set {
                _selectedPath = value;
                InternalSelectedPathChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler InternalSelectedPathChanged;

        private INavNodeModel _rootNavNode;
        public INavNodeModel RootNavNode {
            get => _rootNavNode;
            set {
                SetProperty(ref _rootNavNode, value);
                if (value != null) {
                    SelectedPath = null;
                }
            }
        }

    }
}
