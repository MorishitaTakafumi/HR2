<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParamReviewForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ParamReviewForm))
        Me.BtnFile = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.LstRaceHeader = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnGoStd = New System.Windows.Forms.Button()
        Me.LstStdParam = New System.Windows.Forms.ListBox()
        Me.LstTestParam = New System.Windows.Forms.ListBox()
        Me.BtnGoTestParam = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTimeR1 = New System.Windows.Forms.TextBox()
        Me.txtTimeR2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtTimeP0 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTimeP1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTimeP2 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTimeP3 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTimeZone = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.BtnAutoSet = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnLoad = New System.Windows.Forms.Button()
        Me.LstTestParam2 = New System.Windows.Forms.ListBox()
        Me.BtnGoTestParam2 = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.BtnStdSet = New System.Windows.Forms.Button()
        Me.txtParamRemarks = New System.Windows.Forms.TextBox()
        Me.BtnClsAll = New System.Windows.Forms.Button()
        Me.txtScoreP0 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtScoreP1 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtScoreP2 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtScoreP3 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtScoreP23 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtScoreP22 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtScoreP21 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtScoreP20 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnFile
        '
        Me.BtnFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnFile.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnFile.Location = New System.Drawing.Point(550, 6)
        Me.BtnFile.Name = "BtnFile"
        Me.BtnFile.Size = New System.Drawing.Size(72, 31)
        Me.BtnFile.TabIndex = 51
        Me.BtnFile.Text = "参照"
        Me.BtnFile.UseVisualStyleBackColor = False
        '
        'txtFile
        '
        Me.txtFile.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFile.Location = New System.Drawing.Point(93, 12)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(451, 20)
        Me.txtFile.TabIndex = 50
        '
        'LstRaceHeader
        '
        Me.LstRaceHeader.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LstRaceHeader.FormattingEnabled = True
        Me.LstRaceHeader.ItemHeight = 15
        Me.LstRaceHeader.Location = New System.Drawing.Point(14, 48)
        Me.LstRaceHeader.Name = "LstRaceHeader"
        Me.LstRaceHeader.Size = New System.Drawing.Size(263, 199)
        Me.LstRaceHeader.TabIndex = 49
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 12)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "出馬表ファイル"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(292, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 12)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "標準パラメータ"
        '
        'BtnGoStd
        '
        Me.BtnGoStd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnGoStd.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnGoStd.Location = New System.Drawing.Point(371, 48)
        Me.BtnGoStd.Name = "BtnGoStd"
        Me.BtnGoStd.Size = New System.Drawing.Size(72, 31)
        Me.BtnGoStd.TabIndex = 54
        Me.BtnGoStd.Text = "Go"
        Me.BtnGoStd.UseVisualStyleBackColor = False
        '
        'LstStdParam
        '
        Me.LstStdParam.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LstStdParam.FormattingEnabled = True
        Me.LstStdParam.ItemHeight = 15
        Me.LstStdParam.Location = New System.Drawing.Point(294, 85)
        Me.LstStdParam.Name = "LstStdParam"
        Me.LstStdParam.Size = New System.Drawing.Size(149, 409)
        Me.LstStdParam.TabIndex = 55
        '
        'LstTestParam
        '
        Me.LstTestParam.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LstTestParam.FormattingEnabled = True
        Me.LstTestParam.ItemHeight = 15
        Me.LstTestParam.Location = New System.Drawing.Point(461, 85)
        Me.LstTestParam.Name = "LstTestParam"
        Me.LstTestParam.Size = New System.Drawing.Size(149, 409)
        Me.LstTestParam.TabIndex = 58
        '
        'BtnGoTestParam
        '
        Me.BtnGoTestParam.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnGoTestParam.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnGoTestParam.Location = New System.Drawing.Point(538, 48)
        Me.BtnGoTestParam.Name = "BtnGoTestParam"
        Me.BtnGoTestParam.Size = New System.Drawing.Size(72, 31)
        Me.BtnGoTestParam.TabIndex = 57
        Me.BtnGoTestParam.Text = "Go"
        Me.BtnGoTestParam.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(453, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 12)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "試験パラメータ1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(594, 519)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 12)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "パラメータ名前"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(818, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 12)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "timeR1 -0.05～+0.25"
        '
        'txtTimeR1
        '
        Me.txtTimeR1.Location = New System.Drawing.Point(998, 93)
        Me.txtTimeR1.Name = "txtTimeR1"
        Me.txtTimeR1.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeR1.TabIndex = 61
        '
        'txtTimeR2
        '
        Me.txtTimeR2.Location = New System.Drawing.Point(998, 118)
        Me.txtTimeR2.Name = "txtTimeR2"
        Me.txtTimeR2.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeR2.TabIndex = 63
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(818, 121)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 12)
        Me.Label6.TabIndex = 62
        Me.Label6.Text = "timeR2 0.1～0.4"
        '
        'txtTimeP0
        '
        Me.txtTimeP0.Location = New System.Drawing.Point(998, 143)
        Me.txtTimeP0.Name = "txtTimeP0"
        Me.txtTimeP0.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeP0.TabIndex = 65
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(818, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(128, 12)
        Me.Label7.TabIndex = 64
        Me.Label7.Text = "1走前　timeP(0) 0.85～1"
        '
        'txtTimeP1
        '
        Me.txtTimeP1.Location = New System.Drawing.Point(998, 168)
        Me.txtTimeP1.Name = "txtTimeP1"
        Me.txtTimeP1.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeP1.TabIndex = 67
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(818, 171)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(128, 12)
        Me.Label8.TabIndex = 66
        Me.Label8.Text = "2走前　timeP(1) 0.85～1"
        '
        'txtTimeP2
        '
        Me.txtTimeP2.Location = New System.Drawing.Point(998, 193)
        Me.txtTimeP2.Name = "txtTimeP2"
        Me.txtTimeP2.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeP2.TabIndex = 69
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(818, 196)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(128, 12)
        Me.Label9.TabIndex = 68
        Me.Label9.Text = "3走前　timeP(2) 0.85～1"
        '
        'txtTimeP3
        '
        Me.txtTimeP3.Location = New System.Drawing.Point(998, 218)
        Me.txtTimeP3.Name = "txtTimeP3"
        Me.txtTimeP3.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeP3.TabIndex = 71
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(818, 221)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(128, 12)
        Me.Label10.TabIndex = 70
        Me.Label10.Text = "4走前　timeP(3) 0.85～1"
        '
        'txtTimeZone
        '
        Me.txtTimeZone.Location = New System.Drawing.Point(998, 243)
        Me.txtTimeZone.Name = "txtTimeZone"
        Me.txtTimeZone.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeZone.TabIndex = 73
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(818, 246)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(174, 12)
        Me.Label11.TabIndex = 72
        Me.Label11.Text = "タイムゾーンに応じた係数 0.01～0.1"
        '
        'BtnAutoSet
        '
        Me.BtnAutoSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnAutoSet.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAutoSet.Location = New System.Drawing.Point(929, 45)
        Me.BtnAutoSet.Name = "BtnAutoSet"
        Me.BtnAutoSet.Size = New System.Drawing.Size(72, 31)
        Me.BtnAutoSet.TabIndex = 74
        Me.BtnAutoSet.Text = "自動発生"
        Me.BtnAutoSet.UseVisualStyleBackColor = False
        '
        'BtnSave
        '
        Me.BtnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnSave.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(805, 553)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(142, 31)
        Me.BtnSave.TabIndex = 75
        Me.BtnSave.Text = "このパラメータを登録"
        Me.BtnSave.UseVisualStyleBackColor = False
        '
        'BtnLoad
        '
        Me.BtnLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnLoad.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnLoad.Location = New System.Drawing.Point(953, 553)
        Me.BtnLoad.Name = "BtnLoad"
        Me.BtnLoad.Size = New System.Drawing.Size(191, 31)
        Me.BtnLoad.TabIndex = 76
        Me.BtnLoad.Text = "パラメータをファイルから読み込む"
        Me.BtnLoad.UseVisualStyleBackColor = False
        '
        'LstTestParam2
        '
        Me.LstTestParam2.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LstTestParam2.FormattingEnabled = True
        Me.LstTestParam2.ItemHeight = 15
        Me.LstTestParam2.Location = New System.Drawing.Point(628, 85)
        Me.LstTestParam2.Name = "LstTestParam2"
        Me.LstTestParam2.Size = New System.Drawing.Size(149, 409)
        Me.LstTestParam2.TabIndex = 79
        '
        'BtnGoTestParam2
        '
        Me.BtnGoTestParam2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnGoTestParam2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnGoTestParam2.Location = New System.Drawing.Point(705, 48)
        Me.BtnGoTestParam2.Name = "BtnGoTestParam2"
        Me.BtnGoTestParam2.Size = New System.Drawing.Size(72, 31)
        Me.BtnGoTestParam2.TabIndex = 78
        Me.BtnGoTestParam2.Text = "Go"
        Me.BtnGoTestParam2.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(620, 58)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 12)
        Me.Label12.TabIndex = 77
        Me.Label12.Text = "試験パラメータ2"
        '
        'BtnStdSet
        '
        Me.BtnStdSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnStdSet.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnStdSet.Location = New System.Drawing.Point(1007, 45)
        Me.BtnStdSet.Name = "BtnStdSet"
        Me.BtnStdSet.Size = New System.Drawing.Size(72, 31)
        Me.BtnStdSet.TabIndex = 80
        Me.BtnStdSet.Text = "標準値"
        Me.BtnStdSet.UseVisualStyleBackColor = False
        '
        'txtParamRemarks
        '
        Me.txtParamRemarks.Location = New System.Drawing.Point(673, 516)
        Me.txtParamRemarks.Name = "txtParamRemarks"
        Me.txtParamRemarks.Size = New System.Drawing.Size(471, 19)
        Me.txtParamRemarks.TabIndex = 81
        '
        'BtnClsAll
        '
        Me.BtnClsAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnClsAll.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnClsAll.Location = New System.Drawing.Point(1122, 21)
        Me.BtnClsAll.Name = "BtnClsAll"
        Me.BtnClsAll.Size = New System.Drawing.Size(54, 50)
        Me.BtnClsAll.TabIndex = 82
        Me.BtnClsAll.Text = "すべて消去"
        Me.BtnClsAll.UseVisualStyleBackColor = False
        '
        'txtScoreP0
        '
        Me.txtScoreP0.Location = New System.Drawing.Point(998, 268)
        Me.txtScoreP0.Name = "txtScoreP0"
        Me.txtScoreP0.Size = New System.Drawing.Size(100, 19)
        Me.txtScoreP0.TabIndex = 84
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(818, 271)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(158, 12)
        Me.Label13.TabIndex = 83
        Me.Label13.Text = "4着以下scoreP(0) -0.05～0.05"
        '
        'txtScoreP1
        '
        Me.txtScoreP1.Location = New System.Drawing.Point(998, 293)
        Me.txtScoreP1.Name = "txtScoreP1"
        Me.txtScoreP1.Size = New System.Drawing.Size(100, 19)
        Me.txtScoreP1.TabIndex = 86
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(818, 296)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(122, 12)
        Me.Label14.TabIndex = 85
        Me.Label14.Text = "3着　scoreP(1) 0～0.05"
        '
        'txtScoreP2
        '
        Me.txtScoreP2.Location = New System.Drawing.Point(998, 318)
        Me.txtScoreP2.Name = "txtScoreP2"
        Me.txtScoreP2.Size = New System.Drawing.Size(100, 19)
        Me.txtScoreP2.TabIndex = 88
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(818, 321)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(122, 12)
        Me.Label15.TabIndex = 87
        Me.Label15.Text = "2着　scoreP(2) 0～0.05"
        '
        'txtScoreP3
        '
        Me.txtScoreP3.Location = New System.Drawing.Point(998, 343)
        Me.txtScoreP3.Name = "txtScoreP3"
        Me.txtScoreP3.Size = New System.Drawing.Size(100, 19)
        Me.txtScoreP3.TabIndex = 90
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(818, 346)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(122, 12)
        Me.Label16.TabIndex = 89
        Me.Label16.Text = "1着　scoreP(3) 0～0.05"
        '
        'txtScoreP23
        '
        Me.txtScoreP23.Location = New System.Drawing.Point(998, 443)
        Me.txtScoreP23.Name = "txtScoreP23"
        Me.txtScoreP23.Size = New System.Drawing.Size(100, 19)
        Me.txtScoreP23.TabIndex = 98
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(818, 446)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(128, 12)
        Me.Label17.TabIndex = 97
        Me.Label17.Text = "1着　scoreP2(3) 0～0.05"
        '
        'txtScoreP22
        '
        Me.txtScoreP22.Location = New System.Drawing.Point(998, 418)
        Me.txtScoreP22.Name = "txtScoreP22"
        Me.txtScoreP22.Size = New System.Drawing.Size(100, 19)
        Me.txtScoreP22.TabIndex = 96
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(818, 421)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(128, 12)
        Me.Label18.TabIndex = 95
        Me.Label18.Text = "2着　scoreP2(2) 0～0.05"
        '
        'txtScoreP21
        '
        Me.txtScoreP21.Location = New System.Drawing.Point(998, 393)
        Me.txtScoreP21.Name = "txtScoreP21"
        Me.txtScoreP21.Size = New System.Drawing.Size(100, 19)
        Me.txtScoreP21.TabIndex = 94
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(818, 396)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(128, 12)
        Me.Label19.TabIndex = 93
        Me.Label19.Text = "3着　scoreP2(1) 0～0.05"
        '
        'txtScoreP20
        '
        Me.txtScoreP20.Location = New System.Drawing.Point(998, 368)
        Me.txtScoreP20.Name = "txtScoreP20"
        Me.txtScoreP20.Size = New System.Drawing.Size(100, 19)
        Me.txtScoreP20.TabIndex = 92
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(818, 371)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(164, 12)
        Me.Label20.TabIndex = 91
        Me.Label20.Text = "4着以下scoreP2(0) -0.00～0.05"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(862, 54)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(61, 12)
        Me.Label21.TabIndex = 99
        Me.Label21.Text = "パラメータ値"
        '
        'ParamReviewForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1178, 596)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtScoreP23)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtScoreP22)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtScoreP21)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtScoreP20)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtScoreP3)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtScoreP2)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtScoreP1)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtScoreP0)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.BtnClsAll)
        Me.Controls.Add(Me.txtParamRemarks)
        Me.Controls.Add(Me.BtnStdSet)
        Me.Controls.Add(Me.LstTestParam2)
        Me.Controls.Add(Me.BtnGoTestParam2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.BtnLoad)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.BtnAutoSet)
        Me.Controls.Add(Me.txtTimeZone)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtTimeP3)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtTimeP2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtTimeP1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTimeP0)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTimeR2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTimeR1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LstTestParam)
        Me.Controls.Add(Me.BtnGoTestParam)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LstStdParam)
        Me.Controls.Add(Me.BtnGoStd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnFile)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.LstRaceHeader)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "ParamReviewForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "パラメータの検討"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnFile As Button
    Friend WithEvents txtFile As TextBox
    Friend WithEvents LstRaceHeader As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnGoStd As Button
    Friend WithEvents LstStdParam As ListBox
    Friend WithEvents LstTestParam As ListBox
    Friend WithEvents BtnGoTestParam As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtTimeR1 As TextBox
    Friend WithEvents txtTimeR2 As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtTimeP0 As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtTimeP1 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtTimeP2 As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtTimeP3 As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtTimeZone As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents BtnAutoSet As Button
    Friend WithEvents BtnSave As Button
    Friend WithEvents BtnLoad As Button
    Friend WithEvents LstTestParam2 As ListBox
    Friend WithEvents BtnGoTestParam2 As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents BtnStdSet As Button
    Friend WithEvents txtParamRemarks As TextBox
    Friend WithEvents BtnClsAll As Button
    Friend WithEvents txtScoreP0 As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtScoreP1 As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtScoreP2 As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtScoreP3 As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtScoreP23 As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtScoreP22 As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtScoreP21 As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents txtScoreP20 As TextBox
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
End Class
