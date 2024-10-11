Public Class raceAnanClass
    'レース解析値

    Public Property waku As Short
    Public Property umaban As Short
    Public Property bamei As String
    Public Property ninki As Short
    Public Property spanVal As String

    Private m_hist(5) As String

    Public Property kyoriScore As Integer
    Public Property dateScore As Integer


    Public Sub New()
        waku = -1
        umaban = -1
        ninki = -1
        bamei = ""
        spanVal = ""
        For j As Integer = 0 To 5
            m_hist(j) = ""
        Next
        kyoriScore = 0
        dateScore = 0
    End Sub

    Public Property hist(ByVal idx As Integer) As String
        Get
            Return m_hist(idx)
        End Get
        Set(value As String)
            m_hist(idx) = value
        End Set
    End Property

    Public Function isGoodHist(ByVal idx As Integer, Optional ByVal sa As Single = 0.5) As Boolean
        If m_hist(idx).Length > 0 Then
            Dim ip As Integer = InStr(m_hist(idx), "(")
            If ip > 0 Then
                Dim ss As String = Replace(Left(m_hist(idx), ip - 1), "[", "")
                Dim c_sa As Single = CSng(ss)
                If c_sa <= sa Then
                    Return True
                End If
            End If
        End If
        Return False
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
End Class
