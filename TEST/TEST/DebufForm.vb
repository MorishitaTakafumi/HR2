Imports C1.Win.C1FlexGrid
Imports System.Data.SQLite

Public Class DebufForm
    Private Enum FlxCol
        classname = 0
        ave_time = 1
        ave_agari = 2
        cols
    End Enum

    Public Sub New()
        InitializeComponent()
        SetupCombobox()
        SetUpFlx()
    End Sub

    Private Sub SetupCombobox()
        With CbSyubetu
            .Items.Clear()
            .Items.Add("芝")
            .Items.Add("ダート")
            .Items.Add("障害")
            .SelectedIndex = 0
        End With
        '距離はDBから選択肢を取得する
        Dim errmsg As String = ""
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = conn.CreateCommand
            conn.Open()
            Try
                cmd.CommandText = "SELECT DISTINCT(kyori) FROM RaceHeader ORDER BY kyori"
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
        Dim type_code As Integer = CbSyubetu.SelectedIndex + 1
        Dim kyori As Integer = CInt(CbKyori.Text)
        Dim class_code As Integer = 0
        Dim sv As Single
        With flx
            .Redraw = False
            For jrow As Integer = .Rows.Fixed To .Rows.Count - 1
                sv = oTC.get_time_correction(class_code, type_code, kyori)
                If sv = DMY_VAL Then
                    .Item(jrow, FlxCol.ave_time) = "*****"
                Else
                    .Item(jrow, FlxCol.ave_time) = sv.ToString("F2")
                End If
                sv = oTC.get_agari_correction(class_code, type_code, kyori)
                If sv = DMY_VAL Then
                    .Item(jrow, FlxCol.ave_agari) = "*****"
                Else
                    .Item(jrow, FlxCol.ave_agari) = sv.ToString("F2")
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
End Class