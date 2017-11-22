using System.Windows;
using Singularity.Previewers.ViewModels;
using Singularity.Interfaces;

namespace Singularity.Previewers {
    /// <summary>
    /// sqlite数据库预览器;
    /// </summary>
    public class SqlitePreviewer : IPreviewer {
        public SqlitePreviewer(string fileName) {
            vm = new SqlitePreviewerModel(fileName);
        }
        private SqlitePreviewerModel vm;
        public object DataContext {
            get {
                return vm;
            }
        }

        private Views.SqlitePreviewer view;
        public UIElement View {
            get {
                if(view == null) {
                    view = new Views.SqlitePreviewer();
                    view.DataContext = DataContext;
                }
                return view;
            }
        }

        public void Dispose() {
            vm?.Close();
        }
    }
}
