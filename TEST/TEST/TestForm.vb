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

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim a As New DebufForm
        a.ShowDialog()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        ListBox1.Items.Clear()
        Dim errmsg As String = ""
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                Dim race_id As Integer = 13859
                Dim empcnt As Integer = 0
                Dim kekkaList As New KekkaListClass
                While empcnt < 3
                    errmsg = kekkaList.loadAll(cmd, race_id)
                    If errmsg.Length > 0 Then
                        Exit Try
                    End If
                    If kekkaList.raceHeader.id < 0 Then
                        empcnt += 1
                    Else
                        empcnt = 0
                        kekkaList.setCyakusa()
                        errmsg = kekkaList.updateCyakusa(cmd)
                    End If
                    race_id += 1
                End While
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        ListBox1.Items.Clear()
        Dim errmsg As String = ""
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "select R.dt, R.race_name, K.cyakujun, k.bamei, k.href
                                    from raceheader R inner join kekka K on R.id=K.race_header_id 
                                    Left JOIN UmaHeader U on K.bamei=U.name
                                    where K.cyakujun>0 AND K.cyakujun<2 AND U.id IS NULL"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                Dim cnt As Integer = 0
                While r.Read
                    cnt += 1
                    ListBox1.Items.Add(cnt.ToString & " : " & CDate(r("dt")).ToString & " " & r("race_name") & " " & r("bamei"))
                End While
                r.Close()
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        ClearWebPageAccessCounter()
        ListBox1.Items.Clear()
        Dim umaHref As New List(Of String)
        Dim errmsg As String = ""
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "select k.href
                                    from raceheader R inner join kekka K on R.id=K.race_header_id 
                                    Left JOIN UmaHeader U on K.bamei=U.name
                                    where K.cyakujun>0 AND K.cyakujun<2 AND U.id IS NULL"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    Dim ss As String = r("href")
                    If Not umaHref.Contains(ss) Then
                        umaHref.Add(ss)
                    End If
                End While
                r.Close()

                Dim umaHistList As New umaHistListClass
                For j As Integer = 0 To umaHref.Count - 1
                    If (j Mod 10) = 0 Then
                        lb_msg.Text = j.ToString & "/" & umaHref.Count.ToString
                        Application.DoEvents()
                        Sleep(1000)
                    End If
                    errmsg = umaHistList.GetUmaInfo(umaHref(j), "", Today, True)
                    If errmsg.Length > 0 Then
                        Exit Try
                    End If
                    If j > 100 Then
                        Exit For
                    End If
                Next
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        End If
        showWebPageAccessCounter()
    End Sub
End Class