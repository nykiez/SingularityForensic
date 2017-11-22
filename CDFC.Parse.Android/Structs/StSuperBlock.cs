using System.Runtime.InteropServices;

namespace CDFC.Parse.Android.Structs {
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
        public ulong s_first_error_block;	/* block involved of first error */
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string s_first_error_func;        /* function where the error happened */
        public uint s_first_error_line;          /* line number where error happened */
        public uint s_last_error_time;           /* most recent time of an error */
        public uint s_last_error_ino;            /* inode involved in last error */
        public uint s_last_error_line;           /* line number where error happened */
        public ulong s_last_error_block;	/* block involved of last error */
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
    }
}
