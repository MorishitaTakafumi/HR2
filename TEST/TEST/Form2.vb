Imports C1.Win.C1FlexGrid
Imports System.Data.SQLite

Public Class Form2
    '競走馬情報とりこみ

    Private Enum FlxCol
        dt = 0
        ba = 1
        racename = 2
        kyori = 3
        baba = 4
        tosu = 5
        ninki = 6
        cyakujun = 7
        kisyu = 8
        hutan = 9
        bataiju = 10
        tokei = 11
        href = 12
        cols = 13
    End Enum

    Public umaHistList As New umaHistListClass
    Public oUmaHeader As UmaHeaderClass

    '一覧グリッド書式設定
    Private Sub SetUpFlx()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 1
            .Rows.Fixed = 1
            .Cols.Fixed = 2
            .Item(0, FlxCol.dt) = "年月日"
            .Item(0, FlxCol.ba) = "場"
            .Item(0, FlxCol.racename) = "レース名"
            .Item(0, FlxCol.kyori) = "距離"
            .Item(0, FlxCol.baba) = "馬場"
            .Item(0, FlxCol.tosu) = "頭数"
            .Item(0, FlxCol.ninki) = "人気"
            .Item(0, FlxCol.cyakujun) = "着順"
            .Item(0, FlxCol.kisyu) = "騎手名"
            .Item(0, FlxCol.hutan) = "負担" & vbLf & "重量"
            .Item(0, FlxCol.tokei) = "タイム"
            .Item(0, FlxCol.bataiju) = "馬体重"
            .Item(0, FlxCol.href) = "href"

            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 20

            .Cols(FlxCol.racename).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.kisyu).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.href).TextAlign = TextAlignEnum.LeftCenter

            .AllowMerging = AllowMergingEnum.None
            .AllowEditing = False
            .AllowSorting = AllowSortingEnum.SingleColumn
        End With
    End Sub

    Private Sub ShowTable(ByVal hlist As umaHistListClass)
        SetUpFlx()
        Dim xx(FlxCol.cols - 1) As String
        For j As Integer = 0 To hlist.cnt - 1
            Dim oHist As UmaHistClass = hlist.GetBodyRef(j)
            Dim e_flg As Boolean = (oHist.cyakujun < 1)
            xx(FlxCol.cyakujun) = oHist.CyakujunStr
            xx(FlxCol.dt) = oHist.dt.ToString("yyyy年MM月dd日")
            xx(FlxCol.ba) = oHist.keibajo
            xx(FlxCol.racename) = oHist.racename
            xx(FlxCol.href) = oHist.href
            xx(FlxCol.hutan) = oHist.hutan
            xx(FlxCol.kisyu) = oHist.kisyu
            If oHist.tokei > 0 Then
                xx(FlxCol.tokei) = oHist.tokei.ToString("F1")
            Else
                xx(FlxCol.tokei) = ""
            End If
            xx(FlxCol.kyori) = oHist.syubetu & oHist.distance.ToString
            xx(FlxCol.baba) = oHist.baba
            If oHist.w > 0 Then
                xx(FlxCol.bataiju) = oHist.w
            Else
                xx(FlxCol.bataiju) = ""
            End If
            xx(FlxCol.tosu) = oHist.tosu
            If oHist.ninki > 0 Then
                xx(FlxCol.ninki) = oHist.ninki
            Else
                xx(FlxCol.ninki) = ""
            End If
            flx.AddItem(xx)
        Next
        flx.AutoSizeCols()
        flx.AutoSizeRows()
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        GetData(DMY_DATE)
    End Sub

    Private Sub GetData(ByVal dt_max As Date)
        umaHistList.init()
        Dim errmsg As String = umaHistList.GetUmaInfo(txtURL.Text.Trim, txtBamei.Text.Trim, dt_max, True)
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            Return
        End If
        oUmaHeader = umaHistList.umaHeader
        If (oUmaHeader IsNot Nothing) AndAlso oUmaHeader.bamei.Length > 0 Then
            ListBox1.Items.Clear()
            ListBox1.Items.Add("馬名：" & oUmaHeader.bamei)
            ListBox1.Items.Add("父：" & oUmaHeader.father)
            ListBox1.Items.Add("母：" & oUmaHeader.mother)
            ListBox1.Items.Add("性別：" & oUmaHeader.seibetu)
            ListBox1.Items.Add("誕生日：" & oUmaHeader.birthday.ToString("yyyy年MM月dd日"))
            ShowTable(umaHistList)
        End If
    End Sub

    Public Sub entry(ByVal url As String, Optional ByVal bamei As String = "", Optional ByVal dt_max As Date = DMY_DATE)
        txtURL.Text = makeJRAurl(url)
        txtBamei.Text = bamei
        Me.WindowState = FormWindowState.Minimized
        Show()
        GetData(dt_max)
    End Sub

    Private Sub flx_Click(sender As Object, e As EventArgs) Handles flx.Click
        Dim url As String = flx.Item(flx.Row, FlxCol.href)
        Dim a As New Form1
        a.entry(url)
    End Sub

    Private Sub BtnURL_Click(sender As Object, e As EventArgs) Handles BtnURL.Click
        Clipboard2URL(txtURL)
    End Sub
End Class
