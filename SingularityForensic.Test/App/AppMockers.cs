using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SingularityForensic.Contracts.App;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

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
                    mocker.Setup(p => p.GetSaveFilePath(It.IsAny<string>())).Returns(() => SaveFileName);
                    mocker.Setup(p => p.CreateDoubleLoadingDialog()).Returns(() => new DoubleLoadingDialogMocker());
                    mocker.Setup(p => p.CreateLoadingDialog()).Returns(() => new LoadingDialogMocker());
                    _dialogMocker = mocker.Object;
                }

                return _dialogMocker;
            }
        }

        private static List<(string key, string value)> lanList = new List<(string key, string value)>();
        private static ILanguageDict _languageDictObjectMocker;
        public static ILanguageDict LanguageDictObjectMocker {
            get {
                if(_languageDictObjectMocker == null) {
                    var mocker = new Mock<ILanguageDict>();
                    mocker.Setup(p => p.AddMergedDictionaryFromPath(It.IsAny<string>())).Callback<string>(path => {
                        try {
                            var doc = XDocument.Load(path);
                            foreach (var elem in doc.Root.Elements()) {
                                //var s = elem.Attribute("sys:")
                            } 
                        }
                        catch(Exception ex) {
                            Assert.Fail();
                        }
                    });
                    _languageDictObjectMocker = mocker.Object;
                }

                return _languageDictObjectMocker;
            }
        }

        private static ILocalExplorerService _localExplorerServiceMocker;
        public static ILocalExplorerService LocalExplorerServiceMocker {
            get {
                if(_localExplorerServiceMocker == null) {
                    var mocker = new Mock<ILocalExplorerService>();
                    mocker.Setup(p => p.OpenFolderAndSelectFile(It.IsAny<string>())).Callback<string>(p => {
                        Trace.WriteLine($"We are gonna select file : {p}");
                    });

                    mocker.Setup(p => p.OpenFile(It.IsAny<string>())).Callback<string>(p => {
                        Trace.WriteLine($"We are gonna open file : {p}");
                    });
                }
                return _localExplorerServiceMocker;
            }
        }
        
        internal static string OpenFileName { get; set; }
        internal static string SaveFileName { get; set; }
    }
}
