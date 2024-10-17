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
        spanVal = 5
        histStart = 6
        kyoriScore = histStart + HIS_CNT * 2
        dateScore
        cols = dateScore + 1
    End Enum

    Private anaList As New raceAnaListClass

    Public Sub New()
        InitializeComponent()
        SetupCombobox()
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
            .Items.Add("1着のみ")
            .Items.Add("1,2着")
            .Items.Add("1,2,3着")
            .SelectedIndex = 0
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
            .Cols.Fixed = 2
            .Item(0, FlxCol.dt) = "日付"
            .Item(0, FlxCol.racename) = "レース名"
            .Item(0, FlxCol.bamei) = "馬名"
            .Item(0, FlxCol.cyakujun) = "着順 "
            .Item(0, FlxCol.ninki) = "人気"
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
                If j > 0 AndAlso j <= 4 Then
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

    Private Sub PaintTable(ByVal sblist As raceAnaListClass)

        For j As Integer = 0 To sblist.cnt - 1
            Dim oUma As raceAnanClass = sblist.GetBodyRef(j)
            '並べ替えに対応するため馬名で行を検索する
            Dim jrow As Integer
            For jrow = flx.Rows.Fixed To flx.Rows.Count - 1
                If oUma.bamei = flx.Item(jrow, FlxCol.bamei) Then
                    Exit For
                End If
            Next

            Dim torikesi As Boolean = False
            If oUma.waku > 0 AndAlso oUma.umaban < 0 Then
                torikesi = True
            End If

            If torikesi Then
                For jcol As Integer = FlxCol.chk To flx.Cols.Count - 1
                    flx.SetCellStyle(jrow, jcol, "torikesi")
                Next
            Else
                If oUma.isGoodSpan Then
                    flx.SetCellStyle(jrow, FlxCol.spanVal, "span7")
                Else
                    flx.SetCellStyle(jrow, FlxCol.spanVal, "normal")
                End If
                For i As Integer = 0 To 5
                    If oUma.isGoodHist(i, NumericUpDown1.Value) Then
                        flx.SetCellStyle(jrow, FlxCol.histStart + i, "agari0")
                    Else
                        flx.SetCellStyle(jrow, FlxCol.histStart + i, "normal")
                    End If
                Next
            End If

        Next
    End Sub

    Private Sub BtnRedisp_Click(sender As Object, e As EventArgs) Handles BtnRedisp.Click
        'PaintTable(anaList)
    End Sub

    Private Sub BtnHistGet_Click(sender As Object, e As EventArgs) Handles BtnHistGet.Click
        SetUpFlx()
        ListBox2.Items.Clear()
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim errmsg As String = ""
            Dim spanScore As New List(Of Integer)
            Dim cyakujun As New List(Of Integer)
            Dim agarisa1 As New List(Of Single)
            Dim agarisa2 As New List(Of Single)
            Dim agarisa3 As New List(Of Single)
            Dim agarisa4 As New List(Of Single)

            Dim cmd As SQLiteCommand = conn.CreateCommand
            conn.Open()
            Try
                cmd.CommandText = "SELECT A.*, R.dt, R.race_name FROM RaceHeader R INNER JOIN AnaVal A ON R.id=A.rhead_id"
                Dim sql As String = ""
                If CbJo.SelectedIndex > 0 Then
                    sql &= "R.jo_code=@jo_code"
                    cmd.Parameters.AddWithValue("@jo_code", GetKeibajoCode(CbJo.Text))
                End If
                If CbKyori.SelectedIndex > 0 Then
                    If sql.Length > 0 Then
                        sql &= " AND "
                    End If
                    sql &= "R.kyori=@kyori"
                    cmd.Parameters.AddWithValue("@kyori", CInt(CbKyori.Text))
                End If
                If CbSyubetu.SelectedIndex > 0 Then
                    If sql.Length > 0 Then
                        sql &= " AND "
                    End If
                    sql &= "R.type_code=@type_code"
                    cmd.Parameters.AddWithValue("@type_code", CbSyubetu.SelectedIndex)
                End If
                If CbRacename.SelectedIndex > 0 Then
                    If sql.Length > 0 Then
                        sql &= " AND "
                    End If
                    sql &= "R.race_name=@race_name"
                    cmd.Parameters.AddWithValue("@race_name", CbRacename.Text)
                End If
                If CbGrade.SelectedIndex > 0 Then
                    If sql.Length > 0 Then
                        sql &= " AND "
                    End If
                    sql &= "R.class_code=@class_code"
                    cmd.Parameters.AddWithValue("@class_code", CbGrade.SelectedIndex - 1)
                End If
                If CbCyakujun.SelectedIndex > 0 Then
                    If sql.Length > 0 Then
                        sql &= " AND "
                    End If
                    sql &= "A.cyakujun<=@cyakujun"
                    cmd.Parameters.AddWithValue("@cyakujun", CbCyakujun.SelectedIndex)
                End If
                If CbMonth.SelectedIndex > 0 Then
                    If sql.Length > 0 Then
                        sql &= " AND "
                    End If
                    sql &= "strftime('%m', R.dt) = @tuki"
                    cmd.Parameters.AddWithValue("@tuki", CbMonth.SelectedIndex.ToString("D2"))
                End If
                If sql.Length > 0 Then
                    cmd.CommandText &= " WHERE " & sql
                End If
                Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
                Dim xx(FlxCol.cols - 1) As String
                While r.Read
                    xx(FlxCol.dt) = CDate(r("dt")).ToString("yyyy.MM.dd")
                    xx(FlxCol.racename) = r("race_name")
                    xx(FlxCol.bamei) = r("bamei")
                    xx(FlxCol.cyakujun) = r("cyakujun")
                    xx(FlxCol.ninki) = r("ninki")
                    xx(FlxCol.spanVal) = AnaValClass.Score2String(r("spanScore"))
                    For i As Integer = 0 To HIS_CNT - 1
                        xx(FlxCol.histStart + 2 * i + 0) = time2str(r.GetFloat(4 + 2 * i)) 'agarisa1～4
                        xx(FlxCol.histStart + 2 * i + 1) = time2str(r.GetFloat(5 + 2 * i)) 'cyakusa1～4
                    Next
                    xx(FlxCol.kyoriScore) = AnaValClass.Score2String(r("kyoriScore"))
                    xx(FlxCol.dateScore) = AnaValClass.Score2String(r("dateScore"))
                    flx.AddItem(xx)
                    '
                    cyakujun.Add(r("cyakujun"))
                    spanScore.Add(r("spanScore"))
                    agarisa1.Add(r("agarisa1"))
                    agarisa2.Add(r("agarisa2"))
                    agarisa3.Add(r("agarisa3"))
                    agarisa4.Add(r("agarisa4"))
                End While
                r.Close()
                flx.AutoSizeCols()
                flx.AutoSizeRows()

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

                ' 度数分布を計算
                For Each number As Integer In spanScore
                    If frequencyDistribution.ContainsKey(number) Then
                        frequencyDistribution(number) += 1
                    Else
                        frequencyDistribution(number) = 1
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

                    If agarisa1(j) <= 0 Then
                        cnt9(0) += 1
                    ElseIf agarisa1(j) <= 0.3 Then
                        cnt9(1) += 1
                    ElseIf agarisa1(j) <= 0.6 Then
                        cnt9(2) += 1
                    ElseIf agarisa1(j) <= 0.9 Then
                        cnt9(3) += 1
                    Else
                        cnt9(4) += 1
                    End If

                    If agarisa2(j) <= 0 Then
                        cnt10(0) += 1
                    ElseIf agarisa2(j) <= 0.3 Then
                        cnt10(1) += 1
                    ElseIf agarisa2(j) <= 0.6 Then
                        cnt10(2) += 1
                    ElseIf agarisa2(j) <= 0.9 Then
                        cnt10(3) += 1
                    Else
                        cnt10(4) += 1
                    End If

                    If agarisa3(j) <= 0 Then
                        cnt11(0) += 1
                    ElseIf agarisa3(j) <= 0.3 Then
                        cnt11(1) += 1
                    ElseIf agarisa3(j) <= 0.6 Then
                        cnt11(2) += 1
                    ElseIf agarisa3(j) <= 0.9 Then
                        cnt11(3) += 1
                    Else
                        cnt11(4) += 1
                    End If

                    If agarisa4(j) <= 0 Then
                        cnt12(0) += 1
                    ElseIf agarisa4(j) <= 0.3 Then
                        cnt12(1) += 1
                    ElseIf agarisa4(j) <= 0.6 Then
                        cnt12(2) += 1
                    ElseIf agarisa4(j) <= 0.9 Then
                        cnt12(3) += 1
                    Else
                        cnt12(4) += 1
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
                        ListBox2.Items.Add($"{AnaValClass.Score2String(kvp.Key)}  | {kvp.Value}")
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

            Catch ex As Exception
                errmsg = ex.Message
            End Try

            If errmsg.Length > 0 Then
                MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            End If
        End Using

    End Sub
    Private Function time2str(ByVal tm As Single) As String
        If tm <= DMY_VAL Then
            Return ""
        Else
            Return tm.ToString("F1")
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
        CbCyakujun.SelectedIndex = 0
        CbRacename.SelectedIndex = 0
    End Sub
End Class
