Public Class UmaHistClass
    '競走馬レース履歴

    Public Property rec_id As Integer
    Public Property race_id As Integer
    Public Property dt As Date
    Public Property keibajo As String
    Public Property racename As String
    Public Property grade As String
    Public Property distance As Integer
    Public Property syubetu As String
    Public Property baba As String
    Public Property tosu As Short
    Public Property ninki As Short
    Public Property cyakujun As Short '除外=-998、中止=-999
    Public Property kisyu As String
    Public Property hutan As Single
    Public Property w As Single
    Public Property tokei As Single
    Public Property bamei As String
    Public Property href As String

    Public Sub New()
        rec_id = -1
        race_id = -1
        dt = #1900/1/1#
        keibajo = ""
        racename = ""
        grade = ""
        distance = -1
        syubetu = ""
        baba = ""
        tosu = -1
        ninki = -1
        cyakujun = -1 '除外=-998、中止=-999
        kisyu = ""
        hutan = -1
        w = -1
        tokei = -1
        bamei = ""
        href = ""
    End Sub

    Public Function CyakujunStr() As String
        Return cyakujunDecode(cyakujun)
    End Function
End Class
