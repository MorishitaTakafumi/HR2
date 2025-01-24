Public Class SelectTukiForm
    '月の選択フォーム

    Public Property SaveFlag As Boolean = False
    Public Property SelectedTukiText As String

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub entry(ByVal defTuki As String, ByVal sx As Integer, ByVal sy As Integer)
        Me.Location = New Point(sx, sy)
        If defTuki.Length > 0 Then
            Dim sbf() As String = Split(defTuki, ",")
            For Each txt As String In sbf
                If IsNumeric(txt) AndAlso CInt(txt) >= 1 AndAlso CInt(txt) <= 12 Then
                    LstTuki.SetItemChecked(CInt(txt) - 1, True)
                Else
                    Exit For
                End If
            Next
        End If
        ShowDialog()
    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Dim TmpText As String = ""
        For j As Integer = 0 To LstTuki.Items.Count - 1
            If LstTuki.GetItemChecked(j) Then
                TmpText &= (j + 1).ToString & ","
            End If
        Next
        If TmpText.Length > 0 Then
            SelectedTukiText = TmpText.Substring(0, TmpText.Length - 1)
        Else
            SelectedTukiText = ""
        End If
        SaveFlag = True
        Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        SaveFlag = False
        Close()
    End Sub

    Private Sub BtnAllOff_Click(sender As Object, e As EventArgs) Handles BtnAllOff.Click
        For j As Integer = 0 To LstTuki.Items.Count - 1
            LstTuki.SetItemChecked(j, False)
        Next
    End Sub

End Class