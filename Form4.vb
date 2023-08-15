
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class Form4
    Dim connectionString As String = "Server=127.0.0.1;Database=Emplorex;Uid=root;Pwd=root;"

    Public Property Form7 As Form7
        Get
            Return Nothing
        End Get
        Set(value As Form7)
        End Set
    End Property

    Private Function CalculateAge(birthdate As String) As Integer
        Dim today As Date = DateTime.Today
        Dim birthDateValue As Date = DateTime.ParseExact(birthdate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
        Dim age As Integer = today.Year - birthDateValue.Year

        ' Check if the birthday has not occurred yet this year
        If today < birthDateValue.AddYears(age) Then
            age -= 1
        End If

        Return age
    End Function


    Private Sub btnsignupsubmit_Click(sender As Object, e As EventArgs) Handles btnsignupsubmit.Click
        Dim fname, sname, midname, pass, age, sex, gender, gmail, contact, birthdate, department, position, jobcode As String
        fname = txtname.Text
        sname = txtsname.Text
        midname = txtmidname.Text
        age = txtage.Text
        sex = cbsex.Text
        gender = cbgender.Text
        birthdate = birthday.Value.ToString("yyyy-MM-dd")
        gmail = txtgmail.Text
        contact = txtcontact.Text
        department = cbdepartment.Text
        position = cbposition.Text
        pass = txtpass.Text
        jobcode = ""

        Dim maxPositions As New Dictionary(Of String, Integer) From {
            {"CEO", 1},
            {"Director", 1},
            {"Manager", 3},
            {"Team Lead", 15},
            {"Regular", 75},
            {"Maintenance", 10},
            {"Security", 6}
        }

        ' Check if the job position is full
        If maxPositions.ContainsKey(position) Then
            Dim currentPositionCount As Integer = GetJobPositionCount(position)
            If currentPositionCount >= maxPositions(position) Then
                MessageBox.Show($"The job position '{position}' is currently full.")
                Return ' Stop further execution
            End If
        End If

        Dim ceocode As String = "543261"
        Dim dircode As String = "231500"
        Dim mancode As String = "622103"
        Dim leadcode As String = "591007"
        Dim regcode As String = "456880"
        Dim seccode As String = "591700"
        Dim maincode As String = "131967"

        If cbdepartment.SelectedItem = "Executive" Then
            department = "Executive"
            position = cbposition.SelectedItem.ToString()
            jobcode = ceocode
        ElseIf cbdepartment.SelectedItem = "Service" Then
            department = "Service"
            position = cbposition.SelectedItem.ToString()
            Select Case position
                Case "Director"
                    jobcode = dircode
                Case "Manager"
                    jobcode = mancode
                Case "Team Lead"
                    jobcode = leadcode
                Case "Regular"
                    jobcode = regcode
            End Select
        ElseIf cbdepartment.SelectedItem = "Operations" Then
            department = "Operations"
            position = cbposition.SelectedItem.ToString()
            Select Case position
                Case "Security"
                    jobcode = seccode
                Case "Maintenance"
                    jobcode = maincode
            End Select
        End If

        If String.IsNullOrEmpty(fname) OrElse String.IsNullOrEmpty(sname) OrElse String.IsNullOrEmpty(midname) OrElse String.IsNullOrEmpty(age) OrElse String.IsNullOrEmpty(birthdate) OrElse String.IsNullOrEmpty(gmail) OrElse String.IsNullOrEmpty(contact) Then
            MsgBox("Please fill in all fields")
        ElseIf Not Regex.IsMatch(txtcontact.Text, "^\d{11}$") Then
            MsgBox("Please enter a 11-digit contact number")
        ElseIf Not Regex.IsMatch(txtpass.Text, "^(?=.*[A-Za-z])(?=.*\d).{8}$") Then
            MsgBox("Please enter an 8-character password with at least one letter and one digit")
        ElseIf Not Regex.IsMatch(txtgmail.Text, "^\w+([\.-]?\w+)*@gmail\.com$") Then
            MsgBox("Please enter a valid Gmail address")
        ElseIf age <> CalculateAge(birthdate) Then
            MsgBox("The age does not match the provided birthdate")
        Else
            Using connection As New MySqlConnection(connectionString)
                connection.Open()
                Dim commandText As String = "INSERT INTO `employees`(`FirstName`, `LastName`, `MidName`, `age`, `birthday`, `sex`, `gender`, `contactnumber`, `gmail`, `Department`, `Position`, `ID`, `jobcode`, `password`) VALUES (@firstName, @lastName, @midName, @age, @birthdate, @sex, @gender, @contact, @gmail, @department, @position, @id, @jobcode, @password)"
                Using command As New MySqlCommand(commandText, connection)
                    command.Parameters.AddWithValue("@firstName", fname)
                    command.Parameters.AddWithValue("@lastName", sname)
                    command.Parameters.AddWithValue("@midName", midname)
                    command.Parameters.AddWithValue("@age", age)
                    command.Parameters.AddWithValue("@birthdate", birthdate)
                    command.Parameters.AddWithValue("@sex", sex)
                    command.Parameters.AddWithValue("@gender", gender)
                    command.Parameters.AddWithValue("@contact", contact)
                    command.Parameters.AddWithValue("@gmail", gmail)
                    command.Parameters.AddWithValue("@department", department)
                    command.Parameters.AddWithValue("@position", position)
                    command.Parameters.AddWithValue("@id", "")
                    command.Parameters.AddWithValue("@jobcode", jobcode)
                    command.Parameters.AddWithValue("@password", pass)

                    command.ExecuteNonQuery()
                    Dim generatedId As Long = command.LastInsertedId
                    MessageBox.Show($"Data saved successfully! Kindly save this for authentication purposes" & vbCrLf & $"ID: {generatedId}" & vbCrLf & $"Job Code: {jobcode} " & vbCrLf & $"Password: {pass}")
                    Form2.Show()
                    Me.Hide()
                End Using
            End Using
        End If
    End Sub

    Private Sub cbdepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbdepartment.SelectedIndexChanged
        If cbdepartment.SelectedItem = "Executive" Then
            cbposition.Items.Clear()
            cbposition.Items.Add("CEO")
            cbposition.SelectedIndex = 0
        ElseIf cbdepartment.SelectedItem = "Service" Then
            cbposition.Items.Clear()
            cbposition.Items.Add("Director")
            cbposition.Items.Add("Manager")
            cbposition.Items.Add("Team Lead")
            cbposition.Items.Add("Regular")
            cbposition.SelectedIndex = 0
        ElseIf cbdepartment.SelectedItem = "Operations" Then
            cbposition.Items.Clear()
            cbposition.Items.Add("Security")
            cbposition.Items.Add("Maintenance")
            cbposition.SelectedIndex = 0
        Else
            cbposition.Items.Clear()
        End If
    End Sub

    Private Function GetJobPositionCount(position As String) As Integer
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim commandText As String = "SELECT COUNT(*) FROM employees WHERE Position = @position"
            Using command As New MySqlCommand(commandText, connection)
                command.Parameters.AddWithValue("@position", position)
                Return Convert.ToInt32(command.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Form2.Show()
        Me.Close()
    End Sub

End Class
