#include "stdafx.h"
#include "MyDLL.h"//ò��������ͷ�ļ���˳���ܵߵ��������˺ܶ�Σ����ǲ���ȷ����
#include "stdlib.h"
int StateNum = 0;
MyStruct ***res;
MyStruct GetStruct(MyStruct st) {
	st.MyVal = StateNum++;

	return st;
}
MyStruct* GetStruct1(MyStruct *st) {
	st->MyVal = StateNum++;

	return st;
}
MyStruct** GetStruct2(MyStruct **st) {
	(*st)->MyVal = StateNum++;

	return st;
}
MyStruct*** GetStruct3(MyStruct ***st) {
	(**st)->MyVal = StateNum++;

	return st;
}
MyStruct**** GetStruct4(MyStruct ****st) {
	(***st)->MyVal = StateNum++;

	return st;
}
int add(int x, int y)//�Ƿ�������Ϊ��VS2010�Ѿ�Ĭ���� _stdcall�����Ժ���������Ӹ����η�
{

	return x + y;
}
void AddEach(int length, int arr[], int *sum) {
	for (int index = 0; index < length; index++) {
		*sum += arr[index];
	}
}

void CreateArray(int *arr, int *length) {
	for (int index = 0; index < 10; index++) {
		arr[index] = index;
	}
	*length = 10;
}
char sub(char x, char y)
{
	return x - y;
}
int* PinInt(int* x, int* y) {
	int res = (*x) + (*y);
	*x++;
	return &res;
}

