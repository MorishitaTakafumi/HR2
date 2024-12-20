Imports System.Data.SQLite

Public Class SelectParamForm
    'パラメータ選択フォーム

    Public Property SaveFlag As Boolean = False

    Public Sub entry()
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT * FROM Param"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    LstParams.Items.Add(r("id") & ":" & r("remarks"))
                End While
                r.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
        ShowDialog()
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        If LstParams.SelectedIndex >= 0 Then
            Dim ss As String = LstParams.SelectedItem.ToString
            Dim ip As Integer = InStr(ss, ":")
            If ip > 0 Then
                Dim id As Integer = CInt(ss.Substring(0, ip - 1))
                Dim errmsg As String = oParam.load(id)
                If errmsg.Length > 0 Then
                    MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
                End If
                SaveFlag = True
                Close()
            End If
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Close()
    End Sub

    Private Sub LstParams_DoubleClick(sender As Object, e As EventArgs) Handles LstParams.DoubleClick
        BtnOK.PerformClick()
    End Sub

    Private Sub BtnSelectDefv_Click(sender As Object, e As EventArgs) Handles BtnSelectDefv.Click
        oParam.setDefValue()
        SaveFlag = True
        Close()
    End Sub
End Class