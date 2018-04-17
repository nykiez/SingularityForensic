using CDFCMessageBoxes.MessageBoxes;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using SingularityForensic.MainPage.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.MainPage {
    [Export(Contracts.MainPage.Constants.MainTreeService,typeof(ITreeService))]
    public class MainTreeService : ITreeService {
        [ImportingConstructor]
        public MainTreeService(UnitTreeViewModel vm) {
            this.VM = vm;
            Initialize();
        }
        
        public UnitTreeViewModel VM { get; }
        
        
        private void Initialize() {
            RegisterEvents();
        }

        private void RegisterEvents() {
            VM.SelectedUnitChanged += vm_SelectedUnitChanged;
        }

        private void vm_SelectedUnitChanged(object sender, EventArgs e) {
            if(sender != VM) {
                return;
            }

            PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((VM.SelectedUnit, this));
        }

        //移除unit;
        public void RemoveUnit(ITreeUnit unit) {
            if(unit == null) {
                throw new ArgumentNullException(nameof(unit));
            }

            if(unit.Parent != null) {
                unit.Parent.Children.Remove(unit);
            }
            else if (VM.TreeUnits.Contains(unit)) {
                VM.TreeUnits.Remove(unit);
            }
            else {
                var ex = new InvalidOperationException($"The tree unit is not contained in the list:{unit.TypeGuid}.");
                LoggerService.WriteCallerLine(ex.Message);
                throw ex;
            }

            PubEventHelper.GetEvent<TreeUnitRemovedEvent>().Publish((unit, this));
        }

        //所有的跟Unit;
        public IEnumerable<ITreeUnit> CurrentUnits => VM?.TreeUnits.Select(p => p);

        public ITreeUnit SelectedUnit => VM?.SelectedUnit;
        
        public void ClearNodes() {
            var cArgs = new CancelEventArgs();
            PubEventHelper.GetEvent<TreeUnitsClearingEvent>().Publish((cArgs,this));
            if (!cArgs.Cancel) {
                VM.TreeUnits.Clear();
            }
        }
        
        public void AddUnit(ITreeUnit parentUnit,ITreeUnit nUnit) {
            if(parentUnit == null) {
                VM.AddUnit(nUnit);
            }
            else {
                parentUnit.Children.Add(nUnit);
            }

            PubEventHelper.GetEvent<TreeUnitAddedEvent>().Publish((nUnit, this));
        }
        
    }
}
