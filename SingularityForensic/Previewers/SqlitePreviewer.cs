using System.Windows;
using SingularityForensic.Previewers.ViewModels;
using SingularityForensic.Contracts.Previewers;

namespace SingularityForensic.Previewers {
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

       
        public FrameworkElement View {
            get {
                if (view == null) {
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
