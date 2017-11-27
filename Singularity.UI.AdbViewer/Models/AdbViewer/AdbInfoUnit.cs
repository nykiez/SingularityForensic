using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cflab.DataTransport.Modules.Transport.Model;
using Prism.Mvvm;
using Singularity.UI.Info.Contracts;
using Singularity.UI.AdbViewer.Helpers;

namespace Singularity.UI.AdbViewer.Models.AdbViewer {
    public class AdbTreeUnit : BindableBase {
        public AdbTreeUnit(string name) {
            this.Name = name;
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

        public event EventHandler<bool?> CheckStateChanged;
        private bool? _isChecked = true;
        public bool? IsChecked {
            get {
                return _isChecked;
            }
            set {
                SetProperty(ref _isChecked, value);
                if (value != null) {
                    foreach (var node in Children) {
                        node.IsChecked = value;
                    }
                }
                CheckStateChanged?.Invoke(this, _isChecked);
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled {
            get {
                return _isEnabled; 
            }
            set {
                SetProperty(ref _isEnabled, value);
            }
        }

        //子项;
        public ObservableCollection<AdbTreeUnit> Children { get; set; } = new ObservableCollection<AdbTreeUnit>();
        private int _level;
        public int Level {
            get {
                return _level;
            }
            set {
                SetProperty(ref _level, value);
            }
        }

        //private string _tipWord;
        //public string TipWord {
        //    get {
        //        return _tipWord;
        //    }
        //    set {
        //        SetProperty(ref _tipWord, value);
        //    }
        //}
    }

    //Adb信息勾选项;
    public class AdbInfoTreeUnit<TNode>: AdbFileTreeUnit where TNode:IInfo{
        public AdbInfoTreeUnit(MInfoType tp):base(MInfoTypeHelper.GetInfoTypeWord(tp)) {
            this.MInfoType = tp;
        }
        public List<TNode> Infoes { get; } = new List<TNode>();

    }

    //所有文件列表节点;
    public class AdbAllFileTreeUnit : AdbFileTreeUnit {
        public AdbAllFileTreeUnit(string name) : base(name) {

        }
        public List<AnFile> Files { get; set; }
    }

    public abstract class AdbFileTreeUnit:AdbTreeUnit {
        public AdbFileTreeUnit(string name) : base(name) {
        }
        private double _process;
        public MInfoType MInfoType { get; protected set; }
        public double Process {
            get {
                return _process;
            }
            set {
                SetProperty(ref _process, value);
            }
        }
    }
   
    public class AdbBackUpFilesTreeUnit : AdbFileTreeUnit {
        public AdbBackUpFilesTreeUnit(string relPath = null):
            base(MInfoTypeHelper.GetInfoTypeWord(MInfoType.BackUp)) {
            this.Direct = relPath;
            this.Level = 1;
        }

        public string Direct { get; set; }

        public bool Succeed { get; set; }
    }
    
}
