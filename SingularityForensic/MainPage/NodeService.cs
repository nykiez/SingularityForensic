using CDFCMessageBoxes.MessageBoxes;
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
        Lazy<MainPageNodeManagerViewModel> VM;

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
            VM?.Value?.TreeUnits.Remove(unit);
        }

        //所有的Tab;
        public IEnumerable<TreeUnit> CurrentUnits => VM?.Value?.TreeUnits.Select(p => p);

        public TreeUnit SelectedNode => VM?.Value?.SelectedUnit;

        IEnumerable<TreeUnit> INodeService.CurrentUnits => throw new NotImplementedException();

        TreeUnit INodeService.SelectedNode => throw new NotImplementedException();

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
                EventLogger.Logger.WriteCallerLine(ex.Message);
                RemainingMessageBox.Tell(ex.Message);
            }
        }
        
    }
}
