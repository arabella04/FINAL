Public Class Form2

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub btnsignup_Click(sender As Object, e As EventArgs) Handles btnsignup.Click
        Dim form4 As New Form4()
        form4.Show()
        Me.Hide()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Close()
    End Sub
End Class