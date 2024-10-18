Imports System.Data.SQLite

Public Class WinRateForm
    Private Sub BtnWinRate_Click(sender As Object, e As EventArgs) Handles BtnWinRate.Click
        'ListBox.Items.Clear()
        'Dim errmsg As String = ""
        'Using conn As New SQLiteConnection(GetDbConnectionString)
        '    Dim cmd As SQLiteCommand = conn.CreateCommand
        '    conn.Open()
        '    Try
        '        cmd.CommandText = "SELECT R.* FROM RaceHeader R INNER JOIN AnaVal A ON R.id=A.rhead_id WHERE "
        '        Dim sql As String = ""
        '        sql &= "R.jo_code=@jo_code"
        '        cmd.Parameters.AddWithValue("@jo_code", GetKeibajoCode(CbJo.Text))

        '        Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader

        '    Catch ex As Exception
        '        errmsg = ex.Message
        '    End Try
        'End Using
        'If errmsg.Length > 0 Then
        '    MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        'End If
    End Sub
End Class