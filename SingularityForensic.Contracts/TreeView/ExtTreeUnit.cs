﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Authentication;
using CDFCUIContracts.Commands;
using EventLogger;
using Prism.Mvvm;
using SingularityForensic.Contracts.Common;

namespace SingularityForensic.Contracts.TreeView {
    public sealed class TreeUnit : BindableBase {
        public TreeUnit(
            string typeGuid,
            object data = null) {
            
            this.TypeGuid = typeGuid;
            this.Data = data;
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

        //数据实体;
        public object Data { get;  }

        //子节点;
        public ObservableCollection<TreeUnit> Children { get; set; } = new ObservableCollection<TreeUnit>();

        //节点图标;
        private Uri _icon;
        public Uri Icon {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        //上下文菜单;
        public ObservableCollection<ICommandItem> ContextCommands { get; set; } = new ObservableCollection<ICommandItem>();
        
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
                Logger.WriteCallerLine(ex.Message);
                return false;
            }
        }
    }

}