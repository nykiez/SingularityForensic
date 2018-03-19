using System;
using System.Linq;
using System.Text;
using CDFC.Parse.Contracts;
using static CDFCCultures.Managers.ManagerLocator;
using CDFC.Parse.Modules.Contracts;
using CDFC.Util;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Android.FileSystem.Models {

    public class Ext4NodeDetailProvider : GenericStaticInstance<Ext4NodeDetailProvider>, IFileDetailInfoProvider {
        public bool CheckIsValidFile(IFilefile) => file is IExt4Node;

        public string GetAttachedInfo(IFilefile) {
            var comma = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("Comma");
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
