Imports System.Data.SQLite

Public Class TimeCorrectionClass
    'クラス間のタイム補正値：Openクラスを基準とする

    Private Class c_t_k
        Public class_code As Integer
        Public type_code As Integer
        Public kyori As Integer
        Public ave_time As Single
        Public ave_agari As Single
    End Class

    Private m_bf As New List(Of c_t_k)

    Public Function createTable() As String

        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT R.class_code, R.type_code, R.kyori, avg(K.secs) AS ave_time, avg(K.agari) AS ave_agari 
                                    FROM kekka K INNER JOIN raceheader R ON K.race_header_id=R.id 
                                    WHERE K.secs>0 AND K.cyakujun=2 GROUP BY R.class_code, R.type_code, R.kyori"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    Dim a As New c_t_k
                    With a
                        .class_code = r("class_code")
                        .type_code = r("type_code")
                        .kyori = r("kyori")
                        .ave_time = r("ave_time")
                        .ave_agari = r("ave_agari")
                    End With
                    m_bf.Add(a)
                End While
                r.Close()
                Return ""
            Catch ex As Exception
                Return "TimeCorrectionClass.crateTable() " & ex.Message
            End Try
        End Using
    End Function


    '時計の補正値取得
    Public Function get_time_correction(ByVal class_code As Integer, ByVal type_code As Integer, ByVal kyori As Integer) As Single
        If class_code = 4 Then 'Op/Lクラスのとき
            Return 0
        End If
        Dim std_time As Single = 0
        Dim my_time As Single = 0
        For j As Integer = 0 To m_bf.Count - 1
            With m_bf(j)
                If .type_code = type_code AndAlso .kyori = kyori Then
                    If .class_code = 4 Then
                        std_time = .ave_time
                        If my_time > 0 Then
                            Exit For
                        End If
                    ElseIf .class_code = class_code Then
                        my_time = .ave_time
                        If std_time > 0 Then
                            Exit For
                        End If
                    End If
                End If
            End With
        Next
        If std_time > 0 AndAlso my_time > 0 Then
            Return my_time - std_time
        Else
            Return 0
        End If
    End Function

    '上りの補正値取得
    Public Function get_agari_correction(ByVal class_code As Integer, ByVal type_code As Integer, ByVal kyori As Integer) As Single
        If class_code = 4 Then 'Op/Lクラスのとき
            Return 0
        End If
        Dim std_agari As Single = 0
        Dim my_agari As Single = 0
        For j As Integer = 0 To m_bf.Count - 1
            With m_bf(j)
                If .type_code = type_code AndAlso .kyori = kyori Then
                    If .class_code = 4 Then
                        std_agari = .ave_agari
                        If my_agari > 0 Then
                            Exit For
                        End If
                    ElseIf .class_code = class_code Then
                        my_agari = .ave_agari
                        If std_agari > 0 Then
                            Exit For
                        End If
                    End If
                End If
            End With
        Next
        If std_agari > 0 AndAlso my_agari > 0 Then
            Return my_agari - std_agari
        Else
            Return 0
        End If
    End Function
End Class
