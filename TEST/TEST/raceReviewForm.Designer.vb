<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class raceReviewForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(raceReviewForm))
        Me.flx = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.BtnRedisp = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BtnHistGet = New System.Windows.Forms.Button()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.CbCyakujun = New System.Windows.Forms.ComboBox()
        Me.chkDosu = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CbJo = New System.Windows.Forms.ComboBox()
        Me.CbSyubetu = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CbKyori = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CbGrade = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CbMonth = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CbRacename = New System.Windows.Forms.ComboBox()
        Me.BtnJokenCls = New System.Windows.Forms.Button()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.RbInai = New System.Windows.Forms.RadioButton()
        Me.RbIka = New System.Windows.Forms.RadioButton()
        Me.BtnWinRate = New System.Windows.Forms.Button()
        Me.BtnFilterClear = New System.Windows.Forms.Button()
        Me.BtnCoefReview = New System.Windows.Forms.Button()
        Me.chkRmv1999 = New System.Windows.Forms.CheckBox()
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'flx
        '
        Me.flx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flx.ColumnInfo = resources.GetString("flx.ColumnInfo")
        Me.flx.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.flx.Location = New System.Drawing.Point(275, 55)
        Me.flx.Name = "flx"
        Me.flx.Rows.DefaultSize = 18
        Me.flx.Size = New System.Drawing.Size(1125, 564)
        Me.flx.StyleInfo = resources.GetString("flx.StyleInfo")
        Me.flx.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(330, 21)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "上差強調表示基準値："
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.DecimalPlaces = 1
        Me.NumericUpDown1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown1.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.NumericUpDown1.Location = New System.Drawing.Point(465, 15)
        Me.NumericUpDown1.Margin = New System.Windows.Forms.Padding(2)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(65, 26)
        Me.NumericUpDown1.TabIndex = 10
        Me.NumericUpDown1.Value = New Decimal(New Integer() {3, 0, 0, 65536})
        '
        'BtnRedisp
        '
        Me.BtnRedisp.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRedisp.Location = New System.Drawing.Point(770, 5)
        Me.BtnRedisp.Name = "BtnRedisp"
        Me.BtnRedisp.Size = New System.Drawing.Size(82, 41)
        Me.BtnRedisp.TabIndex = 11
        Me.BtnRedisp.Text = "再表示"
        Me.BtnRedisp.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(187, 12)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "***過去レース解析値の検索条件***"
        '
        'BtnHistGet
        '
        Me.BtnHistGet.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnHistGet.Image = CType(resources.GetObject("BtnHistGet.Image"), System.Drawing.Image)
        Me.BtnHistGet.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnHistGet.Location = New System.Drawing.Point(169, 83)
        Me.BtnHistGet.Name = "BtnHistGet"
        Me.BtnHistGet.Size = New System.Drawing.Size(93, 50)
        Me.BtnHistGet.TabIndex = 7
        Me.BtnHistGet.Text = "検索" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "実行"
        Me.BtnHistGet.TextAlign = System.Drawing.ContentAlignment.BottomRight
        Me.BtnHistGet.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 12
        Me.ListBox2.Location = New System.Drawing.Point(15, 324)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(254, 280)
        Me.ListBox2.TabIndex = 9
        '
        'CbCyakujun
        '
        Me.CbCyakujun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbCyakujun.FormattingEnabled = True
        Me.CbCyakujun.Location = New System.Drawing.Point(63, 183)
        Me.CbCyakujun.Margin = New System.Windows.Forms.Padding(2)
        Me.CbCyakujun.Name = "CbCyakujun"
        Me.CbCyakujun.Size = New System.Drawing.Size(92, 20)
        Me.CbCyakujun.TabIndex = 4
        '
        'chkDosu
        '
        Me.chkDosu.AutoSize = True
        Me.chkDosu.Checked = True
        Me.chkDosu.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDosu.Location = New System.Drawing.Point(15, 290)
        Me.chkDosu.Name = "chkDosu"
        Me.chkDosu.Size = New System.Drawing.Size(187, 16)
        Me.chkDosu.TabIndex = 8
        Me.chkDosu.Text = "spanScoreの度数分布を表示する"
        Me.chkDosu.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 46)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "競馬場"
        '
        'CbJo
        '
        Me.CbJo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbJo.FormattingEnabled = True
        Me.CbJo.Items.AddRange(New Object() {"1着のみ", "1,2着", "1,2,3着"})
        Me.CbJo.Location = New System.Drawing.Point(63, 43)
        Me.CbJo.Margin = New System.Windows.Forms.Padding(2)
        Me.CbJo.Name = "CbJo"
        Me.CbJo.Size = New System.Drawing.Size(92, 20)
        Me.CbJo.TabIndex = 0
        '
        'CbSyubetu
        '
        Me.CbSyubetu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbSyubetu.FormattingEnabled = True
        Me.CbSyubetu.Items.AddRange(New Object() {"1着のみ", "1,2着", "1,2,3着"})
        Me.CbSyubetu.Location = New System.Drawing.Point(63, 78)
        Me.CbSyubetu.Margin = New System.Windows.Forms.Padding(2)
        Me.CbSyubetu.Name = "CbSyubetu"
        Me.CbSyubetu.Size = New System.Drawing.Size(92, 20)
        Me.CbSyubetu.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 81)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "種別"
        '
        'CbKyori
        '
        Me.CbKyori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbKyori.FormattingEnabled = True
        Me.CbKyori.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.CbKyori.Items.AddRange(New Object() {"1着のみ", "1,2着", "1,2,3着"})
        Me.CbKyori.Location = New System.Drawing.Point(63, 113)
        Me.CbKyori.Margin = New System.Windows.Forms.Padding(2)
        Me.CbKyori.Name = "CbKyori"
        Me.CbKyori.Size = New System.Drawing.Size(92, 20)
        Me.CbKyori.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 116)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "距離"
        '
        'CbGrade
        '
        Me.CbGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbGrade.FormattingEnabled = True
        Me.CbGrade.Items.AddRange(New Object() {"1着のみ", "1,2着", "1,2,3着"})
        Me.CbGrade.Location = New System.Drawing.Point(63, 149)
        Me.CbGrade.Margin = New System.Windows.Forms.Padding(2)
        Me.CbGrade.Name = "CbGrade"
        Me.CbGrade.Size = New System.Drawing.Size(92, 20)
        Me.CbGrade.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 152)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 12)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "グレード"
        '
        'CbMonth
        '
        Me.CbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbMonth.FormattingEnabled = True
        Me.CbMonth.Items.AddRange(New Object() {"1着のみ", "1,2着", "1,2,3着"})
        Me.CbMonth.Location = New System.Drawing.Point(63, 218)
        Me.CbMonth.Margin = New System.Windows.Forms.Padding(2)
        Me.CbMonth.Name = "CbMonth"
        Me.CbMonth.Size = New System.Drawing.Size(92, 20)
        Me.CbMonth.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 186)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "着順"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 221)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 12)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "開催月"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 256)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 12)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "レース名"
        '
        'CbRacename
        '
        Me.CbRacename.FormattingEnabled = True
        Me.CbRacename.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.CbRacename.Items.AddRange(New Object() {"1着のみ", "1,2着", "1,2,3着"})
        Me.CbRacename.Location = New System.Drawing.Point(63, 253)
        Me.CbRacename.Margin = New System.Windows.Forms.Padding(2)
        Me.CbRacename.Name = "CbRacename"
        Me.CbRacename.Size = New System.Drawing.Size(197, 20)
        Me.CbRacename.TabIndex = 6
        '
        'BtnJokenCls
        '
        Me.BtnJokenCls.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnJokenCls.Location = New System.Drawing.Point(169, 40)
        Me.BtnJokenCls.Name = "BtnJokenCls"
        Me.BtnJokenCls.Size = New System.Drawing.Size(47, 37)
        Me.BtnJokenCls.TabIndex = 34
        Me.BtnJokenCls.Text = "条件クリア"
        Me.BtnJokenCls.UseVisualStyleBackColor = True
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.DecimalPlaces = 1
        Me.NumericUpDown2.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown2.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.NumericUpDown2.Location = New System.Drawing.Point(691, 15)
        Me.NumericUpDown2.Margin = New System.Windows.Forms.Padding(2)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown2.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(65, 26)
        Me.NumericUpDown2.TabIndex = 36
        Me.NumericUpDown2.Value = New Decimal(New Integer() {3, 0, 0, 65536})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(550, 21)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(131, 13)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "着差強調表示基準値："
        '
        'RbInai
        '
        Me.RbInai.AutoSize = True
        Me.RbInai.Checked = True
        Me.RbInai.Location = New System.Drawing.Point(162, 186)
        Me.RbInai.Name = "RbInai"
        Me.RbInai.Size = New System.Drawing.Size(47, 16)
        Me.RbInai.TabIndex = 37
        Me.RbInai.TabStop = True
        Me.RbInai.Text = "以内"
        Me.RbInai.UseVisualStyleBackColor = True
        '
        'RbIka
        '
        Me.RbIka.AutoSize = True
        Me.RbIka.Location = New System.Drawing.Point(215, 187)
        Me.RbIka.Name = "RbIka"
        Me.RbIka.Size = New System.Drawing.Size(47, 16)
        Me.RbIka.TabIndex = 38
        Me.RbIka.Text = "以下"
        Me.RbIka.UseVisualStyleBackColor = True
        '
        'BtnWinRate
        '
        Me.BtnWinRate.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnWinRate.Location = New System.Drawing.Point(222, 40)
        Me.BtnWinRate.Name = "BtnWinRate"
        Me.BtnWinRate.Size = New System.Drawing.Size(47, 37)
        Me.BtnWinRate.TabIndex = 39
        Me.BtnWinRate.Text = "勝率検索"
        Me.BtnWinRate.UseVisualStyleBackColor = True
        '
        'BtnFilterClear
        '
        Me.BtnFilterClear.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnFilterClear.Location = New System.Drawing.Point(901, 5)
        Me.BtnFilterClear.Name = "BtnFilterClear"
        Me.BtnFilterClear.Size = New System.Drawing.Size(102, 41)
        Me.BtnFilterClear.TabIndex = 40
        Me.BtnFilterClear.Text = "フィルタークリア"
        Me.BtnFilterClear.UseVisualStyleBackColor = True
        '
        'BtnCoefReview
        '
        Me.BtnCoefReview.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnCoefReview.Location = New System.Drawing.Point(169, 139)
        Me.BtnCoefReview.Name = "BtnCoefReview"
        Me.BtnCoefReview.Size = New System.Drawing.Size(91, 37)
        Me.BtnCoefReview.TabIndex = 41
        Me.BtnCoefReview.Text = "係数検証"
        Me.BtnCoefReview.UseVisualStyleBackColor = True
        '
        'chkRmv1999
        '
        Me.chkRmv1999.AutoSize = True
        Me.chkRmv1999.Checked = True
        Me.chkRmv1999.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRmv1999.Location = New System.Drawing.Point(169, 220)
        Me.chkRmv1999.Name = "chkRmv1999"
        Me.chkRmv1999.Size = New System.Drawing.Size(97, 16)
        Me.chkRmv1999.TabIndex = 42
        Me.chkRmv1999.Text = "id<1999は除外"
        Me.chkRmv1999.UseVisualStyleBackColor = True
        '
        'raceReviewForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1412, 631)
        Me.Controls.Add(Me.chkRmv1999)
        Me.Controls.Add(Me.BtnCoefReview)
        Me.Controls.Add(Me.BtnFilterClear)
        Me.Controls.Add(Me.BtnWinRate)
        Me.Controls.Add(Me.RbIka)
        Me.Controls.Add(Me.RbInai)
        Me.Controls.Add(Me.NumericUpDown2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.BtnJokenCls)
        Me.Controls.Add(Me.CbRacename)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.CbMonth)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.CbGrade)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CbKyori)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CbSyubetu)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CbJo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkDosu)
        Me.Controls.Add(Me.CbCyakujun)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.BtnHistGet)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BtnRedisp)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.flx)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "raceReviewForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "レース結果の検討"
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents flx As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents BtnRedisp As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents BtnHistGet As Button
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents CbCyakujun As ComboBox
    Friend WithEvents chkDosu As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CbJo As ComboBox
    Friend WithEvents CbSyubetu As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents CbKyori As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CbGrade As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents CbMonth As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents CbRacename As ComboBox
    Friend WithEvents BtnJokenCls As Button
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents Label10 As Label
    Friend WithEvents RbInai As RadioButton
    Friend WithEvents RbIka As RadioButton
    Friend WithEvents BtnWinRate As Button
    Friend WithEvents BtnFilterClear As Button
    Friend WithEvents BtnCoefReview As Button
    Friend WithEvents chkRmv1999 As CheckBox
End Class
