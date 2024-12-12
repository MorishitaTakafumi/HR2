Imports C1.Win.C1FlexGrid

Public Class Form1
    'レース結果の取り込み

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
        href = 13
        cols = 14
    End Enum

    Public kekkaList As New KekkaListClass
    Public Property DbErrMsg As String = ""

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
            .Item(0, FlxCol.href) = "馬href"

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
            xx(FlxCol.href) = oKekka.uma_href
            flx.AddItem(xx)
        Next
        flx.AutoSizeCols()
        flx.AutoSizeRows()
    End Sub

    Private Sub ShowHeader()
        ListBox1.Items.Clear()
        Dim oRaceHeader As RaceHeaderClass = kekkaList.raceHeader
        ListBox1.Items.Add("競馬場：" & oRaceHeader.keibajo)
        ListBox1.Items.Add("開催日：" & oRaceHeader.dt.ToString("yyyy年MM月dd日"))
        ListBox1.Items.Add("レースNo.：" & oRaceHeader.race_no)
        ListBox1.Items.Add("レース名：" & oRaceHeader.race_name)
        ListBox1.Items.Add("グレード：" & oRaceHeader.grade)
        ListBox1.Items.Add("距離：" & oRaceHeader.kyori.ToString)
        ListBox1.Items.Add("種別：" & oRaceHeader.syubetu)
        ListBox1.Items.Add("クラス：" & oRaceHeader.class_name)
        ListBox1.Items.Add("頭数：" & oRaceHeader.tosu)
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        GetData()
    End Sub

    Private Sub GetData()
        ListBox1.Items.Clear()
        Dim existFlag As Boolean
        DbErrMsg = kekkaList.GetRaceKekka(txtURL.Text, existFlag, txtDate.Text, txtRaceName.Text, -1, -1, -1, chkAutoSave.Checked)
        If DbErrMsg.Length > 0 Then
            MsgBox(DbErrMsg, MsgBoxStyle.Critical, Me.Text)
            Return
        End If
        If kekkaList.cnt > 0 Then
            txtResult.Text = kekkaList.WebPageContents
            ShowHeader()
            ShowTable(kekkaList)
            Return
        End If
    End Sub

    Public Sub entry(ByVal url As String, Optional ByVal dt_race As String = "", Optional ByVal racename As String = "", Optional ByVal autosave As Boolean = False)
        txtURL.Text = makeJRAurl(url)
        txtDate.Text = dt_race
        txtRaceName.Text = racename
        chkAutoSave.Checked = autosave
        Me.WindowState = FormWindowState.Minimized
        Show()
        GetData()
    End Sub

    Private Sub BtnURL_Click(sender As Object, e As EventArgs) Handles BtnURL.Click
        Clipboard2URL(txtURL)
    End Sub

    Private Sub flx_Click(sender As Object, e As EventArgs) Handles flx.Click
        Dim url As String = flx.Item(flx.Row, FlxCol.href)
        Dim bamei As String = flx.Item(flx.Row, FlxCol.bamei)
        Dim a As New Form2
        a.entry(url, bamei)
    End Sub
End Class
