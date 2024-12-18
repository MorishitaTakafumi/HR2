Public Class ParamSetClass
    '適合度計算のためのパラメータセット

    '*** spanScore/dateScore/distancrScoreによる係数 ***
    Public scoreP(3) As Double '4着以下,3着,2着,1着の順 GlblMod.GetScoreCoefficient()

    '*** 上り差／着差の適合度を得点化するための係数 ***
    Public timeR1 As Double 'myTimeがcmpTimeより良いとき満点からの減量を決める係数
    Public timeR2 As Double 'myTimeがcmpTimeより悪いとき満点からの減量を決める係数
    Public timeP(3) As Double '何走前かでの重みづけ用 1走前,2走前,3走前,4走前の順 GlblMod.GetDegreeOfFit_time()
    Public timeZoneCoef As Double 'タイムゾーンに応じた係数　GlblMod.GetTimeZoneCoef()

    Private rand As New Random()

    Public Sub New()
        setDefValue()
    End Sub

    Public Sub setDefValue()
        scoreP(0) = 0.003
        scoreP(1) = 0.01
        scoreP(2) = 0.025
        scoreP(3) = 0.05
        timeR1 = 0.05
        timeR2 = 0.15
        timeP(0) = 1
        timeP(1) = 0.95
        timeP(2) = 0.9
        timeP(3) = 0.85
        timeZoneCoef = 0.03
    End Sub

    Public Sub shiftValue()
        'myTimeがcmpTimeより良いとき満点からの減量を決める係数 -0.05～+0.25
        timeR1 = makeRandomValue(-0.05, 0.25)
        'myTimeがcmpTimeより悪いとき満点からの減量を決める係数 0.1～0.4
        timeR2 = makeRandomValue(0.1, 0.4)
        '何走前かでの重みづけ用 1走前,2走前,3走前,4走前の順
        timeP(0) = makeRandomValue(0.85, 1)
        timeP(1) = makeRandomValue(0.85, 1)
        timeP(2) = makeRandomValue(0.85, 1)
        timeP(3) = makeRandomValue(0.85, 1)
        'タイムゾーンに応じた係数 0.01～0.1
        timeZoneCoef = makeRandomValue(0.01, 0.1)
    End Sub

    Private Function makeRandomValue(ByVal minv As Double, ByVal maxv As Double) As Double
        Dim sa As Integer = Int((maxv - minv) * 10000)
        Dim tmpv As Double = 0.0001 * rand.Next(0, sa)
        Return tmpv + minv
    End Function
End Class
