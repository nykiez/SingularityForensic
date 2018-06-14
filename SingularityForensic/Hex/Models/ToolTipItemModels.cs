using Prism.Mvvm;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using SingularityForensic.Hex.ViewModels;
using System;

namespace SingularityForensic.Hex.Models
{
    public interface IToolTipItemModel:IUIObjectProvider
    {
        IToolTipItem ToolTipItem { get; set; }
    }

    class ToolTipDataItemModel :  IToolTipItemModel {
        private ToolTipDataItemViewModel _toolTipDataItemViewModel = new ToolTipDataItemViewModel();

        public string KeyName {
            get => _toolTipDataItemViewModel.KeyName;
            set => _toolTipDataItemViewModel.KeyName = value;
        }

        public string Value {
            get => _toolTipDataItemViewModel.Value;
            set => _toolTipDataItemViewModel.Value = value;
        }

        private object _uiObject;
        public object UIObject {
            get {
                if (_uiObject == null) {
                    _uiObject = ViewProvider.CreateView(Contracts.Hex.Constants.ToolTipDataItemView, _toolTipDataItemViewModel);
                }
                return _uiObject;
            }
        }

        private void SetToolTipDataItem(IToolTipDataItem dataItem) {
            _toolTipDataItem = dataItem;
            KeyName = dataItem?.KeyName;
            Value = dataItem?.Value;
        }

        private IToolTipDataItem _toolTipDataItem;
        public IToolTipItem ToolTipItem {
            get => _toolTipDataItem;
            set {
                if(value == null) {
                    SetToolTipDataItem(null);
                }
                if(!(value is IToolTipDataItem dataItem)) {
                    throw new InvalidOperationException($"{nameof(value)} should be a {nameof(IToolTipDataItem)} typed.");
                }
                SetToolTipDataItem(dataItem);
            }
        }
    }

    class ToolTipObjectItemModel : BindableBase, IToolTipItemModel {
        private IToolTipObjectItem _toolTipObjectItem;
        public IToolTipItem ToolTipItem {
            get => _toolTipObjectItem;
            set {
                if(value == null) {
                    SetToolTipObjectItem(null);
                }

                if (!(value is IToolTipObjectItem objectItem)) {
                    throw new InvalidOperationException($"{nameof(value)} should be a {nameof(IToolTipObjectItem)} typed.");
                }
                SetToolTipObjectItem(objectItem);
               
            }
        }

        private void SetToolTipObjectItem(IToolTipObjectItem objectItem) {
            _toolTipObjectItem = objectItem;
            RaisePropertyChanged(nameof(UIObject));
        }

        public object UIObject => _toolTipObjectItem?.UIObject;
    }

    static class ToolTipItemModelFactory {
        public static IToolTipItemModel CreateToolTipDataItemModel() => new ToolTipDataItemModel();
        public static IToolTipItemModel CreateToolTipObjectItemModel() => new ToolTipObjectItemModel();
    }


}
