using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Ext {
    /// <summary>
    /// Feature flags for backwards compatible features.
    /// </summary>
    [Flags]
    public enum CompatibleFeatures : ushort {
        /// <summary>
        /// Indicates pre-allocation hints are present.
        /// </summary>
        DirectoryPreallocation = 0x0001,

        /// <summary>
        /// AFS support in inodex.
        /// </summary>
        IMagicInodes = 0x0002,

        /// <summary>
        /// Indicates an EXT3-style journal is present.
        /// </summary>
        HasJournal = 0x0004,

        /// <summary>
        /// Indicates extended attributes (e.g. FileACLs) are present.
        /// </summary>
        ExtendedAttributes = 0x0008,

        /// <summary>
        /// Indicates space is reserved through a special inode to enable the file system to be resized dynamically.
        /// </summary>
        ResizeInode = 0x0010,

        /// <summary>
        /// Indicates that directory indexes are present (not used in mainline?).
        /// </summary>
        DirectoryIndex = 0x0020
    }
}
