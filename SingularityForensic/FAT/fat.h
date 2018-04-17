#ifndef __FAT_RECOVER_H__
#define __FAT_RECOVER_H__

#include "Stream.h"

//簇链表
#pragma pack(1)
typedef struct tagClusterList
{
	unsigned __int64 nClusterNum;		//簇号
	unsigned __int64 nLBAByte;			//簇对应的字节地址(相对于本分区)
	tagClusterList *Next;
}StClusterList;
#pragma pack()

//文件链表
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
	struct
	{
		USHORT Day;
		USHORT Month;
		USHORT Year;
	}LastAccessDate;		//最后访问时间；	
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

//FSINFO信息
#pragma pack(1)
typedef struct TagFatFSDBR
{
	unsigned __int64 nOffset;
	StFatDBR *stFatDBR;
}StFatFSDBR;
#pragma pack()

#pragma pack(1)
typedef struct TagFatFSInfo
{
	unsigned __int64 nOffset;
	StFSINFO *stFSINFO;
}StFatFSInfo;
#pragma pack()

void *Fat_Init( Stream *stStream );
StFileList *Fat_Get_RootDir( void *stPartition );
StFileList *Fat_Parse_Dir( void *stPartition, StClusterList *stCluster );
void Fat_Exit( void *stPartition );

//引导扇区
StFatFSDBR *Fat_Get_FsDBR( void *stPartition );

//引导扇区备份
StFatFSDBR *Fat_Get_BackupFsDBR( void *stPartition );

//FSINFO信息
StFatFSInfo *Fat_Get_BackupFsInfo( void *stPartition );

//FSINFO信息备份
StFatFSInfo *Fat_Get_FsInfo( void *stPartition );

#endif