using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Previewers;
using SingularityForensic.NTFS.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.NTFS.USN {
    public class UsnJrnlPreviewer : IPreviewer {
        public UsnJrnlPreviewer(Stream stream) {
            this._stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _vm = new UsnJrnlPreviewerViewModel(stream);
            UIObject = ViewProvider.CreateView(Constants.UsnJrnlPreviewerView, _vm);
        }
        
        private UsnJrnlPreviewerViewModel _vm;
        public object UIObject { get; }
        private readonly Stream _stream;

        public void Dispose() {
            _vm.Dispose();
        }

#if DEBUG
        ~UsnJrnlPreviewer() {

        }
#endif
    }
}
