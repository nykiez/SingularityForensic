using Prism.Mvvm;
using SingularityForensic.Contracts.Case;
using System;
using System.Collections.ObjectModel;

namespace SingularityForensic.Info.Models {
    //选项组;
    public class CheckGroupTreeItem : BindableBase  {
        public CheckGroupTreeItem(string name) {
            this.Name = name;
        }

        private bool? _isChecked = false;
        public bool? IsChecked {
            get => _isChecked;
            set {
                if (value != null) {
                    foreach (var child in Children) {
                        child.IsChecked = value.Value;
                    }
                }
                SetProperty(ref _isChecked, value);
            }
        }

        private string _name;
        public string Name {
            get {
                return _name;
            }
            set {
                SetProperty(ref _name, value);
            }
        }

        private int _level = 0;
        public int Level {
            get {
                return _level;
            }
            set {
                SetProperty(ref _level, value);
            }
        }

        public bool IsEnabled { get; set; } = true;

        private string _tipWord;
        public string TipWord {
            get {
                return _tipWord;
            }
            set {
                SetProperty(ref _tipWord, value);
            }
        }

        public ObservableCollection<CheckItemTreeItem> Children { get; set; } = new ObservableCollection<CheckItemTreeItem>();
    }
    
    

    //选项;
    public abstract class CheckItemTreeItem : BindableBase {
        public CheckItemTreeItem(CheckGroupTreeItem group) {
            this.Group = group;
        }
        private bool _isChecked;
        public bool IsChecked {
            get => _isChecked;
            set {
                if (IsReadOnly) {
                    return;
                }
                var preVal = _isChecked;
                SetProperty(ref _isChecked, value);
                if (preVal != value) {
                    CheckStateChanged?.Invoke(this, value);
                }
            }
        }

        private bool _isReadOnly;
        public bool IsReadOnly {
            set => _isReadOnly = value;
            get => _isReadOnly;
        }

        public string Name { get; set; }

        private int _pro;
        public int Pro {
            get => _pro;
            set => SetProperty(ref _pro, value);
        }

        public event EventHandler<bool> CheckStateChanged;
        public CheckGroupTreeItem Group { get; }

        /// <summary>
        /// 开始取证
        /// </summary>
        /// <param name="isCancel">是否取消;</param>
        public abstract void StartForensic(Func<bool> isCancel);

        /// <summary>
        /// 装载(保存,显示)
        /// </summary>
        public abstract void Setup();
        
        /// <summary>
        /// 释放;
        /// </summary>
        public abstract void Free();

        public CaseEvidence CaseFile { get; protected set; }
    }

   
}
