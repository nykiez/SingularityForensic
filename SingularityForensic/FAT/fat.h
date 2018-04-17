#ifndef __FAT_RECOVER_H__
#define __FAT_RECOVER_H__

#include "Stream.h"

//������
#pragma pack(1)
typedef struct tagClusterList
{
	unsigned __int64 nClusterNum;		//�غ�
	unsigned __int64 nLBAByte;			//�ض�Ӧ���ֽڵ�ַ(����ڱ�����)
	tagClusterList *Next;
}StClusterList;
#pragma pack()

//�ļ�����
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
	struct
	{
		USHORT Day;
		USHORT Month;
		USHORT Year;
	}LastAccessDate;		//������ʱ�䣻	
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

//FSINFO��Ϣ
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

//��������
StFatFSDBR *Fat_Get_FsDBR( void *stPartition );

//������������
StFatFSDBR *Fat_Get_BackupFsDBR( void *stPartition );

//FSINFO��Ϣ
StFatFSInfo *Fat_Get_BackupFsInfo( void *stPartition );

//FSINFO��Ϣ����
StFatFSInfo *Fat_Get_FsInfo( void *stPartition );

#endif