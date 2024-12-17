Public Class SelectJoForm
    '競馬場の選択フォーム

    Public Property SaveFlag As Boolean = False
    Public Property SelectedJoText As String

    Public Shared Function JoText2JoCode(ByVal JoText As String) As String
        Dim ss As String = JoText.Trim
        If ss.Length > 0 Then
            For j As Integer = 0 To JoMei.Length - 1
                ss = ss.Replace(JoMei(j), j.ToString)
            Next
            Return ss
        Else
            Return ""
        End If
    End Function

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub entry(ByVal defJoText As String, ByVal sx As Integer, ByVal sy As Integer)
        Me.Location = New Point(sx, sy)
        With LstJo
            .Items.Clear()
            For j As Integer = 0 To JoMei.Length - 1
                .Items.Add(JoMei(j))
                If defJoText.Length > 0 AndAlso InStr(defJoText, JoMei(j)) > 0 Then
                    .SetItemChecked(j, True)
                End If
            Next
        End With
        ShowDialog()
    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Dim TmpText As String = ""
        For j As Integer = 0 To LstJo.Items.Count - 1
            If LstJo.GetItemChecked(j) Then
                TmpText &= LstJo.Items(j).ToString & ","
            End If
        Next
        If TmpText.Length > 0 Then
            SelectedJoText = TmpText.Substring(0, TmpText.Length - 1)
        Else
            SelectedJoText = ""
        End If
        SaveFlag = True
        Close()
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        SaveFlag = False
        Close()
    End Sub

    Private Sub BtnAllOff_Click(sender As Object, e As EventArgs) Handles BtnAllOff.Click
        For j As Integer = 0 To LstJo.Items.Count - 1
            LstJo.SetItemChecked(j, False)
        Next
    End Sub
End Class