Public Class KekkaStoreClass
    'レース結果のキャッシュクラス

    Private MAX_CNT As Integer = 300
    Private KekkaQ As New Queue(Of KekkaListClass)

    Public Sub add1(ByVal o As KekkaListClass)
        KekkaQ.Enqueue(o)
        If KekkaQ.Count > MAX_CNT Then
            KekkaQ.Dequeue()
        End If
    End Sub

    Public Function GetData(ByVal dt As Date, ByVal race_name As String) As KekkaListClass
        For Each item In KekkaQ
            If dt.Date = item.raceHeader.dt.Date AndAlso IsRaceNameMatch(item.raceHeader.race_name, race_name) Then
                Return item.Clone()
            End If
        Next
        Return Nothing
    End Function
End Class
