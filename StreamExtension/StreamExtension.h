#pragma once
#pragma once
#include "stdafx.h"
#include "../Contracts/Stream.h"
#ifdef STREAMEXTENSION_EXPORTS
#define XYZAPI __declspec(dllexport)
#else
#define XYZAPI __declspec(dllimport)
#endif


//数据源规范(RAW,VHD等);
class XYZAPI UnManagedStream : public Stream {
public:
	UnManagedStream(int s)
	{
			
	}
	//获取长度
	long long GetLength();

	void SetGetLengthFunc(long long(*getLengthFunc)());

	//获取位置;
	long long GetPosition();

	//设定位置;(Seek);
	void SetPosition(long long position);
	
	void SetPositionFunc(long long(*getPositionFunc)(), void(*setPositionFunc)(long long pos));

	bool CanRead();

	void SetCanReadFunc(bool(*canReadFunc)());

	//读取;
	//参数lpBuffer:缓冲区;
	//参数nNumberOfBytesToRead:读取大小;
	//参数nPos:流的位置;
	//返回:实际读取大小;
	bool Read(BYTE * lpBuffer, unsigned long nNumberOfBytesToRead, unsigned long *nRetSize, long long nPos);

	void SetReadFunc(int(*readFunc)(BYTE *lpBuffer, int nNumberOfBytesToRead));

	//是否可写;
	bool CanWrite();

	void SetCanWriteFunc(bool(*canWriteFunc)());

	//写入数据;
	//参数lpBuffer:缓冲区;
	//参数nNumberOfBytesToRead:写入大小;
	//参数nPos:流的位置;
	//返回:实际写入大小;
	bool Write(BYTE* lpBuffer, unsigned long nNumberOfBytesToWrite, unsigned long *nRetSize, long long nPos);

	void SetWriteFunc(int(*writeFunc)(BYTE* lpBuffer, int nNumberOfBytesToWrite));

	//关闭流;
	void Close();

private:
	//typedef char* (*__cdecl AddCallBack)(const char* a, const char* b);
	long long(*_getLengthFunc)();

	long long(*_getPositionFunc)();

	void(*_setPositionFunc)(long long pos);

	bool(*_canReadFunc)();

	int(*_readFunc)(BYTE *lpBuffer, int nNumberOfBytesToRead);

	int(*_writeFunc)(BYTE* lpBuffer, int nNumberOfBytesToWrite);

	//是否可写;
	bool(*_canWriteFunc)();
};

//获取非托管流;
extern "C" _declspec(dllexport) UnManagedStream* _cdecl CreateUnManagedStream() {
	return new UnManagedStream(2);
}

extern "C" _declspec(dllexport) void SetGetLengthFunc(UnManagedStream* stream, long long(*getLengthFunc)()) {
	if (stream == nullptr) {
		return;
	}
	stream->SetGetLengthFunc(getLengthFunc);
}

extern "C" _declspec(dllexport) void SetPositionFunc(UnManagedStream* stream, long long(*getPositionFunc)(), void(*setPositionFunc)(long long pos)) {
	if (stream == nullptr) {
		return;
	}
	stream->SetPositionFunc(getPositionFunc, setPositionFunc);
}

extern "C" _declspec(dllexport) void SetCanReadFunc(UnManagedStream* stream, bool(*canReadFunc)()) {
	if (stream == nullptr) {
		return;
	}

	stream->SetCanReadFunc(canReadFunc);
}

extern "C" _declspec(dllexport) void SetCanWriteFunc(UnManagedStream* stream, bool(*canWriteFunc)()) {
	if (stream == nullptr) {
		return;
	}

	stream->SetCanWriteFunc(canWriteFunc);
}

extern "C" _declspec(dllexport) void SetWriteFunc(UnManagedStream* stream, int(*writeFunc)(BYTE* lpBuffer, int nNumberOfBytesToWrite)) {
	if (stream == nullptr) {
		return;
	}

	stream->SetWriteFunc(writeFunc);
}

extern "C" _declspec(dllexport) void SetReadFunc(UnManagedStream* stream, int(*readFunc)(BYTE *lpBuffer, int nNumberOfBytesToRead)) {
	if (stream == nullptr) {
		return;
	}

	stream->SetReadFunc(readFunc);
}

extern "C" _declspec(dllexport) void CloseStream(UnManagedStream* stream) {
	if (stream == nullptr) {
		return;
	}

	stream->Close();
	delete stream;
}

extern "C" _declspec(dllexport) void ReadTest(Stream *stream) {
	BYTE arr[512];
	stream->Read(arr,512, nullptr,0);
}

extern "C" _declspec(dllexport) void WriteTest(Stream *stream) {
	BYTE arr[512] = { 0 };
	stream->Write(arr,512,nullptr,0);
}


extern "C" _declspec(dllexport) long long GetStreamLength(Stream *stream) {
	if (stream == nullptr) {
		return -1;
	}

	return stream->GetLength();
}

extern "C" _declspec(dllexport) long long GetStreamPosition(Stream *stream) {
	if (stream == nullptr) {
		return -1;
	}

	stream->GetPosition();
}

extern "C" _declspec(dllexport) void SetStreamPosition(Stream *stream, long long pos) {
	if (stream == nullptr) {
		return ;
	}

	stream->SetPosition(pos);
}
