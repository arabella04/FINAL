<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form5
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form5))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbtricky = New System.Windows.Forms.ComboBox()
        Me.txtadmincode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlsecret = New System.Windows.Forms.Panel()
        Me.btnadminsub = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pnlsecret.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Candara", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(391, 134)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Who are you?"
        '
        'cbtricky
        '
        Me.cbtricky.FormattingEnabled = True
        Me.cbtricky.Items.AddRange(New Object() {"User", "Admin"})
        Me.cbtricky.Location = New System.Drawing.Point(490, 134)
        Me.cbtricky.Name = "cbtricky"
        Me.cbtricky.Size = New System.Drawing.Size(133, 21)
        Me.cbtricky.TabIndex = 1
        '
        'txtadmincode
        '
        Me.txtadmincode.Location = New System.Drawing.Point(33, 26)
        Me.txtadmincode.Name = "txtadmincode"
        Me.txtadmincode.Size = New System.Drawing.Size(100, 20)
        Me.txtadmincode.TabIndex = 2
        Me.txtadmincode.UseSystemPasswordChar = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Candara", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Goldenrod
        Me.Label2.Location = New System.Drawing.Point(48, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Enter Code"
        '
        'pnlsecret
        '
        Me.pnlsecret.BackColor = System.Drawing.Color.Transparent
        Me.pnlsecret.Controls.Add(Me.Label2)
        Me.pnlsecret.Controls.Add(Me.txtadmincode)
        Me.pnlsecret.Location = New System.Drawing.Point(416, 178)
        Me.pnlsecret.Name = "pnlsecret"
        Me.pnlsecret.Size = New System.Drawing.Size(158, 62)
        Me.pnlsecret.TabIndex = 4
        Me.pnlsecret.Visible = False
        '
        'btnadminsub
        '
        Me.btnadminsub.Font = New System.Drawing.Font("Candara", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadminsub.Location = New System.Drawing.Point(586, 290)
        Me.btnadminsub.Name = "btnadminsub"
        Me.btnadminsub.Size = New System.Drawing.Size(71, 30)
        Me.btnadminsub.TabIndex = 5
        Me.btnadminsub.Text = "Submit"
        Me.btnadminsub.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.FINAL.My.Resources.Resources.photo1687118361__1_
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(45, 48)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(294, 249)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(509, 290)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(71, 30)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "◂"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form5
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.FINAL.My.Resources.Resources.photo1687062966
        Me.ClientSize = New System.Drawing.Size(692, 343)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnadminsub)
        Me.Controls.Add(Me.pnlsecret)
        Me.Controls.Add(Me.cbtricky)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form5"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.pnlsecret.ResumeLayout(False)
        Me.pnlsecret.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cbtricky As ComboBox
    Friend WithEvents txtadmincode As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents pnlsecret As Panel
    Friend WithEvents btnadminsub As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Button1 As Button
End Class
