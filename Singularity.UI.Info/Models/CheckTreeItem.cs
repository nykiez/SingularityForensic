using Prism.Mvvm;
using Singularity.Contracts.Case;
using System;
using System.Collections.ObjectModel;

namespace Singularity.UI.Info.Models {
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
    
    public class CheckGroupTreeItem<TCaseFile> : CheckGroupTreeItem where TCaseFile : ICaseEvidence {
        public CheckGroupTreeItem(string name):base(name) {

        }
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
    }

    //选项;
    public abstract class CheckItemTreeItem<TCaseEvidence> :CheckItemTreeItem  where TCaseEvidence:ICaseEvidence {
        public CheckItemTreeItem(CheckGroupTreeItem group) : base(group) {

        }

        public virtual void Init(TCaseEvidence adcFile) {
            CaseFile = adcFile;
            Pro = 0;
        }
        
        public ObservableCollection<CheckItemTreeItem> Children { get; set; }

        public int Level { get; set; } = 1;
        
        public TCaseEvidence CaseFile { get; protected set; }

        /// <summary>
        /// 由于此处导出的TreeItem实例是唯一的，无法重新构造实例,对于多次取证过程，
        /// 必须处理多次的优先，及之前尚未完成的任务完成后;
        /// 将不会对于更高的优先级产生任何影响;
        /// </summary>
        public override void Free() {
            CaseFile = default(TCaseEvidence);
        }
    }
}
