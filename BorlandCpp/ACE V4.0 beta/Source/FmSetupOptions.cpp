//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "FmSetupOptions.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
//---------------------------------------------------------------------------
TFm_SetupOptions *Fm_SetupOptions;
//---------------------------------------------------------------------------
__fastcall TFm_SetupOptions::TFm_SetupOptions(TComponent* Owner)
   : TForm(Owner)
{
}
//---------------------------------------------------------------------------


void __fastcall TFm_SetupOptions::ED_DimensionPrecisionKeyPress(TObject *Sender, char &Key)
{
   // only allow values, no alphabet
   switch ( Key ) {
      case '\b':  // back space
      case 4   :  // delete
      case '.' :
      case '0' :
      case '1' :
      case '2' :
      case '3' :
      case '4' :
      case '5' :
      case '6' :
      case '7' :
      case '8' :
      case '9' :
      case '-' :
      case '+' : {
         break;
      }
      default : {
         Key = 0;
      }
   }
}
//---------------------------------------------------------------------------
void __fastcall TFm_SetupOptions::ED_EndXKeyPress(TObject *Sender,  char &Key)
{
   // only allow values, no alphabet
   switch ( Key ) {
      case '\b':  // back space
      case 4   :  // delete
      case '.' :
      case '0' :
      case '1' :
      case '2' :
      case '3' :
      case '4' :
      case '5' :
      case '6' :
      case '7' :
      case '8' :
      case '9' : {
         break;
      }
      default : {
         Key = 0;
      }
   }
}
//---------------------------------------------------------------------------

