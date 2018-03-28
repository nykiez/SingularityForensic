#pragma once

extern "C" _declspec(dllexport) void _cdecl Dummy();


class Stream {
public:
	//获取长度
	virtual __int64 GetLength() = 0;

	//获取位置;
	virtual __int64 GetPosition() = 0;

	//设定位置;(Seek);
	virtual void SetPosition(__int64 position) = 0;

	virtual bool CanRead() = 0;

	//读取;
	//参数lpBuffer:缓冲区;
	//参数nNumberOfBytesToRead:读取大小;
	//参数nPos:流的位置;
	//返回:实际读取大小;
	virtual bool Read(BYTE * lpBuffer, unsigned long nNumberOfBytesToRead, unsigned long *nRetSize, __int64 nPos) = 0;
	
	//是否可写;
	virtual bool CanWrite() = 0;

	//写入数据;
	//参数lpBuffer:缓冲区;
	//参数nNumberOfBytesToRead:写入大小;
	//参数nPos:流的位置;
	//返回:实际写入大小;
	virtual bool Write(BYTE* lpBuffer, unsigned long nNumberOfBytesToWrite, unsigned long *nRetSize, __int64 nPos) = 0;

	//关闭流;
	virtual void Close() = 0;
};