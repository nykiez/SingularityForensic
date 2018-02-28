#ifndef _FHR_H_
#define _FHR_H_

typedef struct tagStPhotoFileNode1
{
	unsigned __int64 start;
	unsigned __int64 end;
	unsigned __int64 filesize;
	char name[32];
	char data[32];
	char type[32];
	tagStPhotoFileNode1 *next;
}StPhotoFileNode1;
/*
������Ƭ�ļ�
hFile���豸���
nSize�������ļ���С(�ֽ�)������LBA-��ʼLBA��������������񣬾ʹ����������С
*/
bool __cdecl photo_searchfile( HANDLE hFile,unsigned __int64 startSec, unsigned __int64 nSize );

/*
����Ľӿ�������󣬵�������ӿڴ�����Ӧ�ĺ�׺������ȡ�ļ�png,jpg......
szExtension�������׺��
����ֵ�����ض�Ӧ��׺�����ļ�����
*/
void *__cdecl photo_getflie( char *szExtension );

/*
��ȡ��ǰ���������������ڽ���������ʾ
*/
unsigned __int64 photo_get_offset();

/*
������ҳ���ϣ���ȡҳ���ϵ�ֵ������
*/
bool photo_searchfile(HANDLE hFile,unsigned int64 startSec,int secSize,unsigned int64 nSize)
//void photo_int_searchsec( unsigned __int64 nStartSec, unsigned __int64 nEndSec, int nSecSize );

/*
�ͷ��ڴ棬����˳�ʱ�ȵ���������ٵ��������exit
*/
void __cdecl photo_first();

/*
�ͷ�photo_getflie���ص��ļ�����
stFile���ļ�����
*/
void __cdecl photo_exit( void *stFile );

/*
д�ļ�
hHandle������һ���ļ����,�����ͨ��createfile����һ���ļ�
hFile������photo_searchfile��õ�Ҫ�ָ����ļ�
*/
bool photo_writefile( HANDLE hHandle, void *hFile );

/*
��������ӿڣ���ֹͣphoto_searchfile������������
*/
void __cdecl photo_stop();

#endif