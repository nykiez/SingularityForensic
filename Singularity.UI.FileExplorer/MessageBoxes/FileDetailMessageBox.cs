using CDFC.Parse.Contracts;
using Singularity.UI.MessageBoxes.Windows;
using System;
using System.Linq;
using System.Text;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.MessageBoxes.MessageBoxes {
    //显示文件详细的讯息;
    public class FileDetailMessageBox {
        /// <summary>
        /// 显示对象;
        /// </summary>
        /// <param name="file"></param>
        public static void Show(IFile file) {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            var comma = FindResourceString("Comma");
            Action<object, StringBuilder,string[]> afa = (ob, sbr ,ignored) => {
                foreach (var prop in ob.GetType().GetFields()) {
                    if (!(ignored?.Contains(prop.Name) ?? false) && !prop.Name.ToLower().Contains("unknown")) {
                        var propName = FindResourceString(prop.Name);
                        if (string.IsNullOrEmpty(propName)) {
                            propName = prop.Name;
                        }
                        sbr.AppendLine($"{propName}{comma}{prop.GetValue(ob).ToString()}");
                    }
                }
            };
            
            var sb = new StringBuilder();
            sb.AppendLine($"{FindResourceString("BasicFileInfo")}");
            sb.AppendLine($"{FindResourceString("FileName")}{comma}{file.Name}");
            sb.AppendLine($"{FindResourceString("FileSize")}{comma}{file.Size}{FindResourceString("Byte")}");
            sb.AppendLine();

            //if(file is IExt4Node) {
            //    var dnTry = (file as IExt4Node).StDirEntry;
            //    if(dnTry != null) {
            //        var stDirEntry = dnTry.Value;
            //        afa(stDirEntry, sb, new string[] { nameof(stDirEntry.Pre), nameof(stDirEntry.Next),nameof(stDirEntry.DirInfo) });
            //        sb.AppendLine();
            //    }
            //    var ext4Entry = (file as IExt4Node).StExt4DirEntry;
            //    if(ext4Entry != null) {
            //        var stE4Ety = ext4Entry.Value;
            //        afa(stE4Ety, sb, null);
            //    }
            //    var inode = (file as IExt4Node).StExt4Inode;
            //    if(inode != null) {
            //        var stInode = inode.Value;
            //        afa(stInode, sb, null);
            //    }
                
            //}


            var window = new FileDetailWindow();
            window.LoadString(sb.ToString());

            window.Show();
        }
        
    }
}
