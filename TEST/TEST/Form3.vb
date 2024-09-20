Imports C1.Win.C1FlexGrid

Public Class Form3
    Private Enum FlxCol
        waku = 0
        umaban = 1
        bamei = 2
        seirei = 3
        hutan = 4
        kisyu = 5
        bataiju = 6
        ninki = 7
        cyokyosi = 8
        href = 9
        cols = 10
    End Enum


    Public oRaceHeader As New RaceHeaderClass
    Public syutubaList As New SyutubaListClass

    '一覧グリッド書式設定
    Private Sub SetUpFlx()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 2
            .Item(0, FlxCol.waku) = "枠"
            .Item(0, FlxCol.umaban) = "馬番"
            .Item(0, FlxCol.bamei) = "馬名"
            .Item(0, FlxCol.seirei) = "性齢"
            .Item(0, FlxCol.hutan) = "負担" & vbLf & "重量"
            .Item(0, FlxCol.kisyu) = "騎手名"
            .Item(0, FlxCol.bataiju) = "馬体重" & vbLf & "(増減)"
            .Item(0, FlxCol.ninki) = "人気"
            .Item(0, FlxCol.cyokyosi) = "調教師名"
            .Item(0, FlxCol.href) = "href"

            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 20

            .Cols(FlxCol.bamei).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.kisyu).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.cyokyosi).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.href).TextAlign = TextAlignEnum.LeftCenter

            .AllowMerging = AllowMergingEnum.None
            .AllowEditing = False
            .AllowSorting = AllowSortingEnum.SingleColumn
        End With
    End Sub

    Private Sub ShowTable(ByVal sblist As SyutubaListClass)
        SetUpFlx()
        Dim xx(FlxCol.cols - 1) As String
        For j As Integer = 0 To sblist.cnt - 1
            Dim oKekka As SyutubaClass = sblist.GetBodyRef(j)
            If oKekka.waku > 0 Then
                xx(FlxCol.waku) = oKekka.waku
                xx(FlxCol.umaban) = oKekka.umaban
            Else
                xx(FlxCol.waku) = ""
                xx(FlxCol.umaban) = ""
            End If
            xx(FlxCol.bamei) = oKekka.bamei
            xx(FlxCol.seirei) = oKekka.sex & oKekka.age.ToString
            xx(FlxCol.hutan) = oKekka.hutan
            xx(FlxCol.kisyu) = oKekka.kisyu

            If oKekka.ninki > 0 Then
                xx(FlxCol.ninki) = oKekka.ninki
            Else
                xx(FlxCol.ninki) = ""
            End If
            If oKekka.w > 0 Then
                xx(FlxCol.bataiju) = oKekka.w & vbLf & "(" & oKekka.zogen & ")"
            Else
                xx(FlxCol.bataiju) = ""
            End If
            xx(FlxCol.cyokyosi) = oKekka.cyokyosi
            xx(FlxCol.href) = oKekka.href
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

            GetSyutuba(contents, syutubaList)

            ShowTable(syutubaList)
        End If
    End Sub

    Public Sub entry(ByVal url As String)
        txtURL.Text = url
        Me.WindowState = FormWindowState.Minimized
        Show()
        BtnTest.PerformClick()
    End Sub

    Private Sub flx_Click(sender As Object, e As EventArgs) Handles flx.Click
        Dim url As String = flx.Item(flx.Row, FlxCol.href)
        Dim a As New Form2
        a.entry(url)

    End Sub
End Class
