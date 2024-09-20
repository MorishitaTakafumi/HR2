Public Class KekkaListClass

    Private m_bf As New List(Of KekkaClass)

    Public ReadOnly Property cnt As Integer
        Get
            Return m_bf.Count
        End Get
    End Property

    Public Sub init()
        m_bf.Clear()
    End Sub

    Public Sub add1(ByVal o As KekkaClass)
        m_bf.Add(o)
    End Sub

    Public Function GetBodyRef(ByVal idx As Integer) As KekkaClass
        Return m_bf(idx)
    End Function

    Public Function GetAgarisa(ByVal arg_bamei As String) As String
        For j As Integer = 0 To cnt - 1
            If StrComp(arg_bamei, m_bf(j).bamei, CompareMethod.Text) = 0 Then
                Return m_bf(j).agarisa.ToString("F1") & "(" & m_bf(j).cyakusa.ToString("F1") & ")"
            End If
        Next
        Return ""
    End Function

    '着差セット
    Public Sub setCyakusa()
        Dim time1 As Single
        Dim time2 As Single
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun = 1 Then
                time1 = m_bf(j).tokei
            End If
            If m_bf(j).cyakujun = 2 Then
                time2 = m_bf(j).tokei
            End If
        Next
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun = 1 Then
                m_bf(j).cyakusa = time1 - time2
            ElseIf m_bf(j).cyakujun > 0 Then
                m_bf(j).cyakusa = m_bf(j).tokei - time1
            End If
        Next
    End Sub

    '上り差補正値セット
    Public Sub setAgarisa(ByVal oHead As RaceHeaderClass)
        Dim agariList As New List(Of Single)
        '1～5着以内での上り順
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun >= 1 AndAlso m_bf(j).cyakujun <= 5 Then
                agariList.Add(m_bf(j).agari)
            End If
        Next
        agariList.Sort()
        'レースクラスをオープンに標準化するための補正値
        Dim hoseiti As Single = 0
        Select Case oHead.grade
            Case "G3"
                hoseiti = -0.2
            Case "G2"
                hoseiti = -0.4
            Case "G1"
                hoseiti = -0.6
            Case Else
                If InStr(oHead.classname, "未勝利") > 0 OrElse InStr(oHead.classname, "新馬") > 0 Then
                    hoseiti = 0.8
                ElseIf InStr(oHead.classname, "1勝") > 0 OrElse InStr(oHead.classname, "500万") > 0 Then
                    hoseiti = 0.6
                ElseIf InStr(oHead.classname, "2勝") > 0 OrElse InStr(oHead.classname, "1000万") > 0 Then
                    hoseiti = 0.4
                ElseIf InStr(oHead.classname, "3勝") > 0 OrElse InStr(oHead.classname, "1600万") > 0 Then
                    hoseiti = 0.2
                End If
        End Select

        '補正計算では何コーナーの通過順位を使うか
        Dim corner_idx As Integer = oHead.GetCornerToCalcAgarisa() - 1

        For j As Integer = 0 To cnt - 1
            If m_bf(j).agari = agariList(0) Then '最速の上り馬は2番目の上りとの差
                m_bf(j).agarisa = m_bf(j).agari - agariList(1)
            Else
                m_bf(j).agarisa = m_bf(j).agari - agariList(0)
            End If
            'コーナーでの順位を考慮
            If m_bf(j).tukajun(corner_idx) > 0 Then
                m_bf(j).agarisa += (m_bf(j).tukajun(corner_idx) - 1) * 0.1 - 0.4 + hoseiti
            End If
        Next

    End Sub
End Class
