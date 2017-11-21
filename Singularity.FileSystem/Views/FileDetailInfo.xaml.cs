using CDFC.Parse.Android.Contracts;
using CDFC.Parse.Contracts;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static CDFCCultures.Managers.ManagerLocator;

namespace Singularity.UI.FileSystem.Views {
    /// <summary>
    /// Interaction logic for FileDetailInfo.xaml
    /// </summary>
    public partial class FileDetailInfo : UserControl {
        public FileDetailInfo() {
            InitializeComponent();
        }

        
        public static readonly DependencyProperty FileProperty = DependencyProperty.Register("File", typeof(IFile), typeof(FileDetailInfo)
            , new PropertyMetadata(null, File_PropertyChanged));
        private static void File_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var ctrl = d as FileDetailInfo;
            var file = e.NewValue as IFile;
            if(ctrl != null && file != null) {
                var comma = FindResourceString("Comma");
                Action<object, StringBuilder, string[]> afa = (ob, sbr, ignored) => {
                    foreach (var prop in ob.GetType().GetFields()) {
                        if (!(ignored?.Contains(prop.Name) ?? false) && !prop.Name.ToLower().Contains("unknown")) {
                            var propName = FindResourceString(prop.Name);
                            if (string.IsNullOrEmpty(propName)) {
                                propName = prop.Name;
                            }
                            sbr.AppendLine($"{propName}{comma}{prop.GetValue(ob)??string.Empty.ToString()}");
                        }
                    }
                };

                var sb = new StringBuilder();
                sb.AppendLine($"{FindResourceString("BasicFileInfo")}");
                sb.AppendLine($"{FindResourceString("FileName")}{comma}{file.Name}");
                sb.AppendLine($"{FindResourceString("FileSize")}{comma}{file.Size}{FindResourceString("Byte")}");
                sb.AppendLine();

                if (file is IExt4Node) {
                    var dnTry = (file as IExt4Node).StDirEntry;
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
                    var inode = (file as IExt4Node).StExt4Inode;
                    if (inode != null) {
                        var stInode = inode.Value;
                        afa(stInode, sb, null);
                    }
                    ctrl.mainTxb.Text = sb.ToString();
                }
            }
        }
        //所表示的文件类型;
        public IFile File {
            get {
                return (IFile) GetValue(FileProperty);
            }
            set {
                SetValue(FileProperty, value);
            }
        }
        
    }
}
