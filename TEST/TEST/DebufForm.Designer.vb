<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DebufForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DebufForm))
        Me.CbKyori = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CbSyubetu = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.BtnRedisp = New System.Windows.Forms.Button()
        Me.flx = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.CbBa = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CbKyori
        '
        Me.CbKyori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbKyori.FormattingEnabled = True
        Me.CbKyori.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.CbKyori.Location = New System.Drawing.Point(354, 24)
        Me.CbKyori.Margin = New System.Windows.Forms.Padding(2)
        Me.CbKyori.Name = "CbKyori"
        Me.CbKyori.Size = New System.Drawing.Size(92, 20)
        Me.CbKyori.TabIndex = 27
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(321, 27)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "距離"
        '
        'CbSyubetu
        '
        Me.CbSyubetu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbSyubetu.FormattingEnabled = True
        Me.CbSyubetu.Location = New System.Drawing.Point(208, 24)
        Me.CbSyubetu.Margin = New System.Windows.Forms.Padding(2)
        Me.CbSyubetu.Name = "CbSyubetu"
        Me.CbSyubetu.Size = New System.Drawing.Size(92, 20)
        Me.CbSyubetu.TabIndex = 26
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(175, 27)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "種別"
        '
        'BtnRedisp
        '
        Me.BtnRedisp.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRedisp.Location = New System.Drawing.Point(468, 12)
        Me.BtnRedisp.Name = "BtnRedisp"
        Me.BtnRedisp.Size = New System.Drawing.Size(82, 41)
        Me.BtnRedisp.TabIndex = 30
        Me.BtnRedisp.Text = "再表示"
        Me.BtnRedisp.UseVisualStyleBackColor = True
        '
        'flx
        '
        Me.flx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flx.ColumnInfo = resources.GetString("flx.ColumnInfo")
        Me.flx.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.flx.Location = New System.Drawing.Point(22, 59)
        Me.flx.Name = "flx"
        Me.flx.Rows.DefaultSize = 21
        Me.flx.Size = New System.Drawing.Size(987, 494)
        Me.flx.StyleInfo = resources.GetString("flx.StyleInfo")
        Me.flx.TabIndex = 31
        '
        'CbBa
        '
        Me.CbBa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbBa.FormattingEnabled = True
        Me.CbBa.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.CbBa.Location = New System.Drawing.Point(41, 24)
        Me.CbBa.Margin = New System.Windows.Forms.Padding(2)
        Me.CbBa.Name = "CbBa"
        Me.CbBa.Size = New System.Drawing.Size(92, 20)
        Me.CbBa.TabIndex = 32
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 27)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "場"
        '
        'DebufForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1021, 565)
        Me.Controls.Add(Me.CbBa)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.BtnRedisp)
        Me.Controls.Add(Me.CbKyori)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CbSyubetu)
        Me.Controls.Add(Me.Label4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DebufForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "デバッグ用"
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CbKyori As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents CbSyubetu As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents BtnRedisp As Button
    Friend WithEvents flx As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents CbBa As ComboBox
    Friend WithEvents Label1 As Label
End Class
