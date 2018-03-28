// CflabProjectDev.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include <windows.h>
#include "../contracts/Stream.h"
#include "DevStream.h"

DevStream::DevStream(HANDLE m_hDisk)
{
	hDisk = m_hDisk;
}

long long DevStream::GetLength()
{
	return 0;
}

long long DevStream::GetPosition()
{
	return 0;
}

void DevStream::SetPosition(long long position)
{
	return;
}

bool DevStream::CanRead()
{
	return 0;
}

bool DevStream::Read(BYTE * lpBuffer, unsigned long nNumberOfBytesToRead, unsigned long *nRetSize, long long nPos)
{
	LARGE_INTEGER inte;
	LARGE_INTEGER newPos;
	newPos.QuadPart = 0;
	inte.QuadPart = nPos;
	SetFilePointerEx(hDisk, inte, &newPos, FILE_BEGIN);
	return ReadFile(hDisk, lpBuffer, nNumberOfBytesToRead, nRetSize, nullptr);
	
}

bool DevStream::CanWrite()
{
	return 0;
}

bool DevStream::Write(BYTE* lpBuffer, unsigned long nNumberOfBytesToWrite, unsigned long *nRetSize, long long nPos)
{
	return false;
	//return videorecover_write(hDisk, nPos, (char *)lpBuffer, nNumberOfBytesToWrite, nRetSize);
}

void DevStream::Close()
{
	CloseHandle(hDisk);
	//videorecover_closehandle(hDisk);
}


DevStream dd = new DevStream(NULL);
