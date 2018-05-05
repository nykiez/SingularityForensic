using CDFCUIContracts.Abstracts;
using System;
using System.Text;
using EventLogger;
using Cflab.DataTransport.Modules.Transport.Model;
using Prism.Mvvm;
using SingularityForensic.Contracts.Info;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Adb.ViewModels.AdbGrid {
    public class AdbInfoDetailTabViewModel : BindableBase,ITabModel {
        public MInfoType InfoType { get;
            set; }
        private IAdbModel _adbModel;
        public IAdbModel AdbModel {
            get {
                return _adbModel;
            }
            set {
                _adbModel = value;
                RaisePropertyChanged(nameof(InfoPropsText));
            }
        }

        public string Header => ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AdbDetailInfo");
        
        public string InfoPropsText {
            get {
                if (AdbModel == null) {
                    return  ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("AdbNoDetail");
                }
                var sb = new StringBuilder();
                try {
                    foreach (var prop in AdbModel.GetType().GetProperties()) {
                        Action normalAddAct = () => {
                            sb.AppendLine((LanguageService.FindResourceString($"Adb{InfoType}{prop.Name}")??prop.Name) +
                         $":{prop.GetValue(AdbModel)}");
                        };
                        if (prop.PropertyType == typeof(DateTime)
                            || prop.PropertyType == typeof(DateTime?)
                            || prop.PropertyType == typeof(string)
                            || prop.PropertyType == typeof(int)
                            || prop.PropertyType == typeof(TimeSpan))
                              {
                            normalAddAct();
                        }
                        
                    }
                    if (AdbModel is AdbContactModel) {
                        var contact = (AdbModel as AdbContactModel).Info as Contact;
                        sb.AppendLine();
                        sb.AppendLine($"{LanguageService.FindResourceString("AdbContactNumbers")}({contact.Numbers?.Count??0})");
                        contact?.Numbers?.ForEach(p => {
                            sb.AppendLine($"{LanguageService.FindResourceString("AdbContactNumberName")       }:{p.Name      }");
                            sb.AppendLine($"{LanguageService.FindResourceString("AdbContactNumberNumber")     }:{p.Number    }");
                            sb.AppendLine($"{LanguageService.FindResourceString("AdbContactNumberFullNumber") }:{p.FullNumber}");
                            sb.AppendLine($"{LanguageService.FindResourceString("AdbContactNumberLocation")   }:{p.Location  }");
                        });
                        sb.AppendLine();

                        sb.AppendLine($"{LanguageService.FindResourceString("AdbContactEmails")}({contact.Emails?.Count ?? 0})");
                        contact?.Emails?.ForEach(p => {
                            sb.Append($"{LanguageService.FindResourceString("AdbContactEmailName")   }:{p.Name}");
                            sb.Append($"{LanguageService.FindResourceString("AdbContactEmailAddress")}:{p.Address}");
                        });
                    }
                    return sb.ToString();
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(AdbInfoDetailTabViewModel)}->{nameof(InfoPropsText)}:{ex.Message}");
                    MsgBoxService.ShowError(ex.Message);
                    return string.Empty;
                }
            }
        }


    }
}
