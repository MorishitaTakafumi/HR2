Public Class TopMenu

    Public Sub New()
        InitializeComponent()
        Me.Text &= Application.ProductVersion
    End Sub

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
        a.Show()
    End Sub

    Private Sub BtnStoreAnaVal_Click(sender As Object, e As EventArgs) Handles BtnStoreAnaVal.Click
        Dim a As New StoreAnaValForm
        Me.Hide()
        a.ShowDialog()
        Me.Show()
    End Sub

    Private Sub BtnReview_Click(sender As Object, e As EventArgs) Handles BtnReview.Click
        Dim a As New raceReviewForm
        a.Show()
    End Sub
End Class