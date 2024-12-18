Imports System.IO
Public Class ParamReviewForm

    'パラメータの検討用
    Private Sub BtnFile_Click(sender As Object, e As EventArgs) Handles BtnFile.Click
        Dim dlg As New OpenFileDialog
        dlg.InitialDirectory = Path.Combine(GetTextDataFolder(), "出馬表")
        dlg.Filter = "txt files (*.txt)|*.txt"
        If dlg.ShowDialog() = DialogResult.OK Then
            txtFile.Text = dlg.FileName
        End If
    End Sub

    Private Sub showParam()
        txtTimeR1.Text = oParam.timeR1.ToString("F3")
        txtTimeR2.Text = oParam.timeR2.ToString("F3")
        txtTimeP0.Text = oParam.timeP(0).ToString("F3")
        txtTimeP1.Text = oParam.timeP(1).ToString("F3")
        txtTimeP2.Text = oParam.timeP(2).ToString("F3")
        txtTimeP3.Text = oParam.timeP(3).ToString("F3")
        txtTimeZone.Text = oParam.timeZoneCoef.ToString("F3")
    End Sub

    Private Sub torikomi()
        If IsNumeric(txtTimeR1.Text) Then
            oParam.timeR1 = CSng(txtTimeR1.Text)
        End If
        If IsNumeric(txtTimeR2.Text) Then
            oParam.timeR2 = CSng(txtTimeR2.Text)
        End If
        If IsNumeric(txtTimeP0.Text) Then
            oParam.timeP(0) = CSng(txtTimeP0.Text)
        End If
        If IsNumeric(txtTimeP1.Text) Then
            oParam.timeP(1) = CSng(txtTimeP1.Text)
        End If
        If IsNumeric(txtTimeP2.Text) Then
            oParam.timeP(2) = CSng(txtTimeP2.Text)
        End If
        If IsNumeric(txtTimeP3.Text) Then
            oParam.timeP(3) = CSng(txtTimeP3.Text)
        End If
        If IsNumeric(txtTimeZone.Text) Then
            oParam.timeZoneCoef = CSng(txtTimeZone.Text)
        End If
    End Sub

    Private Sub BtnAutoSet_Click(sender As Object, e As EventArgs) Handles BtnAutoSet.Click
        oParam.shiftValue()
        showParam()
    End Sub

    Private Sub go(ByVal param_lstbox As ListBox)
        Dim a As New AnaForm
        a.autoRun(txtFile.Text)
        LstRaceHeader.Items.Clear()
        For j As Integer = 0 To a.ListBox1.Items.Count - 1
            LstRaceHeader.Items.Add(a.ListBox1.Items(j))
        Next
        param_lstbox.Items.Clear()
        Dim sbf() As String = Split(a.autoModeResult, vbLf)
        param_lstbox.Items.Add("No:着順,人気,適合度")
        For j As Integer = 0 To sbf.Length - 1
            param_lstbox.Items.Add((j + 1).ToString & " : " & sbf(j))
        Next
        a.Close()
    End Sub

    Private Sub BtnGoStd_Click(sender As Object, e As EventArgs) Handles BtnGoStd.Click
        oParam.setDefValue()
        showParam()
        go(LstStdParam)
    End Sub


    Private Sub BtnGoTestParam_Click(sender As Object, e As EventArgs) Handles BtnGoTestParam.Click
        torikomi()
        go(LstTestParam)
    End Sub
End Class