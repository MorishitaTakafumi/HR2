Imports System.Data.SQLite

Public Class ShortRaceNameClass
    '短縮レース名と正式名の対照表

    Private dicShortName As New Dictionary(Of String, String)

    Public Function GetLongName(ByVal shortname As String) As String
        If dicShortName.ContainsKey(shortname) Then
            Return dicShortName(shortname)
        Else
            Return shortname
        End If
    End Function

    Public Function load(ByVal cmd As SQLiteCommand) As String
        dicShortName.Clear()
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT * FROM ShortRaceNameTable"
            Dim r As SQLiteDataReader = cmd.ExecuteReader
            While r.Read
                If Not dicShortName.ContainsKey(r("shortname")) Then
                    dicShortName.Add(r("shortname"), r("longname"))
                End If
            End While
            r.Close()
            Return ""
        Catch ex As Exception
            Return "ShortRaceNameClass.load() " & ex.Message
        End Try
    End Function

    Public Function addNew(ByVal cmd As SQLiteCommand, ByVal shortname As String, ByVal longname As String) As String
        If dicShortName.ContainsKey(shortname) Then
            Return ""
        End If
        Try
            cmd.Parameters.Clear()
            cmd.CommandText = "INSERT INTO ShortRaceNameTable(shortname, longname) VALUES(@shortname, @longname)"
            cmd.Parameters.AddWithValue("@shortname", shortname)
            cmd.Parameters.AddWithValue("@longname", longname)
            cmd.ExecuteNonQuery()
            dicShortName.Add(shortname, longname)
            Return ""
        Catch ex As Exception
            Return "ShortRaceNameClass.addNew() " & ex.Message
        End Try
    End Function

End Class
