Imports System.Data.SQLite

Public Class RaceHeaderClass
    'レースヘッダー
    Implements ICloneable

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

    'データ一時退避用
    Private org_id As Integer
    Private org_dt As Date
    Private org_class_code As Short
    Private org_class_name As String
    Private org_type_code As Short '1=芝,2=ダート,3=障害
    Private org_syubetu As String
    Private org_kyori As Integer
    Private org_jo_code As Short
    Private org_keibajo As String
    Private org_race_name As String
    Private org_race_no As Short
    Private org_tosu As Short
    Private org_grade As String

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

    Public Function Clone() As Object Implements ICloneable.Clone
        Return New RaceHeaderClass With {
            .id = Me.id,
            .dt = Me.dt,
            .class_code = Me.class_code,
            .class_name = Me.class_name,
            .type_code = Me.type_code,
            .syubetu = Me.syubetu,
            .kyori = Me.kyori,
            .jo_code = Me.jo_code,
            .keibajo = Me.keibajo,
            .race_name = Me.race_name,
            .race_no = Me.race_no,
            .tosu = Me.tosu,
            .grade = Me.grade
        }
    End Function

    Public Sub New()
        init()
    End Sub

    Public Sub push()
        org_id = id
        org_dt = dt
        org_class_code = class_code
        org_class_name = class_name
        org_type_code = type_code
        org_syubetu = syubetu
        org_kyori = kyori
        org_jo_code = jo_code
        org_keibajo = keibajo
        org_race_name = race_name
        org_race_no = race_no
        org_tosu = tosu
        org_grade = grade
    End Sub

    Public Sub pop()
        id = org_id
        dt = org_dt
        class_code = org_class_code
        class_name = org_class_name
        type_code = org_type_code
        syubetu = org_syubetu
        kyori = org_kyori
        jo_code = org_jo_code
        keibajo = org_keibajo
        race_name = org_race_name
        race_no = org_race_no
        tosu = org_tosu
        grade = org_grade
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

    Public Shared Function GetClassName(ByVal arg_class_code As Integer) As String
        Select Case arg_class_code
            Case 0
                Return "新馬・未勝利"
            Case 1, 2, 3
                Return arg_class_code.ToString & "勝"
            Case 4
                Return "Open・L"
            Case 5
                Return "G3"
            Case 6
                Return "G2"
            Case 7
                Return "G1"
        End Select
        Return "?"
    End Function

    Public Function GetShortClassName() As String
        Select Case class_code
            Case 0, 1, 2, 3
                Return class_code.ToString & "w"
            Case 4
                Return "Op"
            Case 5
                Return "G3"
            Case 6
                Return "G2"
            Case 7
                Return "G1"
        End Select
        Return "?"
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

    'レースIDを指定してロード
    'Return DBアクセス失敗ならばエラーメッセージを返す
    Public Function loadById(ByVal cmd As SQLiteCommand, ByVal race_id As Integer) As String
        init()
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT * FROM RaceHeader WHERE id=@id"
            cmd.Parameters.AddWithValue("@id", race_id)
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
            Return "raceHeaderClass.loadById() " & ex.Message
        End Try
    End Function

    '日付とレース名を指定してロード
    'Return① DBアクセス正常ならば "" を返す
    'Return② DBアクセス失敗ならばエラーメッセージを返す
    Public Function loadByDateAndName(ByVal cmd As SQLiteCommand,
                                      ByVal dt_race As Date,
                                      ByVal racename As String,
                                      Optional ByVal arg_jo_code As Integer = -1,
                                      Optional ByVal arg_type_code As Integer = -1,
                                      Optional ByVal arg_kyori As Integer = -1,
                                      Optional ByVal arg_bamei As String = ""
                                      ) As String
        '初期化は呼び出し側責任とする init()
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT R.* FROM RaceHeader R INNER JOIN Kekka K ON R.id=K.race_header_id WHERE R.dt=@dt AND R.race_name=@race_name"
            cmd.Parameters.AddWithValue("@dt", dt_race)
            cmd.Parameters.AddWithValue("@race_name", racename)
            If arg_jo_code >= 0 Then
                cmd.CommandText &= " AND R.jo_code=@jo_code"
                cmd.Parameters.AddWithValue("@jo_code", arg_jo_code)
            End If
            If arg_type_code >= 0 Then
                cmd.CommandText &= " AND R.type_code=@type_code"
                cmd.Parameters.AddWithValue("@type_code", arg_type_code)
            End If
            If arg_kyori >= 0 Then
                cmd.CommandText &= " AND R.kyori=@kyori"
                cmd.Parameters.AddWithValue("@kyori", arg_kyori)
            End If
            If arg_bamei.Length > 0 Then
                cmd.CommandText &= " AND K.bamei=@bamei"
                cmd.Parameters.AddWithValue("@bamei", arg_bamei)
            End If

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

    'UmaHistを指定してロード
    'Return① DBアクセス正常ならば "" を返す
    'Return② DBアクセス失敗ならばエラーメッセージを返す
    Public Function loadByUmaHist(ByVal cmd As SQLiteCommand, ByVal oUmaHist As UmaHistClass) As String
        init()
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT R.* FROM RaceHeader R INNER JOIN Kekka K ON R.id=K.race_header_id 
                               WHERE R.dt=@dt AND R.jo_code=@jo_code AND R.type_code=@type_code AND R.kyori=@kyori
                               AND K.bamei=@bamei"
            cmd.Parameters.AddWithValue("@dt", oUmaHist.dt)
            cmd.Parameters.AddWithValue("@jo_code", oUmaHist.jo_code)
            cmd.Parameters.AddWithValue("@type_code", oUmaHist.type_code)
            cmd.Parameters.AddWithValue("@kyori", oUmaHist.distance)
            cmd.Parameters.AddWithValue("@bamei", oUmaHist.bamei)
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

End Class
