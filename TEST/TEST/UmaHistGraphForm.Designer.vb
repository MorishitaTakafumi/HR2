<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UmaHistGraphForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UmaHistGraphForm))
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lb_bamei = New System.Windows.Forms.Label()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pic
        '
        Me.pic.Location = New System.Drawing.Point(30, 46)
        Me.pic.Margin = New System.Windows.Forms.Padding(4)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(1098, 679)
        Me.pic.TabIndex = 0
        Me.pic.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(56, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "馬名"
        '
        'lb_bamei
        '
        Me.lb_bamei.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lb_bamei.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lb_bamei.Location = New System.Drawing.Point(113, 11)
        Me.lb_bamei.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_bamei.Name = "lb_bamei"
        Me.lb_bamei.Size = New System.Drawing.Size(276, 19)
        Me.lb_bamei.TabIndex = 2
        '
        'UmaHistGraphForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1160, 753)
        Me.Controls.Add(Me.lb_bamei)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UmaHistGraphForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "出走レースの履歴グラフ"
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pic As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lb_bamei As Label
End Class
