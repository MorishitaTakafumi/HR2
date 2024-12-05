Imports System.Data.SQLite

Public Class KekkaListClass
    'レース結果リスト・・・ある１つの出走各場のレース結果のリスト

    Private m_bf As New List(Of KekkaClass)
    Public raceHeader As New RaceHeaderClass

    Public ReadOnly Property cnt As Integer
        Get
            Return m_bf.Count
        End Get
    End Property

    Public Sub init()
        raceHeader.init()
        m_bf.Clear()
    End Sub

    Public Sub add1(ByVal o As KekkaClass)
        m_bf.Add(o)
    End Sub

    '着順を指定してBody参照を取得する
    Public Function GetBodyRefByCyakujun(ByVal cyakujun As Integer) As KekkaClass
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun = cyakujun Then
                Return m_bf(j)
            End If
        Next
        Return Nothing
    End Function

    '馬名を指定してBody参照を取得する
    '
    Public Function GetBodyRefByBamei(ByVal arg_bamei As String) As KekkaClass
        For j As Integer = 0 To cnt - 1
            If StrComp(arg_bamei, m_bf(j).bamei, CompareMethod.Text) = 0 Then
                Return m_bf(j)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetBodyRef(ByVal idx As Integer) As KekkaClass
        Return m_bf(idx)
    End Function

    '馬名を指定して上り差と着差を文字列で返す
    Public Function GetAgarisa(ByVal arg_bamei As String, ByVal konkaiSyubetu As String) As String
        For j As Integer = 0 To cnt - 1
            If StrComp(arg_bamei, m_bf(j).bamei, CompareMethod.Text) = 0 Then
                If m_bf(j).cyakujun > 0 Then
                    Dim ss As String = m_bf(j).agarisa.ToString("F1") & "(" & m_bf(j).cyakusa.ToString("F1") & ")"
                    If IsSameTypeRace(konkaiSyubetu) Then
                        Return ss
                    Else
                        Return "[" & ss & "]"
                    End If
                End If
            End If
        Next
        Return ""
    End Function

    'レース種別の同異
    '戻り値：True=同種, False=異種
    Public Function IsSameTypeRace(ByVal konkaiSyubetu As String) As Boolean
        Dim cmps As String
        If InStr(konkaiSyubetu, "芝") Then
            cmps = "芝"
        Else
            cmps = "ダート"
        End If
        If InStr(raceHeader.syubetu, cmps) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    '着差セット
    Public Sub setCyakusa()
        Dim time1 As Single
        Dim time2 As Single
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun = 1 Then
                time1 = m_bf(j).tokei
            End If
            If m_bf(j).cyakujun = 2 Then
                time2 = m_bf(j).tokei
            End If
        Next
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun = 1 Then
                m_bf(j).cyakusa = time1 - time2
            ElseIf m_bf(j).cyakujun > 0 Then '除外・中止・取消は除く
                m_bf(j).cyakusa = m_bf(j).tokei - time1
            End If
        Next
    End Sub

    '上り差補正値セット
    Public Sub setAgarisa(ByVal oHead As RaceHeaderClass)
        Dim agariList As New List(Of Single)
        '1～5着以内での上り順
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun >= 1 AndAlso m_bf(j).cyakujun <= 5 Then
                agariList.Add(m_bf(j).agari)
            End If
        Next
        agariList.Sort()
        'レースクラスをオープンに標準化するための補正値
        Dim hoseiti As Single = 0
        Select Case oHead.grade
            Case "G3"
                hoseiti = -0.2
            Case "G2"
                hoseiti = -0.4
            Case "G1"
                hoseiti = -0.6
            Case Else
                If InStr(oHead.class_name, "未勝利") > 0 OrElse InStr(oHead.class_name, "新馬") > 0 Then
                    hoseiti = 0.8
                ElseIf InStr(oHead.class_name, "1勝") > 0 OrElse InStr(oHead.class_name, "500万") > 0 Then
                    hoseiti = 0.6
                ElseIf InStr(oHead.class_name, "2勝") > 0 OrElse InStr(oHead.class_name, "1000万") > 0 Then
                    hoseiti = 0.4
                ElseIf InStr(oHead.class_name, "3勝") > 0 OrElse InStr(oHead.class_name, "1600万") > 0 Then
                    hoseiti = 0.2
                End If
        End Select

        '補正計算では何コーナーの通過順位を使うか
        Dim corner_idx As Integer = oHead.GetCornerToCalcAgarisa() - 1

        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun > 0 Then
                If m_bf(j).agari = agariList(0) AndAlso m_bf(j).cyakujun <= 5 Then '5着内で最速の上り馬は5着内で2番目の上りとの差(その馬が5着より下の場合は5着内で最速場と比較する)
                    m_bf(j).agarisa = m_bf(j).agari - agariList(1)
                Else
                    m_bf(j).agarisa = m_bf(j).agari - agariList(0)
                End If
                'コーナーでの順位を考慮
                If m_bf(j).tukajun(corner_idx) > 0 Then
                    m_bf(j).agarisa += (m_bf(j).tukajun(corner_idx) - 1) * 0.1 - 0.4 + hoseiti
                End If
            End If
        Next

    End Sub

    Public Function load(ByVal cmd As SQLiteCommand, ByVal race_header_id As Integer) As String
        Dim errmsg As String = ""
        m_bf.Clear()
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT * FROM Kekka WHERE race_header_id=@race_header_id"
            cmd.Parameters.AddWithValue("@race_header_id", race_header_id)
            Dim r As SQLiteDataReader = cmd.ExecuteReader
            While r.Read
                Dim a As New KekkaClass
                With a
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
                m_bf.Add(a)
            End While
            r.Close()
            Return ""
        Catch ex As Exception
            Return "kekkaListClass.load() " & ex.Message
        End Try
    End Function

    'レース結果登録
    Public Function save(ByVal cmd As SQLiteCommand) As String
        Dim errmsg As String = ""
        For j As Integer = 0 To cnt - 1
            If m_bf(j).rec_id <= 0 Then
                m_bf(j).race_id = raceHeader.id
                errmsg = m_bf(j).addNew(cmd)
                If errmsg.Length > 0 Then
                    Return errmsg
                End If
            End If
        Next
        Return ""
    End Function

    'DBからデータ取得
    'Return True=成功、False=ない/失敗
    Private Function DB_GetDataByName(ByVal cmd As SQLiteCommand, ByVal dt_race As Date, ByVal racename As String) As String
        Try
            cmd.Parameters.Clear()
            Dim errmsg As String = raceHeader.loadByDateAndName(cmd, dt_race, racename)
            If errmsg.Length = 0 AndAlso raceHeader.id > 0 Then
                errmsg = load(cmd, raceHeader.id)
                setAgarisa(raceHeader)
            End If
            Return errmsg
        Catch ex As Exception
            Return "KekkaListClass.DB_GetDataByName() " & ex.Message
        End Try
    End Function

    'Return ""=成功、エラーメッセージ=失敗
    Private Function SaveData(ByVal cmd As SQLiteCommand) As String
        Dim errmsg As String = ""
        Dim oK As RaceHeaderClass = raceHeader
        If oK.id < 0 Then
            errmsg = oK.loadByDateAndName(cmd, oK.dt, oK.race_name)
            If errmsg.Length > 0 Then
                Return errmsg
            End If
            If oK.id < 0 Then
                errmsg = oK.addNew(cmd)
            End If
        End If
        If errmsg.Length = 0 Then
            errmsg = save(cmd)
        End If
        Return errmsg
    End Function

    'レース結果を取得して登録する
    'Return ""=成功、エラーメッセージ=失敗
    Private Function _GetRaceKekka(ByVal cmd As SQLiteCommand,
                                   ByVal url As String,
                                   ByRef existFlag As Boolean,
                                   ByVal dt_race As String,
                                   ByVal racename As String,
                                   ByVal autosave As Boolean) As String
        Dim errmsg As String = ""
        existFlag = False
        init()
        'Case 1:レース日とレース名が既知でDBに登録済み 
        If IsDate(dt_race) AndAlso racename.Trim.Length > 0 Then
            errmsg = DB_GetDataByName(cmd, CDate(dt_race), racename)
            If errmsg.Length > 0 Then
                Return errmsg
            Else
                If raceHeader.id > 0 AndAlso cnt > 0 Then
                    existFlag = True
                    Return ""
                End If
            End If
        End If

        'Case 2:URLから情報取得 
        If url.Trim.Length = 0 Then
            Return ""
        End If
        Dim contents As String = GetWebPageText(url.Trim)
        If contents.Length = 0 Then
            Return ""
        End If
        raceHeader.keibajo = GetWhenWhere(contents, raceHeader.dt)
        raceHeader.race_no = GetRaceNo(contents)
        raceHeader.race_name = GetRaceName(contents, raceHeader.grade)
        raceHeader.class_name = GetClassCource(contents, raceHeader.kyori, raceHeader.syubetu)
        raceHeader.class_code = raceHeader.GetClassCode()
        'Case 1でDB_GetDataByNameやっていてもレース名のテキストが"ジャパンC"のように簡略表記されているケースがあるので正式名で再度トライする
        errmsg = DB_GetDataByName(cmd, raceHeader.dt, raceHeader.race_name)
        If errmsg.Length > 0 Then
            Return errmsg
        Else
            If raceHeader.id > 0 AndAlso cnt > 0 Then 'DBに登録済み
                existFlag = True
                Return ""
            End If
        End If
        'Case 3:DB未登録（Headerのみ登録済みのケースがあり得る）
        GetKekka(contents, Me)
        raceHeader.tosu = cnt
        setCyakusa()
        setAgarisa(raceHeader)
        If autosave Then
            Return SaveData(cmd)
        Else
            Return ""
        End If
    End Function

    'レース結果を取得して登録する
    'Return ""=成功、エラーメッセージ=失敗
    Public Function GetRaceKekka(ByVal cmd As SQLiteCommand,
                                 ByVal url As String,
                                 ByRef existFlag As Boolean,
                                 Optional ByVal dt_race As String = "",
                                 Optional ByVal racename As String = "",
                                 Optional ByVal autosave As Boolean = False) As String
        Return _GetRaceKekka(cmd, url, existFlag, dt_race, racename, autosave)
    End Function

    'レース結果を取得して登録する
    'Return ""=成功、エラーメッセージ=失敗
    Public Function GetRaceKekka(ByVal url As String,
                                 ByRef existFlag As Boolean,
                                 Optional ByVal dt_race As String = "",
                                 Optional ByVal racename As String = "",
                                 Optional ByVal autosave As Boolean = False) As String
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            Return _GetRaceKekka(cmd, url, existFlag, dt_race, racename, autosave)
        End Using
    End Function

End Class
