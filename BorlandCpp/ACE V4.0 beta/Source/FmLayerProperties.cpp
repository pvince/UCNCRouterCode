//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "FmLayerProperties.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma link "RxGIF"
#pragma resource "*.dfm"
//---------------------------------------------------------------------------
TFm_LayerProperties *Fm_LayerProperties;
//---------------------------------------------------------------------------
__fastcall TFm_LayerProperties::TFm_LayerProperties(TComponent* Owner)
   : TForm(Owner)
{
}
//---------------------------------------------------------------------------

void __fastcall TFm_LayerProperties::ED_ZOffsetKeyPress(TObject *Sender,  char &Key)
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


void __fastcall TFm_LayerProperties::ED_MaxZPerPassKeyPress(
      TObject *Sender, char &Key)
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
      case '+' : {
         break;
      }
      default : {
         Key = 0;
      }
   }
}
//---------------------------------------------------------------------------

