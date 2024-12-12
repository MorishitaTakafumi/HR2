Imports System.Data.SQLite

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

    '性齢パック
    Public Function packSeirei() As String
        Return sex & "-" & age.ToString
    End Function

    '性齢アンパック
    Public Sub unpackSeirei(ByVal seirei As String)
        If seirei.Trim.Length >= 3 Then
            Dim ip As Integer = InStr(seirei, "-")
            If ip > 1 Then
                sex = Left(seirei, ip - 1)
                Try
                    age = CInt(Mid(seirei, ip + 1))
                Catch ex As Exception
                    age = 0
                End Try
                Return
            End If
        End If
        sex = ""
        age = 0
    End Sub

    '通過順パック
    Public Function packTukajun() As String
        Return m_tukajun(0).ToString & "-" & m_tukajun(1).ToString & "-" & m_tukajun(2).ToString & "-" & m_tukajun(3).ToString
    End Function

    '通過順アンパック
    Public Sub unpackTukajun(ByVal tukajun As String)
        For j As Integer = 0 To 3
            m_tukajun(j) = 0
        Next
        If tukajun.Trim.Length > 0 Then
            Dim sbf() As String = Split(tukajun, "-")
            For j As Integer = 0 To sbf.Length - 1
                If IsNumeric(sbf(j)) Then
                    m_tukajun(j) = CInt(sbf(j))
                End If
            Next
        End If
    End Sub

    'レースヘッダーIDと馬名を指定してロード
    Public Function load(ByVal cmd As SQLiteCommand, ByVal raceheader_id As Integer, ByVal arg_bamei As String) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT * FROM kekka WHERE race_header_id=@race_header_id AND bamei=@bamei"
            cmd.Parameters.AddWithValue("@race_header_id", raceheader_id)
            cmd.Parameters.AddWithValue("@bamei", arg_bamei)
            Dim r As SQLiteDataReader = cmd.ExecuteReader
            If r.Read Then
                With Me
                    .rec_id = r("id")
                    .race_id = r("race_header_id")
                    .cyakujun = r("cyakujun")
                    .umaban = r("umaban")
                    .bamei = r("bamei")
                    .unpackSeirei(r("seirei"))
                    .hutan = r("hutan")
                    .kisyu = r("kisyu")
                    .tokei = r("secs")
                    .unpackTukajun(r("tocyu"))
                    .agari = r("agari")
                    .agarisa = r("agarisa")
                    .cyakusa = r("cyakusa")
                    .w = r("w")
                    .zogen = r("zogen")
                    .cyokyosi = r("cyokyosi")
                    .ninki = r("ninki")
                    .uma_href = r("href")
                End With
            End If
            r.Close()
            Return ""
        Catch ex As Exception
            Return "KekkaClass.load() " & ex.Message
        End Try
    End Function

    '新規登録
    Public Function addNew(ByVal cmd As SQLiteCommand) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO Kekka(race_header_id, cyakujun, umaban, bamei, seirei, hutan, kisyu, secs, tocyu, agari, agarisa, 
                                cyakusa, w, zogen, cyokyosi, ninki, href) 
                            VALUES(@race_header_id, @cyakujun, @umaban, @bamei, @seirei, @hutan, @kisyu, @secs, @tocyu, @agari, @agarisa, 
                                @cyakusa, @w, @zogen, @cyokyosi, @ninki, @href)"

            cmd.Parameters.AddWithValue("@race_header_id", race_id)
            cmd.Parameters.AddWithValue("@cyakujun", cyakujun)
            cmd.Parameters.AddWithValue("@umaban", umaban)
            cmd.Parameters.AddWithValue("@bamei", bamei)
            cmd.Parameters.AddWithValue("@seirei", packSeirei())
            cmd.Parameters.AddWithValue("@hutan", hutan)
            cmd.Parameters.AddWithValue("@kisyu", kisyu)
            cmd.Parameters.AddWithValue("@secs", tokei)
            cmd.Parameters.AddWithValue("@tocyu", packTukajun())
            cmd.Parameters.AddWithValue("@agari", agari)
            cmd.Parameters.AddWithValue("@agarisa", agarisa)
            cmd.Parameters.AddWithValue("@cyakusa", cyakusa)
            cmd.Parameters.AddWithValue("@w", w)
            cmd.Parameters.AddWithValue("@zogen", zogen)
            cmd.Parameters.AddWithValue("@cyokyosi", cyokyosi)
            cmd.Parameters.AddWithValue("@ninki", ninki)
            cmd.Parameters.AddWithValue("@href", uma_href)
            cmd.ExecuteNonQuery()
            Return ""
        Catch ex As Exception
            Return "KekkaClass.addNew() " & ex.Message
        End Try
    End Function

    '着差更新
    Public Function updateCyakusa(ByVal cmd As SQLiteCommand) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "UPDATE Kekka SET cyakusa=@cyakusa WHERE id=@id"
            cmd.Parameters.AddWithValue("@cyakusa", cyakusa)
            cmd.Parameters.AddWithValue("@id", rec_id)
            cmd.ExecuteNonQuery()
            Return ""
        Catch ex As Exception
            Return "KekkaClass.updateCyakusa() " & ex.Message
        End Try
    End Function
End Class
