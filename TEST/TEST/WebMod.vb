Imports System.Net.Http
Imports System.Text
Imports System.IO

Module WebMod

    Public Function GetWebPageText(ByVal url As String) As String
        Dim client As New HttpClient()

        ' 非同期でデータを取得するタスクを実行
        Dim task As Task(Of Byte()) = client.GetByteArrayAsync(url)
        task.Wait() ' タスクが完了するまで待機

        ' 結果をバイト配列として取得
        Dim byteArray As Byte() = task.Result

        ' Shift-JISでデコード
        Dim result As String = Encoding.GetEncoding("Shift_JIS").GetString(byteArray)
        Return result
    End Function

    Public Function makeJRAurl(ByVal url As String) As String
        If url.Length > 0 AndAlso InStr(url, "https://www.jra.go.jp") = 0 Then
            url = "https://www.jra.go.jp" & url
        End If
        Return url
    End Function

    Public Sub test(ByVal url As String)
        ' HttpClientのインスタンスを作成
        Dim client As New HttpClient()

        ' 非同期でデータを取得するタスクを実行
        Dim task As Task(Of Byte()) = client.GetByteArrayAsync(url)
        task.Wait() ' タスクが完了するまで待機

        ' 結果をバイト配列として取得
        Dim byteArray As Byte() = task.Result

        ' Shift-JISでデコード
        Dim result As String = Encoding.GetEncoding("Shift_JIS").GetString(byteArray)

        ' ファイルパスを指定
        Dim filePath As String = "C:\TMP\TEST.txt"

        ' テキストファイルに保存
        File.WriteAllText(filePath, result, Encoding.GetEncoding("Shift_JIS"))

        ' 終了メッセージ
        Console.WriteLine("データが保存されました: " & filePath)
    End Sub

End Module
