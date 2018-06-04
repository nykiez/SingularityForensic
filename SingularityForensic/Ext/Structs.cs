using SingularityForensic.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Ext {
    //超级块结构体;
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StSuperBlock {
        public uint s_inodes_count;              /* 文件系统总i-node数 */
        public uint s_blocks_count_lo;           /* 文件系统总块数 */
        public uint s_r_blocks_count_lo;         /* 为文件系统预保留块数 */
        public uint s_free_blocks_count_lo;      /* 空闲块数 */
        public uint s_free_inodes_count;         /* 空闲i-node数 */
        public uint s_first_data_block;          /* 0号块组起始块号 */
        public uint s_log_block_size;            /* Block size is 2 ^ (10 + s_log_block_size). n * 2 * 1024 (0=1K, 1=2K, 2=4K)*/
        public uint s_log_cluster_size;          /* Cluster size is (2 ^ s_log_cluster_size) blocks if bigalloc is enabled, zero otherwise.*/
        public uint s_blocks_per_group;          /* 每组块数 */
        public uint s_clusters_per_group;        /* Clusters per group, if bigalloc is enabled */
        public uint s_inodes_per_group;          /* 每组inode数 */
        public uint s_mtime;                   /* 最后挂载时间 */
        public uint s_wtime;                   /* 最后写入时间 */
        public ushort s_mnt_count;             /* 当前挂载数 */
        public ushort s_max_mnt_count;         /* 最大挂载数 */
        public ushort s_magic;                 /* 签名标志53EF */
        public ushort s_state;                 /* 文件系统状态 */
        public ushort s_errors;                /* 错误处理方式 */
        public ushort s_minor_rev_level;       /* 辅版本级别 */
        public uint s_lastcheck;             /* 最后一次性检查时间 */
        public uint s_checkinterval;         /* 一次性检查间隔时间 */
        public uint s_creator_os;                /* 创建本文件系统的操作系统 */
        public uint s_rev_level;             /* 主版本级别 */
        public ushort s_def_resuid;            /* 默认UID保留模块 */
        public ushort s_def_resgid;            /* 默认GDI保留模块 */
                                               /*
                                                * These fields are for EXT4_DYNAMIC_REV superblocks only.
                                                *
                                                * Note: the difference between the compatible feature set and
                                                * the incompatible feature set is that if there is a bit set
                                                * in the incompatible feature set that the kernel doesn't
                                                * know about, it should refuse to mount the filesystem.
                                                *
                                                * e2fsck's requirements are more strict; if it doesn't know
                                                * about a feature in either the compatible or incompatible
                                                * feature set, it must abort and not try to meddle with
                                                * things it doesn't understand...
                                                */
        public uint s_first_ino;             /* 第一个非保留inode */
        public ushort s_inode_size;                /* 每个inode结构大小 */
        public ushort s_block_group_nr;        /* 本超级块所在块组号 */
        public uint s_feature_compat;            /* 兼容性特征标志 */
        public uint s_feature_incompat;          /* 非兼容性特征标志 */
        public uint s_feature_ro_compat;     /* 只读兼容性特征标志 */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string s_uuid;                    /* 128-bit uuid for volume */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string s_volume_name;         /* 卷名 */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string s_last_mounted;            /* 最后挂载时间 */
        public uint s_algorithm_usage_bitmap;    /* 位图使用运算法则 */
                                                 /*
                                                  * Performance hints.  Directory preallocation should only
                                                  * happen if the EXT4_FEATURE_COMPAT_DIR_PREALLOC flag is on.
                                                  */
        public char s_prealloc_blocks;         /* 文件再分配块数*/
        public char s_prealloc_dir_blocks;     /* 目录再分配块数 */
        public ushort s_reserved_gdt_blocks;       /* Per group desc for online growth */
                                                   /*Journaling support valid if EXT4_FEATURE_COMPAT_HAS_JOURNAL set. */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string s_journal_uuid;            /* uuid of journal superblock */
        public uint s_journal_inum;              /* 日志的inode */
        public uint s_journal_dev;               /* device number of journal file */
        public uint s_last_orphan;               /* start of list of inodes to delete */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] s_hash_seed;              /* HTREE hash seed */
        public char s_def_hash_version;            /* Default hash version to use */
        public char s_jnl_backup_type;
        public ushort s_desc_size;             /* size of group descriptor */
        public uint s_default_mount_opts;
        public uint s_first_meta_bg;         /* First metablock block group */
        public uint s_mkfs_time;             /* When the filesystem was created */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public int[] s_jnl_blocks;            /* Backup of the journal inode */
                                              /* 64bit support valid if EXT4_FEATURE_COMPAT_64BIT */
        public uint s_blocks_count_hi;           /* Blocks count */
        public uint s_r_blocks_count_hi;     /* Reserved blocks count */
        public uint s_free_blocks_count_hi;      /* Free blocks count */
        public ushort s_min_extra_isize;       /* All inodes have at least # bytes */
        public ushort s_want_extra_isize;  /* New inodes should reserve # bytes */
        public uint s_flags;                 /* Miscellaneous flags */
        public ushort s_raid_stride;           /* RAID stride */
        public ushort s_mmp_update_interval;   /* # seconds to wait in MMP checking */
        public ulong s_mmp_block;      /* Block for multi-mount protection */
        public uint s_raid_stripe_width;     /* blocks on all data disks (N*stride)*/
        public char s_log_groups_per_flex;     /* FLEX_BG group size */
        public char s_checksum_type;           /* metadata checksum algorithm used */
        public char s_encryption_level;            /* versioning level for encryption */
        public char s_reserved_pad;                /* Padding to next 32bits */
        public ulong s_kbytes_written; /* nr of lifetime kilobytes written */
        public uint s_snapshot_inum;         /* Inode number of active snapshot */
        public uint s_snapshot_id;               /* sequential ID of active snapshot */
        public ulong s_snapshot_r_blocks_count; /* reserved blocks for active snapshot's future use */
        public uint s_snapshot_list;         /* inode number of the head of the on-disk snapshot list */
                                             //#define EXT4_S_ERR_START offsetof( struct ext4_super_block, s_error_count )
        public uint s_error_count;               /* number of fs errors */
        public uint s_first_error_time;          /* first time an error happened */
        public uint s_first_error_ino;           /* inode involved in first error */
        public ulong s_first_error_block;    /* block involved of first error */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string s_first_error_func;        /* function where the error happened */
        public uint s_first_error_line;          /* line number where error happened */
        public uint s_last_error_time;           /* most recent time of an error */
        public uint s_last_error_ino;            /* inode involved in last error */
        public uint s_last_error_line;           /* line number where error happened */
        public ulong s_last_error_block;    /* block involved of last error */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string s_last_error_func; /* function where the error happened */
                                         //#define EXT4_S_ERR_END offsetof( struct ext4_super_block, s_mount_opts )
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string s_mount_opts;
        public uint s_usr_quota_inum;    /* inode for tracking user quota */
        public uint s_grp_quota_inum;    /* inode for tracking group quota */
        public uint s_overhead_clusters; /* overhead blocks/clusters in fs */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] s_backup_bgs; /* groups with sparse_super2 SBs */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string s_encrypt_algos;    /* Encryption algorithms in use  */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string s_encrypt_pw_salt; /* Salt used for string2key algorithm */
        public uint s_lpf_ino;       /* Location of the lost+found inode */
        public uint s_prj_quota_inum;    /* inode for tracking project quota */
        public uint s_checksum_seed; /* crc32c(uuid) if csum_seed set */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 98)]
        public int[] s_reserved;      /* Padding to the end of the block */
        public uint s_checksum;      /* crc32c(superblock) */

        public long BlockSize => s_log_block_size * 2048;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StExtGroupDesc {
        public uint bg_block_bitmap_lo;              /* Blocks bitmap block */
        public uint bg_inode_bitmap_lo;              /* Inodes bitmap block */
        public uint bg_inode_table_lo;               /* Inodes table block */
        public ushort bg_free_blocks_count_lo;     /* Free blocks count */
        public ushort bg_free_inodes_count_lo;     /* Free inodes count */
        public ushort bg_used_dirs_count_lo;           /* Directories count */
        public ushort bg_flags;                        /* EXT4_BG_flags (INODE_UNINIT, etc) */
        public uint bg_exclude_bitmap_lo;            /* Exclude bitmap for snapshots */
        public ushort bg_block_bitmap_csum_lo;     /* crc32c(s_uuid+grp_num+bbitmap) LE */
        public ushort bg_inode_bitmap_csum_lo;     /* crc32c(s_uuid+grp_num+ibitmap) LE */
        public ushort bg_itable_unused_lo;         /* Unused inodes count */
        public ushort bg_checksum;                 /* crc16(sb_uuid+group+desc) */
        //#if 0    //下面的32位是给64位预留的,只有当64位标记可用和超级块中s_desc_size > 32才能到下面的
        //    uint bg_block_bitmap_hi;                /* Blocks bitmap block MSB */
        //    uint bg_inode_bitmap_hi;                /* Inodes bitmap block MSB */
        //    uint bg_inode_table_hi;                /* Inodes table block MSB */
        //    ushort bg_free_blocks_count_hi;        /* Free blocks count MSB */
        //    ushort bg_free_inodes_count_hi;        /* Free inodes count MSB */
        //    ushort bg_used_dirs_count_hi;            /* Directories count MSB */
        //    ushort bg_itable_unused_hi;            /* Unused inodes count MSB */
        //    uint bg_exclude_bitmap_hi;            /* Exclude bitmap block MSB */
        //    ushort bg_block_bitmap_csum_hi;        /* crc32c(s_uuid+grp_num+bbitmap) BE */
        //    ushort bg_inode_bitmap_csum_hi;        /* crc32c(s_uuid+grp_num+ibitmap) BE */
        //    uint bg_reserved;
        //#endif
        public IntPtr Next;    //Ext4GroupDescTag* next;
        public IntPtr Pre; //Ext4GroupDescTag* pre;
    }

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
        //    {
        //        struct

        //        {

        //            ushort l_i_blocks_high; /* were l_i_reserved1 */
        //            ushort l_i_file_acl_high;
        //            ushort l_i_uid_high;    /* these 2 fields */
        //            ushort l_i_gid_high;    /* were reserved2[0] */
        //            ushort l_i_checksum_lo;/* crc32c(uuid+inum+inode) LE */
        //            ushort l_i_reserved;
        //        }linux2;
        //        struct
        //        {

        //            ushort h_i_reserved1;   /* Obsoleted fragment number/size which are removed in ext4 */
        //            ushort h_i_mode_high;
        //            ushort h_i_uid_high;
        //            ushort h_i_gid_high;
        //            uint h_i_author;
        //        }hurd2;
        //        struct
        //        {

        //            ushort h_i_reserved1;   /* Obsoleted fragment number/size which are removed in ext4 */
        //            ushort m_i_file_acl_high;
        //            uint m_i_reserved2[2];
        //        }masix2;
        //    }osd2;                /* OS dependent 2 */

        public ushort i_extra_isize;
        public ushort i_checksum_hi;   /* crc32c(uuid+inum+inode) BE */
        public uint i_ctime_extra;  /* extra Change time      (nsec << 2 | epoch) */
        public uint i_mtime_extra;  /* extra Modification time(nsec << 2 | epoch) */
        public uint i_atime_extra;  /* extra Access time      (nsec << 2 | epoch) */
        public uint i_crtime;       /* File Creation time */
        public uint i_crtime_extra; /* extra FileCreationtime (nsec << 2 | epoch) */
        public uint i_version_hi;    /* high 32 bits for 64-bit version */
        public uint i_projid;    /* Project ID */


        private static readonly DateTime InitTime = DateTime.Parse("1970/01/01").AddHours(8);
        public void GetMacTime(out DateTime? modifiedTime, out DateTime? accessedTime, out DateTime? createTime) {
            modifiedTime = InitTime.AddSeconds(i_mtime);
            createTime = InitTime.AddSeconds(i_crtime);
            accessedTime = InitTime.AddSeconds(i_atime);
        }

        public long Size => i_size_lo + i_size_high << 32;
    }

    //每个文件对应的块列表;
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct StBlockList {
        public ulong address;
        public uint count;
        public IntPtr Next;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct StDirEntry {
        public IntPtr DirInfo;      /* 目录表项信息 StExt4_Dir_Dntry**/
        [MarshalAs(UnmanagedType.I1)]
        public bool bDel;           /*是否已被删除*/
        public IntPtr Next;
        public IntPtr Pre;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct StExt4DirEntry {
        public uint inode;                                      /* inode号 */
        public ushort rec_len;                                  /* 本目录项长度字节数 */
        public byte name_len;                                   /* 目录名字字节数 */
        public Ext4FileType file_type;                                  /* 文件类型 */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string name;                                     /* File name */

        
    }
    /*文件类型
    Unknown,                0未知文件;
    RegularFile,            1常规文件;
    Directory,              2目录;
    CharacterDeviceFile,    3挂载设备文件;
    BlockDeviceFile,        4块设备文件;
    FIFO,                   5管道通信;
    Socket,                 6套接字;
    SymbolicLink            7快捷方式;
    */
    public enum Ext4FileType : byte {
        Unknown,                //0未知文件;
        RegularFile,            //1常规文件;
        Directory,              //2目录;
        CharacterDeviceFile,    //3挂载设备文件;
        BlockDeviceFile,        //4块设备文件;
        FIFO,                   //5管道通信;
        Socket,                 //6套接字;
        SymbolicLink            //7快捷方式;
    }
}
