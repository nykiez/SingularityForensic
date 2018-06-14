using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SingularityForensic.Contracts.Hex {
    //搜寻方法;
    public enum FindMethod {
        Text,                                                               //文字检索;
        Hex                                                                 //十六进制检索;
    }
    

    public interface IHexDataContext:IUIObjectProvider,IExtensible {
        bool ReadOnlyMode { get; set; }
        Stream Stream { get; set; }

        long SelectionStart { get; set; }
        long SelectionLength { get; set; }
        event EventHandler SelectionStateChanged;
        
        long Position { get; set; }
        long FocusPosition { get; set; }
        event EventHandler FocusPositionChanged;

        //失去焦点时触发;
        //event EventHandler LostFocus;

        ICollection<IBrushBlock> CustomBackgroundBlocks { get; }

        IBytesToCharEncoding BytesToCharEncoding { get; set; }
        /// <summary>
        /// 手动刷新自定义块内容,避免多次添加色块所带来的性能损耗;
        /// </summary>
        void UpdateCustomBackgroundBlocks();

        IEnumerable<ICommandItem> ContextCommands { get; }
        void AddContextCommand(ICommandItem command);
        void RemoveContextCommand(ICommandItem command);

        /// <summary>
        /// ToolTip项(键值型);
        /// </summary>
        /// <remarks>之所以会将ToolTip键值型与自定义类型分离,是因为若合并处理，将会导致过多的键值型ToolTip中含有大量的UI元素,导致内存占用过大.</remarks>
        ICollection<(long position, long size, IToolTipDataItem toolTipDataItem)> CustomDataToolTipItems { get; }
        
        /// <summary>
        /// ToolTip项(自定义型)
        /// </summary>
        ICollection<(long position, long size, IToolTipObjectItem toolTipObjectItem)> CustomObjectToolTipItems { get; }
        
    }

    
}
