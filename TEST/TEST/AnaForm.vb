Imports C1.Win.C1FlexGrid

Public Class AnaForm
    Private Enum FlxCol
        umaban = 0
        bamei = 1
        spanVal = 2
        histStart = 3
        cols = 9
    End Enum

    '一覧グリッド書式設定
    Private Sub SetUpFlx()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 2
            .Item(0, FlxCol.umaban) = "馬番"
            .Item(0, FlxCol.bamei) = "馬名"
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

            .AllowMerging = AllowMergingEnum.None
            .AllowEditing = False
            .AllowSorting = AllowSortingEnum.SingleColumn
            .AllowFiltering = True

            If Not .Styles.Contains("agari0") Then
                Dim cs As CellStyle = .Styles.Add("agari0")
                cs.Name = "agari0"
                cs.BackColor = Color.Yellow
            End If
        End With
    End Sub

    Private Sub ShowTable(ByVal sblist As raceAnaListClass)
        SetUpFlx()
        Dim xx(FlxCol.cols - 1) As String
        For j As Integer = 0 To sblist.cnt - 1
            Dim oKekka As raceAnanClass = sblist.GetBodyRef(j)

            If oKekka.umaban > 0 Then
                xx(FlxCol.umaban) = oKekka.umaban
            Else
                xx(FlxCol.umaban) = ""
            End If

            xx(FlxCol.bamei) = oKekka.bamei
            xx(FlxCol.spanVal) = oKekka.spanVal
            For i As Integer = 0 To 5
                xx(FlxCol.histStart + i) = oKekka.hist(i)
            Next
            flx.AddItem(xx)

            For i As Integer = 0 To 5
                If oKekka.isGoodHist(i) Then
                    flx.SetCellStyle(flx.Rows.Count - 1, FlxCol.histStart + i, "agari0")
                End If
            Next

        Next
        flx.AutoSizeCols()
        flx.AutoSizeRows()
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

            Dim anaList As New raceAnaListClass
            For j As Integer = 0 To fm3.syutubaList.cnt - 1
                lb_msg.Text = (j + 1).ToString & "/" & (fm3.syutubaList.cnt).ToString
                Dim rA As New raceAnanClass
                Dim o As SyutubaClass = fm3.syutubaList.GetBodyRef(j)
                rA.umaban = o.umaban
                rA.bamei = o.bamei
                fm2.entry(o.href)
                rA.spanVal = fm2.umaHistList.GetSpanVal(oRaceHeader.dt)
                For i As Integer = 0 To fm2.umaHistList.cnt - 1
                    If i > 5 Then
                        Exit For
                    End If

                    lb_msg.Text = (j + 1).ToString & "/" & (fm3.syutubaList.cnt).ToString & " | " & (i + 1).ToString & "/6"
                    Me.Refresh()

                    Dim oS As UmaHistClass = fm2.umaHistList.GetBodyRef(i)
                    fm1.entry(oS.href)
                    rA.hist(i) = fm1.kekkaList.GetAgarisa(o.bamei)
                Next

                anaList.add1(rA)
            Next
            ShowTable(anaList)
            fm1.Close()
            fm2.Close()
            fm3.Close()
        End If
    End Sub

End Class
