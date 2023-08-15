Public Class Form5

    Private Sub btnadminsub_Click(sender As Object, e As EventArgs) Handles btnadminsub.Click
        If cbtricky.SelectedItem = "Admin" Then
            If txtadmincode.Text = "QWERTYADMIN" Then
                MsgBox("Welcome!")
                Me.Hide()
                Form6.Show()
            Else
                MsgBox("OH NO, UNAUTHORIZED")
                Form1.Show()
                Me.Close()
            End If
        Else
            MsgBox("OH NO, UNAUTHORIZED")
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlsecret.Visible = False ' Initially hide the panel

        cbtricky.SelectedIndex = -1 ' Clear the selected item in combobox
    End Sub

    Private Sub cbtricky_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbtricky.SelectedIndexChanged
        If cbtricky.SelectedItem = "Admin" Then
            pnlsecret.Visible = True ' Show the panel when "Admin" is selected
        Else
            pnlsecret.Visible = False ' Hide the panel for other selections
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Hide()
    End Sub
End Class
