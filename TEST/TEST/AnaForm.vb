Imports C1.Win.C1FlexGrid
Imports System.Data.SQLite

Public Class AnaForm

    Private Const HIS_CNT As Integer = 6
    Private Enum FlxCol
        waku = 0
        umaban = 1
        bamei = 2
        chk = 3
        ninki = 4
        spanVal = 5
        histStart = 6
        kyoriScore = histStart + HIS_CNT
        dateScore
        dof_span
        dof_agarisa
        dof_cyakusa
        dof_total
        cols = dof_total + 1
    End Enum

    Private anaList As New raceAnaListClass
    Private oHead As RaceHeaderClass
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
        If Clipboard.ContainsText Then
            Dim tmp As String = Clipboard.GetText
            If InStr(tmp, "https") Then
                txtURL.Text = tmp
            End If
        End If
    End Sub

    '一覧グリッド書式設定
    Private Sub SetUpFlx()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 2
            .Item(0, FlxCol.waku) = "枠番"
            .Item(0, FlxCol.umaban) = "馬番"
            .Item(0, FlxCol.bamei) = "馬名"
            .Item(0, FlxCol.chk) = " マーク "
            .Item(0, FlxCol.ninki) = "人気"
            .Item(0, FlxCol.spanVal) = "前走間隔" & vbLf & "±７日"
            For j As Integer = 0 To 5
                .Item(0, FlxCol.histStart + j) = (j + 1).ToString & "走前"
            Next
            .Item(0, FlxCol.kyoriScore) = "今回距離" & vbLf & "成績"
            .Item(0, FlxCol.dateScore) = "今回日付" & vbLf & "±７日成績"
            .Item(0, FlxCol.dof_span) = "span" & vbLf & "適合度"
            .Item(0, FlxCol.dof_agarisa) = "上差" & vbLf & "適合度"
            .Item(0, FlxCol.dof_cyakusa) = "着差" & vbLf & "適合度"
            .Item(0, FlxCol.dof_total) = "適合度合計"

            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 25

            .Cols(FlxCol.bamei).TextAlign = TextAlignEnum.LeftCenter

            .AllowMerging = AllowMergingEnum.FixedOnly
            .Cols(FlxCol.waku).AllowMerging = True
            .AllowEditing = True

            .AllowSorting = AllowSortingEnum.SingleColumn
            .AllowFiltering = True

            For j As Integer = 0 To .Cols.Count - 1
                If j = FlxCol.chk Then
                    .Cols(j).AllowEditing = True
                    .Cols(j).AllowFiltering = AllowFiltering.Default
                Else
                    .Cols(j).AllowEditing = False
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
                cs = .Styles.Add("waku1")
                cs.BackColor = Color.White
                cs.ForeColor = Color.Black
                '
                cs = .Styles.Add("waku2")
                cs.BackColor = Color.Black
                cs.ForeColor = Color.White
                '
                cs = .Styles.Add("waku3")
                cs.BackColor = Color.Red
                cs.ForeColor = Color.White
                '
                cs = .Styles.Add("waku4")
                cs.BackColor = Color.Blue
                cs.ForeColor = Color.White
                '
                cs = .Styles.Add("waku5")
                cs.BackColor = Color.Yellow
                cs.ForeColor = Color.Black
                '
                cs = .Styles.Add("waku6")
                cs.BackColor = Color.Green
                cs.ForeColor = Color.White
                '
                cs = .Styles.Add("waku7")
                cs.BackColor = Color.Orange
                cs.ForeColor = Color.Black
                '
                cs = .Styles.Add("waku8")
                cs.BackColor = Color.Pink
                cs.ForeColor = Color.Black
                '
                cs = .Styles.Add("mark")
                cs.DataType = Type.GetType("System.String")
                cs.ComboList = "　|×|△|▲|○|◎"
                .Cols(FlxCol.chk).Style = cs
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

    Private Sub ShowTable(ByVal sblist As raceAnaListClass)
        SetUpFlx()
        Dim xx(FlxCol.cols - 1) As String
        For j As Integer = 0 To sblist.cnt - 1
            Dim oUma As raceAnanClass = sblist.GetBodyRef(j)

            If oUma.waku > 0 Then
                xx(FlxCol.waku) = oUma.waku
                If oUma.umaban > 0 Then
                    xx(FlxCol.umaban) = oUma.umaban
                Else
                    xx(FlxCol.umaban) = "取消"
                End If
                If oUma.ninki > 0 Then
                    xx(FlxCol.ninki) = oUma.ninki
                Else
                    xx(FlxCol.ninki) = ""
                End If
            Else
                xx(FlxCol.waku) = ""
                xx(FlxCol.umaban) = ""
                xx(FlxCol.ninki) = ""
            End If
            xx(FlxCol.bamei) = oUma.bamei
            If xx(FlxCol.umaban) = "取消" Then
                xx(FlxCol.spanVal) = ""
                For i As Integer = FlxCol.histStart To FlxCol.cols - 1
                    xx(i) = ""
                Next
            Else
                xx(FlxCol.spanVal) = AnaValClass.Score2String(oUma.spanScore) 'oUma.spanVal
                For i As Integer = 0 To 5
                    xx(FlxCol.histStart + i) = oUma.hist(i)
                Next
                xx(FlxCol.kyoriScore) = AnaValClass.Score2String(oUma.kyoriScore)
                xx(FlxCol.dateScore) = AnaValClass.Score2String(oUma.dateScore)
            End If
            flx.AddItem(xx)
        Next
        flx.AutoSizeCols()
        flx.AutoSizeRows()
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

            Select Case oUma.waku
                Case 1
                    flx.SetCellStyle(jrow, FlxCol.waku, "waku1")
                Case 2
                    flx.SetCellStyle(jrow, FlxCol.waku, "waku2")
                Case 3
                    flx.SetCellStyle(jrow, FlxCol.waku, "waku3")
                Case 4
                    flx.SetCellStyle(jrow, FlxCol.waku, "waku4")
                Case 5
                    flx.SetCellStyle(jrow, FlxCol.waku, "waku5")
                Case 6
                    flx.SetCellStyle(jrow, FlxCol.waku, "waku6")
                Case 7
                    flx.SetCellStyle(jrow, FlxCol.waku, "waku7")
                Case 8
                    flx.SetCellStyle(jrow, FlxCol.waku, "waku8")
            End Select
        Next
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Dim url As String = txtURL.Text.Trim
        If url.Length > 0 Then
            Dim fm1 As New Form1 '結果
            Dim fm2 As New Form2 '馬情報
            Dim fm3 As New Form3 '出走馬
            fm3.entry(url)


            ListBox1.Items.Clear()
            oHead = fm3.oRaceHeader

            ListBox1.Items.Add("競馬場：" & oHead.keibajo)
            ListBox1.Items.Add("開催日：" & oHead.dt.ToString("yyyy年MM月dd日"))

            ListBox1.Items.Add("レース名：" & oHead.race_name)
            ListBox1.Items.Add("グレード：" & oHead.grade)

            ListBox1.Items.Add("距離：" & oHead.kyori.ToString)
            ListBox1.Items.Add("種別：" & oHead.syubetu)
            ListBox1.Items.Add("クラス：" & oHead.class_name)
            oHead.class_code = oHead.GetClassCode()
            anaList.init()
            For j As Integer = 0 To fm3.syutubaList.cnt - 1
                lb_msg.Text = (j + 1).ToString & "/" & (fm3.syutubaList.cnt).ToString
                Dim rA As New raceAnanClass
                Dim o As SyutubaClass = fm3.syutubaList.GetBodyRef(j)
                rA.waku = o.waku
                rA.umaban = o.umaban
                rA.bamei = o.bamei
                rA.ninki = o.ninki
                fm2.entry(o.href, oHead.dt)
                rA.spanScore = fm2.umaHistList.GetSpanScore(oHead.dt, rA.spanVal)
                rA.dateScore = fm2.umaHistList.GetSameDateSameKyoriScore(oHead.dt, oHead.kyori, oHead.syubetu, rA.kyoriScore)
                For i As Integer = 0 To fm2.umaHistList.cnt - 1
                    If i > 5 Then
                        Exit For
                    End If

                    lb_msg.Text = (j + 1).ToString & "/" & (fm3.syutubaList.cnt).ToString & " | " & (i + 1).ToString & "/6"
                    Me.Refresh()

                    Dim oS As UmaHistClass = fm2.umaHistList.GetBodyRef(i)

                    'If j = 11 AndAlso i = 0 Then
                    '    MsgBox(o.bamei & " " & oS.racename)
                    'End If

                    fm1.entry(oS.href)
                    rA.hist(i) = fm1.kekkaList.GetAgarisa(o.bamei, oHead.syubetu)
                Next

                anaList.add1(rA)
            Next
            ShowTable(anaList)
            PaintTable(anaList)
            fm1.Close()
            fm2.Close()
            fm3.Close()
        End If
    End Sub

    Private Sub BtnURL_Click(sender As Object, e As EventArgs) Handles BtnURL.Click
        If Clipboard.ContainsText Then
            txtURL.Text = Clipboard.GetText()
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

    Private Sub BtnRedisp_Click(sender As Object, e As EventArgs) Handles BtnRedisp.Click
        PaintTable(anaList)
    End Sub

    Private Sub BtnHistGet_Click(sender As Object, e As EventArgs) Handles BtnHistGet.Click
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

        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim errmsg As String = ""

            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            Try
                cmd.CommandText = "SELECT A.* FROM RaceHeader R INNER JOIN AnaVal A ON R.id=A.rhead_id WHERE R.dt<@dt"
                cmd.Parameters.AddWithValue("@dt", oHead.dt)
                Dim sql As String = ""
                If chkJo.Checked Then
                    sql &= " AND R.jo_code=@jo_code"
                    cmd.Parameters.AddWithValue("@jo_code", oHead.jo_code)
                End If
                If chkKyori.Checked Then
                    sql &= " AND R.kyori=@kyori AND R.type_code=@type_code"
                    cmd.Parameters.AddWithValue("@kyori", oHead.kyori)
                    cmd.Parameters.AddWithValue("@type_code", oHead.type_code)
                End If
                If chkRacename.Checked Then
                    sql &= " AND R.race_name=@race_name"
                    cmd.Parameters.AddWithValue("@race_name", oHead.race_name)
                ElseIf chkRacename2.Checked Then
                    sql &= " AND R.race_name like @race_name"
                    cmd.Parameters.AddWithValue("@race_name", "%" & txtRacename.Text & "%")
                End If
                If chkGrade.Checked Then
                    sql &= " AND R.class_code=@class_code"
                    cmd.Parameters.AddWithValue("@class_code", oHead.class_code)
                End If
                If CbCyakujun.SelectedIndex >= 0 Then
                    sql &= " AND A.cyakujun<=@cyakujun AND A.cyakujun>0"
                    cmd.Parameters.AddWithValue("@cyakujun", CbCyakujun.SelectedIndex + 1)
                End If

                If chkMonth.Checked Then
                    sql &= " AND strftime('%m', R.dt) = @tuki"
                    cmd.Parameters.AddWithValue("@tuki", oHead.dt.Month.ToString("D2"))
                End If

                If sql.Length > 0 Then
                    cmd.CommandText &= sql
                End If
                Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    cyakujun.Add(r("cyakujun"))
                    spanScore.Add(r("spanScore"))
                    agarisa1.Add(r("agarisa1"))
                    agarisa2.Add(r("agarisa2"))
                    agarisa3.Add(r("agarisa3"))
                    agarisa4.Add(r("agarisa4"))
                    cyakusa1.Add(r("cyakusa1"))
                    cyakusa2.Add(r("cyakusa2"))
                    cyakusa3.Add(r("cyakusa3"))
                    cyakusa4.Add(r("cyakusa4"))
                End While
                r.Close()
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

    Private Sub BtnWinRate_Click(sender As Object, e As EventArgs) Handles BtnWinRate.Click
        Dim a As New WinRateForm

        If flx.Row >= flx.Rows.Fixed Then
            Dim tmpcyakusa As Single
            a.entry(spanScore, cyakujun, agarisa1, agarisa2, agarisa3, agarisa4,
                    flx.Item(flx.Row, FlxCol.spanVal),
                    cnvAgarisaStr2Val(flx.Item(flx.Row, FlxCol.histStart + 0), tmpcyakusa),
                    cnvAgarisaStr2Val(flx.Item(flx.Row, FlxCol.histStart + 1), tmpcyakusa),
                    cnvAgarisaStr2Val(flx.Item(flx.Row, FlxCol.histStart + 2), tmpcyakusa),
                    cnvAgarisaStr2Val(flx.Item(flx.Row, FlxCol.histStart + 3), tmpcyakusa)
                    )
        Else
            a.entry(spanScore, cyakujun, agarisa1, agarisa2, agarisa3, agarisa4)
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim a As New raceReviewForm
        a.entry(oHead.race_name)
    End Sub

    Private Sub BtnDof_Click(sender As Object, e As EventArgs) Handles BtnDof.Click
        If spanScore.Count > 0 Then
            Dim cmp_cyakujun As Integer = 1
            If CbCyakujun.SelectedIndex > 0 Then
                cmp_cyakujun = CbCyakujun.SelectedIndex + 1
            End If
            For jrow As Integer = flx.Rows.Fixed To flx.Rows.Count - 1
                If flx.Item(jrow, FlxCol.spanVal) IsNot Nothing Then
                    Dim myScore As Integer = cnvScoreStr2Val(flx.Item(jrow, FlxCol.spanVal))
                    Dim spanPoint As Integer = GetDofSpan(myScore, cmp_cyakujun)
                    flx.Item(jrow, FlxCol.dof_span) = spanPoint
                    Dim agarisaPoint, cyakusaPoint As Integer
                    agarisaPoint = GetDofTime(jrow, cmp_cyakujun, cyakusaPoint)
                    flx.Item(jrow, FlxCol.dof_agarisa) = agarisaPoint
                    flx.Item(jrow, FlxCol.dof_cyakusa) = cyakusaPoint
                    flx.Item(jrow, FlxCol.dof_total) = spanPoint + agarisaPoint + cyakusaPoint
                End If
            Next
        Else
            MsgBox("条件を指定して「検索実行」して比較するためのデータをロードしてください", MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub

    'spanの適合度の得点を取得
    'Return span得点　0 ～ 200の範囲の値
    Private Function GetDofSpan(ByVal myScore As Integer, ByVal cmp_cyakujun As Integer) As Integer
        Dim pnt As Integer = 0
        Dim cnt As Integer = 0
        For j As Integer = 0 To spanScore.Count - 1
            If spanScore(j) >= 0 AndAlso cyakujun(j) >= 1 AndAlso cyakujun(j) <= cmp_cyakujun Then
                pnt += GetDegreeOfFit_spanScore(myScore, spanScore(j))
                cnt += 1
            End If
        Next
        If cnt > 0 Then
            pnt /= cnt
        End If
        Return pnt
    End Function

    '上り差および着差の適合度の得点を取得
    'Return 上り差得点　※cyakusaPointに着差得点をセットして返す　共に0 ～ 400の範囲の値
    Private Function GetDofTime(ByVal jrow As Integer, ByVal cmp_cyakujun As Integer, ByRef cyakusaPoint As Integer) As Integer
        Dim dataCnt As Integer = 0
        Dim agarisaPoint As Integer = 0
        cyakusaPoint = 0
        For j As Integer = 0 To 3
            Dim tmpcyakusa As Single
            Dim tmpagarisa As Single = cnvAgarisaStr2Val(flx.Item(jrow, FlxCol.histStart + j), tmpcyakusa)
            If tmpagarisa <> DMY_VAL Then
                dataCnt += 1
                For i As Integer = 0 To agarisa1.Count - 1
                    If cyakujun(i) >= 1 AndAlso cyakujun(i) <= cmp_cyakujun Then
                        Select Case j
                            Case 0
                                If Math.Abs(cyakusa1(i)) < 999 Then '取消,中止,除外などで変なのが混じってる
                                    agarisaPoint += GetDegreeOfFit_time(tmpagarisa, agarisa1(i), j)
                                    cyakusaPoint += GetDegreeOfFit_time(tmpagarisa, cyakusa1(i), j)
                                End If
                            Case 1
                                If Math.Abs(cyakusa2(i)) < 999 Then
                                    agarisaPoint += GetDegreeOfFit_time(tmpagarisa, agarisa2(i), j)
                                    cyakusaPoint += GetDegreeOfFit_time(tmpagarisa, cyakusa2(i), j)
                                End If
                            Case 2
                                If Math.Abs(cyakusa3(i)) < 999 Then
                                    agarisaPoint += GetDegreeOfFit_time(tmpagarisa, agarisa3(i), j)
                                    cyakusaPoint += GetDegreeOfFit_time(tmpagarisa, cyakusa3(i), j)
                                End If
                            Case 3
                                If Math.Abs(cyakusa4(i)) < 999 Then
                                    agarisaPoint += GetDegreeOfFit_time(tmpagarisa, agarisa4(i), j)
                                    cyakusaPoint += GetDegreeOfFit_time(tmpagarisa, cyakusa4(i), j)
                                End If
                        End Select
                    End If
                Next
            End If
        Next
        If dataCnt > 0 Then
            '取消しや海外レース等でデータの無い回の影響を除くため平均をとる
            agarisaPoint /= dataCnt
            cyakusaPoint /= dataCnt
            '比較対象数の多い少ないの影響を除くため平均をとる
            agarisaPoint /= agarisa1.Count
            cyakusaPoint /= agarisa1.Count
        End If
        Return agarisaPoint
    End Function

    Private Sub chkRacename2_CheckedChanged(sender As Object, e As EventArgs) Handles chkRacename2.CheckedChanged
        If chkRacename2.Checked Then
            chkRacename.Checked = False
            If txtRacename.Text.Length = 0 Then
                txtRacename.Text = oHead.race_name
            End If
        End If
    End Sub

    Private Sub chkRacename_CheckedChanged(sender As Object, e As EventArgs) Handles chkRacename.CheckedChanged
        If chkRacename.Checked Then
            chkRacename2.Checked = False
        End If
    End Sub
End Class
