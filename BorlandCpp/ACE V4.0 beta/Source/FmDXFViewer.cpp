//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "FmDXFViewer.h"
#include "FmAceConverter.h"


//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TFm_DXFViewer *Fm_DXFViewer;
//---------------------------------------------------------------------------
__fastcall TFm_DXFViewer::TFm_DXFViewer(TComponent* Owner) : TForm(Owner) {};


void __fastcall TFm_DXFViewer::FormShow(TObject *Sender)
{
   // setup DXF reading class
   MyCreationClass = new MyTest_CreationClass();
   MyDXF = new DL_Dxf();

   // setup
   LST_LayerNames->Items->Clear();
   Caption = "DXF Viewer: \"" + Fm_AceConverter->szDxfFile + "\"";

   // Read in the DXF
   if (!MyDXF->in(Fm_AceConverter->szDxfFile.c_str(), MyCreationClass)) { // if file open failed
      MessageDlg("Cannot open DXF File", mtError, TMsgDlgButtons() << mbOK, 0);
   }

   delete MyDXF;
   delete MyCreationClass;
}

/**
 * Default constructor.
 */
MyTest_CreationClass::MyTest_CreationClass()
{
   // my layer colours which are used when there is NO colour info in the DXF file
   MyLayerColours[ 0] = clBlack;
   MyLayerColours[ 1] = clMaroon;
   MyLayerColours[ 2] = clRed;
   MyLayerColours[ 3] = clRed | clYellow;
   MyLayerColours[ 4] = clYellow;
   MyLayerColours[ 5] = clGreen;
   MyLayerColours[ 6] = clBlue;
   MyLayerColours[ 7] = clPurple;
   MyLayerColours[ 8] = clDkGray;
   MyLayerColours[ 9] = clLime;
   MyLayerColours[10] = clOlive;
   MyLayerColours[11] = clSilver;
   MyLayerColours[12] = clMoneyGreen;
   MyLayerColours[13] = clNavy;
   MyLayerColours[14] = clCream;
   MyLayerColours[15] = clFuchsia;
   MyLayerColours[16] = clLtGray;
   MyLayerColours[17] = clTeal;
   MyLayerColours[18] = clSkyBlue;
   MyLayerColours[19] = clAqua;
}

// list the layers
void MyTest_CreationClass::addLayer(const DL_LayerData& data)
{
   AnsiString Name = data.name.c_str();

   Fm_DXFViewer->LST_LayerNames->Items->Add (Name);
}

// plot point
void MyTest_CreationClass::addPoint(const DL_PointData& data)
{
   TPointSeries *CHTP_PointSeries;
   CHTP_PointSeries              = new TPointSeries(Fm_DXFViewer->CHT_DXFViewer);
   CHTP_PointSeries->ParentChart =                  Fm_DXFViewer->CHT_DXFViewer;

   CHTP_PointSeries->SeriesColor = attributes.getColor();
   CHTP_PointSeries->ColorEachPoint = false;
   CHTP_PointSeries->Pointer->HorizSize  = 3;
   CHTP_PointSeries->Pointer->VertSize   = 3;
   CHTP_PointSeries->Pointer->Style  = psCircle;
   CHTP_PointSeries->AddXY(data.x, data.y, "");
}

// plot line
void MyTest_CreationClass::addLine(const DL_LineData& data)
{
   TFastLineSeries                   *CHTL_LineSeries;
   CHTL_LineSeries              = new TFastLineSeries(Fm_DXFViewer->CHT_DXFViewer);
   CHTL_LineSeries->ParentChart =                     Fm_DXFViewer->CHT_DXFViewer;
   CHTL_LineSeries->LinePen->Color = attributes.getColor();
   CHTL_LineSeries->LinePen->Width = 2;

   CHTL_LineSeries->AddXY(data.x1, data.y1);
   CHTL_LineSeries->AddXY(data.x2, data.y2);
}

// plot arc
void MyTest_CreationClass::addArc(const DL_ArcData& data)
{
   TChartShape                        *CHTS_ShapeSeries;
   CHTS_ShapeSeries              = new TChartShape(Fm_DXFViewer->CHT_DXFViewer);
   CHTS_ShapeSeries->ParentChart =                 Fm_DXFViewer->CHT_DXFViewer;

   CHTS_ShapeSeries->Style = chasCircle;
   CHTS_ShapeSeries->Pen->Color = attributes.getColor();
   CHTS_ShapeSeries->Pen->Width = 2;
   CHTS_ShapeSeries->Transparent = true;
   CHTS_ShapeSeries->X0 = data.cx - data.radius;
   CHTS_ShapeSeries->Y0 = data.cy + data.radius;
   CHTS_ShapeSeries->X1 = data.cx + data.radius;
   CHTS_ShapeSeries->Y1 = data.cy - data.radius;

//    printf("ARC      (%6.3f, %6.3f, %6.3f) %6.3f, %6.3f, %6.3f\n", data.cx, data.cy, data.cz, data.radius, data.angle1, data.angle2);
}

// plot circle
void MyTest_CreationClass::addCircle(const DL_CircleData& data)
{
   TChartShape                        *CHTS_ShapeSeries;
   CHTS_ShapeSeries              = new TChartShape(Fm_DXFViewer->CHT_DXFViewer);
   CHTS_ShapeSeries->ParentChart =                 Fm_DXFViewer->CHT_DXFViewer;

   CHTS_ShapeSeries->Style = chasCircle;
   CHTS_ShapeSeries->Pen->Color = attributes.getColor();
   CHTS_ShapeSeries->Pen->Width = 2;
   CHTS_ShapeSeries->Transparent = true;
   CHTS_ShapeSeries->X0 = data.cx - data.radius;
   CHTS_ShapeSeries->Y0 = data.cy + data.radius;
   CHTS_ShapeSeries->X1 = data.cx + data.radius;
   CHTS_ShapeSeries->Y1 = data.cy - data.radius;

//    printf("CIRCLE   (%6.3f, %6.3f, %6.3f) %6.3f\n", data.cx, data.cy, data.cz, data.radius);
}

void MyTest_CreationClass::addPolyline(const DL_PolylineData& data)
{
   // create
   CHTL_PolyLineSeries              = new TFastLineSeries(Fm_DXFViewer->CHT_DXFViewer);
   CHTL_PolyLineSeries->ParentChart =                     Fm_DXFViewer->CHT_DXFViewer;
   CHTL_PolyLineSeries->LinePen->Color = attributes.getColor();
   CHTL_PolyLineSeries->LinePen->Width = 2;
   LastBulge = 0;

//    printf("POLYLINE \n");
//    printf("flags: %d\n", (int)data.flags);
}


/**
 * Sample implementation of the method which handles vertices.
 */
void MyTest_CreationClass::addVertex(const DL_VertexData& data)
#define PI 3.141592654
{
   // for the Arc
   TChartShape *CHTS_ShapeSeries;
   double X1,Y1,X2,Y2;

   // populate
   if ( (data.bulge == 0) && (LastBulge == 0) ) {
      if ( CHTL_PolyLineSeries->XValues->Count() > 0 ) {
         // new line or last being repeated ?
         if ( (CHTL_PolyLineSeries->XValues->Last() != data.x) && (CHTL_PolyLineSeries->YValues->Last() != data.x) ) {
            LastX = data.x;
            LastY = data.y;
            CHTL_PolyLineSeries->AddXY(data.x, data.y);
         }
      } else {
         LastX = data.x;
         LastY = data.y;
         CHTL_PolyLineSeries->AddXY(data.x, data.y);
      }
   } else {
         // points
         X1 = LastX;
         Y1 = LastY;
         X2 = data.x;
         Y2 = data.y;

         // new point or first point again ?
         if ( (X1 == X2) && (Y1 == Y2) ) {
            // this is the Xstart, Ystart with the bulge
            LastBulge = data.bulge;
         } else {
            // draw full circle, an arc is to hard
            CHTS_ShapeSeries              = new TChartShape(Fm_DXFViewer->CHT_DXFViewer);
            CHTS_ShapeSeries->ParentChart =                 Fm_DXFViewer->CHT_DXFViewer;
            CHTS_ShapeSeries->Style = chasCircle;
            CHTS_ShapeSeries->Pen->Color = attributes.getColor();
            CHTS_ShapeSeries->Transparent = true;

            CHTS_ShapeSeries->X0 = X1;
            CHTS_ShapeSeries->Y0 = Y1;
            CHTS_ShapeSeries->X1 = X2;
            CHTS_ShapeSeries->Y1 = Y2;

            // make no height have some
            //    - perfect circle
            if ( Y1 == Y2 ) {
               CHTS_ShapeSeries->Y0 = Y2 + (abs(X2-X1)/PI);
               CHTS_ShapeSeries->Y1 = Y2 - (abs(X2-X1)/PI);
            } else {
               // small make bigger
               if ( abs(Y1-Y2) < 2 ) {
                  CHTS_ShapeSeries->Y0 -= 2;
                  CHTS_ShapeSeries->Y1 += 2;
               }
            }

            // get ready for next point
            LastBulge = 0;
         }
   }

//   printf("VERTEX   (%6.3f, %6.3f, %6.3f) %6.3f\n", data.x, data.y, data.z, data.bulge);
}


//---------------------------------------------------------------------------

