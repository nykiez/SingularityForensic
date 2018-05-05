using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SingularityForensic.Contracts.Controls {
    /// <summary>
    /// UI堆叠栈,本契约封装对Grid的直接操作;
    /// </summary>
    /// <typeparam name="IStackItem">堆叠项类型</typeparam>
    public interface IStackGrid<TStackItem> : IUIObjectProvider where TStackItem:IUIObjectProvider{
         IEnumerable<(TStackItem stackItem,GridChildLength gridChildLength)> Children { get; }
        /// <summary>
        /// 添加子项;
        /// </summary>
        /// <typeparam name="TDefinition"></typeparam>
        /// <param name="child"></param>
        /// <param name="definition">子项类型,须是ColumnDefinition/RowDefinition</param>
        /// <param name="index">插入位置</param>
        void AddChild<TDefinition>(TStackItem child,GridChildLength gridChildLength, int index = -1);
        void Remove(TStackItem child);
        /// <summary>
        /// 朝向;
        /// </summary>
		Orientation Orientation { get; set; }
        double SplitterLength { get; set; }
    }
	
	public interface IStackGridFactory {
		/// <summary>
        /// 创建一个GridStack;
        /// </summary>
        /// <param name="spliterLength">分割大小</param>
        /// <returns></returns>
        IStackGrid<TStackItem> CreateNew<TStackItem>() where TStackItem:IUIObjectProvider;
    }

    public class StackGridFactory :GenericServiceStaticInstance<IStackGridFactory>{
        public static IStackGrid<TStackItem> CreateNew<TStackItem>() where TStackItem : IUIObjectProvider {
            return Current?.CreateNew<TStackItem>();
        }
    }
    
}
