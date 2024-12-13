Imports C1.Win.C1FlexGrid
Imports System.Data.SQLite

Public Class raceReviewForm

    Private Const HIS_CNT As Integer = 4
    Private Enum FlxCol
        dt = 0
        racename = 1
        bamei = 2
        cyakujun = 3
        ninki = 4
        sa
        spanVal
        histStart
        kyoriScore = histStart + HIS_CNT * 2
        dateScore
        cols = dateScore + 1
    End Enum

    Private oShortRaceName As New ShortRaceNameClass
    Private spanScore As New List(Of Integer)
    Private cyakujun As New List(Of Integer)
    Private agarisa1 As New List(Of Single)
    Private agarisa2 As New List(Of Single)
    Private agarisa3 As New List(Of Single)
    Private agarisa4 As New List(Of Single)
    Private cyakusa1 As New List(Of Single)
    Private cyakusa2 As New List(Of Single)
    Private cyakusa3 As New List(Of Single)
    Private cyakusa4 As New List(Of Single)

    Public Sub New()
        InitializeComponent()
        SetupCombobox()
    End Sub

    Public Sub entry(ByVal racename As String)
        CbRacename.Text = racename
        Show()
    End Sub

    Private Sub SetupCombobox()
        With CbJo
            .Items.Clear()
            .Items.Add("All")
            For j As Integer = 0 To JoMei.Length - 1
                .Items.Add(JoMei(j))
            Next
            .SelectedIndex = 0
        End With
        With CbSyubetu
            .Items.Clear()
            .Items.Add("All")
            .Items.Add("芝")
            .Items.Add("ダート")
            .Items.Add("障害")
            .SelectedIndex = 0
        End With
        With CbGrade
            .Items.Clear()
            .Items.Add("All")
            .Items.Add("新馬・未勝利")
            .Items.Add("1勝")
            .Items.Add("2勝")
            .Items.Add("3勝")
            .Items.Add("Open/L")
            .Items.Add("G3")
            .Items.Add("G2")
            .Items.Add("G1")
            .SelectedIndex = 0
        End With
        With CbCyakujun
            .Items.Clear()
            .Items.Add("All")
            For j As Integer = 1 To 18
                .Items.Add($"{j}着")
            Next
            .SelectedIndex = 1
        End With
        With CbMonth
            .Items.Clear()
            .Items.Add("All")
            For j As Integer = 1 To 12
                .Items.Add(j.ToString & "月")
            Next
            .SelectedIndex = 0
        End With
        '距離とレース名はDBから選択肢を取得する
        Dim errmsg As String = ""
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = conn.CreateCommand
            conn.Open()
            Try
                cmd.CommandText = "SELECT DISTINCT(kyori) FROM RaceHeader ORDER BY kyori"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                With CbKyori
                    .Items.Clear()
                    .Items.Add("All")
                    While r.Read
                        .Items.Add(r("kyori"))
                    End While
                    .SelectedIndex = 0
                End With
                r.Close()
                '
                cmd.CommandText = "SELECT DISTINCT(race_name) FROM RaceHeader ORDER BY race_name"
                r = cmd.ExecuteReader
                With CbRacename
                    .Items.Clear()
                    .Items.Add("All")
                    While r.Read
                        .Items.Add(r("race_name"))
                    End While
                    .SelectedIndex = 0
                End With
                r.Close()
            Catch ex As Exception
                errmsg = ex.Message
            End Try

            If errmsg.Length > 0 Then
                MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            End If
        End Using
    End Sub

    '一覧グリッド書式設定
    Private Sub SetUpFlx()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 2
            .Rows.Fixed = 2
            .Cols.Fixed = 0
            .Cols.Frozen = 2
            .Item(0, FlxCol.dt) = "日付"
            .Item(0, FlxCol.racename) = "レース名"
            .Item(0, FlxCol.bamei) = "馬名"
            .Item(0, FlxCol.cyakujun) = "着順 "
            .Item(0, FlxCol.ninki) = "人気"
            .Item(0, FlxCol.sa) = "人気-着順"
            .Item(0, FlxCol.spanVal) = "前走間隔" & vbLf & "±７日"
            For j As Integer = 0 To HIS_CNT - 1
                .Item(0, FlxCol.histStart + 2 * j + 0) = (j + 1).ToString & "走前"
                .Item(1, FlxCol.histStart + 2 * j + 0) = "上差"
                .Item(1, FlxCol.histStart + 2 * j + 1) = "着差"
            Next
            .Item(0, FlxCol.kyoriScore) = "今回距離" & vbLf & "成績"
            .Item(0, FlxCol.dateScore) = "今回日付" & vbLf & "±７日成績"

            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 25
            .Cols.MaxSize = 120

            .Cols(FlxCol.racename).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.bamei).TextAlign = TextAlignEnum.LeftCenter

            .AllowMerging = AllowMergingEnum.Custom
            .AllowEditing = False

            .AllowSorting = AllowSortingEnum.SingleColumn
            .AllowFiltering = True

            If .MergedRanges.Count = 0 Then
                Dim crng As CellRange
                For j As Integer = 0 To .Cols.Count - 1
                    If j < FlxCol.histStart OrElse j >= FlxCol.kyoriScore Then
                        crng = .GetCellRange(0, j, 1, j)
                        .MergedRanges.Add(crng)
                    End If
                Next
                For j As Integer = 0 To HIS_CNT - 1
                    crng = .GetCellRange(0, FlxCol.histStart + 2 * j, 0, FlxCol.histStart + 2 * j + 1)
                    .MergedRanges.Add(crng)
                Next
            End If

            For j As Integer = 0 To .Cols.Count - 1
                If j > 0 AndAlso j <= FlxCol.spanVal OrElse j <= FlxCol.kyoriScore OrElse j <= FlxCol.dateScore Then
                    .Cols(j).AllowFiltering = AllowFiltering.Default
                Else
                    .Cols(j).AllowFiltering = AllowFiltering.None
                End If
            Next

            If Not .Styles.Contains("agari0") Then
                Dim cs As CellStyle = .Styles.Add("agari0")
                cs.BackColor = Color.Yellow
                '
                cs = .Styles.Add("normal")
                cs.BackColor = Color.White
                cs.ForeColor = Color.Black
                '
                cs = .Styles.Add("span7")
                cs.BackColor = Color.Gold
                '
                cs = .Styles.Add("torikesi")
                cs.BackColor = Color.LightGray
                '
                cs = .Styles.Add("cyakusa0")
                cs.BackColor = Color.Gold
            End If

        End With
        ' イベントの追加（フィルターの適用ボタンが表示されない不具合の回避）
        AddHandler flx.MouseClick, AddressOf C1FlexGrid1_MouseClick
    End Sub

    Private Sub C1FlexGrid1_MouseClick(sender As Object, e As MouseEventArgs)
        If flx.HitTest(e.Location).Type = HitTestTypeEnum.FilterIcon Then
            For Each frm As Form In Application.OpenForms
                If frm.Name = "FilterEditorForm" AndAlso frm.GetType().ToString() = "C1.Win.C1FlexGrid.FilterEditorForm" Then
                    Dim wFrm As Integer = 600
                    frm.MaximumSize = New Size(wFrm, 400)
                    frm.Width = wFrm
                    frm.Controls(1).Width = frm.Width
                End If
            Next
        End If
    End Sub


    Private Sub flx_MouseDown(sender As Object, e As MouseEventArgs) Handles flx.MouseDown
        Dim jcol As Integer = flx.MouseCol
        Dim jrow As Integer = flx.MouseRow
        If jcol < 0 OrElse jcol > flx.Cols.Count - 1 OrElse jrow < flx.Rows.Fixed OrElse jrow > flx.Rows.Count - 1 Then
            flx.Col = -1
            flx.Row = -1
            flx.HighLight = HighLightEnum.Never
        Else
            flx.HighLight = HighLightEnum.Always
        End If
    End Sub

    Private Sub PaintTable()
        With flx
            .Redraw = False
            For jrow As Integer = .Rows.Fixed To .Rows.Count - 1
                For jcol As Integer = FlxCol.histStart To FlxCol.kyoriScore - 1 Step 2
                    If IsNumeric(.Item(jrow, jcol)) Then
                        If CSng(.Item(jrow, jcol)) <= NumericUpDown1.Value Then
                            .SetCellStyle(jrow, jcol, "agari0")
                        Else
                            .SetCellStyle(jrow, jcol, "normal")
                        End If
                    End If

                    If IsNumeric(.Item(jrow, jcol + 1)) Then
                        If CSng(.Item(jrow, jcol + 1)) <= NumericUpDown2.Value Then
                            .SetCellStyle(jrow, jcol + 1, "cyakusa0")
                        Else
                            .SetCellStyle(jrow, jcol + 1, "normal")
                        End If
                    End If
                Next
            Next
            .Redraw = True
        End With
    End Sub

    Private Sub BtnRedisp_Click(sender As Object, e As EventArgs) Handles BtnRedisp.Click
        PaintTable()
    End Sub

    Private Sub BtnHistGet_Click(sender As Object, e As EventArgs) Handles BtnHistGet.Click
        ClearWebPageAccessCounter()
        SetUpFlx()
        ListBox2.Items.Clear()
        spanScore.Clear()
        cyakujun.Clear()
        agarisa1.Clear()
        agarisa2.Clear()
        agarisa3.Clear()
        agarisa4.Clear()
        cyakusa1.Clear()
        cyakusa2.Clear()
        cyakusa3.Clear()
        cyakusa4.Clear()

        Dim errmsg As String = ""

        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = conn.CreateCommand
            Dim cmd2 As SQLiteCommand = conn.CreateCommand
            Try
                conn.Open()
                errmsg = oShortRaceName.load(cmd)
                If errmsg.Length > 0 Then
                    Exit Try
                End If
                cmd.CommandText = "SELECT R.dt, R.kyori, R.type_code, R.race_name, K.cyakujun, K.bamei, K.ninki FROM RaceHeader R INNER JOIN Kekka K ON R.id=K.race_header_id WHERE K.cyakujun>0"
                Dim sql As String = ""
                If CbJo.SelectedIndex > 0 Then
                    sql &= " AND R.jo_code=@jo_code"
                    cmd.Parameters.AddWithValue("@jo_code", GetKeibajoCode(CbJo.Text))
                End If
                If CbKyori.SelectedIndex > 0 Then
                    sql &= " AND R.kyori=@kyori"
                    cmd.Parameters.AddWithValue("@kyori", CInt(CbKyori.Text))
                End If
                If CbSyubetu.SelectedIndex > 0 Then
                    sql &= " AND R.type_code=@type_code"
                    cmd.Parameters.AddWithValue("@type_code", CbSyubetu.SelectedIndex)
                End If
                If CbRacename.Text.Trim.Length > 0 Then
                    'sql &= " AND R.race_name=@race_name"
                    sql &= " AND R.race_name like @race_name"
                    cmd.Parameters.AddWithValue("@race_name", "%" & CbRacename.Text & "%")
                End If
                If CbGrade.SelectedIndex > 0 Then
                    sql &= " AND R.class_code=@class_code"
                    cmd.Parameters.AddWithValue("@class_code", CbGrade.SelectedIndex - 1)
                End If
                If CbCyakujun.SelectedIndex > 0 Then
                    If RbInai.Checked Then
                        sql &= " AND K.cyakujun<=@cyakujun"
                    Else
                        sql &= " AND K.cyakujun>=@cyakujun"
                    End If
                    cmd.Parameters.AddWithValue("@cyakujun", CbCyakujun.SelectedIndex)
                End If
                If CbMonth.SelectedIndex > 0 Then
                    sql &= " AND strftime('%m', R.dt) = @tuki"
                    cmd.Parameters.AddWithValue("@tuki", CbMonth.SelectedIndex.ToString("D2"))
                End If
                If sql.Length > 0 Then
                    cmd.CommandText &= sql
                End If
                flx.Redraw = False
                Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    errmsg = GetAgarisaCyakusa(cmd2, r("bamei"), r("cyakujun"), r("ninki"), r("dt"), r("race_name"), r("kyori"), GetRaceTypeName(r("type_code")))
                    If errmsg.Length > 0 Then
                        Exit While
                    End If
                End While
                r.Close()
                flx.Redraw = True
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using

        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        Else
            ListBox2.Items.Add("COUNT=" & (cyakujun.Count).ToString)
            Dim cnt As Integer = 0
            Dim cnt2 As Integer = 0
            Dim cnt3 As Integer = 0
            Dim cnt4 As Integer = 0
            Dim cnt5 As Integer = 0
            Dim cnt6 As Integer = 0
            Dim cnt7 As Integer = 0
            Dim cnt8 As Integer = 0
            Dim cnt9(4) As Integer
            Dim cnt10(4) As Integer
            Dim cnt11(4) As Integer
            Dim cnt12(4) As Integer
            Dim cnt13(4) As Integer
            For j As Integer = 0 To 4
                cnt9(j) = 0
                cnt10(j) = 0
                cnt11(j) = 0
                cnt12(j) = 0
                cnt13(j) = 0
            Next
            Dim tmpcnt As Integer
            Dim sikii As Single = NumericUpDown1.Value

            ' 度数分布を格納する辞書
            Dim frequencyDistribution As New Dictionary(Of Integer, Integer)

            'spanScoreに対する着順のカウント
            For j As Integer = 0 To spanScore.Count - 1
                Dim Number As Integer = spanScore(j)
                If frequencyDistribution.ContainsKey(Number) Then
                    frequencyDistribution(Number) += cyakujun2score(cyakujun(j))
                Else
                    frequencyDistribution(Number) = cyakujun2score(cyakujun(j))
                End If
            Next

            ' 度数分布を度数の多い順にソート
            Dim sortedDistribution = frequencyDistribution.OrderByDescending(Function(kvp) kvp.Value)

            For j As Integer = 0 To spanScore.Count - 1
                tmpcnt = 0
                If spanScore(j) = 0 Then
                    cnt += 1
                ElseIf spanScore(j) = 2000000 Then
                    cnt2 += 1
                ElseIf spanScore(j) = 1000000 Then
                    cnt3 += 1
                ElseIf spanScore(j) = 10000 Then
                    cnt4 += 1
                ElseIf spanScore(j) = 100 Then
                    cnt5 += 1
                ElseIf spanScore(j) = 1 Then
                    cnt6 += 1
                ElseIf spanScore(j) = 101 Then
                    cnt7 += 1
                ElseIf spanScore(j) <= 10 Then
                    cnt8 += 1
                End If

                Dim ag_idx As Integer = GetAgarisaIdx(agarisa1(j))
                If ag_idx >= 0 Then
                    cnt9(ag_idx) += 1
                End If

                ag_idx = GetAgarisaIdx(agarisa2(j))
                If ag_idx >= 0 Then
                    cnt10(ag_idx) += 1
                End If

                ag_idx = GetAgarisaIdx(agarisa3(j))
                If ag_idx >= 0 Then
                    cnt11(ag_idx) += 1
                End If

                ag_idx = GetAgarisaIdx(agarisa4(j))
                If ag_idx >= 0 Then
                    cnt12(ag_idx) += 1
                End If

                If agarisa1(j) <= sikii Then
                    tmpcnt += 1
                End If
                If agarisa2(j) <= sikii Then
                    tmpcnt += 1
                End If
                If agarisa3(j) <= sikii Then
                    tmpcnt += 1
                End If
                If agarisa4(j) <= sikii Then
                    tmpcnt += 1
                End If

                cnt13(tmpcnt) += 1
            Next
            ListBox2.Items.Add("上がり差 <=0, 0.3, 0.6, 0.9, **")
            ListBox2.Items.Add("1走前 " & intAry2str(cnt9))
            ListBox2.Items.Add("2走前 " & intAry2str(cnt10))
            ListBox2.Items.Add("3走前 " & intAry2str(cnt11))
            ListBox2.Items.Add("4走前 " & intAry2str(cnt12))
            ListBox2.Items.Add("直近４走の上がり差 <=" & sikii.ToString("F1") & "の回数")
            ListBox2.Items.Add("0回 " & cnt13(0).ToString & " 1回 " & cnt13(1).ToString & " 2回 " & cnt13(2).ToString & " 3回 " & cnt13(3).ToString & " 4回 " & cnt13(4).ToString)

            ListBox2.Items.Add("*** SpanScore ***")
            If chkDosu.Checked Then
                For Each kvp As KeyValuePair(Of Integer, Integer) In sortedDistribution
                    ListBox2.Items.Add($"{AnaValClass.Score2String(kvp.Key)}  | {AnaValClass.Score2String(kvp.Value)}")
                Next
            Else
                ListBox2.Items.Add("－：" & cnt.ToString)
                ListBox2.Items.Add("2-0-0-0：" & cnt2.ToString)
                ListBox2.Items.Add("1-0-0-0：" & cnt3.ToString)
                ListBox2.Items.Add("0-1-0-0：" & cnt4.ToString)
                ListBox2.Items.Add("0-0-1-0：" & cnt5.ToString)
                ListBox2.Items.Add("0-0-0-1：" & cnt6.ToString)
                ListBox2.Items.Add("0-0-1-1：" & cnt7.ToString)
                ListBox2.Items.Add("0-0-0-2～10：" & cnt8.ToString)
            End If
            PaintTable()
            flx.AutoSizeCols()
            flx.AutoSizeRows()
        End If
        showWebPageAccessCounter()
    End Sub

    '馬名を指定して直近４走の上り差と着差をagarisa1-4, cyakusa1-4にセットする
    '
    Private Function GetAgarisaCyakusa(ByVal cmd As SQLiteCommand, ByVal arg_bamei As String, ByVal arg_cyakujun As Integer, ByVal arg_ninki As Integer,
                                                                    ByVal arg_dt_race As Date, ByVal arg_racename As String, ByVal arg_kyori As Integer,
                                                                    ByVal arg_syubetu As String) As String
        Dim oUmaHist As New umaHistListClass
        Dim oUmaHeader As UmaHeaderClass = oUmaHist.umaHeader
        Try
            Dim errmsg As String = oUmaHeader.load(cmd, arg_bamei)
            If errmsg.Length = 0 AndAlso oUmaHeader.rec_id > 0 Then
                errmsg = oUmaHist.load(cmd, oUmaHeader.rec_id, arg_dt_race)
                If errmsg.Length = 0 Then
                    Dim kekkaList As New KekkaListClass
                    Dim rA As New raceAnanClass
                    rA.spanScore = oUmaHist.GetSpanScore(arg_dt_race, rA.spanVal)
                    rA.dateScore = oUmaHist.GetSameDateSameKyoriScore(arg_dt_race, arg_kyori, arg_syubetu, rA.kyoriScore)

                    Dim agarisa(3) As Single
                    Dim cyakusa(3) As Single
                    Dim cnt As Integer = 0
                    For j As Integer = 0 To oUmaHist.cnt - 1
                        Dim oS As UmaHistClass = oUmaHist.GetBodyRef(j)
                        Dim shortname As String = oS.racename
                        If DateDiff(DateInterval.Day, oS.dt, arg_dt_race) > 1 AndAlso oS.href.Trim.Length > 0 Then
                            kekkaList.init()
                            Dim oRaceHead As RaceHeaderClass = kekkaList.raceHeader
                            oS.racename = oShortRaceName.GetLongName(shortname)
                            errmsg = oRaceHead.loadByUmaHist(cmd, oS)
                            If errmsg.Length > 0 Then
                                Return errmsg
                            End If
                            'DB未登録レースはWebPageをロードして情報取得する
                            If oRaceHead.id < 0 Then
                                Dim contents As String = GetWebPageText(makeJRAurl(oS.href))
                                GetKekka(contents, kekkaList)
                                oRaceHead.keibajo = GetWhenWhere(contents, oRaceHead.dt)
                                oRaceHead.race_no = GetRaceNo(contents)
                                oRaceHead.race_name = GetRaceName(contents, oRaceHead.grade)
                                oRaceHead.class_name = GetClassCource(contents, oRaceHead.kyori, oRaceHead.syubetu)
                                oRaceHead.class_code = oRaceHead.GetClassCode()
                                oRaceHead.tosu = kekkaList.cnt
                                oS.racename = oRaceHead.race_name
                                errmsg = oRaceHead.loadByUmaHist(cmd, oS)
                                If errmsg.Length > 0 Then
                                    Return errmsg
                                End If
                                '短縮レース名と正式レース名の対比表に登録する
                                If oRaceHead.race_name.Trim.Length > 0 Then
                                    If shortname <> oRaceHead.race_name Then
                                        errmsg = oShortRaceName.addNew(cmd, shortname, oRaceHead.race_name)
                                        If errmsg.Length > 0 Then
                                            Return errmsg
                                        End If
                                    End If
                                End If
                            End If
                            If oRaceHead.race_name.Trim.Length > 0 Then
                                'DB未登録レースはここで登録する
                                If oRaceHead.id < 0 Then
                                    kekkaList.setCyakusa()
                                    errmsg = SaveRaceKekka(cmd, kekkaList)
                                    If errmsg.Length > 0 Then
                                        Return errmsg
                                    End If
                                Else
                                    errmsg = kekkaList.load(cmd, oRaceHead.id)
                                    If errmsg.Length > 0 Then
                                        Return errmsg
                                    End If
                                End If
                                kekkaList.correctCyakusa(oRaceHead)
                                kekkaList.setAgarisa(oRaceHead)
                                Dim oK As KekkaClass = kekkaList.GetBodyRefByBamei(arg_bamei)
                                If oK IsNot Nothing Then
                                    agarisa(cnt) = oK.agarisa
                                    cyakusa(cnt) = oK.cyakusa
                                    cnt += 1
                                    If cnt > 3 Then
                                        Exit For
                                    End If
                                Else
                                    Dim dmy As Integer = 0
                                End If
                            End If
                        End If
                    Next
                    If cnt > 0 Then
                        Dim xx(FlxCol.cols - 1) As String
                        xx(FlxCol.dt) = arg_dt_race.ToString("yyyy.MM.dd")
                        xx(FlxCol.racename) = arg_racename
                        xx(FlxCol.bamei) = arg_bamei
                        xx(FlxCol.cyakujun) = arg_cyakujun.ToString("D2")
                        xx(FlxCol.ninki) = arg_ninki
                        Dim sa As Integer = arg_ninki - arg_cyakujun
                        If sa >= 0 Then
                            xx(FlxCol.sa) = "+" & sa.ToString("D2")
                        Else
                            xx(FlxCol.sa) = sa.ToString("D2")
                        End If
                        xx(FlxCol.spanVal) = AnaValClass.Score2String(rA.spanScore)
                        For i As Integer = 0 To HIS_CNT - 1
                            xx(FlxCol.histStart + 2 * i + 0) = time2str(agarisa(i)) 'agarisa1～4
                            xx(FlxCol.histStart + 2 * i + 1) = time2str(cyakusa(i)) 'cyakusa1～4
                        Next
                        xx(FlxCol.kyoriScore) = AnaValClass.Score2String(rA.kyoriScore)
                        xx(FlxCol.dateScore) = AnaValClass.Score2String(rA.dateScore)
                        flx.AddItem(xx)
                        '
                        cyakujun.Add(arg_cyakujun)
                        spanScore.Add(rA.spanScore)
                        agarisa1.Add(agarisa(0))
                        agarisa2.Add(agarisa(1))
                        agarisa3.Add(agarisa(2))
                        agarisa4.Add(agarisa(3))
                        cyakusa1.Add(cyakusa(0))
                        cyakusa2.Add(cyakusa(1))
                        cyakusa3.Add(cyakusa(2))
                        cyakusa4.Add(cyakusa(3))
                    End If
                End If
            End If
            Return ""
        Catch ex As Exception
            Return "GetAgarisaCyakusa() " & ex.Message
        End Try
    End Function

    Private Function SaveRaceKekka(ByVal cmd As SQLiteCommand, ByVal kekkaList As KekkaListClass) As String
        Dim oK As RaceHeaderClass = kekkaList.raceHeader
        Dim errmsg As String = oK.addNew(cmd)
        If errmsg.Length = 0 Then
            errmsg = kekkaList.save(cmd)
        End If
        Return errmsg
    End Function

    Private Function GetAgarisaIdx(ByVal agarisa As Single) As Integer
        If agarisa = DMY_VAL Then
            Return -1
        End If

        If agarisa <= 0 Then
            Return 0
        ElseIf agarisa <= 0.3 Then
            Return 1
        ElseIf agarisa <= 0.6 Then
            Return 2
        ElseIf agarisa <= 0.9 Then
            Return 3
        Else
            Return 4
        End If
    End Function

    Private Function time2str(ByVal tm As Single) As String
        If tm <= DMY_VAL Then
            Return ""
        Else
            If tm < 0 Then
                Return tm.ToString("F1")
            Else
                Return "+" & tm.ToString("F1")
            End If
        End If
    End Function

    Private Function intAry2str(ByVal ary() As Integer) As String
        Dim ss As String = ""
        For j As Integer = 0 To ary.Length - 1
            ss &= n2s(ary(j)) & ", "
        Next
        Return ss
    End Function

    Private Function n2s(ByVal n As Integer) As String
        If n < 100 Then
            Dim ss As String = "  " & n.ToString
            Return ss.Substring(ss.Length - 2)
        Else
            Return n.ToString
        End If
    End Function

    Private Sub BtnJokenCls_Click(sender As Object, e As EventArgs) Handles BtnJokenCls.Click
        CbJo.SelectedIndex = 0
        CbSyubetu.SelectedIndex = 0
        CbKyori.SelectedIndex = 0
        CbGrade.SelectedIndex = 0
        CbMonth.SelectedIndex = 0
        CbCyakujun.SelectedIndex = 2
        CbRacename.SelectedIndex = 0
    End Sub

    Private Sub BtnWinRate_Click(sender As Object, e As EventArgs) Handles BtnWinRate.Click
        Dim a As New WinRateForm
        a.entry(spanScore, cyakujun, agarisa1, agarisa2, agarisa3, agarisa4)
    End Sub

    Private Sub BtnFilterClear_Click(sender As Object, e As EventArgs) Handles BtnFilterClear.Click
        FlexFilterClear(flx)
    End Sub

    'フィルタークリア
    Public Sub FlexFilterClear(ByVal arg_flx As C1FlexGrid)
        With arg_flx
            Dim redraw As Boolean = .Redraw
            .Redraw = False
            '.ClearFilter() <==これだとエラーが発生する！ 
            For j As Integer = 0 To .Cols.Count - 1
                If .Cols(j).AllowFiltering = AllowFiltering.Default OrElse .Cols(j).AllowFiltering = AllowFiltering.ByCondition Then
                    .ClearFilter(j)
                End If
            Next
            .Redraw = redraw
        End With
    End Sub

    '係数検証
    Private Sub BtnCoefReview_Click(sender As Object, e As EventArgs) Handles BtnCoefReview.Click
        Dim spanCoefRankCnt(10, 2) As Integer
        Dim dateCoefRankCnt(10, 2) As Integer
        Dim distCoefRankCnt(10, 2) As Integer
        Dim errmsg As String = ""

        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = conn.CreateCommand
            conn.Open()
            Try
                cmd.CommandText = "SELECT A.* FROM RaceHeader R INNER JOIN AnaVal A ON R.id=A.rhead_id WHERE A.cyakujun>0"
                Dim sql As String = ""
                If CbJo.SelectedIndex > 0 Then
                    sql &= " AND R.jo_code=@jo_code"
                    cmd.Parameters.AddWithValue("@jo_code", GetKeibajoCode(CbJo.Text))
                End If
                If CbKyori.SelectedIndex > 0 Then
                    sql &= " AND R.kyori=@kyori"
                    cmd.Parameters.AddWithValue("@kyori", CInt(CbKyori.Text))
                End If
                If CbSyubetu.SelectedIndex > 0 Then
                    sql &= " AND R.type_code=@type_code"
                    cmd.Parameters.AddWithValue("@type_code", CbSyubetu.SelectedIndex)
                End If
                If CbRacename.SelectedIndex > 0 Then
                    sql &= " AND R.race_name=@race_name"
                    cmd.Parameters.AddWithValue("@race_name", CbRacename.Text)
                End If
                If CbGrade.SelectedIndex > 0 Then
                    sql &= " AND R.class_code=@class_code"
                    cmd.Parameters.AddWithValue("@class_code", CbGrade.SelectedIndex - 1)
                End If
                If CbCyakujun.SelectedIndex > 0 Then
                    If RbInai.Checked Then
                        sql &= " AND A.cyakujun<=@cyakujun"
                    Else
                        sql &= " AND A.cyakujun>=@cyakujun"
                    End If
                    cmd.Parameters.AddWithValue("@cyakujun", CbCyakujun.SelectedIndex)
                End If
                If CbMonth.SelectedIndex > 0 Then
                    sql &= " AND strftime('%m', R.dt) = @tuki"
                    cmd.Parameters.AddWithValue("@tuki", CbMonth.SelectedIndex.ToString("D2"))
                End If
                If sql.Length > 0 Then
                    cmd.CommandText &= sql
                End If
                Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
                Dim CyakujunNinkiSa As Integer
                Dim CyakujunNinkiSaIdx As Integer
                Dim rank As Integer
                While r.Read
                    'CyakujunNinkiSa = CInt(r("cyakujun")) - CInt(r("ninki"))
                    'CyakujunNinkiSaIdx = GetCyakujunNinkiSaIndex(CyakujunNinkiSa)
                    Select Case CInt(r("cyakujun"))
                        Case 1, 2
                            CyakujunNinkiSaIdx = 0
                        Case 3
                            CyakujunNinkiSaIdx = 1
                        Case Else
                            CyakujunNinkiSaIdx = 2
                    End Select

                    rank = GetCoefRank(GetScoreCoefficient(r("spanScore")))
                    spanCoefRankCnt(rank, CyakujunNinkiSaIdx) += 1
                    rank = GetCoefRank(GetScoreCoefficient(r("dateScore")))
                    dateCoefRankCnt(rank, CyakujunNinkiSaIdx) += 1
                    rank = GetCoefRank(GetScoreCoefficient(r("kyoriScore")))
                    distCoefRankCnt(rank, CyakujunNinkiSaIdx) += 1

                    'CyakujunNinkiSa = NinkiCyakujunPoint(CInt(r("cyakujun")), CInt(r("ninki")))
                    'rank = GetCoefRank(GetScoreCoefficient(r("spanScore")))
                    'spanCoefRankCnt(rank, CyakujunNinkiSaIdx) += CyakujunNinkiSa
                    'rank = GetCoefRank(GetScoreCoefficient(r("dateScore")))
                    'dateCoefRankCnt(rank, CyakujunNinkiSaIdx) += CyakujunNinkiSa
                    'rank = GetCoefRank(GetScoreCoefficient(r("kyoriScore")))
                    'distCoefRankCnt(rank, CyakujunNinkiSaIdx) += CyakujunNinkiSa
                End While
                r.Close()
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        Else
            Dim a As New CoefReviewForm
            a.entry(spanCoefRankCnt, dateCoefRankCnt, distCoefRankCnt)
        End If
    End Sub

    '人気と着順から得点を決める
    Private Function NinkiCyakujunPoint(ByVal ninki As Integer, ByVal cyakujun As Integer) As Integer
        Select Case ninki
            Case 1
                Select Case cyakujun
                    Case 1
                        Return 2
                    Case 2, 3
                        Return 1
                    Case 4, 5
                        Return -2
                    Case 6, 7, 8, 9
                        Return -3
                    Case Else
                        Return -4
                End Select
            Case 2, 3
                Select Case cyakujun
                    Case 1
                        Return 3
                    Case 2, 3
                        Return 1
                    Case 4, 5
                        Return -1
                    Case 6, 7, 8, 9
                        Return -2
                    Case Else
                        Return -3
                End Select
            Case 4, 5
                Select Case cyakujun
                    Case 1
                        Return 4
                    Case 2, 3
                        Return 2
                    Case 4, 5
                        Return 0
                    Case 6, 7, 8, 9
                        Return -1
                    Case Else
                        Return -2
                End Select
            Case 6, 7, 8, 9
                Select Case cyakujun
                    Case 1
                        Return 5
                    Case 2, 3
                        Return 2
                    Case 4, 5
                        Return 1
                    Case 6, 7, 8, 9
                        Return 0
                    Case Else
                        Return -1
                End Select
            Case Else
                Select Case cyakujun
                    Case 1
                        Return 6
                    Case 2, 3
                        Return 3
                    Case 4, 5
                        Return 1
                    Case 6, 7, 8, 9
                        Return 0
                    Case Else
                        Return 0
                End Select
        End Select
    End Function

    Private Function GetCyakujunNinkiSaIndex(ByVal CyakujunNinkiSa As Integer) As Integer
        If CyakujunNinkiSa < 0 Then
            Return 0
        ElseIf CyakujunNinkiSa = 0 Then
            Return 1
        Else
            Return 2
        End If
    End Function

    Private Function GetCoefRank(ByVal coef As Double) As Integer
        If coef = 1 Then
            Return 5
        ElseIf coef > 1 Then
            Dim sa As Double = coef - 1
            If sa < 0.075 Then
                Return 4
            ElseIf sa < 0.15 Then
                Return 3
            ElseIf sa < 0.225 Then
                Return 2
            ElseIf sa < 0.3 Then
                Return 1
            Else
                Return 0
            End If
        Else
            Dim sa As Double = 1 - coef
            If sa < 0.0251 Then
                Return 6
            ElseIf sa < 0.051 Then
                Return 7
            ElseIf sa < 0.0751 Then
                Return 8
            ElseIf sa < 0.1001 Then
                Return 9
            Else
                Return 10
            End If
        End If
    End Function
End Class
