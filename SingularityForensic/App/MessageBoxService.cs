using CDFCMessageBoxes.MessageBoxes;
using SingularityForensic.Contracts.App;
using System.ComponentModel.Composition;

namespace SingularityForensic.App {
    [Export(typeof(IMessageBoxService))]
    class MessageBoxService : IMessageBoxService {
        //从Windows.Result转为契约Result;
        private static MessageBoxResult ConvertFromWindowsMsgResToLocalRes(System.Windows.MessageBoxResult res) {
            switch (res) {
                case System.Windows.MessageBoxResult.None:
                    return MessageBoxResult.None;
                case System.Windows.MessageBoxResult.OK:
                    return MessageBoxResult.OK;
                case System.Windows.MessageBoxResult.Cancel:
                    return MessageBoxResult.Cancel;
                case System.Windows.MessageBoxResult.Yes:
                    return MessageBoxResult.Yes;
                case System.Windows.MessageBoxResult.No:
                    return MessageBoxResult.No;
                default:
                    return MessageBoxResult.None;
            }
        }

        //从契约Button转为WindowsButton;
        private static System.Windows.MessageBoxButton ConvertFromLocalResToWindowsMsgBtn(MessageBoxButton btn) {
            switch (btn) {
                case MessageBoxButton.OK:
                    return System.Windows.MessageBoxButton.OK;
                case MessageBoxButton.OKCancel:
                    return System.Windows.MessageBoxButton.OKCancel;
                case MessageBoxButton.YesNoCancel:
                    return System.Windows.MessageBoxButton.YesNoCancel;
                case MessageBoxButton.YesNo:
                    return System.Windows.MessageBoxButton.YesNo;
                default:
                    return System.Windows.MessageBoxButton.OK;
            }
        }

        public MessageBoxResult Show(string msg) {
            var res = CDFCMessageBox.Show(msg);
            return ConvertFromWindowsMsgResToLocalRes(res);
        }

        public void ShowError(string error) {
            CDFCMessageBox.Show(error);
        }

        public MessageBoxResult Show(string msgText, MessageBoxButton button) {
            var res = CDFCMessageBox.Show(msgText, ConvertFromLocalResToWindowsMsgBtn(button));
            return ConvertFromWindowsMsgResToLocalRes(res);
        }

        public MessageBoxResult Show(string msgText, string caption, MessageBoxButton button) {
            var res = CDFCMessageBox.Show(msgText, caption,ConvertFromLocalResToWindowsMsgBtn(button));
            return ConvertFromWindowsMsgResToLocalRes(res);
        }
    }
}
