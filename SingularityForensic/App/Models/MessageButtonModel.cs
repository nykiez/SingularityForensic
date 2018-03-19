using CDFCCultures.Managers;
using System.Windows;
using System;
using EventLogger;
using CDFCMessageBoxes.Commands;

namespace SingularityForensic.App.Models {
    
    public partial class MessageButtonModel {
        public static string LangDic {
            get {
                if(!string.IsNullOrEmpty( LanguageHelper.Language)) {
                    return $"/CDFCMessageBoxes;component/Languages/{LanguageHelper.Language}/CDFCMessageBoxes.xaml";
                }
                return $"/CDFCMessageBoxes;component/Languages/{LanguageHelper.DefaultLanguage}/CDFCMessageBoxes.xaml";
            }
        }
        private static ResourceDictionary _staticRes;
        public static ResourceDictionary StaticRes {
            get {
                if(_staticRes == null) {
                    _staticRes = new ResourceDictionary();
                    try {
                        _staticRes.Source = new Uri(LangDic,UriKind.Relative);
                    }
                    catch(Exception ex) {
                        Logger.WriteLine($"{nameof(MessageButtonModel)}->{nameof(StaticRes)}:{ex.Message}");
                    }
                }
                return _staticRes;
            }
        }

        private MessageButtonModel(string btnWord, MessageBoxResult result) {
            this.Result = result;
            this.BtnWord = btnWord;
        }
        public static MessageButtonModel OK {
            get {
                return new MessageButtonModel(StaticRes["OK"] as string, MessageBoxResult.OK);
            }
        }
        public static MessageButtonModel YES {
            get {
                return new MessageButtonModel(StaticRes["Yes"] as string,MessageBoxResult.Yes);
            }
        }
        public static MessageButtonModel NO {
            get {
                return new MessageButtonModel(StaticRes["Cancel"] as string,MessageBoxResult.No);
            }
        }
        public static MessageButtonModel Cancel {
            get {
                return new MessageButtonModel(StaticRes["Cancel"] as string, MessageBoxResult.Cancel);
            }
        }
    }
    public partial class MessageButtonModel  {
        public RelayCommand Command {
            get;
            set;
        }
        public string BtnWord { get;set; }
        public MessageBoxResult Result { get; }
    }
    
}
