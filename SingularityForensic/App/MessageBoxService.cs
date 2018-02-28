using CDFCMessageBoxes.MessageBoxes;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SingularityForensic.App {
    [Export(typeof(IMessageBoxService))]
    class MessageBoxService : IMessageBoxService {
        public MessageBoxResult Show(string msg) => CDFCMessageBox.Show(msg);

        public void ShowError(string error) {
            CDFCMessageBox.Show(error);
        }

        public MessageBoxResult Show(string msgText, MessageBoxButton button) {
            return CDFCMessageBox.Show(msgText, button);
        }
        public MessageBoxResult Show(string msgText, string caption, MessageBoxButton button) {
            return CDFCMessageBox.Show(msgText, caption, button);
        }
    }
}
