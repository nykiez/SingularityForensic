
/**
 * 模块 :
 */

#ifndef __CDFCQD_H__
#define __CDFCQD_H__

#define EXT4_N_BLOCKS 15
#define Tranverse16( X ) ( ( ( ( int16_t )( X ) & 0xff00 ) >> 8 ) | ( ( ( int16_t )( X ) & 0x00ff ) << 8 ) )
#define Tranverse32( X ) ( ( ( ( UINT32)( X ) & 0xff000000 ) >> 24 ) | ( ( ( UINT32 )( X ) & 0x00ff0000 ) >> 8) | ( ( ( UINT32 )( X ) & 0x0000ff00 ) << 8 ) | ( ( ( UINT32 )( X ) & 0x000000ff ) << 24 ) )

typedef	enum FsType_Enum
{
};

/*
DOS 保护MBR:
*/
#pragma pack(1)
typedef struct MbrInfoTag
{
	char Unknown[450];
	char	EELogo;
	char Unknown0[7];
	UINT32 DiskSize;
	char Unknown1[48];
	unsigned short AA55Logo;
}StMbrInfo;
#pragma pack()

/*
EFI 信息:
*/
#pragma pack(1)
typedef struct EFIInfoTag
{
	unsigned __int64 EFIPART;						//EFI签名(EFI PART)		8 byte
	UINT32 Version;									//版本					4 byte
	UINT32 EFISize;									//EFI信息大小字节数		4 byte
	UINT32 CRC;										//EFI信息 CRC校验和		4 byte
	UINT32 Unknown;									//保留					4 byte
	unsigned __int64 EFICurrSecNum;					//当前EFI LBA扇区号		8 byte
	unsigned __int64 EFIBackupSecNum;				//备份EFI LBA扇区号		8 byte
	unsigned __int64 GPTStartLBA;					//GPT分区区域起始LBA	8 byte
	unsigned __int64 GPTEndLBA;						//GPT分区区域结束LBA	8 byte
	char DiskGUID[16];							//磁盘GUID				16 byte
	unsigned __int64 GPTPartTabStartLBA;			//备份EFI LBA扇区号		8 byte
	UINT32 PartTabCount;							//分区表项数			4 byte
	UINT32 PartTabCRC;								//分区表CRC校验和		4 byte
}StEFIInfo;
#pragma pack()

/*
GPT分区表项	128字节
*/
#pragma pack(1)
typedef struct PartInfoTag
{
	char PartTabType[16];				//分区类型GUID		16 byte
	char PartTabOnly[16];				//分区唯一GUID		16 byte
	unsigned __int64 PartTabStartLBA;		//分区起始LAB		8 byte
	unsigned __int64 PartTabEndLBA;			//分区结束LAB		8 byte
	unsigned __int64 PartTabProp;			//分区属性			8 byte
	char PartTabNameUnicode[72];			//分区名unicode码	72 byte
}StPartInfo;
#pragma pack()

#pragma pack(1)
typedef struct Ext4SuperBlockTAG
{
	UINT32 s_inodes_count;				/* 文件系统总i-node数 */
	UINT32 s_blocks_count_lo;			/* 文件系统总块数 */
	UINT32	s_r_blocks_count_lo;		/* 为文件系统预保留块数 */
	UINT32	s_free_blocks_count_lo;		/* 空闲块数 */
	UINT32 s_free_inodes_count;			/* 空闲i-node数 */
	UINT32	s_first_data_block;			/* 0号块组起始块号 */
	UINT32	s_log_block_size;			/* Block size is 2 ^ (10 + s_log_block_size). n * 2 * 1024 (0=1K, 1=2K, 2=4K)*/
	UINT32	s_log_cluster_size;			/* Cluster size is (2 ^ s_log_cluster_size) blocks if bigalloc is enabled, zero otherwise.*/
	UINT32	s_blocks_per_group;			/* 每组块数 */
	UINT32	s_clusters_per_group;		/* Clusters per group, if bigalloc is enabled */
	UINT32	s_inodes_per_group;			/* 每组inode数 */
	UINT32	s_mtime;					/* 最后挂载时间 */
	UINT32	s_wtime;					/* 最后写入时间 */
	unsigned short	s_mnt_count;			/* 当前挂载数 */
	unsigned short	s_max_mnt_count;		/* 最大挂载数 */
	unsigned short	s_magic;				/* 签名标志53EF */
	unsigned short	s_state;				/* 文件系统状态 */
	unsigned short	s_errors;				/* 错误处理方式 */
	unsigned short	s_minor_rev_level;		/* 辅版本级别 */
	UINT32	s_lastcheck;				/* 最后一次性检查时间 */
	UINT32	s_checkinterval;			/* 一次性检查间隔时间 */
	UINT32	s_creator_os;				/* 创建本文件系统的操作系统 */
	UINT32	s_rev_level;				/* 主版本级别 */
	unsigned short	s_def_resuid;			/* 默认UID保留模块 */
	unsigned short	s_def_resgid;			/* 默认GDI保留模块 */
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
	UINT32	s_first_ino;				/* 第一个非保留inode */
	unsigned short  s_inode_size;				/* 每个inode结构大小 */
	unsigned short	s_block_group_nr;		/* 本超级块所在块组号 */
	UINT32	s_feature_compat;			/* 兼容性特征标志 */
	UINT32	s_feature_incompat;			/* 非兼容性特征标志 */
	UINT32	s_feature_ro_compat;		/* 只读兼容性特征标志 */
	char	s_uuid[16];					/* 128-bit uuid for volume */
	char	s_volume_name[16];			/* 卷名 */
	char	s_last_mounted[64];			/* 最后挂载时间 */
	UINT32	s_algorithm_usage_bitmap;	/* 位图使用运算法则 */
	/*
	 * Performance hints.  Directory preallocation should only
	 * happen if the EXT4_FEATURE_COMPAT_DIR_PREALLOC flag is on.
	 */
	char s_prealloc_blocks;			/* 文件再分配块数*/
	char s_prealloc_dir_blocks;		/* 目录再分配块数 */
	unsigned short s_reserved_gdt_blocks;		/* Per group desc for online growth */
	/*Journaling support valid if EXT4_FEATURE_COMPAT_HAS_JOURNAL set. */
	char s_journal_uuid[16];			/* uuid of journal superblock */
	UINT32	s_journal_inum;				/* 日志的inode */
	UINT32	s_journal_dev;				/* device number of journal file */
	UINT32	s_last_orphan;				/* start of list of inodes to delete */
	UINT32	s_hash_seed[4];				/* HTREE hash seed */
	char s_def_hash_version;			/* Default hash version to use */
	char s_jnl_backup_type;
	unsigned short s_desc_size;				/* size of group descriptor */
	UINT32 s_default_mount_opts;
	UINT32	s_first_meta_bg;			/* First metablock block group */
	UINT32	s_mkfs_time;				/* When the filesystem was created */
	UINT32	s_jnl_blocks[17];			/* Backup of the journal inode */
	/* 64bit support valid if EXT4_FEATURE_COMPAT_64BIT */
	UINT32	s_blocks_count_hi;			/* Blocks count */
	UINT32	s_r_blocks_count_hi;		/* Reserved blocks count */
	UINT32	s_free_blocks_count_hi;		/* Free blocks count */
	unsigned short	s_min_extra_isize;		/* All inodes have at least # bytes */
	unsigned short	s_want_extra_isize; 	/* New inodes should reserve # bytes */
	UINT32	s_flags;					/* Miscellaneous flags */
	unsigned short  s_raid_stride;			/* RAID stride */
	unsigned short  s_mmp_update_interval;	/* # seconds to wait in MMP checking */
	unsigned __int64  s_mmp_block;      /* Block for multi-mount protection */
	UINT32  s_raid_stripe_width;		/* blocks on all data disks (N*stride)*/
	char	s_log_groups_per_flex;		/* FLEX_BG group size */
	char	s_checksum_type;			/* metadata checksum algorithm used */
	char	s_encryption_level;			/* versioning level for encryption */
	char	s_reserved_pad;				/* Padding to next 32bits */
	unsigned __int64	s_kbytes_written;	/* nr of lifetime kilobytes written */
	UINT32	s_snapshot_inum;			/* Inode number of active snapshot */
	UINT32	s_snapshot_id;				/* sequential ID of active snapshot */
	unsigned __int64	s_snapshot_r_blocks_count; /* reserved blocks for active snapshot's future use */
	UINT32	s_snapshot_list;			/* inode number of the head of the on-disk snapshot list */
#define EXT4_S_ERR_START offsetof( struct ext4_super_block, s_error_count )
	UINT32	s_error_count;				/* number of fs errors */
	UINT32	s_first_error_time;			/* first time an error happened */
	UINT32	s_first_error_ino;			/* inode involved in first error */
	unsigned __int64	s_first_error_block;	/* block involved of first error */
	char	s_first_error_func[32];		/* function where the error happened */
	UINT32	s_first_error_line;			/* line number where error happened */
	UINT32	s_last_error_time;			/* most recent time of an error */
	UINT32	s_last_error_ino;			/* inode involved in last error */
	UINT32	s_last_error_line;			/* line number where error happened */
	unsigned __int64	s_last_error_block;	/* block involved of last error */
	char	s_last_error_func[32];	/* function where the error happened */
#define EXT4_S_ERR_END offsetof( struct ext4_super_block, s_mount_opts )
	char	s_mount_opts[64];
	UINT32	s_usr_quota_inum;	/* inode for tracking user quota */
	UINT32	s_grp_quota_inum;	/* inode for tracking group quota */
	UINT32	s_overhead_clusters;	/* overhead blocks/clusters in fs */
	UINT32	s_backup_bgs[2];	/* groups with sparse_super2 SBs */
	char	s_encrypt_algos[4];	/* Encryption algorithms in use  */
	char	s_encrypt_pw_salt[16];	/* Salt used for string2key algorithm */
	UINT32	s_lpf_ino;		/* Location of the lost+found inode */
	UINT32	s_prj_quota_inum;	/* inode for tracking project quota */
	UINT32	s_checksum_seed;	/* crc32c(uuid) if csum_seed set */
	UINT32	s_reserved[98];		/* Padding to the end of the block */
	UINT32	s_checksum;		/* crc32c(superblock) */
}StExt4SuperBlock;
#pragma pack()

#pragma pack( 1 )
typedef struct Ext4GroupDescTag
{
	UINT32 bg_block_bitmap_lo;				/* Blocks bitmap block */
	UINT32 bg_inode_bitmap_lo;				/* Inodes bitmap block */
	UINT32 bg_inode_table_lo;				/* Inodes table block */
	unsigned short bg_free_blocks_count_lo;		/* Free blocks count */
	unsigned short bg_free_inodes_count_lo;		/* Free inodes count */
	unsigned short bg_used_dirs_count_lo;			/* Directories count */
	unsigned short bg_flags;						/* EXT4_BG_flags (INODE_UNINIT, etc) */
	UINT32 bg_exclude_bitmap_lo;			/* Exclude bitmap for snapshots */
	unsigned short bg_block_bitmap_csum_lo;		/* crc32c(s_uuid+grp_num+bbitmap) LE */
	unsigned short bg_inode_bitmap_csum_lo;		/* crc32c(s_uuid+grp_num+ibitmap) LE */
	unsigned short bg_itable_unused_lo;			/* Unused inodes count */
	unsigned short bg_checksum;					/* crc16(sb_uuid+group+desc) */
#if 0	//下面的32位是给64位预留的,只有当64位标记可用和超级块中s_desc_size > 32才能到下面的
	UINT32 bg_block_bitmap_hi;				/* Blocks bitmap block MSB */
	UINT32 bg_inode_bitmap_hi;				/* Inodes bitmap block MSB */
	UINT32 bg_inode_table_hi;				/* Inodes table block MSB */
	unsigned short bg_free_blocks_count_hi;		/* Free blocks count MSB */
	unsigned short bg_free_inodes_count_hi;		/* Free inodes count MSB */
	unsigned short bg_used_dirs_count_hi;			/* Directories count MSB */
	unsigned short bg_itable_unused_hi;			/* Unused inodes count MSB */
	UINT32 bg_exclude_bitmap_hi;			/* Exclude bitmap block MSB */
	unsigned short bg_block_bitmap_csum_hi;		/* crc32c(s_uuid+grp_num+bbitmap) BE */
	unsigned short bg_inode_bitmap_csum_hi;		/* crc32c(s_uuid+grp_num+ibitmap) BE */
	UINT32 bg_reserved;
#endif
	Ext4GroupDescTag *next;
	Ext4GroupDescTag *pre;
}StExt4GroupDesc;
#pragma pack()

#pragma pack( 1 )
typedef struct Ext4InodeTag
{
	unsigned short i_mode;		/* File mode */
	unsigned short i_uid;		/* Low 16 bits of Owner Uid */
	UINT32 i_size_lo;	/* Size in bytes */
	UINT32 i_atime;	/* Access time */
	UINT32 i_ctime;	/* Inode Change time */
	UINT32 i_mtime;	/* Modification time */
	UINT32 i_dtime;	/* Deletion Time */
	unsigned short i_gid;		/* Low 16 bits of Group Id */
	unsigned short i_links_count;	/* Links count */
	UINT32 i_blocks_lo;	/* Blocks count */
	UINT32 i_flags;	/* File flags */
	union
	{
		struct
		{
			UINT32  l_i_version;
		}linux1;
		struct
		{
			UINT32  h_i_translator;
		}hurd1;
		struct
		{
			UINT32  m_i_reserved1;
		}masix1;
	}osd1;				/* OS dependent 1 */
	UINT32 i_block[EXT4_N_BLOCKS];/* Pointers to blocks */
	UINT32 i_generation;	/* File version (for NFS) */
	UINT32 i_file_acl_lo;	/* File ACL */
	UINT32 i_size_high;
	UINT32 i_obso_faddr;	/* Obsoleted fragment address */
	union
	{
		struct
		{
			unsigned short l_i_blocks_high; /* were l_i_reserved1 */
			unsigned short l_i_file_acl_high;
			unsigned short l_i_uid_high;	/* these 2 fields */
			unsigned short l_i_gid_high;	/* were reserved2[0] */
			unsigned short l_i_checksum_lo;/* crc32c(uuid+inum+inode) LE */
			unsigned short l_i_reserved;
		}linux2;
		struct
		{
			unsigned short h_i_reserved1;	/* Obsoleted fragment number/size which are removed in ext4 */
			unsigned short h_i_mode_high;
			unsigned short h_i_uid_high;
			unsigned short h_i_gid_high;
			UINT32 h_i_author;
		}hurd2;
		struct
		{
			unsigned short	h_i_reserved1;	/* Obsoleted fragment number/size which are removed in ext4 */
			unsigned short	m_i_file_acl_high;
			UINT32	m_i_reserved2[2];
		}masix2;
	}osd2;				/* OS dependent 2 */

	unsigned short i_extra_isize;
	unsigned short i_checksum_hi;	/* crc32c(uuid+inum+inode) BE */
	UINT32 i_ctime_extra;  /* extra Change time      (nsec << 2 | epoch) */
	UINT32 i_mtime_extra;  /* extra Modification time(nsec << 2 | epoch) */
	UINT32 i_atime_extra;  /* extra Access time      (nsec << 2 | epoch) */
	UINT32 i_crtime;       /* File Creation time */
	UINT32 i_crtime_extra; /* extra FileCreationtime (nsec << 2 | epoch) */
	UINT32 i_version_hi;	/* high 32 bits for 64-bit version */
	UINT32 i_projid;	/* Project ID */
}StExt4Inode;
#pragma pack()

#pragma pack(1)
typedef struct ext4_extent
{
	UINT32 ee_block;	/* first logical block extent covers (这个extent包含的第一个逻辑块)*/
	unsigned short ee_len;		/* number of blocks covered by extent (extent包含的块数量)*/
	unsigned short ee_start_hi;	/* high 16 bits of physical block 这个extent指向的高16位*/
	UINT32 ee_start_lo;	/* low 32 bits of physical block 这个extent指向的低32位*/
}StExt4_Extent;
#pragma pack()

/*
 * This is index on-disk structure.
 * It's used at all the levels except the bottom.
 */
#pragma pack(1)
typedef struct ext4_extent_idx
{
	UINT32 ei_block;	/* index covers logical blocks from 'block'  (它包含这个"block"里面的逻辑块)*/
	UINT32 ei_leaf_lo;	/* pointer to the physical block of the next * * level. leaf or next index could be there */
	unsigned short ei_leaf_hi;	/* high 16 bits of physical block */
	unsigned short ei_unused;
}StExt4_Extent_Idx;
#pragma pack()

/*
 * Each block (leaves and indexes), even inode-stored has header.
 */
#pragma pack(1)
typedef struct ext4_extent_header
{
	unsigned short eh_magic;	/* probably will support different formats */
	unsigned short eh_entries;	/* number of valid entries 这个头的可用节点数*/
	unsigned short eh_max;		/* capacity of store in entries 这个头下最大可用节点数*/
	unsigned short eh_depth;	/* has tree real underlying blocks? */
	UINT32 eh_generation;	/* generation of the tree */
}StExt4_Extent_Header;
#pragma pack()

/*
分区表和分区信息
*/
#pragma pack(1)
typedef struct PartTabInfoTag
{
	StPartInfo *PartInfo;				//分区表项
	StExt4SuperBlock *Ext4SuperBlock;	//本分区超级块
	StExt4GroupDesc *Ext4GroupDesc;		//本分区组描述符表
	PartTabInfoTag *next;
	PartTabInfoTag *pre;
}StPartTabInfo;
#pragma pack()

/*
GPT分区表项	128字节
*/
#pragma pack(1)
typedef struct PhoneDiskMgrTag
{
	StMbrInfo *MbrInfo;					//保护MBR信息
	StEFIInfo *EFIInfo;					//EFI信息
	StPartTabInfo *PartTabInfo;			//分区表链表
}StDiskInfo;
#pragma pack()

/*
*/
#pragma pack(1)
typedef struct Ext4_Dir_DntryTag
{
	UINT32 inode;				/* inode号 */
	unsigned short rec_len;		/* 本目录项长度字节数 */
	char name_len;				/* 目录名字字节数 */
	char file_type;				/*  */
	char name[255];				/* File name */
}StExt4_Dir_Dntry;
#pragma pack()

/*
每个文件对应的块链表
*/
#pragma pack(1)
typedef struct BlockListTag
{
	unsigned __int64 address;
	BlockListTag *next;
}StBlockList;
#pragma pack()

/*
每个文件对应的块链表
*/
#pragma pack(1)
typedef struct FileInfoTag
{
	StExt4Inode *InodeInfo;			/* 文件inode信息 */
	StBlockList *BlockList;
}StFileInfo;
#pragma pack()

/*
每个文件对应的块链表
*/
#pragma pack(1)
typedef struct DirDntryTag
{
	StExt4_Dir_Dntry *DirInfo;		/* 目录表项信息 */
	bool bDel;
	DirDntryTag *next;
	DirDnytyTag *pre;
}StDirDntry;
#pragma pack()

typedef struct DirDntryListTag
{
	StDirDntry **stDirDntry;
	DirDntryListTag *next;
}StDirDntryList;
#pragma pack()

/*
功能：初始化镜像分区
返回：分区链表
*/
StDiskInfo *Cflabqd_Init( HANDLE H_Disk );

/*
功能：初始化分区指针，对选中双击的分区做调用
返回：分区链表
*/
void Cflabqd_Partition_Init( StPartTabInfo *ST_PartTabInfo );

/*
功能：解析目录
stBlockList：目录块链表，通过调用Cflabqd_Get_BlockList获取
返回：目录链表
*/
StDirDntry *Cflabqd_Parse_Dir( StBlockList *stBlockList );

/*
功能：释放目录链表
stBlockList：目录块链表
*/

void Cflabqd_Dir_Free( StDirDntry *ST_DirDntry );

/*
功能：获取inode详细信息
N_Inode：inode节点号
返回：inode节点结构体
*/
StExt4Inode *Cflabqd_Get_InodeInfo( UINT32 N_Inode );

/*
功能：释放inode节点
ST_Ext4Inode：inode节点
*/
void Cflabqd_InodeInfo_Free( StExt4Inode *ST_Ext4Inode );

/*
功能：解析出inode中的块链表
ST_Ext4Inode：inode节点结构体
返回：inode中的块链表
*/
StBlockList *Cflabqd_Get_BlockList( StExt4Inode *ST_Ext4Inode );

/*
功能：释放块链表
ST_BlockList：块链表
*/
void Cflabqd_BlockList_Free( StBlockList *ST_BlockList );

/*
功能：读
返回实际读到的大小
*/
DWORD Cflabqd_Read( HANDLE H_Disk, unsigned __int64 N_Offset, char *SZ_Buffer, unsigned __int64 N_BufferSize );


#endif /*__CDFCQD_H__*/