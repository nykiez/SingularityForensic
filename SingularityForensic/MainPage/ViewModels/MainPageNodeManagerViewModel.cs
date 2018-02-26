using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.ComponentModel.Composition;
using Prism.Mvvm;
using CDFCUIContracts.Models;
using Singularity.Contracts.Helpers;
using Singularity.Contracts.MainPage.Events;

namespace SingularityForensic.ViewModels.Modules.MainPage.ViewModels {
    [Export]
    public partial class MainPageNodeManagerViewModel:BindableBase {
        public MainPageNodeManagerViewModel() {


        }
        public ObservableCollection<ITreeUnit> TreeUnits { get; set; } = new ObservableCollection<ITreeUnit>();

        public void AddUnit(ITreeUnit unit) {
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
        private ITreeUnit _selectedUnit;
        public ITreeUnit SelectedUnit {
            get {
                return _selectedUnit;
            }
            set {
                SetProperty(ref _selectedUnit, value);
                PubEventHelper.GetEvent<SelectedNodeChangedEvent>()?.Publish(_selectedUnit);
            }
            
        }  //选定的单元;
        
        public void NotifyUnitClick(ITreeUnit unit) {
            SelectedUnit = unit;
            PubEventHelper.GetEvent<TreeNodeClickEvent>()?.Publish(unit);
        }
        
        public void NotifyRightClick(ITreeUnit treeUnit) {
            //NotifyUnitClick(treeUnit);
            PubEventHelper.Publish<TreeNodeRightClicked, ITreeUnit>(treeUnit);
        }
        
    }

    
}
