Public Class KekkaClass
    'レース結果・・・ある1頭の馬のある１つのレース結果

    Public Property rec_id As Integer
    Public Property race_id As Integer
    Public Property cyakujun As Short '除外=-998、中止=-999
    Public Property umaban As Short
    Public Property bamei As String
    Public Property sex As String
    Public Property age As Short
    Public Property hutan As Single
    Public Property kisyu As String
    Public Property tokei As Single
    Public Property agari As Single
    Public Property w As Single
    Public Property zogen As Single
    Public Property cyokyosi As String
    Public Property ninki As Short
    Public Property cyakusa As Single '1(2)着馬とのタイム差
    Public Property agarisa As Single '上りの差の補正値

    Private m_tukajun(3) As Short '1-4角通過順
    Public Property uma_href As String

    Public Sub New()
        rec_id = -1
        race_id = -1
        cyakujun = -1
        umaban = -1
        bamei = ""
        sex = ""
        age = -1
        hutan = 0
        kisyu = ""
        tokei = 0
        For j As Integer = 0 To 3
            m_tukajun(j) = 0
        Next
        agari = 0
        w = 0
        zogen = 0
        cyokyosi = ""
        ninki = -1
        cyakusa = DMY_VAL
        agarisa = DMY_VAL
        uma_href = ""
    End Sub

    Public Property tukajun(ByVal corner As Integer) As Short
        Get
            Return m_tukajun(corner)
        End Get
        Set(value As Short)
            m_tukajun(corner) = value
        End Set
    End Property

    Public Function CyakujunStr() As String
        Return cyakujunDecode(cyakujun)
    End Function

End Class
