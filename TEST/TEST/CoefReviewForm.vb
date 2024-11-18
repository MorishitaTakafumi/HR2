Imports C1.Win.C1FlexGrid

Public Class CoefReviewForm
    'span/date/distance係数と着順/人気の差の検証

    Private Enum FlxCol
        coefRank = 0
        spanScore = 1
        dateScore = 2
        distScore = 3
        cols = distScore + 1
    End Enum

    Private Sub SetupFlex()
        With flx
            .Cols.Count = FlxCol.cols
            .Rows.Count = 12
            .Rows.Fixed = 1
            .Cols.Fixed = 1
            .Item(0, FlxCol.coefRank) = "係数範囲"
            .Item(0, FlxCol.spanScore) = "span係数"
            .Item(0, FlxCol.dateScore) = "date係数"
            .Item(0, FlxCol.distScore) = "dist係数"

            .Item(1, FlxCol.coefRank) = "～1.100"
            .Item(2, FlxCol.coefRank) = "～1.075"
            .Item(3, FlxCol.coefRank) = "～1.050"
            .Item(4, FlxCol.coefRank) = "～1.025"
            .Item(5, FlxCol.coefRank) = "～1.000"
            .Item(6, FlxCol.coefRank) = "＝1.000"
            .Item(7, FlxCol.coefRank) = "～0.975"
            .Item(8, FlxCol.coefRank) = "～0.950"
            .Item(9, FlxCol.coefRank) = "～0.925"
            .Item(10, FlxCol.coefRank) = "～0.900"
            .Item(11, FlxCol.coefRank) = "～0.000"

            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 25
            .Cols.MaxSize = 120

            .AllowMerging = AllowMergingEnum.None
            .AllowEditing = False

            .AllowSorting = AllowSortingEnum.SingleColumn
            .AllowFiltering = False
        End With
    End Sub


    Public Sub entry(ByVal spanCoefCnt(,) As Integer, ByVal dateCoefCnt(,) As Integer, ByVal distCoefCnt(,) As Integer)
        SetupFlex()
        For j As Integer = 0 To 10
            'flx.Item(j + 1, FlxCol.spanScore) = spanCoefCnt(j, 0).ToString("D2") & "-" & spanCoefCnt(j, 1).ToString("D2") & "-" & spanCoefCnt(j, 2).ToString("D2")
            'flx.Item(j + 1, FlxCol.dateScore) = dateCoefCnt(j, 0).ToString("D2") & "-" & dateCoefCnt(j, 1).ToString("D2") & "-" & dateCoefCnt(j, 2).ToString("D2")
            'flx.Item(j + 1, FlxCol.distScore) = distCoefCnt(j, 0).ToString("D2") & "-" & distCoefCnt(j, 1).ToString("D2") & "-" & distCoefCnt(j, 2).ToString("D2")

            flx.Item(j + 1, FlxCol.spanScore) = (spanCoefCnt(j, 0) + spanCoefCnt(j, 1) + spanCoefCnt(j, 2)).ToString
            flx.Item(j + 1, FlxCol.dateScore) = (dateCoefCnt(j, 0) + dateCoefCnt(j, 1) + dateCoefCnt(j, 2)).ToString
            flx.Item(j + 1, FlxCol.distScore) = (distCoefCnt(j, 0) + distCoefCnt(j, 1) + distCoefCnt(j, 2)).ToString
        Next
        flx.AutoSizeCols()
        Show()
    End Sub

End Class