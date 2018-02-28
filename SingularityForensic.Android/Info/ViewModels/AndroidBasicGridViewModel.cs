using Prism.Mvvm;
using SingularityForensic.Contracts.Info;
using SingularityForensic.Info.Models;
using System.Collections.Generic;
using System.Text;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Android.Info.ViewModels {
    public class AndroidGridViewModel<TDbModel>:BindableBase where TDbModel: ForensicInfoDbModel {
        public AndroidGridViewModel(IEnumerable<TDbModel> dbModels) {
            DataGridViewModel = new AndroidDataGridViewModel<TDbModel>(dbModels);
            DataGridViewModel.SelectedModelChanged += (sender,e) => {
                if(e == null) {
                    BasicText = string.Empty;
                    return;
                }

                try {
                    var sb = new StringBuilder();
                    foreach (var prop in e.GetType().GetProperties()) {
                        if (prop.PropertyType == typeof(long)
                        || prop.PropertyType == typeof(byte[])) {
                            continue;
                        }
                        else if(prop.Name == "sequence_name" 
                        || prop.Name == "collect_target_id") {
                            continue;
                        }

                        var val = prop.GetValue(e);
                        sb.Append($"{FindResourceString(prop.Name)}:");
                        if (val is IEnumerable<object> itr) {
                            sb.AppendLine();
                            foreach (var item in itr) {
                                foreach (var itemProp in item.GetType().GetProperties()) {
                                    sb.AppendLine($"\t{itemProp.GetValue(item)}");
                                }
                                sb.AppendLine();
                            }
                        }
                        else if (prop.PropertyType == typeof(FromWhom)) {
                            var fromWhom = (FromWhom)val;
                            switch (fromWhom) {
                                case FromWhom.Self:
                                    sb.AppendLine("发送");
                                    break;
                                case FromWhom.Unknown:
                                    sb.AppendLine("未发送");
                                    break;
                                default:
                                    sb.AppendLine("接收");
                                    break;
                            }
                        }
                        else if (prop.Name == "mail_view_status") {
                            var sta = val.ToString();
                            switch (sta) {
                                case "0":
                                    sb.AppendLine("已读");
                                    break;
                                case "1":
                                    sb.AppendLine("未读");
                                    break;
                                default:
                                    sb.AppendLine("其它");
                                    break;
                            }
                        }
                        else if(prop.Name == "delete_status" && prop.PropertyType == typeof(long?)) {
                            var sta = val as long?;
                            if(sta == null) {
                                sb.AppendLine("正常");
                            }
                            else if(sta == 1) {
                                sb.AppendLine("删除");
                            }
                        }
                        else {
                            sb.AppendLine($"{ val}");
                        }
                    }
                    BasicText = sb.ToString();
                }
                catch {

                }
                
            };
        }
        public AndroidDataGridViewModel<TDbModel> DataGridViewModel { get; set; }
        private string _basicText;
        public string BasicText {
            set => SetProperty(ref _basicText, value);
            get => _basicText;
        }
    }
}
