Module syutubaParseMod
    '出馬表の解析

    Public Function GetSyutuba(ByVal src As String, ByVal sblist As SyutubaListClass) As Integer
        sblist.init()
        Dim findpos As Integer = 1
        Dim lineStr As String = SearchLineByKeyword(findpos, src, "<tbody>", findpos)
        If lineStr.Length > 0 Then
            Dim endpos As Integer
            lineStr = SearchLineByKeyword(findpos + 1, src, "</tbody>", endpos)
            If lineStr.Length > 0 Then
                While findpos < endpos AndAlso findpos > 0
                    lineStr = SearchLineByKeyword(findpos + 1, src, """waku""", findpos)
                    If lineStr.Length > 0 Then
                        Dim a As New SyutubaClass
                        Dim imgname As String = GetAttributeValue(lineStr, "img", "src")
                        Dim ip As Integer = InStr(imgname, "waku/")
                        If ip > 0 Then
                            imgname = Replace(Mid(imgname, ip + 5), ".png", "")
                            If IsNumeric(imgname) Then
                                a.waku = CShort(imgname)
                            End If
                        End If

                        Dim words As List(Of String)
                        lineStr = SearchLineByKeyword(findpos + 1, src, """num""", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If IsNumeric(words(0)) Then
                                    a.umaban = CInt(words(0))
                                End If
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, "a href", findpos)
                        If lineStr.Length > 0 Then
                            a.bamei = GetTagValue(lineStr, "a")
                            a.href = GetAttributeValue(lineStr, "a", "href")
                        End If

                        lineStr = SearchLineByKeyword(findpos + 1, src, """trainer""", findpos)
                        If lineStr.Length > 0 Then
                            a.cyokyosi = GetTagValue(lineStr, "a")
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, """age""", findpos)
                        If lineStr.Length > 0 Then
                            Dim jun As Integer
                            If a.waku > 0 Then
                                jun = 2
                            Else
                                jun = 1
                            End If
                            Dim tmp_word As String = GetTagValue(lineStr, "p", jun)
                            If tmp_word.Length > 0 Then
                                If InStr(tmp_word, "牝") > 0 Then
                                    a.sex = "牝"
                                ElseIf InStr(tmp_word, "牡") > 0 Then
                                    a.sex = "牡"
                                ElseIf InStr(tmp_word, "せん") > 0 Then
                                    a.sex = "せん"
                                End If
                                Dim age As String = Replace(tmp_word, a.sex, "")
                                Dim sp As Integer = InStr(age, "/")
                                If sp > 0 Then
                                    age = Left(age, sp - 1)
                                End If
                                If IsNumeric(age) Then
                                    a.age = CInt(age)
                                End If
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, "<span>kg</span>", findpos)
                        If lineStr.Length > 0 Then
                            Dim j As Integer = InStr(lineStr, "<span>kg</span>")
                            lineStr = Left(lineStr, j - 1)
                            If IsNumeric(lineStr) Then
                                a.hutan = CSng(lineStr)
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, """jockey""", findpos)
                        If lineStr.Length > 0 Then
                            lineStr = SearchLineByKeyword(findpos + 1, src, "a href", findpos)
                            If lineStr.Length > 0 Then
                                a.kisyu = GetTagContents(lineStr, "href")
                            End If
                        End If

                        sblist.add1(a)
                    Else
                        Exit While
                    End If
                End While
            End If
        End If
        Return sblist.cnt
    End Function

End Module
