Imports C1.Win.C1FlexGrid

Public Class AnaForm
    Private Enum FlxCol
        waku = 0
        umaban = 1
        bamei = 2
        chk = 3
        ninki = 4
        spanVal = 5
        histStart = 6
        cols = 12
    End Enum

    Private anaList As New raceAnaListClass

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
            .Item(0, FlxCol.spanVal) = "前走間隔±７日"
            For j As Integer = 0 To 5
                .Item(0, FlxCol.histStart + j) = (j + 1).ToString & "走前"
            Next

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
                cs.ComboList = "　|△|▲|○|◎"
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
                For i As Integer = 0 To 5
                    xx(FlxCol.histStart + i) = ""
                Next
            Else
                xx(FlxCol.spanVal) = oUma.spanVal
                For i As Integer = 0 To 5
                    xx(FlxCol.histStart + i) = oUma.hist(i)
                Next
            End If
            flx.AddItem(xx)
        Next
        flx.AutoSizeCols()
        flx.AutoSizeRows()
    End Sub

    Private Sub PaintTable(ByVal sblist As raceAnaListClass)

        For j As Integer = 0 To sblist.cnt - 1
            Dim jrow As Integer = flx.Rows.Fixed + j
            Dim oUma As raceAnanClass = sblist.GetBodyRef(j)

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

    Private Sub BtnTGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Dim url As String = txtURL.Text.Trim
        If url.Length > 0 Then
            Dim fm1 As New Form1
            Dim fm2 As New Form2
            Dim fm3 As New Form3
            fm3.entry(url)


            ListBox1.Items.Clear()
            Dim oRaceHeader As RaceHeaderClass = fm3.oRaceHeader

            ListBox1.Items.Add("競馬場：" & oRaceHeader.keibajo)
            ListBox1.Items.Add("開催日：" & oRaceHeader.dt.ToString("yyyy年MM月dd日"))

            ListBox1.Items.Add("レース名：" & oRaceHeader.racename)
            ListBox1.Items.Add("グレード：" & oRaceHeader.grade)

            ListBox1.Items.Add("距離：" & oRaceHeader.distance.ToString)
            ListBox1.Items.Add("種別：" & oRaceHeader.syubetu)
            ListBox1.Items.Add("クラス：" & oRaceHeader.classname)

            anaList.init()
            For j As Integer = 0 To fm3.syutubaList.cnt - 1
                lb_msg.Text = (j + 1).ToString & "/" & (fm3.syutubaList.cnt).ToString
                Dim rA As New raceAnanClass
                Dim o As SyutubaClass = fm3.syutubaList.GetBodyRef(j)
                rA.waku = o.waku
                rA.umaban = o.umaban
                rA.bamei = o.bamei
                rA.ninki = o.ninki
                fm2.entry(o.href)
                rA.spanVal = fm2.umaHistList.GetSpanVal(oRaceHeader.dt)
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
                    rA.hist(i) = fm1.kekkaList.GetAgarisa(o.bamei, oRaceHeader.syubetu)
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
End Class
