<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AnaForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnaForm))
        Me.BtnGo = New System.Windows.Forms.Button()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.flx = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lb_msg = New System.Windows.Forms.Label()
        Me.BtnURL = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.BtnRedisp = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkJo = New System.Windows.Forms.CheckBox()
        Me.chkKyori = New System.Windows.Forms.CheckBox()
        Me.chkRacename = New System.Windows.Forms.CheckBox()
        Me.chkMonth = New System.Windows.Forms.CheckBox()
        Me.BtnHistGet = New System.Windows.Forms.Button()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.CbCyakujun = New System.Windows.Forms.ComboBox()
        Me.chkDosu = New System.Windows.Forms.CheckBox()
        Me.BtnWinRate = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnDof = New System.Windows.Forms.Button()
        Me.chkRacename2 = New System.Windows.Forms.CheckBox()
        Me.txtRacename = New System.Windows.Forms.TextBox()
        Me.RbURL = New System.Windows.Forms.RadioButton()
        Me.RbFile = New System.Windows.Forms.RadioButton()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.BtnFile = New System.Windows.Forms.Button()
        Me.CbCyakujun2 = New System.Windows.Forms.ComboBox()
        Me.CbGradeL = New System.Windows.Forms.ComboBox()
        Me.CbGradeH = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BtnGetCount = New System.Windows.Forms.Button()
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnGo
        '
        Me.BtnGo.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnGo.Location = New System.Drawing.Point(787, 10)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(131, 67)
        Me.BtnGo.TabIndex = 0
        Me.BtnGo.Text = "解析実行"
        Me.BtnGo.UseVisualStyleBackColor = True
        '
        'txtURL
        '
        Me.txtURL.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtURL.Location = New System.Drawing.Point(76, 16)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(564, 20)
        Me.txtURL.TabIndex = 1
        Me.txtURL.Text = "https://www.jra.go.jp/JRADB/accessD.html?CNAME=pw01dde0105202404081120241027/54"
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(10, 127)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(288, 199)
        Me.ListBox1.TabIndex = 4
        '
        'flx
        '
        Me.flx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flx.ColumnInfo = resources.GetString("flx.ColumnInfo")
        Me.flx.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.flx.Location = New System.Drawing.Point(303, 94)
        Me.flx.Name = "flx"
        Me.flx.Rows.DefaultSize = 18
        Me.flx.Size = New System.Drawing.Size(1055, 495)
        Me.flx.StyleInfo = resources.GetString("flx.StyleInfo")
        Me.flx.TabIndex = 5
        '
        'lb_msg
        '
        Me.lb_msg.AutoSize = True
        Me.lb_msg.Location = New System.Drawing.Point(11, 344)
        Me.lb_msg.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lb_msg.Name = "lb_msg"
        Me.lb_msg.Size = New System.Drawing.Size(23, 12)
        Me.lb_msg.TabIndex = 6
        Me.lb_msg.Text = "***"
        '
        'BtnURL
        '
        Me.BtnURL.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnURL.Location = New System.Drawing.Point(652, 10)
        Me.BtnURL.Name = "BtnURL"
        Me.BtnURL.Size = New System.Drawing.Size(115, 31)
        Me.BtnURL.TabIndex = 7
        Me.BtnURL.Text = "URL貼り付け"
        Me.BtnURL.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 97)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(137, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "上がり強調表示基準値："
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.DecimalPlaces = 1
        Me.NumericUpDown1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown1.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.NumericUpDown1.Location = New System.Drawing.Point(152, 90)
        Me.NumericUpDown1.Margin = New System.Windows.Forms.Padding(2)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(65, 26)
        Me.NumericUpDown1.TabIndex = 9
        Me.NumericUpDown1.Value = New Decimal(New Integer() {3, 0, 0, 65536})
        '
        'BtnRedisp
        '
        Me.BtnRedisp.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRedisp.Location = New System.Drawing.Point(223, 90)
        Me.BtnRedisp.Name = "BtnRedisp"
        Me.BtnRedisp.Size = New System.Drawing.Size(74, 30)
        Me.BtnRedisp.TabIndex = 10
        Me.BtnRedisp.Text = "再表示"
        Me.BtnRedisp.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 379)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(151, 12)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "過去レース解析値の検索条件"
        '
        'chkJo
        '
        Me.chkJo.AutoSize = True
        Me.chkJo.Checked = True
        Me.chkJo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkJo.Location = New System.Drawing.Point(15, 402)
        Me.chkJo.Name = "chkJo"
        Me.chkJo.Size = New System.Drawing.Size(84, 16)
        Me.chkJo.TabIndex = 12
        Me.chkJo.Text = "同一競馬場"
        Me.chkJo.UseVisualStyleBackColor = True
        '
        'chkKyori
        '
        Me.chkKyori.AutoSize = True
        Me.chkKyori.Checked = True
        Me.chkKyori.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkKyori.Location = New System.Drawing.Point(15, 429)
        Me.chkKyori.Name = "chkKyori"
        Me.chkKyori.Size = New System.Drawing.Size(72, 16)
        Me.chkKyori.TabIndex = 13
        Me.chkKyori.Text = "同一距離"
        Me.chkKyori.UseVisualStyleBackColor = True
        '
        'chkRacename
        '
        Me.chkRacename.AutoSize = True
        Me.chkRacename.Location = New System.Drawing.Point(15, 456)
        Me.chkRacename.Name = "chkRacename"
        Me.chkRacename.Size = New System.Drawing.Size(88, 16)
        Me.chkRacename.TabIndex = 14
        Me.chkRacename.Text = "同一レース名"
        Me.chkRacename.UseVisualStyleBackColor = True
        '
        'chkMonth
        '
        Me.chkMonth.AutoSize = True
        Me.chkMonth.Location = New System.Drawing.Point(15, 510)
        Me.chkMonth.Name = "chkMonth"
        Me.chkMonth.Size = New System.Drawing.Size(60, 16)
        Me.chkMonth.TabIndex = 16
        Me.chkMonth.Text = "同一月"
        Me.chkMonth.UseVisualStyleBackColor = True
        '
        'BtnHistGet
        '
        Me.BtnHistGet.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnHistGet.Location = New System.Drawing.Point(248, 478)
        Me.BtnHistGet.Name = "BtnHistGet"
        Me.BtnHistGet.Size = New System.Drawing.Size(42, 42)
        Me.BtnHistGet.TabIndex = 17
        Me.BtnHistGet.Text = "検索実行"
        Me.BtnHistGet.UseVisualStyleBackColor = True
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.Font = New System.Drawing.Font("ＭＳ ゴシック", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 14
        Me.ListBox2.Location = New System.Drawing.Point(15, 589)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(288, 18)
        Me.ListBox2.TabIndex = 18
        '
        'CbCyakujun
        '
        Me.CbCyakujun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbCyakujun.FormattingEnabled = True
        Me.CbCyakujun.Items.AddRange(New Object() {"ALL", "1着のみ", "2着以内", "3着以内", "4着以下"})
        Me.CbCyakujun.Location = New System.Drawing.Point(132, 412)
        Me.CbCyakujun.Margin = New System.Windows.Forms.Padding(2)
        Me.CbCyakujun.Name = "CbCyakujun"
        Me.CbCyakujun.Size = New System.Drawing.Size(92, 20)
        Me.CbCyakujun.TabIndex = 19
        '
        'chkDosu
        '
        Me.chkDosu.AutoSize = True
        Me.chkDosu.Location = New System.Drawing.Point(15, 564)
        Me.chkDosu.Name = "chkDosu"
        Me.chkDosu.Size = New System.Drawing.Size(187, 16)
        Me.chkDosu.TabIndex = 20
        Me.chkDosu.Text = "spanScoreの度数分布を表示する"
        Me.chkDosu.UseVisualStyleBackColor = True
        '
        'BtnWinRate
        '
        Me.BtnWinRate.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnWinRate.Location = New System.Drawing.Point(248, 362)
        Me.BtnWinRate.Name = "BtnWinRate"
        Me.BtnWinRate.Size = New System.Drawing.Size(42, 44)
        Me.BtnWinRate.TabIndex = 40
        Me.BtnWinRate.Text = "勝率検索"
        Me.BtnWinRate.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.Location = New System.Drawing.Point(964, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(76, 67)
        Me.Button1.TabIndex = 41
        Me.Button1.Text = "過去レース検討"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnDof
        '
        Me.BtnDof.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnDof.Location = New System.Drawing.Point(1046, 10)
        Me.BtnDof.Name = "BtnDof"
        Me.BtnDof.Size = New System.Drawing.Size(76, 30)
        Me.BtnDof.TabIndex = 42
        Me.BtnDof.Text = "適合度"
        Me.BtnDof.UseVisualStyleBackColor = True
        '
        'chkRacename2
        '
        Me.chkRacename2.AutoSize = True
        Me.chkRacename2.Location = New System.Drawing.Point(15, 537)
        Me.chkRacename2.Name = "chkRacename2"
        Me.chkRacename2.Size = New System.Drawing.Size(112, 16)
        Me.chkRacename2.TabIndex = 43
        Me.chkRacename2.Text = "レース名一部指定"
        Me.chkRacename2.UseVisualStyleBackColor = True
        '
        'txtRacename
        '
        Me.txtRacename.Location = New System.Drawing.Point(132, 535)
        Me.txtRacename.Margin = New System.Windows.Forms.Padding(2)
        Me.txtRacename.Name = "txtRacename"
        Me.txtRacename.Size = New System.Drawing.Size(152, 19)
        Me.txtRacename.TabIndex = 44
        '
        'RbURL
        '
        Me.RbURL.AutoSize = True
        Me.RbURL.Checked = True
        Me.RbURL.Location = New System.Drawing.Point(14, 18)
        Me.RbURL.Margin = New System.Windows.Forms.Padding(2)
        Me.RbURL.Name = "RbURL"
        Me.RbURL.Size = New System.Drawing.Size(45, 16)
        Me.RbURL.TabIndex = 45
        Me.RbURL.TabStop = True
        Me.RbURL.Text = "URL"
        Me.RbURL.UseVisualStyleBackColor = True
        '
        'RbFile
        '
        Me.RbFile.AutoSize = True
        Me.RbFile.Location = New System.Drawing.Point(14, 52)
        Me.RbFile.Margin = New System.Windows.Forms.Padding(2)
        Me.RbFile.Name = "RbFile"
        Me.RbFile.Size = New System.Drawing.Size(42, 16)
        Me.RbFile.TabIndex = 46
        Me.RbFile.Text = "File"
        Me.RbFile.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFile.Location = New System.Drawing.Point(76, 50)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(564, 20)
        Me.txtFile.TabIndex = 47
        '
        'BtnFile
        '
        Me.BtnFile.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnFile.Location = New System.Drawing.Point(652, 46)
        Me.BtnFile.Name = "BtnFile"
        Me.BtnFile.Size = New System.Drawing.Size(115, 31)
        Me.BtnFile.TabIndex = 48
        Me.BtnFile.Text = "参照"
        Me.BtnFile.UseVisualStyleBackColor = True
        '
        'CbCyakujun2
        '
        Me.CbCyakujun2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbCyakujun2.FormattingEnabled = True
        Me.CbCyakujun2.Items.AddRange(New Object() {"1着", "2着以内", "3着以内"})
        Me.CbCyakujun2.Location = New System.Drawing.Point(1127, 14)
        Me.CbCyakujun2.Margin = New System.Windows.Forms.Padding(2)
        Me.CbCyakujun2.Name = "CbCyakujun2"
        Me.CbCyakujun2.Size = New System.Drawing.Size(92, 20)
        Me.CbCyakujun2.TabIndex = 49
        '
        'CbGradeL
        '
        Me.CbGradeL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbGradeL.FormattingEnabled = True
        Me.CbGradeL.Items.AddRange(New Object() {"0勝", "1勝", "2勝", "3勝", "Op", "G3", "G2", "G1"})
        Me.CbGradeL.Location = New System.Drawing.Point(13, 478)
        Me.CbGradeL.Margin = New System.Windows.Forms.Padding(2)
        Me.CbGradeL.Name = "CbGradeL"
        Me.CbGradeL.Size = New System.Drawing.Size(72, 20)
        Me.CbGradeL.TabIndex = 50
        '
        'CbGradeH
        '
        Me.CbGradeH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbGradeH.FormattingEnabled = True
        Me.CbGradeH.Items.AddRange(New Object() {"0勝", "1勝", "2勝", "3勝", "Op", "G3", "G2", "G1"})
        Me.CbGradeH.Location = New System.Drawing.Point(120, 478)
        Me.CbGradeH.Margin = New System.Windows.Forms.Padding(2)
        Me.CbGradeH.Name = "CbGradeH"
        Me.CbGradeH.Size = New System.Drawing.Size(72, 20)
        Me.CbGradeH.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(87, 481)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "以上"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(196, 481)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "以下"
        '
        'BtnGetCount
        '
        Me.BtnGetCount.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnGetCount.Location = New System.Drawing.Point(248, 412)
        Me.BtnGetCount.Name = "BtnGetCount"
        Me.BtnGetCount.Size = New System.Drawing.Size(42, 59)
        Me.BtnGetCount.TabIndex = 54
        Me.BtnGetCount.Text = "該当件数取得"
        Me.BtnGetCount.UseVisualStyleBackColor = True
        '
        'AnaForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1412, 631)
        Me.Controls.Add(Me.BtnGetCount)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CbGradeH)
        Me.Controls.Add(Me.CbGradeL)
        Me.Controls.Add(Me.CbCyakujun2)
        Me.Controls.Add(Me.BtnFile)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.RbFile)
        Me.Controls.Add(Me.RbURL)
        Me.Controls.Add(Me.txtRacename)
        Me.Controls.Add(Me.chkRacename2)
        Me.Controls.Add(Me.BtnDof)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.BtnWinRate)
        Me.Controls.Add(Me.chkDosu)
        Me.Controls.Add(Me.CbCyakujun)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.BtnHistGet)
        Me.Controls.Add(Me.chkMonth)
        Me.Controls.Add(Me.chkRacename)
        Me.Controls.Add(Me.chkKyori)
        Me.Controls.Add(Me.chkJo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BtnRedisp)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnURL)
        Me.Controls.Add(Me.lb_msg)
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.BtnGo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AnaForm"
        Me.Text = "レース解析"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnGo As Button
    Friend WithEvents txtURL As TextBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents flx As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents lb_msg As Label
    Friend WithEvents BtnURL As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents BtnRedisp As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents chkJo As CheckBox
    Friend WithEvents chkKyori As CheckBox
    Friend WithEvents chkRacename As CheckBox
    Friend WithEvents chkMonth As CheckBox
    Friend WithEvents BtnHistGet As Button
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents CbCyakujun As ComboBox
    Friend WithEvents chkDosu As CheckBox
    Friend WithEvents BtnWinRate As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents BtnDof As Button
    Friend WithEvents chkRacename2 As CheckBox
    Friend WithEvents txtRacename As TextBox
    Friend WithEvents RbURL As RadioButton
    Friend WithEvents RbFile As RadioButton
    Friend WithEvents txtFile As TextBox
    Friend WithEvents BtnFile As Button
    Friend WithEvents CbCyakujun2 As ComboBox
    Friend WithEvents CbGradeL As ComboBox
    Friend WithEvents CbGradeH As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents BtnGetCount As Button
End Class
