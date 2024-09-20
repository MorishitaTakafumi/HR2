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

End Class
