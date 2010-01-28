object Fm_AceConverter: TFm_AceConverter
  Left = 763
  Top = 288
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'Ace - Convert DXF files to GCode Files'
  ClientHeight = 282
  ClientWidth = 471
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  Menu = MM_MainMenu
  OldCreateOrder = False
  Position = poScreenCenter
  ShowHint = True
  OnCreate = FormCreate
  OnDestroy = FormDestroy
  PixelsPerInch = 96
  TextHeight = 13
  object LB_DXF_Filename: TLabel
    Left = 8
    Top = 8
    Width = 98
    Height = 13
    Caption = 'Please Open a File...'
  end
  object LB_Layers: TLabel
    Left = 8
    Top = 82
    Width = 116
    Height = 18
    Caption = 'Layer ... Priority :'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
  end
  object LB_Priority: TLabel
    Left = 248
    Top = 82
    Width = 57
    Height = 18
    Caption = 'Priority :'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
  end
  object LB_GCode_Filename: TLabel
    Left = 8
    Top = 24
    Width = 3
    Height = 13
  end
  object Bn_FileOpen: TBitBtn
    Left = 8
    Top = 48
    Width = 100
    Height = 25
    Caption = '&Open'
    ModalResult = 8
    TabOrder = 0
    OnClick = Bn_FileOpenClick
    Glyph.Data = {
      DE010000424DDE01000000000000760000002800000024000000120000000100
      04000000000068010000130B0000130B00001000000010000000000000000000
      80000080000000808000800000008000800080800000C0C0C000808080000000
      FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00666666666666
      6666666666666666666666660000666666666666666666666666666666666666
      00006666000000000000066666FFFFFFFFFFFFF6000066688888888888800666
      68888888888888F600006668FB7B7B7B7B80066668F6666666668FF60000668F
      B7B7B7B7B70806668F666666666688F60000668F7B7B7B7B780806668F666666
      6668F8F6000068F7B7B7B7B7B0880668F6666666666888F6000068FFFFFFFFFF
      80780668FFFFFFFFFF8F68F6000068888888888888B8066888888888888868F6
      0000668F78FFFFFFF07806668F68F666666868F60000668FB8FFFFFFF0F80666
      8F68F6666668F8F60000668F78FFFFFFF08866668F68F666FFF8886600006668
      F8FFFF000066666668F8F666888866660000666688FFFF7F866666666688F666
      6F8666660000666668FFFF78666666666668FFFF686666660000666668888886
      6666666666688888866666660000666666666666666666666666666666666666
      0000}
    NumGlyphs = 2
  end
  object Bn_Convert: TBitBtn
    Left = 128
    Top = 48
    Width = 100
    Height = 25
    Caption = '&Convert'
    Enabled = False
    TabOrder = 1
    OnClick = Bn_ConvertClick
    Glyph.Data = {
      DE010000424DDE01000000000000760000002800000024000000120000000100
      0400000000006801000000000000000000001000000000000000000000000000
      80000080000000808000800000008000800080800000C0C0C000808080000000
      FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00666666666666
      6666666666666F6666666666000066666636666666666666666686F666666666
      0000666666306666666666666666886F66666666000066666663066666666666
      66666886F6666666000066666663B066666666666666F8F86F66666600006666
      63000F06666666666668888686F666660000666663FBFBF06666666666686F66
      686F666600006666663FB0333666666666668F688886666600006666663BFB06
      6666666666FF8FF686F66666000066630000BFB06666666668888866686F6666
      00006663FBFBFBFB06666666686F6666668F6666000066663FBFB03336666666
      668F666888866666000066663BFBFB06666666666686F66686F6666600006666
      63BFBFB0666666666668F666686F66660000666663FBFBFB0666666666686F66
      6686F66600006666663FBFBFB066666666668FFFFFF8FF660000666666333333
      3336666666668888888886660000666666666666666666666666666666666666
      0000}
    NumGlyphs = 2
  end
  object Bn_Setup: TBitBtn
    Left = 365
    Top = 48
    Width = 100
    Height = 25
    Caption = '&Setup'
    ModalResult = 8
    TabOrder = 2
    OnClick = Bn_SetupClick
    Glyph.Data = {
      DE010000424DDE01000000000000760000002800000024000000120000000100
      0400000000006801000000000000000000001000000010000000000000000000
      80000080000000808000800000008000800080800000C0C0C000808080000000
      FF0000FF000000FFFF00FF000000FF00FF00FFFF0000FFFFFF00666666666666
      6666666666666666666666660000666666666666666666666666666666666666
      0000640066666400666666688866666888666666000064F0600064F060006668
      F8688868F8688866000064466666644666666668866666688666666600006666
      6666666666666666666666666666666600006666666666666666666666666666
      666666660000640066666400666666688866666888666666000064F0600064F0
      60006668F8688868F86888660000644666666446666666688666666886666666
      0000666666666666666666666666666666666666000066666666666666666666
      66666666666666660000640066666400666666688866666888666666000064F0
      600064F060006668F8688868F868886600006446666664466666666886666668
      8666666600006666666666666666666666666666666666660000666666666666
      6666666666666666666666660000666666666666666666666666666666666666
      0000}
    NumGlyphs = 2
  end
  object LST_Layers: TListBox
    Left = 8
    Top = 104
    Width = 217
    Height = 169
    Hint = 'Double Click on a Layer to adjust its Properties'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Arial'
    Font.Style = []
    ItemHeight = 18
    ParentFont = False
    TabOrder = 3
    OnDblClick = LST_LayersDblClick
  end
  object LST_Priority: TListBox
    Left = 248
    Top = 104
    Width = 217
    Height = 169
    Hint = 'Double Click on a Priority to adjust its Properties'
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'Arial'
    Font.Style = []
    ItemHeight = 18
    ParentFont = False
    TabOrder = 4
    OnDblClick = LST_PriorityDblClick
  end
  object BN_DXFView: TBitBtn
    Left = 248
    Top = 48
    Width = 100
    Height = 25
    Caption = '&DXF Viewer'
    Enabled = False
    TabOrder = 5
    OnClick = BN_DXFViewClick
    Kind = bkHelp
  end
  object dlgOpen_OpenDXF: TOpenDialog
    DefaultExt = '*.DXF'
    Filter = 'DXF Files (*.dxf)|*.dxf'
    Options = [ofOverwritePrompt, ofPathMustExist, ofEnableSizing]
    Title = 'Select the DXF File to Import'
    Left = 64
    Top = 128
  end
  object dlgSave_SaveCNC: TSaveDialog
    DefaultExt = '*.NC'
    Filter = 'NC Files (*.NC)|*.NC'
    Options = [ofOverwritePrompt, ofHideReadOnly, ofEnableSizing]
    Left = 64
    Top = 176
  end
  object MM_MainMenu: TMainMenu
    Left = 360
    Top = 120
    object Mn_File: TMenuItem
      Caption = 'File'
      OnClick = Mn_FileClick
      object Open1: TMenuItem
        Caption = '&Open'
        OnClick = Open1Click
      end
      object Mn_Convert: TMenuItem
        Caption = '&Convert'
        OnClick = Mn_ConvertClick
      end
      object N2: TMenuItem
        Caption = '-'
      end
      object Setup1: TMenuItem
        Caption = '&Setup'
        OnClick = Setup1Click
      end
      object N1: TMenuItem
        Caption = '-'
      end
      object Mn_OldFile1: TMenuItem
        Tag = 1
        Caption = 'File1'
        OnClick = Mn_OldFile1Click
      end
      object Mn_OldFile2: TMenuItem
        Tag = 2
        Caption = 'File2'
        OnClick = Mn_OldFile2Click
      end
      object Mn_OldFile3: TMenuItem
        Tag = 3
        Caption = 'File3'
        OnClick = Mn_OldFile3Click
      end
      object Mn_OldFile4: TMenuItem
        Tag = 4
        Caption = 'File4'
        OnClick = Mn_OldFile4Click
      end
      object Mn_OldFile5: TMenuItem
        Tag = 5
        Caption = 'File5'
        OnClick = Mn_OldFile5Click
      end
      object Mn_OldFile6: TMenuItem
        Tag = 6
        Caption = 'File6'
        OnClick = Mn_OldFile6Click
      end
      object Mn_OldFile7: TMenuItem
        Tag = 7
        Caption = 'File7'
        OnClick = Mn_OldFile7Click
      end
      object Mn_OldFile8: TMenuItem
        Tag = 8
        Caption = 'File8'
        OnClick = Mn_OldFile8Click
      end
      object Mn_OldFile9: TMenuItem
        Tag = 9
        Caption = 'File9'
        OnClick = Mn_OldFile9Click
      end
      object Mn_OldFile10: TMenuItem
        Tag = 10
        Caption = 'File10'
        OnClick = Mn_OldFile10Click
      end
      object N3: TMenuItem
        Caption = '-'
      end
      object Exit1: TMenuItem
        Caption = '&Exit'
        OnClick = Exit1Click
      end
    end
    object Properties1: TMenuItem
      Caption = 'Properties'
      OnClick = Properties1Click
      object Mn_LayerProperties: TMenuItem
        Caption = '&Layer Properties'
        OnClick = Mn_LayerPropertiesClick
      end
      object Mn_PriorityProperties: TMenuItem
        Caption = '&Priority Properties'
        OnClick = Mn_PriorityPropertiesClick
      end
    end
    object Help1: TMenuItem
      Caption = '&Help'
      object About1: TMenuItem
        Caption = '&Help'
        OnClick = About1Click
      end
      object Abount1: TMenuItem
        Caption = '&About'
        OnClick = Abount1Click
      end
    end
  end
end
