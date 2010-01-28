//---------------------------------------------------------------------------

#ifndef FmSetupOptionsH
#define FmSetupOptionsH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>

//---------------------------------------------------------------------------
class TFm_SetupOptions : public TForm
{
__published:	// IDE-managed Components
   TEdit *ED_DimensionPrecision;
   TEdit *ED_DefaultZOffset;
   TLabel *LB_DimensionPrecision;
   TLabel *LB_DimensionZOffset;
   TEdit *ED_DefaultMaxZPass;
   TLabel *LB_DefaultMaxZPass;
   TEdit *ED_DefaultReleasePlane;
   TLabel *LB_DefaultReleasePlane;
   TEdit *ED_DefaultCloseEnough;
   TLabel *LB_DefaultCloseEnough;
   TBitBtn *Bn_OK;
   TBitBtn *Bn_Cancel;
   TLabel *LB_DefRelP_info;
   TLabel *LB_MaxZP_info;
   TCheckBox *CK_AddGotoStartCommand_ToFileStart;
   TCheckBox *CK_AddGotoEndCommand_ToFileEnd;
   TCheckBox *CK_StartAndStopSpindleCommands;
   TLabel *LB_SpindleHelp;
   TEdit *ED_StartX;
   TLabel *LB_StartX;
   TEdit *ED_StartY;
   TLabel *LB_StartY;
   TEdit *ED_StartZ;
   TLabel *LB_StartZ;
   TEdit *ED_EndX;
   TLabel *LB_EndX;
   TEdit *ED_EndY;
   TLabel *LB_EndY;
   TEdit *ED_EndZ;
   TLabel *LB_EndZ;
   void __fastcall ED_DimensionPrecisionKeyPress(TObject *Sender,
          char &Key);
   void __fastcall ED_EndXKeyPress(TObject *Sender, char &Key);
private:	// User declarations
public:		// User declarations
   __fastcall TFm_SetupOptions(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFm_SetupOptions *Fm_SetupOptions;
//---------------------------------------------------------------------------
#endif
