Imports C1.Win.C1FlexGrid

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
            .Rows.MinSize = 25

            .Cols(FlxCol.racename).TextAlign = TextAlignEnum.LeftCenter
            .Cols(FlxCol.kisyu).TextAlign = TextAlignEnum.LeftCenter
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
            'Dim oKekka As KekkaClass = klist.GetBodyRef(j)
            'Dim e_flg As Boolean = (oKekka.cyakujun < 1)
            'xx(FlxCol.cyakujun) = oKekka.CyakujunStr
            'xx(FlxCol.umaban) = oKekka.umaban
            'xx(FlxCol.bamei) = oKekka.bamei
            'xx(FlxCol.seirei) = oKekka.sex & oKekka.age.ToString
            'xx(FlxCol.hutan) = oKekka.hutan
            'xx(FlxCol.kisyu) = oKekka.kisyu
            'If e_flg Then
            '    xx(FlxCol.tokei) = ""
            'Else
            '    xx(FlxCol.tokei) = oKekka.tokei.ToString("F1")
            'End If
            'xx(FlxCol.tukajun) = ""
            'If Not e_flg Then
            '    For i As Integer = 0 To 3
            '        If oKekka.tukajun(i) > 0 Then
            '            xx(FlxCol.tukajun) &= oKekka.tukajun(i) & " "
            '        End If
            '    Next
            '    xx(FlxCol.agari) = oKekka.agari
            '    xx(FlxCol.ninki) = oKekka.ninki
            '    xx(FlxCol.cyakusa) = oKekka.agarisa.ToString("F1") & vbLf & "(" & oKekka.cyakusa.ToString("F1") & ")"
            'Else
            '    xx(FlxCol.cyakusa) = ""
            '    xx(FlxCol.agari) = ""
            '    xx(FlxCol.ninki) = ""
            'End If
            'xx(FlxCol.bataiju) = oKekka.w & vbLf & "(" & oKekka.zogen & ")"
            'xx(FlxCol.cyokyosi) = oKekka.cyokyosi
            'flx.AddItem(xx)
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
            Dim oUmaHeader As UmaHeaderClass = GetUmaHeader(contents)

            ListBox1.Items.Add("馬名：" & oUmaHeader.bamei)
            ListBox1.Items.Add("父：" & oUmaHeader.titi)
            ListBox1.Items.Add("母：" & oUmaHeader.haha)
            ListBox1.Items.Add("性別：" & oUmaHeader.sex)
            ListBox1.Items.Add("誕生日：" & oUmaHeader.birthday.ToString("yyyy年MM月dd日"))

            'Dim kekkaList As New KekkaListClass
            'GetKekka(contents, kekkaList)

            'kekkaList.setCyakusa()
            'kekkaList.setAgarisa(oRaceHeader)

            'ShowTable(kekkaList)
        End If
    End Sub
End Class
