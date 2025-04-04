Imports System.Net.Http
Imports System.Text
Imports System.IO

Module WebMod

    Private WebPageAccessCount As Integer

    Public Sub ClearWebPageAccessCounter()
        WebPageAccessCount = 0
    End Sub

    Public Sub showWebPageAccessCounter()
        MsgBox($"WebPageアクセス回数は{WebPageAccessCount }です")
    End Sub

    Public Function GetWebPageAccessCounter() As String
        Return $"WebPageアクセス回数は{WebPageAccessCount }です"
    End Function

    Public Function GetWebPageText(ByVal url As String) As String
        Dim client As New HttpClient()
        url = makeJRAurl(url)
        ' 非同期でデータを取得するタスクを実行
        Dim task As Task(Of Byte()) = client.GetByteArrayAsync(url)
        task.Wait() ' タスクが完了するまで待機

        ' 結果をバイト配列として取得
        Dim byteArray As Byte() = task.Result

        ' Shift-JISでデコード
        Dim result As String = Encoding.GetEncoding("Shift_JIS").GetString(byteArray)
        WebPageAccessCount += 1
        Return result
    End Function

    Public Function makeJRAurl(ByVal url As String) As String
        If url.Length > 0 AndAlso InStr(url, "https://www.jra.go.jp") = 0 Then
            url = "https://www.jra.go.jp" & url
        End If
        Return url
    End Function

    Public Sub Clipboard2URL(ByVal txtbox As TextBox)
        If Clipboard.ContainsText Then
            Dim tmp As String = makeJRAurl(Clipboard.GetText)
            If InStr(tmp, "https") Then
                txtbox.Text = tmp
            End If
        End If
    End Sub

End Module
