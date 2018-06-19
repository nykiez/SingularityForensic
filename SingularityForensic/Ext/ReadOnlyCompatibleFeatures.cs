using System;

namespace SingularityForensic.Ext {
    [Flags]
    public enum ReadOnlyCompatibleFeatures : ushort {
        /// <summary>
        /// Indicates that not all block groups contain a backup superblock.
        /// </summary>
        SparseSuperblock = 0x0001,

        /// <summary>
        /// Indicates file system contains files greater than 0x7FFFFFFF in size (limit of unsigned uints).
        /// </summary>
        LargeFiles = 0x0002,

        /// <summary>
        /// Indicates BTree-style directories present (not used in mainline?).
        /// </summary>
        BtreeDirectory = 0x0004,

        /// <summary>
        /// Ext4 feature - support for storing huge files.
        /// </summary>
        HugeFile,

        /// <summary>
        /// Ext4 feature - checksum block group structures.
        /// </summary>
        GdtChecksum,

        /// <summary>
        /// Ext4 feature - Unknown.
        /// </summary>
        DirNlink,

        /// <summary>
        /// Ext4 feature - extra inode size.
        /// </summary>
        ExtraInodeSize
    }
}
