object Fm_ConvertionOptions: TFm_ConvertionOptions
  Left = 570
  Top = 325
  ActiveControl = Bn_OK
  BorderIcons = []
  BorderStyle = bsDialog
  Caption = 'Convertion Options'
  ClientHeight = 113
  ClientWidth = 279
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poOwnerFormCenter
  PixelsPerInch = 96
  TextHeight = 13
  object CK_GenerateIandJasRelativeCoordinates: TCheckBox
    Left = 8
    Top = 8
    Width = 265
    Height = 17
    Caption = 'Generate I and J as Relative Coordinates'
    Checked = True
    State = cbChecked
    TabOrder = 0
  end
  object CK_GenerateIandJbeforeOtherCoordinatesInBlock: TCheckBox
    Left = 8
    Top = 24
    Width = 265
    Height = 17
    Caption = 'Generate I and J Before Other Coordinates in Block'
    TabOrder = 1
  end
  object CK_GenerateBlockNumbers: TCheckBox
    Left = 8
    Top = 40
    Width = 265
    Height = 17
    Caption = 'Generate Block Numbers'
    TabOrder = 2
  end
  object CK_GenerateZonlyIfChanging: TCheckBox
    Left = 8
    Top = 56
    Width = 265
    Height = 17
    Caption = 'Generate Z only If Changing'
    TabOrder = 3
  end
  object Bn_OK: TBitBtn
    Left = 8
    Top = 80
    Width = 75
    Height = 25
    TabOrder = 4
    Kind = bkOK
  end
  object BN_Cancel: TBitBtn
    Left = 200
    Top = 80
    Width = 75
    Height = 25
    TabOrder = 5
    Kind = bkCancel
  end
end
