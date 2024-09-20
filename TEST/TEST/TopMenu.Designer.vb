<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TopMenu
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
        Me.BtnRaceKekka = New System.Windows.Forms.Button()
        Me.BtnHorce = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnSyutubahyo = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnRaceKekka
        '
        Me.BtnRaceKekka.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnRaceKekka.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnRaceKekka.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRaceKekka.Location = New System.Drawing.Point(163, 56)
        Me.BtnRaceKekka.Name = "BtnRaceKekka"
        Me.BtnRaceKekka.Size = New System.Drawing.Size(249, 54)
        Me.BtnRaceKekka.TabIndex = 0
        Me.BtnRaceKekka.Text = "レース結果の取り込み"
        Me.BtnRaceKekka.UseVisualStyleBackColor = False
        '
        'BtnHorce
        '
        Me.BtnHorce.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnHorce.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnHorce.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnHorce.Location = New System.Drawing.Point(163, 127)
        Me.BtnHorce.Name = "BtnHorce"
        Me.BtnHorce.Size = New System.Drawing.Size(249, 54)
        Me.BtnHorce.TabIndex = 1
        Me.BtnHorce.Text = "競走馬情報の取り込み"
        Me.BtnHorce.UseVisualStyleBackColor = False
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnExit.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnExit.Location = New System.Drawing.Point(163, 271)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(249, 54)
        Me.BtnExit.TabIndex = 2
        Me.BtnExit.Text = "終了"
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnSyutubahyo
        '
        Me.BtnSyutubahyo.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnSyutubahyo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnSyutubahyo.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnSyutubahyo.Location = New System.Drawing.Point(163, 199)
        Me.BtnSyutubahyo.Name = "BtnSyutubahyo"
        Me.BtnSyutubahyo.Size = New System.Drawing.Size(249, 54)
        Me.BtnSyutubahyo.TabIndex = 3
        Me.BtnSyutubahyo.Text = "出馬表の取り込み"
        Me.BtnSyutubahyo.UseVisualStyleBackColor = False
        '
        'TopMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(569, 411)
        Me.Controls.Add(Me.BtnSyutubahyo)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnHorce)
        Me.Controls.Add(Me.BtnRaceKekka)
        Me.Name = "TopMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HR2トップメニュー"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnRaceKekka As Button
    Friend WithEvents BtnHorce As Button
    Friend WithEvents BtnExit As Button
    Friend WithEvents BtnSyutubahyo As Button
End Class
