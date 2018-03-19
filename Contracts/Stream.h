#pragma once

extern "C" _declspec(dllexport) void _cdecl Dummy();


class Stream {
public:
	//��ȡ����
	virtual long long GetLength() = 0;

	//��ȡλ��;
	virtual long long GetPosition() = 0;

	//�趨λ��;(Seek);
	virtual void SetPosition(long long position) = 0;

	virtual bool CanRead() = 0;

	//��ȡ;
	//����lpBuffer:������;
	//����nNumberOfBytesToRead:��ȡ��С;
	//����nPos:����λ��;
	//����:ʵ�ʶ�ȡ��С;
	virtual bool Read(BYTE * lpBuffer, unsigned long nNumberOfBytesToRead, unsigned long *nRetSize, long long nPos) = 0;
	
	//�Ƿ��д;
	virtual bool CanWrite() = 0;

	//д������;
	//����lpBuffer:������;
	//����nNumberOfBytesToRead:д���С;
	//����nPos:����λ��;
	//����:ʵ��д���С;
	virtual bool Write(BYTE* lpBuffer, unsigned long nNumberOfBytesToWrite, unsigned long *nRetSize, long long nPos) = 0;

	//�ر���;
	virtual void Close() = 0;
};