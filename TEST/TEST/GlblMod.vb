Public Module GlblMod
    Public Const DMY_DATE As Date = #1900/1/1#
    Public Const DMY_VAL As Integer = -9999

    Public JoMei() As String = {"札幌", "函館", "福島", "新潟", "中山", "東京", "中京", "京都", "阪神", "小倉"}
    Public RaceSyubetuMei() As String = {"", "芝", "ダート", "障害"}
    Public oTC As New TimeCorrectionClass 'クラス間のタイム補正用
    Public oParam As New ParamSetClass '適合度計算用のパラメータ
    Public UmaStore As New UmaStoreClass '馬情報のキャッシュ
    Public kekkaStore As New KekkaStoreClass 'レース結果のキャッシュ

    'レース名の照合
    Public Function IsRaceNameMatch(ByVal fullName As String, ByVal shortname As String) As Boolean
        If shortname.Trim.Length = 0 Then
            Return False
        End If
        If fullName = shortname Then
            Return True
        End If

        Dim ip1 As Integer = InStr(fullName, "勝クラス")
        If ip1 > 0 Then
            Dim ip2 As Integer = InStr(shortname, "勝クラス")
            If ip2 > 0 Then
                If fullName.Substring(ip1 - 2, 1) = shortname.Substring(ip2 - 2, 1) Then '勝数比較
                    If fullName.Substring(0, 1) = shortname.Substring(0, 1) Then '歳比較
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End If


        Dim front3c As String
        If shortname.Length > 3 Then
            front3c = shortname.Substring(0, 3)
        ElseIf shortname.Length = 3 Then
            If shortname.Substring(2, 1).ToUpper = "S" Then
                front3c = shortname.Substring(0, 2) & "ス"
            Else
                front3c = shortname
            End If
        Else
            front3c = shortname
        End If
        If InStr(fullName, front3c) > 0 Then
            Return True
        Else
            If InStr(shortname, fullName) > 0 Then '農林水産省章典○○みたいな名前は昔は○○だけだった
                Return True
            Else
                If InStr(fullName, "セントライト記念") > 0 AndAlso InStr(shortname, "セントライト記念") Then
                    Return True '冠名が無い／朝日杯／ラジオ日本賞の3通りある
                Else
                    Return False
                End If
            End If
        End If
    End Function

    Public Function GetKeibajoCode(ByVal keibajo As String) As Short
        For j As Integer = 0 To JoMei.Length - 1
            If InStr(keibajo, JoMei(j)) > 0 Then
                Return j
            End If
        Next
        Return -1
    End Function

    Public Function GetKeibajoName(ByVal jo_code As Short) As String
        If jo_code >= 0 AndAlso jo_code < JoMei.Length Then
            Return JoMei(jo_code)
        Else
            Return ""
        End If
    End Function

    '種別名を種別コードに変換
    Public Function GetTypeCode(ByVal typename As String) As Short
        If InStr(typename, "芝") > 0 Then
            Return 1
        ElseIf InStr(typename, "障害") > 0 Then
            Return 3
        Else
            Return 2
        End If
    End Function

    Public Function GetRaceTypeName(ByVal type_code As Short) As String
        If type_code >= 0 AndAlso type_code < RaceSyubetuMei.Length Then
            Return RaceSyubetuMei(type_code)
        Else
            Return ""
        End If
    End Function

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

    Public Function cyakujunEncode(ByVal strCyakujun As String) As Integer
        If IsNumeric(strCyakujun) Then
            Return CInt(strCyakujun)
        ElseIf InStr(strCyakujun, "除外") > 0 Then
            Return -998
        ElseIf InStr(strCyakujun, "中止") > 0 Then
            Return -999
        ElseIf InStr(strCyakujun, "取消") > 0 Then
            Return -997
        Else
            Return -9999
        End If

    End Function

    Public Function cyakujunDecode(ByVal cyakujun As Integer) As String
        Select Case cyakujun
            Case -998
                Return "除外"
            Case -999
                Return "中止"
            Case -997
                Return "取消"
            Case 1 To 18
                Return cyakujun.ToString
            Case Else
                Return ""
        End Select
    End Function

    Public Function cnvScoreStr2Val(ByVal strScore As String) As Integer
        If IsNumeric(strScore) Then
            Return CInt(strScore)
        End If

        Dim ip As Integer = InStr(strScore, "-")
        If ip > 0 Then
            Dim sbf() As String = Split(strScore, "-")
            If sbf.Length = 4 Then
                Try
                    Dim sv As Integer = 0
                    For j As Integer = 0 To 3
                        sv += sbf(j) * 100 ^ (3 - j)
                    Next
                    Return sv
                Catch ex As Exception
                    Return 0
                End Try
            Else
                Return 0
            End If
        End If
        Return 0
    End Function

    '上差と着差を取得する
    'Return 上がり差
    Public Function cnvAgarisaStr2Val(ByVal strScore As String, ByRef cyakusa As Single) As Single
        If strScore Is Nothing OrElse strScore.Length = 0 Then
            Return DMY_VAL
        End If

        If InStr(strScore, "[") > 0 Then '異種レースは対象外とする
            Return DMY_VAL
        End If

        strScore = Replace(Replace(strScore, "[", ""), "]", "")

        Dim ip As Integer = InStr(strScore, "(")
        If ip > 1 Then
            Dim agarisa As Single = CSng(strScore.Substring(0, ip - 1))
            cyakusa = CSng(Replace(strScore.Substring(ip), ")", ""))
            Return agarisa
        End If
        Return DMY_VAL
    End Function

    'spanScoreの適合度を得点化する
    'Return 得点(0～200)
    Public Function GetDegreeOfFit_spanScore(ByVal myScore As Integer, ByVal cmpScore As Integer) As Integer
        'spanScoreの適合度
        '1着,2着,3着,4着以下の４区分で出走馬myScoreと比較対象馬cmpScoreの得点を比較して得点化する
        '各区分で｛ ①0回もない　②1回ある　③2回以上ある }に分けて比較する
        '  ①　  ②　 ③ ←cmpScoreとする
        '①R00　  0    0 
        '②  0  R11   R1
        '③  0   R2  R22
        '
        '各着での完全適合時の得点を P1, P2, P3, P4 とする
        '
        '（例）myScore：1-0-0-3, cmpScore:2-1-0-1 ならば myScore：②①①③, cmpScore:③②①① である
        '1着：②vs③だからP1*R1、2着：①vs②だからP2*0、3着：①vs①だからP3*1、4着以下：③vs①だからP4*R2
        'よって得点は P1*R1+P2*0+P3*1+P4*R2 となる

        'R00, R11, R22, R1, R2, P1, P2, P3, P4 はパラメータ化したい
        Dim R00 As Single = 0.6
        Dim R11 As Single = 1
        Dim R22 As Single = 0.9
        Dim R1 As Single = 0.8
        Dim R2 As Single = 0.8
        Dim P() As Integer = {40, 45, 55, 60} '合計200にする
        Dim psum As Integer = 0
        For j As Integer = 0 To 3 '何着か （注）4着以下,3着,2着,1着の順になっている
            Dim myp As Integer = (myScore \ (100 ^ j)) Mod 100
            Dim cmpp As Integer = (cmpScore \ (100 ^ j)) Mod 100
            If j = 0 Then
                '4着以下はマイナス要素なので別ロジックとする
                If myp = 0 Then
                    If myScore > 0 Then '3着以上しかない場合はプラス要素
                        If cmpp = 0 Then
                            If cmpScore > 0 Then
                                psum += P(j) * R00 '相手も3着以上のみ
                            Else
                                psum += P(j) * R00 * 0.5 '相手は0-0-0-0
                            End If
                        Else
                            psum += 0 '相手は4着以下あり、このときはマイナス要素としない
                        End If
                    Else '0-0-0-0
                        If cmpp = 0 Then
                            If cmpScore > 0 Then
                                psum += 0 '相手は3着以上しかない、このときはマイナス要素としない
                            Else
                                psum += 0 '相手も0-0-0-0、このときはマイナス要素としない
                            End If
                        Else
                            psum += 0 '相手は4着以下あり、このときはマイナス要素としない
                        End If
                    End If
                Else '4着以下あり
                    If cmpp = 0 Then
                        If cmpScore > 0 Then
                            psum -= P(j) * R00 '相手は3着以上のみ、このときマイナス要素とする
                        Else
                            psum -= P(j) * R00 * 0.5 '相手は0-0-0-0
                        End If
                    Else '相手も4着以下あり
                        If myp = cmpp Then
                            psum += 0 '同じ回数、このときはマイナス要素としない
                        ElseIf myp > cmpp Then
                            psum -= P(j) * R00 '相手より多い、このときマイナス要素とする
                        Else
                            psum -= 0 '相手より少ない、このときマイナス要素としない
                        End If
                    End If
                End If
            Else
                Select Case cmpp
                    Case 0 '①
                        If myp = 0 Then
                            psum += P(j) * R00
                        End If
                    Case 1 '②
                        If myp = 1 Then
                            psum += P(j) * R11
                        ElseIf myp > 1 Then
                            psum += P(j) * R2
                        End If
                    Case Else '③
                        If myp = 1 Then
                            psum += P(j) * R1
                        ElseIf myp > 1 Then
                            psum += P(j) * R22
                        End If
                End Select
            End If
        Next
        Return Int(psum)
    End Function

    'spanScoreによる係数
    'Return 係数
    Public Function GetSpanScoreCoefficient(ByVal myScore As Integer) As Double
        '1着,2着,3着,4着以下の４区分で
        'Σ各着の回数×係数（パラメータ）
        Dim psum As Double = 1
        For j As Integer = 0 To 3 '何着か （注）4着以下,3着,2着,1着の順になっている
            Dim myp As Integer = (myScore \ (100 ^ j)) Mod 100
            psum += myp * oParam.scoreP(j)
        Next
        Return psum
    End Function

    'dateScoreによる係数
    'Return 係数
    Public Function GetDateScoreCoefficient(ByVal myScore As Integer) As Double
        '1着,2着,3着,4着以下の４区分で
        'Σ各着の回数×係数（パラメータ）
        Dim psum As Double = 1
        For j As Integer = 0 To 3 '何着か （注）4着以下,3着,2着,1着の順になっている
            Dim myp As Integer = (myScore \ (100 ^ j)) Mod 100
            psum += myp * oParam.scoreP2(j)
        Next
        Return psum
    End Function

    '上り差／着差の適合度を得点化する
    'Return 得点(0～100)
    Public Function GetDegreeOfFit_time(ByVal myTime As Single, ByVal cmpTime As Single, ByVal soumaeV As Integer) As Integer
        '上り差／着差の適合度
        '
        '出走馬myTimeと比較対象馬cmpTimeのゾーンの一致度を得点化する
        '
        'myTimeのゾーン=a, cmpTimeのゾーン=b として
        '(a-b)=0　のとき 1
        '(a-b)<0　のとき (a-b)*係数（パラメータ）
        '(a-b)>0　のとき (a-b)*係数（パラメータ）
        '
        'さらにそれが何走前かでも重みづけの係数（パラメータ）を掛ける
        '
        If myTime = DMY_VAL OrElse cmpTime = DMY_VAL Then
            Return 0
        End If

        Dim fullPoint As Integer = 100 '満点
        Dim myZone As Integer = GetTimeZoneN(myTime, 0.2, 24) ' GetTimeZone12(myTime)
        Dim cmpZone As Integer = GetTimeZoneN(cmpTime, 0.2, 24) 'GetTimeZone12(cmpTime)
        Dim coef As Single

        If myZone = cmpZone Then
            coef = 1
        ElseIf myZone < cmpZone Then
            coef = 1 - (myZone - cmpZone) * oParam.timeR1
        Else
            coef = 1 - (myZone - cmpZone) * oParam.timeR2
        End If
        Return fullPoint * oParam.timeP(soumaeV) * coef * GetTimeZoneCoef(myZone)
    End Function

    'ゾーンに応じた係数
    '優秀なタイムに多く得点を与えたいので最優秀ゾーンの係数を1としてゾーンが落ちるほど係数は小さくする
    Private Function GetTimeZoneCoef(ByVal myZone As Integer) As Single
        Return (1 - myZone * oParam.timeZoneCoef)
    End Function

    'S秒間隔でN個ゾーンを作る
    '①-∞～-1.0/ ②-1.0～-0.8/ ③-0.8～-0.6/・・・/⑫1.0～+∞
    Public Function GetTimeZoneN(ByVal tmpTime As Single, ByVal S As Single, ByVal N As Integer) As Integer
        Dim idx As Integer = Int(tmpTime / S) + N \ 2
        If idx < 0 Then
            Return 0
        ElseIf idx >= N Then
            Return N - 1
        Else
            Return idx
        End If
    End Function

    '0.2秒間隔で12個ゾーンを作る
    '①-∞～-1.0/ ②-1.0～-0.8/ ③-0.8～-0.6/・・・/⑫1.0～+∞
    Public Function GetTimeZone12(ByVal tmpTime As Single) As Integer
        Dim idx As Integer = Int(tmpTime / 0.2) + 6
        If idx < 0 Then
            Return 0
        ElseIf idx > 11 Then
            Return 11
        Else
            Return idx
        End If
    End Function

    '0.3秒間隔で６個ゾーンを作る
    '①～-0.3/ ②-0.3～0/ ③0～0.3/ ④0.3～0.6/ ⑤0.6～0.9/ ⑥0.9～
    Private Function GetTimeZone6(ByVal tmpTime As Single) As Integer
        If tmpTime < -0.299 Then
            Return 0
        ElseIf tmpTime < 0 Then
            Return 1
        ElseIf tmpTime < 0.301 Then
            Return 2
        ElseIf tmpTime < 0.601 Then
            Return 3
        ElseIf tmpTime < 0.901 Then
            Return 4
        Else
            Return 5
        End If
    End Function
End Module
