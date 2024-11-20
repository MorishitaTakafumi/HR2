Imports System.Data.SQLite

Public Class UmaHeaderClass
    '馬ヘッダー

    Public Property rec_id As Integer
    Public Property bamei As String
    Public Property titi As String
    Public Property haha As String
    Public Property sex As String
    Public Property birthday As Date
    Public Property dt_update As Date

    Public Property dirtyFlag As Boolean

    Public Sub New()
        init()
    End Sub

    Public Sub init()
        rec_id = -1
        bamei = ""
        titi = ""
        haha = ""
        sex = ""
        birthday = DMY_DATE
        dt_update = DMY_DATE
        dirtyFlag = False
    End Sub

    '既存の馬か？
    Public Function IsExist(ByVal cmd As SQLiteCommand, ByRef exist_flag As Boolean) As String
        Try
            cmd.CommandText = "SELECT * FROM UmaHeader WHERE bamei=@bamei AND birthday=@birthday"
            cmd.Parameters.AddWithValue("@bamei", bamei)
            cmd.Parameters.AddWithValue("@birthday", birthday)
            Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
            If r.Read Then
                exist_flag = True
            Else
                exist_flag = False
            End If
            r.Close()
            Return ""
        Catch ex As Exception
            Return "UmaHeaderClass.IsExist() " & ex.Message
        End Try
    End Function

    '既存の馬か？
    Public Function IsExist(ByRef exist_flag As Boolean) As String
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            Return IsExist(cmd, exist_flag)
        End Using
    End Function

    'データ取得
    Public Function load(ByVal cmd As SQLiteCommand, ByVal arg_name As String) As String
        init()
        Try
            cmd.CommandText = "SELECT * FROM UmaHeader WHERE name=@name"
            cmd.Parameters.AddWithValue("@name", arg_name)
            Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
            If r.Read Then
                rec_id = r("id")
                bamei = r("name")
                titi = r("father")
                haha = r("mother")
                sex = r("seibetu")
                birthday = r("birthday")
                dt_update = r("dt_update")
            End If
            r.Close()
            Return ""
        Catch ex As Exception
            Return "UmaHeaderClass.load() " & ex.Message
        End Try
    End Function

    '新規登録
    Public Function addNew(ByVal cmd As SQLiteCommand) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO UmaHeader(name, father, mother, seibetu, birthday, dt_update) 
                            VALUES(@name, @father, @mother, @seibetu, @birthday, @dt_update)"
            cmd.Parameters.AddWithValue("@name", bamei)
            cmd.Parameters.AddWithValue("@father", titi)
            cmd.Parameters.AddWithValue("@mother", haha)
            cmd.Parameters.AddWithValue("@seibetu", sex)
            cmd.Parameters.AddWithValue("@birthday", birthday)
            cmd.Parameters.AddWithValue("@dt_update", Now)
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
            Return "UmaHeaderClass.addNew() " & ex.Message
        End Try
    End Function

    '更新
    Public Function update(ByVal cmd As SQLiteCommand) As String
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "UPDATE UmaHeader SET seibetu=@seibetu, dt_update=@dt_update"
            cmd.Parameters.AddWithValue("@seibetu", sex)
            cmd.Parameters.AddWithValue("@dt_update", Now)
            cmd.ExecuteNonQuery()
            Return ""
        Catch ex As Exception
            Return "UmaHeaderClass.update() " & ex.Message
        End Try
    End Function

    '登録
    Public Function save(ByVal uma_hist_list As umaHistListClass) As String
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            Dim errmsg As String = ""
            If rec_id > 0 Then
                errmsg = update(cmd)
            Else
                errmsg = addNew(cmd)
            End If
            If errmsg.Length = 0 Then
                errmsg = uma_hist_list.save(cmd, rec_id)
            End If
            Return errmsg
        End Using
    End Function

End Class
