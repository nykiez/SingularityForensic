using System;
using System.Windows;
using static CDFCCultures.Managers.ManagerLocator;
using CDFCMessageBoxes.MessageBoxes;
using CDFCCultures.Helpers;
using CDFCControls.Controls;
using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.App;

namespace SingularityForensic.Casing.Views {
    /// <summary>
    /// Interaction logic for IFilePropertyWindow.xaml
    /// </summary>
    public partial class ShowCaseFilePropertyWindow : CorneredWindow {
        public CaseEvidence CFile { get; }
        public ShowCaseFilePropertyWindow(CaseEvidence cFile) {
            InitializeComponent();
            this.CFile = cFile ?? throw new ArgumentNullException();

            var comma = ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("Comma");
            var newLine = Environment.NewLine;

            //添加日期;
            DateAddedTxb.Text = $"{cFile.DateAdded.ToWDTimeString()}";
            
            //报告相关;
            ReportTxb.Text    = $"0";

            ObjectTitleTxb.Text = CFile.Name;
            CmtTxb.Text = CFile.Comments;
            
            //if (CFile is Singularity.Interfaces.IHaveData<IFile> fCFile ) {
            //    //大小;
            //    SizeTxb.Text = $"{ByteSizeToSizeConverter.StaticInstance.Convert(fCFile.Data.Size)}";

            //    DesTxb.Text += $"{FindResourceString("TotalCapacity")}{comma}{fCFile.Data.Size}{FindResourceString("Byte")}" +
            //     "=" + $"{ByteSizeToSizeConverter.StaticInstance.Convert(fCFile.Data.Size)}{Environment.NewLine}";

            //    if(fCFile.Data is Device device) {
            //        DesTxb.Text += $"{FindResourceString("BytesPerSector")}{comma}{device.SecSize}" + newLine;
            //        DesTxb.Text += $"{FindResourceString("SectorCount")}{comma}{device.Size / device.SecSize}" + newLine;

            //        DesTxb.Text += $"{FindResourceString("PartitioningStyle")}{comma}{device.PartsType}{Environment.NewLine}";

            //        device.Children.ForEach(p => {
            //            var part = p as Partition;
            //            DesTxb.Text += $"{Environment.NewLine}{part.Name}{Environment.NewLine}";
            //            DesTxb.Text += $"{FindResourceString("Sectors")}{part.StartLBA / device.SecSize}-{part.EndLBA / device.SecSize}{Environment.NewLine}";
            //        });
            //    }

            //    else if(fCFile.Data is Partition part) {
            //        DesTxb.Text += $"{FindResourceString("FileSystem")}{comma}{part.FSType}" + newLine;
            //        //DesTxb.Text += $"{FindResourceString("TotalCapacity")}{comma}{part.Size}" + newLine;

            //        if (part is AndroidPartition) {
            //            var adPart = part as AndroidPartition;
            //            DesTxb.Text += $"{FindResourceString("SectorSize")}{comma}{adPart.SectorSize}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("ClusterSize")}{comma}{adPart.ClusterSize}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("BlockSize")}{comma}{adPart.BlockSize}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("FreeClusters")}{comma}{adPart.FreeBlockCount} = {adPart.FreeBlockCount * 100 / adPart.BlockCount} %{FindResourceString("Free")}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("ClusterCount")}{comma}{adPart.BlockCount}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("INodeCount")}{comma}{adPart.INodeCount}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("FreeINodeCount")}{comma}{adPart.FreeInodeCount}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("BlockPergroup")}{comma}{adPart.BlockPergroup}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("INodePergroup")}{comma}{adPart.InodePerGroup}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("INodeSize")}{comma}{adPart.INodeSize}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("LastMountTime")}{comma}{adPart.LastMountTime.ToWDTimeString()}" + newLine;
            //            DesTxb.Text += $"{FindResourceString("LastWriteTime")}{comma}{adPart.LastWriteTime.ToWDTimeString()}";
            //        }
            //    }
                
            //}
            
            InterDTxb.Text = cFile.InterLabel;
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e) {
            if (CheckInput()) {
                CFile.Name = ObjectTitleTxb.Text;
                CFile.Comments = CmtTxb.Text;
                this.DialogResult = true;
                this.Close();
            }
            
        }

        private bool CheckInput() {
            if (string.IsNullOrEmpty(ObjectTitleTxb.Text)) {
                CDFCMessageBox.Show(FindResourceString("ObjectTitleOrNumberCannotBeNull"));
                return false;
            }
            return true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) {
            this.DialogResult = false;

            this.Close();
        }

        private void MetroWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Escape) {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}
