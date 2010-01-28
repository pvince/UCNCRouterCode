//---------------------------------------------------------------------------

#ifndef FmPriorityPropertiesH
#define FmPriorityPropertiesH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
#include <ExtCtrls.hpp>
#include "RxGIF.hpp"
//---------------------------------------------------------------------------
class TFm_PriorityProperties : public TForm
{
__published:	// IDE-managed Components
   TCheckBox *CK_Optimize;
   TLabel *LB_ReleasePlane;
   TEdit *ED_ReleasePane;
   TLabel *LB_CloseEnough;
   TEdit *ED_CloseEnough;
   TBitBtn *Bn_OK;
   TBitBtn *Bn_Cancel;
   TMemo *MM_PrePriorityCode;
   TLabel *LB_PrePriorityCode;
   TLabel *LB_PostPriorityCode;
   TMemo *MM_PostPriorityCode;
   TLabel *LB_Priority;
   TLabel *LB_PriorityValue;
   TImage *IMG_Help;
   TLabel *LB_1;
   void __fastcall ED_ReleasePaneKeyPress(TObject *Sender, char &Key);
private:	// User declarations
public:		// User declarations
   __fastcall TFm_PriorityProperties(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFm_PriorityProperties *Fm_PriorityProperties;
//---------------------------------------------------------------------------
#endif
