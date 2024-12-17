<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectJoForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectJoForm))
        Me.LstJo = New System.Windows.Forms.CheckedListBox()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnAllOff = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LstJo
        '
        Me.LstJo.CheckOnClick = True
        Me.LstJo.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LstJo.FormattingEnabled = True
        Me.LstJo.Location = New System.Drawing.Point(21, 21)
        Me.LstJo.Name = "LstJo"
        Me.LstJo.Size = New System.Drawing.Size(237, 229)
        Me.LstJo.TabIndex = 0
        '
        'BtnOk
        '
        Me.BtnOk.Image = CType(resources.GetObject("BtnOk.Image"), System.Drawing.Image)
        Me.BtnOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnOk.Location = New System.Drawing.Point(102, 270)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 38)
        Me.BtnOk.TabIndex = 1
        Me.BtnOk.Text = "決定"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Image = CType(resources.GetObject("BtnCancel.Image"), System.Drawing.Image)
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnCancel.Location = New System.Drawing.Point(183, 270)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 38)
        Me.BtnCancel.TabIndex = 2
        Me.BtnCancel.Text = "中止"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnAllOff
        '
        Me.BtnAllOff.Image = CType(resources.GetObject("BtnAllOff.Image"), System.Drawing.Image)
        Me.BtnAllOff.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnAllOff.Location = New System.Drawing.Point(12, 270)
        Me.BtnAllOff.Name = "BtnAllOff"
        Me.BtnAllOff.Size = New System.Drawing.Size(75, 38)
        Me.BtnAllOff.TabIndex = 3
        Me.BtnAllOff.Text = "全オフ"
        Me.BtnAllOff.UseVisualStyleBackColor = True
        '
        'SelectJoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(277, 320)
        Me.Controls.Add(Me.BtnAllOff)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.LstJo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SelectJoForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "競馬場の選択"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LstJo As CheckedListBox
    Friend WithEvents BtnOk As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnAllOff As Button
End Class
