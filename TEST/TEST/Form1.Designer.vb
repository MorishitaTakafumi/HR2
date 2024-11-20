<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.BtnTest = New System.Windows.Forms.Button()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtResult = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.flx = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.BtnURL = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRaceName = New System.Windows.Forms.TextBox()
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnTest
        '
        Me.BtnTest.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnTest.Location = New System.Drawing.Point(18, 80)
        Me.BtnTest.Name = "BtnTest"
        Me.BtnTest.Size = New System.Drawing.Size(139, 44)
        Me.BtnTest.TabIndex = 0
        Me.BtnTest.Text = "レース結果の取り込み"
        Me.BtnTest.UseVisualStyleBackColor = True
        '
        'txtURL
        '
        Me.txtURL.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtURL.Location = New System.Drawing.Point(55, 11)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(852, 20)
        Me.txtURL.TabIndex = 1
        Me.txtURL.Text = "https://www.jra.go.jp/JRADB/accessD.html?CNAME=pw01dde0106202404091120240929/C6"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "URL"
        '
        'txtResult
        '
        Me.txtResult.Location = New System.Drawing.Point(163, 80)
        Me.txtResult.Multiline = True
        Me.txtResult.Name = "txtResult"
        Me.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtResult.Size = New System.Drawing.Size(706, 44)
        Me.txtResult.TabIndex = 3
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 15
        Me.ListBox1.Location = New System.Drawing.Point(16, 139)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(320, 244)
        Me.ListBox1.TabIndex = 4
        '
        'flx
        '
        Me.flx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flx.ColumnInfo = resources.GetString("flx.ColumnInfo")
        Me.flx.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.flx.Location = New System.Drawing.Point(358, 139)
        Me.flx.Name = "flx"
        Me.flx.Rows.DefaultSize = 19
        Me.flx.Size = New System.Drawing.Size(917, 470)
        Me.flx.StyleInfo = resources.GetString("flx.StyleInfo")
        Me.flx.TabIndex = 5
        '
        'BtnURL
        '
        Me.BtnURL.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnURL.Location = New System.Drawing.Point(913, 11)
        Me.BtnURL.Name = "BtnURL"
        Me.BtnURL.Size = New System.Drawing.Size(139, 44)
        Me.BtnURL.TabIndex = 8
        Me.BtnURL.Text = "URL貼り付け"
        Me.BtnURL.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "日付"
        '
        'txtDate
        '
        Me.txtDate.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtDate.Location = New System.Drawing.Point(55, 44)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(162, 20)
        Me.txtDate.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(253, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "レース名"
        '
        'txtRaceName
        '
        Me.txtRaceName.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtRaceName.Location = New System.Drawing.Point(310, 44)
        Me.txtRaceName.Name = "txtRaceName"
        Me.txtRaceName.Size = New System.Drawing.Size(204, 20)
        Me.txtRaceName.TabIndex = 11
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1312, 631)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRaceName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.BtnURL)
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.txtResult)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.BtnTest)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "レース結果のとりこみ"
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnTest As Button
    Friend WithEvents txtURL As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtResult As TextBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents flx As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents BtnURL As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDate As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtRaceName As TextBox
End Class
