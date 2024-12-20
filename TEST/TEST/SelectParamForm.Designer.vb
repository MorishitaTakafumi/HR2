<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectParamForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectParamForm))
        Me.LstParams = New System.Windows.Forms.ListBox()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnSelectDefv = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LstParams
        '
        Me.LstParams.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LstParams.FormattingEnabled = True
        Me.LstParams.Location = New System.Drawing.Point(25, 24)
        Me.LstParams.Name = "LstParams"
        Me.LstParams.Size = New System.Drawing.Size(746, 355)
        Me.LstParams.TabIndex = 0
        '
        'BtnOK
        '
        Me.BtnOK.Image = CType(resources.GetObject("BtnOK.Image"), System.Drawing.Image)
        Me.BtnOK.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnOK.Location = New System.Drawing.Point(615, 404)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 34)
        Me.BtnOK.TabIndex = 1
        Me.BtnOK.Text = "決定"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Image = CType(resources.GetObject("BtnCancel.Image"), System.Drawing.Image)
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnCancel.Location = New System.Drawing.Point(696, 404)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 34)
        Me.BtnCancel.TabIndex = 2
        Me.BtnCancel.Text = "中止"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnSelectDefv
        '
        Me.BtnSelectDefv.Image = CType(resources.GetObject("BtnSelectDefv.Image"), System.Drawing.Image)
        Me.BtnSelectDefv.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnSelectDefv.Location = New System.Drawing.Point(361, 404)
        Me.BtnSelectDefv.Name = "BtnSelectDefv"
        Me.BtnSelectDefv.Size = New System.Drawing.Size(169, 34)
        Me.BtnSelectDefv.TabIndex = 3
        Me.BtnSelectDefv.Text = "プログラム既定値を選択"
        Me.BtnSelectDefv.UseVisualStyleBackColor = True
        '
        'SelectParamForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.BtnSelectDefv)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.LstParams)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SelectParamForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "パラメータ選択"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LstParams As ListBox
    Friend WithEvents BtnOK As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnSelectDefv As Button
End Class
