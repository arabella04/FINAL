Public Class Form1

    Private Sub btnemployee_Click(sender As Object, e As EventArgs) Handles btnemployee.Click
        Dim form2 As New Form2() ' Initialize a new instance of Form2
        form2.Show()
        Me.Hide()
    End Sub
    Private Sub btnadmin_Click(sender As Object, e As EventArgs) Handles btnadmin.Click
        Form5.Show()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Form8.Show()
        Me.Hide()
    End Sub
End Class
