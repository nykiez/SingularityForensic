#ifndef LIB_H
#define LIB_H
typedef struct MyStruct {
	int MyVal;
	MyStruct *Next;
}MyStruct;
extern "C" _declspec(dllexport) int add(int x, int y);    // ����ΪC���롢���ӷ�ʽ���ⲿ����
extern "C" _declspec(dllexport) char sub(char x, char y);    // ����ΪC���롢���ӷ�ʽ���ⲿ����
extern "C" _declspec(dllexport) int *PinInt(int* x, int* y);
extern "C" _declspec(dllexport) MyStruct  GetStruct(MyStruct st);
extern "C" _declspec(dllexport) MyStruct*  GetStruct1(MyStruct *st);
extern "C" _declspec(dllexport) MyStruct**  GetStruct2(MyStruct **st);
extern "C" _declspec(dllexport) MyStruct***  GetStruct3(MyStruct ***st);
extern "C" _declspec(dllexport) MyStruct****  GetStruct4(MyStruct ****st);
extern "C" _declspec(dllexport) void AddEach(CONST int length, int arr[], int *sum);
extern "C" _declspec(dllexport) void CreateArray(int *arr, int *length);
#endif

