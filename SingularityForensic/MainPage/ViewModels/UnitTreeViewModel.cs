using System;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.ComponentModel.Composition;
using Prism.Mvvm;
using SingularityForensic.Contracts.TreeView;

namespace SingularityForensic.MainPage.ViewModels {

    [Export]
    public partial class UnitTreeViewModel:BindableBase {
        public UnitTreeViewModel() {


        }
        public ObservableCollection<ITreeUnit> TreeUnits { get; set; } = new ObservableCollection<ITreeUnit>();

        public void AddUnit(ITreeUnit unit) {
            TreeUnits.Add(unit);   
        }
        
    }

    //案件相关;
    public partial class UnitTreeViewModel {
        //通知节点展开;
        public event EventHandler<ITreeUnit> NotifyUnitExpanded;
        protected void NotifyUnitExpand(ITreeUnit unit) {
            NotifyUnitExpanded?.Invoke(this, unit);
        }
        
        private DelegateCommand unAvailebleCommand = new DelegateCommand(() => { }, () => false);
    }
    
    
    //选定单元以及上下文命令;
    public partial class UnitTreeViewModel {
        //选定的单元;
        private ITreeUnit _selectedUnit;
        public ITreeUnit SelectedUnit {
            get {
                return _selectedUnit;
            }
            set {
                SetProperty(ref _selectedUnit, value);
                SelectedUnitChanged?.Invoke(this, EventArgs.Empty);
            }
            
        }  

        public event EventHandler SelectedUnitChanged;
        public event EventHandler<ITreeUnit> UnitRightClicked;

        public void NotifyUnitClick(ITreeUnit unit) {
            SelectedUnit = unit;
        }
        
        public void NotifyRightClick(ITreeUnit treeUnit) {
            //NotifyUnitClick(treeUnit);
            UnitRightClicked?.Invoke(this, treeUnit);
        }
        
    }

    
}
