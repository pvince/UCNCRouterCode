//----------------------------------------------------------------------------
//                                         * presented by BlueWater Software *
//
//
//					<< VerInfo Class >>
//
//		Get Version Infomation in the resource of 32bit file
//
//
//
//	Copyright (C)1998 biac (biac@asahi-net.email.ne.jp)
//
//	File: VerInfo.cpp
//
//	Legend:
//	1998/01/18	First Release
//
//---------------------------------------------------------------------------
//---------------------------------------------------------------------------
#include <vcl\vcl.h>
#pragma hdrstop

#include "VerInfo.h"

//---------------------------------------------------------------------------
VerInfo::VerInfo()
{
	// initialize members
    m_ClearData();

	// set filename of this file
	this->FileName = Application->ExeName;
}


//---------------------------------------------------------------------------
//
//	property "FileName"
//
AnsiString VerInfo::m_strGetFileName()
{
	return m_strFileName;
}

void VerInfo::m_SetFileName(AnsiString strNewName)
{
	m_strFileName = strNewName;
	m_GetVerInfo();
}


//---------------------------------------------------------------------------
//
//	property "FixedFileVersion" (read only)
//		returns like "4.0.0.950"
//
AnsiString	VerInfo::m_strGetFixedFileVersion(void)
{
	AnsiString s0((int)HIWORD(m_stFileInfo.dwFileVersionMS));
    AnsiString s1((int)LOWORD(m_stFileInfo.dwFileVersionMS));
	AnsiString s2((int)HIWORD(m_stFileInfo.dwFileVersionLS));
	AnsiString s3((int)LOWORD(m_stFileInfo.dwFileVersionLS));

	return (s0 + "." + s1 + "." + s2 + "." + s3);
}

//---------------------------------------------------------------------------
//
//	property "FixedProductVersion" (read only)
//		returns like "4.0.0.950"
//
AnsiString	VerInfo::m_strGetFixedProductVersion(void)
{
	AnsiString s0((int)HIWORD(m_stFileInfo.dwProductVersionMS));
    AnsiString s1((int)LOWORD(m_stFileInfo.dwProductVersionMS));
	AnsiString s2((int)HIWORD(m_stFileInfo.dwProductVersionLS));
	AnsiString s3((int)LOWORD(m_stFileInfo.dwProductVersionLS));

	return (s0 + "." + s1 + "." + s2 + "." + s3);
}

//---------------------------------------------------------------------------
//
//	property "FixedFileFlags" (read only)
//
DWORD	VerInfo::m_dwGetFixedFileFlags(void)
{
	return m_stFileInfo.dwFileFlags	;
}

//---------------------------------------------------------------------------
//
//	property "FixedFileOS" (read only)
//
DWORD	VerInfo::m_dwGetFixedFileOS(void)
{
	return m_stFileInfo.dwFileOS;
}

//---------------------------------------------------------------------------
//
//	property "FixedFileType" (read only)
//
DWORD	VerInfo::m_dwGetFixedFileType(void)
{
	return m_stFileInfo.dwFileType;
}

//---------------------------------------------------------------------------
//
//	property "FixedFileSubtype" (read only)
//
DWORD	VerInfo::m_dwGetFixedFileSubtype(void)
{
	return m_stFileInfo.dwFileSubtype;
}

//---------------------------------------------------------------------------
//
//	property "FixedFileDate" (read only)
//
FILETIME	VerInfo::m_ftGetFixedFileDate(void)
{
	FILETIME	ft;
    ft.dwHighDateTime = m_stFileInfo.dwFileDateMS;
    ft.dwLowDateTime  = m_stFileInfo.dwFileDateLS;

	return ft;
}


//---------------------------------------------------------------------------
//
//	property "LangID" (read only)
//
WORD	VerInfo::m_wGetLangID(void)
{
	return m_wLangID;
}

//---------------------------------------------------------------------------
//
//	property "CharsetID" (read only)
//
WORD	VerInfo::m_wGetCharsetID(void)
{
	return m_wCharsetID;
}


//---------------------------------------------------------------------------
//
//	property "ProductName" (read only)
//
AnsiString	VerInfo::m_strGetProductName(void)
{
	return m_strProductName;
}

//---------------------------------------------------------------------------
//
//	property "ProductVersion" (read only)
//
AnsiString	VerInfo::m_strGetProductVersion(void)
{
	return m_strProductVersion;
}

//---------------------------------------------------------------------------
//
//	property "OriginalFilename" (read only)
//
AnsiString	VerInfo::m_strGetOriginalFilename(void)
{
	return m_strOriginalFilename;
}

//---------------------------------------------------------------------------
//
//	property "FileDescription" (read only)
//
AnsiString	VerInfo::m_strGetFileDescription(void)
{
	return m_strFileDescription;
}

//---------------------------------------------------------------------------
//
//	property "FileVersion" (read only)
//
AnsiString	VerInfo::m_strGetFileVersion(void)
{
	return m_strFileVersion;
}

//---------------------------------------------------------------------------
//
//	property "LegalCopyright" (read only)
//
AnsiString	VerInfo::m_strGetLegalCopyright(void)
{
	return m_strLegalCopyright;
}

//---------------------------------------------------------------------------
//
//	property "CompanyName" (read only)
//
AnsiString	VerInfo::m_strGetCompanyName(void)
{
	return m_strCompanyName;
}

//---------------------------------------------------------------------------
//
//	property "LegalTrademarks" (read only)
//
AnsiString	VerInfo::m_strGetLegalTrademarks(void)
{
	return m_strLegalTrademarks;
}

//---------------------------------------------------------------------------
//
//	property "InternalName" (read only)
//
AnsiString	VerInfo::m_strGetInternalName(void)
{
	return m_strInternalName;
}

//---------------------------------------------------------------------------
//
//	property "PrivateBuild" (read only)
//
AnsiString	VerInfo::m_strGetPrivateBuild(void)
{
	return m_strPrivateBuild;
}

//---------------------------------------------------------------------------
//
//	property "SpecialBuild" (read only)
//
AnsiString	VerInfo::m_strGetSpecialBuild(void)
{
	return m_strSpecialBuild;
}

//---------------------------------------------------------------------------
//
//	property "Comments" (read only)
//
AnsiString	VerInfo::m_strGetComments(void)
{
	return m_strComments;
}


//---------------------------------------------------------------------------
//
//	property "LastError" (read only)
//
DWORD	VerInfo::m_dwGetLastError(void)
{
	return m_dwLastError;
}


//---------------------------------------------------------------------------
void VerInfo::m_ClearData(void)
{
    ZeroMemory((void *)(&m_stFileInfo), sizeof(m_stFileInfo));
    m_wLangID = 0;
    m_wCharsetID = 0;
    m_strProductName = "";
    m_strProductVersion = "";
	m_strOriginalFilename = "";
	m_strFileDescription = "";
	m_strFileVersion = "";
	m_strCompanyName = "";
	m_strLegalCopyright = "";
	m_strLegalTrademarks = "";
	m_strInternalName = "";
	m_strPrivateBuild = "";
	m_strSpecialBuild = "";
	m_strComments = "";

	m_dwLastError = 0;
}


//---------------------------------------------------------------------------
//
//	Get Version Info from the file
//
void VerInfo::m_GetVerInfo(void)
{
	#define VERSION_INFO_KEY_ROOT	TEXT("\\StringFileInfo\\")
	#define VERSION_INFO_KEY_TRANS	TEXT("\\VarFileInfo\\Translation")

	const int	NUM_VERSION_INFO_KEYS = 12;
	CONST static TCHAR   *VersionKeys[] = {
	    TEXT("ProductName"),
	    TEXT("ProductVersion"),
	    TEXT("OriginalFilename"),
	    TEXT("FileDescription"),
	    TEXT("FileVersion"),
	    TEXT("CompanyName"),
	    TEXT("LegalCopyright"),
	    TEXT("LegalTrademarks"),
	    TEXT("InternalName"),
	    TEXT("PrivateBuild"),
	    TEXT("SpecialBuild"),
	    TEXT("Comments")
	};

	typedef struct _VersionKeyInfo {
	    TCHAR const *szKey;
	    TCHAR       *szValue;
	} VKINFO, *LPVKINFO;
	VKINFO	gVKArray[NUM_VERSION_INFO_KEYS];



    m_ClearData();


	// Get size of Version Info Buffer
	DWORD   dwHandle = 0;	// always set zero
	DWORD	dwLength;
	dwLength = GetFileVersionInfoSize(m_strFileName.c_str(), &dwHandle);
	if(1 > dwLength){
		m_dwLastError = GetLastError();
		return;								//*** not reached ***
	}

	// Allocate Version Info buffer
	HANDLE  hMem;
	LPVOID  lpvMem;
	hMem = GlobalAlloc(GMEM_MOVEABLE, dwLength);
	if(NULL == hMem){
		m_dwLastError = GetLastError();
		return;								//*** not reached ***
	}
	lpvMem = GlobalLock(hMem);
	if(NULL == lpvMem){
		m_dwLastError = GetLastError();
		GlobalUnlock(hMem);
		GlobalFree(hMem);
		return;								//*** not reached ***
	}

	// Get Version Info block to buffer
    BOOL	fRet;
	fRet = GetFileVersionInfo(	m_strFileName.c_str(),
    							 dwHandle,
                                 dwLength,
                                 (LPVOID)lpvMem
    						  );
	if(FALSE == fRet){
		m_dwLastError = GetLastError();
		GlobalUnlock(hMem);
		GlobalFree(hMem);
		return;								//*** not reached ***
	}

    // Get root block
	LPVOID  lpInfo;
	UINT	cch;
	if(VerQueryValue(lpvMem, "\\", &lpInfo, &cch)){
        CopyMemory(	(void*)&m_stFileInfo,
        			(const void*)lpInfo,
                    sizeof(m_stFileInfo)
        		   );
    }
	else{
		m_dwLastError = GetLastError();
		GlobalUnlock(hMem);
		GlobalFree(hMem);
		return;								//*** not reached ***
	}

	// Get Translation
    AnsiString	strLangID;
    AnsiString	strCharset;
	if(VerQueryValue(lpvMem, VERSION_INFO_KEY_TRANS, &lpInfo, &cch)){
    	m_wLangID    = ((WORD *)lpInfo)[0];
        m_wCharsetID = ((WORD *)lpInfo)[1];
		strLangID  =  strLangID.IntToHex((int)m_wLangID, 4);
		strCharset = strCharset.IntToHex((int)m_wCharsetID, 4);
	}
	else{
		m_dwLastError = GetLastError();
		GlobalUnlock(hMem);
		GlobalFree(hMem);
		return;								//*** not reached ***
	}
	AnsiString	strVerInfoLangID(strLangID + strCharset);

	// Enumerate Version Info
	TCHAR   key[80];
	static	TCHAR szNull[1] = TEXT("");
	for (UINT i = 0; i < NUM_VERSION_INFO_KEYS; i++) {
		lstrcpy(key, VERSION_INFO_KEY_ROOT);
        lstrcat(key, strVerInfoLangID.c_str());
		lstrcat(key, "\\");
		lstrcat(key, VersionKeys[i]);
		gVKArray[i].szKey = VersionKeys[i];

		// If version info exists, and the key query is successful, add
		//  the value.  Otherwise, the value for the key is NULL.
		if(VerQueryValue(lpvMem, key, &lpInfo, &cch)){
		    gVKArray[i].szValue = (char *)lpInfo;
		}
		else{
		    gVKArray[i].szValue = szNull;
		}
	}
	m_strProductName      = gVKArray[0].szValue;
	m_strProductVersion   = gVKArray[1].szValue;
	m_strOriginalFilename = gVKArray[2].szValue;
    m_strFileDescription  = gVKArray[3].szValue;
    m_strFileVersion      = gVKArray[4].szValue;
    m_strCompanyName      = gVKArray[5].szValue;
    m_strLegalCopyright   = gVKArray[6].szValue;
    m_strLegalTrademarks  = gVKArray[7].szValue;
    m_strInternalName     = gVKArray[8].szValue;
    m_strPrivateBuild     = gVKArray[9].szValue;
    m_strSpecialBuild     = gVKArray[10].szValue;
    m_strComments         = gVKArray[11].szValue;


	// Release buffer
	GlobalUnlock(hMem);
	GlobalFree(hMem);

	m_dwLastError = GetLastError();
}


//---------------------------------------------------------------------------
// END OF FILE

