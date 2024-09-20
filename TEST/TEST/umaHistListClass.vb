Public Class umaHistListClass
    Private m_bf As New List(Of UmaHistClass)

    Public ReadOnly Property cnt As Integer
        Get
            Return m_bf.Count
        End Get
    End Property

    Public Sub init()
        m_bf.Clear()
    End Sub

    Public Sub add1(ByVal o As UmaHistClass)
        m_bf.Add(o)
    End Sub

    Public Function GetBodyRef(ByVal idx As Integer) As UmaHistClass
        Return m_bf(idx)
    End Function

    '前走日取得
    Private Function GetDtZenso() As Date
        Dim dtMax As Date = #1900/1/1#
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun > 0 OrElse m_bf(j).cyakujun = -999 Then '中止は含めるが取消、除外は含めない
                If m_bf(j).dt > dtMax Then
                    dtMax = m_bf(j).dt
                End If
            End If
        Next
        Return dtMax
    End Function

    '初出走インデックス取得
    Private Function Get1stIndex() As Integer
        For j As Integer = cnt - 1 To 0 Step -1
            If m_bf(j).cyakujun > 0 OrElse m_bf(j).cyakujun = -999 Then '中止は含めるが取消、除外は含めない
                Return j
            End If
        Next
        Return -1
    End Function


    '前走と今回レース日の間隔での成績調査
    Public Function GetSpanVal(ByVal dtRace As Date) As String
        Dim span As Integer = DateDiff(DateInterval.Day, GetDtZenso(), dtRace)
        Dim Cyakucnt(3) As Integer
        For j As Integer = 0 To 3
            Cyakucnt(j) = 0
        Next
        Dim st_idx As Integer = Get1stIndex()
        If st_idx >= 0 Then
            Dim dtZenso As Date = m_bf(st_idx).dt
            Dim ttlCnt As Integer = 0
            For j As Integer = st_idx - 1 To 0 Step -1
                If m_bf(j).cyakujun > 0 OrElse m_bf(j).cyakujun = -999 Then '中止は含めるが取消、除外は含めない
                    Dim hisa As Integer = DateDiff(DateInterval.Day, dtZenso, m_bf(j).dt)
                    If hisa >= span - 7 AndAlso hisa <= span + 7 Then
                        If m_bf(j).cyakujun = 1 Then
                            Cyakucnt(0) += 1
                        ElseIf m_bf(j).cyakujun = 2 Then
                            Cyakucnt(1) += 1
                        ElseIf m_bf(j).cyakujun = 3 Then
                            Cyakucnt(2) += 1
                        Else
                            Cyakucnt(3) += 1
                        End If
                        ttlCnt += 1
                    End If
                    dtZenso = m_bf(j).dt
                End If
            Next
            If ttlCnt > 0 Then
                Dim ttlp As Integer = Cyakucnt(0) * 5 + Cyakucnt(1) * 3 + Cyakucnt(2) * 1
                Dim avep As Single = ttlp / ttlCnt
                Return ttlp.ToString & "/" & avep.ToString("F2") & "(" & ttlCnt & "回)"
            End If
        End If
        Return "－"
    End Function

End Class
