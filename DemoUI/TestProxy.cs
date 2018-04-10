using CDFC.Util.IO;
using SingularityForensic.Contracts.FileSystem;
using SingularityForensic.Contracts.Helpers;
using SingularityForensic.Contracts.MainPage;
using SingularityForensic.Contracts.MainPage.Events;
using SingularityForensic.Contracts.TreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for (int i = 0; i < 24; i++) {
                var partEnStoken = new PartitionEntryStoken {
                    StartLBA = i * 16,
                    Size = 16
                };
                devStoken.PartitionEntries.Add(
                    new PartitionEntry(string.Empty,partEnStoken)
                );
            }

            
            var dev = new Device(string.Empty, devStoken);
            var rand = new Random();
            for (int i = 0; i < 24; i++) {
                var part = new Partition(string.Empty, new PartitionStoken {
                    Name = "Dada",
                    Size = rand.Next(25535),
                    BaseStream = MulPeriodsStream.CreateFromStream(devStoken.BaseStream,
                    new(long StartIndex, long Size)[] {
                        (25535 * i,1024)
                    })
                });
                
                dev.Children.Add(part);
                dev.SetStartLBA(part , i * 200);
            }

            var unit = new TreeUnit(SingularityForensic.FileExplorer.Constants.FileSystemTreeUnit);
            NodeService.Current?.AddUnit(null, unit);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Publish(unit);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Publish(unit);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Publish(unit);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Publish(unit);
        }

        static void TestPartitionNodeClick() {
            var file = FSService.Current.MountStream(System.IO.File.OpenRead("E://anli/Fat32_Test.img"), "mmp", null, null);
            var unit = new TreeUnit(SingularityForensic.FileExplorer.Constants.FileSystemTreeUnit);
            unit.SetInstance(file, SingularityForensic.Contracts.FileExplorer.Constants.TreeUnitTag_FileSystem_File);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Publish(unit);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Publish(unit);
            PubEventHelper.GetEvent<TreeNodeClickEvent>().Publish(unit);
        }
    }
}
