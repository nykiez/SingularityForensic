using CDFC.Parse.Abstracts;
using CDFC.Parse.Contracts;
using Microsoft.Win32.SafeHandles;
using CDFC.Parse.Modules.Contracts;
using System.Collections.Generic;
using CDFCCultures.Helpers;
using System.Linq;

namespace CDFC.Parse.Modules.DeviceObjects {
    //为搜索创建的一个"分区";
    public class SearcherPartition : Partition {
        /// <summary>
        /// 虚构恢复分区构造方法;
        /// </summary>
        /// <param name="parent">父文件</param>
        /// <param name="oriFile">所虚拟的对象</param>
        /// <param name="startLBA">对象的起始LBA</param>
        /// <param name="endLBA">对象的终止LBA</param>
        public SearcherPartition(Device parent,IFileoriFile,long startLBA,long endLBA,string name) : base(parent) {
            this.StartLBA = startLBA;
            
            this.Name = name;
            if(parent is IHaveHandle) {
                Handle = (parent as IHaveHandle).Handle;
                OriFile = oriFile;
            }
        }
        public IFileOriFile { get; }
        public SafeFileHandle Handle { get; private set; }
        public override uint ClusterSize => 0;

        public override FileSystemType FSType => FileSystemType.Unknown;

        //从文件节点链表中加载虚拟分区;
        public static SearcherPartition LoadFromNodeList(BlockDeviceFile blDevice,List<IFileNode> ndList,string name) {
            Device device = null;
            long startLBA = 0;
            long endLBA = 0;
            if (blDevice is Device) {
                device = blDevice as Device;
                endLBA = device.Size - 1;
            }
            else if (blDevice is Partition) {
                var part = blDevice as Partition;
                device = blDevice.GetParent<Device>();
                startLBA = part.StartLBA;
                endLBA = part.Size + part.StartLBA;

            }

            try {
                var part = new SearcherPartition(device, blDevice, startLBA, endLBA, name);
                //文件系统对比;
                var files = new List<RegularFile>();
                foreach (var p in device.Children) {
                    if (p is Partition fspart) {
                        foreach (var q in fspart.Children) {
                            if (q is CDFC.Parse.Abstracts.Directory) {
                                TraveringAddFile(q as CDFC.Parse.Abstracts.Directory, files);
                            }
                            else if (q is RegularFile regFile && regFile.Deleted == false) {
                                files.Add(regFile);
                            }
                        }
                    }
                }
                
                files.Sort((a, b) => a.StartLBA > b.StartLBA ? 1 : (a.StartLBA == b.StartLBA ? 0 : -1));
                files.RemoveAll(q => q.Deleted == true);

                var searchedfileList = new List<SearcherFile>();
                var fileList = new List<RegularFile>();

                if (ndList?.Count != 0) {
                    searchedfileList.AddRange(ndList.Select(p => new SearcherFile(part, p)));
                }

                searchedfileList.ForEach(p => {
                    //二分查询是否有现存项;
                    var shFile = files.BinarySearch(0, files.Count - 1,
                        file => file.DeviceStartLBA,
                        p.DeviceStartLBA);

                    if (shFile != null) {
                        fileList.Add(shFile);
                    }
                    else {
                        if (device.Children.FirstOrDefault(q => {
                            if (q is Partition pt2) {
                                return pt2.StartLBA <= p.DeviceStartLBA && (pt2.StartLBA + pt2.Size) >= p.DeviceStartLBA;
                            }
                            return false;
                        }) is IIterableFile pt) {
                            p.SetParent(pt);
                        }
                        else {
                            //Logger.WriteLine($"{nameof(SearcherPartition)}:{nameof(LoadFromNodeList)}:pt can't be null!");
                        }
                        fileList.Add(p);
                    }
                });
                part.AddChildren(fileList);

                return part;
            }
            catch {
                return null;
            }
            
        }

        /// <summary>
        /// 遍历获得文件集;
        /// </summary>
        /// <param name="itr"></param>
        /// <param name="files"></param>
        private static void TraveringAddFile(IIterableFile itr, List<RegularFile> files) {
            foreach (var p in itr.Children) {
                if (p is Directory && !IIterableHelper.IsBackUpFile(p as IIterableFile) && 
                    !IIterableHelper.IsBackFile(p as IIterableFile)) {
                    TraveringAddFile(p as Directory, files);
                }
                else if (p is RegularFile regFile && regFile.Deleted == false) {
                    files.Add(regFile);
                }
            }
            
        }

        private List<IFile> _children = new List<IFile>();
        public override IEnumerable<IFile> Children => _children;
        public void AddChildren(IEnumerable<IFile> files) {
            _children.AddRange(files);
        }

    }
}
