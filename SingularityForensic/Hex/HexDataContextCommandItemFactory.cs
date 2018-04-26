using CDFC.Util.IO;
using CDFCCultures.Helpers;
using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SingularityForensic.Hex {
    /// <summary>
    /// 十六进制右键菜单工厂;
    /// </summary>
    public static class HexDataContextCommandFactory {
        /// <summary>
        /// 最大粘贴大小;
        /// </summary>
        public const int MaxCopyToClipBoardSize = 1024 * 1024 * 4;          

        /// <summary>
        /// 检查是否存在可用的选中状态;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <returns></returns>
        private static bool SelectionAvailable(IHexDataContext hexDataContext) {
            if(hexDataContext == null) {
                return false;
            }

            return hexDataContext.SelectionStart >= 0 && hexDataContext.SelectionLength > 0;
        }

        /// <summary>
        /// 拷贝至新文件;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <returns></returns>
        public static ICommandItem CreateCopyToNewFileCommandItem(IHexDataContext hexDataContext) {
            if(hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            var cmi = CommandItemFactory.CreateNew(CreateCopyToNewFileCommand(hexDataContext));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CopyToNewFile);
            return cmi;
        }
        internal static DelegateCommand CreateCopyToNewFileCommand(IHexDataContext hexDataContext) {
            var copyToNewFileCommand = new DelegateCommand(
                () => {
                    if (hexDataContext.SelectionLength == -1 || hexDataContext.SelectionStart == -1) {
                        return;
                    }

                    var dialogService = ServiceProvider.Current.GetInstance<IDialogService>();
                    if (dialogService == null) {
                        LoggerService.WriteCallerLine($"{nameof(dialogService)} can't be null.");
                        return;
                    }

                    var path = dialogService.GetSaveFilePath();
                    if (path == null) {
                        return;
                    }

                    var dialog = dialogService.CreateLoadingDialog();
                    dialog.WindowTitle = LanguageService.FindResourceString(Constants.WindowTitle_DataCopying);
                    dialog.DoWork += (sender, e) => {
                        FileStream fs = null;
                        InterceptStream sourceStream = null;

                        try {
                            fs = File.Create(path);
                            byte[] buffer = new byte[10485760];
                            int read;
                            long readSize = 0;
                            hexDataContext.Stream.Position = hexDataContext.SelectionStart;

                            sourceStream = InterceptStream.CreateFromStream(
                                hexDataContext.Stream,
                                hexDataContext.SelectionStart,
                                hexDataContext.SelectionLength);

                            while ((read = sourceStream.Read(buffer, 0, buffer.Length)) != 0 && !dialog.CancellationPending) {
                                fs.Write(buffer, 0, read);
                                readSize += read;
                                dialog.ReportProgress((int)(readSize * 100 / sourceStream.Length));
                            }


                        }
                        catch (Exception ex) {
                            LoggerService.WriteCallerLine(ex.Message);
                        }
                        finally {
                            fs?.Dispose();
                            sourceStream?.Dispose();
                        }
                    };

                    dialog.RunWorkerCompleted += (sender, e) => {
                        if (e.Cancelled) {
                            return;
                        }

                        LocalExplorerService.OpenFolderAndSelectFile(path);
                        MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString(Constants.WindowTitle_DataCopyDone));
                    };

                    dialog.ShowDialog();
                }, () => SelectionAvailable(hexDataContext));
            
            hexDataContext.SelectionStateChanged += delegate {
                copyToNewFileCommand.RaiseCanExecuteChanged();
            };
            return copyToNewFileCommand;
        }
        
        /// <summary>
                                                                                   /// 拷贝至剪切板;
                                                                                   /// </summary>                                                                           
        public static ICommandItem CreateCopyToClipBoardCommandItem(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            var cmi = CommandItemFactory.CreateNew(CreateCopyToClipBoardCommand(hexDataContext));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CopyToClipBoard);
            return cmi;
        }
        internal static DelegateCommand CreateCopyToClipBoardCommand(IHexDataContext hexDataContext) {
            var copyToClipBoardCommand = new DelegateCommand(
                () => {
                    if (hexDataContext.SelectionStart == -1 || hexDataContext.SelectionLength == -1) {       
                        return;
                    }

                    //若需剪切数据大于4GB,提示警告;
                    if (hexDataContext.SelectionLength - hexDataContext.SelectionStart > MaxCopyToClipBoardSize) {
                        MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString(Constants.MsgText_TooLargeCopySize));
                        return;
                    }
                    
                    hexDataContext.Stream.Position = hexDataContext.SelectionStart;

                    InterceptStream sourceStream = null;
                    StreamReader sr = null;

                    try {
                        sourceStream = InterceptStream.CreateFromStream(
                            hexDataContext.Stream, 
                            hexDataContext.SelectionStart, 
                            hexDataContext.SelectionLength);

                        sr = new StreamReader(sourceStream);
                        ClipBoardService.SetDataObject(sr.ReadToEnd());
                    }
                    catch(Exception ex) {
                        LoggerService.WriteCallerLine(ex.Message);
                        MsgBoxService.Current?.Show($"{ex.Message}");
                    }
                    finally {
                        sourceStream?.Dispose();
                        sr?.Dispose();
                    }
                    
                });

            return copyToClipBoardCommand;
        }
        
        /// <summary>
        /// 拷贝十六进制到剪切板;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <returns></returns>
        public static ICommandItem CreateCopyToCopyHexToCBoardCommandItem(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            var cmi = CommandItemFactory.CreateNew(CreateCopyToCopyHexToCBoardCommand(hexDataContext));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CopyHexToCBoard);
            return cmi;
        }
        internal static DelegateCommand CreateCopyToCopyHexToCBoardCommand(IHexDataContext hexDataContext) {
            var copyHexToCBoardCommand = new DelegateCommand(
            () => {
                if (hexDataContext.SelectionStart == -1 || hexDataContext.SelectionStart == -1) {       //若需剪切数据大于4GB
                    return;
                }

                if (hexDataContext.SelectionLength - hexDataContext.SelectionStart > MaxCopyToClipBoardSize) {
                    MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString(Constants.MsgText_TooLargeCopySize));
                    return;
                }
                
                try {
                    var buffer = hexDataContext.GetSelectionData();
                    ClipBoardService.SetText(ByteConverterHelper.ByteToHex(buffer));
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                    MsgBoxService.Current?.Show($"{ex.Message}");
                }
            });

            return copyHexToCBoardCommand;
        }

        /// <summary>
        /// 拷贝ASCII字符串至剪切板;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <returns></returns>
        public static ICommandItem CreateCopyASCIIToCBoardCommandItem(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            var cmi = CommandItemFactory.CreateNew(CopyASCIIToCBoardCommand(hexDataContext));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CopyASCIIToCBoard);
            return cmi;
        }
        internal static DelegateCommand CopyASCIIToCBoardCommand(IHexDataContext hexDataContext) {
            var copyASCIIToCBoardCommand = new DelegateCommand(
                () => {
                    if (hexDataContext.SelectionStart == -1 || hexDataContext.SelectionLength == -1) {       //若需剪切数据大于4GB
                        return;
                    }

                    if (hexDataContext.SelectionLength > MaxCopyToClipBoardSize) {
                        MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString(Constants.MsgText_TooLargeCopySize));
                    }
                    
                    try {
                        var buffer = hexDataContext.GetSelectionData();
                        ClipBoardService.SetDataObject(Encoding.ASCII.GetString(buffer, 0, buffer.Length));
                    }
                    catch(Exception ex) {
                        LoggerService.WriteCallerLine(ex.Message);
                        MsgBoxService.Current?.Show($"{ex.Message}");
                    }
                    
                });

            return copyASCIIToCBoardCommand;
        }
        
        /// <summary>
        /// 设为起始选定块;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <returns></returns>
        public static ICommandItem CreateSetAsStartCommandItem(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            var cmi = CommandItemFactory.CreateNew(CreateSetAsStartCommand(hexDataContext));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_SetAsStart);
            return cmi;
        }
        internal static DelegateCommand CreateSetAsStartCommand(IHexDataContext hexDataContext) {
            var setAsStartCommand = new DelegateCommand(() => {
                if (hexDataContext.Stream == null) {
                    return;
                }

                //Check if FocusPosition is valid;
                if (hexDataContext.FocusPosition == -1) {
                    return;
                }
                if (hexDataContext.FocusPosition >= (hexDataContext.Stream?.Length ?? 0)) {
                    return;
                }

                if (hexDataContext.SelectionStart != -1 && hexDataContext.SelectionLength != 0
                && hexDataContext.SelectionStart + hexDataContext.SelectionLength - 1 > hexDataContext.FocusPosition) {
                    hexDataContext.SelectionLength = hexDataContext.SelectionStart + hexDataContext.SelectionLength - hexDataContext.FocusPosition;
                }
                else {
                    hexDataContext.SelectionLength = 1;
                }

                hexDataContext.SelectionStart = hexDataContext.FocusPosition;
            });

            return setAsStartCommand;
        }

        /// <summary>
        /// 设为终止选定块命令;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <returns></returns>
        public static ICommandItem CreateSetAsEndCommandItem(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            var cmi = CommandItemFactory.CreateNew(CreateSetAsEndCommand(hexDataContext));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_SetAsEnd);
            return cmi;
        }
        internal static DelegateCommand CreateSetAsEndCommand(IHexDataContext hexDataContext) {
            var setAsEndCommand = new DelegateCommand(() => {
                if (hexDataContext.Stream == null) {
                    return;
                }

                //Check if FocusPosition is valid;
                if (hexDataContext.FocusPosition == -1) {
                    return;
                }
                if (hexDataContext.FocusPosition >= (hexDataContext.Stream.Length)) {
                    return;
                }
                
                if (hexDataContext.SelectionStart != -1 && hexDataContext.SelectionLength != 0
                && hexDataContext.SelectionStart < hexDataContext.FocusPosition) {
                    hexDataContext.SelectionLength = hexDataContext.FocusPosition - hexDataContext.SelectionStart + 1;
                }
                else {
                    hexDataContext.SelectionStart = hexDataContext.FocusPosition;
                    hexDataContext.SelectionLength = 1;
                }
            });

            return setAsEndCommand;    
        }

        /// <summary>
        /// 保存修改的内容命令,暂未使用;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <returns></returns>
        private static DelegateCommand CreateSubmitChangesCommand(IHexDataContext hexDataContext) {
            var submitChangesCommand = new DelegateCommand(() => {
                //SubmitChangesRequired?.Invoke(this, new EventArgs());
            });
            return submitChangesCommand;
        }

        /// <summary>
        /// 复制为代码;
        /// </summary>
        /// <param name="hexDataContext"></param>
        /// <returns></returns>
        public static ICommandItem CreateCopyAsProCodeCommandItem(IHexDataContext hexDataContext) {
            if (hexDataContext == null) {
                throw new ArgumentNullException(nameof(hexDataContext));
            }

            var cmi = CommandItemFactory.CreateNew(CreateSetAsEndCommand(hexDataContext));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CopyAsProgramCode);
            var children = GetCopyAsProCommandItems(hexDataContext);
            foreach (var child in children) {
                cmi.AddChild(child);
            }
            return cmi;
        }

        private static IEnumerable<ICommandItem> GetCopyAsProCommandItems(IHexDataContext hexDataContext) {
            var formatters = ServiceProvider.GetAllInstances<IBufferToCodeFormatter>().OrderBy(p => p.Sort);
            foreach (var formatter in formatters) {
                yield return GetCopyAsProCommandItem(hexDataContext, formatter);
            }
        }

        private static ICommandItem GetCopyAsProCommandItem(IHexDataContext hexDataContext,IBufferToCodeFormatter formatter) {
            var comm = new DelegateCommand(() => {
                if(hexDataContext.SelectionStart == - 1 || hexDataContext.SelectionLength == -1) {
                    return;
                }

                if (hexDataContext.SelectionLength - hexDataContext.SelectionStart > MaxCopyToClipBoardSize) {
                    MsgBoxService.Current?.Show(LanguageService.Current?.FindResourceString(Constants.MsgText_TooLargeCopySize));
                    return;
                }

                try {
                    var buffer = GetSelectionData(hexDataContext);
                    ClipBoardService.SetText(formatter.FormatAsCode(buffer));
                }
                catch (Exception ex) {
                    LoggerService.WriteCallerLine(ex.Message);
                }
            });

            var cmi = CommandItemFactory.CreateNew(comm);
            cmi.Name = LanguageService.FindResourceString(formatter.CodeLanguageName);
            return cmi;
        }
        
        private static byte[] GetSelectionData(this IHexDataContext hexDataContext) {
            if (hexDataContext.SelectionLength > MaxCopyToClipBoardSize) {
                throw new InsufficientMemoryException();
            }

            hexDataContext.Stream.Position = hexDataContext.SelectionStart;
            var buffer = new byte[hexDataContext.SelectionLength];
            hexDataContext.Stream.Read(buffer, 0, (int)hexDataContext.SelectionLength);
            return buffer;
        }
        
    }
}
