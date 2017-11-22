using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Models;
using SingularityForensic.Helpers;
using SingularityForensic.Modules.MainPage.Global.Events;
using SingularityForensic.ViewModels.Modules.MainPage.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SingularityForensic.Modules.MainPage.Global.Services {
    /// <summary>
    /// 节点服务契约;
    /// </summary>
    public interface INodeService {
        //void AddUnit(ITreeUnit unit);
        void AddUnitWith<TParentUnit, TNewUnit>(TParentUnit pUnit, TNewUnit nUnit) 
            where TParentUnit : ITreeUnit 
            where TNewUnit : ITreeUnit;

        void AddUnit<TNewUnit>(TNewUnit nUnit) where TNewUnit : ITreeUnit;

        void RemoveUnit(ITreeUnit unit);
        IEnumerable<ITreeUnit> CurrentUnits { get; }
        ITreeUnit SelectedNode { get; }
        void ClearNodes();
    }

    [Export(typeof(INodeService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NodeService:INodeService {
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
        public void RemoveUnit(ITreeUnit unit) {
            VM?.Value?.TreeUnits.Remove(unit);
        }

        //所有的Tab;
        public IEnumerable<ITreeUnit> CurrentUnits => VM?.Value?.TreeUnits.Select(p => p);

        public ITreeUnit SelectedNode => VM?.Value?.SelectedUnit;

        public void ClearNodes() {
            var cArgs = new CancelEventArgs();
            PubEventHelper.Publish<TreeNodesClearingEvent, CancelEventArgs>(cArgs);
            if(!cArgs.Cancel) {
                VM?.Value?.TreeUnits.Clear();
            }
        }

        public void AddUnitWith<TParentUnit, TNewUnit>(TParentUnit pUnit, TNewUnit nUnit)
            where TParentUnit : ITreeUnit
            where TNewUnit : ITreeUnit {
            if(pUnit == null) {
                throw new ArgumentNullException(nameof(pUnit));
            }
            if(nUnit == null) {
                throw new ArgumentNullException(nameof(nUnit));
            }

            pUnit.Children.Add(nUnit);

            PubEventHelper.Publish<TreeNodeAddedWith,(ITreeUnit,ITreeUnit)>((pUnit,nUnit));
            PubEventHelper.Publish<TreeNodeAddedWith<TParentUnit, TNewUnit>,(TParentUnit, TNewUnit)> ((pUnit, nUnit));
        }

        public void AddUnit<TNewUnit>(TNewUnit nUnit) where TNewUnit : ITreeUnit {
            VM?.Value?.AddUnit(nUnit);
            try {
                PubEventHelper.Publish<TreeNodeAdded,ITreeUnit>(nUnit);
                PubEventHelper.Publish<TreeNodeAdded<TNewUnit>, TNewUnit>(nUnit);
            }
            catch (Exception ex){
                EventLogger.Logger.WriteCallerLine(ex.Message);
                RemainingMessageBox.Tell(ex.Message);
            }
        }
    }
}
