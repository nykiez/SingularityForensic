#pragma once

extern "C" _declspec(dllexport) void _cdecl Dummy();


class Stream {
public:
	//��ȡ����
	virtual __int64 GetLength() = 0;

	//��ȡλ��;
	virtual __int64 GetPosition() = 0;

	//�趨λ��;(Seek);
	virtual void SetPosition(__int64 position) = 0;

	virtual bool CanRead() = 0;

	//��ȡ;
	//����lpBuffer:������;
	//����nNumberOfBytesToRead:��ȡ��С;
	//����nPos:����λ��;
	//����:ʵ�ʶ�ȡ��С;
	virtual bool Read(BYTE * lpBuffer, unsigned long nNumberOfBytesToRead, unsigned long *nRetSize, __int64 nPos) = 0;
	
	//�Ƿ��д;
	virtual bool CanWrite() = 0;

	//д������;
	//����lpBuffer:������;
	//����nNumberOfBytesToRead:д���С;
	//����nPos:����λ��;
	//����:ʵ��д���С;
	virtual bool Write(BYTE* lpBuffer, unsigned long nNumberOfBytesToWrite, unsigned long *nRetSize, __int64 nPos) = 0;

	//�ر���;
	virtual void Close() = 0;
};