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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox1.Items.Clear()
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cnt As Integer = 0
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT name, COUNT(*) AS cnt FROM UmaHeader GROUP BY name HAVING COUNT(*)>1"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    cnt += 1
                    ListBox1.Items.Add(cnt.ToString & ":" & CStr(r("name")) & " " & CInt(r("cnt")).ToString)
                End While
                r.Close()
                If cnt > 0 Then
                    ListBox1.Items.Add("以上")
                Else
                    ListBox1.Items.Add("該当なし")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ListBox1.Items.Clear()
        Dim bameiList As New List(Of String)
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT name FROM UmaHeader GROUP BY name HAVING COUNT(*)>1"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    bameiList.Add(r("name"))
                End While
                r.Close()
                '
                Dim cnt As Integer
                Dim subsql As String
                For j As Integer = 0 To bameiList.Count - 1
                    lb_msg.Text = j.ToString & "/" & bameiList.Count.ToString & " 処理中"
                    Me.Refresh()
                    cnt = 0
                    subsql = ""
                    cmd.CommandText = "SELECT id FROM UmaHeader WHERE name=@name"
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@name", bameiList(j))
                    r = cmd.ExecuteReader
                    While r.Read
                        If cnt > 0 Then
                            subsql &= r.GetInt32(0).ToString & ","
                        End If
                        cnt += 1
                    End While
                    r.Close()

                    cmd.Parameters.Clear()
                    cmd.CommandText = "DELETE FROM UmaHeader WHERE id IN(" & subsql.Substring(0, subsql.Length - 1) & ")"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "DELETE FROM UmaHist WHERE uma_id IN(" & subsql.Substring(0, subsql.Length - 1) & ")"
                    cmd.ExecuteNonQuery()
                Next
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ListBox1.Items.Clear()
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cnt As Integer = 0
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT race_name, dt, jo_code, type_code, kyori, COUNT(*) AS cnt FROM RaceHeader 
                                    GROUP BY race_name, dt, jo_code, type_code, kyori HAVING COUNT(*)>1"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    cnt += 1
                    ListBox1.Items.Add(cnt.ToString & ":" & CStr(r("race_name")) & " " & CDate(r("dt")).ToString("yyyy年M月d日") &
                                       " " & GetKeibajoName(r("jo_code")) & " " &
                                       GetRaceTypeName(r("type_code")) & " " & CInt(r("kyori")).ToString & " " & CInt(r("cnt")).ToString)
                End While
                r.Close()
                If cnt > 0 Then
                    ListBox1.Items.Add("以上")
                Else
                    ListBox1.Items.Add("該当なし")
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ListBox1.Items.Clear()
        Dim raceNameList As New List(Of String)
        Dim dtList As New List(Of Date)
        Dim joList As New List(Of Integer)
        Dim typeList As New List(Of Integer)
        Dim kyoriList As New List(Of Integer)
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT race_name, dt, jo_code, type_code, kyori FROM RaceHeader 
                                    GROUP BY race_name, dt, jo_code, type_code, kyori HAVING COUNT(*)>1"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    raceNameList.Add(r("race_name"))
                    dtList.Add(r("dt"))
                    joList.Add(r("jo_code"))
                    typeList.Add(r("type_code"))
                    kyoriList.Add(r("kyori"))
                End While
                r.Close()
                '
                Dim cnt As Integer
                Dim subsql As String
                For j As Integer = 0 To raceNameList.Count - 1
                    lb_msg.Text = j.ToString & "/" & raceNameList.Count.ToString & " 処理中"
                    Me.Refresh()
                    cnt = 0
                    subsql = ""
                    cmd.CommandText = "SELECT id FROM RaceHeader WHERE race_name=@race_name AND dt=@dt AND jo_code=@jo_code AND type_code=@type_code AND kyori=@kyori"
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@race_name", raceNameList(j))
                    cmd.Parameters.AddWithValue("@dt", dtList(j))
                    cmd.Parameters.AddWithValue("@jo_code", joList(j))
                    cmd.Parameters.AddWithValue("@type_code", typeList(j))
                    cmd.Parameters.AddWithValue("@kyori", kyoriList(j))
                    r = cmd.ExecuteReader
                    While r.Read
                        If cnt > 0 Then
                            subsql &= r.GetInt32(0).ToString & ","
                        End If
                        cnt += 1
                    End While
                    r.Close()

                    cmd.Parameters.Clear()
                    cmd.CommandText = "DELETE FROM RaceHeader WHERE id IN(" & subsql.Substring(0, subsql.Length - 1) & ")"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "DELETE FROM Kekka WHERE race_header_id IN(" & subsql.Substring(0, subsql.Length - 1) & ")"
                    cmd.ExecuteNonQuery()
                Next
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ListBox1.Items.Clear()
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT * FROM ShortRaceNameTable WHERE longname=''"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    ListBox1.Items.Add(r("shortname"))
                End While
                r.Close()
                '
                cmd.CommandText = "DELETE FROM ShortRaceNameTable WHERE longname=''"
                cmd.ExecuteNonQuery()
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ListBox1.Items.Clear()
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT A.id, A.dt, A.race_name, A.tosu, B.cnt FROM RaceHeader A INNER JOIN
                                  (SELECT race_header_id, COUNT(*) AS cnt FROM Kekka Group By race_header_id) AS B
                                   ON A.id=B.race_header_id WHERE A.tosu<>B.cnt"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                Dim ss As String
                While r.Read
                    ss = "ID:" & CInt(r("id")).ToString & " dt=" & CDate(r("dt")).ToString("yyyy/M/d") &
                        "  レース名=" & CStr(r("race_name")) & " 頭数1=" &
                        CInt(r("tosu")).ToString & " 頭数2=" & CInt(r("cnt")).ToString
                    ListBox1.Items.Add(ss)
                End While
                r.Close()
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ListBox1.Items.Clear()
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT A.id, A.dt, A.race_name, A.tosu, B.cnt FROM RaceHeader A INNER JOIN
                                  (SELECT race_header_id, COUNT(*) AS cnt FROM Kekka Group By race_header_id) AS B
                                   ON A.id=B.race_header_id WHERE A.tosu<>B.cnt"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                Dim ss As String = ""
                While r.Read
                    ss &= CInt(r("id")).ToString & ","
                End While
                r.Close()
                ss = ss.Substring(0, ss.Length - 1)
                cmd.Parameters.Clear()
                cmd.CommandText = "DELETE FROM RaceHeader WHERE id IN(" & ss & ")"
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.CommandText = "DELETE FROM Kekka WHERE race_header_id IN(" & ss & ")"
                cmd.ExecuteNonQuery()
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End Using
    End Sub
End Class