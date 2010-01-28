object Fm_DXFViewer: TFm_DXFViewer
  Left = 499
  Top = 260
  BorderIcons = []
  BorderStyle = bsDialog
  Caption = 'DXF Viewer'
  ClientHeight = 554
  ClientWidth = 749
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poOwnerFormCenter
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object LB_LayerNames: TLabel
    Left = 616
    Top = 8
    Width = 62
    Height = 13
    Caption = 'Layer Names'
  end
  object CHT_DXFViewer: TChart
    Left = 8
    Top = 8
    Width = 600
    Height = 497
    BackWall.Brush.Color = clWhite
    BackWall.Brush.Style = bsClear
    MarginBottom = 1
    MarginRight = 5
    MarginTop = 3
    Title.Text.Strings = (
      'TChart')
    Title.Visible = False
    BottomAxis.ExactDateTime = False
    BottomAxis.Increment = 1
    BottomAxis.Title.Caption = 'X'
    LeftAxis.ExactDateTime = False
    LeftAxis.Increment = 1
    LeftAxis.Title.Angle = 0
    LeftAxis.Title.Caption = 'Y'
    Legend.Visible = False
    View3D = False
    Color = clWhite
    TabOrder = 0
  end
  object BN_OK: TBitBtn
    Left = 271
    Top = 520
    Width = 75
    Height = 25
    TabOrder = 1
    Kind = bkOK
  end
  object LST_LayerNames: TListBox
    Left = 616
    Top = 24
    Width = 121
    Height = 481
    ItemHeight = 13
    TabOrder = 2
  end
end
