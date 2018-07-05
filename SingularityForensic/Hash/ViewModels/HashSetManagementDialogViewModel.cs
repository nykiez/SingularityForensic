using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.Hash;
using SingularityForensic.Hash.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Hash.ViewModels
{
    public class HashSetManagementDialogViewModel : BindableBase {
        public HashSetManagementDialogViewModel() {

        }

        public void Initialize() {
            var hashSets = HashSetManagementService.HashSets;
            if (hashSets == null) {
                return;
            }

            foreach (var hashSet in hashSets) {
                HashSetModels.Add(new HashSetModel(hashSet));
            }

            //加入删除哈希集命令;
            var delCmi = CommandItemFactory.CreateNew(DeleteHashSetCommand);
            delCmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_DeleteHashSet);
            ContextCommands.Add(delCmi);

            //加入导入哈希命令;
            var importCmi = CommandItemFactory.CreateNew(ImportHashCommand);
            importCmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_ImportHash);
            ContextCommands.Add(importCmi);
        }

        public ObservableCollection<ICommandItem> ContextCommands { get; } = new ObservableCollection<ICommandItem>();

        public ObservableCollection<HashSetModel> HashSetModels { get; } = new ObservableCollection<HashSetModel>();

        public InteractionRequest<INotification> CloseRequest { get; } = new InteractionRequest<INotification>();

        private HashSetModel _selectedHashSetModel;
        public HashSetModel SelectedHashSetModel {
            get => _selectedHashSetModel;
            set => SetProperty(ref _selectedHashSetModel, value);
        }

        /// <summary>
        /// 确定更改命令;
        /// </summary>
        private DelegateCommand _confirmCommand;
        public DelegateCommand ConfirmCommand => _confirmCommand ??
            (_confirmCommand = new DelegateCommand(
                () => {
                    if (!ValidateInput()) {
                        return;
                    }

                    try {
                        ApplyModifications();
                        CloseRequest.Raise(new Notification());
                    }
                    catch (Exception ex) {
                        LoggerService.WriteException(ex);
                        MsgBoxService.Show(ex.Message);
                    }
                }
            ));


        /// <summary>
        /// 删除当前的哈希集;
        /// </summary>
        private DelegateCommand _deleteHashSetCommand;
        public DelegateCommand DeleteHashSetCommand => _deleteHashSetCommand ??
            (_deleteHashSetCommand = new DelegateCommand(
                () => {
                    if(SelectedHashSetModel == null) {
                        return;
                    }

                    if (!HashSetModels.Contains(SelectedHashSetModel)) {
                        return;
                    }

                    try {
                        HashSetManagementService.RemoveHashSet(SelectedHashSetModel.HashSet);
                        HashSetModels.Remove(SelectedHashSetModel);
                    }
                    catch(Exception ex) {
                        LoggerService.WriteException(ex);
                    }
                    
                    
                }
            ));


        /// <summary>
        /// 导入(从文件种)导入哈希;
        /// </summary>
        private DelegateCommand _importHashCommand;
        public DelegateCommand ImportHashCommand => _importHashCommand ??
            (_importHashCommand = new DelegateCommand(
                () => {
                    if(SelectedHashSetModel == null) {
                        MsgBoxService.Show(LanguageService.FindResourceString(Constants.MsgText_NoHashSetModelBeenSelected));
                        return;
                    }

                    var fileName = DialogService.Current.OpenFile();
                    if (string.IsNullOrEmpty(fileName)) {
                        return;
                    }

                    //使用进度对话框显示进度;
                    var loadingDialog = DialogService.Current.CreateLoadingDialog();
                    loadingDialog.WindowTitle = LanguageService.FindResourceString(Constants.WindowTitle_ImportingHash);
                    loadingDialog.IsProgressVisible = false;

                    //当前行号;
                    var lineIndex = 0;
                    //未成功行数;
                    var errLineCount = 0;
                    //成功行数;
                    var succeedLineCount = 0;

                    StreamReader sr = null;

                    loadingDialog.DoWork += delegate {
                        try {
                            SelectedHashSetModel.HashSet.BeginEdit();
                            sr = new StreamReader(fileName);
                            string line = null;
                            
                            //逐行读取;
                            while((line = sr.ReadLine()) != null) {
                                //查看哈希
                                if(line.Length != SelectedHashSetModel.HashSet.Hasher.BytesPerHashValue * 2) {
                                    errLineCount++;
                                }
                                else {
                                    SelectedHashSetModel.HashSet.AddHashPair(string.Empty, line);
                                    succeedLineCount++;
                                }

                                lineIndex++;
                                if(lineIndex % 100 == 0) {
                                    loadingDialog.ReportProgress(
                                        0, 
                                        LanguageService.Current.TryGetStringWithFormat(
                                            Constants.MsgText_ImportingHashFormat,
                                            lineIndex,
                                            succeedLineCount,
                                            errLineCount
                                        ),
                                        string.Empty
                                    );

                                    //如若点击了取消;
                                    if (loadingDialog.CancellationPending) {
                                        return;
                                    }
                                }
                            }
                        }
                        catch (Exception ex) {
                            LoggerService.WriteException(ex);
                            ThreadInvoker.UIInvoke(() => MsgBoxService.Show(ex.Message));
                        }
                        finally {
                            SelectedHashSetModel.HashSet.EndEdit();
                            sr?.Dispose();
                        }
                    };

                    loadingDialog.RunWorkerCompleted += delegate {
                        MsgBoxService.Show(
                            LanguageService.Current.TryGetStringWithFormat(
                                Constants.MsgText_HashImportedFormat,
                                lineIndex,
                                succeedLineCount,
                                errLineCount
                            )
                        );
                    };

                    loadingDialog.ShowDialog();
                }
            ));

        
        

        /// <summary>
        /// 应用更改;
        /// </summary>
        private void ApplyModifications() {
            foreach (var hashSetModel in HashSetModels) {
                hashSetModel.HashSet.Name = hashSetModel.HashSetName;
                hashSetModel.HashSet.IsEnabled = hashSetModel.HashSetEnabled;
                hashSetModel.HashSet.Description = hashSetModel.HashSetDescription;
            }
        }

        /// <summary>
        /// 验证输入;
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput() {
            var index = 0;
            foreach (var hashSetModel in HashSetModels) {
                //检查哈希集名称是否正确;
                if (string.IsNullOrWhiteSpace(hashSetModel.HashSetName)) {
                    MsgBoxService.Show(
                        $"{LanguageService.FindResourceString(Constants.MsgText_HashSetNameCannotBeEmpty)}"
                        + $"({LanguageService.FindResourceString(Constants.MsgText_Index)}:{index})");
                    return false;
                }



                index++;
            }

            return true;
        }

        /// <summary>
        /// 取消更改命令;
        /// </summary>
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand ??
            (_cancelCommand = new DelegateCommand(
                () => {
                    CloseRequest.Raise(new Notification());
                }
            ));

        /// <summary>
        /// 创建哈希集命令;
        /// </summary>
        private DelegateCommand _createHashSetCommand;
        public DelegateCommand CreateHashSetCommand => _createHashSetCommand ??
            (_createHashSetCommand = new DelegateCommand(
                () => {
                    var hashSet = HashSetDialogService.CreateNewHashSet();
                    if (hashSet == null) {
                        return;
                    }

                    try {
                        HashSetManagementService.AddHashSet(hashSet);
                        HashSetModels.Add(new HashSetModel(hashSet));
                    }
                    catch (Exception ex) {
                        LoggerService.WriteException(ex);
                        MsgBoxService.Show(ex.Message);
                    }

                }
            ));

        private DelegateCommand<IEnumerable<object>> _itemsDeletedCommand;
        public DelegateCommand<IEnumerable<object>> ItemsDeletedCommand => _itemsDeletedCommand ?? (
            _itemsDeletedCommand = new DelegateCommand<IEnumerable<object>>(args => {
                if(args == null) {
                    return;
                }

                foreach (var item in args) {
                    if(!(item is HashSetModel hashSetModel)) {
                        continue;
                    }

                    if (HashSetManagementService.Current.HashSets.Contains(hashSetModel.HashSet)) {
                        HashSetManagementService.RemoveHashSet(hashSetModel.HashSet);
                    }
                }
                //
            }));

        
    }
}
