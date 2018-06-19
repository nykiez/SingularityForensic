using SingularityForensic.Contracts.App;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace SingularityForensic.App {
    [Export(typeof(IMouseService))]
    class MouseServiceImpl : IMouseService {
        public Contracts.App.Cursor AppCursor {
            get {
                if(Application.Current.MainWindow.Cursor == Cursors.Wait) {
                    return Contracts.App.Cursor.Loading;
                }
                //var s = Application.Current.Dispatcher.Thread.ThreadState == System.Threading.ThreadState.Running;
                return Contracts.App.Cursor.Normal;
            }
            set {
                if(value == Contracts.App.Cursor.Loading) {
                    Application.Current.MainWindow.Cursor = Cursors.Wait;
                }
                else {
                    Application.Current.MainWindow.Cursor = Cursors.Arrow;
                }
            }
        }

        //private static FromCursorToAppCursor()

    }
}
