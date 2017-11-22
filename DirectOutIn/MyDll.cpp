#include "stdafx.h"
#include "MyDLL.h"//貌似这两个头文件的顺序不能颠倒。我试了很多次，但是不能确定。
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
int add(int x, int y)//是否可以理解为，VS2010已经默认是 _stdcall，所以函数不用添加该修饰符
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

