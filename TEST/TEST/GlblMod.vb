Module GlblMod
    Public JoMei() As String = {"札幌", "函館", "福島", "新潟", "中山", "東京", "中京", "京都", "阪神", "小倉"}

    Public Function cnvTimeStr2Sec(ByVal strTime As String) As Single
        Dim ip As Integer = InStr(strTime, ":")
        If ip > 0 Then
            Dim hun As Integer = CInt(Mid(strTime, 1, ip - 1))
            Dim ip2 As Integer = InStr(ip, strTime, ".")
            If ip2 > 0 Then
                Dim byo As Single = CSng(Mid(strTime, ip + 1, ip2 - ip - 1))
                byo += CSng(Mid(strTime, ip2 + 1)) * 0.1
                Return hun * 60 + byo
            End If
        End If
        Return 0
    End Function


End Module
