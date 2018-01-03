using CDFCMessageBoxes.MessageBoxes;
using EventLogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WPFHexaEditor.Control.Abstracts;
using WPFHexaEditor.Core.Bytes;
using static CDFCCultures.Managers.ManagerLocator;
using CDFC.Parse.IO;
using Ookii.Dialogs.Wpf;
using System.Windows;
using SysIO = System.IO;
using Prism.Commands;
using CDFC.Util.IO;
using CDFCCultures.Helpers;

namespace Singularity.UI.Controls.ViewModels {
    public enum CodeLanguage {
        C,
        CSharp,
        Java,
        FSharp,
        VBNET
    }

    //十六进制流查看器视图模型
    public abstract partial class HexStreamEditorViewModel : HexEditorViewModel {
        public HexStreamEditorViewModel(Stream stream) {
            this.Stream = stream;
        }

        public override event EventHandler SubmitChangesRequired;
    }

    //十六进制流查看器视图模型的命令绑定项;
    public abstract partial class HexStreamEditorViewModel {

        private DelegateCommand copyToNewFileCommand;                              //拷贝至新文件命令;
        public DelegateCommand CopyToNewFileCommand {
            get {
                return copyToNewFileCommand ??
                    (copyToNewFileCommand = new DelegateCommand(
                        () => {
                            if (SelectionStop != -1 && SelectionStart != -1) {      //验证是否存在选中部分;
                                var dialog = new VistaSaveFileDialog();
                                if (dialog.ShowDialog() == true) {

                                    var proDialog = new ProgressMessageBox();
                                    proDialog.WindowTitle = "数据正在拷贝中";
                                    proDialog.DoWork += (sender, e) => {
                                        try {
                                            var fs = SysIO.File.Create(dialog.FileName);
                                            byte[] buffer = new byte[10485760];
                                            int read;
                                            long readSize = 0;
                                            Stream.Position = SelectionStart;
                                            var end = Math.Max(SelectionStart, SelectionStop);
                                            var start = Math.Min(SelectionStart, SelectionStop);
                                            using (var Ins = InterceptStream.CreateFromStream(Stream,start , end - start + 1)) {
                                                while ((read = Ins.Read(buffer, 0, buffer.Length)) != 0 && !proDialog.CancellationPending) {
                                                    fs.Write(buffer, 0, read);
                                                    readSize += read;
                                                    proDialog.ReportProgress((int)(readSize * 100 / Ins.Length));
                                                }
                                            }
                                            fs.Close();
                                        }
                                        catch {

                                        }
                                    };
                                    proDialog.RunWorkerCompleted += (sender, e) => {
                                        if (!e.Cancelled) {
                                            CDFCMessageBox.Show(FindResourceString("Finished"));
                                        }
                                    };

                                    proDialog.ShowDialog();
                                }
                            }
                            else {

                            }
                        },
                        () => Stream != null
                        ));
            }
        }

        private static readonly long maxCopyToClipBoardSize = 4294967296;          //最大粘贴大小为4GB;
        private DelegateCommand copyToClipBoardCommand;                            //拷贝至剪切板;
        public DelegateCommand CopyToClipBoardCommand {
            get {
                return copyToClipBoardCommand ??
                    (copyToClipBoardCommand = new DelegateCommand(
                        () => {
                            if (SelectionStart != -1 && SelectionStop != -1) {       //若需剪切数据大于4GB
                                if (SelectionStop - SelectionStart > maxCopyToClipBoardSize) {
                                    CDFCMessageBox.Show(FindResourceString("TooLargeCopySize"));
                                }
                                else {                               
                                    Stream.Position = SelectionStart;
                                    var start = Math.Min(SelectionStart, SelectionStop);
                                    var end = Math.Max(SelectionStart, SelectionStop);

                                    using (var ins = InterceptStream.CreateFromStream(Stream, start, end - start + 1)) { 
                                        StreamReader sr = new StreamReader(ins);
                                        try {
                                            Clipboard.SetDataObject(sr.ReadToEnd());
                                        }
                                        catch (Exception ex) {
                                            CDFCMessageBox.Show($"{ex.Message}");
                                        }
                                        finally {
                                            sr.Close();
                                        }
                                    }
                                }
                            }
                        },
                        () => Stream != null));
            }
        }

        private DelegateCommand copyHexToCBoardCommand;                            //拷贝十六进制到剪切板;
        public DelegateCommand CopyHexToCBoardCommand {
            get {
                return copyHexToCBoardCommand ??
                    (copyHexToCBoardCommand = new DelegateCommand(
                        () => {
                            if (SelectionStart != -1 && SelectionStop != -1) {       //若需剪切数据大于4GB
                                if (SelectionStop - SelectionStart > maxCopyToClipBoardSize) {
                                    CDFCMessageBox.Show(FindResourceString("TooLargeCopySize"));
                                }
                                else {
                                    Stream.Position = SelectionStart;
                                    var start = Math.Min(SelectionStart, SelectionStop);
                                    var end = Math.Max(SelectionStart, SelectionStop);

                                    using (var ins = InterceptStream.CreateFromStream(Stream, start, end - start + 1)) {
                                        try {
                                            var buffer = new byte[ins.Length];
                                            ins.Read(buffer, 0, (int)ins.Length);
                                            Clipboard.SetDataObject(ByteConverterHelper.ByteToHex(buffer));
                                        }
                                        catch (Exception ex) {
                                            CDFCMessageBox.Show($"{ex.Message}");
                                        }
                                    }
                                }
                            }
                        },
                        () => Stream != null
                        ));
            }
        }

        private DelegateCommand copyASCIIToCBoardCommand;                          //拷贝ASCII字符串至剪切板;
        public DelegateCommand CopyASCIIToCBoardCommand {
            get {
                return copyASCIIToCBoardCommand ??
                    (copyASCIIToCBoardCommand = new DelegateCommand(
                        () => {
                            if (SelectionStart != -1 && SelectionStop != -1) {       //若需剪切数据大于4GB
                                if (SelectionStop - SelectionStart > maxCopyToClipBoardSize) {
                                    CDFCMessageBox.Show(FindResourceString("TooLargeCopySize"));
                                }
                                else {
                                    Stream.Position = SelectionStart;
                                    var start = Math.Min(SelectionStart, SelectionStop);
                                    var end = Math.Max(SelectionStart, SelectionStop);

                                    using (var ins = InterceptStream.CreateFromStream(Stream, start, end - start + 1)) {
                                        try {
                                            var buffer = new byte[ins.Length];
                                            ins.Read(buffer, 0, (int)ins.Length);
                                            Clipboard.SetDataObject(ByteConverters.BytesToString(buffer));
                                        }
                                        catch (Exception ex) {
                                            CDFCMessageBox.Show($"{ex.Message}");
                                        }
                                    }
                                }
                            }
                        },
                        () => Stream != null
                        ));
            }
        }

        private DelegateCommand setAsStartCommand;                                 //设为起始选定块命令;
        public DelegateCommand SetAsStartCommand {
            get {
                return setAsStartCommand ??
                    (setAsStartCommand = new DelegateCommand(() => {
                        if (FocusPosition != -1) {
                            SelectionStart = FocusPosition;
                        }
                    }));
            }
        }

        private DelegateCommand setAsEndCommand;                                   //设为终止选定块命令;
        public DelegateCommand SetAsEndCommand {
            get {
                return setAsEndCommand ??
                    (setAsEndCommand = new DelegateCommand(() => {
                        if (FocusPosition != -1) {
                            SelectionStop = FocusPosition;
                        }
                    }));
            }
        }

        private DelegateCommand submitChangesCommand;                             //保存修改的内容命令;
        public DelegateCommand SubmitChangesCommand {
            get {
                return submitChangesCommand ??
                (submitChangesCommand = new DelegateCommand(() => {
                    SubmitChangesRequired?.Invoke(this, new EventArgs());
                }, () => Stream != null && Stream.CanWrite));
            }
        }

        
        private byte[] GetCopyData(long selectionStart, long selectionStop, bool copyChange) {
            //Validation
            if (selectionStop == -1 || selectionStop == -1) return new byte[0];

            
            //Variable
            long byteStartPosition = -1;
            List<byte> bufferList = new List<byte>();

            //Set start position
            byteStartPosition = Math.Min(selectionStart, selectionStop);

            //set position
            Stream.Position = byteStartPosition;

            var selectionLength = Math.Abs(selectionStart - selectionStop) + 1;
            if(selectionLength > maxCopyToClipBoardSize) {
                return new byte[0];
            }

            //Exclude byte deleted from copy
            if (!copyChange) {
                byte[] buffer = new byte[selectionLength];
                Stream.Read(buffer, 0, (int)selectionLength);
                return buffer;
            }
            else {


            }

            return bufferList.ToArray();
        }
        
        private DelegateCommand<CodeLanguage?> _copyAsProCodeCommand;
        public DelegateCommand<CodeLanguage?> CopyAsProCodeCommand =>
            _copyAsProCodeCommand ??
            (_copyAsProCodeCommand = new DelegateCommand<CodeLanguage?>(language => {
                //Variables
                byte[] buffer = GetCopyData(SelectionStart, SelectionStop, false);
                int i = 0;
                long lenght = 0;
                string delimiter = language == CodeLanguage.FSharp ? ";" : ",";

                StringBuilder sb = new StringBuilder();

                if (SelectionStop > SelectionStart)
                    lenght = SelectionStop - SelectionStart + 1;
                else
                    lenght = SelectionStart - SelectionStop + 1;

                
                sb.AppendLine();
                sb.AppendLine();

                switch (language) {
                    case CodeLanguage.CSharp:
                        sb.Append($"string sData =\"{ByteConverters.BytesToString(buffer)}\";");
                        sb.AppendLine();
                        sb.Append($"string sDataHex =\"{ByteConverters.StringToHex(ByteConverters.BytesToString(buffer))}\";");
                        sb.AppendLine();
                        sb.AppendLine();
                        sb.Append("byte[] rawData = {");
                        sb.AppendLine();
                        sb.Append("\t");
                        break;
                    case CodeLanguage.Java:
                        sb.Append($"String sData =\"{ByteConverters.BytesToString(buffer)}\";");
                        sb.AppendLine();
                        sb.Append($"String sDataHex =\"{ByteConverters.StringToHex(ByteConverters.BytesToString(buffer))}\";");
                        sb.AppendLine();
                        sb.AppendLine();
                        sb.Append("byte rawData[] = {");
                        sb.AppendLine();
                        sb.Append("\t");
                        break;
                    case CodeLanguage.C:
                        sb.Append($"char sData[] =\"{ByteConverters.BytesToString(buffer)}\";");
                        sb.AppendLine();
                        sb.Append($"char sDataHex[] =\"{ByteConverters.StringToHex(ByteConverters.BytesToString(buffer))}\";");
                        sb.AppendLine();
                        sb.AppendLine();
                        sb.Append($"unsigned char rawData[{lenght}]{{ ");
                        sb.AppendLine();
                        sb.Append("\t");
                        break;
                    case CodeLanguage.FSharp:
                        sb.Append($"let sData = @\"{ByteConverters.BytesToString(buffer)}\";");
                        sb.AppendLine();
                        sb.Append($"let sDataHex = @\"{ByteConverters.StringToHex(ByteConverters.BytesToString(buffer))}\";");
                        sb.AppendLine();
                        sb.AppendLine();
                        sb.Append("let bytes = [|");
                        sb.AppendLine();
                        sb.Append("    ");
                        break;
                    case CodeLanguage.VBNET:
                        sb.Append($"Dim sData as String =\"{ByteConverters.BytesToString(buffer)}\";");
                        sb.AppendLine();
                        sb.Append($"Dim sDataHex as String =\"{ByteConverters.StringToHex(ByteConverters.BytesToString(buffer))}\";");
                        sb.AppendLine();
                        sb.AppendLine();
                        sb.Append("Dim rawData As Byte() = { _");
                        sb.AppendLine();
                        sb.Append("\t");
                        break;
                }

                foreach (byte b in buffer) {
                    i++;
                    if (language == CodeLanguage.Java) sb.Append("(byte)");

                    if (language == CodeLanguage.VBNET)
                        sb.Append($"&H{ByteConverters.ByteToHex(b)}, ");
                    else
                        sb.Append($"0x{ByteConverters.ByteToHex(b)}{delimiter} ");

                    if (i == (language == CodeLanguage.Java ? 6 : 12)) {
                        i = 0;
                        if (language == CodeLanguage.VBNET) sb.Append("_");
                        sb.AppendLine();
                        if (language != CodeLanguage.FSharp)
                            sb.Append("\t");
                        else
                            sb.Append("    ");
                    }
                }
                if (language == CodeLanguage.VBNET) sb.Append("_");
                sb.AppendLine();
                if (language != CodeLanguage.FSharp)
                    sb.Append("};");
                else
                    sb.Append("|]");
                var da = new DataObject();
                da.SetText(sb.ToString(), TextDataFormat.Text);
                Clipboard.SetDataObject(da);
                
            }));
        
    }
    
    public abstract partial class HexStreamEditorViewModel {
        public bool IsFileSystemHex { get;protected set; }
    }
}
