Imports System.Data.SQLite

Public Class UmaHistClass
    '競走馬レース履歴
    Implements ICloneable

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
    Public Property jo_code As Integer
    Public Property type_code As Integer

    Public Sub New()
        rec_id = -1
        race_id = -1
        dt = DMY_DATE
        keibajo = ""
        jo_code = -1
        racename = ""
        grade = ""
        distance = -1
        syubetu = ""
        type_code = -1
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

    Public Function Clone() As Object Implements ICloneable.Clone
        Return New UmaHistClass With {
            .rec_id = Me.rec_id,
            .race_id = Me.race_id,
            .dt = Me.dt,
            .keibajo = Me.keibajo,
            .jo_code = Me.jo_code,
            .racename = Me.racename,
            .grade = Me.grade,
            .distance = Me.distance,
            .syubetu = Me.syubetu,
            .type_code = Me.type_code,
            .baba = Me.baba,
            .tosu = Me.tosu,
            .ninki = Me.ninki,
            .cyakujun = Me.cyakujun,
            .kisyu = Me.kisyu,
            .hutan = Me.hutan,
            .w = Me.w,
            .tokei = Me.tokei,
            .bamei = Me.bamei,
            .href = Me.href
        }
    End Function

    Public Function CyakujunStr() As String
        Return cyakujunDecode(cyakujun)
    End Function

    '新規登録
    Public Function addNew(ByVal cmd As SQLiteCommand, ByVal uma_id As Integer) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO UmaHist(uma_id, dt, jo_code, race_name, type_code, kyori, baba, tosu, ninki, cyakujun, kisyu, hutan, w, secs, href) 
                            VALUES(@uma_id, @dt, @jo_code, @race_name, @type_code, @kyori, @baba, @tosu, @ninki, @cyakujun, @kisyu, @hutan, @w, @secs, @href)"

            cmd.Parameters.AddWithValue("@uma_id", uma_id)
            cmd.Parameters.AddWithValue("@dt", dt)
            cmd.Parameters.AddWithValue("@jo_code", GetKeibajoCode(keibajo))
            cmd.Parameters.AddWithValue("@race_name", racename)
            cmd.Parameters.AddWithValue("@type_code", GetTypeCode(syubetu))
            cmd.Parameters.AddWithValue("@kyori", distance)
            cmd.Parameters.AddWithValue("@baba", baba)
            cmd.Parameters.AddWithValue("@tosu", tosu)
            cmd.Parameters.AddWithValue("@ninki", ninki)
            cmd.Parameters.AddWithValue("@cyakujun", cyakujun)
            cmd.Parameters.AddWithValue("@kisyu", kisyu)
            cmd.Parameters.AddWithValue("@hutan", hutan)
            cmd.Parameters.AddWithValue("@w", w)
            cmd.Parameters.AddWithValue("@secs", tokei)
            cmd.Parameters.AddWithValue("@href", href)
            cmd.ExecuteNonQuery()
            Return ""
        Catch ex As Exception
            Return "UmaHistClass.addNew() " & ex.Message
        End Try
    End Function

End Class
