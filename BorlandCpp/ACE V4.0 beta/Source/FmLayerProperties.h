//---------------------------------------------------------------------------

#ifndef FmLayerPropertiesH
#define FmLayerPropertiesH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <ExtCtrls.hpp>
#include <Buttons.hpp>
#include "RxGIF.hpp"
//---------------------------------------------------------------------------
class TFm_LayerProperties : public TForm
{
__published:	// IDE-managed Components
   TLabel *LB_Layer;
   TLabel *LB_LayerValue;
   TCheckBox *CK_TurnLayerOff;
   TRadioGroup *RG_ArcDirection;
   TLabel *LB_ZOffset;
   TEdit *ED_ZOffset;
   TEdit *ED_MaxZPerPass;
   TLabel *LB_MaxZPerPass;
   TLabel *LB_ZCharacter;
   TComboBox *LS_ZCharacter;
   TLabel *lb_Priority;
   TComboBox *LS_Priority;
   TBitBtn *Bn_OK;
   TBitBtn *Bn_Cancel;
   TImage *IMG_Help;
   TLabel *LB_1;
   void __fastcall ED_ZOffsetKeyPress(TObject *Sender, char &Key);
   void __fastcall ED_MaxZPerPassKeyPress(TObject *Sender, char &Key);
private:	// User declarations
public:		// User declarations
   __fastcall TFm_LayerProperties(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFm_LayerProperties *Fm_LayerProperties;
//---------------------------------------------------------------------------
#endif
