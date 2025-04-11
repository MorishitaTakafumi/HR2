Imports System.Data.SQLite
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing

Public Class UmaHistGraphForm
    '出走レース履歴グラフ

    Private Class PointDataList
        Private Class PointData
            Public Property dt As Date
            Public Property w As Double
            Public Property cyakusa As Double
            Public Property agarisa As Double
            Public Property rh As String
        End Class

        Private m_bf As New List(Of PointData)

        Public ReadOnly Property cnt As Integer
            Get
                Return m_bf.Count
            End Get
        End Property

        Public ReadOnly Property dt(ByVal idx As Integer) As Date
            Get
                Return m_bf(idx).dt
            End Get
        End Property

        Public ReadOnly Property w(ByVal idx As Integer) As Double
            Get
                Return m_bf(idx).w
            End Get
        End Property

        Public ReadOnly Property cyakusa(ByVal idx As Integer) As Double
            Get
                Return m_bf(idx).cyakusa
            End Get
        End Property

        Public ReadOnly Property agarisa(ByVal idx As Integer) As Double
            Get
                Return m_bf(idx).agarisa
            End Get
        End Property

        Public ReadOnly Property rh(ByVal idx As Integer) As String
            Get
                Return m_bf(idx).rh
            End Get
        End Property

        Public Sub add(ByVal arg_dt As Date, ByVal arg_w As Double, ByVal arg_rh As String)
            Dim a As New PointData
            a.dt = arg_dt.Date
            a.w = arg_w
            a.cyakusa = 0
            a.agarisa = 0
            a.rh = arg_rh
            m_bf.Add(a)
        End Sub

        Public Sub setTp(ByVal arg_dt As Date, ByVal arg_cyakusa As Double, ByVal arg_agarisa As Double, ByVal grade As String)
            For j As Integer = 0 To m_bf.Count - 1
                If arg_dt.Date = m_bf(j).dt Then
                    m_bf(j).cyakusa = arg_cyakusa
                    m_bf(j).agarisa = arg_agarisa
                    m_bf(j).rh &= " " & grade
                    Return
                End If
            Next
        End Sub

    End Class


    Private oShortRaceName As New ShortRaceNameClass
    Private kekka As New KekkaListClass
    Private umaHists As New umaHistListClass
    Private oUmaHeader As UmaHeaderClass
    Private oPoints As New PointDataList

    Public Sub entry(ByVal bamei As String, ByVal dt_max As Date, ByVal cyakusa_line As Double, ByVal agarisa_line As Double)
        If bamei.Trim.Length = 0 Then
            Return
        End If
        Dim errmsg As String = LoadData(bamei, dt_max)
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            Return
        End If
        lb_bamei.Text = bamei
        ddChart(cyakusa_line, agarisa_line, dt_max)
        Show()
    End Sub

    Private Function LoadData(ByVal bamei As String, ByVal dt_max As Date) As String
        Dim errmsg As String = ""
        Using con As New SQLiteConnection(GetDbConnectionString)
            Dim cmd As SQLiteCommand = con.CreateCommand
            Try
                con.Open()
                errmsg = oShortRaceName.load(cmd)
                If errmsg.Length > 0 Then
                    Exit Try
                End If
                umaHists.init()
                errmsg = umaHists.GetUmaInfo(cmd, "", bamei, dt_max, False)
                If errmsg.Length > 0 Then
                    Exit Try
                End If
                For i As Integer = 0 To umaHists.cnt - 1
                    Dim oS As UmaHistClass = umaHists.GetBodyRef(i)
                    Dim shortname As String = oS.racename
                    oS.racename = oShortRaceName.GetLongName(oS.racename)
                    oS.bamei = bamei
                    If oS.w > 0 AndAlso oS.hutan > 0 AndAlso
                        oS.cyakujun > 0 AndAlso oS.cyakujun <= 18 AndAlso DateDiff(DateInterval.Month, oS.dt, dt_max) <= 54 Then
                        Dim rh As String = oS.syubetu.Substring(0, 1) & (oS.distance \ 100).ToString
                        If oS.cyakujun <= 3 Then
                            rh &= Mid("①②③", oS.cyakujun, 1)
                        End If
                        oPoints.add(oS.dt, oS.hutan + oS.w, rh)
                        kekka.init()
                        Dim oRaceHead As RaceHeaderClass
                        oRaceHead = kekka.raceHeader
                        errmsg = oRaceHead.loadByUmaHist(cmd, oS)
                        If errmsg.Length > 0 Then
                            Exit Try
                        End If
                        If oRaceHead.id > 0 AndAlso kekka.cnt = 0 Then
                            errmsg = kekka.load(cmd, oRaceHead.id)
                            If errmsg.Length > 0 Then
                                Exit Try
                            End If
                        End If
                        If oRaceHead.race_name.Trim.Length > 0 Then
                            kekka.correctCyakusa(oRaceHead)
                            kekka.setAgarisa(oRaceHead)
                            Dim agarisa, cyakusa As Double
                            agarisa = kekka.GetAgarisaCyakusa(bamei, cyakusa)
                            oPoints.setTp(oRaceHead.dt, cyakusa, agarisa, oRaceHead.GetShortClassName)
                        End If
                    End If
                Next
            Catch ex As Exception
                errmsg = ex.Message
            End Try
        End Using
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
        End If

        Return ""
    End Function

    Private Sub ClsAll()
        lb_bamei.Text = ""
        pic.Image = Nothing
    End Sub

    '****************************************************************************************************************************
#Region "グラフ関係"
    'グラフ用紙の位置とサイズ
    Private Const N_TATE As Integer = 50
    Private Const N_YOKO As Integer = 50

    Private GW As Single = 660
    Private GH As Single = 425
    Private GSX As Single = 80
    Private GSY As Single = 45
    Private GEX As Single = GSX + GW
    Private GEY As Single = GSY + GH
    Private YokoKizami As Integer '横軸１目盛間隔の日数
    Private TateKizami As Double  '縦軸１目盛の時計優秀さ
    Private TateKizami2 As Double '縦軸１目盛の重量
    Private mjh, mjw As Single
    Private RightEdgeDate As Date '横軸の右端の日付

    '描画内容を消えないようにする工夫
    Public Function AutoGraphics(ByVal picSource As PictureBox) As Graphics
        If picSource.Image Is Nothing Then
            picSource.Image = New Bitmap(picSource.ClientRectangle.Width, picSource.ClientRectangle.Height)
        End If
        Return Graphics.FromImage(picSource.Image)
    End Function

    'Draw the point
    Private Sub ddTen(ByVal g As Graphics, ByVal cx As Single, ByVal cy As Single)
        Dim sz As Single = 4
        g.FillEllipse(Brushes.Red, cx - sz, cy - sz, 2 * sz + 1, 2 * sz + 1)
    End Sub

    Private Sub ddTen2(ByVal g As Graphics, ByVal cx As Single, ByVal cy As Single)
        Dim sz As Single = 3
        g.FillEllipse(Brushes.Purple, cx - sz, cy - sz, 2 * sz + 1, 2 * sz + 1)
    End Sub

    Private Sub ddTen3(ByVal g As Graphics, ByVal mypen As Pen, ByVal cx As Single, ByVal cy As Single)
        Dim sz As Single = 3
        g.DrawRectangle(mypen, cx - sz, cy - sz, 2 * sz, 2 * sz)
    End Sub

    Private Sub ddTen4(ByVal g As Graphics, ByVal mypen As Pen, ByVal cx As Single, ByVal cy As Single)
        Dim sz As Single = 10
        g.DrawRectangle(mypen, cx - sz, cy - sz, 2 * sz, 2 * sz)
    End Sub

    'グラフ用紙の外枠と縦横軸の刻み線
    Private Sub ddWaku(ByVal g As Graphics)
        g.FillRectangle(Brushes.White, 0, 0, pic.Width, pic.Height) 'これが無いと文字が太くにじむ

        Dim pen As New Drawing.Pen(Brushes.Black, 2)
        g.DrawRectangle(pen, GSX, GSY, GW, GH) '外枠
        pen.Width = 1
        pen.DashStyle = Drawing2D.DashStyle.Solid
        Dim pen2 As New Drawing.Pen(Brushes.Gray, 1)
        pen2.DashStyle = Drawing2D.DashStyle.DashDot
        '縦軸
        Dim sy As Single
        For j As Integer = 0 To N_TATE - 1
            sy = GEY - j * (GH / N_TATE)
            If j > 0 AndAlso (j Mod 5) = 0 Then
                g.DrawLine(pen2, GSX, sy, GEX, sy)
            Else
                g.DrawLine(pen, GSX, sy, GSX + 5, sy)
                g.DrawLine(pen, GEX - 5, sy, GEX, sy)
            End If
        Next
        '横軸
        Dim sx As Single
        For j As Integer = 0 To N_YOKO - 1
            sx = GSX + j * (GW / N_YOKO)
            g.DrawLine(pen, sx, GEY - 5, sx, GEY)
        Next
        pen.Dispose()
    End Sub

    '座標変換(日付)をグラフ上の座標系に変換する
    Private Function cnvAxisX(ByVal dt As Date) As Integer
        Dim sa As Integer = DateDiff(DateInterval.Day, dt, RightEdgeDate)
        Return GEX - (GW / N_YOKO) * (sa / YokoKizami)
    End Function

    '座標変換(時計優秀性)をグラフ上の座標系に変換する
    Private Function cnvAxisY(ByVal timeval As Double) As Integer
        Dim whaba As Double = GetTateUe() - GetTateSita()
        timeval -= GetTateSita()
        Return GEY - (GH * timeval) / whaba
    End Function

    '座標変換(馬体重＋負荷)をグラフ上の座標系に変換する
    Private Function cnvAxisY2(ByVal wval As Double) As Integer
        Dim whaba As Double = GetTateMax2() - GetTateMin2()
        wval -= GetTateMin2()
        Return GEY - (GH * wval) / whaba
    End Function

    'グラフの上端の値(時計優秀性)
    Private Function GetTateUe() As Double
        Return -5
    End Function

    'グラフの下端の値(時計優秀性)
    Private Function GetTateSita() As Double
        Return 5
    End Function

    'グラフの上端の値(馬体重＋負担)
    Private Function GetTateMax2() As Double
        Return 650
    End Function

    'グラフの下端の値(馬体重＋負担)
    Private Function GetTateMin2() As Double
        Return 400
    End Function

    '横軸目盛り描画
    Private Sub ddYokoMemori(ByVal g As Graphics, ByVal mfont As Font, ByVal dt_max As Date)
        YokoKizami = Int(365 * 5 / N_YOKO) 'グラフの目盛り間の時間幅（日)全日数を5年固定とする
        RightEdgeDate = dt_max.AddMonths(1)
        Dim ss As String
        Dim sy As Single = GEY + 0.5 * mjh
        Dim sx As Single
        Dim dt As Date = RightEdgeDate

        For j As Integer = 0 To N_YOKO Step 10
            sx = cnvAxisX(dt)
            ss = dt.ToString("yyyy/M/d")
            g.DrawString(ss, mfont, Brushes.Black, sx - 0.5 * g.MeasureString(ss, mfont).Width, sy)
            dt = RightEdgeDate.AddDays(-YokoKizami * j)
        Next
    End Sub

    '縦軸目盛り
    Private Sub ddTateMemori(ByVal g As Graphics, ByVal mfont As Font)
        TateKizami = (GetTateUe() - GetTateSita()) / N_TATE
        TateKizami2 = (GetTateMax2() - GetTateMin2()) / N_TATE
        Dim ss As String
        Dim sy As Single
        Dim sx As Single = GSX - 4 * mjw
        Dim sx2 As Single = GEX + mjw

        For j As Integer = 0 To N_TATE Step 5
            sy = GEY - j * (GH / N_TATE) - 0.5 * mjh
            ss = (GetTateSita() + j * TateKizami).ToString("F0")
            g.DrawString(ss, mfont, Brushes.Black, sx, sy)

            ss = (GetTateMin2() + j * TateKizami2).ToString("F0")
            g.DrawString(ss, mfont, Brushes.Blue, sx2, sy)
        Next
        ss = "時計優秀さ"
        g.DrawString(ss, mfont, Brushes.Black, GSX - g.MeasureString(ss, mfont).Width + mjw, GSY - 1.5 * mjh)

        ss = "馬体重+負担"
        g.DrawString(ss, mfont, Brushes.Black, GEX - 0.5 * g.MeasureString(ss, mfont).Width + mjw, GSY - 1.5 * mjh)
    End Sub

    'グラフ目盛り描画
    Private Sub ddScale(ByVal g As Graphics, ByVal dt_max As Date)
        Dim mFont As New Font("Meiryo UI", 9)
        mjh = g.MeasureString("8", mFont).Height
        mjw = g.MeasureString("8", mFont).Width
        ddTateMemori(g, mFont)
        ddYokoMemori(g, mFont, dt_max)
        mFont.Dispose()

        '本日の位置に縦線をひく
        Dim pen2 As New Drawing.Pen(Brushes.Red, 1)
        pen2.DashStyle = Drawing2D.DashStyle.DashDot
        Dim sx As Integer = cnvAxisX(dt_max)
        g.DrawLine(pen2, sx, GSY, sx, GEY)
        pen2.Dispose()
    End Sub

    '凡例描画
    Private Sub ddHanrei(ByVal g As Graphics)
        Dim mFont As New Font("Meiryo UI", 9, FontStyle.Regular)

        Dim sx, sy As Integer
        Dim ss As String = "●馬体重＋負担"
        sx = GSX + GW * 1 / 2
        sy = GSY + 5
        g.DrawString(ss, mFont, Brushes.Red, sx, sy)
        '
        ss = "●着差"
        sx = GSX + GW * 1 / 3
        g.DrawString(ss, mFont, Brushes.Purple, sx, sy)

        ss = "□あがり差"
        sx += 50
        g.DrawString(ss, mFont, Brushes.Green, sx, sy)
        mFont.Dispose()
    End Sub

    'Redraw Trend Graph.
    Private Sub ddChart(ByVal cyakusa_line As Double, ByVal agarisa_line As Double, ByVal dt_max As Date)
        If Not pic.Image Is Nothing Then
            pic.Image.Dispose()
            pic.Image = Nothing
        End If

        '描画先のピクチャーボックスに、グラフィックオブジェクトを作成
        Dim lg As Graphics = AutoGraphics(pic)
        lg.TranslateTransform(0, 0)
        ddWaku(lg)
        ddScale(lg, dt_max)
        Dim ox As Integer = DMY_VAL
        Dim oy, oy2, oy3 As Integer
        Dim sx, sy As Integer
        Dim mFont As New Font("Meiryo UI", 8)

        Dim pen As New Drawing.Pen(Brushes.Red, 1)
        Dim pen2 As New Drawing.Pen(Brushes.Purple, 1)
        Dim pen3 As New Drawing.Pen(Brushes.Green, 1)
        Dim pen4 As New Drawing.Pen(Brushes.Blue, 2)

        Dim ck_x, ck_y(1) As Integer
        Dim ck_katamuki(1) As Double
        Dim ruiji_index As Integer = -1
        For j As Integer = 0 To oPoints.cnt - 1
            sx = cnvAxisX(oPoints.dt(j))
            sy = cnvAxisY2(oPoints.w(j))
            Dim msy As Integer = GSY + 25 + 15 * (j Mod 5)
            lg.DrawString(oPoints.rh(j), mFont, Brushes.Black, sx - 10, msy)
            ddTen(lg, sx, sy)
            If ox <> DMY_VAL Then
                lg.DrawLine(pen, ox, oy, sx, sy)
            End If
            oy = sy

            sy = cnvAxisY(oPoints.cyakusa(j))
            ddTen2(lg, sx, sy)
            If ox <> DMY_VAL Then
                lg.DrawLine(pen, ox, oy2, sx, sy)
            End If
            oy2 = sy

            sy = cnvAxisY(oPoints.agarisa(j))
            ddTen3(lg, pen3, sx, sy)
            If ox <> DMY_VAL Then
                lg.DrawLine(pen3, ox, oy3, sx, sy)
            End If
            oy3 = sy
            ox = sx
            '直近走の座標と傾き
            If j = 0 Then
                ck_x = ox
                ck_y(0) = oy2
                ck_y(1) = oy3
            ElseIf j = 1 Then
                ck_katamuki(0) = (ck_y(0) - oy2) / (ck_x - ox)
                ck_katamuki(1) = (ck_y(1) - oy3) / (ck_x - ox)
            End If
            '直近走の類似点
            If j > 0 AndAlso j < oPoints.cnt - 1 Then
                If Math.Abs(ck_y(0) - oy2) <= 10 Then
                    Dim tmp_x As Integer = cnvAxisX(oPoints.dt(j + 1))
                    Dim tmp_y As Integer = cnvAxisY(oPoints.cyakusa(j + 1))
                    Dim tmp_katamuki As Double = (oy2 - tmp_y) / (ox - tmp_x)
                    Dim tmp_v As Double
                    If ck_katamuki(0) = 0 Then
                        tmp_v = tmp_katamuki / 0.001
                    Else
                        tmp_v = tmp_katamuki / ck_katamuki(0)
                    End If
                    If tmp_v >= 0.75 AndAlso tmp_v <= 1.25 Then
                        ddTen4(lg, pen4, ox, oy2)
                        If ruiji_index = -1 Then
                            ruiji_index = j
                        End If
                    End If
                End If
            End If
        Next
        '予想ライン(最小二乗法)
        Dim a, b As Double
        Dim clc_sts As Short
        Dim x As New List(Of Double)
        Dim y As New List(Of Double)
        MakeSaisyo2joList(x, y, dt_max)
        a = saisyo2jo_line(x, y, b, clc_sts)
        If clc_sts = 1 Then
            b = line_slide(a, x(x.Count - 1), y(y.Count - 1)) '直近走の点を通るように平行移動する

            Dim tmp_x(1) As Integer
            Dim tmp_y(1) As Integer
            tmp_x(0) = x(0)
            tmp_x(1) = cnvAxisX(dt_max)
            tmp_y(0) = a * tmp_x(0) + b
            tmp_y(1) = a * tmp_x(1) + b
            Dim pen5 As New Drawing.Pen(Brushes.Gold, 3)
            lg.DrawLine(pen5, tmp_x(0), tmp_y(0), tmp_x(1), tmp_y(1))
            pen5.Dispose()
        End If

        '予想ライン(類似点での次走傾き)
        If ruiji_index > 0 Then
            Dim tmp_x(1) As Integer
            Dim tmp_y(1) As Integer
            tmp_x(0) = cnvAxisX(oPoints.dt(ruiji_index - 1))
            tmp_x(1) = cnvAxisX(oPoints.dt(ruiji_index))
            tmp_y(0) = cnvAxisY(oPoints.cyakusa(ruiji_index - 1))
            tmp_y(1) = cnvAxisY(oPoints.cyakusa(ruiji_index))

            Dim tmp_katamuki As Double = (tmp_y(1) - tmp_y(0)) / (tmp_x(1) - tmp_x(0))
            Dim yoso_x As Integer = cnvAxisX(dt_max)
            Dim yoso_y As Integer = ck_y(0) + tmp_katamuki * (yoso_x - ck_x)
            pen2.Width = 2
            pen2.DashStyle = Drawing2D.DashStyle.DashDot
            lg.DrawLine(pen2, ck_x, ck_y(0), yoso_x, yoso_y)
        End If

        'タイム目標ライン
        pen2.Width = 0.8
        pen2.DashStyle = Drawing2D.DashStyle.DashDot
        sy = cnvAxisY(cyakusa_line)
        lg.DrawLine(pen2, GSX, sy, GEX, sy)
        pen3.Width = 0.5
        pen3.DashStyle = Drawing2D.DashStyle.DashDot

        If Math.Abs(cyakusa_line - agarisa_line) < 0.1 Then
            agarisa_line += 0.05
        End If
        sy = cnvAxisY(agarisa_line)
        lg.DrawLine(pen3, GSX, sy, GEX, sy)
        '
        pen.Dispose()
        pen2.Dispose()
        pen3.Dispose()
        pen4.Dispose()
        mFont.Dispose()
        ddHanrei(lg)
    End Sub

    '最小二乗法の結果(Y=aX+b)を直近の点を通るように平行移動する(Y=ax+b')
    '戻り値：b'
    Private Function line_slide(ByVal a As Double, ByVal last_x As Double, ByVal last_y As Double) As Double
        Dim b2 As Double = last_y - a * last_x    'last_y = a * last_x + b2
        Return b2
    End Function

    '最小二乗法(Y = aX + b)
    '戻り値：a
    Public Function saisyo2jo_line(ByVal x As List(Of Double), ByVal y As List(Of Double), ByRef b As Double, ByRef calc_sts As Short) As Double
        Dim A00, A01, A02, A11, A12 As Double
        A00 = 0
        A01 = 0
        A02 = 0
        A11 = 0
        A12 = 0
        Dim last_idx As Integer = x.Count - 1
        For i As Integer = 0 To last_idx
            A00 += 1.0
            A01 += x(i)
            A02 += y(i)
            A11 += x(i) * x(i)
            A12 += x(i) * y(i)
        Next
        Try
            'Y=aX+bの係数
            '
            b = (A02 * A11 - A01 * A12) / (A00 * A11 - A01 * A01)
            Dim a As Double = (A00 * A12 - A01 * A02) / (A00 * A11 - A01 * A01)
            calc_sts = 1
            Return a
        Catch ex As Exception
            b = 0
            calc_sts = 2
            Return 0
        End Try
    End Function

    '最小二乗法のための測定点リストを作成する
    Private Function MakeSaisyo2joList(ByVal x As List(Of Double), ByVal y As List(Of Double), ByVal dt_max As Date) As Boolean
        x.Clear()
        y.Clear()
        '過去３年以内の全点セット
        Dim sx, sy As Integer
        For j As Integer = 0 To oPoints.cnt - 1
            If DateDiff(DateInterval.Month, oPoints.dt(j), dt_max) <= 36 Then
                sx = cnvAxisX(oPoints.dt(j))
                sy = cnvAxisY(oPoints.cyakusa(j))
                x.Add(sx)
                y.Add(sy)
            Else
                Exit For
            End If
        Next

        ''直近の５点に絞る
        'While x.Count > max_cnt
        '    Dim least_idx As Integer = -1
        '    For j As Integer = 0 To x.Count - 1
        '        If least_idx = -1 OrElse x(j) < x(least_idx) Then
        '            least_idx = j
        '        End If
        '    Next
        '    x.RemoveAt(least_idx)
        '    y.RemoveAt(least_idx)
        'End While

        'xの昇順に並べ替え
        For j As Integer = 0 To x.Count - 1
            Dim min_idx As Integer = j
            For i As Integer = j + 1 To x.Count - 1
                If x(i) < x(min_idx) Then
                    min_idx = i
                End If
            Next
            If min_idx <> j Then
                Dim temp_v As Double = x(j)
                x(j) = x(min_idx)
                x(min_idx) = temp_v
                '
                temp_v = y(j)
                y(j) = y(min_idx)
                y(min_idx) = temp_v
            End If
        Next

        Return True
    End Function

#End Region

End Class