﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StoreAnaValForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StoreAnaValForm))
        Me.BtnURL = New System.Windows.Forms.Button()
        Me.lb_msg = New System.Windows.Forms.Label()
        Me.flx = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.BtnGo = New System.Windows.Forms.Button()
        Me.lb_cnt = New System.Windows.Forms.Label()
        Me.BtnStop = New System.Windows.Forms.Button()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.flx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnURL
        '
        Me.BtnURL.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnURL.Location = New System.Drawing.Point(1273, 2)
        Me.BtnURL.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnURL.Name = "BtnURL"
        Me.BtnURL.Size = New System.Drawing.Size(185, 55)
        Me.BtnURL.TabIndex = 17
        Me.BtnURL.Text = "URL貼り付け"
        Me.BtnURL.UseVisualStyleBackColor = True
        '
        'lb_msg
        '
        Me.lb_msg.AutoSize = True
        Me.lb_msg.Location = New System.Drawing.Point(13, 469)
        Me.lb_msg.Name = "lb_msg"
        Me.lb_msg.Size = New System.Drawing.Size(31, 15)
        Me.lb_msg.TabIndex = 16
        Me.lb_msg.Text = "***"
        '
        'flx
        '
        Me.flx.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flx.ColumnInfo = resources.GetString("flx.ColumnInfo")
        Me.flx.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.flx.Location = New System.Drawing.Point(412, 62)
        Me.flx.Margin = New System.Windows.Forms.Padding(4)
        Me.flx.Name = "flx"
        Me.flx.Rows.DefaultSize = 26
        Me.flx.Size = New System.Drawing.Size(1405, 669)
        Me.flx.StyleInfo = resources.GetString("flx.StyleInfo")
        Me.flx.TabIndex = 15
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 19
        Me.ListBox1.Location = New System.Drawing.Point(16, 62)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(383, 289)
        Me.ListBox1.TabIndex = 14
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 17)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "URL"
        '
        'txtURL
        '
        Me.txtURL.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtURL.Location = New System.Drawing.Point(72, 16)
        Me.txtURL.Margin = New System.Windows.Forms.Padding(4)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(976, 24)
        Me.txtURL.TabIndex = 12
        Me.txtURL.Text = "https://www.jra.go.jp/JRADB/accessS.html?CNAME=pw01sde1005202404021120241006/CC"
        '
        'BtnGo
        '
        Me.BtnGo.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnGo.Location = New System.Drawing.Point(1080, 2)
        Me.BtnGo.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(185, 55)
        Me.BtnGo.TabIndex = 11
        Me.BtnGo.Text = "実行"
        Me.BtnGo.UseVisualStyleBackColor = True
        '
        'lb_cnt
        '
        Me.lb_cnt.AutoSize = True
        Me.lb_cnt.Location = New System.Drawing.Point(13, 428)
        Me.lb_cnt.Name = "lb_cnt"
        Me.lb_cnt.Size = New System.Drawing.Size(31, 15)
        Me.lb_cnt.TabIndex = 18
        Me.lb_cnt.Text = "***"
        '
        'BtnStop
        '
        Me.BtnStop.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnStop.Location = New System.Drawing.Point(283, 416)
        Me.BtnStop.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnStop.Name = "BtnStop"
        Me.BtnStop.Size = New System.Drawing.Size(100, 39)
        Me.BtnStop.TabIndex = 19
        Me.BtnStop.Text = "中止"
        Me.BtnStop.UseVisualStyleBackColor = True
        Me.BtnStop.Visible = False
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Font = New System.Drawing.Font("MS UI Gothic", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NumericUpDown1.Location = New System.Drawing.Point(189, 364)
        Me.NumericUpDown1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(87, 30)
        Me.NumericUpDown1.TabIndex = 21
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 372)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(167, 17)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "取り込むレース数上限："
        '
        'StoreAnaValForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1831, 748)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BtnStop)
        Me.Controls.Add(Me.lb_cnt)
        Me.Controls.Add(Me.BtnURL)
        Me.Controls.Add(Me.lb_msg)
        Me.Controls.Add(Me.flx)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.BtnGo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "StoreAnaValForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "勝ち馬の解析値登録"
        CType(Me.flx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnURL As Button
    Friend WithEvents lb_msg As Label
    Friend WithEvents flx As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtURL As TextBox
    Friend WithEvents BtnGo As Button
    Friend WithEvents lb_cnt As Label
    Friend WithEvents BtnStop As Button
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents Label2 As Label
End Class