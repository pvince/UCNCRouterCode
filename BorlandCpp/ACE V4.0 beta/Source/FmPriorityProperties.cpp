//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "FmPriorityProperties.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma link "RxGIF"
#pragma resource "*.dfm"
//---------------------------------------------------------------------------
TFm_PriorityProperties *Fm_PriorityProperties;
//---------------------------------------------------------------------------
__fastcall TFm_PriorityProperties::TFm_PriorityProperties(TComponent* Owner)
   : TForm(Owner)
{
}
//---------------------------------------------------------------------------
void __fastcall TFm_PriorityProperties::ED_ReleasePaneKeyPress(TObject *Sender, char &Key)
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

