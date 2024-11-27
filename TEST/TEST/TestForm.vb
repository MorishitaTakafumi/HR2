Imports System.Data.SQLite

Public Class TestForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim hrefList As New List(Of String)
        Dim hrefCnt As Integer = 0
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "select href from UmaHist GROUP BY href"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    hrefCnt += 1
                End While
                r.Close()
                MsgBox("href count=" & hrefCnt.ToString, MsgBoxStyle.Information, Me.Text)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
    End Sub

    'raceHeaderに登録されているがkekkaが未登録のレース情報を補完する
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dtList As New List(Of Date)
        'Dim raceName As New List(Of String) UmaHistのレース名は省略名になっているので使えない!
        Dim hrefList As New List(Of String)
        Dim fm1 As New Form1
        Dim errmsg As String = ""
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "select A.dt, B.id FROM RaceHeader A LEFT JOIN Kekka B ON A.id=B.race_header_id"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    If IsDBNull(r("id")) Then
                        dtList.Add(r("dt"))
                    End If
                End While
                r.Close()
                '
                cmd.CommandText = "select href from UmaHist WHERE dt=@dt"
                For j As Integer = 0 To dtList.Count - 1
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@dt", dtList(j))
                    r = cmd.ExecuteReader
                    While r.Read
                        If Not hrefList.Contains(r("href")) Then
                            hrefList.Add(r("href"))
                        End If
                    End While
                    r.Close()
                Next
                For j As Integer = 0 To hrefList.Count - 1
                    fm1.entry(hrefList(j), "", "", True)
                    If fm1.DbErrMsg.Length > 0 Then
                        errmsg = fm1.DbErrMsg
                        Exit For
                    End If
                Next
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub

    'AnaVal生成テスト
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ListBox1.Items.Clear()

        Dim bamei As String = "ドウデュース"
        Dim dtRace As Date = DateSerial(2024, 10, 27)
        Dim oUmaHeader As New UmaHeaderClass
        Dim errmsg As String = ""
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                errmsg = oUmaHeader.load(cmd, bamei)
                If errmsg.Length = 0 Then
                    ListBox1.Items.Add("馬名：" & oUmaHeader.bamei)
                    Dim oUmaHist As New umaHistListClass
                    Dim KekkaList As New KekkaListClass
                    errmsg = oUmaHist.load(cmd, oUmaHeader.rec_id)
                    If errmsg.Length = 0 Then
                        Dim rA As New raceAnanClass
                        rA.spanScore = oUmaHist.GetSpanScore(dtRace, rA.spanVal)
                        rA.dateScore = oUmaHist.GetSameDateSameKyoriScore(dtRace, 2000, "芝", rA.kyoriScore)
                        ListBox1.Items.Add("spanScore=" & AnaValClass.Score2String(rA.spanScore))
                        ListBox1.Items.Add("kyoriScore=" & AnaValClass.Score2String(rA.kyoriScore))
                        ListBox1.Items.Add("dateScore=" & AnaValClass.Score2String(rA.dateScore))
                        For j As Integer = 0 To oUmaHist.cnt - 1
                            KekkaList.init()
                            Dim oRaceHead As RaceHeaderClass = KekkaList.raceHeader
                            errmsg = oRaceHead.loadByUmaHist(cmd, oUmaHist.GetBodyRef(j))
                            If errmsg.Length > 0 Then
                                Exit For
                            End If
                            If oRaceHead.id > 0 AndAlso DateDiff(DateInterval.Day, oRaceHead.dt, dtRace) > 1 Then
                                errmsg = KekkaList.load(cmd, oRaceHead.id)
                                If errmsg.Length > 0 Then
                                    Exit For
                                Else
                                    Dim oKekka As KekkaClass = KekkaList.GetBodyRefByBamei(bamei)
                                    If oKekka IsNot Nothing Then
                                        KekkaList.setAgarisa(oRaceHead)
                                        ListBox1.Items.Add(oRaceHead.race_name)
                                        ListBox1.Items.Add("上り差=" & oKekka.agarisa.ToString("F1") & "　着差=" & oKekka.cyakusa.ToString("F1"))
                                        ListBox1.Items.Add("")
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub
End Class