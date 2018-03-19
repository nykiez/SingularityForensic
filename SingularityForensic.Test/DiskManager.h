#ifndef	PARTITION_HEAD_FILE
#define PARTITION_HEAD_FILE

#pragma once
#define P_NO_OS         0x00	

#define P_EXTENDED		0x05	//��չ����
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

#define P_NEWS_NTFS		0x42	//NTFS��̬����

#define P_LINUX         0x83	//EXT2,3,4
#define P_LINUXEXTENDX  0x85

#define P_WIN_GPT		0xEE	//windows�µ�GPT����

#define P_HFS			0xAF	//APPLE�ķ�������



#define  P_RAID			0xFF	//0xff�Լ������ֵ����
#define  SIGE_DEF		0x32	//�Զ����RAID��������������̷���


#define	 SIGE_ON		0x33	//�Զ��嵱ֻ�з���û�з���ʱ�����̷��Ŵ��ڵ���0x33��Ϊ�Զ���ġ�



void __cdecl Init_Part_i386(void *pPart,void *Buffer);

typedef	enum File_System_Type
{
	U_nknown,	//δ֪����
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
	e_nknown,	//δ֪����
	e_gpt,
	e_dos,
	e_apple,


	e_fat,
	e_ntfs
};


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
	UCHAR DiskGUID[16];								//����GUID				16 byte
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
	UCHAR PartTabType[16];					//��������GUID		16 byte
	UCHAR PartTabOnly[16];					//����ΨһGUID		16 byte
	unsigned __int64 PartTabStartLBA;		//������ʼLAB		8 byte
	unsigned __int64 PartTabEndLBA;			//��������LAB		8 byte
	unsigned __int64 PartTabProp;			//��������			8 byte
	UCHAR PartTabNameUnicode[72];			//������unicode��	72 byte
}StEFIPTable;
#pragma pack()

#pragma pack(1)
typedef struct InFoDisk
{
	UCHAR     BootID;					//80h��ʾ����������������Ϊ0�������������ã�
	UCHAR     SartHead ;				//��������ʼ��ͷ�ţ� 
	USHORT    SartSectorTrack ;			//��������ʼ�����ʹŵ���
	UCHAR     FileSystemID;				//05H��0FHΪ��չ������06H��0EHΪFAT16��0BH��0CHΪFAT32 ,07ΪNTFS��
	UCHAR     EndHead;					//����������ͷ�ţ�
	USHORT    EndSectorTrack;			//�������������ʹŵ���
	ULONG     HeadSecor;				//����ǰ�������� 
	ULONG     AllSector;				//�������������� 
}InFoDisk,*PInFoDisk;
#pragma pack()

//λ�ڴ��̵�0��ͷ0����1������ 
#pragma pack(1)
typedef struct _System_Boot_Sector
{
	UCHAR		BootCode[0x1be];			//�������룻 
	InFoDisk	InFo[4];					//�����Ļ�����Ϣ 
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