using SingularityForensic.Contracts.App;
using SingularityForensic.Contracts.FileExplorer;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.FileExplorer.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SingularityForensic.FileExplorer {
    public static class FileExplorerStreamFileExtensions {
       
        /// <summary>
        /// 签名搜索;
        /// </summary>
        /// <param name="blDevice"></param>
        /// <param name="setting"></param>
        public static void SignSearch(IStreamFile streamFile) {
            var setting = CustomSignSearchService.GetSetting();
            if(setting == null) {
                return;
            }

            var loadingDialog = DialogService.Current.CreateLoadingDialog();
            loadingDialog.WindowTitle = LanguageService.FindResourceString(Constants.WindowTitle_CustomSignSearch);

            var part = FileFactory.CreatePartition(Constants.PartitionKey_CustomSignSearch);

            var partStoken = part.GetStoken(Constants.PartitionKey_CustomSignSearch);
            partStoken.BaseStream = streamFile.BaseStream;
            if(streamFile is IPartition streamPart) {
                partStoken.Name = $"{FileExtensions.GetPartFixAndName(streamPart)}-{LanguageService.FindResourceString(Constants.DocumentTitle_CustomSignSearch)}" +
                $"({CDFCCultures.Helpers.ByteConverterHelper.ConvertToHexFormat(setting.KeyWord)})";
            }
            else {
                partStoken.Name = $"{streamFile.Name}-{LanguageService.FindResourceString(Constants.DocumentTitle_CustomSignSearch)}" +
                $"({CDFCCultures.Helpers.ByteConverterHelper.ConvertToHexFormat(setting.KeyWord)})";
            }
            

            (long position, long size)[] fileBlocks = null;
            
            loadingDialog.DoWork += (sender, e) => {
                fileBlocks = CustomSignSearchServiceOnDialog(loadingDialog, setting, streamFile.BaseStream).ToArray();
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

                FileExplorerUIHelper.GetOrAddFileDocument(part);
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
                if (latestPro < e.totalPer) {
                    latestPro = e.totalPer;
                    loadingDialog.ReportProgress(latestPro,e.desc,e.detail);
                }
            };
            return CustomSignSearchService.Search(stream, setting, reporter);
        }
    }
}
