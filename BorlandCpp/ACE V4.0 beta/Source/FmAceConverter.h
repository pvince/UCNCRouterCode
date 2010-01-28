//---------------------------------------------------------------------------

#ifndef FmAceConverterH
#define FmAceConverterH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Dialogs.hpp>
#include <IniFiles.hpp>
#include <Buttons.hpp>
#include <Menus.hpp>
#include <stdio.h>

//---------------------------------------------------------------------------
// DXF data

#define IDD_FILE       1
#define IDD_OPEN       2
#define IDD_CONVERT    3
#define IDD_HELP       4
#define IDD_LAYER      5
#define IDD_PRIORITY   6
#define IDD_SETUP    7
#define IDD_OK         9
#define IDC_CHECKBOX1	101
#define IDC_ZONCHANGE	101
#define IDD_ZONCHANGE	101
#define IDC_PRECISION_UPDOWN 110
#define IDD_PRECISION_EDIT 111
#define IDD_ZOFFSET_EDIT 112
#define IDD_DEFMAXZPASS_EDIT 113
#define IDD_DEFRELEASEPLANE_EDIT 114
#define IDD_DEFCLOSEENOUGH_EDIT 115
#define IDD_CANCEL    10
#define IDD_LAYERDISP 11
#define IDD_LAYEROFF  12
#define IDD_EITHERARC 13
#define IDD_CCWARC    14
#define IDD_CWARC     15
#define IDD_ZOFFSET   16
#define IDD_ZCHAR     19
#define IDD_PRIOR     20
#define IDD_DEPTH     21
#define IDD_PRIODISP  22
#define IDD_PRECODE   23
#define IDD_POSTCODE  24
#define IDD_OPTIMIZE  25
#define IDD_RELEASE   26
#define IDD_CLOSE     27
#define IDD_IJREL     28
#define IDD_IJFIRST   29
#define IDD_LINENUM   30
#define IDD_LICENSE   31
#define IDD_USER      32
#define IDD_STATUS    33
#define ARC           0
#define PARC          99                     // added by ray to scribe polyling arc's
#define LINE          1
#define POINT         2
#define CW            0
#define CCW           1
#define REV           0
#define FOR           1

//---------------------------------------------------------------------------
// Layer Storage

struct layer_obj {char name[30];
                  int   status;       // 0 = on, 1 = turned off
                  int   priority;
                  int   arc;
                  char  zchar;
                  float depth;
                  float zoffset;
                 };

struct priority_obj {char *precode;
                     char *postcode;
                     int   optimize;
                     float release;
                     float close;
                    };

struct convert_obj {int ijrel;
                    int ijfirst;
                    int line_num;
                    int extraz;
                   };

struct entity_obj {int type;
                   int layer;
                   int dir;

                   double x1;
                   double y1;
                   double z1;

                   double x2;
                   double y2;
                   double z2;

                   double radius;
                   double ang_start;
                   double ang_end;
                  };

//---------------------------------------------------------------------------

class TFm_AceConverter : public TForm
{
__published:	// IDE-managed Components
   TBitBtn *Bn_FileOpen;
   TBitBtn *Bn_Convert;
   TBitBtn *Bn_Setup;
   TLabel *LB_DXF_Filename;
   TLabel *LB_Layers;
   TListBox *LST_Layers;
   TLabel *LB_Priority;
   TListBox *LST_Priority;
   TOpenDialog *dlgOpen_OpenDXF;
   TSaveDialog *dlgSave_SaveCNC;
   TLabel *LB_GCode_Filename;
   TMainMenu *MM_MainMenu;
   TMenuItem *Mn_File;
   TMenuItem *Exit1;
   TMenuItem *Help1;
   TMenuItem *About1;
   TMenuItem *Open1;
   TMenuItem *N1;
   TMenuItem *Abount1;
   TMenuItem *Mn_Convert;
   TMenuItem *N2;
   TMenuItem *Setup1;
   TMenuItem *Properties1;
   TMenuItem *Mn_LayerProperties;
   TMenuItem *Mn_PriorityProperties;
   TMenuItem *N3;
   TMenuItem *Mn_OldFile10;
   TMenuItem *Mn_OldFile1;
   TMenuItem *Mn_OldFile2;
   TMenuItem *Mn_OldFile3;
   TMenuItem *Mn_OldFile4;
   TMenuItem *Mn_OldFile5;
   TMenuItem *Mn_OldFile6;
   TMenuItem *Mn_OldFile7;
   TMenuItem *Mn_OldFile8;
   TMenuItem *Mn_OldFile9;
   TBitBtn *BN_DXFView;
   void __fastcall Bn_FileOpenClick(TObject *Sender);
   void __fastcall FormCreate(TObject *Sender);
   void __fastcall Bn_SetupClick(TObject *Sender);
   void __fastcall Bn_ConvertClick(TObject *Sender);
   void __fastcall FormDestroy(TObject *Sender);
   void __fastcall LST_LayersDblClick(TObject *Sender);
        void __fastcall LST_PriorityDblClick(TObject *Sender);
   void __fastcall Exit1Click(TObject *Sender);
   void __fastcall About1Click(TObject *Sender);
   void __fastcall Open1Click(TObject *Sender);
   void __fastcall Abount1Click(TObject *Sender);
   void __fastcall Mn_ConvertClick(TObject *Sender);
   void __fastcall Setup1Click(TObject *Sender);
   void __fastcall Mn_FileClick(TObject *Sender);
   void __fastcall Properties1Click(TObject *Sender);
   void __fastcall Mn_LayerPropertiesClick(TObject *Sender);
   void __fastcall Mn_PriorityPropertiesClick(TObject *Sender);
   void __fastcall Mn_OldFile1Click(TObject *Sender);
   void __fastcall Mn_OldFile2Click(TObject *Sender);
   void __fastcall Mn_OldFile3Click(TObject *Sender);
   void __fastcall Mn_OldFile4Click(TObject *Sender);
   void __fastcall Mn_OldFile5Click(TObject *Sender);
   void __fastcall Mn_OldFile6Click(TObject *Sender);
   void __fastcall Mn_OldFile7Click(TObject *Sender);
   void __fastcall Mn_OldFile8Click(TObject *Sender);
   void __fastcall Mn_OldFile9Click(TObject *Sender);
   void __fastcall Mn_OldFile10Click(TObject *Sender);
   void __fastcall BN_DXFViewClick(TObject *Sender);
private:	// User declarations

   TIniFile *ApplicationIniFile;

   AnsiString szToFileName;    // output file

   unsigned long TotalLines;

   int demo;
   int precision;

   struct layer_obj *layer;
   struct layer_obj *temp_lay;

   struct priority_obj *priority;
   struct priority_obj *temp_pri;
   struct convert_obj convertop ;
   int iLayerCount;
   int iPrioCount;

   double defaultzoffset;
   double defaultmaxzpass;
   double defaultreleaseplane;
   double defaultcloseenough;
   int    defaultpriorityoptimization;

   bool   AddGotoStartCommand_ToFileStart;
   double Default_GotoStart_X;
   double Default_GotoStart_Y;
   double Default_GotoStart_Z;

   bool   AddGotoEndCommand_ToFileEnd;
   double Default_GotoEnd_X;
   double Default_GotoEnd_Y;
   double Default_GotoEnd_Z;

   bool   Add_M4_M5_ForSpindleControl;

   void __fastcall OpenFile(AnsiString MyFilename);

   int __fastcall ReadLayer   (void);
   int __fastcall NewPriority (int count);
   int __fastcall DelPriority (int num, int count);

   int __fastcall MyMin(int v1, int v2);

   // Convertion functions
   void __fastcall Convert         (void);
   int  __fastcall get_values      (FILE *ifp, struct entity_obj *ent);
   int  __fastcall get_string      (FILE *ifp, const char *string_match, char *string_ret);
   void __fastcall get_first_point (double *x, double *y, double *z, struct entity_obj *entity);
   void __fastcall get_second_point(double *x, double *y, double *z, struct entity_obj *entity);
   void __fastcall make_arc        (struct entity_obj *ent);

public:		// User declarations
   AnsiString szDxfFile;       // source file

   __fastcall TFm_AceConverter(TComponent* Owner);
};

//---------------------------------------------------------------------------
extern PACKAGE TFm_AceConverter *Fm_AceConverter;
//---------------------------------------------------------------------------
#endif
