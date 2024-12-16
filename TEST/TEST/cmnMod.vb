Imports System.IO

Module cmnMod

    Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)
    Private HR2Path As String = ""

    'データベースファイルや出馬表を置くフォルダーの取得
    Private Function GetHR2Path() As String
        If File.Exists("C:\STUDY\HR2\HR2.sqlite3") Then
            Return "C:\STUDY\HR2"
        ElseIf File.Exists("C:\学習\HR2\HR2.sqlite3") Then
            Return "C:\学習\HR2"
        Else
            Dim openFileDialog1 As New OpenFileDialog()
            openFileDialog1.InitialDirectory = "c:\"
            openFileDialog1.Filter = "sqlite files (*.sqlite3)|*.sqlite3|All files (*.*)|*.*"
            openFileDialog1.FilterIndex = 2
            openFileDialog1.RestoreDirectory = True

            If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Return Path.GetDirectoryName(openFileDialog1.FileName)
            End If
        End If
        Return ""
    End Function

    '出馬表のテキストファイルを置くフォルダー名の取得
    Public Function GetTextDataFolder() As String
        If HR2Path.Length = 0 Then
            HR2Path = GetHR2Path()
        End If
        Return HR2Path
    End Function

    Public Function GetSyutubahyoTextFileName(ByVal oHead As RaceHeaderClass) As String
        Dim fnm As String = oHead.dt.ToString("yyyy年M月d日") & "_" & oHead.race_name & ".Txt"
        Return Path.Combine(GetTextDataFolder(), "出馬表", fnm)
    End Function

    'データベース接続文字列
    Public Function GetDbConnectionString() As String
        If HR2Path.Length = 0 Then
            HR2Path = GetHR2Path()
        End If
        Return "Data Source=" & Path.Combine(HR2Path, "HR2.sqlite3")
    End Function

    '指定したキーワードを含む行をさがす
    Public Function SearchLineByKeyword(ByVal startpos As Integer, ByVal src As String, ByVal keyword As String, ByRef findpos As Integer) As String
        Dim ip As Integer
        ip = InStr(startpos, src, keyword)
        If ip > 0 Then
            findpos = ip - 1
            While findpos > 0
                If Mid(src, findpos, 1) = vbLf Then
                    findpos += 1
                    Exit While
                Else
                    findpos -= 1
                End If
            End While
            If findpos <= 0 Then
                findpos = 1
            End If
            ip = InStr(ip + 1, src, vbLf)
            If ip = 0 Then
                ip = src.Length
            End If
            Return Mid(src, findpos, ip - findpos)
        Else
            Return ""
        End If
    End Function

    'タグを除去する
    Public Function removeTagPair(ByVal linestr As String) As List(Of String)
        Dim p As New List(Of Integer) 'p()には'<'と'>'の位置が記録される
        Dim ip As Integer = 1
        Dim ip2 As Integer = 999
        While ip > 0 AndAlso ip2 > 0
            ip = InStr(ip, linestr, "<")
            If ip > 0 Then
                p.Add(ip)
                ip2 = InStr(ip + 1, linestr, ">")
                If ip2 > 0 Then
                    p.Add(ip2)
                    ip = ip2
                End If
            End If
        End While
        Dim ans As New List(Of String)
        If p.Count > 1 Then
            For j As Integer = 1 To p.Count - 2 Step 2
                ans.Add(Mid(linestr, p(j) + 1, p(j + 1) - p(j) - 1))
            Next
        Else
            ans.Add(linestr)
        End If
        Return ans
    End Function

    'タグ内に指定のキーワードを持つ要素の要素内容を取得する
    Function GetTagContents(ByVal linestr As String, ByVal keyword As String) As String
        Dim ip As Integer = InStr(linestr, keyword)
        If ip > 0 Then
            Dim ip2 As Integer = InStr(ip + 1, linestr, ">")
            If ip2 > 0 Then
                Dim ip3 As Integer = InStr(ip2 + 1, linestr, "<")
                If ip3 > 0 Then
                    Return Mid(linestr, ip2 + 1, ip3 - ip2 - 1)
                End If
            End If
        End If
        Return ""
    End Function

    'タグ名と属性名を指定して値を取得する
    Function GetAttributeValue(ByVal linestr As String, ByVal tagname As String, ByVal attributename As String) As String
        Dim ip As Integer = InStr(linestr, "<" & tagname)
        If ip > 0 Then
            Dim ip2 As Integer = InStr(ip + 1, linestr, attributename)
            If ip2 > 0 Then
                Dim ip3 As Integer = InStr(ip2 + 1, linestr, "=")
                If ip3 > 0 Then
                    Dim ip4 As Integer = InStr(ip3 + 1, linestr, """")
                    If ip4 > 0 Then
                        Dim ip5 As Integer = InStr(ip4 + 1, linestr, """")
                        If ip5 > 0 Then
                            Return Mid(linestr, ip4 + 1, ip5 - ip4 - 1)
                        End If
                    End If
                End If
            End If
        End If
        Return ""
    End Function

    'タグ名を指定して値を取得する
    Function GetTagValue(ByVal linestr As String, ByVal tagname As String, Optional ByVal jun As Integer = 1) As String
        Dim ip As Integer = 1
        While jun > 0
            ip = InStr(ip, linestr, "<" & tagname)
            If ip > 0 Then
                ip = ip + 1
            End If
            jun -= 1
        End While

        If ip > 0 Then
            Dim ip2 As Integer = InStr(ip + 1, linestr, ">")
            If ip2 > 0 Then
                Dim ip3 As Integer = InStr(ip2 + 1, linestr, "</" & tagname & ">")
                If ip3 > 0 Then
                    Return Mid(linestr, ip2 + 1, ip3 - ip2 - 1)
                End If
            End If
        End If
        Return ""
    End Function

    'タグの値をとりだす
    Public Function removeTagPair2(ByVal linestr As String) As List(Of String)
        Dim p As New List(Of Integer) 'p()には'>'と'<'の位置が記録される
        Dim ip As Integer = 1
        Dim ip2 As Integer = 999
        While ip > 0 AndAlso ip2 > 0
            ip = InStr(ip, linestr, ">")
            If ip > 0 Then
                p.Add(ip)
                ip2 = InStr(ip + 1, linestr, "<")
                If ip2 > 0 Then
                    p.Add(ip2)
                    ip = ip2
                End If
            End If
        End While
        Dim ans As New List(Of String)
        If p.Count > 1 Then
            For j As Integer = 0 To p.Count - 2 Step 2
                If p(j + 1) - p(j) = 1 Then
                    ans.Add("")
                Else
                    ans.Add(Mid(linestr, p(j) + 1, p(j + 1) - p(j) - 1))
                End If
            Next
        Else
            ans.Add(linestr)
        End If
        Return ans
    End Function

    '着順をscore値に変換
    Public Function cyakujun2score(ByVal cyakujun As Integer) As Integer
        Select Case cyakujun
            Case 1
                Return 10 ^ 6
            Case 2
                Return 10 ^ 4
            Case 3
                Return 10 ^ 2
            Case 4 To 18
                Return 1
            Case Else
                Return 0
        End Select
    End Function

End Module
