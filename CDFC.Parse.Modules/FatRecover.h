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

enum eFatSearchType
{
	eFatSearchType_FS,			//�ļ�ϵͳ�ָ�
	eFatSearchType_FORMAT,		//��ʽ���ָ�
	eFatSearchType_FULL,		//�ļ�ǩ��ɨ��
};

//������
#pragma pack(1)
typedef struct tagClusterList
{
	unsigned __int64 nClusterNum;
	unsigned __int64 nLBAByte;
	tagClusterList *Next;
}StClusterList;
#pragma pack()

//�ļ�����
#pragma pack(1)
typedef struct tagFileList
{
	char Name[512];				//�ļ���
	UCHAR FileAttrib;			//�ļ����ԣ�0x01��ʾֻ����0x02���أ�0x04ϵͳ�ļ���0x08��� ���ļ���Ŀ¼�0x10��ʾĿ¼��0x20��ʾ�浵
	UCHAR MTen;					//��ȷ1/10��
	struct
	{
		USHORT Second;
		USHORT Minute;
		USHORT Hour;
		USHORT Day;
		USHORT Month;
		USHORT Year;
	}CreateTime;
    USHORT LastAccessTime;		//������ʱ�䣻
    struct
	{
		USHORT Second;
		USHORT Minute;
		USHORT Hour;
		USHORT Day;
		USHORT Month;
		USHORT Year;
	}ChangeTime;
    ULONG FileSize;					//�ļ���С����Ŀ¼��Ϊ0
	bool bDeleted;
	StClusterList *stClusterList;	//������
	tagFileList *Next;
}StFileList;
#pragma pack()

//�ļ�ϵͳ��������DBR
#pragma pack(1)
typedef struct tagFatDBR
{
	UCHAR ignored[3];				//��תָ��
	UCHAR system_id[8];				//�ļ�ϵͳ��־��ASCII�룩	
	USHORT sector_size;				//ÿ�����ֽ���	
	UCHAR sectors_per_cluster;		//ÿ��������
	USHORT reserved;				//����������//��FAT������ڴ�֮��
	UCHAR fats;						//fat��ĸ��� һ��Ϊ2��
	USHORT dir_entries;				//��Ŀ¼�������ɵ�Ŀ¼��FAT32����Ϊ0��FAT12��16һ��Ϊ512��
	USHORT sectors;					//������������������  С��32MB���ڴˣ�
	UCHAR media;					//����������һ��Ϊ0xF8;
	USHORT fat_length;				//FAT32���ã�ÿFAT��Ĵ�С������/* 0x16 sectors/FAT */
	USHORT secs_track;				//ÿ�ŵ�������
	USHORT heads;					//��ͷ����
	UINT hidden;					//����ǰ����������
	UINT total_sect;				//�ļ�ϵͳ����������
	/* �����ֶ�ֻ��ʹ��FAT32�� */
	UINT fat32_length;				//FAT���������Сֵ
	USHORT flags;					//ȷ��FAT��Ĺ�����ʽbit7����Ϊ1��ʾֻ��һ��FAT���ǻ�� 	/* 0x28 bit 8: fat mirroring, low 4: active fat */
	UCHAR version[2];				//�ļ�ϵͳ�İ汾�ţ�
	UINT root_cluster;				//��Ŀ¼��ʼ�غ� һ��Ϊ2
	USHORT info_sector;				//FSINFO���������ţ� 1�ţ�
	USHORT backup_boot;				//���������ţ���6��������
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

//FSINFO��Ϣ
#pragma pack(1)
typedef struct TagFSINFO
{
	UINT32 Flag;				//��չ������־'52 52 61 41'
	char Uable[480];			//δʹ��
	UINT32 FSINFO;				//FSINFOǩ��'72 71 41 61'
	UINT32 FreeCluster;			//���д�����
	UINT32 NextFreeCluter;		//��һ���ôغ�
	char Uable2[14];				//δʹ��
	uint16_t End;				//"55aa"
}StFSINFO;
#pragma pack()

//�ļ�ϵͳ�ṹ
#pragma pack(1)
typedef struct TagFileSys
{
	StFatDBR *stFatDBR;			//������
	StFSINFO *stFSINFO;			//FSINFO��Ϣ��
	StFatDBR *stFatDBR_BK;			//����������
	StFSINFO *stFSINFO_BK;			//FSINFO��Ϣ������
}StFileSys;
#pragma pack()

/*
��ʼ��fat
hDisk:������
nPosition:�ļ�ϵͳ����ھ���ı���(�ֽ�)

eFsType:�ļ�ϵͳ����
*/
StFileSys *fat_init( HANDLE hDisk, unsigned __int64 nPosition, File_System_Type eFsType );

/*
��ȡ��Ŀ¼
stFatDBR��������
*/
StFileList *fat_get_rootdir( StFatDBR *stFatDBR );

/*
��ȡ��Ŀ¼
stCluster:Ŀ¼ռ�õĴ�����
*/
StFileList *fat_parse_dir( StClusterList *stCluster );

void fat_exit();

#endif