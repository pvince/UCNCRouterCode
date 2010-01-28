object Fm_SetupOptions: TFm_SetupOptions
  Left = 803
  Top = 270
  BorderIcons = []
  BorderStyle = bsDialog
  Caption = 'Default Setup Options and Values'
  ClientHeight = 241
  ClientWidth = 441
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poOwnerFormCenter
  DesignSize = (
    441
    241)
  PixelsPerInch = 96
  TextHeight = 13
  object LB_DimensionPrecision: TLabel
    Left = 8
    Top = 12
    Width = 98
    Height = 13
    Caption = 'Dimension Precision:'
  end
  object LB_DimensionZOffset: TLabel
    Left = 8
    Top = 36
    Width = 93
    Height = 13
    Caption = 'Dimension Z Offset:'
  end
  object LB_DefaultMaxZPass: TLabel
    Left = 8
    Top = 60
    Width = 98
    Height = 13
    Caption = 'Default Max Z/Pass:'
  end
  object LB_DefaultReleasePlane: TLabel
    Left = 8
    Top = 84
    Width = 112
    Height = 13
    Caption = 'Z Height between Cuts:'
  end
  object LB_DefaultCloseEnough: TLabel
    Left = 8
    Top = 108
    Width = 106
    Height = 13
    Caption = 'Default Close Enough:'
  end
  object LB_DefRelP_info: TLabel
    Left = 272
    Top = 85
    Width = 109
    Height = 13
    Caption = 'Absolute Z pos (not rel)'
  end
  object LB_MaxZP_info: TLabel
    Left = 272
    Top = 61
    Width = 75
    Height = 13
    Caption = 'Relative per cut'
  end
  object LB_SpindleHelp: TLabel
    Left = 272
    Top = 187
    Width = 159
    Height = 13
    Caption = 'Add M4/M5 to Start/Stop Spindle'
  end
  object LB_StartX: TLabel
    Left = 270
    Top = 140
    Width = 7
    Height = 13
    Caption = 'X'
  end
  object LB_StartY: TLabel
    Left = 326
    Top = 140
    Width = 7
    Height = 13
    Caption = 'Y'
  end
  object LB_StartZ: TLabel
    Left = 382
    Top = 140
    Width = 7
    Height = 13
    Caption = 'Z'
  end
  object LB_EndX: TLabel
    Left = 270
    Top = 164
    Width = 7
    Height = 13
    Caption = 'X'
  end
  object LB_EndY: TLabel
    Left = 326
    Top = 164
    Width = 7
    Height = 13
    Caption = 'Y'
  end
  object LB_EndZ: TLabel
    Left = 382
    Top = 164
    Width = 7
    Height = 13
    Caption = 'Z'
  end
  object ED_DimensionPrecision: TEdit
    Left = 128
    Top = 8
    Width = 137
    Height = 21
    TabOrder = 0
    Text = '4'
    OnKeyPress = ED_DimensionPrecisionKeyPress
  end
  object ED_DefaultZOffset: TEdit
    Left = 128
    Top = 32
    Width = 137
    Height = 21
    TabOrder = 1
    Text = '-4.000000'
    OnKeyPress = ED_DimensionPrecisionKeyPress
  end
  object ED_DefaultMaxZPass: TEdit
    Left = 128
    Top = 56
    Width = 137
    Height = 21
    TabOrder = 2
    Text = '1.000000'
    OnKeyPress = ED_DimensionPrecisionKeyPress
  end
  object ED_DefaultReleasePlane: TEdit
    Left = 128
    Top = 80
    Width = 137
    Height = 21
    TabOrder = 3
    Text = '2.000000'
    OnKeyPress = ED_DimensionPrecisionKeyPress
  end
  object ED_DefaultCloseEnough: TEdit
    Left = 128
    Top = 104
    Width = 137
    Height = 21
    TabOrder = 4
    Text = '0.001000'
    OnKeyPress = ED_DimensionPrecisionKeyPress
  end
  object Bn_OK: TBitBtn
    Left = 8
    Top = 208
    Width = 75
    Height = 25
    Anchors = [akTop]
    TabOrder = 5
    Kind = bkOK
  end
  object Bn_Cancel: TBitBtn
    Left = 360
    Top = 208
    Width = 75
    Height = 25
    Anchors = [akTop]
    TabOrder = 6
    Kind = bkCancel
  end
  object CK_AddGotoStartCommand_ToFileStart: TCheckBox
    Left = 8
    Top = 138
    Width = 257
    Height = 17
    Hint = 
      'Ace was originally written to Expect Radius information in a Pol' +
      'y line Arc.'#13#10'   -> It is noticed that Protel stores percentage i' +
      'nfo, were 1= full circle'
    Alignment = taLeftJustify
    Caption = 'Insert a Goto Start Command at begining of file'
    TabOrder = 7
  end
  object CK_AddGotoEndCommand_ToFileEnd: TCheckBox
    Left = 8
    Top = 162
    Width = 257
    Height = 17
    Hint = 
      'Ace was originally written to Expect Radius information in a Pol' +
      'y line Arc.'#13#10'   -> It is noticed that Protel stores percentage i' +
      'nfo, were 1= full circle'
    Alignment = taLeftJustify
    Caption = 'Insert a Goto End Command at end of file'
    TabOrder = 8
  end
  object CK_StartAndStopSpindleCommands: TCheckBox
    Left = 8
    Top = 186
    Width = 257
    Height = 17
    Hint = 
      'Ace was originally written to Expect Radius information in a Pol' +
      'y line Arc.'#13#10'   -> It is noticed that Protel stores percentage i' +
      'nfo, were 1= full circle'
    Alignment = taLeftJustify
    Caption = 'Insert Spindle Control Commands'
    TabOrder = 9
  end
  object ED_StartX: TEdit
    Left = 280
    Top = 136
    Width = 41
    Height = 21
    TabOrder = 10
    Text = '0'
    OnKeyPress = ED_EndXKeyPress
  end
  object ED_StartY: TEdit
    Left = 336
    Top = 136
    Width = 41
    Height = 21
    TabOrder = 11
    Text = '0'
    OnKeyPress = ED_EndXKeyPress
  end
  object ED_StartZ: TEdit
    Left = 392
    Top = 136
    Width = 41
    Height = 21
    TabOrder = 12
    Text = '0'
    OnKeyPress = ED_EndXKeyPress
  end
  object ED_EndX: TEdit
    Left = 280
    Top = 160
    Width = 41
    Height = 21
    TabOrder = 13
    Text = '0'
    OnKeyPress = ED_EndXKeyPress
  end
  object ED_EndY: TEdit
    Left = 336
    Top = 160
    Width = 41
    Height = 21
    TabOrder = 14
    Text = '0'
    OnKeyPress = ED_EndXKeyPress
  end
  object ED_EndZ: TEdit
    Left = 392
    Top = 160
    Width = 41
    Height = 21
    TabOrder = 15
    Text = '0'
    OnKeyPress = ED_EndXKeyPress
  end
end
