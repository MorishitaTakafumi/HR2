Module umaInfoParseMode
    '競走馬情報の解析

    'ヘッダーの取得
    Public Function GetUmaHeader(ByVal src As String) As UmaHeaderClass
        Dim oHeader As New UmaHeaderClass
        Dim findpos As Integer = 1
        Dim lineStr As String = SearchLineByKeyword(findpos, src, "競走馬情報", findpos)
        If lineStr.Length > 0 Then
            lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "競走馬情報", findpos)
            If lineStr.Length > 0 Then
                Dim words As List(Of String) = removeTagPair(lineStr)
                For i As Integer = 0 To words.Count - 1
                    If InStr(words(i), "競走馬情報") > 0 Then
                        oHeader.bamei = words(i + 1)
                        Exit For
                    End If
                Next
            End If
        End If
        Return oHeader
    End Function

End Module
