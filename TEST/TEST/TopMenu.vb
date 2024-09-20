Public Class TopMenu

    Private Sub BtnRaceKekka_Click(sender As Object, e As EventArgs) Handles BtnRaceKekka.Click
        Dim a As New Form1
        Me.Hide()
        a.ShowDialog()
        Me.Show()
    End Sub

    Private Sub BtnHorce_Click(sender As Object, e As EventArgs) Handles BtnHorce.Click
        Dim a As New Form2
        Me.Hide()
        a.ShowDialog()
        Me.Show()
    End Sub

    Private Sub BtnSyutubahyo_Click(sender As Object, e As EventArgs) Handles BtnSyutubahyo.Click
        Dim a As New Form3
        Me.Hide()
        a.ShowDialog()
        Me.Show()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnAnalysis_Click(sender As Object, e As EventArgs) Handles BtnAnalysis.Click
        Dim a As New AnaForm
        Me.Hide()
        a.ShowDialog()
        Me.Show()
    End Sub
End Class