#ifndef	__FAT_RECIVER__
#define __FAT_RECIVER__

#define	ATTR_NORMAL		0x00
#define	ATTR_READONLY	0x01
#define	ATTR_SYSTEM		0x04
#define	ATTR_VOLUMENAME	0x08
#define	ATTR_DIRECTORY	0x10
#define	ATTR_ARCHIVE	0x20
#define ATTR_LONG_NAME	( ATTR_READONLY | ATTR_HIDDEN | ATTR_SYSTEM | ATTR_VOLUMENAME )

/* Buffer to store file system data (e.g. cluster buffer for FAT) */
#define FILESYSBUFFER 0x80000
#define SWAPW( x ) ( ( ( ( x )&0xff)<<8 ) | ( ( ( x ) >> 8 ) & 0xff ) )
#define ATTR_RO      1  /* read-only */
#define ATTR_HIDDEN  2  /* hidden */
#define ATTR_SYS     4  /* system */
#define ATTR_VOLUME  8  /* volume label */
#define ATTR_DIR     16 /* directory */
#define ATTR_ARCH    32 /* archived */
#define ATTR_EXT     (ATTR_RO | ATTR_HIDDEN | ATTR_SYS | ATTR_VOLUME)
#define ATTR_EXT_MASK     (ATTR_RO | ATTR_HIDDEN | ATTR_SYS | ATTR_VOLUME | ATTR_DIR | ATTR_ARCH)

typedef	enum File_System_Type
{
	U_nknown,	//未知类型
	U_FAT12,
	U_FAT16,
	U_FAT32,
	U_NTFS,
	U_EXFAT,
	U_EXT2,
	U_EXT3,
	U_EXT4,
	U_HFS,
	U_HFSP,
	U_HFSX
};

enum eFatSearchType
{
	eFatSearchType_FS,			//文件系统恢复
	eFatSearchType_FORMAT,		//格式化恢复
	eFatSearchType_FULL,		//文件签名扫描
};

//簇链表
#pragma pack(1)
typedef struct tagClusterList
{
	unsigned __int64 nClusterNum;
	unsigned __int64 nLBAByte;
	tagClusterList *Next;
}StClusterList;
#pragma pack()

//文件链表
#pragma pack(1)
typedef struct tagFileList
{
	char Name[512];				//文件名
	UCHAR FileAttrib;			//文件属性：0x01表示只读，0x02隐藏，0x04系统文件，0x08卷标 长文件名目录项，0x10表示目录，0x20表示存档
	UCHAR MTen;					//精确1/10秒
	struct
	{
		USHORT Second;
		USHORT Minute;
		USHORT Hour;
		USHORT Day;
		USHORT Month;
		USHORT Year;
	}CreateTime;
    USHORT LastAccessTime;		//最后访问时间；
    struct
	{
		USHORT Second;
		USHORT Minute;
		USHORT Hour;
		USHORT Day;
		USHORT Month;
		USHORT Year;
	}ChangeTime;
    ULONG FileSize;					//文件大小，子目录设为0
	bool bDeleted;
	StClusterList *stClusterList;	//簇链表
	tagFileList *Next;
}StFileList;
#pragma pack()

//文件系统引导扇区DBR
#pragma pack(1)
typedef struct tagFatDBR
{
	UCHAR ignored[3];				//跳转指令
	UCHAR system_id[8];				//文件系统标志（ASCII码）	
	USHORT sector_size;				//每扇区字节数	
	UCHAR sectors_per_cluster;		//每簇扇区数
	USHORT reserved;				//保留扇区；//其FAT表紧跟在此之后
	UCHAR fats;						//fat表的个数 一般为2；
	USHORT dir_entries;				//根目录最多可容纳的目录项FAT32不用为0，FAT12，16一般为512；
	USHORT sectors;					//整个分区的扇区总数  小于32MB放在此；
	UCHAR media;					//介质描述符一般为0xF8;
	USHORT fat_length;				//FAT32不用；每FAT表的大小扇区数/* 0x16 sectors/FAT */
	USHORT secs_track;				//每磁道扇区数
	USHORT heads;					//磁头数；
	UINT hidden;					//分区前的扇区数；
	UINT total_sect;				//文件系统的总扇区；
	/* 以下字段只能使用FAT32的 */
	UINT fat32_length;				//FAT表的扇区大小值
	USHORT flags;					//确定FAT表的工作方式bit7设置为1表示只有一份FAT表是活动的 	/* 0x28 bit 8: fat mirroring, low 4: active fat */
	UCHAR version[2];				//文件系统的版本号；
	UINT root_cluster;				//根目录起始簇号 一般为2
	USHORT info_sector;				//FSINFO所在扇区号； 1号；
	USHORT backup_boot;				//备份扇区号；（6号扇区）
	UCHAR BPB_Reserved[12];			/* 0x34 Unused */
	UCHAR BS_DrvNum;				/* 0x40 */
	UCHAR BS_Reserved1;				/* 0x41 */
	UCHAR BS_BootSig;				/* 0x42 */
	UCHAR BS_VolID[4];				/* 0x43 */
	UCHAR BS_VolLab[11];			/* 0x47 */
	UCHAR BS_FilSysType[8];			/* 0x52=82*/
	UCHAR nothing[420];				/* 0x5A */
	USHORT marker;
}StFatDBR;

//FSINFO信息
#pragma pack(1)
typedef struct TagFSINFO
{
	UINT32 Flag;				//扩展引导标志'52 52 61 41'
	char Uable[480];			//未使用
	UINT32 FSINFO;				//FSINFO签名'72 71 41 61'
	UINT32 FreeCluster;			//空闲簇数量
	UINT32 NextFreeCluter;		//下一可用簇号
	char Uable2[14];				//未使用
	uint16_t End;				//"55aa"
}StFSINFO;
#pragma pack()

//文件系统结构
#pragma pack(1)
typedef struct TagFileSys
{
	StFatDBR *stFatDBR;			//引导区
	StFSINFO *stFSINFO;			//FSINFO信息区
	StFatDBR *stFatDBR_BK;			//引导区备份
	StFSINFO *stFSINFO_BK;			//FSINFO信息区备份
}StFileSys;
#pragma pack()

/*
初始化fat
hDisk:镜像句柄
nPosition:文件系统相对于镜像的便宜(字节)

eFsType:文件系统类型
*/
StFileSys *fat_init( HANDLE hDisk, unsigned __int64 nPosition, File_System_Type eFsType );

/*
获取根目录
stFatDBR：引导区
*/
StFileList *fat_get_rootdir( StFatDBR *stFatDBR );

/*
获取子目录
stCluster:目录占用的簇链表
*/
StFileList *fat_parse_dir( StClusterList *stCluster );

void fat_exit();

#endif