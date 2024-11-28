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
                lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "競走馬情報", findpos)
                If lineStr.Length > 0 Then
                    Dim words As List(Of String) = removeTagPair2(lineStr)
                    Dim flg As Boolean = False
                    For i As Integer = 0 To words.Count - 1
                        If InStr(words(i), "競走馬情報") > 0 Then
                            flg = True
                        ElseIf words(i).Trim.Length > 0 AndAlso flg Then
                            oHeader.bamei = words(i)
                            Exit For
                        End If
                    Next
                    lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "父", findpos)
                    If lineStr.Length > 0 Then
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<dd>", findpos)
                        lineStr = GetTagValue(lineStr, "dd")
                        If InStr(lineStr, "href") > 0 Then
                            oHeader.father = GetTagValue(lineStr, "a")
                        Else
                            oHeader.father = lineStr
                        End If
                    End If
                    lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "性別", findpos)
                    If lineStr.Length > 0 Then
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<dd>", findpos)
                        lineStr = GetTagValue(lineStr, "dd")
                        oHeader.seibetu = lineStr
                    End If

                    lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "母", findpos)
                    If lineStr.Length > 0 Then
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<dd>", findpos)
                        lineStr = GetTagValue(lineStr, "dd")
                        If InStr(lineStr, "href") > 0 Then
                            oHeader.mother = GetTagValue(lineStr, "a")
                        Else
                            oHeader.mother = lineStr
                        End If
                    End If
                    lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "生年月日", findpos)
                    If lineStr.Length > 0 Then
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<dd>", findpos)
                        lineStr = GetTagValue(lineStr, "dd")
                        If IsDate(lineStr) Then
                            oHeader.birthday = CDate(lineStr)
                        End If
                    End If

                End If
            End If
        End If
        Return oHeader
    End Function

    '履歴表の取得
    Public Function GetUmaHist(ByVal src As String, ByVal hlist As umaHistListClass, ByVal dt_max As Date) As Integer
        Dim findpos As Integer = 1
        Dim lineStr As String = SearchLineByKeyword(findpos, src, "<tbody>", findpos)
        If lineStr.Length > 0 Then
            Dim endpos As Integer
            lineStr = SearchLineByKeyword(findpos + 1, src, "</tbody>", endpos)
            If lineStr.Length > 0 Then
                While findpos < endpos AndAlso findpos > 0
                    lineStr = SearchLineByKeyword(findpos + 1, src, """date""", findpos)
                    If lineStr.Length > 0 Then
                        Dim a As New UmaHistClass
                        Dim words As List(Of String) = removeTagPair(lineStr)
                        If words.Count > 0 Then
                            If IsDate(words(0)) Then
                                a.dt = CDate(words(0))
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td>", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                a.keibajo = words(0)
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, """race""", findpos)
                        If lineStr.Length > 0 Then
                            If InStr(lineStr, "href") > 0 Then
                                a.racename = GetTagValue(lineStr, "a")
                                a.href = GetAttributeValue(lineStr, "a", "href")
                            Else
                                words = removeTagPair(lineStr)
                                If words.Count > 0 Then
                                    a.racename = words(0)
                                End If
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td>", findpos)
                        words = removeTagPair(lineStr)
                        If words.Count > 0 Then
                            If InStr(words(0), "芝ダ") > 0 Then
                                a.syubetu = "芝ダ"
                            ElseIf InStr(words(0), "芝") > 0 Then
                                a.syubetu = "芝"
                            ElseIf InStr(words(0), "ダ") > 0 Then
                                a.syubetu = "ダ"
                            End If
                            words(0) = Replace(words(0), a.syubetu, "")
                            If IsNumeric(words(0)) Then
                                a.distance = CInt(words(0))
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td>", findpos)
                        words = removeTagPair(lineStr)
                        If words.Count > 0 Then
                            a.baba = words(0)
                        End If
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td>", findpos)
                        words = removeTagPair(lineStr)
                        If words.Count > 0 Then
                            If IsNumeric(words(0)) Then
                                a.tosu = CInt(words(0))
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td>", findpos)
                        words = removeTagPair(lineStr)
                        If words.Count > 0 Then
                            If IsNumeric(words(0)) Then
                                a.ninki = CInt(words(0))
                            End If
                        End If

                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td", findpos)
                        words = removeTagPair(lineStr)
                        If words.Count > 0 Then
                            a.cyakujun = cyakujunEncode(words(0))
                        End If

                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, """jockey""", findpos)
                        If lineStr.Length > 0 Then
                            If InStr(lineStr, "href") > 0 Then
                                a.kisyu = GetTagValue(lineStr, "a")
                            Else
                                words = removeTagPair(lineStr)
                                If words.Count > 0 Then
                                    a.kisyu = words(0)
                                End If
                            End If
                        End If

                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td>", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If IsNumeric(words(0)) Then
                                    a.hutan = CSng(words(0))
                                End If
                            End If
                        End If

                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td>", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If IsNumeric(words(0)) Then
                                    a.w = CSng(words(0))
                                End If
                            End If
                        End If

                        lineStr = SearchLineByKeyword(findpos + lineStr.Length, src, "<td>", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                a.tokei = cnvTimeStr2Sec(words(0))
                            End If
                        End If
                        If a.dt < dt_max OrElse dt_max = DMY_DATE Then
                            hlist.add1(a)
                        End If
                    Else
                        Exit While
                    End If

                End While
            End If
        End If
        Return hlist.cnt
    End Function

End Module
