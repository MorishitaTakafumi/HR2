Imports System.Data.SQLite

Public Class ParamSetClass
    '適合度計算のためのパラメータセット

    Public Property rec_id As Integer
    Public Property remarks As String

    '*** spanScoreによる係数 ***
    Public scoreP(3) As Double '4着以下,3着,2着,1着の順 GlblMod.GetScoreCoefficient()
    '*** dateScoreによる係数 ***
    Public scoreP2(3) As Double '4着以下,3着,2着,1着の順 GlblMod.GetScoreCoefficient()

    '*** 上り差／着差の適合度を得点化するための係数 ***
    Public timeR1 As Double 'myTimeがcmpTimeより良いとき満点からの減量を決める係数
    Public timeR2 As Double 'myTimeがcmpTimeより悪いとき満点からの減量を決める係数
    Public timeP(3) As Double '何走前かでの重みづけ用 1走前,2走前,3走前,4走前の順 GlblMod.GetDegreeOfFit_time()
    Public timeZoneCoef As Double 'タイムゾーンに応じた係数　GlblMod.GetTimeZoneCoef()

    Public waribiki As Double = 0.025 '近４走にデータが不足する時の割引率
    Public agarisaCyakusaRate As Double = 1.1 '上り差と着差の得点有効率：着差を１としたときの上り差の掛け率

    Private rand As New Random()

    Public Sub New()
        rec_id = -1
        remarks = ""
    End Sub

    Public Sub setDefValue()
        rec_id = 0
        scoreP(0) = 0.003
        scoreP(1) = 0.01
        scoreP(2) = 0.025
        scoreP(3) = 0.05
        scoreP2(0) = 0.003
        scoreP2(1) = 0.01
        scoreP2(2) = 0.025
        scoreP2(3) = 0.05
        timeR1 = 0.05
        timeR2 = 0.15
        timeP(0) = 1
        timeP(1) = 0.95
        timeP(2) = 0.9
        timeP(3) = 0.85
        timeZoneCoef = 0.03
        remarks = "プログラム既定値"
    End Sub

    Public Sub shiftValue()
        rec_id = -1
        remarks = "自動発生" & Now.ToString("D")
        'myTimeがcmpTimeより良いとき満点からの減量を決める係数 -0.05～+0.25
        timeR1 = makeRandomValue(-0.05, 0.25)
        'myTimeがcmpTimeより悪いとき満点からの減量を決める係数 0.1～0.4
        timeR2 = makeRandomValue(0.1, 0.4)
        '何走前かでの重みづけ用 1走前,2走前,3走前,4走前の順
        timeP(0) = makeRandomValue(0.85, 1)
        timeP(1) = makeRandomValue(0.85, 1)
        timeP(2) = makeRandomValue(0.85, 1)
        timeP(3) = makeRandomValue(0.85, 1)
        'タイムゾーンに応じた係数 0.01～0.1
        timeZoneCoef = makeRandomValue(0.01, 0.1)
        'spanScoreに応じた係数
        scoreP(0) = makeRandomValue(-0.05, 0.05) '4着以下
        scoreP(1) = makeRandomValue(0, 0.05)
        scoreP(2) = makeRandomValue(0, 0.05)
        scoreP(3) = makeRandomValue(0, 0.05) '1着
        'dateScoreに応じた係数
        scoreP2(0) = makeRandomValue(-0.05, 0.05) '4着以下
        scoreP2(1) = makeRandomValue(0, 0.05)
        scoreP2(2) = makeRandomValue(0, 0.05)
        scoreP2(3) = makeRandomValue(0, 0.05) '1着
    End Sub

    Private Function makeRandomValue(ByVal minv As Double, ByVal maxv As Double) As Double
        Dim sa As Integer = Int((maxv - minv) * 10000)
        Dim tmpv As Double = 0.0001 * rand.Next(0, sa)
        Return tmpv + minv
    End Function

    Private Function packParams() As String
        Return "timeR1:" & timeR1.ToString("F3") & "," &
               "timeR2:" & timeR2.ToString("F3") & "," &
               "timeP0:" & timeP(0).ToString("F3") & "," &
               "timeP1:" & timeP(1).ToString("F3") & "," &
               "timeP2:" & timeP(2).ToString("F3") & "," &
               "timeP3:" & timeP(3).ToString("F3") & "," &
               "timeZoneCoef:" & timeZoneCoef.ToString("F3") & "," &
               "scoreP0:" & scoreP(0).ToString("F3") & "," &
               "scoreP1:" & scoreP(1).ToString("F3") & "," &
               "scoreP2:" & scoreP(2).ToString("F3") & "," &
               "scoreP3:" & scoreP(3).ToString("F3") & "," &
               "scoreP20:" & scoreP2(0).ToString("F3") & "," &
               "scoreP21:" & scoreP2(1).ToString("F3") & "," &
               "scoreP22:" & scoreP2(2).ToString("F3") & "," &
               "scoreP23:" & scoreP2(3).ToString("F3")
    End Function

    Private Sub unpackParams(ByVal str_params As String)
        Dim sbf() As String = Split(str_params, ",")
        For j As Integer = 0 To sbf.Length - 1
            Dim ip As Integer = InStr(sbf(j), ":")
            If ip > 0 Then
                Select Case sbf(j).Substring(0, ip - 1)
                    Case "timeR1"
                        timeR1 = CDbl(sbf(j).Substring(ip))
                    Case "timeR2"
                        timeR2 = CDbl(sbf(j).Substring(ip))
                    Case "timeP0"
                        timeP(0) = CDbl(sbf(j).Substring(ip))
                    Case "timeP1"
                        timeP(1) = CDbl(sbf(j).Substring(ip))
                    Case "timeP2"
                        timeP(2) = CDbl(sbf(j).Substring(ip))
                    Case "timeP3"
                        timeP(3) = CDbl(sbf(j).Substring(ip))
                    Case "timeZoneCoef"
                        timeZoneCoef = CDbl(sbf(j).Substring(ip))
                    Case "scoreP0"
                        scoreP(0) = CDbl(sbf(j).Substring(ip))
                    Case "scoreP1"
                        scoreP(1) = CDbl(sbf(j).Substring(ip))
                    Case "scoreP2"
                        scoreP(2) = CDbl(sbf(j).Substring(ip))
                    Case "scoreP3"
                        scoreP(3) = CDbl(sbf(j).Substring(ip))
                    Case "scoreP20"
                        scoreP2(0) = CDbl(sbf(j).Substring(ip))
                    Case "scoreP21"
                        scoreP2(1) = CDbl(sbf(j).Substring(ip))
                    Case "scoreP22"
                        scoreP2(2) = CDbl(sbf(j).Substring(ip))
                    Case "scoreP23"
                        scoreP2(3) = CDbl(sbf(j).Substring(ip))
                End Select
            End If
        Next
    End Sub

    Public Function load(ByVal arg_id As Integer) As String
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT * FROM Param WHERE id=@id"
                cmd.Parameters.AddWithValue("@id", arg_id)
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                If r.Read Then
                    rec_id = r("id")
                    remarks = r("remarks")
                    unpackParams(r("params"))
                End If
                r.Close()
                Return ""
            Catch ex As Exception
                Return "ParamSetClass.addNew() " & ex.Message
            End Try
        End Using
    End Function

    Public Function addNew() As String
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "INSERT INTO Param(remarks, params) VALUES(@remarks, @params)"
                cmd.Parameters.AddWithValue("@remarks", remarks)
                cmd.Parameters.AddWithValue("@params", packParams())
                cmd.ExecuteNonQuery()
                '
                cmd.Parameters.Clear()
                cmd.CommandText = "SELECT last_insert_rowid() As new_id"
                Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
                If r.Read Then
                    rec_id = r("new_id")
                End If
                r.Close()
                Return ""
            Catch ex As Exception
                Return "ParamSetClass.addNew() " & ex.Message
            End Try
        End Using
    End Function

    Public Function update() As String
        If rec_id <= 0 Then
            Return "未登録なので上書きできません！"
        End If

        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "UPDATE Param SET remarks=@remarks, params=@params WHERE id=@id"
                cmd.Parameters.AddWithValue("@remarks", remarks)
                cmd.Parameters.AddWithValue("@params", packParams())
                cmd.Parameters.AddWithValue("@id", rec_id)
                cmd.ExecuteNonQuery()
                Return ""
            Catch ex As Exception
                Return "ParamSetClass.update() " & ex.Message
            End Try
        End Using
    End Function
End Class
