using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Helpers;
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

            //To arrary将会阻止ServiceProvider.GetAllInstances的反复执行;
            _treeUnitRightClickeEventHandlers = ServiceProvider.GetAllInstances<ITreeUnitRightClickedEventHandler>().ToArray();
            _treeUnitSelectedChangedEventHandlers = ServiceProvider.GetAllInstances<ITreeUnitSelectedChangedEventHandler>().ToArray();
            _treeUnitAddedEventHandlers = ServiceProvider.GetAllInstances<ITreeUnitAddedEventHandler>().ToArray();
        }

        private void RegisterEvents() {
            VM.SelectedUnitChanged += VM_SelectedUnitChanged;
            VM.UnitRightClicked += VM_UnitRightClicked;
        }
        
        private IEnumerable<ITreeUnitRightClickedEventHandler> _treeUnitRightClickeEventHandlers;
        private IEnumerable<ITreeUnitSelectedChangedEventHandler> _treeUnitSelectedChangedEventHandlers;
        private IEnumerable<ITreeUnitAddedEventHandler> _treeUnitAddedEventHandlers;

        private void VM_UnitRightClicked(object sender, ITreeUnit e) {
            if(sender != VM) {
                return;
            }

            try {
                PubEventHelper.GetEvent<TreeUnitRightClicked>().Publish((e, this as ITreeService));
                PubEventHelper.PublishEventToHandlers((e, this as ITreeService), _treeUnitRightClickeEventHandlers);
            }
            catch(Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
        }
        
        

        /// <summary>
        /// 视图模型选定单元发生变化时;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VM_SelectedUnitChanged(object sender, EventArgs e) {
            if(sender != VM) {
                return;
            }

            PubEventHelper.PublishEventToHandlers((VM.SelectedUnit, this as ITreeService), _treeUnitSelectedChangedEventHandlers);
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
            if(nUnit == null) {
                throw new ArgumentNullException($"{nameof(nUnit)}");
            }

            ThreadInvoker.UIInvoke(() => {
                if (parentUnit == null) {
                    VM.AddUnit(nUnit);
                }
                else {
                    parentUnit.Children.Add(nUnit);
                }
            });
            
            try {
                PubEventHelper.GetEvent<TreeUnitAddedEvent>().Publish((nUnit, this));
                PubEventHelper.PublishEventToHandlers((nUnit, this as ITreeService),_treeUnitAddedEventHandlers);
            }
            catch (Exception ex) {
                LoggerService.WriteCallerLine(ex.Message);
            }
            
        }
        
    }
}
