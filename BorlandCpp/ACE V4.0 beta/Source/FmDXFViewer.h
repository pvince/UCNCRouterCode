//---------------------------------------------------------------------------

#ifndef FmDXFViewerH
#define FmDXFViewerH
//---------------------------------------------------------------------------
#include <Classes.hpp>
#include <Controls.hpp>
#include <StdCtrls.hpp>
#include <Forms.hpp>
#include <Buttons.hpp>
#include <Chart.hpp>
#include <ExtCtrls.hpp>
#include <TeEngine.hpp>
#include <TeeProcs.hpp>
#include <TeeShape.hpp>

#include "dxflib\dl_dxf.h"
#include "dxflib\dl_creationadapter.h"
#include <Series.hpp>

//---------------------------------------------------------------------------
class MyTest_CreationClass : public DL_CreationAdapter {
public:
    MyTest_CreationClass();

    TFastLineSeries *CHTL_PolyLineSeries;
    TColor           MyLayerColours[20];
    double           LastBulge, LastX, LastY;

    virtual void addLayer(const DL_LayerData& data);
    virtual void addPoint(const DL_PointData& data);
    virtual void addLine(const DL_LineData& data);
    virtual void addArc(const DL_ArcData& data);
    virtual void addCircle(const DL_CircleData& data);
    virtual void addPolyline(const DL_PolylineData& data);
    virtual void addVertex(const DL_VertexData& data);
};


//---------------------------------------------------------------------------
class TFm_DXFViewer : public TForm
{
__published:	// IDE-managed Components
   TChart *CHT_DXFViewer;
   TBitBtn *BN_OK;
   TListBox *LST_LayerNames;
   TLabel *LB_LayerNames;
   void __fastcall FormShow(TObject *Sender);
private:	// User declarations

   MyTest_CreationClass* MyCreationClass;
   DL_Dxf*               MyDXF;

public:		// User declarations
   __fastcall TFm_DXFViewer(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern PACKAGE TFm_DXFViewer *Fm_DXFViewer;
//---------------------------------------------------------------------------
#endif
