using Prism.Commands;
using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.Common;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.FileExplorer {
    public static class FileExplorerTreeUnitCommandItemFactory {
        /// <summary>
        /// 自定义签名功能;
        /// </summary>
        /// <param name="blockedStream"></param>
        /// <returns></returns>
        public static ICommandItem CreateCustomSignSearchCommandItem(IStreamFile blockedStream) {
            if(blockedStream == null) {
                throw new ArgumentNullException(nameof(blockedStream));
            }
            
            var cmi = CommandItemFactory.CreateNew(CreateCustomSignSearchCommand(blockedStream));
            cmi.Name = LanguageService.FindResourceString(Constants.ContextCommandName_CustomSignSearch);
            cmi.Sort = 12;
            return cmi;
        }

        private static DelegateCommand CreateCustomSignSearchCommand(IStreamFile blockedStream) {
            if (blockedStream == null) {
                throw new ArgumentNullException(nameof(blockedStream));
            }
            
            var comm = new DelegateCommand(
                () => {
                    var setting = CustomSignSearchService.GetSetting();
                    if(setting == null) {
                        return;
                    }
                    SignSearch(blockedStream, setting);
                }
            );
            return comm;
        }

        /// <summary>
        /// 签名搜索;
        /// </summary>
        /// <param name="blDevice"></param>
        /// <param name="setting"></param>
        private static void SignSearch(IStreamFile blockedStream,ICustomSignSearchSetting setting) {
            var loadingDialog = DialogService.Current.CreateLoadingDialog();
            loadingDialog.WindowTitle = LanguageService.FindResourceString(Constants.WindowTitle_CustomSignSearch);

            var part = FileFactory.CreatePartition(Constants.PartitionKey_CustomSignSearch);

            var partStoken = part.GetStoken(Constants.PartitionKey_CustomSignSearch);
            partStoken.BaseStream = blockedStream.BaseStream;
            partStoken.Name = $"{blockedStream.Name}-{LanguageService.FindResourceString(Constants.DocumentTitle_CustomSignSearch)}"+
                $"({CDFCCultures.Helpers.ByteConverterHelper.ConvertToHexFormat(setting.KeyWord)})";

            (long position, long size)[] fileBlocks = null;
            
            loadingDialog.DoWork += (sender, e) => {
                fileBlocks = CustomSignSearchServiceOnDialog(loadingDialog, setting, blockedStream.BaseStream).ToArray();
            };
            loadingDialog.RunWorkerCompleted += (sender, e) => {
                if(fileBlocks == null) {
                    return;
                }

                var index = 0;
                foreach (var block in fileBlocks) {
                    var regFile = FileFactory.CreateRegularFile(Constants.RegularFileKey_CustomSignSearch);

                    var fileStoken = regFile.GetStoken(Constants.RegularFileKey_CustomSignSearch);
                    fileStoken.Size = block.size;
                    fileStoken.BlockGroups.Add(BlockGroupFactory.CreateNewBlockGroup(0, 1, block.size, block.position));
                    fileStoken.Name = $"{LanguageService.FindResourceString(Constants.RegularFileName_CustomSignSearch)}"+
                    $".{setting.FileExtension??Constants.DefaultFileExtension_CustomSignSearch}";

                    part.Children.Add(regFile);

                    index++;
                }

                FileExplorerUIHelper.AddFileToDocument(part);

            };
            loadingDialog.ShowDialog();
        }

        private static IEnumerable<(long position,long size)> CustomSignSearchServiceOnDialog(ILoadingDialog loadingDialog,ICustomSignSearchSetting setting,Stream stream) {
            var reporter = ProgessReporterFactory.CreateNew();

            loadingDialog.Canceld += (sender, e) => {
                reporter.Cancel();
            };

            
            var latestPro = 0;
            reporter.ProgressReported += (sender, e) => {
                if (latestPro < e.pro) {
                    latestPro = e.pro;
                    loadingDialog.ReportProgress(latestPro,e.text,e.descrip);
                }
            };
            return CustomSignSearchService.Search(stream, setting, reporter);
        }
    }
}
