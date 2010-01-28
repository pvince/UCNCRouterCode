//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "FmAboutBox.h"

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFm_AboutBox *Fm_AboutBox;
//---------------------------------------------------------------------------
__fastcall TFm_AboutBox::TFm_AboutBox(TComponent* Owner)
   : TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFm_AboutBox::FormCreate(TObject *Sender)
{
   LB_ProductName->Caption =                 myVerInfo.ProductName;
   LB_Description->Caption =                 myVerInfo.FileDescription;
   LB_Comments->Caption    = "Comments: "  + myVerInfo.Comments;
   LB_Version->Caption     = "Version: "   + myVerInfo.FixedFileVersion;
   LB_Coprright->Caption   = "Copyright: " + myVerInfo.LegalCopyright;

   // Display which conditional compile options are active
   LB_Conditionals->Caption = "";

#ifdef DEMO
   LB_Conditionals->Caption := LB_Conditionals->Caption + "-> DEMONSTRATION VERSION <-" + "\x0D" + "\x0A";
#endif
}
//---------------------------------------------------------------------------
