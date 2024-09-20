Public Class SyutubaListClass
    Private m_bf As New List(Of SyutubaClass)

    Public ReadOnly Property cnt As Integer
        Get
            Return m_bf.Count
        End Get
    End Property

    Public Sub init()
        m_bf.Clear()
    End Sub

    Public Sub add1(ByVal o As SyutubaClass)
        m_bf.Add(o)
    End Sub

    Public Function GetBodyRef(ByVal idx As Integer) As SyutubaClass
        Return m_bf(idx)
    End Function


End Class
