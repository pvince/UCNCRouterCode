//---------------------------------------------------------------------------

#ifndef FmConvertOptionsH
#define FmConvertOptionsH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
//---------------------------------------------------------------------------
class TFm_ConvertionOptions : public TForm
{
__published:	// IDE-managed Components
   TCheckBox *CK_GenerateIandJasRelativeCoordinates;
   TCheckBox *CK_GenerateIandJbeforeOtherCoordinatesInBlock;
   TCheckBox *CK_GenerateBlockNumbers;
   TCheckBox *CK_GenerateZonlyIfChanging;
   TBitBtn *Bn_OK;
   TBitBtn *BN_Cancel;
private:	// User declarations
public:		// User declarations
   __fastcall TFm_ConvertionOptions(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFm_ConvertionOptions *Fm_ConvertionOptions;
//---------------------------------------------------------------------------
#endif
