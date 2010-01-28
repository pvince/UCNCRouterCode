object Fm_AboutBox: TFm_AboutBox
  Left = 706
  Top = 287
  BorderIcons = []
  BorderStyle = bsDialog
  Caption = 'About Box'
  ClientHeight = 290
  ClientWidth = 321
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poOwnerFormCenter
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object BN_OK: TBitBtn
    Left = 124
    Top = 256
    Width = 75
    Height = 25
    TabOrder = 0
    Kind = bkOK
  end
  object GP_About: TGroupBox
    Left = 8
    Top = 8
    Width = 305
    Height = 241
    TabOrder = 1
    object LB_Comments: TLabel
      Left = 8
      Top = 104
      Width = 68
      Height = 13
      Caption = 'LB_Comments'
    end
    object LB_ProductName: TLabel
      Left = 8
      Top = 24
      Width = 289
      Height = 29
      Alignment = taCenter
      AutoSize = False
      Caption = 'LB_ProductName'
      Font.Charset = ANSI_CHARSET
      Font.Color = clRed
      Font.Height = -24
      Font.Name = 'Arial'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object LB_Version: TLabel
      Left = 8
      Top = 148
      Width = 54
      Height = 13
      Caption = 'LB_Version'
    end
    object LB_Coprright: TLabel
      Left = 8
      Top = 192
      Width = 61
      Height = 13
      Caption = 'LB_Coprright'
    end
    object LB_Conditionals: TLabel
      Left = 8
      Top = 216
      Width = 76
      Height = 13
      Caption = 'LB_Conditionals'
    end
    object LB_Description: TLabel
      Left = 8
      Top = 61
      Width = 289
      Height = 19
      Alignment = taCenter
      AutoSize = False
      Caption = 'LB_Description'
      Font.Charset = ANSI_CHARSET
      Font.Color = clBlue
      Font.Height = -16
      Font.Name = 'Arial'
      Font.Style = [fsBold]
      ParentFont = False
    end
  end
end
