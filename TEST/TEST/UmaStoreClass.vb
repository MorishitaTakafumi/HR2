Public Class UmaStoreClass
    '馬情報のキャッシュクラス

    Private MAX_CNT As Integer = 300
    Private UmaQ As New Queue(Of umaHistListClass)

    Public Sub add1(ByVal o As umaHistListClass)
        UmaQ.Enqueue(o)
        If UmaQ.Count > MAX_CNT Then
            UmaQ.Dequeue()
        End If
    End Sub

    Public Function GetData(ByVal bamei As String) As umaHistListClass
        For Each item In UmaQ
            If item.umaHeader.bamei = bamei Then
                Return item.Clone()
            End If
        Next
        Return Nothing
    End Function

End Class
