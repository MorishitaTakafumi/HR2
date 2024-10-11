Imports System.Data.SQLite

Public Class AnaValClass
    'レース解析値

    Public Property id As Integer
    Public Property rhead_id As Integer
    Public Property cyakujun As Integer
    Public Property spanScore As Integer

    Private m_agarisa(3) As Single
    Private m_cyakusa(3) As Single
    Public Property dateScore As Integer
    Public Property kyoriScore As Integer
    Public Property bamei As String
    Public Property ninki As Short

    Private m_flags As Integer

    Public Property agarisa(ByVal idx As Integer) As Single
        Get
            Return m_agarisa(idx)
        End Get
        Set(value As Single)
            m_agarisa(idx) = value
        End Set
    End Property

    Public Property cyakusa(ByVal idx As Integer) As Single
        Get
            Return m_cyakusa(idx)
        End Get
        Set(value As Single)
            m_cyakusa(idx) = value
        End Set
    End Property

    Public Property OtherTypeRaceFlag(ByVal idx As Integer) As Boolean
        Get
            Return ((m_flags And (2 ^ idx)) <> 0)
        End Get
        Set(value As Boolean)
            If value Then
                m_flags = m_flags Or (2 ^ idx)
            Else
                m_flags = m_flags And (Not (2 ^ idx))
            End If
        End Set
    End Property

    Public ReadOnly Property spanVal() As String
        Get
            Return Score2String(spanScore)
        End Get
    End Property

    Public ReadOnly Property dateVal() As String
        Get
            Return Score2String(dateScore)
        End Get
    End Property

    Public ReadOnly Property kyoriVal() As String
        Get
            Return Score2String(kyoriScore)
        End Get
    End Property

    Public Sub New()
        id = -1
        rhead_id = -1
        cyakujun = -1
        spanScore = 0
        For j As Integer = 0 To 3
            m_agarisa(j) = DMY_VAL
            m_cyakusa(j) = DMY_VAL
        Next
        dateScore = 0
        kyoriScore = 0
        bamei = ""
        ninki = 0
        m_flags = 0
    End Sub

    '得点値を"0-0-0-0"の形式の文字列で返す
    Public Shared Function Score2String(ByVal score As Integer) As String
        Dim ss As String = ""
        For j As Integer = 3 To 1 Step -1
            Dim xv As Integer = 10 ^ (j * 2)
            ss &= (score \ xv).ToString & "-"
            score = score Mod xv
        Next
        Return ss & score.ToString
    End Function

    '上り補正値と着差を"上り補正値(着差)"の形式の文字列で返す
    Public Function GetHist(ByVal idx As Integer) As String
        If agarisa(idx) = DMY_VAL Then
            Return ""
        End If

        Dim ss As String = agarisa(idx).ToString("F1") & "(" & cyakusa(idx).ToString("F1") & ")"
        If OtherTypeRaceFlag(idx) Then
            Return "[" & ss & "]"
        Else
            Return ss
        End If
    End Function

    '新規登録
    Public Function addNew(ByVal cmd As SQLiteCommand) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO AnaVal(rhead_id, cyakujun, spanScore, agarisa1, cyakusa1, agarisa2, cyakusa2, agarisa3, cyakusa3, agarisa4, cyakusa4, dateScore, kyoriScore, bamei, ninki, flags) 
                            VALUES(@rhead_id, @cyakujun, @spanScore, @agarisa1, @cyakusa1, @agarisa2, @cyakusa2, @agarisa3, @cyakusa3, @agarisa4, @cyakusa4, @dateScore, @kyoriScore, @bamei, @ninki, @flags)"

            cmd.Parameters.AddWithValue("@rhead_id", rhead_id)
            cmd.Parameters.AddWithValue("@cyakujun", cyakujun)
            cmd.Parameters.AddWithValue("@spanScore", spanScore)
            For j As Integer = 0 To 3
                cmd.Parameters.AddWithValue("@agarisa" & (j + 1).ToString, m_agarisa(j))
                cmd.Parameters.AddWithValue("@cyakusa" & (j + 1).ToString, m_cyakusa(j))
            Next
            cmd.Parameters.AddWithValue("@dateScore", dateScore)
            cmd.Parameters.AddWithValue("@kyoriScore", kyoriScore)
            cmd.Parameters.AddWithValue("@bamei", bamei)
            cmd.Parameters.AddWithValue("@ninki", ninki)
            cmd.Parameters.AddWithValue("@flags", m_flags)
            cmd.ExecuteNonQuery()
            Return ""
        Catch ex As Exception
            Return "AnaValClass.addNew() " & ex.Message
        End Try
    End Function
End Class
