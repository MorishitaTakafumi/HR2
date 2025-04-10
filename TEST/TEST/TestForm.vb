﻿Imports System.Data.SQLite

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
                '
                cmd.CommandText = "SELECT A.id, A.dt, A.race_name FROM RaceHeader A LEFT JOIN
                                   Kekka B ON A.id=B.race_header_id WHERE B.id IS NULL"
                r = cmd.ExecuteReader
                While r.Read
                    ss = "ID:" & CInt(r("id")).ToString & " dt=" & CDate(r("dt")).ToString("yyyy/M/d") &
                        "  レース名=" & CStr(r("race_name"))
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
                cmd.CommandText = "SELECT A.id FROM RaceHeader A INNER JOIN
                                  (SELECT race_header_id, COUNT(*) AS cnt FROM Kekka Group By race_header_id) AS B
                                   ON A.id=B.race_header_id WHERE A.tosu<>B.cnt"
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                Dim ss As String = ""
                While r.Read
                    ss &= CInt(r("id")).ToString & ","
                End While
                r.Close()
                '
                cmd.CommandText = "SELECT A.id FROM RaceHeader A LEFT JOIN Kekka B ON A.id=B.race_header_id WHERE B.id IS NULL"
                r = cmd.ExecuteReader
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
                                    where R.dt>@dt AND R.class_code=1 AND K.cyakujun>0 AND U.id IS NULL"
                cmd.Parameters.AddWithValue("@dt", DateSerial(2024, 6, 1))
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                Dim cnt As Integer = 0
                Dim bameis As New List(Of String)
                While r.Read
                    If Not bameis.Contains(r("bamei")) Then
                        cnt += 1
                        ListBox1.Items.Add(cnt.ToString & " : " & CDate(r("dt")).ToString & " " & r("race_name") & " " & r("bamei"))
                        bameis.Add(r("bamei"))
                    End If
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
                                    where R.dt>@dt AND R.class_code=1 AND K.cyakujun>0 AND U.id IS NULL"
                cmd.Parameters.AddWithValue("@dt", DateSerial(2024, 6, 1))
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
                    End If
                    errmsg = umaHistList.GetUmaInfo(umaHref(j), "", Today, True)
                    If errmsg.Length > 0 Then
                        Exit Try
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ss As String = InputBox("調査する着順を入力してください")
        If Not IsNumeric(ss) Then
            Return
        End If
        Dim cyakujun As Integer = CInt(ss)
        ListBox1.Items.Clear()
        ListBox1.Items.Add (cyakujun.ToString & "着馬の分布")
        Dim dtList As New List(Of Date)
        Dim uidList As New List(Of Integer)
        Dim errmsg As String = ""
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                cmd.CommandText = "SELECT R.dt, U.id
                                    FROM (raceheader R INNER JOIN kekka K ON R.id=K.race_header_id) 
                                    INNER JOIN UmaHeader U ON K.bamei=U.name
                                    WHERE R.dt>@dt AND R.type_code<>3 AND R.class_code>=2 AND K.cyakujun=" & cyakujun.ToString
                cmd.Parameters.AddWithValue("@dt", DateSerial(2024, 6, 1))
                Dim r As SQLiteDataReader = cmd.ExecuteReader
                While r.Read
                    dtList.Add(r("dt"))
                    uidList.Add(r("id"))
                End While
                r.Close()

                Dim umaHistList As New umaHistListClass
                Dim rA As New raceAnanClass
                Dim spanCount(70) As Integer
                Dim dateCount(70) As Integer
                For j As Integer = 0 To dtList.Count - 1
                    If (j Mod 10) = 0 Then
                        lb_msg.Text = j.ToString & "/" & dtList.Count.ToString
                        Application.DoEvents()
                        Sleep(1000)
                    End If
                    errmsg = umaHistList.load(cmd, uidList(j), dtList(j))
                    If errmsg.Length > 0 Then
                        Exit Try
                    End If
                    rA.spanScore = umaHistList.GetSpanScore(dtList(j), rA.spanVal)
                    rA.dateScore = umaHistList.GetSameDateSameKyoriScore(dtList(j), 999, "", rA.kyoriScore)
                    If rA.spanScore <= 0 Then
                        spanCount(0) += 1
                    ElseIf rA.spanScore = 1000000 Then
                        spanCount(60) += 1
                    Else
                        spanCount(1 + 10 * Math.Log10(rA.spanScore)) += 1
                    End If
                    If rA.dateScore <= 0 Then
                        dateCount(0) += 1
                    ElseIf rA.dateScore = 1000000 Then
                        dateCount(60) += 1
                    Else
                        dateCount(1 + 10 * Math.Log10(rA.dateScore)) += 1
                    End If
                Next

                For j As Integer = 0 To 70
                    ss = GetBarString(spanCount(j), dtList.Count)
                    ListBox1.Items.Add("s" & j.ToString("D2") & ":" & ss)
                Next
                For j As Integer = 0 To 70
                    ss = GetBarString(dateCount(j), dtList.Count)
                    ListBox1.Items.Add("d" & j.ToString("D2") & ":" & ss)
                Next
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub

    Private Function GetBarString(ByVal cnt As Integer, ByVal ttlcnt As Integer) As String
        Dim c_s As Integer
        If ttlcnt > 10000 Then
            c_s = cnt / 100
        ElseIf ttlcnt > 1000 Then
            c_s = cnt / 10
        ElseIf ttlcnt > 100 Then
            c_s = cnt / 3
        Else
            c_s = cnt
        End If
        Dim ss As String = ""
        For j As Integer = 0 To c_s
            ss &= "*"
        Next
        Return ss & cnt.ToString
    End Function

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        ListBox1.Items.Clear()
        Dim errmsg As String = ""
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                Dim a As New umaHistListClass
                Dim scoreA(3) As Integer
                Dim scoreB(3) As Integer
                Dim scoreC(3) As Integer
                Dim Ecnt(2) As Integer
                Dim Gcnt(2) As Integer
                Dim Icnt(2) As Integer
                For uma_id As Integer = 1 To 9700
                    errmsg = a.load(cmd, uma_id)
                    If errmsg.Length > 0 Then
                        Exit Try
                    End If
                    If a.cnt > 0 Then
                        Dim o As UmaHistClass
                        Dim idx As Integer
                        Dim flgA As Boolean = False
                        Dim flgB As Boolean = False
                        Dim flgC As Boolean = False
                        Dim flgCzan As Integer = 0
                        Dim Acnt As Integer = 0
                        Dim Asub(1) As Integer
                        Dim Bcnt As Integer = 0
                        Dim Bsub(1) As Integer
                        Dim Ccnt As Integer = 0
                        Dim Csub(1) As Integer
                        For j As Integer = a.cnt - 1 To 0 Step -1
                            o = a.GetBodyRef(j)
                            If o.cyakujun > 0 Then
                                If o.cyakujun > 3 Then
                                    idx = 3
                                Else
                                    idx = o.cyakujun - 1
                                End If
                                If flgA Then
                                    scoreA(idx) += 1
                                    If idx = 3 Then
                                        Asub(0) += 1
                                    Else
                                        Asub(1) += 1
                                    End If
                                    flgA = False
                                End If
                                If flgB Then
                                    scoreB(idx) += 1
                                    If idx = 3 Then
                                        Bsub(0) += 1
                                    Else
                                        Bsub(1) += 1
                                    End If
                                    flgB = False
                                End If
                                If flgC Then
                                    If flgCzan = 1 Then
                                        flgCzan = 0
                                    Else
                                        scoreC(idx) += 1
                                        If idx = 3 Then
                                            Csub(0) += 1
                                        Else
                                            Csub(1) += 1
                                        End If
                                        flgC = False
                                    End If
                                End If

                                If o.ninki <> 1 AndAlso o.cyakujun = 1 Then
                                    flgA = True
                                    Acnt += 1
                                End If
                                If o.ninki = 1 AndAlso o.cyakujun > 1 Then
                                    flgB = True
                                    flgC = True
                                    flgCzan = 1
                                    Bcnt += 1
                                    Ccnt += 1
                                End If
                            Else
                                flgA = False
                                flgB = False
                                flgC = False
                                flgCzan = 0
                            End If
                        Next
                        If Acnt >= 2 Then
                            Ecnt(0) += 1
                            If Asub(0) = Acnt Then
                                Ecnt(1) += 1
                            ElseIf Asub(1) = Acnt Then
                                Ecnt(2) += 1
                            End If
                        End If
                        If Bcnt >= 2 Then
                            Gcnt(0) += 1
                            If Bsub(0) = Bcnt Then
                                Gcnt(1) += 1
                            ElseIf Bsub(1) = Bcnt Then
                                Gcnt(2) += 1
                            End If
                        End If
                        If Ccnt >= 2 Then
                            Icnt(0) += 1
                            If Csub(0) = Ccnt Then
                                Icnt(1) += 1
                            ElseIf Csub(1) = Bcnt Then
                                Icnt(2) += 1
                            End If
                        End If
                    End If
                Next
                ListBox1.Items.Add("A:１番人気ではなく１着になった馬の次レースの成績")
                ListBox1.Items.Add(scoreA(0).ToString & "-" & scoreA(1).ToString & "-" & scoreA(2).ToString & "-" & scoreA(3).ToString)
                ListBox1.Items.Add("B:１番人気で２着以下になった馬の次レースの成績")
                ListBox1.Items.Add(scoreB(0).ToString & "-" & scoreB(1).ToString & "-" & scoreB(2).ToString & "-" & scoreB(3).ToString)
                ListBox1.Items.Add("C:１番人気で２着以下になった馬の次次レースの成績")
                ListBox1.Items.Add(scoreC(0).ToString & "-" & scoreC(1).ToString & "-" & scoreC(2).ToString & "-" & scoreC(3).ToString)
                ListBox1.Items.Add("")
                ListBox1.Items.Add("D:Aが2回以上あった馬の数：" & Ecnt(0))
                ListBox1.Items.Add("E:Aが2回以上あった馬でAが常に４着以下だった馬の数：" & Ecnt(1))
                ListBox1.Items.Add("E':Aが2回以上あった馬でAが常に３着以内だった馬の数：" & Ecnt(2))
                ListBox1.Items.Add("")
                ListBox1.Items.Add("F:Bが2回以上あった馬の数：" & Gcnt(0))
                ListBox1.Items.Add("G:Bが2回以上あった馬でBが常に４着以下だった馬の数：" & Gcnt(1))
                ListBox1.Items.Add("G':Bが2回以上あった馬でBが常に３着以内だった馬の数：" & Gcnt(2))
                ListBox1.Items.Add("")
                ListBox1.Items.Add("H:Cが2回以上あった馬の数：" & Icnt(0))
                ListBox1.Items.Add("I:Cが2回以上あった馬でCが常に４着以下だった馬の数：" & Icnt(1))
                ListBox1.Items.Add("I':Cが2回以上あった馬でCが常に３着以内だった馬の数：" & Icnt(2))
                lb_msg.Text = "処理完了！"
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        End If
    End Sub
End Class