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
    

    public interface IHexDataContext:IUIObjectProvider,IInstanceExtensible {
        bool ReadOnlyMode { get; set; }
        Stream Stream { get; set; }

        long SelectionStart { get; set; }
        long SelectionLength { get; set; }
        event EventHandler SelectionStateChanged;

        long Position { get; set; }
        long FocusPosition { get; set; }
        ICollection<(long index, long length, Brush background)> CustomBackgroundBlocks { get; }

        IEnumerable<ICommandItem> ContextCommands { get; }
        void AddContextCommand(ICommandItem command);
        void RemoveContextCommand(ICommandItem command);

        ICollection<(long position, long size, string key, string value)> CustomDataToolTipItems { get; }
        ICollection<(long position, long size, IToolTipObjectItem toolTipObjectItem)> CustomObjectToolTipItems { get; }
    }

    
}
