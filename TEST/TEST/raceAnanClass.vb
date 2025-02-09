Public Class raceAnanClass
    'レース解析値

    Public Property waku As Short
    Public Property umaban As Short
    Public Property bamei As String
    Public Property ninki As Short
    Public Property hutan As Single
    Public Property spanVal As String

    Private m_hist(5) As String

    Public Property spanScore As Integer
    Public Property kyoriScore As Integer
    Public Property dateScore As Integer
    Public Property agarisaRank As Integer
    Public Property cyakusaRank As Integer

    Public Property extraPoint As Integer


    Public Sub New()
        waku = -1
        umaban = -1
        ninki = -1
        hutan = 0
        bamei = ""
        spanVal = ""
        For j As Integer = 0 To 5
            m_hist(j) = ""
        Next
        spanScore = 0
        kyoriScore = 0
        dateScore = 0
        agarisaRank = DMY_VAL
        cyakusaRank = DMY_VAL
        extraPoint = 0
    End Sub

    Public Property hist(ByVal idx As Integer) As String
        Get
            Return m_hist(idx)
        End Get
        Set(value As String)
            m_hist(idx) = value
        End Set
    End Property

    '上り差、着差の優秀さ
    'Return 1=上り差優秀、2=着差優秀、3=両方優秀、4=異種レース、0=その他
    Public Function isGoodHist(ByVal idx As Integer, Optional ByVal sa As Single = 0.5) As Integer
        If m_hist(idx).Length > 0 Then
            If InStr(m_hist(idx), "]") > 0 Then
                Return 4
            End If
            Dim ss As String = m_hist(idx)
            Dim ip As Integer = InStr(ss, "(")
            If ip > 0 Then
                Dim dd As String = Left(ss, ip - 1)
                Dim agarisa As Single = CSng(dd)
                dd = Replace(Mid(ss, ip + 1), ")", "")
                Dim cyakusa As Single = CSng(dd)
                If agarisa <= sa Then
                    If cyakusa <= sa Then
                        Return 3
                    Else
                        Return 1
                    End If
                ElseIf cyakusa <= sa Then
                    Return 2
                End If
            End If
        End If
        Return 0
    End Function

    Public Function isGoodSpan(Optional ByVal thv As Single = 3) As Boolean
        Dim ip As Integer = InStr(spanVal, "/")
        If ip > 0 Then
            Dim ss As String = Mid(spanVal, ip + 1)
            ip = InStr(ss, "(")
            If ip > 0 Then
                ss = Left(ss, ip - 1)
            End If
            If IsNumeric(ss) Then
                Dim av As Single = CSng(ss)
                If av >= thv Then
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    '近4走の上り差、着差の優秀さプロパティセット
    Public Sub SetTimeRank(ByVal param As ParamSetClass)
        Dim dcnt As Integer = 0
        Dim agarisaZonePointSum As Single = 0
        Dim cyakusaZonePointSum As Single = 0
        Dim n_zone As Integer = 24
        Dim zoneSpan As Single = 0.2
        For idx As Integer = 0 To 3
            If m_hist(idx).Length > 0 Then
                If InStr(m_hist(idx), "]") = 0 Then '異種レースは除外する
                    Dim ss As String = m_hist(idx)
                    Dim ip As Integer = InStr(ss, "(")
                    If ip > 0 Then
                        Dim dd As String = Left(ss, ip - 1)
                        Dim agarisa As Single = CSng(dd)
                        agarisaZonePointSum += (n_zone - 1 - GetTimeZoneN(agarisa, zoneSpan, n_zone)) * param.timeP(idx)
                        dd = Replace(Mid(ss, ip + 1), ")", "")
                        Dim cyakusa As Single = CSng(dd)
                        cyakusaZonePointSum += (n_zone - 1 - GetTimeZoneN(cyakusa, zoneSpan, n_zone)) * param.timeP(idx)
                        dcnt += 1
                    End If
                End If
            End If
        Next
        If dcnt > 0 Then
            agarisaRank = (agarisaZonePointSum / dcnt) / 11 * 100 * param.agarisaCyakusaRate
            cyakusaRank = (cyakusaZonePointSum / dcnt) / 11 * 100
            If dcnt < 4 Then
                Dim waribiki As Double = 1 - (4 - dcnt) * param.waribiki
                agarisaRank *= waribiki
                cyakusaRank *= waribiki
            End If
        Else
            agarisaRank = 0
            cyakusaRank = 0
        End If
    End Sub

End Class
