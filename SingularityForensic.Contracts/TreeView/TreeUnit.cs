using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Authentication;
using EventLogger;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.TreeView {
    public sealed class TreeUnit : ExtensibleBindableBase {
        public TreeUnit(string typeGuid) {
            
            this.TypeGuid = typeGuid;
        }
        
        //类型GUID;
        public string TypeGuid { get; }

        public TreeUnit Parent { get; private set; }

        //节点文字;
        public string Label { get; set; }

        private int? level;
        //节点级别;
        public int Level {
            get {
                if (level == null) {
                    level = 0;
                    var _node = Parent;
                    while (_node != null) {
                        _node = _node.Parent;
                        level++;
                    }
                }
                return level.Value;
            }
        }
        
        //子节点;
        public ObservableCollection<TreeUnit> Children { get; set; } = new ObservableCollection<TreeUnit>();

        //节点图标;
        private Uri _icon;
        public Uri Icon {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        //上下文命令菜单;
        public ObservableCollection<CommandItem> ContextCommands { get; set; } = new ObservableCollection<CommandItem>();

        public void AddCommandItem(CommandItem commandItem) {
            if(commandItem == null) {
                throw new ArgumentNullException(nameof(commandItem));
            }

            var index = 0;
            
            foreach (var ci in ContextCommands) {
                if(ci.Sort > commandItem.Sort) {
                    break;
                }
                index++;
            }

            ContextCommands.Insert(index, commandItem);
        }

        public bool MoveToUnit(TreeUnit newParent) {
            if (newParent == null) {
                throw new ArgumentNullException(nameof(newParent));
            }
            if (newParent == Parent) {
                return true;
            }

            try {
                if (newParent.Children.Contains(this)) {
                    throw new Exception("Parent already contains this unit!");
                }

                newParent.Children.Add(this);
                this.Parent = newParent;
                return true;
            }
            catch (Exception ex) {
                LoggerService.Current?.WriteCallerLine(ex.Message);
                return false;
            }
        }

        
    }

}
