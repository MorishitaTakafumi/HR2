Module resultParseMod
    'レース結果の解析

    '開催日と競馬場名の取得
    Public Function GetWhenWhere(ByVal src As String, ByRef dt As Date) As String
        Dim JoIdx As Integer = -1
        Dim findpos As Integer = 1
        Do
            Dim lineStr As String = SearchLineByKeyword(findpos, src, "cell date", findpos)
            If lineStr.Length > 0 Then
                Dim words As List(Of String) = removeTagPair(lineStr)
                For j As Integer = 0 To JoMei.Length - 1
                    For i As Integer = 0 To words.Count - 1
                        If JoIdx = -1 AndAlso InStr(words(i), JoMei(j)) > 0 Then
                            JoIdx = j
                        End If
                        Dim ip As Integer = InStr(words(i), "日")
                        If ip > 0 Then
                            Dim ss As String = Mid(words(i), 1, ip)
                            If IsDate(ss) Then
                                dt = CDate(ss)
                            End If
                        End If
                    Next
                Next
            Else
                Exit Do
            End If
        Loop While JoIdx = -1
        If JoIdx > 0 Then
            Return JoMei(JoIdx)
        Else
            Return ""
        End If
    End Function

    'レース名とグレードの取得
    Public Function GetRaceName(ByVal src As String, ByRef grade As String) As String
        Dim findpos As Integer = 1
        Dim lineStr As String = SearchLineByKeyword(findpos, src, "race_name", findpos)
        If lineStr.Length > 0 Then
            Dim racename As String = GetTagContents(lineStr, "race_name")
            If InStr(lineStr, "grade_icon") > 0 Then
                Dim imgname As String = GetAttributeValue(lineStr, "img", "src")
                If InStr(imgname, "grade_g3", CompareMethod.Text) > 0 Then
                    grade = "G3"
                ElseIf InStr(imgname, "grade_g2", CompareMethod.Text) > 0 Then
                    grade = "G2"
                ElseIf InStr(imgname, "grade_g1", CompareMethod.Text) > 0 Then
                    grade = "G1"
                End If
            End If
            Return racename
        Else
            Return ""
        End If
    End Function

    'クラス・距離・種別の取得
    Public Function GetClassCource(ByVal src As String, ByRef distance As Integer, ByRef GorD As String) As String
        Dim findpos As Integer = 1
        Dim lineStr As String = SearchLineByKeyword(findpos, src, "cell class", findpos)
        If lineStr.Length > 0 Then
            Dim words As List(Of String) = removeTagPair(lineStr)
            If words.Count > 0 Then
                Dim classname As String = words(0)
                lineStr = SearchLineByKeyword(findpos + 1, src, "コース：", findpos)
                If lineStr.Length > 0 Then
                    words = removeTagPair(lineStr)
                    For j As Integer = 0 To words.Count - 1
                        If IsNumeric(words(j)) Then
                            distance = CInt(words(j))
                        End If
                        If InStr(words(j), "芝") > 0 Then
                            GorD = "芝"
                            If InStr(words(j), "外") > 0 Then
                                GorD &= "外"
                            End If
                        ElseIf InStr(words(j), "ダート") > 0 Then
                            GorD = "ダート"
                        End If
                    Next
                End If
                Return classname
            End If
        End If
        Return ""
    End Function

    '結果表の取得
    Public Function GetKekka(ByVal src As String, ByVal klist As KekkaListClass) As Integer
        klist.init()
        Dim findpos As Integer = 1
        Dim lineStr As String = SearchLineByKeyword(findpos, src, "<tbody>", findpos)
        If lineStr.Length > 0 Then
            Dim endpos As Integer
            lineStr = SearchLineByKeyword(findpos + 1, src, "</tbody>", endpos)
            If lineStr.Length > 0 Then
                While findpos < endpos AndAlso findpos > 0
                    lineStr = SearchLineByKeyword(findpos + 1, src, """place""", findpos)

                    If findpos > endpos Then
                        Exit While
                    End If

                    Dim words As List(Of String) = removeTagPair(lineStr)
                    If words.Count > 0 Then
                        Dim a As New KekkaClass
                        a.cyakujun = cyakujunEncode(words(0))
                        lineStr = SearchLineByKeyword(findpos + 1, src, """num""", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If IsNumeric(words(0)) Then
                                    a.umaban = CInt(words(0))
                                Else
                                    a.umaban = 9999
                                End If
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, "a href", findpos)
                        If lineStr.Length > 0 Then
                            a.bamei = GetTagValue(lineStr, "a")
                            'words = removeTagPair(lineStr)
                            'If words.Count > 0 Then
                            '    a.bamei = words(0)
                            'End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, """age""", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If InStr(words(0), "牝") > 0 Then
                                    a.sex = "牝"
                                ElseIf InStr(words(0), "牡") > 0 Then
                                    a.sex = "牡"
                                ElseIf InStr(words(0), "せん") > 0 Then
                                    a.sex = "せん"
                                End If
                                Dim age As String = Replace(words(0), a.sex, "")
                                If IsNumeric(age) Then
                                    a.age = CInt(age)
                                End If
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, """weight""", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If IsNumeric(words(0)) Then
                                    a.hutan = CSng(words(0))
                                End If
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, """jockey""", findpos)
                        If lineStr.Length > 0 Then
                            If InStr(lineStr, "href") > 0 Then
                                a.kisyu = GetTagContents(lineStr, "href")
                            Else
                                a.kisyu = GetTagContents(lineStr, "td")
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, """time""", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                a.tokei = cnvTimeStr2Sec(words(0))
                            End If
                        End If

                        For corner As Integer = 1 To 4
                            lineStr = SearchLineByKeyword(findpos + 1, src, """" & corner.ToString & "コーナー通過順位""", findpos)
                            If lineStr.Length > 0 Then
                                words = removeTagPair(lineStr)
                                If words.Count > 0 Then
                                    If IsNumeric(words(0)) Then
                                        a.tukajun(corner - 1) = CShort(words(0))
                                    End If
                                End If
                            End If
                        Next

                        lineStr = SearchLineByKeyword(findpos + 1, src, """f_time""", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If IsNumeric(words(0)) Then
                                    a.agari = CSng(words(0))
                                End If
                            End If
                        End If

                        lineStr = SearchLineByKeyword(findpos + 1, src, """h_weight""", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If IsNumeric(words(0)) Then
                                    a.w = CSng(words(0))
                                End If
                                If words.Count > 1 Then
                                    Dim zogen As String = Replace(Replace(words(1), "(", ""), ")", "")
                                    If IsNumeric(zogen) Then
                                        a.zogen = CSng(zogen)
                                    End If
                                End If
                            End If
                        End If
                        lineStr = SearchLineByKeyword(findpos + 1, src, "a href", findpos)
                        If lineStr.Length > 0 Then
                            a.cyokyosi = GetTagContents(lineStr, "href")
                        End If

                        lineStr = SearchLineByKeyword(findpos + 1, src, """pop""", findpos)
                        If lineStr.Length > 0 Then
                            words = removeTagPair(lineStr)
                            If words.Count > 0 Then
                                If IsNumeric(words(0)) Then
                                    a.ninki = CSng(words(0))
                                End If
                            End If
                        End If

                        klist.add1(a)
                    Else
                        Exit While
                    End If
                End While
            End If
        End If
        Return klist.cnt
    End Function

End Module
