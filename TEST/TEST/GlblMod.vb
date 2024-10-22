Imports System.Runtime.InteropServices.WindowsRuntime

Public Module GlblMod
    Public Const DMY_DATE As Date = #1900/1/1#
    Public Const DMY_VAL As Integer = -9999

    Public JoMei() As String = {"札幌", "函館", "福島", "新潟", "中山", "東京", "中京", "京都", "阪神", "小倉"}

    Public Function GetKeibajoCode(ByVal keibajo As String) As Short
        For j As Integer = 0 To JoMei.Length - 1
            If InStr(keibajo, JoMei(j)) > 0 Then
                Return j
            End If
        Next
        Return -1
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
        strScore = Replace(Replace(strScore, "[", ""), "]", "")

        Dim ip As Integer = InStr(strScore, "(")
        If ip > 1 Then
            strScore = strScore.Substring(0, ip - 1)
            cyakusa = CSng(Replace(strScore.Substring(2), ")", ""))
            Return CSng(strScore)
        End If
        Return DMY_VAL
    End Function

    'spanScoreの適合度を得点化する
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
        Dim P() As Integer = {10, 40, 70, 100}
        Dim psum As Integer = 0
        For j As Integer = 0 To 3
            Dim myp As Integer = (myScore \ (100 ^ j)) Mod 100
            Dim cmpp As Integer = (cmpScore \ (100 ^ j)) Mod 100
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
        Next
        Return Int(psum)
    End Function

    '上り差／着差の適合度を得点化する
    Public Function GetDegreeOfFit_time(ByVal myTime As Single, ByVal cmpTime As Single, ByVal soumaeV As Integer) As Integer
        '上り差／着差の適合度
        '
        '0.3秒間隔で６個ゾーンを作る
        '①～-0.3/ ②-0.3～0/ ③0～0.3/ ④0.3～0.6/ ⑤0.6～0.9/ ⑥0.9～
        '
        '出走馬myTimeと比較対象馬cmpTimeのゾーンの一致度を得点化する
        '
        'myTimeのゾーン=a, cmpTimeのゾーン=b として
        '(a-b)=0　のとき 1
        '(a-b)<0　のとき (a-b)*R1
        '(a-b)>0　のとき (a-b)*R2
        '
        'さらにそれが何走前かでも重みづけの係数 P1, P2, P3, P4 を掛ける
        '
        Dim R1 As Single = 0.1
        Dim R2 As Single = 0.3
        Dim P() As Single = {1, 0.9, 0.8, 0.7}
        Dim psum As Integer = 0
        Dim myZone As Integer = GetTimeZone(myTime)
        Dim cmpZone As Integer = GetTimeZone(cmpTime)
        If myZone = cmpZone Then
            Return 1000 * P(soumaeV)
        ElseIf myZone < cmpZone Then
            Return 1000 * P(soumaeV) * (myZone - cmpZone) * R1
        Else
            Return 1000 * P(soumaeV) * (myZone - cmpZone) * R2
        End If
    End Function

    Private Function GetTimeZone(ByVal tmpTime As Single) As Integer
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
