using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.App {
    [Export(typeof(IClipBoardService))]
    class ClipBoardServiceImpl : IClipBoardService {
        public void Clear() => Clipboard.Clear();

        public object GetDataObject(Type format) {
            return Clipboard.GetDataObject();
        }

        public string GetText() => Clipboard.GetText();
        
        public void SetDataObject(object data) => Clipboard.SetDataObject(data);

        public void SetText(string text) => Clipboard.SetText(text);
    }
}
