Public Class RaceHeaderClass
    Public Property keibajo As String
    Public Property dt As Date
    Public Property racename As String
    Public Property grade As String
    Public Property distance As Integer
    Public Property syubetu As String
    Public Property classname As String

    '上り差の補正値を計算するため何コーナーでの通過順位を使うか
    Public Function GetCornerToCalcAgarisa() As Integer

        Select Case keibajo
            Case "札幌"
                Return 3
            Case "函館"
                Return 3
            Case "福島"
                Return 3
            Case "小倉"
                Return 3
            Case "新潟"
                If syubetu = "ダート" Then
                    Return 3
                Else
                    If InStr(syubetu, "外") = 0 Then
                        Return 3
                    Else
                        Return 4
                    End If
                End If
            Case "中山"
                If syubetu = "ダート" Then
                    Return 3
                Else
                    If InStr(syubetu, "外") = 0 Then
                        Return 3
                    Else
                        Return 4
                    End If
                End If
            Case "東京"
                Return 4
            Case "中京"
                Return 4
            Case "京都"
                If syubetu = "ダート" Then
                    Return 3
                Else
                    If InStr(syubetu, "外") = 0 Then
                        Return 3
                    Else
                        Return 4
                    End If
                End If
            Case "阪神"
                If syubetu = "ダート" Then
                    Return 3
                Else
                    If InStr(syubetu, "外") = 0 Then
                        Return 3
                    Else
                        Return 4
                    End If
                End If
            Case Else
                Return 4
        End Select
    End Function

End Class
