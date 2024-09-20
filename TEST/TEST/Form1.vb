Imports C1.Win.C1FlexGrid

Public Class Form1
    Private Enum FlxCol
        cyakujun = 0
        umaban = 1
        bamei = 2
        seirei = 3
        hutan = 4
        kisyu = 5
        tokei = 6
        tukajun = 7
        agari = 8
        cyakusa = 9
        bataiju = 10
        cyokyosi = 11
        ninki = 12
        cols = 13
    End Enum

    Public kekkaList As New KekkaListClass

    '一覧グリッド書式設定
    Private Sub SetUpFlx()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 2
            .Item(0, FlxCol.cyakujun) = "着順"
            .Item(0, FlxCol.umaban) = "馬番"
            .Item(0, FlxCol.bamei) = "馬名"
            .Item(0, FlxCol.seirei) = "性齢"
            .Item(0, FlxCol.hutan) = "負担" & vbLf & "重量"
            .Item(0, FlxCol.kisyu) = "騎手名"
            .Item(0, FlxCol.tokei) = "タイム"
            .Item(0, FlxCol.tukajun) = "コーナー" & vbLf & "通過順位"
            .Item(0, FlxCol.agari) = "推定" & vbLf & "上り"
            .Item(0, FlxCol.cyakusa) = "上り補正値" & vbLf & "(着差)"
            .Item(0, FlxCol.bataiju) = "馬体重" & vbLf & "(増減)"
            .Item(0, FlxCol.cyokyosi) = "調教師名"
            .Item(0, FlxCol.ninki) = "人気"

            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 20

            .Cols(FlxCol.bamei).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.kisyu).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.cyokyosi).TextAlign = TextAlignEnum.LeftCenter

            .AllowMerging = AllowMergingEnum.None
            .AllowEditing = False
            .AllowSorting = AllowSortingEnum.SingleColumn
        End With
    End Sub

    Private Sub ShowTable(ByVal klist As KekkaListClass)
        SetUpFlx()
        Dim xx(FlxCol.cols - 1) As String
        For j As Integer = 0 To klist.cnt - 1
            Dim oKekka As KekkaClass = klist.GetBodyRef(j)
            Dim e_flg As Boolean = (oKekka.cyakujun < 1)
            xx(FlxCol.cyakujun) = oKekka.CyakujunStr
            xx(FlxCol.umaban) = oKekka.umaban
            xx(FlxCol.bamei) = oKekka.bamei
            xx(FlxCol.seirei) = oKekka.sex & oKekka.age.ToString
            xx(FlxCol.hutan) = oKekka.hutan
            xx(FlxCol.kisyu) = oKekka.kisyu
            If e_flg Then
                xx(FlxCol.tokei) = ""
            Else
                xx(FlxCol.tokei) = oKekka.tokei.ToString("F1")
            End If
            xx(FlxCol.tukajun) = ""
            If Not e_flg Then
                For i As Integer = 0 To 3
                    If oKekka.tukajun(i) > 0 Then
                        xx(FlxCol.tukajun) &= oKekka.tukajun(i) & " "
                    End If
                Next
                xx(FlxCol.agari) = oKekka.agari
                xx(FlxCol.ninki) = oKekka.ninki
                xx(FlxCol.cyakusa) = oKekka.agarisa.ToString("F1") & vbLf & "(" & oKekka.cyakusa.ToString("F1") & ")"
            Else
                xx(FlxCol.cyakusa) = ""
                xx(FlxCol.agari) = ""
                xx(FlxCol.ninki) = ""
            End If
            xx(FlxCol.bataiju) = oKekka.w & vbLf & "(" & oKekka.zogen & ")"
            xx(FlxCol.cyokyosi) = oKekka.cyokyosi
            flx.AddItem(xx)
        Next
        flx.AutoSizeCols()
        flx.AutoSizeRows()
    End Sub

    Private Sub BtnTest_Click(sender As Object, e As EventArgs) Handles BtnTest.Click
        Dim url As String = txtURL.Text.Trim
        If url.Length > 0 Then
            Dim contents As String = GetWebPageText(txtURL.Text.Trim)
            txtResult.Text = contents
            ListBox1.Items.Clear()
            Dim oRaceHeader As New RaceHeaderClass

            oRaceHeader.keibajo = GetWhenWhere(contents, oRaceHeader.dt)
            ListBox1.Items.Add("競馬場：" & oRaceHeader.keibajo)
            ListBox1.Items.Add("開催日：" & oRaceHeader.dt.ToString("yyyy年MM月dd日"))

            oRaceHeader.racename = GetRaceName(contents, oRaceHeader.grade)
            ListBox1.Items.Add("レース名：" & oRaceHeader.racename)
            ListBox1.Items.Add("グレード：" & oRaceHeader.grade)

            oRaceHeader.classname = GetClassCource(contents, oRaceHeader.distance, oRaceHeader.syubetu)
            ListBox1.Items.Add("距離：" & oRaceHeader.distance.ToString)
            ListBox1.Items.Add("種別：" & oRaceHeader.syubetu)
            ListBox1.Items.Add("クラス：" & oRaceHeader.classname)

            GetKekka(contents, kekkaList)

            kekkaList.setCyakusa()
            kekkaList.setAgarisa(oRaceHeader)

            ShowTable(kekkaList)
        End If
    End Sub

    Public Sub entry(ByVal url As String)
        txtURL.Text = "https://www.jra.go.jp" & url
        Show()
        BtnTest.PerformClick()
    End Sub

End Class
