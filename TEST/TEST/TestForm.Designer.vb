﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestForm
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.lb_msg = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(179, 44)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "レース href数の調査"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(12, 62)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(179, 52)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "UmaHist.hrefから" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "レース結果の取り込み"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(223, 12)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(846, 420)
        Me.ListBox1.TabIndex = 3
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(12, 170)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(179, 44)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "重複登録馬の調査"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(12, 220)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(179, 44)
        Me.Button5.TabIndex = 5
        Me.Button5.Text = "重複登録馬の削除"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'lb_msg
        '
        Me.lb_msg.AutoSize = True
        Me.lb_msg.Location = New System.Drawing.Point(10, 587)
        Me.lb_msg.Name = "lb_msg"
        Me.lb_msg.Size = New System.Drawing.Size(23, 12)
        Me.lb_msg.TabIndex = 6
        Me.lb_msg.Text = "***"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(14, 270)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(179, 44)
        Me.Button6.TabIndex = 7
        Me.Button6.Text = "重複登録レースの調査"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(14, 320)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(179, 44)
        Me.Button7.TabIndex = 8
        Me.Button7.Text = "重複登録レースの削除"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(14, 370)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(179, 44)
        Me.Button8.TabIndex = 9
        Me.Button8.Text = "空の長いレース名の削除"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(14, 420)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(179, 44)
        Me.Button9.TabIndex = 10
        Me.Button9.Text = "レース情報の不整合チェック"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(14, 470)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(179, 44)
        Me.Button10.TabIndex = 11
        Me.Button10.Text = "不整合レース削除"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(14, 520)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(179, 44)
        Me.Button11.TabIndex = 12
        Me.Button11.Text = "クラス間補正値表示"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(199, 520)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(179, 44)
        Me.Button12.TabIndex = 13
        Me.Button12.Text = "着差を生値に戻す"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(384, 520)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(179, 44)
        Me.Button13.TabIndex = 14
        Me.Button13.Text = "レースの連帯馬で未登録馬の検索"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(569, 520)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(179, 44)
        Me.Button14.TabIndex = 15
        Me.Button14.Text = "レースの連帯馬で未登録馬の登録"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'TestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1081, 608)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.lb_msg)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "TestForm"
        Me.Text = "TestForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents lb_msg As Label
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Button11 As Button
    Friend WithEvents Button12 As Button
    Friend WithEvents Button13 As Button
    Friend WithEvents Button14 As Button
End Class
