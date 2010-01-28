//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "FmConvertOptions.h"
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
//---------------------------------------------------------------------------
TFm_ConvertionOptions *Fm_ConvertionOptions;
//---------------------------------------------------------------------------
__fastcall TFm_ConvertionOptions::TFm_ConvertionOptions(TComponent* Owner)
   : TForm(Owner)
{
}
//---------------------------------------------------------------------------
