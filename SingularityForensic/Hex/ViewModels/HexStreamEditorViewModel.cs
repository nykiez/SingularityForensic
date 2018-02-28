using CDFC.Util.IO;
using CDFCCultures.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfHexaEditor.Core.Bytes;

namespace SingularityForensic.Hex.ViewModels {
    
    public abstract partial class HexStreamEditorViewModel : BindableBase {
        public HexStreamEditorViewModel() {
            InitializeToolTips();
        }
        private bool readOnlyMode;
        public bool ReadOnlyMode {
            get {
                if (Stream != null) {
                    return !Stream.CanWrite;
                }
                return false;
            }
        }

        private Stream stream;                      //所描述的流对象;
        public Stream Stream {
            get {
                return stream;
            }
            set {
                SetProperty(ref stream, value);
                RaisePropertyChanged(nameof(ReadOnlyMode));
            }
        }

        private long selectionStart = -1L;           //选择起始位置;
        public long SelectionStart {
            get {
                return selectionStart;
            }
            set {
                SetProperty(ref selectionStart, value);
            }
        }

        private long selectionStop = -1L;                   //控制选定终止处;
        public long SelectionStop {
            get {
                return selectionStop;
            }
            set {
                SetProperty(ref selectionStop, value);
            }
        }

        private long position = 0;
        public long Position {                      //当前位置;
            get {
                return position;
            }
            set {
                SetProperty(ref position, value);
            }
        }

        private long _focusPosition = -1;
        public long FocusPosition {
            get {
                return _focusPosition;
            }
            set {
                SetProperty(ref _focusPosition, value);
            }
        }
        
    }

    /// <summary>
    /// ToolTip
    /// </summary>
    public abstract partial class HexStreamEditorViewModel {
        public class ToolTipItemModel : BindableBase {
            private string _keyName;
            public string KeyName {
                get => _keyName;
                set => SetProperty(ref _keyName, value);
            }


            private string _value;
            public string Value {
                get => _value;
                set => SetProperty(ref _value, value);
            }

        }

        private void InitializeToolTips() {
            _positionToolTip.KeyName = LanguageService.Current?.FindResourceString("OffsetTag");
            _valToolTip.KeyName = LanguageService.Current?.FindResourceString("ValueTag");

            DataToolTips.Add(_positionToolTip);
            DataToolTips.Add(_valToolTip);
        }

        private ToolTipItemModel _valToolTip = new ToolTipItemModel();
        private ToolTipItemModel _positionToolTip = new ToolTipItemModel();

        private long _hoverPosition;
        public long HoverPosition {
            get => _hoverPosition;
            set {
                SetProperty(ref _hoverPosition, value);
                if (Stream?.CanRead ?? false) {
                    if (_hoverPosition >= Stream.Length) {
                        return;
                    }
                    Stream.Position = _hoverPosition;
                    _positionToolTip.Value = value.ToString();
                    _valToolTip.Value = Stream.ReadByte().ToString();
                }

            }
        }

        public ObservableCollection<ToolTipItemModel> DataToolTips { get; set; } = new ObservableCollection<ToolTipItemModel>();


        private ToolTipItemModel _selectedToolTipItem;
        public ToolTipItemModel SelectedToolTipItem {
            get => _selectedToolTipItem;
            set => SetProperty(ref _selectedToolTipItem, value);
        }


        private DelegateCommand _copyKeyCommand;
        public DelegateCommand CopyKeyCommand => _copyKeyCommand ??
            (_copyKeyCommand = new DelegateCommand(
                () => {
                    Clipboard.SetText(SelectedToolTipItem.KeyName);
                },
                () => SelectedToolTipItem != null
            )).ObservesProperty(() => SelectedToolTipItem);


        private DelegateCommand _copyValueCommand;
        public DelegateCommand CopyValueCommand => _copyValueCommand ??
            (_copyValueCommand = new DelegateCommand(
                () => {
                    Clipboard.SetText(SelectedToolTipItem.Value);
                },
                () => SelectedToolTipItem != null
            )).ObservesProperty(() => SelectedToolTipItem);

        private DelegateCommand _copyExpressionCommand;
        public DelegateCommand CopyExpressionCommand => _copyExpressionCommand ??
            (_copyExpressionCommand = new DelegateCommand(
                () => {
                    Clipboard.SetText($"{SelectedToolTipItem.KeyName}:{SelectedToolTipItem.Value}");
                },
                () => SelectedToolTipItem != null
            )).ObservesProperty(() => SelectedToolTipItem);

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
                                var dialogService = ServiceProvider.Current.GetInstance<IDialogService>();
                                if (dialogService == null) {
                                    LoggerService.Current?.WriteCallerLine($"{nameof(dialogService)} can't be null.");
                                    return;
                                }

                                var path = dialogService.SaveFile();
                                if (path != null) {

                                    var dialog = dialogService.CreateLoadingDialog();
                                    dialog.WindowTitle = "数据正在拷贝中";
                                    dialog.DoWork += (sender, e) => {
                                        try {
                                            var fs = File.Create(path);
                                            byte[] buffer = new byte[10485760];
                                            int read;
                                            long readSize = 0;
                                            Stream.Position = SelectionStart;
                                            var end = Math.Max(SelectionStart, SelectionStop);
                                            var start = Math.Min(SelectionStart, SelectionStop);
                                            using (var Ins = InterceptStream.CreateFromStream(Stream, start, end - start + 1)) {
                                                while ((read = Ins.Read(buffer, 0, buffer.Length)) != 0 && !dialog.CancellationPending) {
                                                    fs.Write(buffer, 0, read);
                                                    readSize += read;
                                                    dialog.ReportProgress((int)(readSize * 100 / Ins.Length));
                                                }
                                            }
                                            fs.Close();
                                        }
                                        catch {

                                        }
                                    };
                                    dialog.RunWorkerCompleted += (sender, e) => {
                                        if (!e.Cancelled) {
                                            MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString("Finished"));
                                        }
                                    };
                                    dialog.ShowDialog();
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
                                    MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString("TooLargeCopySize"));
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
                                            MsgBoxService.Current?.Show($"{ex.Message}");
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
                                    MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString("TooLargeCopySize"));
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
                                            MsgBoxService.Current?.Show($"{ex.Message}");
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
                                    MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString("TooLargeCopySize"));
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
                                            MsgBoxService.Current?.Show($"{ex.Message}");
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
                    //SubmitChangesRequired?.Invoke(this, new EventArgs());
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
            if (selectionLength > maxCopyToClipBoardSize) {
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
        public bool IsFileSystemHex { get; protected set; }
    }
}
