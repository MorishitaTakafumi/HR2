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

    Public Sub entry(ByVal bamei As String, ByVal dt_max As Date)
        If bamei.Trim.Length = 0 Then
            Return
        End If
        Dim errmsg As String = LoadData(bamei, dt_max)
        If errmsg.Length > 0 Then
            MsgBox(errmsg, MsgBoxStyle.Critical, Me.Text)
            Return
        End If
        lb_bamei.Text = bamei
        ddChart()
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
                    If oS.w > 0 AndAlso oS.hutan > 0 AndAlso oS.cyakujun > 0 AndAlso oS.cyakujun <= 18 Then
                        oPoints.add(oS.dt, oS.hutan + oS.w, oS.syubetu.Substring(0, 1) & (oS.distance \ 100).ToString)
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
    Private Sub ddYokoMemori(ByVal g As Graphics, ByVal mfont As Font)
        YokoKizami = Int(365 * 5 / N_YOKO) 'グラフの目盛り間の時間幅（日)全日数を5年固定とする
        RightEdgeDate = Today.AddMonths(1)
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
    Private Sub ddScale(ByVal g As Graphics)
        Dim mFont As New Font("Meiryo UI", 9)
        mjh = g.MeasureString("8", mFont).Height
        mjw = g.MeasureString("8", mFont).Width
        ddTateMemori(g, mFont)
        ddYokoMemori(g, mFont)
        mFont.Dispose()

        '本日の位置に縦線をひく
        Dim pen2 As New Drawing.Pen(Brushes.Red, 1)
        pen2.DashStyle = Drawing2D.DashStyle.DashDot
        Dim sx As Integer = cnvAxisX(Today)
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
    Private Sub ddChart()
        If Not pic.Image Is Nothing Then
            pic.Image.Dispose()
            pic.Image = Nothing
        End If

        '描画先のピクチャーボックスに、グラフィックオブジェクトを作成
        Dim lg As Graphics = AutoGraphics(pic)
        lg.TranslateTransform(0, 0)
        ddWaku(lg)
        ddScale(lg)
        Dim ox As Integer = DMY_VAL
        Dim oy, oy2, oy3 As Integer
        Dim sx, sy As Integer
        Dim mFont As New Font("Meiryo UI", 8)

        Dim pen As New Drawing.Pen(Brushes.Red, 1)
        Dim pen2 As New Drawing.Pen(Brushes.Purple, 1)
        Dim pen3 As New Drawing.Pen(Brushes.Green, 1)
        For j As Integer = 0 To oPoints.cnt - 1
            sx = cnvAxisX(oPoints.dt(j))
            sy = cnvAxisY2(oPoints.w(j))
            Dim msy As Integer = GSY + 25 + 15 * (j Mod 3)
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
        Next
        pen.Dispose()
        pen2.Dispose()
        pen3.Dispose()
        mFont.Dispose()
        ddHanrei(lg)
    End Sub
#End Region

End Class