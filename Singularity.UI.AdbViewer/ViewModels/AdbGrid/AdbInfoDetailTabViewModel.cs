using CDFCUIContracts.Abstracts;
using System;
using System.Text;
using static CDFCCultures.Managers.ManagerLocator;
using EventLogger;
using CDFCMessageBoxes.MessageBoxes;
using Cflab.DataTransport.Modules.Transport.Model;
using CDFC.Info.Infrastructure;
using Prism.Mvvm;
using CDFC.Info.Adb;

namespace Singularity.UI.AdbViewer.ViewModels.AdbGrid {
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

        public string Header => FindResourceString("AdbDetailInfo");
        
        public string InfoPropsText {
            get {
                if (AdbModel == null) {
                    return FindResourceString("AdbNoDetail");
                }
                var sb = new StringBuilder();
                try {
                    foreach (var prop in AdbModel.GetType().GetProperties()) {
                        Action normalAddAct = () => {
                            sb.AppendLine((FindResourceString($"Adb{InfoType}{prop.Name}")??prop.Name) +
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
                        sb.AppendLine($"{FindResourceString("AdbContactNumbers")}({contact.Numbers?.Count??0})");
                        contact?.Numbers?.ForEach(p => {
                            sb.AppendLine($"{FindResourceString("AdbContactNumberName")       }:{p.Name      }");
                            sb.AppendLine($"{FindResourceString("AdbContactNumberNumber")     }:{p.Number    }");
                            sb.AppendLine($"{FindResourceString("AdbContactNumberFullNumber") }:{p.FullNumber}");
                            sb.AppendLine($"{FindResourceString("AdbContactNumberLocation")   }:{p.Location  }");
                        });
                        sb.AppendLine();

                        sb.AppendLine($"{FindResourceString("AdbContactEmails")}({contact.Emails?.Count ?? 0})");
                        contact?.Emails?.ForEach(p => {
                            sb.Append($"{FindResourceString("AdbContactEmailName")   }:{p.Name}");
                            sb.Append($"{FindResourceString("AdbContactEmailAddress")}:{p.Address}");
                        });
                    }
                    return sb.ToString();
                }
                catch (Exception ex) {
                    Logger.WriteLine($"{nameof(AdbInfoDetailTabViewModel)}->{nameof(InfoPropsText)}:{ex.Message}");
                    RemainingMessageBox.Tell(ex.Message);
                    return string.Empty;
                }
            }
        }


    }
}
