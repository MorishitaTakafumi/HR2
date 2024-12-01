Imports System.Data.SQLite

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

    Private Sub SetGradeAndClassname()
        Select Case class_code
            Case 0
                grade = ""
                class_name = "新・未"
            Case 1, 2, 3
                grade = ""
                class_name = class_code.ToString & "勝"
            Case 4
                grade = ""
                class_name = "Op・L"
            Case 5
                grade = "G3"
                class_name = "Open"
            Case 6
                grade = "G2"
                class_name = "Open"
            Case 7
                grade = "G1"
                class_name = "Open"
        End Select
    End Sub

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

    '日付とレース名を指定してロード
    'Return① DBアクセス正常ならば "" を返す
    'Return② DBアクセス失敗ならばエラーメッセージを返す
    Public Function loadByDateAndName(ByVal cmd As SQLiteCommand, ByVal dt_race As Date, ByVal racename As String) As String
        '初期化は呼び出し側責任とする init()
        Try
            cmd.CommandText = "SELECT * FROM RaceHeader WHERE dt=@dt AND race_name=@race_name"
            cmd.Parameters.AddWithValue("@dt", dt_race)
            cmd.Parameters.AddWithValue("@race_name", racename)
            Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
            If r.Read Then
                id = r("id")
                dt = r("dt")
                class_code = r("class_code")
                SetGradeAndClassname()
                type_code = r("type_code")
                syubetu = GetRaceTypeName(type_code)
                kyori = r("kyori")
                jo_code = r("jo_code")
                keibajo = GetKeibajoName(jo_code)
                race_name = r("race_name")
                race_no = r("race_no")
                tosu = r("tosu")
            End If
            r.Close()
            Return ""
        Catch ex As Exception
            Return "raceHeaderClass.loadByDateAndName() " & ex.Message
        End Try
    End Function

    'レース名の照合
    Private Function IsRaceNameMatch(ByVal fullName As String, ByVal shortname As String) As Boolean
        If shortname.Trim.Length = 0 Then
            Return False
        End If
        If fullName = shortname Then
            Return True
        End If
        Dim front3c As String
        If shortname.Length > 3 Then
            front3c = shortname.Substring(0, 3)
        ElseIf shortname.Length = 3 Then
            If shortname.Substring(2, 1).ToUpper = "S" Then
                front3c = shortname.Substring(0, 2) & "ス"
            Else
                front3c = shortname
            End If
        Else
            front3c = shortname
        End If
        If InStr(fullName, front3c) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'UmaHistを指定してロード
    'Return① DBアクセス正常ならば "" を返す
    'Return② DBアクセス失敗ならばエラーメッセージを返す
    Public Function loadByUmaHist(ByVal cmd As SQLiteCommand, ByVal oUmaHist As UmaHistClass) As String
        init()
        Try
            cmd.CommandText = "SELECT * FROM RaceHeader WHERE dt=@dt AND jo_code=@jo_code AND type_code=@type_code AND kyori=@kyori"
            cmd.Parameters.AddWithValue("@dt", oUmaHist.dt)
            cmd.Parameters.AddWithValue("@jo_code", oUmaHist.jo_code)
            cmd.Parameters.AddWithValue("@type_code", oUmaHist.type_code)
            cmd.Parameters.AddWithValue("@kyori", oUmaHist.distance)
            Dim r As SQLite.SQLiteDataReader = cmd.ExecuteReader
            While r.Read
                If IsRaceNameMatch(r("race_name"), oUmaHist.racename) Then
                    id = r("id")
                    dt = r("dt")
                    class_code = r("class_code")
                    SetGradeAndClassname()
                    type_code = r("type_code")
                    syubetu = GetRaceTypeName(type_code)
                    kyori = r("kyori")
                    jo_code = r("jo_code")
                    keibajo = GetKeibajoName(jo_code)
                    race_name = r("race_name")
                    race_no = r("race_no")
                    tosu = r("tosu")
                    Exit While
                End If
            End While
            r.Close()
            Return ""
        Catch ex As Exception
            Return "raceHeaderClass.loadByUmaHist() " & ex.Message
        End Try
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

    '登録
    Public Function save() As String
        Using conn As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLite.SQLiteCommand = conn.CreateCommand
            conn.Open()
            Return addNew(cmd)
        End Using
    End Function
End Class
