using System;
using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StExt4Inode {
        public ushort i_mode;      /* File mode */
        public ushort i_uid;       /* Low 16 bits of Owner Uid */
        public uint i_size_lo;   /* Size in bytes */
        public uint i_atime; /* Access time */
        public uint i_ctime; /* Inode Change time */
        public uint i_mtime; /* Modification time */
        public uint i_dtime; /* Deletion Time */
        public ushort i_gid;       /* Low 16 bits of Group Id */
        public ushort i_links_count;   /* Links count */
        public uint i_blocks_lo; /* Blocks count */
        public uint i_flags; /* File flags */
        public uint osversion;     /* OS dependent 1 */
                                   //[MarshalAs(UnmanagedType.SysUInt, SizeConst = 15,ArraySubType = UnmanagedType.U4)]
                                   //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
                                   //public int[] i_block;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public int[] i_block;/* Pointers to blocks */
        public uint i_generation;    /* File version (for NFS) */
        public uint i_file_acl_lo;   /* File ACL */
        public uint i_size_high;
        public uint i_obso_faddr;    /* Obsoleted fragment address */

        public ushort l_i_blocks_high; /* were l_i_reserved1 */
        public ushort l_i_file_acl_high;
        public ushort l_i_uid_high;    /* these 2 fields */
        public ushort l_i_gid_high;    /* were reserved2[0] */
        public ushort l_i_checksum_lo;/* crc32c(uuid+inum+inode) LE */
        public ushort l_i_reserved;

        //ushort h_i_reserved1;   /* Obsoleted fragment number/size which are removed in ext4 */
        //ushort h_i_mode_high;
        //ushort h_i_uid_high;
        //ushort h_i_gid_high;
        //uint h_i_author;

        //ushort h_i_reserved1;   /* Obsoleted fragment number/size which are removed in ext4 */
        //ushort m_i_file_acl_high;
        //uint[2] m_i_reserved2;
        //union
        //	{
        //		struct

        //        {

        //            ushort l_i_blocks_high; /* were l_i_reserved1 */
        //            ushort l_i_file_acl_high;
        //            ushort l_i_uid_high;    /* these 2 fields */
        //            ushort l_i_gid_high;    /* were reserved2[0] */
        //            ushort l_i_checksum_lo;/* crc32c(uuid+inum+inode) LE */
        //            ushort l_i_reserved;
        //		}linux2;
        //		struct
        //        {

        //            ushort h_i_reserved1;   /* Obsoleted fragment number/size which are removed in ext4 */
        //            ushort h_i_mode_high;
        //            ushort h_i_uid_high;
        //            ushort h_i_gid_high;
        //            uint h_i_author;
        //		}hurd2;
        //		struct
        //        {

        //            ushort h_i_reserved1;   /* Obsoleted fragment number/size which are removed in ext4 */
        //            ushort m_i_file_acl_high;
        //            uint m_i_reserved2[2];
        //		}masix2;
        //	}osd2;				/* OS dependent 2 */

        public ushort i_extra_isize;
        public ushort i_checksum_hi;   /* crc32c(uuid+inum+inode) BE */
        public uint i_ctime_extra;  /* extra Change time      (nsec << 2 | epoch) */
        public uint i_mtime_extra;  /* extra Modification time(nsec << 2 | epoch) */
        public uint i_atime_extra;  /* extra Access time      (nsec << 2 | epoch) */
        public uint i_crtime;       /* File Creation time */
        public uint i_crtime_extra; /* extra FileCreationtime (nsec << 2 | epoch) */
        public uint i_version_hi;    /* high 32 bits for 64-bit version */
        public uint i_projid;	/* Project ID */
        
    }

    public static class InodeHelper {
        private static DateTime InitTime = DateTime.Parse("1970/01/01").AddHours(8);
        public static void GetMacTime(this StExt4Inode inode,out DateTime? modifiedTime,out DateTime? accessedTime,out DateTime? createTime) {
            modifiedTime = InitTime.AddSeconds(inode.i_mtime) ;
            createTime = InitTime.AddSeconds(inode.i_crtime) ;
            accessedTime =  InitTime.AddSeconds(inode.i_atime) ;
        }
    }

    /// <summary>
    ///Inode 权限标志；
    /// </summary>
    [Flags]
    public enum INodePermission {
        Unknown = 0x000,                    //未知

        OtherCanExe = 0x001,                // 其他用户可执行
        OtherCanWrite = 0x002,              //其他用户可写
        OtherCanRead = 0x004,                //其他用户可读

        SibingCanExe = 0x008,                  //同组用户可执行
        SiblingCanWrite = 0x010,                  //同组用户可写
        SiblingCanRead = 0x020,                  //同组用户可读

        OwnerCanExe = 0x040,                  //属主可执行
        OwnerCanWrite = 0x080,                  //属主可写
        OwnerCanRead = 0x100,                  //属主可读

    }
}
