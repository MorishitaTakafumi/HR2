Imports C1.Win.C1FlexGrid

Public Class StoreAnaValForm

    Private Const HIS_CNT As Integer = 4
    Private Enum FlxCol
        cyakujun = 0
        bamei = 1
        ninki = 2
        spanVal = 3
        histStart = 4
        kyoriScore = histStart + HIS_CNT
        dateScore
        cols = dateScore + 1
    End Enum

    Private raceURLque As New Queue(Of String)
    Private saveCount As Integer
    Private fm1 As New Form1 'レース結果
    Private fm1sub As New Form1
    Private fm2 As New Form2 '馬情報
    Private CancelFlag As Boolean


    '一覧グリッド書式設定
    Private Sub SetUpFlx()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 2
            .Item(0, FlxCol.cyakujun) = "着順"
            .Item(0, FlxCol.bamei) = "馬名"
            .Item(0, FlxCol.ninki) = "人気"
            .Item(0, FlxCol.spanVal) = "前走間隔" & vbLf & "±７日"
            For j As Integer = 0 To 5
                .Item(0, FlxCol.histStart + j) = (j + 1).ToString & "走前"
            Next
            .Item(0, FlxCol.kyoriScore) = "今回距離" & vbLf & "成績"
            .Item(0, FlxCol.dateScore) = "今回日付" & vbLf & "±７日成績"

            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 25

            .Cols(FlxCol.bamei).TextAlign = TextAlignEnum.LeftCenter

            .AllowMerging = AllowMergingEnum.FixedOnly
            .AllowEditing = False

            .AllowSorting = AllowSortingEnum.SingleColumn
            .AllowFiltering = False
        End With
    End Sub

    Private Sub ShowTable(ByVal anaValary() As AnaValClass)
        SetUpFlx()
        Dim xx(FlxCol.cols - 1) As String
        For j As Integer = 0 To anaValary.Length - 1
            xx(FlxCol.cyakujun) = anaValary(j).cyakujun
            xx(FlxCol.ninki) = anaValary(j).ninki
            xx(FlxCol.bamei) = anaValary(j).bamei
            xx(FlxCol.spanVal) = anaValary(j).spanVal
            For i As Integer = 0 To HIS_CNT - 1
                xx(FlxCol.histStart + i) = anaValary(j).GetHist(i)
            Next
            xx(FlxCol.kyoriScore) = anaValary(j).kyoriVal
            xx(FlxCol.dateScore) = anaValary(j).dateVal
            flx.AddItem(xx)
        Next
        flx.AutoSizeCols()
        flx.AutoSizeRows()
    End Sub

    Private Sub ShowRaceHeader(ByVal oRaceHeader As RaceHeaderClass)
        ListBox1.Items.Clear()
        ListBox1.Items.Add("競馬場：" & oRaceHeader.keibajo)
        ListBox1.Items.Add("開催日：" & oRaceHeader.dt.ToString("yyyy年MM月dd日"))
        ListBox1.Items.Add("レースNO.：" & oRaceHeader.race_no)
        ListBox1.Items.Add("レース名：" & oRaceHeader.race_name)
        ListBox1.Items.Add("グレード：" & oRaceHeader.grade)
        ListBox1.Items.Add("距離：" & oRaceHeader.kyori.ToString)
        ListBox1.Items.Add("種別：" & oRaceHeader.syubetu)
        ListBox1.Items.Add("クラス：" & oRaceHeader.class_name)
        ListBox1.Refresh()
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Dim url As String = txtURL.Text.Trim
        If url.Length > 0 Then
            raceURLque.Clear()
            raceURLque.Enqueue(url)
            analysis()
        End If
    End Sub

    '連続して解析
    Private Sub analysis()
        BtnStop.Visible = True
        CancelFlag = False
        saveCount = 0
        While raceURLque.Count > 0 AndAlso saveCount < NumericUpDown1.Value AndAlso (Not CancelFlag)
            Dim url As String = raceURLque.Dequeue()
            If analysis1(url) Then
                lb_cnt.Text = "今回登録数：" & saveCount.ToString & " Stack残数：" & raceURLque.Count.ToString
                Application.DoEvents()
            Else
                Exit While
            End If
        End While
        fm1.Close()
        fm1sub.Close()
        fm2.Close()
        If CancelFlag Then
            lb_msg.Text = "中止"
        Else
            lb_msg.Text = "完了"
        End If
        BtnStop.Visible = False
    End Sub

    'URLを指定して１レース分を解析・登録する
    '戻り値：True=成功、False=失敗
    Private Function analysis1(ByVal url As String) As Boolean

        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()
        lb_msg.Text = "レース結果ページの取り込み"
        fm1.entry(url)
        Dim exist_flg As Boolean = False
        Dim oRaceHeader As RaceHeaderClass = fm1.kekkaList.raceHeader
        ShowRaceHeader(oRaceHeader)

        lb_msg.Text = "既登録済みか調査"
        Dim errmsg As String = oRaceHeader.IsExist(exist_flg)
        If errmsg.Length = 0 AndAlso (Not exist_flg) AndAlso oRaceHeader.class_code > 1 Then
            Dim anaValAry(2) As AnaValClass
            '1,2,3着馬の情報
            For cyakujun As Short = 1 To 3
                Application.DoEvents()
                lb_msg.Text = cyakujun.ToString & "着馬の情報処理中"
                anaValAry(cyakujun - 1) = New AnaValClass
                Dim oKekka As KekkaClass = fm1.kekkaList.GetBodyRefByCyakujun(cyakujun)
                If oKekka IsNot Nothing Then '同着のときNothingとなる
                    Dim uma_href As String = oKekka.uma_href
                    fm2.entry(uma_href)
                    Dim stmp As String = ""
                    With anaValAry(cyakujun - 1)
                        .cyakujun = cyakujun
                        .bamei = oKekka.bamei
                        .ninki = oKekka.ninki
                        .spanScore = fm2.umaHistList.GetSpanScore(oRaceHeader.dt, stmp)
                        .dateScore = fm2.umaHistList.GetSameDateSameKyoriScore(oRaceHeader.dt, oRaceHeader.kyori, oRaceHeader.syubetu, .kyoriScore)
                    End With

                    Dim hist_idx As Integer = 0
                    For i As Integer = 0 To fm2.umaHistList.cnt - 1
                        lb_msg.Text = cyakujun.ToString & "着馬の情報処理中 " & hist_idx.ToString & "/4"
                        Application.DoEvents()
                        If hist_idx >= HIS_CNT Then
                            Exit For
                        End If
                        If oRaceHeader.dt > fm2.umaHistList.GetBodyRef(i).dt Then

                            raceURLque.Enqueue(fm2.umaHistList.GetBodyRef(i).href)
                            fm1sub.entry(fm2.umaHistList.GetBodyRef(i).href)

                            Dim oKekkaSub As KekkaClass = fm1sub.kekkaList.GetBodyRefByBamei(fm2.oUmaHeader.bamei)
                            If oKekkaSub IsNot Nothing Then
                                anaValAry(cyakujun - 1).agarisa(hist_idx) = oKekkaSub.agarisa
                                anaValAry(cyakujun - 1).cyakusa(hist_idx) = oKekkaSub.cyakusa
                                anaValAry(cyakujun - 1).OtherTypeRaceFlag(hist_idx) = Not fm1sub.kekkaList.IsSameTypeRace(oRaceHeader.syubetu)
                                hist_idx += 1
                            Else

                            End If
                        End If
                    Next
                End If
            Next
            ShowTable(anaValAry)
            lb_msg.Text = "結果登録"
            errmsg = oRaceHeader.save(anaValAry)
        End If
        Me.Cursor = Cursors.Default
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            Return False
        Else
            If exist_flg Then
                lb_msg.Text = "既登録レース！"
            ElseIf oRaceHeader.class_code < 2 Then
                lb_msg.Text = "1勝クラス以下のレース！"
            Else
                'MsgBox("OK", MsgBoxStyle.Information, Me.Text)
                saveCount += 1
            End If
            Return True
        End If
    End Function

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

    Private Sub BtnStop_Click(sender As Object, e As EventArgs) Handles BtnStop.Click
        CancelFlag = True
    End Sub
End Class