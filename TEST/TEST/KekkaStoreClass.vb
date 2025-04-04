Public Class KekkaStoreClass
    'レース結果のキャッシュクラス

    Private MAX_CNT As Integer = 300
    Private KekkaQ As New Queue(Of KekkaListClass)

    Private m_CntRequest As Integer = 0
    Private m_CntHit As Integer = 0

    Public Sub ClearCnt()
        m_CntRequest = 0
        m_CntHit = 0
    End Sub

    Public Function GetCntRequestHit() As String
        Return m_CntHit.ToString & "/" & m_CntRequest.ToString
    End Function

    Public Sub add1(ByVal o As KekkaListClass)
        KekkaQ.Enqueue(o)
        If KekkaQ.Count > MAX_CNT Then
            KekkaQ.Dequeue()
        End If
    End Sub

    Public Function GetData(ByVal orh As UmaHistClass) As KekkaListClass
        m_CntRequest += 1
        For Each item In KekkaQ
            If orh.dt.Date = item.raceHeader.dt.Date AndAlso
               orh.jo_code = item.raceHeader.jo_code AndAlso
               orh.distance = item.raceHeader.kyori AndAlso
               orh.type_code = item.raceHeader.type_code AndAlso
               item.GetBodyRefByBamei(orh.bamei) IsNot Nothing AndAlso
                IsRaceNameMatch(item.raceHeader.race_name, orh.racename) Then
                m_CntHit += 1
                Return item.Clone()
            End If
        Next
        Return Nothing
    End Function
End Class
