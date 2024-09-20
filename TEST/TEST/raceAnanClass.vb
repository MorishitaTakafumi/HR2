Public Class raceAnanClass
    'レース解析

    Public Property umaban As Short
    Public Property bamei As String
    Public Property spanVal As String

    Private m_hist(5) As String

    Public Sub New()
        umaban = -1
        bamei = ""
        spanVal = ""
        For j As Integer = 0 To 5
            m_hist(j) = ""
        Next
    End Sub

    Public Property hist(ByVal idx As Integer) As String
        Get
            Return m_hist(idx)
        End Get
        Set(value As String)
            m_hist(idx) = value
        End Set
    End Property

End Class
