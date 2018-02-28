using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.ComponentModel.Composition;
using Prism.Mvvm;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.TreeView;

namespace SingularityForensic.ViewModels.Modules.MainPage.ViewModels {
    [Export]
    public partial class MainPageNodeManagerViewModel:BindableBase {
        public MainPageNodeManagerViewModel() {


        }
        public ObservableCollection<TreeUnit> TreeUnits { get; set; } = new ObservableCollection<TreeUnit>();

        public void AddUnit(TreeUnit unit) {
            TreeUnits.Add(unit);   
        }
        
    }

    //案件相关;
    public partial class MainPageNodeManagerViewModel {
        //通知节点展开;
        public event EventHandler<TreeUnit> NotifyUnitExpanded;
        protected void NotifyUnitExpand(TreeUnit unit) {
            NotifyUnitExpanded?.Invoke(this, unit);
        }
        
        private DelegateCommand unAvailebleCommand = new DelegateCommand(() => { }, () => false);
    }
    
    
    //选定单元以及上下文命令;
    public partial class MainPageNodeManagerViewModel {
        private TreeUnit _selectedUnit;
        public TreeUnit SelectedUnit {
            get {
                return _selectedUnit;
            }
            set {
                SetProperty(ref _selectedUnit, value);
                PubEventHelper.GetEvent<SelectedNodeChangedEvent>()?.Publish(_selectedUnit);
            }
            
        }  //选定的单元;
        
        public void NotifyUnitClick(TreeUnit unit) {
            SelectedUnit = unit;
            PubEventHelper.GetEvent<TreeNodeClickEvent>()?.Publish(unit);
        }
        
        public void NotifyRightClick(TreeUnit treeUnit) {
            //NotifyUnitClick(treeUnit);
            PubEventHelper.Publish<TreeNodeRightClicked, TreeUnit>(treeUnit);
        }
        
    }

    
}
