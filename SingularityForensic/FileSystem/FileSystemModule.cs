using Prism.Modularity;
using Prism.Mef.Modularity;

namespace SingularityForensic.FileSystem {
    [ModuleExport(typeof(FileSystemModule))]
    public class FileSystemModule : IModule {
        //[ImportMany(Contracts.FileSystem.Constants.DeviceNodeContextCommand)]
        //private IEnumerable<ICommandItem> DeviceNodeCommandItems;
        
        public void Initialize() {
            Contracts.FileSystem.FileSystemService.Current?.Initialize();
        }
        
        ///// <summary>
        ///// 签名搜索;
        ///// </summary>
        ///// <param name="blDevice"></param>
        ///// <param name="setting"></param>
        //private static void SignSearch(BlockDeviceFile blDevice, SignSearchSetting setting) {
        //    Device device = null;
        //    long startLBA = 0;
        //    long endLBA = 0;
        //    if (blDevice is Device) {
        //        device = blDevice as Device;
        //        endLBA = device.Size - 1;
        //    }
        //    else if (blDevice is Partition) {
        //        var part = blDevice as Partition;
        //        device = blDevice.GetParent<Device>();
        //        startLBA = part.StartLBA;
        //        endLBA = part.EndLBA;

        //    }

        //    if (device != null) {
        //        var dialog = new ProgressMessageBox();
        //        dialog.WindowTitle =LanguageService.Current?.FindResourceString("SignSearch");

        //        var part = new SearcherPartition(device, blDevice, startLBA, endLBA, $"{blDevice.Name}-{FindResourceString("SignSearch")}");

        //        dialog.DoWork += (sender, e) => {
        //            var searcher = new SignSearcher(device.Stream, setting.KeyWord, setting.MaxSize, setting.SectorSize, setting.SecStartLBA);
        //            searcher.AlignToSector = setting.AlignToSec;
        //            searcher.FileExtension = setting.FileExtension;

        //            searcher.CurOffsetChanged += (insender, curOffset) => {
        //                var percentage = (int)((curOffset - startLBA) * 100 / (endLBA - startLBA));
        //                if (percentage >= 0 && percentage <= 100) {
        //                    dialog.ReportProgress(percentage,
        //                   LanguageService.Current?.FindResourceString("SearchingSignFile"),
        //                    $"{FindResourceString("RecoveringBySign")}:{percentage}%");
        //                }
        //                if (dialog.CancellationPending) {
        //                    searcher.Stop();
        //                }
        //            };

        //            searcher.SearchStart(startLBA, endLBA);

        //            var fileList = new List<RegularFile>();
        //            var shfileList = new List<RegularFile>();

        //            try {
        //                //遍历获取文件列表;
        //                searcher.FileExtension = setting.FileExtension;
        //                var ndList = searcher.GetFileList(string.Empty);
        //                if (ndList?.Count != 0) {
        //                    shfileList.AddRange(ndList.Select(p => new SearcherFile(part, p)));
        //                }
        //                shfileList.ForEach(p => {
        //                    fileList.Add(p);
        //                });
        //            }
        //            catch (Exception ex) {
        //                Logger.WriteLine($"{nameof(FileSystemModule)} -> {nameof(SignSearch)}:{ex.Message}");
        //            }
        //            finally {
        //                part.Children.AddRange(fileList);
        //                searcher.Dispose();
        //            }
        //        };
        //        dialog.RunWorkerCompleted += (sender, e) => {
        //            AddShowingFile(part);
        //            ServiceLocator.Current.GetInstance<IShellService>()?.Focus();
        //        };
        //        dialog.ShowDialog();
        //    }
        //}
        
        ////递归浏览节点;
        //public static void RecurUnit(ITreeUnit unit) {
        //    PubEventHelper.GetEvent<TreeNodeClickEvent>()?.Publish(unit);
        //    //通知等待;
        //    ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(true, string.Empty);
        //    ThreadPool.QueueUserWorkItem(callBack => {
        //        try {
        //            IFile file = null;
        //            if (unit is StorageTreeUnit stUnit) {
        //                file = stUnit.File;
        //            }
        //            else if (unit is IHaveData<ICaseFile> cFileUnit) {
        //                if (cFileUnit.Data is IHaveData<IFile> fCFile) {
        //                    file = fCFile.Data;
        //                }
        //            }
        //            if (file is IEnumerableFileFile itrFile) {
        //                ExpandFile(itrFile);
        //            }
        //        }
        //        catch (Exception ex) {
        //            LoggerService.Current?.WriteCallerLine($"{ex.Message}");
        //            AppInvoke(() => {
        //                RemainingMessageBox.Tell(ex.Message);
        //            });
        //        }
        //        finally {
        //            //解除等待；
        //            ServiceLocator.Current.GetInstance<IShellService>()?.ChangeLoadState(false, string.Empty);
        //        }
        //    });
        //}
    }
}
