using CDFC.Parse.Abstracts;
using CDFCMessageBoxes.MessageBoxes;
using CDFCUIContracts.Commands;
using EventLogger;
using Prism.Commands;
using Renci.SshNet;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Controls.FileExplorer.ViewModels;
using System;
using System.ComponentModel.Composition;
using static CDFCCultures.Managers.ManagerLocator;

namespace SingularityForensic.Net {
    [Export(typeof(CommandItem<(DirectoriesBrowserViewModel, IFileRow)>))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UploadFileRowCommandItem:CommandItem<(DirectoriesBrowserViewModel dvm,IFileRow fileRow)> {
        public UploadFileRowCommandItem() {
            CommandName =ServiceProvider.Current?.GetInstance<ILanguageService>()?.FindResourceString("UploadingFileCommandItem");
            Command = new DelegateCommand(() => {
                if(GetData != null) {
                    try {
                        //CDFCMessageBox.Show("Dasd","d");
                        var data = GetData();
                        var file = (data.fileRow as IFileRow<RegularFile>)?.File;
                        if(file is RegularFile regFile) {
                            var msg = new ProgressMessageBox {
                                WindowTitle = "上传文件"
                            };
                            
                            msg.DoWork += delegate {
                                try {
                                    using (var regStream = regFile.GetStream()) {
                                        var connInfo = new PasswordConnectionInfo("59.173.19.248", 32577, "root", "admin@123");
                                        var fcli = new SftpClient(connInfo);
                                        try {
                                            fcli.Connect();
                                            regStream.Position = 0;
                                            fcli.UploadFile(regStream, $"/dev/CDFC/{regFile.Name}", upLoaded => {
                                                msg.ReportProgress((int)(upLoaded * 100 / (ulong)regStream.Length),
                                                    $"{FindResourceString("UploadingFileNet")}{regFile.Name}", $"{upLoaded}/{regStream.Length}");
                                            });
                                            
                                        }
                                        catch(Exception ex) {
                                            Logger.WriteCallerLine(ex.Message);
                                        }
                                        finally {
                                            fcli.Disconnect();
                                            fcli.Dispose();
                                        }
                                    };
                                }
                                catch(Exception ex) {
                                    Logger.WriteCallerLine(ex.Message);
                                }
                            };
                            msg.ShowDialog();
                            
                        }
                    }
                    catch(Exception ex) {
                        Logger.WriteCallerLine(ex.Message);
                        RemainingMessageBox.Tell(ex.Message);
                    }
                }
            });
            
        }
    }
}
