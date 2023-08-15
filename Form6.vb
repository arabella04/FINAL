Imports System.Globalization
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Class Form6
    Dim conn As New MySqlConnection("Server=127.0.0.1;Database=Emplorex;Uid=root;Pwd=root;Convert Zero Datetime=true;")
    Dim updatedDepartment As String = ""
    Dim updatedPosition As String = ""

    Private Sub btnselect_Click(sender As Object, e As EventArgs) Handles btnselect.Click
        btnselect.BackColor = Color.Goldenrod
        Button1.BackColor = Color.White
        btndelete.BackColor = Color.White
        btninsert.BackColor = Color.White
        btnsearch.BackColor = Color.White
        pnlpass.Hide()
        pnlupdate.Hide()
        Panel1.Hide()
        Dim query As String = "SELECT * FROM employees"
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim datatable As New DataTable()
            adapter.Fill(datatable)
            DataGridView2.DataSource = datatable
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub cbupdatedposition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbupdatedposition.SelectedIndexChanged
        Dim selectedPosition As String = cbupdatedposition.SelectedItem.ToString()

        Select Case selectedPosition
            Case "CEO"
                txtupdatedjobcode.Text = "543261"
            Case "Director"
                txtupdatedjobcode.Text = "231500"
            Case "Manager"
                txtupdatedjobcode.Text = "622103"
            Case "Team Lead"
                txtupdatedjobcode.Text = "591007"
            Case "Regular"
                txtupdatedjobcode.Text = "456880"
            Case "Security"
                txtupdatedjobcode.Text = "591700"
            Case "Maintenance"
                txtupdatedjobcode.Text = "131967"
            Case Else
                txtupdatedjobcode.Text = ""
        End Select
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        btnselect.BackColor = Color.White
        Button1.BackColor = Color.Goldenrod
        btndelete.BackColor = Color.White
        btninsert.BackColor = Color.White
        btnsearch.BackColor = Color.White
        pnlupdate.Visible = True

        If cbupdateddepartment.SelectedItem = "Executive" Then
            updatedDepartment = "Executive"
            updatedPosition = cbupdatedposition.SelectedItem.ToString()
            txtupdatedjobcode.Text = "543261"
        ElseIf cbupdateddepartment.SelectedItem = "Service" Then
            updatedDepartment = "Service"
            updatedPosition = cbupdatedposition.SelectedItem.ToString()
            Select Case updatedPosition
                Case "Director"
                    txtupdatedjobcode.Text = "231500"
                Case "Manager"
                    txtupdatedjobcode.Text = "622103"
                Case "Team Lead"
                    txtupdatedjobcode.Text = "591007"
                Case "Regular"
                    txtupdatedjobcode.Text = "456880"
            End Select
        ElseIf cbupdateddepartment.SelectedItem = "Operations" Then
            updatedDepartment = "Operations"
            updatedPosition = cbupdatedposition.SelectedItem.ToString()
            Select Case updatedPosition
                Case "Security"
                    txtupdatedjobcode.Text = "591700"
                Case "Maintenance"
                    txtupdatedjobcode.Text = "131967"
            End Select
        End If

        If Not String.IsNullOrEmpty(cbupdateddepartment.Text) AndAlso Not String.IsNullOrEmpty(cbupdatedposition.Text) Then
            pnlupdate.Visible = False
            pnlpass.Visible = True
        Else
            MsgBox("Please fill up all fields")
        End If

        MsgBox("Enter Pass to UPDATE")

    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        btnselect.BackColor = Color.White
        Button1.BackColor = Color.White
        btndelete.BackColor = Color.Goldenrod
        btninsert.BackColor = Color.White
        btnsearch.BackColor = Color.White
        pnlupdate.Hide()
        Panel1.Hide()
        pnlpass.Show()

        Dim selectedRowIndex As Integer = DataGridView2.SelectedCells(0).RowIndex
        Dim row As DataGridViewRow = DataGridView2.Rows(selectedRowIndex)
        Dim employeeId As Integer = Convert.ToInt32(row.Cells("id").Value)

        ' Show the pnlpass panel
        pnlpass.Visible = True
    End Sub

    Private Sub btninsert_Click(sender As Object, e As EventArgs) Handles btninsert.Click
        btnselect.BackColor = Color.White
        Button1.BackColor = Color.White
        btndelete.BackColor = Color.White
        btninsert.BackColor = Color.Goldenrod
        btnsearch.BackColor = Color.White
        TabControl1.SelectedTab = TabPage2
        pnlpass.Hide()
        Panel1.Hide()
        pnlupdate.Hide()
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnselect.PerformClick()
        btnselect.BackColor = Color.White
        LoadSalaryData()
    End Sub

    Private Sub cbupdateddepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbupdateddepartment.SelectedIndexChanged
        If cbupdateddepartment.SelectedItem = "Executive" Then
            cbupdatedposition.Items.Clear()
            cbupdatedposition.Items.Add("CEO")
            cbupdatedposition.SelectedIndex = 0
        ElseIf cbupdateddepartment.SelectedItem = "Service" Then
            cbupdatedposition.Items.Clear()
            cbupdatedposition.Items.Add("Director")
            cbupdatedposition.Items.Add("Manager")
            cbupdatedposition.Items.Add("Team Lead")
            cbupdatedposition.Items.Add("Regular")
            cbupdatedposition.SelectedIndex = 0
        ElseIf cbupdateddepartment.SelectedItem = "Operations" Then
            cbupdatedposition.Items.Clear()
            cbupdatedposition.Items.Add("Security")
            cbupdatedposition.Items.Add("Maintenance")
            cbupdatedposition.SelectedIndex = 0
        Else
            cbupdatedposition.Items.Clear()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        btnselect.BackColor = Color.White
        Button1.BackColor = Color.Goldenrod
        btndelete.BackColor = Color.White
        btninsert.BackColor = Color.White
        btnsearch.BackColor = Color.White
        pnlpass.Hide()
        Panel1.Hide()
        pnlupdate.Show()
    End Sub

    Private Sub btnenterpass_Click(sender As Object, e As EventArgs) Handles btnenterpass.Click
        Dim selectedRowIndex As Integer = DataGridView2.SelectedCells(0).RowIndex
        Dim row As DataGridViewRow = DataGridView2.Rows(selectedRowIndex)
        Dim employeeId As Integer = Convert.ToInt32(row.Cells("id").Value)
        Dim updatedDepartment As String = cbupdateddepartment.Text
        Dim updatedPosition As String = cbupdatedposition.Text
        Dim updatedJobCode As String = txtupdatedjobcode.Text

        Dim password As String = txtpass.Text

        If password = "QWERTYPASS" Then
            If Button1.BackColor = Color.Red Then
                Dim result As DialogResult = MessageBox.Show("Do you want to proceed with the update?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Try
                        conn.Open()
                        Dim query As String = "UPDATE employees SET Department = @department, Position = @position, jobcode = @jobCode " &
                                          "WHERE id = @id"
                        Dim command As New MySqlCommand(query, conn)
                        command.Parameters.AddWithValue("@department", updatedDepartment)
                        command.Parameters.AddWithValue("@position", updatedPosition)
                        command.Parameters.AddWithValue("@jobCode", updatedJobCode)
                        command.Parameters.AddWithValue("@id", employeeId)
                        command.ExecuteNonQuery()

                        Dim queryUpdateOtherTable As String = "UPDATE salary SET Department = @department, job = @job WHERE id = @id"
                        Dim commandUpdateOtherTable As New MySqlCommand(queryUpdateOtherTable, conn)
                        commandUpdateOtherTable.Parameters.AddWithValue("@department", updatedDepartment)
                        commandUpdateOtherTable.Parameters.AddWithValue("@job", updatedPosition)
                        commandUpdateOtherTable.Parameters.AddWithValue("@id", employeeId)
                        commandUpdateOtherTable.ExecuteNonQuery()

                        MessageBox.Show($"Record updated successfully.{vbCrLf}Department: {updatedDepartment}{vbCrLf}Position: {updatedPosition}{vbCrLf}Job Code: {updatedJobCode}")
                        conn.Close()

                        pnlupdate.Visible = False
                        pnlpass.Visible = False
                        btneselect.PerformClick()
                        btnselect.PerformClick()
                        btnselect.BackColor = Color.White
                        Button1.BackColor = Color.White
                        btninsert.BackColor = Color.White
                        btndelete.BackColor = Color.White
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
                        conn.Close()
                    End Try
                Else
                    MsgBox("Update operation canceled.")
                    pnlupdate.Visible = False
                    pnlpass.Visible = False
                    btnselect.PerformClick()
                    btnselect.BackColor = Color.White
                    Button1.BackColor = Color.White
                    btninsert.BackColor = Color.White
                    btndelete.BackColor = Color.White
                End If
            Else
                MsgBox("Invalid operation. Please select the update button.")
                pnlupdate.Visible = False
                pnlpass.Visible = False
                btnselect.PerformClick()
                btnselect.BackColor = Color.White
                Button1.BackColor = Color.White
                btninsert.BackColor = Color.White
                btndelete.BackColor = Color.White
            End If
        ElseIf password = "QWERTYDELETE" Then
            If btndelete.BackColor = Color.Red Then
                Dim result As DialogResult = MessageBox.Show("Do you want to proceed with the delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    Dim query As String = "DELETE FROM employees WHERE id = @id"
                    Try
                        conn.Open()
                        Dim command As New MySqlCommand(query, conn)
                        command.Parameters.AddWithValue("@id", employeeId)
                        command.ExecuteNonQuery()

                        Dim queryDeleteOtherTable As String = "DELETE FROM salary WHERE id = @id"
                        Dim commandDeleteOtherTable As New MySqlCommand(queryDeleteOtherTable, conn)
                        commandDeleteOtherTable.Parameters.AddWithValue("@id", employeeId)
                        commandDeleteOtherTable.ExecuteNonQuery()

                        MessageBox.Show("Record deleted successfully.")
                        conn.Close()
                        pnlupdate.Visible = False
                        txtpass.Text = ""
                        pnlpass.Visible = False
                        btneselect.PerformClick()
                        btnselect.PerformClick()
                        btnselect.BackColor = Color.White
                        Button1.BackColor = Color.White
                        btninsert.BackColor = Color.White
                        btndelete.BackColor = Color.White
                    Catch ex As Exception
                        MsgBox(ex.ToString())
                    Finally
                        conn.Close()
                    End Try
                Else
                    MsgBox("Delete operation canceled.")
                    pnlupdate.Visible = False
                    pnlpass.Visible = False
                    btnselect.PerformClick()
                    btnselect.BackColor = Color.White
                    Button1.BackColor = Color.White
                    btninsert.BackColor = Color.White
                    btndelete.BackColor = Color.White
                End If
            Else
                MsgBox("Invalid operation. Please select the delete button.")
                pnlupdate.Visible = False
                pnlpass.Visible = False
                btnselect.PerformClick()
                btnselect.BackColor = Color.White
                Button1.BackColor = Color.White
                btninsert.BackColor = Color.White
                btndelete.BackColor = Color.White
            End If
        Else
            MsgBox("Invalid Pass")
            pnlupdate.Visible = False
            txtpass.Text = ""
            pnlpass.Visible = False
            btnselect.PerformClick()
            btnselect.BackColor = Color.White
            Button1.BackColor = Color.White
            btninsert.BackColor = Color.White
            btndelete.BackColor = Color.White
        End If
    End Sub

    Public Function CalculateAge(birthdate As String) As Integer
        Dim today As Date = DateTime.Today
        Dim birthDateValue As Date = DateTime.ParseExact(birthdate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
        Dim age As Integer = today.Year - birthDateValue.Year

        ' Check if the birthday has not occurred yet this year
        If today < birthDateValue.AddYears(age) Then
            age -= 1
        End If

        Return age
    End Function

    Private Sub btninsertsubmit_Click(sender As Object, e As EventArgs) Handles btninsertsubmit.Click
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
        pass = txtpass1.Text
        jobcode = ""

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

        If String.IsNullOrEmpty(fname) OrElse String.IsNullOrEmpty(sname) OrElse String.IsNullOrEmpty(midname) OrElse String.IsNullOrEmpty(age) OrElse String.IsNullOrEmpty(birthdate) OrElse String.IsNullOrEmpty(gmail) OrElse String.IsNullOrEmpty(contact) Then
            MsgBox("Please fill in all fields")
        ElseIf Not Regex.IsMatch(txtcontact.Text, "^\d{11}$") Then
            MsgBox("Please enter a 11-digit contact number")
        ElseIf Not Regex.IsMatch(txtpass1.Text, "^(?=.*[A-Za-z])(?=.*\d).{8}$") Then
            MsgBox("Please enter an 8-character password with at least one letter and one digit")
        ElseIf Not Regex.IsMatch(txtgmail.Text, "^\w+([\.-]?\w+)*@gmail\.com$") Then
            MsgBox("Please enter a valid Gmail address")
        ElseIf age <> CalculateAge(birthdate) Then
            MsgBox("The age does not match the provided birthdate")
        Else
            conn.Open()
            Dim query As String = "INSERT INTO `employees`(`FirstName`, `LastName`, `MidName`, `age`, `birthday`, `sex`, `gender`, `contactnumber`, `gmail`, `Department`, `Position`, `ID`, `jobcode`, `password`) VALUES (@firstName, @lastName, @midName, @age, @birthdate, @sex, @gender, @contact, @gmail, @department, @position, @id, @jobcode, @password)"
            Dim command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@firstName", fname)
            command.Parameters.AddWithValue("@lastName", sname)
            command.Parameters.AddWithValue("@midName", midname)
            command.Parameters.AddWithValue("@age", age)
            command.Parameters.AddWithValue("@birthdate", birthdate)
            command.Parameters.AddWithValue("@sex", sex) ' Add the sex value
            command.Parameters.AddWithValue("@gender", gender) ' Add the gender value
            command.Parameters.AddWithValue("@contact", contact)
            command.Parameters.AddWithValue("@gmail", gmail)
            command.Parameters.AddWithValue("@department", department)
            command.Parameters.AddWithValue("@position", position)
            command.Parameters.AddWithValue("@id", "") ' Add the ID value
            command.Parameters.AddWithValue("@jobcode", jobcode)
            command.Parameters.AddWithValue("@password", pass)
            command.ExecuteNonQuery() ' Execute the command
            Dim generatedId As Long = command.LastInsertedId

            ' Show the ID and job code to the user
            MessageBox.Show($"Data saved successfully! Kindly save this for authentication purposes" & vbCrLf & $"ID: {generatedId}" & vbCrLf & $"Job Code: {jobcode} " & vbCrLf & $"Password: {pass}")
            conn.Close()
            txtname.Text = ""
            txtsname.Text = ""
            txtmidname.Text = ""
            txtage.Text = ""
            cbsex.Text = ""
            cbgender.Text = ""
            birthday.Value = ""
            txtgmail.Text = ""
            txtcontact.Text = ""
            cbdepartment.Text = ""
            cbposition.Items.Clear()
            txtpass1.Text = ""
            btnselect.PerformClick()
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
    Public Function GetJobPositionCount(position As String) As Integer
        conn.Open()
        Dim query As String = "SELECT COUNT(*) FROM employees WHERE Position = @position"
        Using command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@position", position)
            Return Convert.ToInt32(command.ExecuteScalar())
        End Using
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        Else
        End If
    End Sub

    Private Sub LoadSalaryData()
        Dim con As New MySqlConnection("Server=127.0.0.1;Database=emplorex;Uid=root;Pwd=root;")
        Dim command As String = "SELECT * FROM salary"
        Try
            con.Open()
            Dim adapter As New MySqlDataAdapter(command, con)
            Dim datatable As New DataTable()
            adapter.Fill(datatable)
            DataGridViewsalary.DataSource = datatable
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        btnsearch.BackColor = Color.Goldenrod
        btnselect.BackColor = Color.White
        Button1.BackColor = Color.White
        btninsert.BackColor = Color.White
        btndelete.BackColor = Color.White
        Panel1.Show()
        Panel1.Visible = True
        pnlpass.Hide()
        pnlupdate.Hide()
    End Sub

    Private Sub btnsearch1_Click(sender As Object, e As EventArgs) Handles btnsearch1.Click
        Dim id As String = txtid.Text
        Dim query As String = "SELECT * FROM employees WHERE id = @id"
        Try
            conn.Open()
            Dim command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@id", id)
            Dim adapter As New MySqlDataAdapter(command)
            Dim datatable As New DataTable()
            adapter.Fill(datatable)
            DataGridView2.DataSource = datatable
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnesearch_Click(sender As Object, e As EventArgs) Handles btnesearch.Click
        btnesearch.BackColor = Color.Goldenrod
        pnlesearch.Visible = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim id As String = txtid.Text
        Dim query As String = "SELECT * FROM salary WHERE id = @id"
        Try
            conn.Open()
            Dim command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@id", id)
            Dim adapter As New MySqlDataAdapter(command)
            Dim datatable As New DataTable()
            adapter.Fill(datatable)
            DataGridViewsalary.DataSource = datatable
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
        conn.Close()
    End Sub


    Private Sub btneselect_Click(sender As Object, e As EventArgs) Handles btneselect.Click
        btnesearch.BackColor = Color.White
        btneselect.BackColor = Color.Goldenrod
        pnlesearch.Visible = False
        Dim id As String = txtid.Text
        Dim query As String = "SELECT * FROM salary"
        Try
            conn.Open()
            Dim command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@id", id)
            Dim adapter As New MySqlDataAdapter(command)
            Dim datatable As New DataTable()
            adapter.Fill(datatable)
            DataGridViewsalary.DataSource = datatable
        Catch ex As Exception
            MsgBox(ex.ToString())
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btneexit_Click(sender As Object, e As EventArgs) Handles btneexit.Click
        Dim result As DialogResult = MessageBox.Show("Do you want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        Else
        End If
    End Sub
End Class
