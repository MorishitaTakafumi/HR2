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

    Public Function GetData(ByVal orh As UmaHistClass) As KekkaListClass
        For Each item In KekkaQ
            If orh.dt.Date = item.raceHeader.dt.Date AndAlso
               orh.jo_code = item.raceHeader.jo_code AndAlso
               orh.distance = item.raceHeader.kyori AndAlso
               orh.type_code = item.raceHeader.type_code AndAlso
               IsRaceNameMatch(item.raceHeader.race_name, orh.racename) Then
                Return item.Clone()
            End If
        Next
        Return Nothing
    End Function
End Class
