<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Me.BtnGo = New System.Windows.Forms.Button()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.flx = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.BtnURL = New System.Windows.Forms.Button()
        Me.BtnFile = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.RbFile = New System.Windows.Forms.RadioButton()
        Me.RbURL = New System.Windows.Forms.RadioButton()
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnGo
        '
        Me.BtnGo.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnGo.Location = New System.Drawing.Point(782, 16)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(127, 62)
        Me.BtnGo.TabIndex = 0
        Me.BtnGo.Text = "出馬表" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "の取り込み"
        Me.BtnGo.UseVisualStyleBackColor = True
        '
        'txtURL
        '
        Me.txtURL.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtURL.Location = New System.Drawing.Point(73, 21)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(540, 20)
        Me.txtURL.TabIndex = 1
        Me.txtURL.Text = "https://www.jra.go.jp/JRADB/accessD.html?CNAME=pw01dde0108202405041120241013/49"
        '
        'txtResult
        '
        Me.txtResult.Location = New System.Drawing.Point(15, 366)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtResult.Size = New System.Drawing.Size(320, 45)
        Me.txtResult.TabIndex = 3
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(16, 114)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(320, 229)
        Me.ListBox1.TabIndex = 4
        '
        'flx
        '
        Me.flx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.flx.ColumnInfo = resources.GetString("flx.ColumnInfo")
        Me.flx.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.flx.Location = New System.Drawing.Point(358, 114)
        Me.flx.Name = "flx"
        Me.flx.Rows.DefaultSize = 19
        Me.flx.Size = New System.Drawing.Size(917, 495)
        Me.flx.StyleInfo = resources.GetString("flx.StyleInfo")
        Me.flx.TabIndex = 5
        '
        'BtnURL
        '
        Me.BtnURL.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnURL.Location = New System.Drawing.Point(626, 16)
        Me.BtnURL.Name = "BtnURL"
        Me.BtnURL.Size = New System.Drawing.Size(139, 27)
        Me.BtnURL.TabIndex = 8
        Me.BtnURL.Text = "URL貼り付け"
        Me.BtnURL.UseVisualStyleBackColor = True
        '
        'BtnFile
        '
        Me.BtnFile.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnFile.Location = New System.Drawing.Point(626, 53)
        Me.BtnFile.Name = "BtnFile"
        Me.BtnFile.Size = New System.Drawing.Size(139, 26)
        Me.BtnFile.TabIndex = 52
        Me.BtnFile.Text = "参照"
        Me.BtnFile.UseVisualStyleBackColor = True
        '
        'txtFile
        '
        Me.txtFile.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFile.Location = New System.Drawing.Point(73, 57)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.Size = New System.Drawing.Size(540, 20)
        Me.txtFile.TabIndex = 51
        '
        'RbFile
        '
        Me.RbFile.AutoSize = True
        Me.RbFile.Location = New System.Drawing.Point(16, 58)
        Me.RbFile.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RbFile.Name = "RbFile"
        Me.RbFile.Size = New System.Drawing.Size(42, 16)
        Me.RbFile.TabIndex = 50
        Me.RbFile.Text = "File"
        Me.RbFile.UseVisualStyleBackColor = True
        '
        'RbURL
        '
        Me.RbURL.AutoSize = True
        Me.RbURL.Checked = True
        Me.RbURL.Location = New System.Drawing.Point(16, 22)
        Me.RbURL.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.RbURL.Name = "RbURL"
        Me.RbURL.Size = New System.Drawing.Size(45, 16)
        Me.RbURL.TabIndex = 49
        Me.RbURL.TabStop = True
        Me.RbURL.Text = "URL"
        Me.RbURL.UseVisualStyleBackColor = True
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1312, 631)
        Me.Controls.Add(Me.BtnFile)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.RbFile)
        Me.Controls.Add(Me.RbURL)
        Me.Controls.Add(Me.BtnURL)
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.BtnGo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form3"
        Me.Text = "出馬表の取り込み"
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnGo As Button
    Friend WithEvents txtURL As TextBox
    Friend WithEvents txtResult As TextBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents flx As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents BtnURL As Button
    Friend WithEvents BtnFile As Button
    Friend WithEvents txtFile As TextBox
    Friend WithEvents RbFile As RadioButton
    Friend WithEvents RbURL As RadioButton
End Class
