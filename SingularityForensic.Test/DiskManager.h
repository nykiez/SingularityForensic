#ifndef	PARTITION_HEAD_FILE
#define PARTITION_HEAD_FILE

#pragma once
#define P_NO_OS         0x00	

#define P_EXTENDED		0x05	//扩展分区
#define P_EXTENDX		0x0F

#define P_32FAT         0x0B	//FAT32
#define P_32FAT_LBA     0x0C
#define P_32FATH        0x1B
#define P_32FAT_LBAH    0x1C

#define P_16FAT         0x04	//FAT16
#define P_16FATBD       0x06
#define P_16FATBD_LBA   0x0E
#define P_16FATH        0x14
#define P_16FATBDH      0x16
#define P_16FATBD_LBAH  0x1E

#define P_12FATH        0x11	//FAT12
#define P_12FAT         0x01

#define P_NTFS          0x07	//NTFS
#define P_NTFSH         0x17

#define P_NEWS_NTFS		0x42	//NTFS动态分区

#define P_LINUX         0x83	//EXT2,3,4
#define P_LINUXEXTENDX  0x85

#define P_WIN_GPT		0xEE	//windows下的GPT分区

#define P_HFS			0xAF	//APPLE的分区类型



#define  P_RAID			0xFF	//0xff自己定义的值。。
#define  SIGE_DEF		0x32	//自定义的RAID类型虚拟出来的盘符。


#define	 SIGE_ON		0x33	//自定义当只有分区没有符号时，当盘符号大于等于0x33都为自定义的。



void __cdecl Init_Part_i386(void *pPart,void *Buffer);

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

typedef	enum ePTableType
{
	e_nknown,	//未知类型
	e_gpt,
	e_dos,
	e_apple,


	e_fat,
	e_ntfs
};


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
	UCHAR DiskGUID[16];								//磁盘GUID				16 byte
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
	UCHAR PartTabType[16];					//分区类型GUID		16 byte
	UCHAR PartTabOnly[16];					//分区唯一GUID		16 byte
	unsigned __int64 PartTabStartLBA;		//分区起始LAB		8 byte
	unsigned __int64 PartTabEndLBA;			//分区结束LAB		8 byte
	unsigned __int64 PartTabProp;			//分区属性			8 byte
	UCHAR PartTabNameUnicode[72];			//分区名unicode码	72 byte
}StEFIPTable;
#pragma pack()

#pragma pack(1)
typedef struct InFoDisk
{
	UCHAR     BootID;					//80h表示可启动分区，否则为0；对主分区有用；
	UCHAR     SartHead ;				//分区的起始磁头号； 
	USHORT    SartSectorTrack ;			//分区的起始扇区和磁道号
	UCHAR     FileSystemID;				//05H或0FH为扩展分区，06H或0EH为FAT16，0BH或0CH为FAT32 ,07为NTFS；
	UCHAR     EndHead;					//分区结束磁头号；
	USHORT    EndSectorTrack;			//分区结束扇区和磁道号
	ULONG     HeadSecor;				//分区前的扇区； 
	ULONG     AllSector;				//分区的总扇区； 
}InFoDisk,*PInFoDisk;
#pragma pack()

//位于磁盘的0磁头0拄面1扇区； 
#pragma pack(1)
typedef struct _System_Boot_Sector
{
	UCHAR		BootCode[0x1be];			//引导代码； 
	InFoDisk	InFo[4];					//分区的基本信息 
	USHORT		LoGo;						//"0xAA55";     
}System_Boot_Sector,*PSystem_Boot_Sector;
#pragma pack()

#pragma pack(1)
typedef struct TagDosPTable
{
	unsigned __int64 nOffset;
	InFoDisk *InFo;
	TagDosPTable *next;
}StDosPTable;
#pragma pack()

#pragma pack(1)
typedef struct TagPTable
{
	ePTableType eType;
	unsigned __int64 nOffset;
	InFoDisk *InFo;
	StEFIInfo *EFIInfo;
	StEFIPTable *EFIPTable;
	TagPTable *next;
}StPTable;
#pragma pack()


StPTable *DiskManager_Get_PTable(HANDLE hDisk);
void DiskManager_Exit();
#endif