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

    Public Function cnvAgarisaStr2Val(ByVal strScore As String) As Single
        If strScore.Length = 0 Then
            Return DMY_VAL
        End If
        strScore = Replace(Replace(strScore, "[", ""), "]", "")

        Dim ip As Integer = InStr(strScore, "(")
        If ip > 1 Then
            strScore = strScore.Substring(0, ip - 1)
            Return CSng(strScore)
        End If
        Return DMY_VAL
    End Function

End Module
