Imports System.Data.SQLite

Public Class WinRateForm

    Private spanScore As List(Of Integer)
    Private cyakujun As List(Of Integer)
    Private agarisa1 As List(Of Single)
    Private agarisa2 As List(Of Single)
    Private agarisa3 As List(Of Single)
    Private agarisa4 As List(Of Single)

    Public Sub entry(ByVal span_score As List(Of Integer),
                     ByVal cyaku_jun As List(Of Integer),
                     ByVal agarisa_1 As List(Of Single),
                     ByVal agarisa_2 As List(Of Single),
                     ByVal agarisa_3 As List(Of Single),
                     ByVal agarisa_4 As List(Of Single),
                     Optional ByVal c_span_score As String = "",
                     Optional ByVal c_agarisa1 As Single = DMY_VAL,
                     Optional ByVal c_agarisa2 As Single = DMY_VAL,
                     Optional ByVal c_agarisa3 As Single = DMY_VAL,
                     Optional ByVal c_agarisa4 As Single = DMY_VAL
                     )
        spanScore = span_score
        cyakujun = cyaku_jun
        agarisa1 = agarisa_1
        agarisa2 = agarisa_2
        agarisa3 = agarisa_3
        agarisa4 = agarisa_4
        If c_span_score.Length > 0 Then
            TextBox1.Text = c_span_score
            Dim spanval As Integer = cnvScoreStr2Val(c_span_score)
            Dim iv As Integer = (spanval \ (10 ^ 6))
            NumericUpDown5.Value = iv
            iv = (spanval \ (10 ^ 4)) Mod 100
            NumericUpDown6.Value = iv
            iv = (spanval \ (10 ^ 2)) Mod 100
            NumericUpDown7.Value = iv
            iv = spanval Mod 100
            NumericUpDown8.Value = iv
        End If
        If c_agarisa1 <> DMY_VAL Then
            NumericUpDown1.Value = c_agarisa1 + 0.05
        End If
        If c_agarisa2 <> DMY_VAL Then
            NumericUpDown2.Value = c_agarisa2 + 0.05
        End If
        If c_agarisa3 <> DMY_VAL Then
            NumericUpDown3.Value = c_agarisa3 + 0.05
        End If
        If c_agarisa4 <> DMY_VAL Then
            NumericUpDown4.Value = c_agarisa4 + 0.05
        End If
        Show()
    End Sub


    Private Sub BtnWinRate_Click(sender As Object, e As EventArgs) Handles BtnWinRate.Click
        ListBox.Items.Clear()
        Dim jun_cnt(3) As Integer
        Dim ttlcnt As Integer = 0
        For j As Integer = 0 To agarisa1.Count - 1
            Dim flg As Boolean = True
            If CheckBox1.Checked Then
                If agarisa1(j) <> DMY_VAL Then
                    If ComboBox1.Text = "≦" Then
                        If agarisa1(j) > NumericUpDown1.Value Then
                            flg = False
                        End If
                    Else
                        If agarisa1(j) < NumericUpDown1.Value Then
                            flg = False
                        End If
                    End If
                Else
                    flg = False
                End If
            End If
            If CheckBox2.Checked Then
                If agarisa2(j) <> DMY_VAL Then
                    If ComboBox2.Text = "≦" Then
                        If agarisa2(j) > NumericUpDown2.Value Then
                            flg = False
                        End If
                    Else
                        If agarisa2(j) < NumericUpDown2.Value Then
                            flg = False
                        End If
                    End If
                Else
                    flg = False
                End If
            End If
            If CheckBox3.Checked Then
                If agarisa3(j) <> DMY_VAL Then
                    If ComboBox3.Text = "≦" Then
                        If agarisa3(j) > NumericUpDown3.Value Then
                            flg = False
                        End If
                    Else
                        If agarisa3(j) < NumericUpDown3.Value Then
                            flg = False
                        End If
                    End If
                Else
                    flg = False
                End If
            End If
            If CheckBox4.Checked Then
                If agarisa4(j) <> DMY_VAL Then
                    If ComboBox4.Text = "≦" Then
                        If agarisa4(j) > NumericUpDown4.Value Then
                            flg = False
                        End If
                    Else
                        If agarisa4(j) < NumericUpDown4.Value Then
                            flg = False
                        End If
                    End If
                Else
                    flg = False
                End If
            End If
            If CheckBox5.Checked Then
                If spanScore(j) <> cnvScoreStr2Val(TextBox1.Text) Then
                    flg = False
                End If
            End If
            If CheckBox6.Checked Then
                Dim tmpv As Integer = spanScore(j) \ (10 ^ 6)
                If tmpv < NumericUpDown5.Value Then
                    flg = False
                End If
            End If
            If CheckBox7.Checked Then
                Dim tmpv As Integer = (spanScore(j) \ (10 ^ 4)) Mod 100
                If tmpv < NumericUpDown6.Value Then
                    flg = False
                End If
            End If
            If CheckBox8.Checked Then
                Dim tmpv As Integer = (spanScore(j) \ (10 ^ 2)) Mod 100
                If tmpv < NumericUpDown7.Value Then
                    flg = False
                End If
            End If
            If CheckBox9.Checked Then
                Dim tmpv As Integer = spanScore(j) Mod 100
                If tmpv < NumericUpDown8.Value Then
                    flg = False
                End If
            End If
            If cyakujun(j) < 0 Then
                flg = False
            End If

            If flg Then
                Dim jun_idx As Integer = GetCyakujunIdx(cyakujun(j))
                jun_cnt(jun_idx) += 1
                ttlcnt += 1
            End If
        Next
        ListBox.Items.Add(jun_cnt(0).ToString & "-" & jun_cnt(1).ToString & "-" & jun_cnt(2).ToString & "-" & jun_cnt(3).ToString)
        If ttlcnt > 0 Then
            If jun_cnt(0) > 0 Then
                ListBox.Items.Add("単勝目安倍率：" & (1 / (jun_cnt(0) / ttlcnt)).ToString("F1"))
            End If
            If jun_cnt(0) + jun_cnt(1) + jun_cnt(2) > 0 Then
                ListBox.Items.Add("複勝目安倍率：" & (1 / ((jun_cnt(0) + jun_cnt(1) + jun_cnt(2)) / ttlcnt)).ToString("F1"))
            End If

        End If
    End Sub

    Private Function GetCyakujunIdx(ByVal cyakujun As Integer) As Integer
        If cyakujun > 0 AndAlso cyakujun <= 3 Then
            Return cyakujun - 1
        Else
            Return 3
        End If
    End Function

    Private Sub WinRateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 1
        ComboBox2.SelectedIndex = 1
        ComboBox3.SelectedIndex = 1
        ComboBox4.SelectedIndex = 1
    End Sub
End Class