using SingularityForensic.Contracts.Document;

namespace SingularityForensic.Document {
    /// <summary>
    /// 默认文档页;
    /// </summary>
    public class Document : DocumentBase, IDocument {
        private object _uiObject;
        public override object UIObject {
            get => _uiObject;

        }

        object IDocument.UIObject {
            get => _uiObject;
            set => SetProperty(ref _uiObject, value);
        }
    }
}
