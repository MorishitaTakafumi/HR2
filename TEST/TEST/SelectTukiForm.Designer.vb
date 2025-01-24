<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectTukiForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectTukiForm))
        Me.BtnAllOff = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.LstTuki = New System.Windows.Forms.CheckedListBox()
        Me.SuspendLayout()
        '
        'BtnAllOff
        '
        Me.BtnAllOff.Image = CType(resources.GetObject("BtnAllOff.Image"), System.Drawing.Image)
        Me.BtnAllOff.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnAllOff.Location = New System.Drawing.Point(28, 282)
        Me.BtnAllOff.Name = "BtnAllOff"
        Me.BtnAllOff.Size = New System.Drawing.Size(75, 38)
        Me.BtnAllOff.TabIndex = 7
        Me.BtnAllOff.Text = "全オフ"
        Me.BtnAllOff.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Image = CType(resources.GetObject("BtnCancel.Image"), System.Drawing.Image)
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnCancel.Location = New System.Drawing.Point(199, 282)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 38)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "中止"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnOk
        '
        Me.BtnOk.Image = CType(resources.GetObject("BtnOk.Image"), System.Drawing.Image)
        Me.BtnOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnOk.Location = New System.Drawing.Point(118, 282)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 38)
        Me.BtnOk.TabIndex = 5
        Me.BtnOk.Text = "決定"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'LstTuki
        '
        Me.LstTuki.CheckOnClick = True
        Me.LstTuki.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LstTuki.FormattingEnabled = True
        Me.LstTuki.Items.AddRange(New Object() {"1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"})
        Me.LstTuki.Location = New System.Drawing.Point(28, 20)
        Me.LstTuki.Name = "LstTuki"
        Me.LstTuki.Size = New System.Drawing.Size(237, 220)
        Me.LstTuki.TabIndex = 4
        '
        'SelectTukiForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(296, 339)
        Me.Controls.Add(Me.BtnAllOff)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.LstTuki)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectTukiForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "月の選択"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnAllOff As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnOk As Button
    Friend WithEvents LstTuki As CheckedListBox
End Class
