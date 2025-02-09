Imports C1.Win.C1FlexGrid
Imports System.Data.SQLite

Public Class DebufForm
    Private Enum FlxCol
        classname = 0
        ave_time = 1
        ave_agari = 2
        ave_cr_time = 3
        ave_cr_agari = 4
        cols
    End Enum

    Public Sub New()
        InitializeComponent()
        SetupCombobox()
        SetUpFlx()
    End Sub

    Private Sub SetupCombobox2()
        CbKyori.Items.Clear()
        If CbBa.SelectedIndex = -1 OrElse CbSyubetu.SelectedIndex = -1 Then
            Return
        End If
        Dim jo_code As Integer = GetKeibajoCode(CbBa.Text)
        Dim type_code As Integer = CbSyubetu.SelectedIndex + 1

        '距離はDBから選択肢を取得する
        Dim errmsg As String = ""
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = conn.CreateCommand
            conn.Open()
            Try
                cmd.CommandText = "SELECT DISTINCT(kyori) FROM RaceHeader 
                                   WHERE type_code=@type_code AND jo_code=@jo_code
                                   ORDER BY kyori"
                cmd.Parameters.AddWithValue("@type_code", type_code)
                cmd.Parameters.AddWithValue("@jo_code", jo_code)
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                With CbKyori
                    .Items.Clear()
                    While r.Read
                        .Items.Add(r("kyori"))
                    End While
                    .SelectedIndex = 0
                End With
                r.Close()
            Catch ex As Exception
                errmsg = ex.Message
            End Try

            If errmsg.Length > 0 Then
                MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            End If
        End Using
    End Sub

    Private Sub SetupCombobox()
        With CbBa
            .Items.Clear()
            For j As Integer = 0 To JoMei.Length - 1
                .Items.Add(JoMei(j))
            Next
            .SelectedIndex = 0
        End With
        With CbSyubetu
            .Items.Clear()
            .Items.Add("芝")
            .Items.Add("ダート")
            .Items.Add("障害")
            .SelectedIndex = 0
        End With
    End Sub

    '一覧グリッド書式設定
    Private Sub SetUpFlx()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 9
            .Rows.Fixed = 1
            .Cols.Fixed = 1
            .Item(0, FlxCol.classname) = "クラス"
            .Item(0, FlxCol.ave_time) = "平均タイム"
            .Item(0, FlxCol.ave_agari) = "平均あがり"
            .Item(0, FlxCol.ave_cr_time) = "補正平均タイム"
            .Item(0, FlxCol.ave_cr_agari) = "補正平均あがり"
            For j As Integer = 0 To 7
                .Item(1 + j, FlxCol.classname) = RaceHeaderClass.GetClassName(j)
            Next
            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 25
            .Cols.MinSize = 80
            .Cols(FlxCol.classname).TextAlign = TextAlignEnum.LeftCenter

            .AllowMerging = AllowMergingEnum.None
            .AllowEditing = False

            .AllowSorting = AllowSortingEnum.SingleColumn
            .AllowFiltering = False
        End With
    End Sub

    Private Sub ShowTable()
        If CbBa.SelectedIndex = -1 OrElse CbSyubetu.SelectedIndex = -1 Then
            Return
        End If
        Dim jo_code As Integer = GetKeibajoCode(CbBa.Text)
        Dim type_code As Integer = CbSyubetu.SelectedIndex + 1
        Dim kyori As Integer = CInt(CbKyori.Text)
        Dim class_code As Integer = 0
        Dim sv As Single
        With flx
            .Redraw = False
            For jrow As Integer = .Rows.Fixed To .Rows.Count - 1
                sv = oTC.get_time_ave(class_code, type_code, kyori, jo_code)
                If sv = DMY_VAL Then
                    .Item(jrow, FlxCol.ave_time) = "*****"
                Else
                    .Item(jrow, FlxCol.ave_time) = sv.ToString("F2")
                End If

                sv = oTC.get_agari_ave(class_code, type_code, kyori, jo_code)
                If sv = DMY_VAL Then
                    .Item(jrow, FlxCol.ave_agari) = "*****"
                Else
                    .Item(jrow, FlxCol.ave_agari) = sv.ToString("F2")
                End If

                sv = oTC.get_time_correction(class_code, type_code, kyori, jo_code)
                If sv = DMY_VAL Then
                    .Item(jrow, FlxCol.ave_cr_time) = "*****"
                Else
                    .Item(jrow, FlxCol.ave_cr_time) = sv.ToString("F2")
                End If

                sv = oTC.get_agari_correction(class_code, type_code, kyori, jo_code)
                If sv = DMY_VAL Then
                    .Item(jrow, FlxCol.ave_cr_agari) = "*****"
                Else
                    .Item(jrow, FlxCol.ave_cr_agari) = sv.ToString("F2")
                End If
                class_code += 1
            Next
            .AutoSizeCols()
            .AutoSizeRows()
            .Redraw = True
        End With
    End Sub

    Private Sub BtnRedisp_Click(sender As Object, e As EventArgs) Handles BtnRedisp.Click
        ShowTable()
    End Sub

    Private Sub CbBa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbBa.SelectedIndexChanged
        SetupCombobox2()
    End Sub

    Private Sub CbSyubetu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbSyubetu.SelectedIndexChanged
        SetupCombobox2()
    End Sub
End Class