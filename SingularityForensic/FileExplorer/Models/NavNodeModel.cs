﻿using Prism.Commands;
using SingularityForensic.Contracts.FileSystem;
using System;

namespace SingularityForensic.FileExplorer.Models {
    //路径节点;
    public class NavNodeModel {
        public NavNodeModel(IFile file) {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            this.File = file;
        }
        public IFile File { get; private set; }

        public string Name {
            get {
                return File.Name;
            }
        }

        //选择节点跳转的命令;
        private DelegateCommand escapeToCommand;
        public DelegateCommand EscapeToCommand {
            get {
                return escapeToCommand ??
                    (escapeToCommand = new DelegateCommand(() => {
                        EscapeRequired?.Invoke(this, this.File);
                    }));
            }
        }
        //跳转事件;
        public event EventHandler<IFile> EscapeRequired;
    }
}
