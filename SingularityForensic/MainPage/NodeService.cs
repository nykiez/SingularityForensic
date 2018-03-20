using CDFCMessageBoxes.MessageBoxes;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.ViewModels.Modules.MainPage.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.MainPage.Global {
    [Export(typeof(INodeService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NodeService : INodeService {
        [Import]
        Lazy<NodeTreeViewModel> VM;

        //增加unit;
        //public void AddUnit(ITreeUnit unit) {
        //    VM?.Value?.AddUnit(unit);
        //    try {
        //        PubEventHelper.GetEvent<TreeNodeAdded>()?.Publish(unit);
        //    }
        //    catch (Exception ex) {
        //        RemainingMessageBox.Tell(ex.Message);
        //    }
        //}

        //移除unit;
        public void RemoveUnit(TreeUnit unit) {
            if(unit == null) {
                throw new ArgumentNullException(nameof(unit));
            }

            if(unit.Parent != null) {
                unit.Parent.Children.Remove(unit);
                return;
            }

            if (VM?.Value?.TreeUnits?.Contains(unit) ?? false) {
                VM?.Value?.TreeUnits.Remove(unit);
                return;
            }
            
        }

        //所有的跟Unit;
        public IEnumerable<TreeUnit> CurrentUnits => VM?.Value?.TreeUnits.Select(p => p);

        public TreeUnit SelectedUnit => VM?.Value?.SelectedUnit;
        
        public void ClearNodes() {
            var cArgs = new CancelEventArgs();
            PubEventHelper.Publish<TreeNodesClearingEvent, CancelEventArgs>(cArgs);
            if (!cArgs.Cancel) {
                VM?.Value?.TreeUnits.Clear();
            }
        }
        
        public void AddUnit(TreeUnit parentUnit,TreeUnit nUnit) {
            if(parentUnit == null) {
                VM?.Value?.AddUnit(nUnit);
            }
            else {
                nUnit.MoveToUnit(parentUnit);
            }
            
            try {
                PubEventHelper.Publish<TreeNodeAddedEvent, TreeUnit>(nUnit);
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                RemainingMessageBox.Tell(ex.Message);
            }
        }
        
    }
}
