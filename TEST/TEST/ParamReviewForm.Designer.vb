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
        Me.LstStdParam.Size = New System.Drawing.Size(149, 274)
        Me.LstStdParam.TabIndex = 55
        '
        'LstTestParam
        '
        Me.LstTestParam.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LstTestParam.FormattingEnabled = True
        Me.LstTestParam.ItemHeight = 15
        Me.LstTestParam.Location = New System.Drawing.Point(461, 85)
        Me.LstTestParam.Name = "LstTestParam"
        Me.LstTestParam.Size = New System.Drawing.Size(149, 274)
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
        Me.Label4.Location = New System.Drawing.Point(13, 373)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 12)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "パラメータ値"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(102, 401)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 12)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "timeR1 -0.05～+0.25"
        '
        'txtTimeR1
        '
        Me.txtTimeR1.Location = New System.Drawing.Point(217, 398)
        Me.txtTimeR1.Name = "txtTimeR1"
        Me.txtTimeR1.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeR1.TabIndex = 61
        '
        'txtTimeR2
        '
        Me.txtTimeR2.Location = New System.Drawing.Point(217, 423)
        Me.txtTimeR2.Name = "txtTimeR2"
        Me.txtTimeR2.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeR2.TabIndex = 63
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(102, 426)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 12)
        Me.Label6.TabIndex = 62
        Me.Label6.Text = "timeR2 0.1～0.4"
        '
        'txtTimeP0
        '
        Me.txtTimeP0.Location = New System.Drawing.Point(444, 398)
        Me.txtTimeP0.Name = "txtTimeP0"
        Me.txtTimeP0.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeP0.TabIndex = 65
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(329, 401)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 12)
        Me.Label7.TabIndex = 64
        Me.Label7.Text = "timeP(0) 0.85～1"
        '
        'txtTimeP1
        '
        Me.txtTimeP1.Location = New System.Drawing.Point(444, 423)
        Me.txtTimeP1.Name = "txtTimeP1"
        Me.txtTimeP1.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeP1.TabIndex = 67
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(329, 426)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 12)
        Me.Label8.TabIndex = 66
        Me.Label8.Text = "timeP(1) 0.85～1"
        '
        'txtTimeP2
        '
        Me.txtTimeP2.Location = New System.Drawing.Point(444, 448)
        Me.txtTimeP2.Name = "txtTimeP2"
        Me.txtTimeP2.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeP2.TabIndex = 69
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(329, 451)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 12)
        Me.Label9.TabIndex = 68
        Me.Label9.Text = "timeP(2) 0.85～1"
        '
        'txtTimeP3
        '
        Me.txtTimeP3.Location = New System.Drawing.Point(444, 473)
        Me.txtTimeP3.Name = "txtTimeP3"
        Me.txtTimeP3.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeP3.TabIndex = 71
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(329, 476)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(90, 12)
        Me.Label10.TabIndex = 70
        Me.Label10.Text = "timeP(3) 0.85～1"
        '
        'txtTimeZone
        '
        Me.txtTimeZone.Location = New System.Drawing.Point(745, 398)
        Me.txtTimeZone.Name = "txtTimeZone"
        Me.txtTimeZone.Size = New System.Drawing.Size(100, 19)
        Me.txtTimeZone.TabIndex = 73
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(565, 401)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(174, 12)
        Me.Label11.TabIndex = 72
        Me.Label11.Text = "タイムゾーンに応じた係数 0.01～0.1"
        '
        'BtnAutoSet
        '
        Me.BtnAutoSet.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnAutoSet.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnAutoSet.Location = New System.Drawing.Point(15, 395)
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
        Me.BtnSave.Location = New System.Drawing.Point(15, 518)
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
        Me.BtnLoad.Location = New System.Drawing.Point(163, 518)
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
        Me.LstTestParam2.Size = New System.Drawing.Size(149, 274)
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
        Me.BtnStdSet.Location = New System.Drawing.Point(14, 432)
        Me.BtnStdSet.Name = "BtnStdSet"
        Me.BtnStdSet.Size = New System.Drawing.Size(72, 31)
        Me.BtnStdSet.TabIndex = 80
        Me.BtnStdSet.Text = "標準値"
        Me.BtnStdSet.UseVisualStyleBackColor = False
        '
        'txtParamRemarks
        '
        Me.txtParamRemarks.Location = New System.Drawing.Point(104, 370)
        Me.txtParamRemarks.Name = "txtParamRemarks"
        Me.txtParamRemarks.Size = New System.Drawing.Size(741, 19)
        Me.txtParamRemarks.TabIndex = 81
        '
        'BtnClsAll
        '
        Me.BtnClsAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnClsAll.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnClsAll.Location = New System.Drawing.Point(818, 6)
        Me.BtnClsAll.Name = "BtnClsAll"
        Me.BtnClsAll.Size = New System.Drawing.Size(54, 50)
        Me.BtnClsAll.TabIndex = 82
        Me.BtnClsAll.Text = "すべて消去"
        Me.BtnClsAll.UseVisualStyleBackColor = False
        '
        'ParamReviewForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(884, 561)
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
End Class
