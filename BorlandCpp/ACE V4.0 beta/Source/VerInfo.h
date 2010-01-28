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
//	File: VerInfo.h
//
//	Legend:
//	1998/01/18	First Release
//
//---------------------------------------------------------------------------
#ifndef VerInfoH
#define VerInfoH

class VerInfo{
public:
	VerInfo();

    // === PROPERTIES ===
    // FileName (read/write)
    // 	file name in full path
    __property AnsiString FileName = {
    	read = m_strGetFileName,
    	write = m_SetFileName
	};

    // FixedFileVersion (read only)
    // 	like "4.0.0.950"
    __property AnsiString FixedFileVersion = {
    	read = m_strGetFixedFileVersion
	};
    // FixedProductVersion (read only)
    __property AnsiString FixedProductVersion = {
    	read = m_strGetFixedProductVersion
	};
    // FixedFileFlags (read only)
    __property DWORD FixedFileFlags = {
    	read = m_dwGetFixedFileFlags
	};
    // FixedFileOS (read only)
    __property DWORD FixedFileOS = {
    	read = m_dwGetFixedFileOS
	};
    // FixedFileType (read only)
    __property DWORD FixedFileType = {
    	read = m_dwGetFixedFileType
	};
    // FixedFileSubtype (read only)
    __property DWORD FixedFileSubtype = {
    	read = m_dwGetFixedFileSubtype
	};
    // FixedFileDate (read only) --- not supported in Windows95/NT4 (?)
    __property FILETIME FixedFileDate = {
    	read = m_ftGetFixedFileDate
	};

    // LangID (read only)
    __property WORD LangID = {
    	read = m_wGetLangID
	};
    // CharsetID (read only)
    __property WORD CharsetID = {
    	read = m_wGetCharsetID
	};

    // ProductName (read only)
    __property AnsiString ProductName = {
    	read = m_strGetProductName
	};

    // ProductVersion (read only)
    __property AnsiString ProductVersion = {
    	read = m_strGetProductVersion
	};

    // OriginalFilename (read only)
    __property AnsiString OriginalFilename = {
    	read = m_strGetOriginalFilename
	};

    // FileDescription (read only)
    __property AnsiString FileDescription = {
    	read = m_strGetFileDescription
	};

    // FileVersion (read only)
    __property AnsiString FileVersion = {
    	read = m_strGetFileVersion
	};

    // CompanyName (read only)
    __property AnsiString CompanyName = {
    	read = m_strGetCompanyName
	};

    // LegalCopyright (read only)
    __property AnsiString LegalCopyright = {
    	read = m_strGetLegalCopyright
	};

    // LegalTrademarks (read only)
    __property AnsiString LegalTrademarks = {
    	read = m_strGetLegalTrademarks
	};

    // InternalName (read only)
    __property AnsiString InternalName = {
    	read = m_strGetInternalName
	};

    // PrivateBuild (read only)
    __property AnsiString PrivateBuild = {
    	read = m_strGetPrivateBuild
	};

    // SpecialBuild (read only)
    __property AnsiString SpecialBuild = {
    	read = m_strGetSpecialBuild
	};

    // Comments (read only)
    __property AnsiString Comments = {
    	read = m_strGetComments
	};


    // LastError (read only)
    //	result of GetLastError() in this class
    __property DWORD LastError = {
    	read = m_dwGetLastError
	};



private:

    // property - ExeName -
	AnsiString	m_strFileName;
	AnsiString	m_strGetFileName(void);
	void	m_SetFileName(AnsiString);


    // property - FixedFileVersion -
    VS_FIXEDFILEINFO	m_stFileInfo;
	AnsiString m_strGetFixedFileVersion(void);

    // property - FixedProductVersion -
	AnsiString m_strGetFixedProductVersion(void);

    // property - FixedFileFlags -
    DWORD	m_dwGetFixedFileFlags(void);

    // property - FixedFileOS -
    DWORD	m_dwGetFixedFileOS(void);

    // property - FixedFileType -
    DWORD	m_dwGetFixedFileType(void);

	// property - FixedFileSubtype -
    DWORD	m_dwGetFixedFileSubtype(void);

	// property - FixedFileDate -
    FILETIME	m_ftGetFixedFileDate(void);


    // property - LangID -
    WORD	m_wLangID;
    WORD	m_wGetLangID(void);

    // property - CharsetID -
    WORD	m_wCharsetID;
    WORD	m_wGetCharsetID(void);


    // property - ProductName -
    AnsiString	m_strProductName;
    AnsiString	m_strGetProductName(void);

    // property - ProductVersion -
    AnsiString	m_strProductVersion;
    AnsiString	m_strGetProductVersion(void);

    // property - OriginalFilename -
    AnsiString	m_strOriginalFilename;
    AnsiString	m_strGetOriginalFilename(void);

    // property - FileDescription -
    AnsiString	m_strFileDescription;
    AnsiString	m_strGetFileDescription(void);

    // property - FileVersion -
    AnsiString	m_strFileVersion;
    AnsiString	m_strGetFileVersion(void);

    // property - CompanyName -
    AnsiString	m_strCompanyName;
    AnsiString	m_strGetCompanyName(void);

    // property - LegalCopyright -
    AnsiString	m_strLegalCopyright;
    AnsiString	m_strGetLegalCopyright(void);

    // property - LegalTrademarks -
    AnsiString	m_strLegalTrademarks;
    AnsiString	m_strGetLegalTrademarks(void);

    // property - InternalName -
    AnsiString	m_strInternalName;
    AnsiString	m_strGetInternalName(void);

    // property - PrivateBuild -
    AnsiString	m_strPrivateBuild;
    AnsiString	m_strGetPrivateBuild(void);

    // property - SpecialBuild -
    AnsiString	m_strSpecialBuild;
    AnsiString	m_strGetSpecialBuild(void);

    // property - Comments -
    AnsiString	m_strComments;
    AnsiString	m_strGetComments(void);


    // property - LastError -
    DWORD	m_dwLastError;
    DWORD	m_dwGetLastError(void);

    // member functions
    void	m_ClearData(void);
    void	m_GetVerInfo(void);
};

//---------------------------------------------------------------------------
#endif
