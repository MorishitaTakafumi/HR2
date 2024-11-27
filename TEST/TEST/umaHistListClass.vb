Imports System.Data.SQLite

Public Class umaHistListClass
    '競走馬レース履歴

    Private m_bf As New List(Of UmaHistClass)

    Public ReadOnly Property cnt As Integer
        Get
            Return m_bf.Count
        End Get
    End Property

    Public Sub init()
        m_bf.Clear()
    End Sub

    Public Sub add1(ByVal o As UmaHistClass)
        For j As Integer = 0 To cnt - 1
            If o.dt.Date = m_bf(j).dt.Date AndAlso o.racename = m_bf(j).racename Then
                Return
            End If
        Next
        m_bf.Add(o)
    End Sub

    Public Function GetBodyRef(ByVal idx As Integer) As UmaHistClass
        Return m_bf(idx)
    End Function

    '前走日取得
    Private Function GetDtZenso(ByVal dt_race As Date) As Date
        Dim dtMax As Date = #1900/1/1#
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun > 0 OrElse m_bf(j).cyakujun = -999 Then '中止は含めるが取消、除外は含めない
                If m_bf(j).dt > dtMax AndAlso m_bf(j).dt < dt_race Then
                    dtMax = m_bf(j).dt
                End If
            End If
        Next
        Return dtMax
    End Function

    '初出走インデックス取得
    Private Function Get1stIndex() As Integer
        For j As Integer = cnt - 1 To 0 Step -1
            If m_bf(j).cyakujun > 0 OrElse m_bf(j).cyakujun = -999 Then '中止は含めるが取消、除外は含めない
                Return j
            End If
        Next
        Return -1
    End Function

    '前走と今回レース日の間隔での成績調査
    '戻り値：1着回数x10^6 + 2着回数x10^4 +3着回数x10^2 + 4着以下回数
    '※spanValに"合計点/平均点(該当レース回数)"をセットして返す 
    Public Function GetSpanScore(ByVal dtRace As Date, ByRef spanVal As String) As Integer
        Dim dtZenso As Date = GetDtZenso(dtRace)
        Dim span As Integer = DateDiff(DateInterval.Day, dtZenso, dtRace)
        Dim Cyakucnt(3) As Integer
        For j As Integer = 0 To 3
            Cyakucnt(j) = 0
        Next
        Dim st_idx As Integer = Get1stIndex()
        If st_idx >= 0 Then
            dtZenso = m_bf(st_idx).dt '初出走日を起点にする
            Dim ttlCnt As Integer = 0
            For j As Integer = st_idx - 1 To 0 Step -1

                If dtRace <= m_bf(j).dt Then '評価対象のレースの日を含めてそれ以降のレースは対象外とする
                    Exit For
                End If

                If m_bf(j).cyakujun > 0 OrElse m_bf(j).cyakujun = -999 Then '中止は含めるが取消、除外は含めない
                    Dim hisa As Integer = DateDiff(DateInterval.Day, dtZenso, m_bf(j).dt)
                    If hisa >= span - 7 AndAlso hisa <= span + 7 Then
                        If m_bf(j).cyakujun = 1 Then
                            Cyakucnt(0) += 1
                        ElseIf m_bf(j).cyakujun = 2 Then
                            Cyakucnt(1) += 1
                        ElseIf m_bf(j).cyakujun = 3 Then
                            Cyakucnt(2) += 1
                        Else
                            Cyakucnt(3) += 1
                        End If
                        ttlCnt += 1
                    End If
                    dtZenso = m_bf(j).dt
                End If
            Next
            If ttlCnt > 0 Then
                Dim ttlp As Integer = Cyakucnt(0) * 5 + Cyakucnt(1) * 3 + Cyakucnt(2) * 1
                Dim avep As Single = ttlp / ttlCnt
                spanVal = ttlp.ToString & "/" & avep.ToString("F2") & "(" & ttlCnt & "回)"
                Return Cyakucnt(0) * 10 ^ 6 + Cyakucnt(1) * 10 ^ 4 + Cyakucnt(2) * 10 ^ 2 + Cyakucnt(3)
            End If
        End If
        spanVal = "－"
        Return 0
    End Function

    '日付と距離の成績を取得する
    '戻り値：同一月日のレースの1着回数x10^6 + 2着回数x10^4 +3着回数x10^2 + 4着以下回数
    '※kyori_scoreに同一距離のレースの1着回数x10^6 + 2着回数x10^4 +3着回数x10^2 + 4着以下回数をセットして返す 
    Public Function GetSameDateSameKyoriScore(ByVal arg_dt As Date, ByVal kyori As Integer, ByVal syubetu As String, ByRef kyori_score As Integer) As Integer
        Dim kyoriScore(3) As Short
        Dim dateScore(3) As Short
        For j As Integer = 0 To 3
            kyoriScore(j) = 0
            dateScore(j) = 0
        Next
        Dim dt_max As Date = arg_dt.AddDays(7)
        Dim dt_min As Date = arg_dt.AddDays(-7)
        For j As Integer = cnt - 1 To 0 Step -1

            If arg_dt <= m_bf(j).dt Then '対象レース日を含めて後のレースは対象外とする
                Exit For
            End If

            If m_bf(j).cyakujun > 0 OrElse m_bf(j).cyakujun = -999 Then '中止は含めるが取消、除外は含めない
                Dim dt_tmp As Date = DateSerial(arg_dt.Year, m_bf(j).dt.Month, m_bf(j).dt.Day)
                If dt_min <= dt_tmp AndAlso dt_tmp <= dt_max Then
                    Select Case m_bf(j).cyakujun
                        Case 1
                            dateScore(0) += 1
                        Case 2
                            dateScore(1) += 1
                        Case 3
                            dateScore(2) += 1
                        Case Else
                            dateScore(3) += 1
                    End Select
                End If
                If kyori = m_bf(j).distance AndAlso InStr(syubetu, m_bf(j).syubetu) > 0 Then
                    Select Case m_bf(j).cyakujun
                        Case 1
                            kyoriScore(0) += 1
                        Case 2
                            kyoriScore(1) += 1
                        Case 3
                            kyoriScore(2) += 1
                        Case Else
                            kyoriScore(3) += 1
                    End Select
                End If
            End If
        Next
        kyori_score = makeScore(kyoriScore)
        Return makeScore(dateScore)
    End Function

    Private Function makeScore(ByVal score() As Short) As Integer
        Return score(0) * 10 ^ 6 + score(1) * 10 ^ 4 + score(2) * 10 ^ 2 + score(3)
    End Function

    Public Function load(ByVal cmd As SQLiteCommand, ByVal uma_id As Integer) As String
        Dim errmsg As String = ""
        init()
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT * FROM UmaHist WHERE uma_id=@uma_id"
            cmd.Parameters.AddWithValue("@uma_id", uma_id)
            Dim r As SQLiteDataReader = cmd.ExecuteReader
            While r.Read
                Dim a As New UmaHistClass
                With a
                    .rec_id = r("id")
                    'Public Property race_id As Integer
                    .dt = r("dt")
                    .jo_code = r("jo_code")
                    .keibajo = GetKeibajoName(.jo_code)
                    .racename = r("race_name")
                    'Public Property grade As String
                    .distance = r("kyori")
                    .type_code = r("type_code")
                    .syubetu = GetRaceTypeName(.type_code)
                    .baba = r("baba")
                    .tosu = r("tosu")
                    .ninki = r("ninki")
                    .cyakujun = r("cyakujun")
                    .kisyu = r("kisyu")
                    .hutan = r("hutan")
                    .w = r("w")
                    .tokei = r("secs")
                    .href = r("href")
                End With
                add1(a)
            End While
            r.Close()
            Return ""
        Catch ex As Exception
            Return "umaHistListClass.load() " & ex.Message
        End Try
    End Function

    Public Function save(ByVal cmd As SQLiteCommand, ByVal uma_id As Integer) As String
        Dim errmsg As String = ""
        For j As Integer = 0 To cnt - 1
            If m_bf(j).rec_id <= 0 Then
                errmsg = m_bf(j).addNew(cmd, uma_id)
                If errmsg.Length > 0 Then
                    Return errmsg
                End If
            End If
        Next
        Return ""
    End Function

End Class
