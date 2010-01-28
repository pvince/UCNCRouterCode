//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop
//---------------------------------------------------------------------------
USEFORM("FmAceConverter.cpp", Fm_AceConverter);
USEFORM("FmSetupOptions.cpp", Fm_SetupOptions);
USEFORM("FmLayerProperties.cpp", Fm_LayerProperties);
USEFORM("FmConvertOptions.cpp", Fm_ConvertionOptions);
USEFORM("FmPriorityProperties.cpp", Fm_PriorityProperties);
USEFORM("FmAboutBox.cpp", Fm_AboutBox);
USEFORM("FmDXFViewer.cpp", Fm_DXFViewer);
//---------------------------------------------------------------------------
WINAPI WinMain(HINSTANCE, HINSTANCE, LPSTR, int)
{
   try
   {
      Application->Initialize();
      Application->Title = "Ace Converter";
      Application->CreateForm(__classid(TFm_AceConverter), &Fm_AceConverter);
      Application->CreateForm(__classid(TFm_SetupOptions), &Fm_SetupOptions);
      Application->CreateForm(__classid(TFm_LayerProperties), &Fm_LayerProperties);
      Application->CreateForm(__classid(TFm_ConvertionOptions), &Fm_ConvertionOptions);
      Application->CreateForm(__classid(TFm_PriorityProperties), &Fm_PriorityProperties);
      Application->CreateForm(__classid(TFm_AboutBox), &Fm_AboutBox);
      Application->Run();
   }
   catch (Exception &exception)
   {
      Application->ShowException(&exception);
   }
   catch (...)
   {
      try
      {
         throw Exception("");
      }
      catch (Exception &exception)
      {
         Application->ShowException(&exception);
      }
   }
   return 0;
}
//---------------------------------------------------------------------------
