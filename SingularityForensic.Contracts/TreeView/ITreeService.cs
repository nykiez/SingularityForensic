using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.TreeView {
    /// <summary>
    /// 树形节点服务契约;
    /// </summary>
    public interface ITreeService {
        //void AddUnit(TreeUnit unit);
        /// <summary>
        /// 加入节点;
        /// </summary>
        /// <param name="parentUnit">若为空,则直接加入一级节点队列</param>
        /// <param name="unit"></param>
        void AddUnit(ITreeUnit parentUnit, ITreeUnit unit);
        /// <summary>
        /// 移除节点;
        /// </summary>
        /// <param name="unit"></param>
        void RemoveUnit(ITreeUnit unit);
        /// <summary>
        /// 当前所有节点;
        /// </summary>
        IEnumerable<ITreeUnit> CurrentUnits { get; }
        /// <summary>
        /// 当前选中的节点;
        /// </summary>
        ITreeUnit SelectedUnit { get; }
        /// <summary>
        /// 清除所有节点;
        /// </summary>
        void ClearUnits();
        /// <summary>
        /// 上下文命令项;
        /// </summary>
        IEnumerable<ICommandItem> ContextCommands { get; }

        void AddContextCommand(ICommandItem commandItem);

        void RemoveContextCommand(ICommandItem commandItem);
    }

    /// <summary>
    /// 属性服务拓展;
    /// </summary>
    public static class TreeServiceExtensions {
        /// <summary>
        /// 检查选定节点是否为指定类型;
        /// </summary>
        /// <param name="treeService"></param>
        /// <param name="typeGUID"></param>
        /// <returns></returns>
        public static bool CheckTypedUnitSelected(this ITreeService treeService, string typeGUID) {
            var slUnit = treeService.SelectedUnit;
            if (slUnit == null) {
                return false;
            }
            if (slUnit.TypeGuid != typeGUID) {
                return false;
            }
            return true;
        }
        
    }
    
}
