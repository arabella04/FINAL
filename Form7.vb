Imports System.Drawing.Printing
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.AxHost
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class Form7
    Dim conn As New MySqlConnection("Server=127.0.0.1;Database=Emplorex;Uid=root;Pwd=root;Convert Zero Datetime=true;")
    Private ReadOnly loggedInUserID As Integer
    Private ReadOnly salaryPerMonth As Decimal
    Private workingDays As Integer

    Public Sub New(ByVal userID As Integer, ByVal salaryPerMonth As Decimal)
        InitializeComponent()
        loggedInUserID = userID
        Me.salaryPerMonth = salaryPerMonth
        txtsalarypermonth.Text = salaryPerMonth.ToString("N2")
    End Sub

    Private Sub btncompute_Click(sender As Object, e As EventArgs) Handles btncompute.Click
        Me.workingDays = workingDays
        If Not Integer.TryParse(txtWorkingDays.Text, workingDays) OrElse workingDays <= 0 OrElse workingDays > 20 Then
            MessageBox.Show("Please enter a valid number of working days (1-20).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Compute the salary and deductions
        Dim salaryPerDay As Decimal = salaryPerMonth / 20
        Dim salary As Decimal = salaryPerDay * workingDays
        Dim deductions As Decimal = deduction(salaryPerDay, workingDays)

        ' Display the computed values
        txttotaldeduction.Text = deductions.ToString("N")
        txtsalarypermonthdeduc.Text = (salary - deductions).ToString("N")
        Dim sss As Decimal = CalculateSSS(salaryPerDay, workingDays)
        Dim pagibig As Decimal = CalculatePagibig(salaryPerDay, workingDays)
        Dim philhealth As Decimal = CalculatePhilhealth(salaryPerDay, workingDays)
        Dim tax As Decimal = CalculateTax(salaryPerDay, workingDays)
        Dim totalDeductions As Decimal = pagibig + philhealth + sss + tax
        Dim totalsalarypermonthdeduc As Decimal = salary - totalDeductions
        Dim salaryperyr As Decimal = salaryPerMonth * 12
        Dim totaldeducperyr As Decimal = totalDeductions * 12

        ' Display deductions and net salary
        txtsss.Text = sss.ToString("N2")
        txtpagibig.Text = pagibig.ToString("N2")
        txtph.Text = philhealth.ToString("N2")
        txttax.Text = tax.ToString("N2")
        txttotaldeduction.Text = totalDeductions.ToString("N2")
        txtsalarypermonthdeduc.Text = totalsalarypermonthdeduc.ToString("N2")
        txtsalaryperday.Text = salaryPerDay.ToString("N2")
        txtannualincome.Text = salaryperyr.ToString("N2")

        Try
            conn.Open()
            Dim query As String = "UPDATE salary SET department = @department, job = @job, salarypermonth = @salarypermonth, salarypermonthdeduc = @salarypermonthdeduc, salaryperyr = @salaryperyr, sss = @sss, pagibig = @pagibig, philhealth = @philhealth, tax = @tax, totaldeduc = @totaldeduc WHERE id = @id"
            Using command As New MySqlCommand(query, conn)
                command.Parameters.AddWithValue("@id", loggedInUserID)
                command.Parameters.AddWithValue("@department", txtdepartment.Text)
                command.Parameters.AddWithValue("@job", txtjob.Text)
                command.Parameters.AddWithValue("@salarypermonth", txtsalarypermonth.Text)
                command.Parameters.AddWithValue("@salarypermonthdeduc", totalsalarypermonthdeduc)
                command.Parameters.AddWithValue("@salaryperyr", salaryperyr)
                command.Parameters.AddWithValue("@sss", sss)
                command.Parameters.AddWithValue("@pagibig", pagibig)
                command.Parameters.AddWithValue("@philhealth", philhealth)
                command.Parameters.AddWithValue("@tax", tax)
                command.Parameters.AddWithValue("@totaldeduc", totalDeductions)
                command.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while updating the salary details: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Function deduction(ByVal salaryPerDay As Decimal, ByVal workingDays As Integer) As Decimal
        Dim deduc As Decimal = salaryPerDay * workingDays - (CalculatePagibig(salaryPerDay, workingDays) + CalculatePhilhealth(salaryPerDay, workingDays) + CalculateSSS(salaryPerDay, workingDays))
        Return deduc
    End Function

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtempID.Text = loggedInUserID.ToString()
        txtsalarypermonth.Text = salaryPerMonth.ToString("N")
        txtid.Text = loggedInUserID.ToString()

        Dim id As String = loggedInUserID.ToString()

        conn.Open()
        Dim query As String = "SELECT * FROM employees WHERE id = @id"
        Using command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@id", id)
            Using reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    txtname.Text = reader.GetString("FirstName")
                    txtsname.Text = reader.GetString("LastName")
                    txtmidname.Text = reader.GetString("MidName")
                    txtage.Text = reader.GetInt32("age").ToString()
                    cbsex.Text = reader.GetString("sex")
                    cbgender.Text = reader.GetString("gender")
                    birthday.Value = reader.GetDateTime("birthday")
                    txtgmail.Text = reader.GetString("gmail")
                    txtcontact.Text = reader.GetString("contactnumber")
                    cbdepartment.Text = reader.GetString("Department")
                    cbposition.Text = reader.GetString("Position")
                    txtpass2.Text = reader.GetString("password")
                End If
            End Using
        End Using
        conn.Close()
    End Sub


    Public Function CalculateSSS(ByVal salaryPerDay As Decimal, ByVal workingDays As Integer) As Decimal
        Dim empsalary1 As Decimal = (salaryPerDay * 20) * 0.045

        If salaryPerMonth <= 35000 Then
            Return empsalary1
        Else
            Return (1575 / 20) * workingDays
        End If
    End Function

    Public Function CalculatePagibig(ByVal salaryPerDay As Decimal, ByVal workingDays As Integer) As Decimal
        Dim empsalary2 As Decimal = (salaryPerDay * 20) * 0.01

        If salaryPerMonth <= 30000 Then
            Return empsalary2
        Else
            Return (300 / 20) * workingDays
        End If
    End Function

    Public Function CalculatePhilhealth(ByVal salaryPerDay As Decimal, ByVal workingDays As Integer) As Decimal
        Dim empsalary3 As Decimal = (salaryPerDay * 20) * 0.015

        If salaryPerMonth <= 80000 Then
            Return empsalary3
        Else
            Return (1200 / 20) * workingDays
        End If
    End Function

    Public Function CalculateTax(ByVal salaryPerDay As Decimal, ByVal workingDays As Integer) As Decimal
        Dim annualIncome As Decimal = salaryPerMonth * 12
        Dim tax As Decimal

        If annualIncome >= 250000 AndAlso annualIncome <= 400000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.15
        ElseIf annualIncome > 400000 AndAlso annualIncome <= 550000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.2
        ElseIf annualIncome > 550000 AndAlso annualIncome <= 700000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.25
        ElseIf annualIncome > 700000 AndAlso annualIncome <= 850000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.3
        ElseIf annualIncome > 850000 AndAlso annualIncome <= 1000000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.35
        ElseIf annualIncome > 1000000 AndAlso annualIncome <= 1150000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.4
        ElseIf annualIncome > 1150000 AndAlso annualIncome <= 1300000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.45
        ElseIf annualIncome > 1300000 AndAlso annualIncome <= 1450000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.5
        ElseIf annualIncome > 1450000 AndAlso annualIncome <= 1600000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.55
        ElseIf annualIncome > 1600000 AndAlso annualIncome <= 1750000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.6
        ElseIf annualIncome > 1750000 AndAlso annualIncome <= 1900000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.65
        ElseIf annualIncome > 1900000 AndAlso annualIncome <= 2050000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.7
        ElseIf annualIncome > 2050000 AndAlso annualIncome <= 2200000 Then
            tax = deduction(salaryPerDay, workingDays) * 0.75
        Else
            tax = 0
        End If

        Return tax
    End Function




    Private Sub btnsalaryreport_Click(sender As Object, e As EventArgs) Handles btnsalaryreport.Click
        btncompute.PerformClick()
        TabControl1.SelectedTab = TabPage2
        Dim output As String = "" & vbCrLf
        output &= "                                EMPLOREX INC." & vbCrLf
        output &= "                     Amityville San Jose Montalban Rizal" & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf
        output &= "       EmployeeID: " & loggedInUserID.ToString() & vbCrLf & "Date: " & DateTime.Now.ToString() & vbCrLf
        output &= "" & vbCrLf

        output &= "--------------------------PAYSLIP-------------------------" & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf
        output &= "|_________________________________________________________|" & vbCrLf
        output &= "                         INVOICE TO                       |" & vbCrLf
        output &= "|_________________________________________________________|" & vbCrLf
        output &= "Name: " & txtname.Text.ToString() & vbCrLf
        output &= "ID: " & loggedInUserID.ToString() & vbCrLf
        output &= "Date: " & DateTime.Now.ToString() & vbCrLf
        output &= "Position: " & txtjob.Text & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf
        output &= "|________________________________________________________ |" & vbCrLf
        output &= "|                     ►PAYCHECK SUMMARY◄                  |" & vbCrLf
        output &= "|________________________________________________________ |" & vbCrLf
        output &= "     Salary: PHP " & Format(salaryPerMonth, "0.00") & " per month" & vbCrLf
        output &= "     Working Days: " & workingDays & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf
        output &= "|________________________________________________________ |" & vbCrLf
        output &= "|                        ►DEDUCTIONS◄                     |" & vbCrLf
        output &= "|________________________________________________________ |" & vbCrLf
        output &= "     SSS: PHP " & Format(Convert.ToDecimal(txtsss.Text), "0.00") & vbCrLf & vbCrLf
        output &= "     Pag-IBIG: PHP " & Format(Convert.ToDecimal(txtpagibig.Text), "0.00") & vbCrLf & vbCrLf
        output &= "     PhilHealth: PHP " & Format(Convert.ToDecimal(txtph.Text), "0.00") & vbCrLf & vbCrLf
        output &= "     Tax: PHP " & Format(Convert.ToDecimal(txttax.Text), "0.00") & vbCrLf & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf
        output &= "|________________________________________________________ |" & vbCrLf
        output &= "|                    ►TOTAL DEDUCTIONS◄       (PHP)       |" & Format(Convert.ToDecimal(txttotaldeduction.Text), "0.00") & vbCrLf
        output &= "|________________________________________________________ |" & vbCrLf
        output &= "     NET SALARY: PHP " & Format(Convert.ToDecimal(txtsalarypermonthdeduc.Text), "0.00") & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf
        output &= "" & vbCrLf

        TextBox1.Text = output

        PrintPreviewDialog1.ShowDialog()
    End Sub


    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage

        Dim customPaperSize As New PaperSize("Custom Paper Size", 594, 840)
        e.PageSettings.PaperSize = customPaperSize

        Dim text As String = TextBox1.Text
        Dim font As New Font("Arial", 12)
        Dim brush As New SolidBrush(Color.Black)
        Dim xPos As Single = 140
        Dim yPos As Single = 140

        e.Graphics.DrawString(text, font, brush, xPos, yPos)

        e.HasMorePages = False
    End Sub

    Private Sub btnlogout_Click(sender As Object, e As EventArgs) Handles btnlogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub btninsertsubmit_Click(sender As Object, e As EventArgs) Handles btnupdateinfo.Click
        Dim id As String = loggedInUserID

        Dim fname, sname, midname, pass, age, sex, gender, gmail, contact, birthdate, department, position As String
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
        pass = txtpass2.Text

        If String.IsNullOrEmpty(fname) OrElse String.IsNullOrEmpty(sname) OrElse String.IsNullOrEmpty(midname) OrElse String.IsNullOrEmpty(age) OrElse String.IsNullOrEmpty(birthdate) OrElse String.IsNullOrEmpty(gmail) OrElse String.IsNullOrEmpty(contact) Then
            MsgBox("Please fill in all fields")
        ElseIf Not Regex.IsMatch(txtcontact.Text, "^\d{11}$") Then
            MsgBox("Please enter an 11-digit contact number")
        ElseIf Not Regex.IsMatch(txtpass2.Text, "^(?=.*[A-Za-z])(?=.*\d).{8}$") Then
            MsgBox("Please enter an 8-character password with at least one letter and one digit")
        ElseIf Not Regex.IsMatch(txtgmail.Text, "^\w+([\.-]?\w+)*@gmail\.com$") Then
            MsgBox("Please enter a valid Gmail address")
        ElseIf age <> Form6.CalculateAge(birthdate) Then
            MsgBox("The age does not match the provided birthdate")
        Else
            conn.Open()
            Dim commandText As String = "UPDATE `employees` SET `FirstName` = @firstName, `LastName` = @lastName, `MidName` = @midName, `age` = @age, `birthday` = @birthdate, `sex` = @sex, `gender` = @gender, `contactnumber` = @contact, `gmail` = @gmail, `Department` = @department, `Position` = @position, `password` = @password WHERE `ID` = @id"
            Using command As New MySqlCommand(commandText, conn)
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
                command.Parameters.AddWithValue("@password", pass)
                command.Parameters.AddWithValue("@id", id)

                command.ExecuteNonQuery()
                MessageBox.Show("Data updated successfully!")
                Form7_Load(Nothing, Nothing)

                Me.Hide()
            End Using
            conn.Close()
        End If
    End Sub
End Class
