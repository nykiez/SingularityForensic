using CDFC.Parse.Modules.Contracts;
using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Modules.Pictures {
    [Serializable]
    public class FileNode : IFileNode {
        public string Data {
            get {
                if (StPhotoFileNode != null) {
                    return StPhotoFileNode.Value.Data;
                }
                return string.Empty;
            }
        }

        public long EndLBA {
            get {
                if (StPhotoFileNode != null) {
                    return (long) StPhotoFileNode.Value.End;
                }
                return 0;
            }
        }

        public long FileSize {
            get {
                if (StPhotoFileNode != null) {
                    return (long) StPhotoFileNode.Value.FileSize;
                }
                return 0;
            }
        }

        public string Name {
            get {
                if (StPhotoFileNode != null) {
                    return StPhotoFileNode.Value.Name;
                }
                return string.Empty;
            }
        }

        public long StartLBA {
            get {
                if (StPhotoFileNode != null) {
                    return (long) StPhotoFileNode.Value.Start;
                }
                return 0;
            }
        }

        public bool HasThumb {
            get {
                return ThumbStart != 0;
            }
        }
        public long ThumbStart {
            get {
                if(StPhotoFileNode != null) {
                    return (long) StPhotoFileNode.Value.ThumbStart;
                }
                return 0;
            }
        }

        public string Type {
            get {
                if(StPhotoFileNode != null) {
                    return StPhotoFileNode.Value.Type;
                }
                return string.Empty;
            }
        }
        
        public StPhotoFileNode? StPhotoFileNode { get; set; }
    }
    [Serializable]
    [StructLayout(LayoutKind.Sequential,Pack =1, CharSet = CharSet.Ansi)]
    public struct StPhotoFileNode {
        public ulong Start;
        public ulong End;
        public ulong FileSize;
        public ulong ThumbStart;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Name;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Data;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Type;
        public IntPtr Next;
    }
}
