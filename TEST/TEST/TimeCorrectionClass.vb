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
                                    WHERE K.secs>0 AND K.cyakujun=1 GROUP BY R.class_code, R.type_code, R.kyori"
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
        Dim ave_times(7) As Single
        Dim std_time As Single = 9999
        For j As Integer = 0 To m_bf.Count - 1
            With m_bf(j)
                If .type_code = type_code AndAlso .kyori = kyori Then
                    ave_times(.class_code) = .ave_time
                    'Open以下で最小の平均値を標準値とする
                    If .class_code <= 4 AndAlso .ave_time < std_time Then
                        std_time = .ave_time
                    End If
                End If
            End With
        Next
        'いま対象としているクラス以下のクラスでの最小値を求める
        '仮に、G3の平均＜G2の平均であればG2の平均はG3の平均を使う
        '
        Dim minv As Single = 9999
        For j As Integer = class_code To 0 Step -1
            If ave_times(j) < minv AndAlso ave_times(j) > 0 Then
                minv = ave_times(j)
            End If
        Next
        If std_time > 0 Then
            Dim sa As Single = minv - std_time
            If class_code > 4 AndAlso sa > 0 Then
                Return 0
            ElseIf class_code < 4 AndAlso sa < 0 Then
                Return 0
            Else
                Return sa
            End If
        Else
            Return DMY_VAL
        End If
    End Function

    '上りの補正値取得
    Public Function get_agari_correction(ByVal class_code As Integer, ByVal type_code As Integer, ByVal kyori As Integer) As Single
        Dim ave_agari(7) As Single
        Dim std_agari As Single = 9999
        For j As Integer = 0 To m_bf.Count - 1
            With m_bf(j)
                If .type_code = type_code AndAlso .kyori = kyori Then
                    ave_agari(.class_code) = .ave_agari
                    'Open以下で最小の平均値を標準値とする
                    If .class_code <= 4 AndAlso .ave_agari < std_agari Then
                        std_agari = .ave_agari
                    End If
                End If
            End With
        Next
        'いま対象としているクラス以下のクラスでの最小値を求める
        '仮に、G3の平均＜G2の平均であればG2の平均はG3の平均を使う
        '
        Dim minv As Single = 9999
        For j As Integer = class_code To 0 Step -1
            If ave_agari(j) < minv AndAlso ave_agari(j) > 0 Then
                minv = ave_agari(j)
            End If
        Next
        If std_agari > 0 Then
            Dim sa As Single = minv - std_agari
            If class_code > 4 AndAlso sa > 0 Then
                Return 0
            ElseIf class_code < 4 AndAlso sa < 0 Then
                Return 0
            Else
                Return sa
            End If
        Else
            Return DMY_VAL
        End If
    End Function
End Class
