
/**
 * ģ�� :
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
DOS ����MBR:
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
EFI ��Ϣ:
*/
#pragma pack(1)
typedef struct EFIInfoTag
{
	unsigned __int64 EFIPART;						//EFIǩ��(EFI PART)		8 byte
	UINT32 Version;									//�汾					4 byte
	UINT32 EFISize;									//EFI��Ϣ��С�ֽ���		4 byte
	UINT32 CRC;										//EFI��Ϣ CRCУ���		4 byte
	UINT32 Unknown;									//����					4 byte
	unsigned __int64 EFICurrSecNum;					//��ǰEFI LBA������		8 byte
	unsigned __int64 EFIBackupSecNum;				//����EFI LBA������		8 byte
	unsigned __int64 GPTStartLBA;					//GPT����������ʼLBA	8 byte
	unsigned __int64 GPTEndLBA;						//GPT�����������LBA	8 byte
	char DiskGUID[16];							//����GUID				16 byte
	unsigned __int64 GPTPartTabStartLBA;			//����EFI LBA������		8 byte
	UINT32 PartTabCount;							//����������			4 byte
	UINT32 PartTabCRC;								//������CRCУ���		4 byte
}StEFIInfo;
#pragma pack()

/*
GPT��������	128�ֽ�
*/
#pragma pack(1)
typedef struct PartInfoTag
{
	char PartTabType[16];				//��������GUID		16 byte
	char PartTabOnly[16];				//����ΨһGUID		16 byte
	unsigned __int64 PartTabStartLBA;		//������ʼLAB		8 byte
	unsigned __int64 PartTabEndLBA;			//��������LAB		8 byte
	unsigned __int64 PartTabProp;			//��������			8 byte
	char PartTabNameUnicode[72];			//������unicode��	72 byte
}StPartInfo;
#pragma pack()

#pragma pack(1)
typedef struct Ext4SuperBlockTAG
{
	UINT32 s_inodes_count;				/* �ļ�ϵͳ��i-node�� */
	UINT32 s_blocks_count_lo;			/* �ļ�ϵͳ�ܿ��� */
	UINT32	s_r_blocks_count_lo;		/* Ϊ�ļ�ϵͳԤ�������� */
	UINT32	s_free_blocks_count_lo;		/* ���п��� */
	UINT32 s_free_inodes_count;			/* ����i-node�� */
	UINT32	s_first_data_block;			/* 0�ſ�����ʼ��� */
	UINT32	s_log_block_size;			/* Block size is 2 ^ (10 + s_log_block_size). n * 2 * 1024 (0=1K, 1=2K, 2=4K)*/
	UINT32	s_log_cluster_size;			/* Cluster size is (2 ^ s_log_cluster_size) blocks if bigalloc is enabled, zero otherwise.*/
	UINT32	s_blocks_per_group;			/* ÿ����� */
	UINT32	s_clusters_per_group;		/* Clusters per group, if bigalloc is enabled */
	UINT32	s_inodes_per_group;			/* ÿ��inode�� */
	UINT32	s_mtime;					/* ������ʱ�� */
	UINT32	s_wtime;					/* ���д��ʱ�� */
	unsigned short	s_mnt_count;			/* ��ǰ������ */
	unsigned short	s_max_mnt_count;		/* �������� */
	unsigned short	s_magic;				/* ǩ����־53EF */
	unsigned short	s_state;				/* �ļ�ϵͳ״̬ */
	unsigned short	s_errors;				/* ������ʽ */
	unsigned short	s_minor_rev_level;		/* ���汾���� */
	UINT32	s_lastcheck;				/* ���һ���Լ��ʱ�� */
	UINT32	s_checkinterval;			/* һ���Լ����ʱ�� */
	UINT32	s_creator_os;				/* �������ļ�ϵͳ�Ĳ���ϵͳ */
	UINT32	s_rev_level;				/* ���汾���� */
	unsigned short	s_def_resuid;			/* Ĭ��UID����ģ�� */
	unsigned short	s_def_resgid;			/* Ĭ��GDI����ģ�� */
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
	UINT32	s_first_ino;				/* ��һ���Ǳ���inode */
	unsigned short  s_inode_size;				/* ÿ��inode�ṹ��С */
	unsigned short	s_block_group_nr;		/* �����������ڿ���� */
	UINT32	s_feature_compat;			/* ������������־ */
	UINT32	s_feature_incompat;			/* �Ǽ�����������־ */
	UINT32	s_feature_ro_compat;		/* ֻ��������������־ */
	char	s_uuid[16];					/* 128-bit uuid for volume */
	char	s_volume_name[16];			/* ���� */
	char	s_last_mounted[64];			/* ������ʱ�� */
	UINT32	s_algorithm_usage_bitmap;	/* λͼʹ�����㷨�� */
	/*
	 * Performance hints.  Directory preallocation should only
	 * happen if the EXT4_FEATURE_COMPAT_DIR_PREALLOC flag is on.
	 */
	char s_prealloc_blocks;			/* �ļ��ٷ������*/
	char s_prealloc_dir_blocks;		/* Ŀ¼�ٷ������ */
	unsigned short s_reserved_gdt_blocks;		/* Per group desc for online growth */
	/*Journaling support valid if EXT4_FEATURE_COMPAT_HAS_JOURNAL set. */
	char s_journal_uuid[16];			/* uuid of journal superblock */
	UINT32	s_journal_inum;				/* ��־��inode */
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
#if 0	//�����32λ�Ǹ�64λԤ����,ֻ�е�64λ��ǿ��úͳ�������s_desc_size > 32���ܵ������
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
	UINT32 ee_block;	/* first logical block extent covers (���extent�����ĵ�һ���߼���)*/
	unsigned short ee_len;		/* number of blocks covered by extent (extent�����Ŀ�����)*/
	unsigned short ee_start_hi;	/* high 16 bits of physical block ���extentָ��ĸ�16λ*/
	UINT32 ee_start_lo;	/* low 32 bits of physical block ���extentָ��ĵ�32λ*/
}StExt4_Extent;
#pragma pack()

/*
 * This is index on-disk structure.
 * It's used at all the levels except the bottom.
 */
#pragma pack(1)
typedef struct ext4_extent_idx
{
	UINT32 ei_block;	/* index covers logical blocks from 'block'  (���������"block"������߼���)*/
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
	unsigned short eh_entries;	/* number of valid entries ���ͷ�Ŀ��ýڵ���*/
	unsigned short eh_max;		/* capacity of store in entries ���ͷ�������ýڵ���*/
	unsigned short eh_depth;	/* has tree real underlying blocks? */
	UINT32 eh_generation;	/* generation of the tree */
}StExt4_Extent_Header;
#pragma pack()

/*
������ͷ�����Ϣ
*/
#pragma pack(1)
typedef struct PartTabInfoTag
{
	StPartInfo *PartInfo;				//��������
	StExt4SuperBlock *Ext4SuperBlock;	//������������
	StExt4GroupDesc *Ext4GroupDesc;		//����������������
	PartTabInfoTag *next;
	PartTabInfoTag *pre;
}StPartTabInfo;
#pragma pack()

/*
GPT��������	128�ֽ�
*/
#pragma pack(1)
typedef struct PhoneDiskMgrTag
{
	StMbrInfo *MbrInfo;					//����MBR��Ϣ
	StEFIInfo *EFIInfo;					//EFI��Ϣ
	StPartTabInfo *PartTabInfo;			//����������
}StDiskInfo;
#pragma pack()

/*
*/
#pragma pack(1)
typedef struct Ext4_Dir_DntryTag
{
	UINT32 inode;				/* inode�� */
	unsigned short rec_len;		/* ��Ŀ¼����ֽ��� */
	char name_len;				/* Ŀ¼�����ֽ��� */
	char file_type;				/*  */
	char name[255];				/* File name */
}StExt4_Dir_Dntry;
#pragma pack()

/*
ÿ���ļ���Ӧ�Ŀ�����
*/
#pragma pack(1)
typedef struct BlockListTag
{
	unsigned __int64 address;
	BlockListTag *next;
}StBlockList;
#pragma pack()

/*
ÿ���ļ���Ӧ�Ŀ�����
*/
#pragma pack(1)
typedef struct FileInfoTag
{
	StExt4Inode *InodeInfo;			/* �ļ�inode��Ϣ */
	StBlockList *BlockList;
}StFileInfo;
#pragma pack()

/*
ÿ���ļ���Ӧ�Ŀ�����
*/
#pragma pack(1)
typedef struct DirDntryTag
{
	StExt4_Dir_Dntry *DirInfo;		/* Ŀ¼������Ϣ */
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
���ܣ���ʼ���������
���أ���������
*/
StDiskInfo *Cflabqd_Init( HANDLE H_Disk );

/*
���ܣ���ʼ������ָ�룬��ѡ��˫���ķ���������
���أ���������
*/
void Cflabqd_Partition_Init( StPartTabInfo *ST_PartTabInfo );

/*
���ܣ�����Ŀ¼
stBlockList��Ŀ¼������ͨ������Cflabqd_Get_BlockList��ȡ
���أ�Ŀ¼����
*/
StDirDntry *Cflabqd_Parse_Dir( StBlockList *stBlockList );

/*
���ܣ��ͷ�Ŀ¼����
stBlockList��Ŀ¼������
*/

void Cflabqd_Dir_Free( StDirDntry *ST_DirDntry );

/*
���ܣ���ȡinode��ϸ��Ϣ
N_Inode��inode�ڵ��
���أ�inode�ڵ�ṹ��
*/
StExt4Inode *Cflabqd_Get_InodeInfo( UINT32 N_Inode );

/*
���ܣ��ͷ�inode�ڵ�
ST_Ext4Inode��inode�ڵ�
*/
void Cflabqd_InodeInfo_Free( StExt4Inode *ST_Ext4Inode );

/*
���ܣ�������inode�еĿ�����
ST_Ext4Inode��inode�ڵ�ṹ��
���أ�inode�еĿ�����
*/
StBlockList *Cflabqd_Get_BlockList( StExt4Inode *ST_Ext4Inode );

/*
���ܣ��ͷſ�����
ST_BlockList��������
*/
void Cflabqd_BlockList_Free( StBlockList *ST_BlockList );

/*
���ܣ���
����ʵ�ʶ����Ĵ�С
*/
DWORD Cflabqd_Read( HANDLE H_Disk, unsigned __int64 N_Offset, char *SZ_Buffer, unsigned __int64 N_BufferSize );


#endif /*__CDFCQD_H__*/