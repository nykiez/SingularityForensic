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
搜索照片文件
hFile：设备句柄
nSize：传入文件大小(字节)，结束LBA-起始LBA，如果是整个镜像，就传整个镜像大小
*/
bool __cdecl photo_searchfile( HANDLE hFile,unsigned __int64 startSec, unsigned __int64 nSize );

/*
上面的接口搜索完后，调用这个接口传入相应的后缀名，获取文件png,jpg......
szExtension：传入后缀名
返回值：返回对应后缀名的文件链表
*/
void *__cdecl photo_getflie( char *szExtension );

/*
获取当前搜索的扇区，用于进度条上显示
*/
unsigned __int64 photo_get_offset();

/*
在设置页面上，获取页面上的值，传下
*/
bool photo_searchfile(HANDLE hFile,unsigned int64 startSec,int secSize,unsigned int64 nSize)
//void photo_int_searchsec( unsigned __int64 nStartSec, unsigned __int64 nEndSec, int nSecSize );

/*
释放内存，软件退出时先调用这个，再调用下面的exit
*/
void __cdecl photo_first();

/*
释放photo_getflie返回的文件链表
stFile：文件链表
*/
void __cdecl photo_exit( void *stFile );

/*
写文件
hHandle：传入一个文件句柄,你可以通过createfile生成一个文件
hFile：传入photo_searchfile获得的要恢复的文件
*/
bool photo_writefile( HANDLE hHandle, void *hFile );

/*
调用这个接口，会停止photo_searchfile的搜索并返回
*/
void __cdecl photo_stop();

#endif