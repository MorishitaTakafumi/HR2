Public Class SyutubaClass
    '出馬表

    Public Property rec_id As Integer
    Public Property race_id As Integer
    Public Property waku As Short
    Public Property umaban As Short
    Public Property bamei As String
    Public Property sex As String
    Public Property age As Short
    Public Property hutan As Single
    Public Property kisyu As String
    Public Property w As Single
    Public Property zogen As Single
    Public Property cyokyosi As String
    Public Property ninki As Short
    Public Property href As String

    Public Sub New()
        rec_id = -1
        race_id = -1
        waku = -1
        umaban = -1
        bamei = ""
        sex = ""
        age = -1
        hutan = 0
        kisyu = ""
        w = 0
        zogen = 0
        cyokyosi = ""
        ninki = -1
        href = ""
    End Sub

End Class
