﻿Imports System.Data.SQLite

Public Class RaceHeaderClass
    'レースヘッダー

    Public Property id As Integer
    Public Property dt As Date
    Public Property class_code As Short
    Private m_class_name As String
    Public Property class_name As String
        Get
            Return m_class_name
        End Get
        Set(value As String)
            m_class_name = value
        End Set
    End Property

    Public Property type_code As Short '1=芝,2=ダート,3=障害
    Private m_syubetu As String
    Public Property syubetu As String
        Get
            Return m_syubetu
        End Get
        Set(value As String)
            m_syubetu = value
            type_code = GetTypeCode(value)
        End Set
    End Property
    Public Property kyori As Integer

    Public Property jo_code As Short
    Private m_keibajo As String
    Public Property keibajo As String
        Get
            Return m_keibajo
        End Get
        Set(value As String)
            m_keibajo = value
            jo_code = GetKeibajoCode(value)
        End Set
    End Property

    Public Property race_name As String
    Public Property race_no As Short
    Public Property tosu As Short
    Public Property grade As String

    Public Sub init()
        id = -1
        dt = DMY_DATE
        class_code = -1
        class_name = ""
        type_code = 0
        syubetu = ""
        kyori = 0
        jo_code = -1
        keibajo = ""
        race_name = ""
        race_no = 0
        tosu = 0
        grade = ""
    End Sub

    Public Sub New()
        init()
    End Sub

    'クラス名をクラスコードに変換
    Public Function GetClassCode() As Short
        If InStr(class_name, "1勝") OrElse InStr(class_name, "500万") Then
            Return 1
        End If
        If InStr(class_name, "2勝") OrElse InStr(class_name, "1000万") Then
            Return 2
        End If
        If InStr(class_name, "3勝") OrElse InStr(class_name, "1600万") Then
            Return 3
        End If

        If InStr(class_name, "オープン") Then
            If grade.Trim.Length > 0 Then
                If InStr(grade, "G3") Then
                    Return 5
                End If
                If InStr(grade, "G2") Then
                    Return 6
                End If
                If InStr(grade, "G1") Then
                    Return 7
                End If
            End If
            Return 4
        End If
        Return 0 '新馬・未勝利
    End Function

    '種別名を種別コードに変換
    Private Function GetTypeCode(ByVal typename As String) As Short
        If InStr(typename, "芝") > 0 Then
            If InStr(typename, "ダート") > 0 Then
                Return 3 '障害
            Else
                Return 1
            End If
        Else
            Return 2
        End If
    End Function

    '上り差の補正値を計算するため何コーナーでの通過順位を使うか
    Public Function GetCornerToCalcAgarisa() As Integer

        Select Case keibajo
            Case "札幌"
                Return 3
            Case "函館"
                Return 3
            Case "福島"
                Return 3
            Case "小倉"
                Return 3
            Case "新潟"
                If syubetu = "ダート" Then
                    Return 3
                Else
                    If InStr(syubetu, "外") = 0 Then
                        Return 3
                    Else
                        Return 4
                    End If
                End If
            Case "中山"
                If syubetu = "ダート" Then
                    Return 3
                Else
                    If InStr(syubetu, "外") = 0 Then
                        Return 3
                    Else
                        Return 4
                    End If
                End If
            Case "東京"
                Return 4
            Case "中京"
                Return 4
            Case "京都"
                If syubetu = "ダート" Then
                    Return 3
                Else
                    If InStr(syubetu, "外") = 0 Then
                        Return 3
                    Else
                        Return 4
                    End If
                End If
            Case "阪神"
                If syubetu = "ダート" Then
                    Return 3
                Else
                    If InStr(syubetu, "外") = 0 Then
                        Return 3
                    Else
                        Return 4
                    End If
                End If
            Case Else
                Return 4
        End Select
    End Function

    '既存のレースか？
    Public Function IsExist(ByVal cmd As SQLiteCommand, ByRef exist_flag As Boolean) As String
        Try
            cmd.CommandText = "SELECT * FROM RaceHeader WHERE dt=@dt AND jo_code=@jo_code AND race_no=@race_no"
            cmd.Parameters.AddWithValue("@dt", dt)
            cmd.Parameters.AddWithValue("@jo_code", jo_code)
            cmd.Parameters.AddWithValue("@race_no", race_no)
            Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
            If r.Read Then
                exist_flag = True
            Else
                exist_flag = False
            End If
            r.Close()
            Return ""
        Catch ex As Exception
            Return "raceHeaderClass.IsExist() " & ex.Message
        End Try
    End Function

    '既存のレースか？
    Public Function IsExist(ByRef exist_flag As Boolean) As String
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            Return IsExist(cmd, exist_flag)
        End Using
    End Function

    '新規登録
    Public Function addNew(ByVal cmd As SQLiteCommand) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO RaceHeader(dt, class_code, type_code, kyori, jo_code, race_name, race_no, tosu) 
                            VALUES(@dt, @class_code, @type_code, @kyori, @jo_code, @race_name, @race_no, @tosu)"
            cmd.Parameters.AddWithValue("@dt", dt)
            cmd.Parameters.AddWithValue("@class_code", class_code)
            cmd.Parameters.AddWithValue("@type_code", type_code)
            cmd.Parameters.AddWithValue("@kyori", kyori)
            cmd.Parameters.AddWithValue("@jo_code", jo_code)
            cmd.Parameters.AddWithValue("@race_name", race_name)
            cmd.Parameters.AddWithValue("@race_no", race_no)
            cmd.Parameters.AddWithValue("@tosu", tosu)
            cmd.ExecuteNonQuery()
            '
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT last_insert_rowid() As new_id"
            Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
            If r.Read Then
                id = r("new_id")
            End If
            r.Close()
            Return ""
        Catch ex As Exception
            Return "raceHeaderClass.addNew() " & ex.Message
        End Try
    End Function

    '登録
    Public Function save(ByVal anavalary() As AnaValClass) As String
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            Dim errmsg As String = addNew(cmd)
            If errmsg.Length = 0 Then
                For j As Integer = 0 To anavalary.Length - 1
                    If anavalary(j) IsNot Nothing Then
                        anavalary(j).rhead_id = id
                        errmsg = anavalary(j).addNew(cmd)
                        If errmsg.Length > 0 Then
                            Exit For
                        End If
                    End If
                Next
            End If
            Return errmsg
        End Using
    End Function
End Class
