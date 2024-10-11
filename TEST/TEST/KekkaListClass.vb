Public Class KekkaListClass
    'レース結果リスト・・・ある１つの出走各場のレース結果のリスト

    Private m_bf As New List(Of KekkaClass)
    Public raceHeader As New RaceHeaderClass

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

    '着順を指定してBody参照を取得する
    Public Function GetBodyRefByCyakujun(ByVal cyakujun As Integer) As KekkaClass
        For j As Integer = 0 To cnt - 1
            If m_bf(j).cyakujun = cyakujun Then
                Return m_bf(j)
            End If
        Next
        Return Nothing
    End Function

    '馬名を指定してBody参照を取得する
    '
    Public Function GetBodyRefByBamei(ByVal arg_bamei As String) As KekkaClass
        For j As Integer = 0 To cnt - 1
            If StrComp(arg_bamei, m_bf(j).bamei, CompareMethod.Text) = 0 Then
                Return m_bf(j)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetBodyRef(ByVal idx As Integer) As KekkaClass
        Return m_bf(idx)
    End Function

    '馬名を指定して上り差と着差を文字列で返す
    Public Function GetAgarisa(ByVal arg_bamei As String, ByVal konkaiSyubetu As String) As String
        For j As Integer = 0 To cnt - 1
            If StrComp(arg_bamei, m_bf(j).bamei, CompareMethod.Text) = 0 Then
                If m_bf(j).cyakujun > 0 Then
                    Dim ss As String = m_bf(j).agarisa.ToString("F1") & "(" & m_bf(j).cyakusa.ToString("F1") & ")"
                    If IsSameTypeRace(konkaiSyubetu) Then
                        Return ss
                    Else
                        Return "[" & ss & "]"
                    End If
                End If
            End If
        Next
        Return ""
    End Function

    'レース種別の同異
    '戻り値：True=同種, False=異種
    Public Function IsSameTypeRace(ByVal konkaiSyubetu As String) As Boolean
        Dim cmps As String
        If InStr(konkaiSyubetu, "芝") Then
            cmps = "芝"
        Else
            cmps = "ダート"
        End If
        If InStr(raceHeader.syubetu, cmps) > 0 Then
            Return True
        Else
            Return False
        End If
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
                If InStr(oHead.class_name, "未勝利") > 0 OrElse InStr(oHead.class_name, "新馬") > 0 Then
                    hoseiti = 0.8
                ElseIf InStr(oHead.class_name, "1勝") > 0 OrElse InStr(oHead.class_name, "500万") > 0 Then
                    hoseiti = 0.6
                ElseIf InStr(oHead.class_name, "2勝") > 0 OrElse InStr(oHead.class_name, "1000万") > 0 Then
                    hoseiti = 0.4
                ElseIf InStr(oHead.class_name, "3勝") > 0 OrElse InStr(oHead.class_name, "1600万") > 0 Then
                    hoseiti = 0.2
                End If
        End Select

        '補正計算では何コーナーの通過順位を使うか
        Dim corner_idx As Integer = oHead.GetCornerToCalcAgarisa() - 1

        For j As Integer = 0 To cnt - 1
            If m_bf(j).agari = agariList(0) AndAlso m_bf(j).cyakujun <= 5 Then '5着内で最速の上り馬は5着内で2番目の上りとの差(その馬が5着より下の場合は5着内で最速場と比較する)
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
