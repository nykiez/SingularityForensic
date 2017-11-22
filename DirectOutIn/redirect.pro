/* REDIRECT.C 27/08/98 09.01.34 */
int WINMAIN_ENTRYMOD WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance,
	 LPSTR lpCmdLine, int nCmdShow);
WIN_ENTRYSC LRESULT WIN_ENTRYMOD SccWndProc (HWND hWnd, UINT message, WPARAM
	 wParam, LPARAM lParam);
HWND DoCreate (HWND hWnd);
//VOID DoCreate (HWND hWnd);

VOID DoSize(HWND hWnd, HWND viewHND, WORD wWidth, WORD wHeight);
//VOID DoSize (HWND hWnd, WORD wWidth, WORD wHeight);

VOID DoDestroy (HWND hWnd);

VOID DoCloseFile(HWND hWnd,HWND viewerHWND);
//VOID DoCloseFile (HWND hWnd);

VOID DoFileChange (HWND hWnd);
VOID DoOpenFile(HWND viewerHandle,char * locFileName);
//VOID DoOpenFile(HWND hWnd,HWND viewerHandle);
IO_ENTRYSC IOERR IO_ENTRYMOD MyClose (HIOFILE hFile);
IO_ENTRYSC IOERR IO_ENTRYMOD MyRead (HIOFILE hFile, VTBYTE *pData, DWORD
	 dwSize, DWORD *pCount);
IO_ENTRYSC IOERR IO_ENTRYMOD MySeek (HIOFILE hFile, WORD wFrom, LONG dwOffset);
IO_ENTRYSC IOERR IO_ENTRYMOD MyTell (HIOFILE hFile, DWORD *pOffset);
IO_ENTRYSC IOERR IO_ENTRYMOD MyGetInfo (HIOFILE hFile, DWORD dwInfoId, VOID 
	 *pInfo);
IO_ENTRYSC IOERR IO_ENTRYMOD MySeek64(HIOFILE hFile, VTWORD wFrom, VTOFF_T Offset);
IO_ENTRYSC IOERR IO_ENTRYMOD MyTell64(HIOFILE hFile, VTOFF_T * pOffset);
WIN_ENTRYSC BOOL WIN_ENTRYMOD DisplaySecondaryInfo (HWND hDlg, UINT message,
	 WPARAM wParam, LPARAM lParam);
WIN_ENTRYSC BOOL WIN_ENTRYMOD HelpAbout (HWND hDlg, UINT message, WPARAM wParam
	, LPARAM lParam);
