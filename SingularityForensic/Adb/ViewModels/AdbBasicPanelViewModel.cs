using System;
using System.Text;
using static CDFCCultures.Managers.ManagerLocator;
using EventLogger;
using Prism.Mvvm;
using Cflab.DataTransport.Modules.Transport.Model;
using CDFC.Info.Adb;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Adb.ViewModels {
    public class AdbBasicPanelViewModel:BindableBase {
        public AdbBasicPanelViewModel(AdbSingleInfoContainer<Basic,AdbInfoBasicModel> container) {
            this.Container = container;
        }
        public AdbSingleInfoContainer<Basic,AdbInfoBasicModel> Container { get; }

        /// <summary>
        /// SIM卡状态
        /// </summary>
        public int State => Container.Info.State;

        public string Imei => Container.Info.Imei;

        public string Imsi => Container.Info.Imsi;

        public string Release => Container.Info.Release;

        /// <summary>
        /// 运营商识别号
        /// </summary>
        public string Inet => Container.Info.Inet;

        /// <summary>
        /// SIM卡序列号
        /// </summary>
        public string Isim => Container.Info.Isim;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Number => Container.Info.Number;

        /// <summary>
        /// 版本号
        /// </summary>
        public string Model => Container.Info.Model;

        /// <summary>
        /// CPU ABI
        /// </summary>
        public string Abi => Container.Info.Abi;

        /// <summary>
        /// 时区
        /// </summary>
        public int Zone => Container.Info.Zone;

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand => Container.Info.Brand;

        /// <summary>
        /// 处理器型号
        /// </summary>
        public string Board => Container.Info.Board;


        public string Hardware => Container.Info.Hardware;

        public string Device => Container.Info.Device;

        /// <summary>
        /// 前台显示的基本信息;
        /// </summary>
        public string BasicText {
            get {
                try {
                    var sb = new StringBuilder();
                    foreach (var prop in Container.Info.GetType().GetProperties()) {
                        sb.AppendLine(LanguageService.FindResourceString($"AdbBasic{prop.Name}")+
                            $":{ prop.GetValue(Container.Info)}");
                    }
                    return sb.ToString();
                }
                catch(Exception ex) {
                    Logger.WriteLine($"{nameof(AdbBasicPanelViewModel)}->{nameof(BasicText)}:{ex.Message}");
                    return string.Empty;
                }
            }
        }
    }
}
