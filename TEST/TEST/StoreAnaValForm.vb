Imports System.Data.SQLite
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
    Private raceNameque As New Queue(Of String)
    Private saveCount As Integer
    Private fm1 As New Form1 'レース結果
    Private fm1sub As New Form1
    Private fm2 As New Form2 '馬情報
    Private CancelFlag As Boolean
    Private TopRaceName As String

    Public Sub New()
        InitializeComponent()
        Clipboard2URL(txtURL)
    End Sub

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
            If anaValary(j) IsNot Nothing Then
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
            End If
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
            raceNameque.Clear()
            raceNameque.Enqueue("???")
            analysis()
        End If
    End Sub

    '連続して解析
    Private Sub analysis()
        BtnStop.Visible = True
        CancelFlag = False
        saveCount = 0
        TopRaceName = ""
        ClearWebPageAccessCounter()
        While raceURLque.Count > 0 AndAlso saveCount < NumericUpDown1.Value AndAlso (Not CancelFlag)
            Dim url As String = raceURLque.Dequeue()
            Dim racename As String = raceNameque.Dequeue()
            If analysis1_NEW2(url, racename) Then
                lb_cnt.Text = "今回登録数：" & saveCount.ToString & " Stack残数：" & raceURLque.Count.ToString
                lb_cnt.Refresh()
                Application.DoEvents()
            Else
                Exit While
            End If
        End While
        Me.Cursor = Cursors.Default
        fm1.Close()
        fm1sub.Close()
        fm2.Close()
        If CancelFlag Then
            lb_msg.Text = "中止"
        Else
            lb_msg.Text = "完了"
        End If
        BtnStop.Visible = False
        showWebPageAccessCounter()
    End Sub

    'URLを指定して１レース分を解析・登録する
    '戻り値：True=成功、False=失敗
    Private Function analysis1_OLD(ByVal url As String, ByVal racename As String) As Boolean

        If chkSameNameOnly.Checked AndAlso TopRaceName.Length > 0 AndAlso TopRaceName <> racename Then
            lb_msg.Text = "別名レース！"
            Return True
        End If

        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()
        lb_msg.Text = "レース結果ページの取り込み"
        fm1.entry(url)
        Dim exist_flg As Boolean = False
        Dim oRaceHeader As RaceHeaderClass = fm1.kekkaList.raceHeader
        ShowRaceHeader(oRaceHeader)
        If TopRaceName.Length = 0 Then
            TopRaceName = oRaceHeader.race_name
        Else
            If chkSameNameOnly.Checked AndAlso TopRaceName <> oRaceHeader.race_name Then
                lb_msg.Text = "別名レース！"
                Return True
            End If
        End If

        lb_msg.Text = "既登録済みか調査"
        Dim errmsg As String = oRaceHeader.IsExist(exist_flg)
        If errmsg.Length = 0 AndAlso (Not exist_flg) AndAlso oRaceHeader.class_code >= 0 Then
            Dim anaValAry(oRaceHeader.tosu - 1) As AnaValClass
            '全馬の情報
            For j As Short = 0 To oRaceHeader.tosu - 1
                Application.DoEvents()
                lb_msg.Text = "情報処理中" & (j + 1).ToString
                anaValAry(j) = New AnaValClass
                Dim oKekka As KekkaClass = fm1.kekkaList.GetBodyRef(j)
                If oKekka IsNot Nothing Then '同着のときNothingとなる
                    Dim uma_href As String = oKekka.uma_href
                    fm2.entry(uma_href)
                    Dim stmp As String = ""
                    With anaValAry(j)
                        .cyakujun = oKekka.cyakujun
                        .bamei = oKekka.bamei
                        .ninki = oKekka.ninki
                        .spanScore = fm2.umaHistList.GetSpanScore(oRaceHeader.dt, stmp)
                        .dateScore = fm2.umaHistList.GetSameDateSameKyoriScore(oRaceHeader.dt, oRaceHeader.kyori, oRaceHeader.syubetu, .kyoriScore)
                    End With

                    Dim hist_idx As Integer = 0
                    For i As Integer = 0 To fm2.umaHistList.cnt - 1
                        lb_msg.Text = "情報処理中 " & (j + 1).ToString & " " & hist_idx.ToString & "/4"
                        Application.DoEvents()
                        If hist_idx >= HIS_CNT Then
                            Exit For
                        End If
                        If oRaceHeader.dt > fm2.umaHistList.GetBodyRef(i).dt Then

                            raceURLque.Enqueue(fm2.umaHistList.GetBodyRef(i).href)
                            raceNameque.Enqueue(fm2.umaHistList.GetBodyRef(i).racename)
                            fm1sub.entry(fm2.umaHistList.GetBodyRef(i).href)

                            Dim oKekkaSub As KekkaClass = fm1sub.kekkaList.GetBodyRefByBamei(fm2.oUmaHeader.bamei)
                            If oKekkaSub IsNot Nothing Then
                                anaValAry(j).agarisa(hist_idx) = oKekkaSub.agarisa
                                anaValAry(j).cyakusa(hist_idx) = oKekkaSub.cyakusa
                                anaValAry(j).OtherTypeRaceFlag(hist_idx) = Not fm1sub.kekkaList.IsSameTypeRace(oRaceHeader.syubetu)
                                hist_idx += 1
                            Else

                            End If
                        End If
                    Next
                End If
            Next
            ShowTable(anaValAry)
            lb_msg.Text = "結果登録"
            errmsg = SaveData_OLD(fm1.kekkaList, anaValAry)
        End If
        Me.Cursor = Cursors.Default
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            Return False
        Else
            If exist_flg Then
                lb_msg.Text = "既登録レース！"
            Else
                'MsgBox("OK", MsgBoxStyle.Information, Me.Text)
                saveCount += 1
            End If
            Return True
        End If
    End Function

    'URLを指定して１レース分を解析・登録する
    '戻り値：True=成功、False=失敗
    Private Function analysis1_NEW(ByVal url As String, ByVal racename As String) As Boolean

        If chkSameNameOnly.Checked AndAlso TopRaceName.Length > 0 AndAlso TopRaceName <> racename Then
            lb_msg.Text = "別名レース！"
            Return True
        End If
        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()
        lb_msg.Text = "レース結果ページの取り込み"
        fm1.entry(url)
        Dim exist_flg As Boolean = False
        Dim oRaceHeader As RaceHeaderClass = fm1.kekkaList.raceHeader
        ShowRaceHeader(oRaceHeader)
        If TopRaceName.Length = 0 Then
            TopRaceName = oRaceHeader.race_name
        Else
            If chkSameNameOnly.Checked AndAlso TopRaceName <> oRaceHeader.race_name Then
                lb_msg.Text = "別名レース！"
                Return True
            End If
        End If

        lb_msg.Text = "既登録済みか調査"
        Dim errmsg As String = oRaceHeader.IsExist(exist_flg)
        If errmsg.Length = 0 AndAlso (Not exist_flg) AndAlso oRaceHeader.class_code >= 0 Then
            lb_msg.Text = "結果登録"
            errmsg = SaveData_NEW(fm1.kekkaList)
            If errmsg.Length > 0 Then
                Return False
            End If
            '全馬の情報
            For j As Short = 0 To oRaceHeader.tosu - 1
                Application.DoEvents()
                lb_msg.Text = "情報処理中" & (j + 1).ToString
                Dim oKekka As KekkaClass = fm1.kekkaList.GetBodyRef(j)
                If oKekka IsNot Nothing Then '同着のときNothingとなる
                    Dim uma_href As String = oKekka.uma_href
                    fm2.entry(uma_href)

                    Dim hist_idx As Integer = 0
                    For i As Integer = 0 To fm2.umaHistList.cnt - 1
                        lb_msg.Text = "情報処理中 " & (j + 1).ToString & " " & hist_idx.ToString & "/4"
                        Application.DoEvents()
                        If hist_idx >= HIS_CNT Then
                            Exit For
                        End If
                        Dim oUmaHist As UmaHistClass = fm2.umaHistList.GetBodyRef(i)
                        If oRaceHeader.dt > oUmaHist.dt AndAlso oUmaHist.href.Trim.Length > 0 Then
                            raceURLque.Enqueue(oUmaHist.href)
                            raceNameque.Enqueue(oUmaHist.racename)
                            fm1sub.entry(oUmaHist.href)
                            errmsg = SaveData_NEW(fm1sub.kekkaList)
                            If errmsg.Length > 0 Then
                                Return False
                            End If
                            hist_idx += 1
                        End If
                    Next
                End If
            Next
        End If
        Me.Cursor = Cursors.Default
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            Return False
        Else
            If exist_flg Then
                lb_msg.Text = "既登録レース！"
            Else
                'MsgBox("OK", MsgBoxStyle.Information, Me.Text)
                saveCount += 1
            End If
            Return True
        End If
    End Function

    'URLを指定して１レース分を解析・登録する
    '戻り値：True=成功、False=失敗
    Private Function analysis1_NEW2(ByVal url As String, ByVal racename As String) As Boolean

        If chkSameNameOnly.Checked AndAlso TopRaceName.Length > 0 AndAlso TopRaceName <> racename Then
            lb_msg.Text = "別名レース！"
            Return True
        End If
        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()
        lb_msg.Text = "レース結果ページの取り込み"
        Dim kekkaList As New KekkaListClass
        Dim kekkaListSub As New KekkaListClass
        Dim existFlag As Boolean
        Dim errmsg As String = kekkaList.GetRaceKekka(url, existFlag, "", "", -1, -1, -1, True)
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        Dim oRaceHeader As RaceHeaderClass = kekkaList.raceHeader
        ShowRaceHeader(oRaceHeader)
        If TopRaceName.Length = 0 Then
            TopRaceName = oRaceHeader.race_name
        Else
            If chkSameNameOnly.Checked AndAlso TopRaceName <> oRaceHeader.race_name Then
                lb_msg.Text = "別名レース！"
                Return True
            End If
        End If
        If existFlag Then
            Return True
        End If

        If oRaceHeader.class_code >= 0 Then
            Dim umaHistList As New umaHistListClass
            lb_msg.Text = "全馬の情報登録"
            For j As Short = 0 To oRaceHeader.tosu - 1
                Application.DoEvents()
                lb_msg.Text = "情報処理中" & (j + 1).ToString
                Dim oKekka As KekkaClass = kekkaList.GetBodyRef(j)
                If oKekka IsNot Nothing Then '同着のときNothingとなる
                    Dim uma_url As String = makeJRAurl(oKekka.uma_href)
                    errmsg = umaHistList.GetUmaInfo(uma_url, "", DMY_DATE, True)
                    Dim hist_idx As Integer = 0
                    For i As Integer = 0 To umaHistList.cnt - 1
                        lb_msg.Text = "情報処理中 " & (j + 1).ToString & " " & hist_idx.ToString & "/4"
                        Application.DoEvents()
                        If hist_idx >= HIS_CNT Then
                            Exit For
                        End If
                        Dim oUmaHist As UmaHistClass = umaHistList.GetBodyRef(i)
                        If oRaceHeader.dt > oUmaHist.dt AndAlso oUmaHist.href.Trim.Length > 0 Then
                            raceURLque.Enqueue(makeJRAurl(oUmaHist.href))
                            raceNameque.Enqueue(oUmaHist.racename)
                            errmsg = kekkaListSub.GetRaceKekka(oUmaHist.href, existFlag, "", "", -1, -1, -1, True)
                            If errmsg.Length > 0 Then
                                Return False
                            End If
                            hist_idx += 1
                        End If
                    Next
                End If
            Next
        End If
        Me.Cursor = Cursors.Default
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            Return False
        Else
            saveCount += 1
            Return True
        End If
    End Function

    Private Function SaveData_OLD(ByVal kekkalist As KekkaListClass, ByVal anavalary() As AnaValClass) As String
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim errmsg As String = ""
            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            If kekkalist.raceHeader.id < 0 Then
                errmsg = kekkalist.raceHeader.save(anavalary)
            End If
            If errmsg.Length = 0 Then
                errmsg = kekkalist.save(cmd)
            End If
            Return errmsg
        End Using
    End Function

    Private Function SaveData_NEW(ByVal kekkalist As KekkaListClass) As String
        If kekkalist.raceHeader.race_name.Trim.Length = 0 Then
            Return ""
        End If

        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim errmsg As String = ""
            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            If kekkalist.raceHeader.id < 0 Then
                errmsg = kekkalist.raceHeader.addNew(cmd)
            End If
            If errmsg.Length = 0 Then
                errmsg = kekkalist.save(cmd)
            End If
            Return errmsg
        End Using
    End Function

    Private Sub BtnURL_Click(sender As Object, e As EventArgs) Handles BtnURL.Click
        Clipboard2URL(txtURL)
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