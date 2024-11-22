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
            .Rows.Count = 7
            .Rows.Fixed = 1
            .Cols.Fixed = 1
            .Item(0, FlxCol.coefRank) = "係数範囲"
            .Item(0, FlxCol.spanScore) = "span係数"
            .Item(0, FlxCol.dateScore) = "date係数"
            .Item(0, FlxCol.distScore) = "dist係数"

            .Item(1, FlxCol.coefRank) = "1.300～"
            .Item(2, FlxCol.coefRank) = "1.225～1.300"
            .Item(3, FlxCol.coefRank) = "1.150～1.225"
            .Item(4, FlxCol.coefRank) = "1.075～1.150"
            .Item(5, FlxCol.coefRank) = "1.000～1.075"
            .Item(6, FlxCol.coefRank) = "＝1.000"

            .Styles.Normal.Border.Style = BorderStyleEnum.Flat
            .Styles.Fixed.TextAlign = TextAlignEnum.CenterCenter
            .Styles.Normal.TextAlign = TextAlignEnum.LeftCenter
            .Styles.Normal.WordWrap = True
            .Rows.MinSize = 30
            .Cols.MaxSize = 200

            .AllowMerging = AllowMergingEnum.None
            .AllowEditing = False

            .AllowSorting = AllowSortingEnum.SingleColumn
            .AllowFiltering = False
        End With
    End Sub


    Public Sub entry(ByVal spanCoefCnt(,) As Integer, ByVal dateCoefCnt(,) As Integer, ByVal distCoefCnt(,) As Integer)
        SetupFlex()
        Dim cnt As Integer
        For j As Integer = 0 To 5
            cnt = spanCoefCnt(j, 0) + spanCoefCnt(j, 1) + spanCoefCnt(j, 2)
            If cnt > 0 Then
                flx.Item(j + 1, FlxCol.spanScore) = (spanCoefCnt(j, 0) / cnt).ToString("F2") & " | " &
                                                    ((spanCoefCnt(j, 1) + spanCoefCnt(j, 0)) / cnt).ToString("F2") & "(" & cnt.ToString & ")"
            Else
                flx.Item(j + 1, FlxCol.spanScore) = "***"
            End If

            cnt = dateCoefCnt(j, 0) + dateCoefCnt(j, 1) + dateCoefCnt(j, 2)
            If cnt > 0 Then
                flx.Item(j + 1, FlxCol.dateScore) = (dateCoefCnt(j, 0) / cnt).ToString("F2") & " | " &
                                                    ((dateCoefCnt(j, 1) + dateCoefCnt(j, 0)) / cnt).ToString("F2") & "(" & cnt.ToString & ")"
            Else
                flx.Item(j + 1, FlxCol.dateScore) = "***"
            End If

            cnt = distCoefCnt(j, 0) + distCoefCnt(j, 1) + distCoefCnt(j, 2)
            If cnt > 0 Then
                flx.Item(j + 1, FlxCol.distScore) = (distCoefCnt(j, 0) / cnt).ToString("F2") & " | " &
                                                    ((distCoefCnt(j, 1) + distCoefCnt(j, 0)) / cnt).ToString("F2") & "(" & cnt.ToString & ")"
            Else
                flx.Item(j + 1, FlxCol.distScore) = "***"
            End If

            'flx.Item(j + 1, FlxCol.spanScore) = (spanCoefCnt(j, 0) + spanCoefCnt(j, 1) + spanCoefCnt(j, 2)).ToString
            'flx.Item(j + 1, FlxCol.dateScore) = (dateCoefCnt(j, 0) + dateCoefCnt(j, 1) + dateCoefCnt(j, 2)).ToString
            'flx.Item(j + 1, FlxCol.distScore) = (distCoefCnt(j, 0) + distCoefCnt(j, 1) + distCoefCnt(j, 2)).ToString
        Next
        flx.AutoSizeCols()
        lb_guide.Text = "凡例）連帯率｜複勝率(件数)"
        Show()
    End Sub

End Class