using Moq;
using SingularityForensic.App;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Test.App {
    static class AppMockers {
        private static IMessageBoxService _msgBoxMocker;
        public static IMessageBoxService MsgBoxMocker {
            get {
                if (_msgBoxMocker == null) {
                    var msgBoxService = new Mock<IMessageBoxService>();
                    msgBoxService.Setup(p => p.Show(It.IsAny<string>())).Returns(MessageBoxResult.OK);
                    msgBoxService.Setup(p => p.Show(It.IsAny<string>(), It.IsAny<MessageBoxButton>())).Returns(MessageBoxResult.OK);
                    msgBoxService.Setup(p => p.Show(It.IsAny<string>(), MessageBoxButton.YesNo)).Returns(MessageBoxResult.Yes);
                    _msgBoxMocker = msgBoxService.Object;
                }
                return _msgBoxMocker;
            }
        }

        private static ILanguageService _languageMocker;
        public static ILanguageService LanguageMocker {
            get {
                if (_languageMocker == null) {
                    var languageService = new Mock<ILanguageService>();
                    languageService.Setup(p => p.FindResourceString(It.IsAny<string>())).Returns<string>(p => p);
                    _languageMocker = languageService.Object;
                }
                return _languageMocker;
            }
        }

        private static IThreadInvoker _threadInvokerMocker;
        public static IThreadInvoker ThreadInvokerMocker {
            get {
                if(_threadInvokerMocker == null) {
                    var threadInvoker = new Mock<IThreadInvoker>();
                    threadInvoker.Setup(p => p.BackInvoke(It.IsAny<Action>())).Callback<Action>(act => act.Invoke());
                    threadInvoker.Setup(p => p.UIInvoke(It.IsAny<Action>())).Callback<Action>(act => act.Invoke());
                    _threadInvokerMocker = threadInvoker.Object;
                }
                return _threadInvokerMocker;
            }
        }

        private static IDialogService _dialogMocker;
        public static IDialogService DialogMocker {
            get {
                if(_dialogMocker == null) {
                    var mocker = new Mock<IDialogService>();
                    mocker.Setup(p => p.OpenFile()).Returns(() => OpenFileName);
                    mocker.Setup(p => p.OpenFile(It.IsAny<string>())).Returns(() => OpenFileName);
                    mocker.Setup(p => p.SaveFile()).Returns(() => SaveFileName);
                    mocker.Setup(p => p.CreateDoubleLoadingDialog()).Returns(() => new DoubleLoadingDialogMocker());
                    mocker.Setup(p => p.CreateLoadingDialog()).Returns(() => new LoadingDialogMocker());
                    _dialogMocker = mocker.Object;
                }

                return _dialogMocker;
            }
        }

        private static ILanguageDictObject _languageDictObjectMocker;
        public static ILanguageDictObject LanguageDictObjectMocker {
            get {
                if(_languageDictObjectMocker == null) {
                    var mocker = new Mock<ILanguageDictObject>();
                    var resource = new ResourceDictionaryEx();
                    mocker.Setup(p => p.LanguageDict).Returns(resource);
                    _languageDictObjectMocker = mocker.Object;
                }

                return _languageDictObjectMocker;
            }
        }
        

        internal static string OpenFileName { get; set; }
        internal static string SaveFileName { get; set; }
    }
}
