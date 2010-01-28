//---------------------------------------------------------------------------

#ifndef FmAboutBoxH
#define FmAboutBoxH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
#include "VerInfo.h"

//---------------------------------------------------------------------------
class TFm_AboutBox : public TForm
{
__published:	// IDE-managed Components
   TBitBtn *BN_OK;
   TGroupBox *GP_About;
   TLabel *LB_Comments;
   TLabel *LB_ProductName;
   TLabel *LB_Version;
   TLabel *LB_Coprright;
   TLabel *LB_Conditionals;
   TLabel *LB_Description;
   void __fastcall FormCreate(TObject *Sender);
private:	// User declarations
   VerInfo myVerInfo;



public:		// User declarations
   __fastcall TFm_AboutBox(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFm_AboutBox *Fm_AboutBox;
//---------------------------------------------------------------------------
#endif
