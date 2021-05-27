<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DomeConfiguration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DomeConfiguration))
        Me.txtIP = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtMac = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDev = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtChann = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtX = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtY = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'txtIP
        '
        Me.txtIP.Enabled = False
        Me.txtIP.Location = New System.Drawing.Point(101, 34)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(138, 20)
        Me.txtIP.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(78, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "IP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Enabled = False
        Me.Label2.Location = New System.Drawing.Point(65, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "MAC"
        '
        'txtMac
        '
        Me.txtMac.Enabled = False
        Me.txtMac.Location = New System.Drawing.Point(101, 13)
        Me.txtMac.Name = "txtMac"
        Me.txtMac.Size = New System.Drawing.Size(138, 20)
        Me.txtMac.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Όνομα Κάμερας"
        '
        'txtDev
        '
        Me.txtDev.Enabled = False
        Me.txtDev.Location = New System.Drawing.Point(101, 54)
        Me.txtDev.Name = "txtDev"
        Me.txtDev.Size = New System.Drawing.Size(138, 20)
        Me.txtDev.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(36, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Υψόμετρο"
        '
        'txtChann
        '
        Me.txtChann.Enabled = False
        Me.txtChann.Location = New System.Drawing.Point(101, 75)
        Me.txtChann.Name = "txtChann"
        Me.txtChann.Size = New System.Drawing.Size(138, 20)
        Me.txtChann.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Γεωγ. Μήκος"
        '
        'txtX
        '
        Me.txtX.Enabled = False
        Me.txtX.Location = New System.Drawing.Point(101, 95)
        Me.txtX.Name = "txtX"
        Me.txtX.Size = New System.Drawing.Size(138, 20)
        Me.txtX.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 118)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Γεωγ. Πλάτος"
        '
        'txtY
        '
        Me.txtY.Enabled = False
        Me.txtY.Location = New System.Drawing.Point(101, 115)
        Me.txtY.Name = "txtY"
        Me.txtY.Size = New System.Drawing.Size(138, 20)
        Me.txtY.TabIndex = 10
        '
        'DomeConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(251, 144)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtY)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtX)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtChann)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDev)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtMac)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtIP)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DomeConfiguration"
        Me.Text = "Στοιχεία Κάμερας"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMac As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDev As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtChann As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtX As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtY As System.Windows.Forms.TextBox
End Class
