//---------------------------------------------------------------------------
//
//  Change Log
//
//     5/10/08  RWA  re-wrote user interface to support Borland C++ Builder V6
//    10/10/08  RWA  Better ini file support
//    14/10/08  RWA  Added support for Polyline bulge
//                      - NOTE: the G02 & G03 commands use the R (Radius) not I,J for the curve info
//    15/10/08  RWA  Added support to save settings against the loaded DXF file
//                      - This allow app to restore settings against a DXF file when re-loaded later
//              RWA  Added pictures into the layer & priority screens to make them easier to understand
//              RWA  Added support in Setup requestor to allow user to
//                      - start/stop (m4/m5) the spindle at beginning/end of the Gcode file
//                      - move the mill to 0,0,0 at start of gcode file
//                      - move the mill to 0,0,0 at the end of the gcode file
//    20/10/08  RWA  added menus to the main window
//              RWA  added last 10 loaded dxf files into menu for easy access
//    21/10/08  RWA  Added about box
//    22/10/08  RWA  Free allocated memory upon exiting
//              RWA  Re-Formated the code to Linux formt style, to mke it more readable (automatically done by Trita)

//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include <commctrl.h>
#include <dir.h>
#include <alloc.h>
#include <math.h>

// local includes
#include "FmAceConverter.h"
#include "FmSetupOptions.h"
#include "FmPriorityProperties.h"
#include "FmLayerProperties.h"
#include "FmConvertOptions.h"
#include "FmAboutBox.h"
#include "FmDXFViewer.h"

#define PI 3.14159265359

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma link "PJAbout"
#pragma resource "*.dfm"
//---------------------------------------------------------------------------

TFm_AceConverter * Fm_AceConverter;

//---------------------------------------------------------------------------
__fastcall TFm_AceConverter::TFm_AceConverter (TComponent *Owner) :
  TForm (Owner) { }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile1Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile1->Tag), "")); }

//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile2Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile2->Tag), "")); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile3Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile3->Tag), "")); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile4Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile4->Tag), "")); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile5Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile5->Tag), "")); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile6Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile6->Tag), "")); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile7Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile7->Tag), "")); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile8Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile8->Tag), "")); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile9Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile9->Tag), "")); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_OldFile10Click (TObject *Sender) {
// open filename
   OpenFile (ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Mn_OldFile10->Tag), "")); }

void __fastcall TFm_AceConverter::Bn_FileOpenClick (TObject *Sender)
{
  dlgOpen_OpenDXF->InitialDir = ApplicationIniFile->ReadString ("Setup", "DXFPath", ExtractFilePath (szDxfFile));

  if (dlgOpen_OpenDXF->Execute ()) {
    OpenFile (dlgOpen_OpenDXF->FileName);
  }
}

void __fastcall TFm_AceConverter::OpenFile (AnsiString MyFilename)
{
  int  Index;

  int  PIndex;
  char szString[50];
  TStringList * TempStringList;

  // ensure that there is a \ on the end of the Configuration's directory name
  szDxfFile = MyFilename;

  // prevent user interference
  Bn_FileOpen->Enabled = false;
  Bn_Convert->Enabled = false;
  Bn_Setup->Enabled = false;
  Mn_File->Enabled = false;

  // save Dir
  ApplicationIniFile->WriteString ("Setup", "DXFPath", ExtractFilePath (MyFilename));

  // update file history
  if (MyFilename != ApplicationIniFile->ReadString ("Setup", "File_1", "")) {
    for ( Index = 10; Index > 1; Index--) {
      ApplicationIniFile->WriteString ("Setup", "File_" + IntToStr (Index), ApplicationIniFile->ReadString ("Setup", "File_" + IntToStr (Index - 1), ""));
    }
    ApplicationIniFile->WriteString ("Setup", "File_1", MyFilename);
  }

  // empty already loaded data
  LST_Layers->Items->Clear ();
  LST_Layers->Refresh ();
  LST_Priority->Items->Clear ();
  LST_Priority->Refresh ();
  Application->ProcessMessages ();
  Application->ProcessMessages ();

  while ( iPrioCount>0 )
    iPrioCount = DelPriority (iPrioCount, iPrioCount);

  if ( iLayerCount>0 )
    free (layer);

  // update status
  LB_DXF_Filename->Caption = "DXF Filename: " + szDxfFile;
  iLayerCount = ReadLayer ();
  LB_GCode_Filename->Caption = "Please Adjust Properties and Settings...      Then Convert the DXF File to GCode...";

  // read file info
  if (iLayerCount>0) {
    iPrioCount = NewPriority (iPrioCount);

    LST_Priority->Items->Add ("1");
    LST_Priority->ItemIndex = 0;

    if (ApplicationIniFile->ValueExists (ExtractFileName (szDxfFile), "IJRel") == false) {
      MessageDlg ("Default Settings have been loaded for this file. \n\n\n" + szDxfFile, mtInformation, TMsgDlgButtons () << mbOK, 0);
    }

    for ( Index = 0; Index < iLayerCount; Index++ ) {
      // Load the layer setting for this file
      layer[Index].status = ApplicationIniFile->ReadBool (ExtractFileName (szDxfFile), "TurnLayerOff_" + IntToStr (Index), true);

      ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "TurnLayerOff_" + IntToStr (Index), layer[Index].status);

      layer[Index].priority = ApplicationIniFile->ReadInteger (ExtractFileName (szDxfFile), "Priority_" + IntToStr (Index), 1);
      ApplicationIniFile->WriteInteger (ExtractFileName (szDxfFile), "Priority_" + IntToStr (Index), layer[Index].priority);

      // show names and priorities                                    
      sprintf (szString, "%s ... %d", layer[Index].name, layer[Index].priority);
      LST_Layers->Items->Add (szString);

      // make more priorities, load them with defaults for now
      for ( PIndex = iPrioCount; PIndex < layer[Index].priority; PIndex++ ) {
        iPrioCount = NewPriority (iPrioCount);

        LST_Priority->Items->Add (IntToStr (PIndex + 1));
      }

      layer[Index].zchar = ApplicationIniFile->ReadString (ExtractFileName (szDxfFile), "Z_Char_" + IntToStr (Index), "Z") .c_str () [0];
      ApplicationIniFile->WriteString (ExtractFileName (szDxfFile), "Z_Char_" + IntToStr (Index), layer[Index].zchar);

      layer[Index].arc = ApplicationIniFile->ReadInteger (ExtractFileName (szDxfFile), "ArcDir_" + IntToStr (Index), IDD_EITHERARC);
      ApplicationIniFile->WriteInteger (ExtractFileName (szDxfFile), "ArcDir_" + IntToStr (Index), layer[Index].arc);

      layer[Index].depth = ApplicationIniFile->ReadFloat   (ExtractFileName (szDxfFile), "Depth_" + IntToStr (Index), defaultmaxzpass);
      ApplicationIniFile->WriteFloat (ExtractFileName (szDxfFile), "Depth_" + IntToStr (Index), layer[Index].depth);

      layer[Index].zoffset = ApplicationIniFile->ReadFloat   (ExtractFileName (szDxfFile), "ZOffset_" + IntToStr (Index), defaultzoffset);
      ApplicationIniFile->WriteFloat (ExtractFileName (szDxfFile), "ZOffset_" + IntToStr (Index), layer[Index].zoffset);
    }

    LST_Layers->ItemIndex = 0;

    // Load the priority setting for this file
    TempStringList = new TStringList; // used for convertion between "","" to lines

    for ( PIndex = 0; PIndex < iPrioCount; PIndex++ ) {
      priority[PIndex].release = ApplicationIniFile->ReadFloat (ExtractFileName (szDxfFile), "ReleasePane_" + IntToStr (PIndex), defaultreleaseplane);

      ApplicationIniFile->WriteFloat (ExtractFileName (szDxfFile), "ReleasePane_" + IntToStr (PIndex), priority[PIndex].release);

      priority[PIndex].close = ApplicationIniFile->ReadFloat (ExtractFileName (szDxfFile), "CloseEnough_" + IntToStr (PIndex), defaultcloseenough);

      ApplicationIniFile->WriteFloat (ExtractFileName (szDxfFile), "CloseEnough_" + IntToStr (PIndex), priority[PIndex].close);

      priority[PIndex].optimize = ApplicationIniFile->ReadFloat (ExtractFileName (szDxfFile), "Optimize_" + IntToStr (PIndex), defaultpriorityoptimization);

      ApplicationIniFile->WriteFloat (ExtractFileName (szDxfFile), "Optimize_" + IntToStr (PIndex), priority[PIndex].optimize);

      TempStringList->Clear ();
      TempStringList->CommaText = ApplicationIniFile->ReadString (ExtractFileName (szDxfFile), "PreCode_" + IntToStr (PIndex), "\"\"");

      priority[PIndex].precode = TempStringList->GetText ();
      ApplicationIniFile->WriteString (ExtractFileName (szDxfFile), "PreCode_" + IntToStr (PIndex), "\"" + TempStringList->CommaText + "\"");

      TempStringList->Clear ();
      TempStringList->CommaText = ApplicationIniFile->ReadString (ExtractFileName (szDxfFile), "PostCode_" + IntToStr (PIndex), "\"\"");

      priority[PIndex].postcode = TempStringList->GetText ();
      ApplicationIniFile->WriteString (ExtractFileName (szDxfFile), "PostCode_" + IntToStr (PIndex), "\"" + TempStringList->CommaText + "\"");
    }

    delete TempStringList;

    // Convert options
    convertop.ijrel = ApplicationIniFile->ReadBool (ExtractFileName (szDxfFile), "IJRel", true);
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "IJRel", convertop.ijrel);

    convertop.ijfirst = ApplicationIniFile->ReadBool (ExtractFileName (szDxfFile), "IJFirst", false);
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "IJFirst", convertop.ijfirst);

    convertop.line_num = ApplicationIniFile->ReadBool (ExtractFileName (szDxfFile), "LineNum", false);
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "LineNum", convertop.line_num);

    convertop.extraz = ApplicationIniFile->ReadBool (ExtractFileName (szDxfFile), "ZifChange", false);
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "ZifChange", convertop.extraz);

    Bn_Convert->Enabled = true;
    ActiveControl = Bn_Convert;
  }

  // prevent user interference
  Bn_FileOpen->Enabled = true;
  Bn_Convert->Enabled = true;
  Bn_Setup->Enabled = true;
  Mn_File->Enabled = true;
  BN_DXFView->Enabled = true;
}

//---------------------------------------------------------------------------
void __fastcall TFm_AceConverter::FormCreate (TObject *Sender)
{
   AnsiString MyPath;

   demo = FALSE;
   iLayerCount = 0;
   defaultpriorityoptimization = FALSE;

   // Get Access to My ini file
   ApplicationIniFile = new TIniFile (ExtractFilePath (Application->ExeName) + "ace.ini");

   // convertion defaults (read the wite to create entry in INI file if needed)
   convertop.ijrel = ApplicationIniFile->ReadBool ("Setup", "IJRel", TRUE);
   ApplicationIniFile->WriteBool ("Setup", "IJRel", convertop.ijrel);

   convertop.ijfirst = ApplicationIniFile->ReadBool ("Setup", "IJFirst", FALSE);
   ApplicationIniFile->WriteBool ("Setup", "IJFirst", convertop.ijfirst);

   convertop.line_num = ApplicationIniFile->ReadBool ("Setup", "LineNum", FALSE);
   ApplicationIniFile->WriteBool ("Setup", "LineNum", convertop.line_num);

   convertop.extraz = ApplicationIniFile->ReadBool ("Setup", "ZifChange", FALSE);
   ApplicationIniFile->WriteBool ("Setup", "ZifChange", convertop.extraz);

   // limit defaults read the wite to create entry in INI file if needed)
   precision = ApplicationIniFile->ReadFloat ("Setup", "Precision", 3);
   ApplicationIniFile->WriteFloat ("Setup", "Precision", precision);

   defaultzoffset = ApplicationIniFile->ReadFloat ("Setup", "DefZOffset", -1.5);
   ApplicationIniFile->WriteFloat ("Setup", "DefZOffset", defaultzoffset);

   defaultmaxzpass = ApplicationIniFile->ReadFloat ("Setup", "DefZMaxpass", 100);
   ApplicationIniFile->WriteFloat ("Setup", "DefZMaxpass", defaultmaxzpass);

   defaultreleaseplane = ApplicationIniFile->ReadFloat ("Setup", "DefReleasePlane", 30);
   ApplicationIniFile->WriteFloat ("Setup", "DefReleasePlane", defaultreleaseplane);

   defaultcloseenough = ApplicationIniFile->ReadFloat ("Setup", "DefCloseEnough", 0.01);
   ApplicationIniFile->WriteFloat ("Setup", "DefCloseEnough", defaultcloseenough);

   // Path Defaults
   MyPath = ApplicationIniFile->ReadString ("Setup", "DXFPath", ExtractFilePath (Application->ExeName));
   ApplicationIniFile->WriteString ("Setup", "DXFPath", MyPath);

   MyPath = ApplicationIniFile->ReadString ("Setup", "NCPath", ExtractFilePath (Application->ExeName));
   ApplicationIniFile->WriteString ("Setup", "NCPath", MyPath);

   // New setup

   ///////////////
   AddGotoStartCommand_ToFileStart = ApplicationIniFile->ReadBool ("Setup", "AddGotoStart_ToFileStart", true );
   ApplicationIniFile->WriteBool ("Setup", "AddGotoStart_ToFileStart", AddGotoStartCommand_ToFileStart);

   Default_GotoStart_X = ApplicationIniFile->ReadFloat ("Setup", "Default_GotoStart_X", 0.0);
   ApplicationIniFile->WriteFloat ("Setup", "Default_GotoStart_X", Default_GotoStart_X);

   Default_GotoStart_Y = ApplicationIniFile->ReadFloat ("Setup", "Default_GotoStart_Y", 0.0);
   ApplicationIniFile->WriteFloat ("Setup", "Default_GotoStart_Y", Default_GotoStart_Y);

   Default_GotoStart_Z = ApplicationIniFile->ReadFloat ("Setup", "Default_GotoStart_Z", 0.0);
   ApplicationIniFile->WriteFloat ("Setup", "Default_GotoStart_Z", Default_GotoStart_Z);

   ///////////////
   AddGotoEndCommand_ToFileEnd = ApplicationIniFile->ReadBool ("Setup", "AddGotoEnd_ToFileEnd", true);
   ApplicationIniFile->WriteBool ("Setup", "AddGotoEnd_ToFileEnd", AddGotoEndCommand_ToFileEnd);

   Default_GotoEnd_X = ApplicationIniFile->ReadFloat ("Setup", "Default_GotoEnd_X", 0.0);
   ApplicationIniFile->WriteFloat ("Setup", "Default_GotoEnd_X", Default_GotoEnd_X);

   Default_GotoEnd_Y = ApplicationIniFile->ReadFloat ("Setup", "Default_GotoEnd_Y", 0.0);
   ApplicationIniFile->WriteFloat ("Setup", "Default_GotoEnd_Y", Default_GotoEnd_Y);

   Default_GotoEnd_Z = ApplicationIniFile->ReadFloat ("Setup", "Default_GotoEnd_Z", 0.0);
   ApplicationIniFile->WriteFloat ("Setup", "Default_GotoEnd_Z", Default_GotoEnd_Z);

   //////////////////
   Add_M4_M5_ForSpindleControl = ApplicationIniFile->ReadBool ("Setup", "AddSpindleControlCommands", true);
   ApplicationIniFile->WriteBool ("Setup", "AddSpindleControlCommands", Add_M4_M5_ForSpindleControl);

   // update file menu
   Mn_FileClick (Sender);
}

//---------------------------------------------------------------------------
void __fastcall TFm_AceConverter::Bn_SetupClick (TObject *Sender)
{
  // show
  Fm_SetupOptions->ED_DimensionPrecision->Text  = IntToStr (precision);

  Fm_SetupOptions->ED_DefaultZOffset->Text      = FloatToStrF (defaultzoffset,      ffFixed, 10, 6);
  Fm_SetupOptions->ED_DefaultMaxZPass->Text     = FloatToStrF (defaultmaxzpass,     ffFixed, 10, 6);
  Fm_SetupOptions->ED_DefaultReleasePlane->Text = FloatToStrF (defaultreleaseplane, ffFixed, 10, 6);
  Fm_SetupOptions->ED_DefaultCloseEnough->Text  = FloatToStrF (defaultcloseenough,  ffFixed, 10, 6);

  Fm_SetupOptions->CK_AddGotoStartCommand_ToFileStart->Checked = AddGotoStartCommand_ToFileStart;
  Fm_SetupOptions->ED_StartX->Text              = FloatToStrF (Default_GotoStart_X, ffFixed, 10, 2);
  Fm_SetupOptions->ED_StartY->Text              = FloatToStrF (Default_GotoStart_Y, ffFixed, 10, 2);
  Fm_SetupOptions->ED_StartZ->Text              = FloatToStrF (Default_GotoStart_Z, ffFixed, 10, 2);

  Fm_SetupOptions->CK_AddGotoEndCommand_ToFileEnd->Checked     = AddGotoEndCommand_ToFileEnd;
  Fm_SetupOptions->ED_EndX->Text                = FloatToStrF (Default_GotoEnd_X, ffFixed, 10, 2);
  Fm_SetupOptions->ED_EndY->Text                = FloatToStrF (Default_GotoEnd_Y, ffFixed, 10, 2);
  Fm_SetupOptions->ED_EndZ->Text                = FloatToStrF (Default_GotoEnd_Z, ffFixed, 10, 2);

  Fm_SetupOptions->CK_StartAndStopSpindleCommands->Checked     = Add_M4_M5_ForSpindleControl;

  // view and edit
  if (Fm_SetupOptions->ShowModal () == mrOk) {
    // update and save
    precision = StrToInt (Fm_SetupOptions->ED_DimensionPrecision->Text);

    ApplicationIniFile->WriteInteger ("Setup", "Precision", precision);

    defaultzoffset = StrToFloat (Fm_SetupOptions->ED_DefaultZOffset->Text);
    ApplicationIniFile->WriteFloat ("Setup", "DefZOffset", defaultzoffset);

    defaultmaxzpass = StrToFloat (Fm_SetupOptions->ED_DefaultMaxZPass->Text);
    ApplicationIniFile->WriteFloat ("Setup", "DefZMaxpass", defaultmaxzpass);

    defaultreleaseplane = StrToFloat (Fm_SetupOptions->ED_DefaultReleasePlane->Text);
    ApplicationIniFile->WriteFloat ("Setup", "DefReleasePlane", defaultreleaseplane);

    defaultcloseenough = StrToFloat (Fm_SetupOptions->ED_DefaultCloseEnough->Text);
    ApplicationIniFile->WriteFloat ("Setup", "DefCloseEnough", defaultcloseenough);

    ///////////////
    AddGotoStartCommand_ToFileStart = Fm_SetupOptions->CK_AddGotoStartCommand_ToFileStart->Checked;
    ApplicationIniFile->WriteBool ("Setup", "AddGotoStart_ToFileStart", AddGotoStartCommand_ToFileStart);

    Default_GotoStart_X = StrToFloat (Fm_SetupOptions->ED_StartX->Text);
    ApplicationIniFile->WriteFloat ("Setup", "Default_GotoStart_X", Default_GotoStart_X);

    Default_GotoStart_Y = StrToFloat (Fm_SetupOptions->ED_StartY->Text);
    ApplicationIniFile->WriteFloat ("Setup", "Default_GotoStart_Y", Default_GotoStart_Y);

    Default_GotoStart_Z = StrToFloat (Fm_SetupOptions->ED_StartZ->Text);
    ApplicationIniFile->WriteFloat ("Setup", "Default_GotoStart_Z", Default_GotoStart_Z);

    ///////////////
    AddGotoEndCommand_ToFileEnd = Fm_SetupOptions->CK_AddGotoEndCommand_ToFileEnd->Checked;
    ApplicationIniFile->WriteBool ("Setup", "AddGotoEnd_ToFileEnd", AddGotoEndCommand_ToFileEnd);

    Default_GotoEnd_X = StrToFloat (Fm_SetupOptions->ED_EndX->Text);
    ApplicationIniFile->WriteFloat ("Setup", "Default_GotoEnd_X", Default_GotoEnd_X);

    Default_GotoEnd_Y = StrToFloat (Fm_SetupOptions->ED_EndY->Text);
    ApplicationIniFile->WriteFloat ("Setup", "Default_GotoEnd_Y", Default_GotoEnd_Y);

    Default_GotoEnd_Z = StrToFloat (Fm_SetupOptions->ED_EndZ->Text);
    ApplicationIniFile->WriteFloat ("Setup", "Default_GotoEnd_Z", Default_GotoEnd_Z);

    //////////////////
    Add_M4_M5_ForSpindleControl = Fm_SetupOptions->CK_StartAndStopSpindleCommands->Checked;
    ApplicationIniFile->WriteBool ("Setup", "AddSpindleControlCommands", Add_M4_M5_ForSpindleControl);
  }
}

//---------------------------------------------------------------------------
int __fastcall TFm_AceConverter::ReadLayer (void)
{
  TStringList * TotalLineTester;

  int           i, count = 0, temp;
  unsigned long Lines = 0;
  char          string[100];
  fpos_t        pos;
  FILE * ifp;
  ifp = fopen (szDxfFile.c_str (), "r");

  // determine total line is file
  TotalLineTester = new TStringList;
  TotalLineTester->LoadFromFile (szDxfFile);
  TotalLines = TotalLineTester->Count * 2;
  delete TotalLineTester;

  // load the file
  while ( 1 ) {
    LB_GCode_Filename->Caption = "Lines: " + IntToStr (Lines);

    Lines++;

    if ((Lines % 40) == 30) {
      LB_GCode_Filename->Refresh ();

      Application->ProcessMessages ();
      if (Lines>TotalLines) {
        // errors ?
        MessageDlg ("An Unknown Loading error has Occured", mtError, TMsgDlgButtons () << mbOK, 0);
        break;
      }
    }

    if ((temp = fscanf (ifp, "%s", string) ) != EOF && temp != 0 && strcmp (string, "0") == 0) {
      fgetpos (ifp, & pos);
      if ((temp = fscanf (ifp, "%s", string) ) != EOF && temp != 0 && strcmp (string, "LAYER") == 0) {
        while ( 1 ) {
          if ( (temp = fscanf (ifp, "%s", string) ) == EOF || temp == 0 )
            break;

          if ( strcmp (string, "0") == 0 )
            break;
          else if ( strcmp (string, "2") == 0 ) {
            temp_lay = (struct layer_obj *)calloc (++count, sizeof (struct layer_obj));

            for ( i = 0; i < count - 1; i++ )
              temp_lay[i] = layer[i];

            if ( count>1 )
              free (layer);

            layer = temp_lay;
            temp = fscanf (ifp, "%s", layer[count - 1].name);
            break;
          }

          else if ( (temp = fscanf (ifp, "%s", string) ) == EOF || temp == 0 )
            break;
        }
      } else
        fsetpos (ifp, & pos);
    }

    if ( temp == 0 || temp == EOF )
      break;
  }

  if (count == 0) {
    layer = (struct layer_obj *)calloc (++count, sizeof (struct layer_obj));
    sprintf (layer[0].name, "0");
  }

  for ( i = 0; i < count; i++ ) {
    layer[i].status = TRUE;

    layer[i].priority = 1;
    layer[i].depth = defaultmaxzpass;
    layer[i].arc = IDD_EITHERARC;
    layer[i].zchar = 'Z';
    layer[i].zoffset = defaultzoffset;
  }

  fclose (ifp);
  return count;
}

//---------------------------------------------------------------------------

int __fastcall TFm_AceConverter::NewPriority (int count)
{
  int i;

  temp_pri = (struct priority_obj *)calloc ((count + 1), sizeof (struct priority_obj));

  for ( i = 0; i < count; i++ )
    temp_pri[i] = priority[i];

  if ( count>0 )
    free (priority);

  priority = temp_pri;
  priority[count].release = defaultreleaseplane;
  priority[count].close = defaultcloseenough;
  priority[count].optimize = defaultpriorityoptimization;
  priority[count].precode = (char *)calloc (1, sizeof (char));
  priority[count].postcode = (char *)calloc (1, sizeof (char));
  return count + 1;
}

//---------------------------------------------------------------------------

int __fastcall TFm_AceConverter::DelPriority (int num, int count)
{
  int i;

  if ( num>0 && num<=count )
    free (priority[num - 1].precode);

  if ( num>0 && num<=count )
    free (priority[num - 1].postcode);

  for ( i = num; i < count; i++ )
    priority[i - 1] = priority[i];

  if ( count == 1 )
    free (priority);

  return count - 1;
}

void __fastcall TFm_AceConverter::BN_DXFViewClick (TObject *Sender)
{
  Fm_DXFViewer = new TFm_DXFViewer (this);

  // view 
  Fm_DXFViewer->ShowModal ();

  delete Fm_DXFViewer;
}

//---------------------------------------------------------------------------
void __fastcall TFm_AceConverter::Bn_ConvertClick (TObject *Sender)
{
  int Index;

  int PIndex;
  TStringList * TempStringList;

  // save settings for this file
  for ( Index = 0; Index < iLayerCount; Index++ ) {
    // Load the layer setting for this file
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "TurnLayerOff_" + IntToStr (Index), layer[Index].status);

    ApplicationIniFile->WriteInteger (ExtractFileName (szDxfFile), "Priority_" + IntToStr (Index), layer[Index].priority);

    ApplicationIniFile->WriteString (ExtractFileName (szDxfFile), "Z_Char_" + IntToStr (Index), layer[Index].zchar);
    ApplicationIniFile->WriteInteger(ExtractFileName (szDxfFile), "ArcDir_" + IntToStr (Index), layer[Index].arc);
    ApplicationIniFile->WriteFloat  (ExtractFileName (szDxfFile), "Depth_" + IntToStr (Index), layer[Index].depth);
    ApplicationIniFile->WriteFloat  (ExtractFileName (szDxfFile), "ZOffset_" + IntToStr (Index), layer[Index].zoffset);
  }

  // save the priority setting for this file
  TempStringList = new TStringList; // used for convertion between "","" to lines

  for ( PIndex = 0; PIndex < iPrioCount; PIndex++ ) {
    ApplicationIniFile->WriteFloat (ExtractFileName (szDxfFile), "ReleasePane_" + IntToStr (PIndex), priority[PIndex].release);

    ApplicationIniFile->WriteFloat (ExtractFileName (szDxfFile), "CloseEnough_" + IntToStr (PIndex), priority[PIndex].close);

    ApplicationIniFile->WriteFloat (ExtractFileName (szDxfFile), "Optimize_" + IntToStr (PIndex), priority[PIndex].optimize);

    TempStringList->Clear ();
    TempStringList->SetText (priority[PIndex].precode);
    ApplicationIniFile->WriteString (ExtractFileName (szDxfFile), "PreCode_" + IntToStr (PIndex), "\"" + TempStringList->CommaText + "\"");

    TempStringList->Clear ();
    TempStringList->SetText (priority[PIndex].postcode);
    ApplicationIniFile->WriteString (ExtractFileName (szDxfFile), "PostCode_" + IntToStr (PIndex), "\"" + TempStringList->CommaText + "\"");
  }

  delete TempStringList;

  // Setup
  Fm_ConvertionOptions->CK_GenerateIandJasRelativeCoordinates->Checked = convertop.ijrel;
  Fm_ConvertionOptions->CK_GenerateIandJbeforeOtherCoordinatesInBlock->Checked = convertop.ijfirst;
  Fm_ConvertionOptions->CK_GenerateBlockNumbers->Checked = convertop.line_num;
  Fm_ConvertionOptions->CK_GenerateZonlyIfChanging->Checked = convertop.extraz;

  // view and edit
  if (Fm_ConvertionOptions->ShowModal () == mrOk) {
    // update
    convertop.ijrel = Fm_ConvertionOptions->CK_GenerateIandJasRelativeCoordinates->Checked;

    convertop.ijfirst = Fm_ConvertionOptions->CK_GenerateIandJbeforeOtherCoordinatesInBlock->Checked;
    convertop.line_num = Fm_ConvertionOptions->CK_GenerateBlockNumbers->Checked;
    convertop.extraz = Fm_ConvertionOptions->CK_GenerateZonlyIfChanging->Checked;

    // save changes (defaults)
    ApplicationIniFile->WriteBool ("Setup", "IJRel", convertop.ijrel);
    ApplicationIniFile->WriteBool ("Setup", "IJFirst", convertop.ijfirst);
    ApplicationIniFile->WriteBool ("Setup", "LineNum", convertop.line_num);
    ApplicationIniFile->WriteBool ("Setup", "ZifChange", convertop.extraz);

    // against this file
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "IJRel", convertop.ijrel);
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "IJFirst", convertop.ijfirst);
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "LineNum", convertop.line_num);
    ApplicationIniFile->WriteBool (ExtractFileName (szDxfFile), "ZifChange", convertop.extraz);

    // setup
    dlgSave_SaveCNC->InitialDir = ApplicationIniFile->ReadString ("Setup", "NCPath", ExtractFilePath (szDxfFile));
    dlgSave_SaveCNC->FileName = ChangeFileExt (ExtractFileName (szDxfFile), ".nc");

    // choose
    if (dlgSave_SaveCNC->Execute ()) {
      szToFileName = dlgSave_SaveCNC->FileName;

      ApplicationIniFile->WriteString ("Setup", "NCPath", ExtractFilePath (szToFileName));

      // delete file if already exists
      DeleteFile (szToFileName);

      Bn_FileOpen->Enabled = false;
      Bn_Convert->Enabled = false;
      Bn_Setup->Enabled = false;
      LST_Layers->Enabled = false;
      LST_Priority->Enabled = false;

      // Converting
      LB_GCode_Filename->Caption = "Converting file...";
      Convert ();
      LB_GCode_Filename->Caption = "GCode Filename: " + dlgSave_SaveCNC->FileName;
      MessageDlg ("Convertion Completed", mtInformation, TMsgDlgButtons () << mbOK, 0);

      Bn_FileOpen->Enabled = true;
      Bn_Convert->Enabled = true;
      Bn_Setup->Enabled = true;
      LST_Layers->Enabled = true;
      LST_Priority->Enabled = true;
    }
  }
}

void __fastcall TFm_AceConverter::LST_LayersDblClick (TObject *Sender)
{
  int  i;

  bool OK;
  int  iLayer = LST_Layers->ItemIndex;
  char szString[50];

  /////////////////////////////////////////////////////////////////////////////
  // Setup
  Fm_LayerProperties->CK_TurnLayerOff->Checked = layer[iLayer].status == 0;
  Fm_LayerProperties->LB_LayerValue->Caption = layer[iLayer].name;
  Fm_LayerProperties->ED_ZOffset->Text = FloatToStrF (layer[iLayer].zoffset, ffFixed, 10, 6);
  Fm_LayerProperties->ED_MaxZPerPass->Text = FloatToStrF (layer[iLayer].depth, ffFixed, 10, 6);
  Fm_LayerProperties->LS_ZCharacter->ItemIndex = Fm_LayerProperties->LS_ZCharacter->Items->IndexOf (layer[iLayer].zchar);

  // convert to index
  switch ( layer[iLayer].arc ) {
    case IDD_EITHERARC: {
      Fm_LayerProperties->RG_ArcDirection->ItemIndex = 0;
      break;
    }
    case IDD_CCWARC: {
      Fm_LayerProperties->RG_ArcDirection->ItemIndex = 1;
      break;
    }
    case IDD_CWARC: {
      Fm_LayerProperties->RG_ArcDirection->ItemIndex = 2;
      break;
    }
    default: {
      Fm_LayerProperties->RG_ArcDirection->ItemIndex = 0;
    }
  }

  // make the priority items
  Fm_LayerProperties->LS_Priority->Clear ();

  for ( i = 0; i <= iPrioCount; i++ ) {
    Fm_LayerProperties->LS_Priority->Items->Add (IntToStr (i + 1));
  }

  Fm_LayerProperties->LS_Priority->ItemIndex = Fm_LayerProperties->LS_Priority->Items->IndexOf (IntToStr (layer[iLayer].priority));

  /////////////////////////////////////////////////////////////////////////////
  // Edit & Save if OK
  if (Fm_LayerProperties->ShowModal () == mrOk) {
    // Save
    layer[iLayer].status = !Fm_LayerProperties->CK_TurnLayerOff->Checked;

    layer[iLayer].zoffset = StrToFloat (Fm_LayerProperties->ED_ZOffset->Text);
    layer[iLayer].depth = StrToFloat (Fm_LayerProperties->ED_MaxZPerPass->Text);
    layer[iLayer].zchar = Fm_LayerProperties->LS_ZCharacter->Items->Strings[Fm_LayerProperties->LS_ZCharacter->ItemIndex].c_str () [0];
    layer[iLayer].priority = StrToInt (Fm_LayerProperties->LS_Priority->Items->Strings[Fm_LayerProperties->LS_Priority->ItemIndex]);

    // convert to index
    switch ( Fm_LayerProperties->RG_ArcDirection->ItemIndex ) {
      case 0: {
        layer[iLayer].arc = IDD_EITHERARC;
        break;
      }
      case 1: {
        layer[iLayer].arc = IDD_CCWARC;
        break;
      }
      case 2: {
        layer[iLayer].arc = IDD_CWARC;
        break;
      }
      default: {
        layer[iLayer].arc = IDD_EITHERARC;
      }
    }

    // update the layer list
    sprintf (szString, "%s ... %d", layer[iLayer].name, layer[iLayer].priority);
    LST_Layers->Items->Strings[LST_Layers->ItemIndex] = szString;

    // update the priorities list
    for ( i = 0; i < iLayerCount; i++ ) {
      if (layer[i].priority == (iPrioCount + 1)) {
        iPrioCount = NewPriority (iPrioCount);
        LST_Priority->Items->Add (IntToStr (iPrioCount));
      }
    }
  }
}

void __fastcall TFm_AceConverter::LST_PriorityDblClick (TObject *Sender)
{
  int iPriority = LST_Priority->ItemIndex;

  // show
  Fm_PriorityProperties->LB_PriorityValue->Caption = IntToStr (iPriority + 1);
  Fm_PriorityProperties->CK_Optimize->Checked = priority[iPriority].optimize == 1;
  Fm_PriorityProperties->ED_ReleasePane->Text = FloatToStrF (priority[iPriority].release, ffFixed, 10, 6);
  Fm_PriorityProperties->ED_CloseEnough->Text = FloatToStrF (priority[iPriority].close, ffFixed, 10, 6);
  Fm_PriorityProperties->MM_PrePriorityCode->Lines->SetText (priority[iPriority].precode);
  Fm_PriorityProperties->MM_PostPriorityCode->Lines->SetText (priority[iPriority].postcode);

  // edit and save
  if (Fm_PriorityProperties->ShowModal () == mrOk) {
    // save
    priority[iPriority].optimize = Fm_PriorityProperties->CK_Optimize->Checked;

    priority[iPriority].release = StrToFloat (Fm_PriorityProperties->ED_ReleasePane->Text);
    priority[iPriority].close = StrToFloat (Fm_PriorityProperties->ED_CloseEnough->Text);
    priority[iPriority].precode = Fm_PriorityProperties->MM_PrePriorityCode->Lines->GetText ();
    priority[iPriority].postcode = Fm_PriorityProperties->MM_PostPriorityCode->Lines->GetText ();
  }
}

void __fastcall TFm_AceConverter::FormDestroy (TObject *Sender)
{
  // all finished
  ApplicationIniFile->UpdateFile ();

  delete ApplicationIniFile;

  // free used memory
  while ( iPrioCount>0 )
    iPrioCount = DelPriority (iPrioCount, iPrioCount);

  if ( iLayerCount>0 )
    free (layer);
}

//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Exit1Click (TObject *Sender) { Close (); }

//---------------------------------------------------------------------------
void __fastcall TFm_AceConverter::About1Click (TObject *Sender)
{
  AnsiString Help = ExtractFilePath (Application->ExeName) + "\\doc\\index.html";

  ShellExecute (this, "open", Help.c_str (), NULL, NULL, SW_SHOWDEFAULT);
}

//---------------------------------------------------------------------------
void __fastcall TFm_AceConverter::Open1Click (TObject *Sender) { Bn_FileOpen->Click (); }

//---------------------------------------------------------------------------
void __fastcall TFm_AceConverter::Abount1Click (TObject *Sender) {
// view and edit
   Fm_AboutBox->ShowModal (); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_ConvertClick (TObject *Sender) { Bn_Convert->Click (); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Setup1Click (TObject *Sender) { Bn_Setup->Click (); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_FileClick (TObject *Sender)
{
  Mn_Convert->Enabled = Bn_Convert->Enabled;

  // load settings
  Mn_OldFile1->Caption = ApplicationIniFile->ReadString ("Setup", "File_1", "");
  Mn_OldFile1->Visible = Mn_OldFile1->Caption != "";

  Mn_OldFile2->Caption = ApplicationIniFile->ReadString ("Setup", "File_2", "");
  Mn_OldFile2->Visible = Mn_OldFile2->Caption != "";

  Mn_OldFile3->Caption = ApplicationIniFile->ReadString ("Setup", "File_3", "");
  Mn_OldFile3->Visible = Mn_OldFile3->Caption != "";

  Mn_OldFile4->Caption = ApplicationIniFile->ReadString ("Setup", "File_4", "");
  Mn_OldFile4->Visible = Mn_OldFile4->Caption != "";

  Mn_OldFile5->Caption = ApplicationIniFile->ReadString ("Setup", "File_5", "");
  Mn_OldFile5->Visible = Mn_OldFile5->Caption != "";

  Mn_OldFile6->Caption = ApplicationIniFile->ReadString ("Setup", "File_6", "");
  Mn_OldFile6->Visible = Mn_OldFile6->Caption != "";

  Mn_OldFile7->Caption = ApplicationIniFile->ReadString ("Setup", "File_7", "");
  Mn_OldFile7->Visible = Mn_OldFile7->Caption != "";

  Mn_OldFile8->Caption = ApplicationIniFile->ReadString ("Setup", "File_8", "");
  Mn_OldFile8->Visible = Mn_OldFile8->Caption != "";

  Mn_OldFile9->Caption = ApplicationIniFile->ReadString ("Setup", "File_9", "");
  Mn_OldFile9->Visible = Mn_OldFile9->Caption != "";

  Mn_OldFile10->Caption = ApplicationIniFile->ReadString ("Setup", "File_10", "");
  Mn_OldFile10->Visible = Mn_OldFile10->Caption != "";
}

void __fastcall TFm_AceConverter::Properties1Click (TObject *Sender)
{
  Mn_LayerProperties->Enabled = Bn_Convert->Enabled;

  Mn_PriorityProperties->Enabled = Bn_Convert->Enabled;

  if (Bn_Convert->Enabled) {
    Mn_LayerProperties->Caption = "&Layer Properties:   \" " + LST_Layers->Items->Strings[LST_Layers->ItemIndex] + " \"";
    Mn_PriorityProperties->Caption = "&Priority Properties:   \" " + LST_Priority->Items->Strings[LST_Priority->ItemIndex] + " \"";
  }
}
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_LayerPropertiesClick (TObject *Sender) { LST_LayersDblClick (Sender); }
//---------------------------------------------------------------------------

void __fastcall TFm_AceConverter::Mn_PriorityPropertiesClick (TObject *Sender) { LST_PriorityDblClick (Sender); }

//---------------------------------------------------------------------------

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
int __fastcall TFm_AceConverter::MyMin (int v1, int v2)
{
  int Result = v2;

  if (v1<v2) {
    Result = v1;
  }

  return Result;
}

void __fastcall TFm_AceConverter::Convert (void)
{
  int               ctemp,       zchar,     wchar,    achar,    cchar, count = 0, closed,     msg = 0,      Passes;

  long              i,           lay_index, k,        j,        l,     temp,      num_of_ent, line_num = 1, cur_ent,
                    cur_dir,     delay,     display;
  double            x1,          y1,        z1,       ztemp,    x2,    y2,        z2,         dist1,        dist2,
                    cur_dist,    zref,      x1st = 0, y1st = 0, z1st = 0;
  char              string[100], type[100], status[100];
  struct entity_obj temp_ent,    temp_ent2;
  far struct entity_obj *entity;
  FILE * ifp, *ofp;
  fpos_t        pos;

  unsigned long Lines = 0;

  LB_GCode_Filename->Caption = "Begin Converting Process";
  LB_GCode_Filename->Refresh ();

  if ((ifp = fopen (szDxfFile.c_str (), "r") ) == NULL) {
    LB_GCode_Filename->Caption = "Invalid .dxf File Format";
    return;
  }

  if ((ofp = fopen (szToFileName.c_str (), "w") ) == NULL) {
    fclose (ifp);

    LB_GCode_Filename->Caption = "Invalid Output File";
    return;
  }

  if ((entity = (struct entity_obj *)farcalloc (1, sizeof (struct entity_obj)) ) == NULL) {
    msg = 2;
    goto DONE;
  }

  if (AddGotoStartCommand_ToFileStart) {
    fprintf (ofp, "G00 Z%.*f \n",      precision,Default_GotoStart_Z  ); // pull up before moving
    fprintf (ofp, "G00 X%.*f Y%.*f \n",precision,Default_GotoStart_X,   precision,Default_GotoStart_Y );
  }

  if (Add_M4_M5_ForSpindleControl) {
    fprintf (ofp, "M4 \n");
  }

  for ( i = 0; i < iPrioCount; i++ ) { //alocate and store entities
    num_of_ent = 0;

    display = 100;

    while ( 1 ) {
      LB_GCode_Filename->Caption = "Lines: " + IntToStr (Lines);

      Lines++;

      if ((Lines % 40) == 30) {
        LB_GCode_Filename->Refresh ();

        Application->ProcessMessages ();
        if (Lines>(TotalLines * 10)) {
          // errors ?
          MessageDlg ("An Unknown Processing error has Occured", mtError, TMsgDlgButtons () << mbOK, 0);
          break;
        }
      }

      if ( (temp = fscanf (ifp, "%s", string) ) == 0 || temp == EOF )
        break;

      if ( strcmp (string, "0") != 0 )
        continue;

      fgetpos (ifp, & pos);

      if ( (temp = fscanf (ifp, "%s", type) ) == 0 || temp == EOF )
        break;

      if (num_of_ent>=display) {
        display = num_of_ent + 100;

        sprintf (status, "Stored %u Ent., Prio. %u", num_of_ent, i + 1);
        LB_GCode_Filename->Caption = status;
        LB_GCode_Filename->Refresh ();
      }

      if (strcmp (type, "LINE") == 0 || strcmp (type, "CIRCLE") == 0 || strcmp (type, "ARC") == 0 || strcmp (type, "POINT") == 0 || strcmp (type, "POLYLINE") == 0) {
        if (get_string (ifp, "8", string) == 0) {
          msg = 1;
          goto DONE;
        }

        for ( lay_index = 0; lay_index < iLayerCount; lay_index++ )
          if (strcmp (layer[lay_index].name, string) == 0 && layer[lay_index].priority - 1 == i && layer[lay_index].status == TRUE)
            break;

        if ( lay_index == iLayerCount )
          continue;
        if (strcmp (type, "POLYLINE") != 0) {
          if ((entity = (entity_obj *)farrealloc (entity, (++num_of_ent) * sizeof (struct entity_obj)) ) == NULL) {
            msg = 2;
            goto DONE;
          }
        }
      } else if ( strcmp (type, "BLOCK") == 0 ) {
        while ( 1 ) {
          if ((temp = fscanf (ifp, "%s", string) ) == 0 || temp == EOF) {
            msg = 1;
            goto DONE;
          }

          if (strcmp (string, "0") == 0) {
            fgetpos (ifp, & pos);

            if ((temp = fscanf (ifp, "%s", string) ) == 0 || temp == EOF) {
              msg = 1;
              goto DONE;
            }
            if ( strcmp (string, "ENDBLK") == 0 )
              break;
            else
              fsetpos (ifp, & pos);
          }
        }
        continue;
      } else {
        fsetpos (ifp, & pos);
        continue;
      }

      if (strcmp (type, "POLYLINE") == 0) {
        delay = zref = 0;

        if ((temp = get_values (ifp, & temp_ent2) ) == 0) {
          msg = 1;
          goto DONE;
        }

        if ( temp == 2 )
          closed = TRUE;
        else
          closed = FALSE;

        zref = temp_ent2.z1;

        while ( (temp = fscanf (ifp, "%s", string) ) != EOF && temp != 0 ) {
          if ( strcmp (string, "SEQEND") == 0 && closed == FALSE )
            break;

          if (strcmp (string, "VERTEX") == 0 || (strcmp (string, "SEQEND") == 0 && closed == TRUE)) {
            if (strcmp (string, "SEQEND") == 0 && closed == TRUE) {
              temp_ent.x2 = x1st;

              temp_ent.y2 = y1st;
              temp_ent.z2 = z1st;
            } else {
              if (get_values (ifp, & temp_ent2) == 0) {
                msg = 1;
                goto DONE;
              }

              temp_ent.x2 = temp_ent2.x1;
              temp_ent.y2 = temp_ent2.y1;
              temp_ent.z2 = temp_ent2.z1 + zref;
            }

            if (delay>0) {
              if ((entity = (entity_obj *)
              farrealloc (entity, (++num_of_ent) * sizeof (struct entity_obj)) ) == NULL) {
                msg = 2;
                goto DONE;
              }

              entity[num_of_ent - 1] = temp_ent;

              if ( entity[num_of_ent - 1].type == ARC )
                make_arc (& entity[num_of_ent - 1]);

              entity[num_of_ent - 1].layer = lay_index;

              if (entity[num_of_ent - 1].type == LINE) {
                entity[num_of_ent - 1].dir = FOR;

                entity[num_of_ent - 1].z1 = entity[num_of_ent - 1].z1 + layer[lay_index].zoffset;
                entity[num_of_ent - 1].z2 = entity[num_of_ent - 1].z2 + layer[lay_index].zoffset;
                for ( k = 1; k * -layer[lay_index].depth>MyMin (entity[num_of_ent - 1].z1, entity[num_of_ent - 1].z2); k++ ) {
                  if ((entity = (entity_obj *)
                  farrealloc (entity, (++num_of_ent) * sizeof (struct entity_obj)) ) == NULL) {
                    msg = 2;
                    goto DONE;
                  }

                  entity[num_of_ent - 1] = entity[num_of_ent - 2];

                  if ( entity[num_of_ent - 2].z1<k * -layer[lay_index].depth )
                    entity[num_of_ent - 2].z1 = k * -layer[lay_index].depth;

                  if ( entity[num_of_ent - 2].z2<k * -layer[lay_index].depth )
                    entity[num_of_ent - 2].z2 = k * -layer[lay_index].depth;
                }
              }
              if ((entity[num_of_ent - 1].type == ARC) || (entity[num_of_ent - 1].type == PARC)) {
                entity[num_of_ent - 1].z1 = entity[num_of_ent - 1].z1 + layer[lay_index].zoffset;

                if ( layer[lay_index].arc == IDD_CCWARC || layer[lay_index].arc == IDD_EITHERARC )
                  entity[num_of_ent - 1].dir = CCW;
                else
                  entity[num_of_ent - 1].dir = CW;
                for ( k = 1; k * -layer[lay_index].depth>entity[num_of_ent - 1].z1; k++ ) {
                  if ((entity = (entity_obj *)
                  farrealloc (entity, (++num_of_ent) * sizeof (struct entity_obj)) ) == NULL) {
                    msg = 2;
                    goto DONE;
                  }

                  entity[num_of_ent - 1] = entity[num_of_ent - 2];

                  if ( entity[num_of_ent - 2].z1<k * -layer[lay_index].depth )
                    entity[num_of_ent - 2].z1 = k * -layer[lay_index].depth;
                }
              }
            } else {
              delay++;

              x1st = temp_ent.x2;
              y1st = temp_ent.y2;
              z1st = temp_ent.z2;
            }

            if ( strcmp (string, "SEQEND") == 0 && closed == TRUE )
              break;

            temp_ent.radius = temp_ent2.radius;

            if (temp_ent.radius != 0) {
              temp_ent.type = ARC;
            } else {
              temp_ent.type = LINE;
            }

            temp_ent.x1 = temp_ent.x2;
            temp_ent.y1 = temp_ent.y2;
            temp_ent.z1 = temp_ent.z2;
          }
        }

        if ( temp == EOF || temp == 0 )
          break;
        continue;
      }

      if (strcmp (type, "LINE") == 0 || strcmp (type, "POINT") == 0) {
        if ( strcmp (type, "LINE") == 0 )
          entity[num_of_ent - 1].type = LINE;
        else
          entity[num_of_ent - 1].type = POINT;

        entity[num_of_ent - 1].layer = lay_index;

        if ( strcmp (type, "LINE") == 0 )
          entity[num_of_ent - 1].dir = FOR;

        if (get_values (ifp, & entity[num_of_ent - 1]) == 0) {
          msg = 1;
          goto DONE;
        }

        entity[num_of_ent - 1].z1 = entity[num_of_ent - 1].z1 + layer[lay_index].zoffset;

        if ( strcmp (type, "LINE") == 0 )
          entity[num_of_ent - 1].z2 = entity[num_of_ent - 1].z2 + layer[lay_index].zoffset;

        for ( k = 1; k * -layer[lay_index].depth>MyMin (entity[num_of_ent - 1].z1, entity[num_of_ent - 1].z2); k++ ) {
          if ((entity = (entity_obj *)farrealloc (entity, (++num_of_ent) * sizeof (struct entity_obj)) ) == NULL) {
            msg = 2;
            goto DONE;
          }

          entity[num_of_ent - 1] = entity[num_of_ent - 2];

          if ( entity[num_of_ent - 2].z1<k * -layer[lay_index].depth )
            entity[num_of_ent - 2].z1 = k * -layer[lay_index].depth;

          if ( entity[num_of_ent - 2].z2<k * -layer[lay_index].depth )
            entity[num_of_ent - 2].z2 = k * -layer[lay_index].depth;
        }
        continue;
      }

      if (strcmp (type, "ARC") == 0 || strcmp (type, "CIRCLE") == 0) {
        entity[num_of_ent - 1].type = ARC;

        entity[num_of_ent - 1].layer = lay_index;

        if ( layer[lay_index].arc == IDD_CCWARC || layer[lay_index].arc == IDD_EITHERARC )
          entity[num_of_ent - 1].dir = CCW;
        else
          entity[num_of_ent - 1].dir = CW;

        if (get_values (ifp, & entity[num_of_ent - 1]) == 0) {
          msg = 1;
          goto DONE;
        }

        entity[num_of_ent - 1].z1 = entity[num_of_ent - 1].z1 + layer[lay_index].zoffset;

        if (strcmp (type, "ARC") == 0) {
          entity[num_of_ent - 1].ang_start = entity[num_of_ent - 1].ang_start * 2 * PI / 360;
          entity[num_of_ent - 1].ang_end = entity[num_of_ent - 1].ang_end * 2 * PI / 360;
        } else {
          entity[num_of_ent - 1].ang_start = 0;
          entity[num_of_ent - 1].ang_end = 2 * PI;
        }

        for ( k = 1; k * -layer[lay_index].depth>entity[num_of_ent - 1].z1; k++ ) {
          if ((entity = (entity_obj *)farrealloc (entity, (++num_of_ent) * sizeof (struct entity_obj)) ) == NULL) {
            msg = 2;
            goto DONE;
          }

          entity[num_of_ent - 1] = entity[num_of_ent - 2];

          if ( entity[num_of_ent - 2].z1<k * -layer[lay_index].depth )
            entity[num_of_ent - 2].z1 = k * -layer[lay_index].depth;
        }
        continue;
      }
    }

    rewind (ifp);

    //optimize if necessary
    if (priority[i].optimize == TRUE) {
      x1 = y1 = 0;

      z1 = priority[i].release;
      display = 100;
      for ( j = 0; j < num_of_ent; j++ ) {
        if (j + 1 == display) {
          display = display + 100;

          sprintf (status, "Optimizing Priorities %u, %d%% Complete", i + 1, (int)(j + 1) * 100 / num_of_ent);
          LB_GCode_Filename->Caption = status;
          LB_GCode_Filename->Refresh ();
        }

        for ( k = j; k < num_of_ent; k++ ) {
          if (((entity[k].type == ARC) || (entity[k].type == PARC)) && layer[entity[k].layer].arc == IDD_CCWARC) {
            get_first_point (& x2, & y2, & z2, & entity[k]);

            dist1 = sqrt (pow (x2 - x1, 2) + pow (y2 - y1, 2) + pow (z2 - z1, 2));

            if ( j == k )
              cur_dist = dist1;
            if (dist1<cur_dist) {
              cur_ent = k;

              cur_dir = CCW;
              cur_dist = dist1;
            }
          } else if ( ((entity[k].type == ARC) || (entity[k].type == PARC)) && layer[entity[k].layer].arc == IDD_CWARC ) {
            get_second_point (& x2, & y2, & z2, & entity[k]);

            dist1 = sqrt (pow (x2 - x1, 2) + pow (y2 - y1, 2) + pow (z2 - z1, 2));

            if ( j == k )
              cur_dist = dist1;
            if (dist1<=cur_dist) {
              cur_ent = k;

              cur_dir = CW;
              cur_dist = dist1;
            }
          }
          else if ( ((entity[k].type == ARC) || (entity[k].type == PARC)) ) {
            get_first_point (& x2, & y2, & z2, & entity[k]);

            dist1 = sqrt (pow (x2 - x1, 2) + pow (y2 - y1, 2) + pow (z2 - z1, 2));
            get_second_point (& x2, & y2, & z2, & entity[k]);
            dist2 = sqrt (pow (x2 - x1, 2) + pow (y2 - y1, 2) + pow (z2 - z1, 2));

            if ( j == k )
              cur_dist = MyMin (dist1, dist2);

            if (dist1<=cur_dist) {
              cur_ent = k;

              cur_dir = CCW;
              cur_dist = dist1;
            }
            if (dist2<=cur_dist) {
              cur_ent = k;

              cur_dir = CW;
              cur_dist = dist2;
            }
          }
          else if ( entity[k].type == LINE ) {
            get_first_point (& x2, & y2, & z2, & entity[k]);

            dist1 = sqrt (pow (x2 - x1, 2) + pow (y2 - y1, 2) + pow (z2 - z1, 2));
            get_second_point (& x2, & y2, & z2, & entity[k]);
            dist2 = sqrt (pow (x2 - x1, 2) + pow (y2 - y1, 2) + pow (z2 - z1, 2));

            if ( j == k )
              cur_dist = MyMin (dist1, dist2);

            if (dist1<=cur_dist) {
              cur_ent = k;

              cur_dir = FOR;
              cur_dist = dist1;
            }
            if (dist2<=cur_dist) {
              cur_ent = k;

              cur_dir = REV;
              cur_dist = dist2;
            }
          }
          else if ( entity[k].type == POINT ) {
            get_first_point (& x2, & y2, & z2, & entity[k]);

            dist1 = sqrt (pow (x2 - x1, 2) + pow (y2 - y1, 2) + pow (z2 - z1, 2));

            if ( j == k )
              cur_dist = dist1;
            if (dist1<=cur_dist) {
              cur_ent = k;

              cur_dir = REV;
              cur_dist = dist1;
            }
          }
        }

        entity[cur_ent].dir = cur_dir;
        temp_ent = entity[j];
        entity[j] = entity[cur_ent];
        entity[cur_ent] = temp_ent;
        get_second_point (& x1, & y1, & ztemp, & entity[j]);
      }
    } // end of optimize

    //write precode to file
    if (demo == FALSE || (demo == TRUE && count<=20)) {
      if ( strlen (priority[i].precode)>3) {
         fprintf (ofp, "%s", priority[i].precode);
         if ( strlen (priority[i].precode)>0 && priority[i].precode[strlen (priority[i].precode) - 1] != '\n' ) {
           fputc ('\n', ofp);
         }
      }
    }

    // write code to file
    display = 100;

    for ( k = 0; k < num_of_ent; k++ ) { //entity overhead
      if (k + 1 == display) {
        display = display + 100;

        sprintf (status, "Writing Priority %u, %d%% Complete", i + 1, (int)(k + 1) * 100 / num_of_ent);
        LB_GCode_Filename->Caption = status;
        LB_GCode_Filename->Refresh ();
      }

      if (demo == TRUE) {
        if ( count<=20 )
          count++;
        if ( count>20 )
          break;
      }

      get_first_point (& x2, & y2, & z2, & entity[k]);

      if (k == 0) {
        x1 = x2;

        y1 = y2;
        z1 = z2;
        ctemp = layer[entity[k].layer].zchar;
      }

      dist1 = sqrt (pow (x2 - x1, 2) + pow (y2 - y1, 2));

      if (dist1>priority[i].close || k == 0 || entity[k].type == POINT || ctemp != layer[entity[k].layer].zchar) {
        if (k == 0) {
          zchar = wchar = achar = cchar = FALSE;

          for ( l = 0; l < num_of_ent; l++ ) {
            if ( layer[entity[l].layer].zchar == 'Z' )
              zchar = TRUE;

            if ( layer[entity[l].layer].zchar == 'W' )
              wchar = TRUE;

            if ( layer[entity[l].layer].zchar == 'A' )
              achar = TRUE;

            if ( layer[entity[l].layer].zchar == 'C' )
              cchar = TRUE;
          }

          //Added .4 formatter spec

          if ( zchar == TRUE )
            fprintf (ofp, "G00 Z%.*f\n", precision, priority[i].release);

          if ( wchar == TRUE )
            fprintf (ofp, "G00 W%.*f\n", precision, priority[i].release);

          if ( achar == TRUE )
            fprintf (ofp, "G00 A%.*f\n", precision, priority[i].release);
          if ( cchar == TRUE )
            fprintf (ofp, "G00 C%.*f\n", precision, priority[i].release);
        } else if ( ctemp != layer[entity[k].layer].zchar ) {
          fprintf (ofp, "G00 %c%.*f\n", ctemp, precision, priority[i].release);
          ctemp = layer[entity[k].layer].zchar;
        } else
          fprintf (ofp, "G00 %c%.*f\n", layer[entity[k].layer].zchar, precision, priority[i].release);

        fprintf (ofp, "G00 X%.*f Y%.*f\n", precision, x2, precision, y2);
        fprintf (ofp, "G01 %c%.*f\n", layer[entity[k].layer].zchar, precision, z2);
      }

      if ((dist1>.0001 && dist1<=priority[i].close) || fabs (z2 - z1)>.0001) {
        fprintf (ofp, "G01 X%.*f Y%.*f %c%.*f\n", precision, x2, precision, y2, layer[entity[k].layer].zchar, precision, z2);
      }

      get_second_point (& x1, & y1, & z1, & entity[k]);

      //write entity
      //Second point Z is optional here
      if (entity[k].type == LINE) {
        if ( entity[k].dir == FOR )
          if (convertop.extraz == 0) {
            fprintf (ofp, "G01 X%.*f Y%.*f %c%.*f\n", precision, entity[k].x2, precision, entity[k].y2, layer[entity[k].layer].zchar, precision, entity[k].z2);
          } else {
            fprintf (ofp, "G01 X%.*f Y%.*f\n", precision, entity[k].x2, precision, entity[k].y2);
          }
        else if ( convertop.extraz == 0 ) {
          fprintf(ofp,"G01 X%.*f Y%.*f %c%.*f\n",precision,entity[k].x1,precision,entity[k].y1,
          layer[entity[k].layer].zchar, precision,entity[k].z1);
        } else {
          fprintf (ofp, "G01 X%.*f Y%.*f\n", precision, entity[k].x1, precision, entity[k].y1);
        }
      }

      if (entity[k].type == PARC) {
        // polyline arc
        if (entity[k].dir == CCW) {
          fprintf (ofp, "G03 X%.*f Y%.*f R%.*f\n", precision, entity[k].x2, precision, entity[k].y2, precision, entity[k].radius);
        } else {
          fprintf (ofp, "G02 X%.*f Y%.*f R%.*f\n", precision, entity[k].x1, precision, entity[k].y1, precision, entity[k].radius);
        }
      }

      if (entity[k].type == ARC) {
        if (entity[k].dir == CCW) {
          if (convertop.ijfirst == TRUE) {
            if ( convertop.ijrel == TRUE )
              // relative  I J X Y   Counter Clock-Wise
              fprintf (ofp, "G03 I%.*f J%.*f X%.*f Y%.*f\n", precision, -entity[k].radius * cos (entity[k].ang_start), precision, -entity[k].radius * sin (entity[k].ang_start), precision, entity[k].x1 + entity[k].radius * cos (entity[k].ang_end), precision, entity[k].y1 + entity[k].radius * sin (entity[k].ang_end));
            else
              // absolute  I J X Y  Counter Clock-Wise
              fprintf (ofp, "G03 I%.*f J%.*f X%.*f Y%.*f\n", precision, entity[k].x1, precision, entity[k].y1, precision, entity[k].x1 + entity[k].radius * cos (entity[k].ang_end), precision, entity[k].y1 + entity[k].radius * sin (entity[k].ang_end));
          } else {
            if ( convertop.ijrel == TRUE )
              // relative X Y I J   Counter Clock-Wise
              fprintf (ofp, "G03 X%.*f Y%.*f I%.*f J%.*f\n", precision, entity[k].x1 + entity[k].radius * cos (entity[k].ang_end), precision, entity[k].y1 + entity[k].radius * sin (entity[k].ang_end), precision, -entity[k].radius * cos (entity[k].ang_start), precision, -entity[k].radius * sin (entity[k].ang_start));
            else
              // absolute X Y I J   Counter Clock-Wise
              fprintf (ofp, "G03 X%.*f Y%.*f I%.*f J%.*f\n", precision, entity[k].x1 + entity[k].radius * cos (entity[k].ang_end), precision, entity[k].y1 + entity[k].radius * sin (entity[k].ang_end), precision, entity[k].x1, precision, entity[k].y1);
          }
        } else {
          if (convertop.ijfirst == TRUE) {
            if ( convertop.ijrel == TRUE )
              // relative I J X Y   Clock-Wise
              fprintf (ofp, "G02 I%.*f J%.*f X%.*f Y%.*f\n", precision, -entity[k].radius * cos (entity[k].ang_end), precision, -entity[k].radius * sin (entity[k].ang_end), precision, entity[k].x1 + entity[k].radius * cos (entity[k].ang_start), precision, entity[k].y1 + entity[k].radius * sin (entity[k].ang_start));
            else
              // absolute I J X Y   Clock-Wise
              fprintf (ofp, "G02 I%.*f J%.*f X%.*f Y%.*f\n", precision, entity[k].x1, precision, entity[k].y1, precision, entity[k].x1 + entity[k].radius * cos (entity[k].ang_start), precision, entity[k].y1 + entity[k].radius * sin (entity[k].ang_start));
          } else {
            if ( convertop.ijrel == TRUE )
              // relative X Y I J  Clock-Wise
              fprintf (ofp, "G02 X%.*f Y%.*f I%.*f J%.*f\n", precision, entity[k].x1 + entity[k].radius * cos (entity[k].ang_start), precision, entity[k].y1 + entity[k].radius * sin (entity[k].ang_start), precision, -entity[k].radius * cos (entity[k].ang_end), precision, -entity[k].radius * sin (entity[k].ang_end));
            else
              // absolute X Y I J  Clock-Wise
              fprintf (ofp, "G02 X%.*f Y%.*f I%.*f J%.*f\n", precision, entity[k].x1 + entity[k].radius * cos (entity[k].ang_start), precision, entity[k].y1 + entity[k].radius * sin (entity[k].ang_start), precision, entity[k].x1, precision, entity[k].y1);
          }
        }
      }
    }

    if (num_of_ent>0) {
      fprintf (ofp, "G00 %c%.*f\n", layer[entity[num_of_ent - 1].layer].zchar, precision, priority[i].release);
    }

    //write postcode to file
    if (demo == FALSE || (demo == TRUE && count<=20)) {
      if ( strlen (priority[i].postcode)>3 ) {
         fprintf (ofp, "%s", priority[i].postcode);
         if ( strlen (priority[i].postcode)>0 && priority[i].postcode[strlen (priority[i].postcode) - 1] != '\n' ) {
            fputc ('\n', ofp);
         }
      }
    }
  } // priority count

  if (AddGotoEndCommand_ToFileEnd) {
    fprintf (ofp, "G00 Z%.*f \n",      precision,Default_GotoEnd_Z  ); // pull up before moving
    fprintf (ofp, "G00 X%.*f Y%.*f \n",precision,Default_GotoEnd_X,   precision,Default_GotoEnd_Y );
  }

  if (Add_M4_M5_ForSpindleControl) {
    fprintf (ofp, "M5 \n");
  }

  fclose (ofp);
  fclose (ifp);

  if (convertop.line_num == TRUE) {
    ifp = fopen (szToFileName.c_str (), "r");

    ofp = fopen ("temp.tmp", "w");

    while ( (ctemp = fgetc (ifp) ) != EOF ) {
      if (ctemp != '\n') {
        fprintf (ofp, "N%u ", 10 * line_num++);
        fputc (ctemp, ofp);
      } else {
        fputc (ctemp, ofp);
        continue;
      }

      while ( (ctemp = fgetc (ifp) ) != EOF ) {
        fputc (ctemp, ofp);

        if ( ctemp == '\n' )
          break;
      }

      if ( ctemp == EOF )
        break;
    }

    fclose (ofp);
    fclose (ifp);
    ifp = fopen ("temp.tmp", "r");
    ofp = fopen (szToFileName.c_str (), "w");

    while ( (ctemp = fgetc (ifp) ) != EOF )
      fputc (ctemp, ofp);

    fclose (ifp);
    fclose (ofp);
    remove ("temp.tmp");
  }

  if (demo == TRUE) {
    ofp = fopen (szToFileName.c_str (), "a");

    fprintf (ofp, "\n\nThank You For Evaluating ACEconverter\n");
    fprintf (ofp, "Converting Has Stopped Because Of The\n");
    fprintf (ofp, "Evaluation Limit.  To Order Your Licensed\n");
    fprintf (ofp, "Version Of ACEconverter Go To:\n");
    fprintf (ofp, "The World Wide Web At:\n");
    fprintf (ofp, "http://www.yeagerautomation.com\n");
    fclose (ofp);
  }

  sprintf (status, "Converting Complete");
  LB_GCode_Filename->Caption = status;
  LB_GCode_Filename->Refresh ();

  DONE:
  if (msg == 1) {
    fclose (ofp);

    fclose (ifp);
    sprintf (status, "Invalid .dxf File Format");
    LB_GCode_Filename->Caption = status;
  }

  if (msg == 2) {
    fclose (ofp);

    fclose (ifp);
    sprintf (status, "Out Of Memory, Converting Aborted");
    LB_GCode_Filename->Caption = status;
  }

  farfree (entity);
}

int __fastcall TFm_AceConverter::get_values (FILE *ifp, struct entity_obj *ent)
{
  static char string[100];

  int         temp,
              temp2,
              mode = 1;
  fpos_t      pos;

  ent->x1 = ent->y1 = ent->z1 = ent->x2 = ent->y2 = ent->z2 = ent->radius = ent->ang_start = ent->ang_end = 0;

  while ( 1 ) {
    fgetpos (ifp, & pos);

    if ( (temp = fscanf (ifp, "%s", string) ) == EOF || temp == 0 )
      return 0;

    if (strcmp (string, "0") == 0) {
      fsetpos (ifp, & pos);
      return mode;
    } else if ( strcmp (string, "10") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->x1)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "20") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->y1)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "30") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->z1)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "11") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->x2)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "21") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->y2)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "31") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->z2)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "40") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->radius)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "42") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->radius)) ) == 0 || temp == EOF )
        return 0;
    } // Bluge for a polyline
    else if ( strcmp (string, "50") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->ang_start)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "51") == 0 ) {
      if ( (temp = fscanf (ifp, "%lf", & (ent->ang_end)) ) == 0 || temp == EOF )
        return 0;
    }
    else if ( strcmp (string, "70") == 0 ) {
      if ( (temp = fscanf (ifp, "%d", & temp2) ) == 0 || temp == EOF )
        return 0;

      else if ( (temp2 & 1) == 1 )
        mode = 2;
    }

    else if ( (temp = fscanf (ifp, "%s", string) ) == 0 || temp == EOF )
      return 0;
  }
}

int __fastcall TFm_AceConverter::get_string (FILE *ifp, const char *string_match, char *string_ret)
{
  static char string[100];

  int         temp;
  fpos_t      pos;

  while ( 1 ) {
    fgetpos (ifp, & pos);

    if ( (temp = fscanf (ifp, "%s", string) ) == EOF || temp == 0 )
      return 0;

    if (strcmp (string, "0") == 0) {
      fsetpos (ifp, & pos);
      return 1;
    }

    if (strcmp (string, string_match) == 0) {
      if ( (temp = fscanf (ifp, "%s", string_ret) ) == 0 || temp == EOF )
        return 0;
      return 1;
    }

    else if ( (temp = fscanf (ifp, "%s", string) ) == 0 || temp == EOF )
      return 0;
  }
}

////////////////////////////////////////////////////////////////////////////////
// Used to make an arc in a poly line
//    the DXF 42 command (send here as the radius) is the Bulge factor
//       0   = STRAIGHT LINE
//       1   = Half Circle
//       -ve = rotation of clockwise
//       +ve = rotation anti-clockwise
//
//    polyline   h = (Bulge*distance)/2       where distance = from X1,Y1 to X2,Y2 Bulge is from DXF file
//                 = distance from tangent of circle to a line from X1,Y1 to X2,Y2
//
void __fastcall TFm_AceConverter::make_arc (struct entity_obj *ent)
{
  double Bulge = (*ent).radius;
  double Distance = sqrt (pow ((*ent).x2 - (*ent).x1, 2) + pow ((*ent).y2 - (*ent).y1, 2));
  double Height = (Bulge *Distance ) / 2;
  double ThirdSide = sqrt (pow (Height, 2) + pow (Distance / 2, 2));
  double Radius = (Distance *ThirdSide *ThirdSide ) / (4 * Height * (Distance / 2) );

  // save for final calc
  (*ent).radius = Radius;

  // convert to Polyline Arc, which uses X,Y & Radius
  (*ent).type = PARC;
}

void __fastcall TFm_AceConverter::get_first_point (double *x, double *y, double *z, struct entity_obj *entity)
{
  if (entity->type == LINE) {
    if (entity->dir == FOR) {
      *x = entity->x1;

      *y = entity->y1;
      *z = entity->z1;
    } else {
      *x = entity->x2;

      *y = entity->y2;
      *z = entity->z2;
    }
  }

  if (entity->type == ARC) {
    if (entity->dir == CW) {
      *x = entity->x1 + entity->radius * cos (entity->ang_end);

      *y = entity->y1 + entity->radius * sin (entity->ang_end);
      *z = entity->z1;
    } else {
      *x = entity->x1 + entity->radius * cos (entity->ang_start);

      *y = entity->y1 + entity->radius * sin (entity->ang_start);
      *z = entity->z1;
    }
  }

  if (entity->type == PARC) {
    if (entity->dir == CW) {
      *x = entity->x2;

      *y = entity->y2;
      *z = entity->z1;
    } else {
      *x = entity->x1;

      *y = entity->y1;
      *z = entity->z1;
    }
  }

  if (entity->type == POINT) {
    *x = entity->x1;

    *y = entity->y1;
    *z = entity->z1;
  }
}

void __fastcall TFm_AceConverter::get_second_point (double *x, double *y, double *z, struct entity_obj *entity)
{
  if (entity->type == LINE) {
    if (entity->dir == REV) {
      *x = entity->x1;

      *y = entity->y1;
      *z = entity->z1;
    } else {
      *x = entity->x2;

      *y = entity->y2;
      *z = entity->z2;
    }
  }

  if (entity->type == ARC) {
    if (entity->dir == CCW) {
      *x = entity->x1 + entity->radius * cos (entity->ang_end);

      *y = entity->y1 + entity->radius * sin (entity->ang_end);
      *z = entity->z1;
    } else {
      *x = entity->x1 + entity->radius * cos (entity->ang_start);

      *y = entity->y1 + entity->radius * sin (entity->ang_start);
      *z = entity->z1;
    }
  }

  if (entity->type == PARC) {
    if (entity->dir == CCW) {
      *x = entity->x2;

      *y = entity->y2;
      *z = entity->z1;
    } else {
      *x = entity->x1;

      *y = entity->y1;
      *z = entity->z1;
    }
  }

  if (entity->type == POINT) {
    *x = entity->x1;

    *y = entity->y1;
    *z = entity->z1;
  }
}

//---------------------------------------------------------------------------

//---------------------------------------------------------------------------
//---------------------------------------------------------------------------  