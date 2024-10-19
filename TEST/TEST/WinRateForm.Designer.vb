<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WinRateForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinRateForm))
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.BtnWinRate = New System.Windows.Forms.Button()
        Me.ListBox = New System.Windows.Forms.ListBox()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown6 = New System.Windows.Forms.NumericUpDown()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown7 = New System.Windows.Forms.NumericUpDown()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown8 = New System.Windows.Forms.NumericUpDown()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.DecimalPlaces = 2
        Me.NumericUpDown1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown1.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.NumericUpDown1.Location = New System.Drawing.Point(266, 27)
        Me.NumericUpDown1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown1.TabIndex = 23
        Me.NumericUpDown1.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'BtnWinRate
        '
        Me.BtnWinRate.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnWinRate.Location = New System.Drawing.Point(483, 22)
        Me.BtnWinRate.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnWinRate.Name = "BtnWinRate"
        Me.BtnWinRate.Size = New System.Drawing.Size(108, 66)
        Me.BtnWinRate.TabIndex = 40
        Me.BtnWinRate.Text = "勝率検索"
        Me.BtnWinRate.UseVisualStyleBackColor = True
        '
        'ListBox
        '
        Me.ListBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListBox.Font = New System.Drawing.Font("ＭＳ ゴシック", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox.FormattingEnabled = True
        Me.ListBox.ItemHeight = 17
        Me.ListBox.Location = New System.Drawing.Point(74, 409)
        Me.ListBox.Margin = New System.Windows.Forms.Padding(4)
        Me.ListBox.Name = "ListBox"
        Me.ListBox.Size = New System.Drawing.Size(383, 89)
        Me.ListBox.TabIndex = 41
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.DecimalPlaces = 2
        Me.NumericUpDown2.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown2.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.NumericUpDown2.Location = New System.Drawing.Point(266, 63)
        Me.NumericUpDown2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NumericUpDown2.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown2.TabIndex = 43
        Me.NumericUpDown2.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.DecimalPlaces = 2
        Me.NumericUpDown3.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown3.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.NumericUpDown3.Location = New System.Drawing.Point(266, 97)
        Me.NumericUpDown3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown3.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NumericUpDown3.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown3.TabIndex = 45
        Me.NumericUpDown3.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.DecimalPlaces = 2
        Me.NumericUpDown4.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown4.Increment = New Decimal(New Integer() {1, 0, 0, 65536})
        Me.NumericUpDown4.Location = New System.Drawing.Point(266, 131)
        Me.NumericUpDown4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown4.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NumericUpDown4.Minimum = New Decimal(New Integer() {99, 0, 0, -2147483648})
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown4.TabIndex = 47
        Me.NumericUpDown4.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(74, 35)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(111, 19)
        Me.CheckBox1.TabIndex = 48
        Me.CheckBox1.Text = "１走前の上差"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(74, 71)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(111, 19)
        Me.CheckBox2.TabIndex = 49
        Me.CheckBox2.Text = "２走前の上差"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(74, 105)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(111, 19)
        Me.CheckBox3.TabIndex = 50
        Me.CheckBox3.Text = "３走前の上差"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(74, 139)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(111, 19)
        Me.CheckBox4.TabIndex = 51
        Me.CheckBox4.Text = "４走前の上差"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(74, 181)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(96, 19)
        Me.CheckBox5.TabIndex = 52
        Me.CheckBox5.Text = "spanScore"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(213, 179)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(210, 22)
        Me.TextBox1.TabIndex = 53
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(74, 227)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(140, 19)
        Me.CheckBox6.TabIndex = 55
        Me.CheckBox6.Text = "span１着の回数≧"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown5.Location = New System.Drawing.Point(266, 219)
        Me.NumericUpDown5.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown5.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown5.TabIndex = 54
        Me.NumericUpDown5.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.Location = New System.Drawing.Point(74, 270)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(138, 19)
        Me.CheckBox7.TabIndex = 57
        Me.CheckBox7.Text = "span2着の回数≧"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'NumericUpDown6
        '
        Me.NumericUpDown6.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown6.Location = New System.Drawing.Point(266, 262)
        Me.NumericUpDown6.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown6.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NumericUpDown6.Name = "NumericUpDown6"
        Me.NumericUpDown6.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown6.TabIndex = 56
        Me.NumericUpDown6.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = True
        Me.CheckBox8.Location = New System.Drawing.Point(74, 314)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(138, 19)
        Me.CheckBox8.TabIndex = 59
        Me.CheckBox8.Text = "span3着の回数≧"
        Me.CheckBox8.UseVisualStyleBackColor = True
        '
        'NumericUpDown7
        '
        Me.NumericUpDown7.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown7.Location = New System.Drawing.Point(266, 306)
        Me.NumericUpDown7.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown7.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NumericUpDown7.Name = "NumericUpDown7"
        Me.NumericUpDown7.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown7.TabIndex = 58
        Me.NumericUpDown7.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'CheckBox9
        '
        Me.CheckBox9.AutoSize = True
        Me.CheckBox9.Location = New System.Drawing.Point(74, 364)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(168, 19)
        Me.CheckBox9.TabIndex = 61
        Me.CheckBox9.Text = "span4着以下の回数≧"
        Me.CheckBox9.UseVisualStyleBackColor = True
        '
        'NumericUpDown8
        '
        Me.NumericUpDown8.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown8.Location = New System.Drawing.Point(266, 356)
        Me.NumericUpDown8.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown8.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.NumericUpDown8.Name = "NumericUpDown8"
        Me.NumericUpDown8.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown8.TabIndex = 60
        Me.NumericUpDown8.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"≧", "≦"})
        Me.ComboBox1.Location = New System.Drawing.Point(200, 33)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(60, 23)
        Me.ComboBox1.TabIndex = 62
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"≧", "≦"})
        Me.ComboBox2.Location = New System.Drawing.Point(200, 65)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(60, 23)
        Me.ComboBox2.TabIndex = 63
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"≧", "≦"})
        Me.ComboBox3.Location = New System.Drawing.Point(200, 101)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(60, 23)
        Me.ComboBox3.TabIndex = 64
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Items.AddRange(New Object() {"≧", "≦"})
        Me.ComboBox4.Location = New System.Drawing.Point(200, 135)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(60, 23)
        Me.ComboBox4.TabIndex = 65
        '
        'WinRateForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 511)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.CheckBox9)
        Me.Controls.Add(Me.NumericUpDown8)
        Me.Controls.Add(Me.CheckBox8)
        Me.Controls.Add(Me.NumericUpDown7)
        Me.Controls.Add(Me.CheckBox7)
        Me.Controls.Add(Me.NumericUpDown6)
        Me.Controls.Add(Me.CheckBox6)
        Me.Controls.Add(Me.NumericUpDown5)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.CheckBox5)
        Me.Controls.Add(Me.CheckBox4)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.NumericUpDown4)
        Me.Controls.Add(Me.NumericUpDown3)
        Me.Controls.Add(Me.NumericUpDown2)
        Me.Controls.Add(Me.ListBox)
        Me.Controls.Add(Me.BtnWinRate)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "WinRateForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WinRateForm"
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents BtnWinRate As Button
    Friend WithEvents ListBox As ListBox
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents NumericUpDown3 As NumericUpDown
    Friend WithEvents NumericUpDown4 As NumericUpDown
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CheckBox6 As CheckBox
    Friend WithEvents NumericUpDown5 As NumericUpDown
    Friend WithEvents CheckBox7 As CheckBox
    Friend WithEvents NumericUpDown6 As NumericUpDown
    Friend WithEvents CheckBox8 As CheckBox
    Friend WithEvents NumericUpDown7 As NumericUpDown
    Friend WithEvents CheckBox9 As CheckBox
    Friend WithEvents NumericUpDown8 As NumericUpDown
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents ComboBox4 As ComboBox
End Class
