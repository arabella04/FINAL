Imports System.ComponentModel.Design
Imports System.Data.Common
Imports System.Drawing.Text
Imports System.Reflection.Emit
Imports System.Runtime.Remoting.Messaging
Imports MySql.Data.MySqlClient

Public Class Form3
    Dim connectionString As String = "Server=127.0.0.1;Database=Emplorex;Uid=root;Pwd=root;"

    Public Property Form6 As Form6
        Get
            Return Nothing
        End Get
        Set(value As Form6)
        End Set
    End Property

    Private Sub btnloginsubmit_Click(sender As Object, e As EventArgs) Handles btnloginsubmit.Click
        Dim id As String = txtid.Text
        Dim pass As String = txtpass.Text
        Dim jobcode As String = txtjob.Text
        If String.IsNullOrEmpty(id) OrElse String.IsNullOrEmpty(pass) OrElse String.IsNullOrEmpty(jobcode) Then
            MsgBox("Please fill in all fields")
        Else
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim commandText As String = "SELECT COUNT(*) FROM `employees` WHERE `Id` = @id AND `JobCode` = @jobcode AND `password` = @password"
                Using command As New MySqlCommand(commandText, connection)
                    command.Parameters.AddWithValue("@id", id)
                    command.Parameters.AddWithValue("@password", pass)
                    command.Parameters.AddWithValue("@jobcode", jobcode)
                    Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())

                    If result > 0 Then
                        MessageBox.Show("Login successful")
                        Dim loggedInUserID As Integer = Convert.ToInt32(txtid.Text)
                        If Integer.TryParse(txtid.Text, loggedInUserID) Then
                            ' Retrieve department and position from the database
                            Dim department As String = ""
                            Dim position As String = ""
                            Dim query As String = "SELECT Department, Position FROM employees WHERE Id = @id"
                            command.CommandText = query
                            Using reader As MySqlDataReader = command.ExecuteReader()
                                If reader.Read() Then
                                    department = reader.GetString("Department")
                                    position = reader.GetString("Position")
                                End If
                            End Using

                            ' Check if the ID already exists in the salary table
                            Dim selectQuery As String = "SELECT COUNT(*) FROM salary WHERE Id = @id"
                            command.CommandText = selectQuery
                            command.Parameters.Clear()
                            command.Parameters.AddWithValue("@id", loggedInUserID)
                            result = Convert.ToInt32(command.ExecuteScalar())

                            If result > 0 Then
                                ' Update the existing job, position, and salary for the ID
                                Dim updateQuery As String = "UPDATE salary SET Department = @department, Job = @job, SalaryPerMonth = @salarypermonth WHERE Id = @id"
                                Using updateCommand As New MySqlCommand(updateQuery, connection)
                                    updateCommand.Parameters.AddWithValue("@department", department)
                                    updateCommand.Parameters.AddWithValue("@job", position)
                                    updateCommand.Parameters.AddWithValue("@salarypermonth", CalculateSalary(position))
                                    updateCommand.Parameters.AddWithValue("@id", loggedInUserID)
                                    updateCommand.ExecuteNonQuery()
                                End Using
                            Else
                                ' Insert the job, position, and salary for the ID
                                Dim insertQuery As String = "INSERT INTO salary (Id, Department, Job, SalaryPerMonth) VALUES (@id, @department, @job, @salarypermonth)"
                                Using insertCommand As New MySqlCommand(insertQuery, connection)
                                    insertCommand.Parameters.AddWithValue("@id", loggedInUserID)
                                    insertCommand.Parameters.AddWithValue("@department", department)
                                    insertCommand.Parameters.AddWithValue("@job", position)
                                    insertCommand.Parameters.AddWithValue("@salarypermonth", CalculateSalary(position))
                                    insertCommand.ExecuteNonQuery()
                                End Using
                            End If

                            Dim form7 As New Form7(loggedInUserID, CalculateSalary(position))
                            form7.txtdepartment.Text = department
                            form7.txtjob.Text = position

                            form7.Show()
                            Me.Hide()
                        Else
                            MessageBox.Show("Invalid user ID")
                            Return
                        End If
                    Else
                        MessageBox.Show("Invalid login credentials")
                    End If
                End Using
            End Using
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            txtpass.UseSystemPasswordChar = False
        Else
            txtpass.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            txtpass.UseSystemPasswordChar = False
        Else
            txtpass.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Function CalculateSalary(ByVal jobPosition As String) As Decimal
        Select Case jobPosition
            Case "CEO"
                Return 180000
            Case "Director"
                Return 120000
            Case "Manager"
                Return 60000
            Case "Team Lead"
                Return 40000
            Case "Regular"
                Return 25000
            Case "Security"
                Return 15000
            Case "Maintenance"
                Return 10000
            Case Else
                Return 0 ' Return 0 for unknown job positions or when no match is found
        End Select
    End Function

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim firstName As String = InputBox("Enter your first name:")
        Dim id As String = txtid.Text

        If String.IsNullOrEmpty(id) Then
            MessageBox.Show("Please enter your ID first.")
            Return
        End If

        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            Dim commandText As String = "SELECT COUNT(*) FROM `employees` WHERE `Id` = @id AND `FirstName` = @firstName"
            Using command As New MySqlCommand(commandText, connection)
                command.Parameters.AddWithValue("@id", id)
                command.Parameters.AddWithValue("@firstName", firstName)
                Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())

                If result > 0 Then
                    Dim selectQuery As String = "SELECT `Id`, `Password`, `JobCode` FROM `employees` WHERE `Id` = @id"
                    command.CommandText = selectQuery
                    Dim reader As MySqlDataReader = command.ExecuteReader()

                    If reader.Read() Then
                        Dim retrievedId As String = reader.GetString("Id")
                        Dim password As String = reader.GetString("Password")
                        Dim jobCode As String = reader.GetString("JobCode")

                        If retrievedId = id Then
                            MessageBox.Show($"ID: {id}" & Environment.NewLine & $"Password: {password}" & Environment.NewLine & $"Job Code: {jobCode}")
                        Else
                            MessageBox.Show("Contact HR")
                        End If
                    End If

                    reader.Close()
                Else
                    MessageBox.Show("Contact HR")
                End If
            End Using
        End Using
    End Sub

End Class
