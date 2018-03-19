using CDFC.Parse.Abstracts;
using System;
using CDFC.Parse.Contracts;
using CDFC.Parse.Modules.Contracts;
using EventLogger;
using System.IO;
using CDFC.Util.IO;

namespace CDFC.Parse.Modules.DeviceObjects {
    public class SearcherFile : RegularFile {
        public SearcherFile(IFileparent, IFileNode fileNode) : base(parent) {
            if (fileNode == null)
                throw new ArgumentNullException(nameof(fileNode));

            this.FileNode = fileNode;
        }
        public IFileNode FileNode { get; }
        
        public override bool? Deleted => true;

        private string name;
        public override string Name => name ?? FileNode?.Name;
        public void SetName(string name) => this.name = name;

        private long? size;
        public override long Size => size ?? FileNode?.FileSize??0;
        public void SetSize(long size) => this.size = size;
        
        public override long StartLBA {
            get {
                Func<Partition, long> GetLBA = part => {
                    if (part != null) {
                        try {
                            var startLBA = FileNode.StartLBA - part.StartLBA;
                            if (startLBA < 0) {
                                Logger.WriteLine($"{nameof(SearcherFile)}->{nameof(StartLBA)}:Less Than Zero{startLBA} {FileNode.StartLBA}-{part.StartLBA}");
                                return 0;
                            }
                            return startLBA;
                        }
                        catch(Exception ex) {
                            Logger.WriteLine($"{nameof(SearcherFile)}->{nameof(StartLBA)}:{ex.Message}");
                        }
                    }
                    else {
                        Logger.WriteLine($"{nameof(SearcherFile)}->{nameof(StartLBA)}:Null Part");
                    }
                    return 0;
                };

                return GetLBA(Parent as Partition);
                
            }
        }
        
        public bool parentChanged;
        private IIterableFile parent;
        public override IFileParent {
            get {
                return parent ?? base.Parent;
            }
        }
        public IFileRealParent { get; private set; }                       //亲生爸爸;

        public override DateTime? ModifiedTime => null;

        public override DateTime? AccessedTime => null;

        public override DateTime? CreateTime => null;

        public void SetParent(IIterableFile parent) {
            if(Parent is SearcherPartition) {
                this.RealParent = Parent;
                parentChanged = true;
            }
            this.parent = parent;
        }

        public override Stream GetStream(bool isReadOnly = true) {
            var device = this.GetParent<Device>();
            if (device != null) {
                return InterceptStream.CreateFromStream(
                    device.Stream, 
                    this.DeviceStartLBA,
                    Size
                );
            }
            
            return null;
        }
    }
}
