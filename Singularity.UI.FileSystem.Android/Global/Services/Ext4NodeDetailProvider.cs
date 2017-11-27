﻿using Singularity.UI.FileSystem.Interfaces;
using System;
using System.Linq;
using System.Text;
using CDFC.Parse.Contracts;
using static CDFCCultures.Managers.ManagerLocator;
using CDFC.Parse.Android.Contracts;

namespace Singularity.UI.FileSystem.Android.Global.Services {
    public class Ext4NodeDetailProvider : IFileDetailInfoProvider {
        public string GetAttachedInfo(IFile file) {
            var comma = FindResourceString("Comma");
            Action<object, StringBuilder, string[]> afa = (ob, sbr, ignored) => {
                foreach (var prop in ob.GetType().GetFields()) {
                    if (!(ignored?.Contains(prop.Name) ?? false) && !prop.Name.ToLower().Contains("unknown")) {
                        var propName = FindResourceString(prop.Name);
                        if (string.IsNullOrEmpty(propName)) {
                            propName = prop.Name;
                        }
                        sbr.AppendLine($"{propName}{comma}{prop.GetValue(ob) ?? string.Empty.ToString()}");
                    }
                }
            };
            if (file is IExt4Node ext4Node) {
                var sb = new StringBuilder();
                var dnTry = ext4Node.StDirEntry;
                if (dnTry != null) {
                    var stDirEntry = dnTry.Value;
                    afa(stDirEntry, sb, new string[] { nameof(stDirEntry.Pre), nameof(stDirEntry.Next), nameof(stDirEntry.DirInfo) });
                    sb.AppendLine();
                }
                var ext4Entry = (file as IExt4Node).StExt4DirEntry;
                if (ext4Entry != null) {
                    var stE4Ety = ext4Entry.Value;
                    afa(stE4Ety, sb, null);
                    sb.AppendLine();
                }
                var inode = ext4Node.StExt4Inode;
                if (inode != null) {
                    var stInode = inode.Value;
                    afa(stInode, sb, null);
                }
                return sb.ToString();
                //ctrl.mainTxb.Text = sb.ToString();
            }
            return string.Empty;
        }
    }
}
