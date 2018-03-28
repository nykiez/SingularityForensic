// CflabProjectDev.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include <Windows.h>
#include "../Contracts/Stream.h"

#ifdef STREAMEXTENSION_EXPORTS

#define XYZAPI __declspec(dllexport)

#else

#define XYZAPI __declspec(dllimport)

#endif

class XYZAPI DevStream : public Stream
{
public:
	DevStream(HANDLE m_hDisk);
	long long GetLength();
	long long GetPosition();
	void SetPosition(long long position);
	bool CanRead();
	bool Read(BYTE * lpBuffer, unsigned long nNumberOfBytesToRead, unsigned long *nRetSize, long long nPos);
	bool CanWrite();
	bool Write(BYTE* lpBuffer, unsigned long nNumberOfBytesToWrite, unsigned long *nRetSize, long long nPos);
	void Close();
private:
	HANDLE hDisk;
};