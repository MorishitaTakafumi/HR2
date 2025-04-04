Public Class UmaStoreClass
    '馬情報のキャッシュクラス

    Private MAX_CNT As Integer = 300
    Private m_bf As New List(Of umaHistListClass)

    Private UmaQ As New Queue(Of umaHistListClass)

    Private m_CntRequest As Integer = 0
    Private m_CntHit As Integer = 0

    Public Sub ClearCnt()
        m_CntRequest = 0
        m_CntHit = 0
    End Sub

    Public Function GetCntRequestHit() As String
        Return m_CntHit.ToString & "/" & m_CntRequest.ToString
    End Function

    Public Sub add1(ByVal o As umaHistListClass)
        removeData(o.umaHeader.bamei)

        m_bf.Add(o)
        If m_bf.Count > MAX_CNT Then
            m_bf.RemoveAt(0)
        End If
    End Sub

    Public Function GetData(ByVal bamei As String) As umaHistListClass
        m_CntRequest += 1
        For Each item In m_bf
            If item.umaHeader.bamei = bamei Then
                m_CntHit += 1
                Return item.Clone()
            End If
        Next
        Return Nothing
    End Function

    Public Sub removeData(ByVal bamei As String)
        For j As Integer = 0 To m_bf.Count - 1
            If m_bf(j).umaHeader.bamei = bamei Then
                m_bf.RemoveAt(j)
                Return
            End If
        Next
    End Sub

End Class
