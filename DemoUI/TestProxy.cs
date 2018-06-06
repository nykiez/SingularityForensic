using SingularityForensic.Contracts.Casing;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.Imaging;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.TreeView;
using SingularityForensic.Contracts.TreeView.Events;
using System.Linq;

namespace DemoUI {
    /// <summary>
    /// 测试入口类;
    /// </summary>
    static class TestProxy {
        public static void Test() {
            TestPartitionNodeClick();
        }

        //测试点击设备节点后响应;
        static void TestDeviceNodeClick() {
            var devStoken = new DeviceStoken {
                BaseStream = System.IO.File.OpenRead("D://youdaonote_unsilent38.exe"),
                Name = "mmp"
            };

            //for (int i = 0; i < 24; i++) {
            //    var partEnStoken = new PartitionEntryStoken {
            //        StartLBA = i * 16,
            //        Size = 16
            //    };
            //    devStoken.PartitionEntries.Add(
            //        PartitionEntryFactory.CreatePartitionEntry(string.Empty)
            //    );
            //}


            //var dev = new IDevice(string.Empty, devStoken);
            //var rand = new Random();
            //for (int i = 0; i < 24; i++) {
            //    var part = new IPartition(string.Empty, new PartitionStoken {
            //        Name = "Dada",
            //        Size = rand.Next(25535),
            //        BaseStream = MulPeriodsStream.CreateFromStream(devStoken.BaseStream,
            //        new(long StartIndex, long Size)[] {
            //            (25535 * i,1024)
            //        })
            //    });

            //    dev.Children.Add(part);
            //    dev.SetStartLBA(part, i * 200);
            //}

            var unit = TreeUnitFactory.CreateNew(SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitType_FileSystem);
            MainTreeService.Current?.AddUnit(null, unit);
            PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
            PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
            PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
            PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
        }

        static void TestPartitionNodeClick() {
            var file = FileSystemService.Current.MountStream(System.IO.File.OpenRead("E://anli/Fat32_Test.img"), "mmp", null, null);
            
            
            //ImgService.Current.AddImg("I://test.E01");

            //var file = FileSystemService.Current.MountedFiles.First().file;

            //var unit = TreeUnitFactory.CreateNew(SingularityForensic.FileExplorer.Constants.TreeUnitGUID_FileSystem);
            //unit.SetInstance(file, SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            //PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
            //PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
            //PubEventHelper.GetEvent<TreeUnitSelectedChangedEvent>().Publish((unit, MainTreeService.Current));
        }
    }
}
