<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.SCMain = New System.Windows.Forms.SplitContainer
        Me.tvDevices = New System.Windows.Forms.TreeView
        Me.TVDevicesMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TVConnect = New System.Windows.Forms.ToolStripMenuItem
        Me.TVDisConnect = New System.Windows.Forms.ToolStripMenuItem
        Me.PreposReadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectOtherLicFile = New System.Windows.Forms.ToolStripMenuItem
        Me.selectOtherXmlFile = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectNewXmlFile = New System.Windows.Forms.ToolStripMenuItem
        Me.ΚαθάρισμαΚάμεραςToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearAxCameo1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearAxCameo2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ActionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ChangeConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ΥπολογισμόςΘέσηςToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ErrorLevel = New System.Windows.Forms.ToolStripMenuItem
        Me.AllErrors = New System.Windows.Forms.ToolStripMenuItem
        Me.ErrorsAndInfo = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlyErrors = New System.Windows.Forms.ToolStripMenuItem
        Me.NoErrorLogging = New System.Windows.Forms.ToolStripMenuItem
        Me.OnlineLogΕφαρμογήςToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.showOnlineLogs = New System.Windows.Forms.ToolStripMenuItem
        Me.hideOnlineLogs = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CalibrateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CalibrateDisabledToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CalibrateEnabledToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ΒοήθειαToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.txtInfo = New System.Windows.Forms.TextBox
        Me.cmdCalculatePosition = New System.Windows.Forms.Button
        Me.cmdMaxCameo1 = New System.Windows.Forms.Button
        Me.cmdDualView = New System.Windows.Forms.Button
        Me.AxCameo1 = New Bosch.VideoSDK.AxCameoLib.AxCameo
        Me.cmdMaxCameo2 = New System.Windows.Forms.Button
        Me.AxCameo2 = New Bosch.VideoSDK.AxCameoLib.AxCameo
        Me.CameoMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DisconnectDataStream = New System.Windows.Forms.ToolStripMenuItem
        Me.tmrReconnectDevices = New System.Windows.Forms.Timer(Me.components)
        Me.tmrWaitForPanAnswer = New System.Windows.Forms.Timer(Me.components)
        Me.SCMain.Panel1.SuspendLayout()
        Me.SCMain.Panel2.SuspendLayout()
        Me.SCMain.SuspendLayout()
        Me.TVDevicesMenuStrip.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.AxCameo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxCameo2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CameoMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(1118, 734)
        Me.TreeView1.TabIndex = 0
        '
        'SCMain
        '
        Me.SCMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCMain.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.SCMain.IsSplitterFixed = True
        Me.SCMain.Location = New System.Drawing.Point(0, 0)
        Me.SCMain.Name = "SCMain"
        '
        'SCMain.Panel1
        '
        Me.SCMain.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.SCMain.Panel1.Controls.Add(Me.tvDevices)
        Me.SCMain.Panel1.Controls.Add(Me.MenuStrip1)
        '
        'SCMain.Panel2
        '
        Me.SCMain.Panel2.AllowDrop = True
        Me.SCMain.Panel2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.SCMain.Panel2.Controls.Add(Me.txtInfo)
        Me.SCMain.Panel2.Controls.Add(Me.cmdCalculatePosition)
        Me.SCMain.Panel2.Controls.Add(Me.cmdMaxCameo1)
        Me.SCMain.Panel2.Controls.Add(Me.cmdDualView)
        Me.SCMain.Panel2.Controls.Add(Me.AxCameo1)
        Me.SCMain.Panel2.Controls.Add(Me.cmdMaxCameo2)
        Me.SCMain.Panel2.Controls.Add(Me.AxCameo2)
        Me.SCMain.Size = New System.Drawing.Size(1118, 734)
        Me.SCMain.SplitterDistance = 187
        Me.SCMain.TabIndex = 1
        '
        'tvDevices
        '
        Me.tvDevices.AllowDrop = True
        Me.tvDevices.ContextMenuStrip = Me.TVDevicesMenuStrip
        Me.tvDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvDevices.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.tvDevices.Location = New System.Drawing.Point(0, 24)
        Me.tvDevices.Name = "tvDevices"
        Me.tvDevices.Size = New System.Drawing.Size(187, 710)
        Me.tvDevices.TabIndex = 2
        '
        'TVDevicesMenuStrip
        '
        Me.TVDevicesMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TVConnect, Me.TVDisConnect, Me.PreposReadingToolStripMenuItem})
        Me.TVDevicesMenuStrip.Name = "TVDevicesMenuStriop"
        Me.TVDevicesMenuStrip.Size = New System.Drawing.Size(161, 70)
        '
        'TVConnect
        '
        Me.TVConnect.Name = "TVConnect"
        Me.TVConnect.Size = New System.Drawing.Size(160, 22)
        Me.TVConnect.Text = "Connect"
        '
        'TVDisConnect
        '
        Me.TVDisConnect.Name = "TVDisConnect"
        Me.TVDisConnect.Size = New System.Drawing.Size(160, 22)
        Me.TVDisConnect.Text = "Disconnect"
        '
        'PreposReadingToolStripMenuItem
        '
        Me.PreposReadingToolStripMenuItem.Name = "PreposReadingToolStripMenuItem"
        Me.PreposReadingToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.PreposReadingToolStripMenuItem.Text = "Prepos Reading"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ActionsToolStripMenuItem, Me.HelpToolStripMenuItem, Me.ToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(187, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectOtherLicFile, Me.selectOtherXmlFile, Me.SelectNewXmlFile, Me.ΚαθάρισμαΚάμεραςToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.FileToolStripMenuItem.Text = "Αρχείο"
        '
        'SelectOtherLicFile
        '
        Me.SelectOtherLicFile.Name = "SelectOtherLicFile"
        Me.SelectOtherLicFile.Size = New System.Drawing.Size(324, 22)
        Me.SelectOtherLicFile.Text = "Επιλογή νέου αρχείου αδειών"
        '
        'selectOtherXmlFile
        '
        Me.selectOtherXmlFile.Name = "selectOtherXmlFile"
        Me.selectOtherXmlFile.Size = New System.Drawing.Size(324, 22)
        Me.selectOtherXmlFile.Text = "Επιλογή Νέου αρχείου στοιχείων καμερών"
        '
        'SelectNewXmlFile
        '
        Me.SelectNewXmlFile.Name = "SelectNewXmlFile"
        Me.SelectNewXmlFile.Size = New System.Drawing.Size(324, 22)
        Me.SelectNewXmlFile.Text = "Δημιουργία νέου αρχείου στοιχείων καμερών"
        '
        'ΚαθάρισμαΚάμεραςToolStripMenuItem
        '
        Me.ΚαθάρισμαΚάμεραςToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClearAxCameo1, Me.ClearAxCameo2})
        Me.ΚαθάρισμαΚάμεραςToolStripMenuItem.Name = "ΚαθάρισμαΚάμεραςToolStripMenuItem"
        Me.ΚαθάρισμαΚάμεραςToolStripMenuItem.Size = New System.Drawing.Size(324, 22)
        Me.ΚαθάρισμαΚάμεραςToolStripMenuItem.Text = "Καθάρισμα Κάμερας"
        '
        'ClearAxCameo1
        '
        Me.ClearAxCameo1.Name = "ClearAxCameo1"
        Me.ClearAxCameo1.Size = New System.Drawing.Size(172, 22)
        Me.ClearAxCameo1.Text = "Κάμερα Αριστερή"
        '
        'ClearAxCameo2
        '
        Me.ClearAxCameo2.Name = "ClearAxCameo2"
        Me.ClearAxCameo2.Size = New System.Drawing.Size(172, 22)
        Me.ClearAxCameo2.Text = "Κάμερα Δεξιά"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(324, 22)
        Me.ExitToolStripMenuItem.Text = "Έξοδος"
        '
        'ActionsToolStripMenuItem
        '
        Me.ActionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem, Me.ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem, Me.ChangeConfigurationToolStripMenuItem, Me.ΥπολογισμόςΘέσηςToolStripMenuItem, Me.ErrorLevel, Me.OnlineLogΕφαρμογήςToolStripMenuItem, Me.CalibrateToolStripMenuItem})
        Me.ActionsToolStripMenuItem.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem"
        Me.ActionsToolStripMenuItem.Size = New System.Drawing.Size(64, 20)
        Me.ActionsToolStripMenuItem.Text = "Διάφορα"
        '
        'ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem
        '
        Me.ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem.Name = "ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem"
        Me.ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem.Size = New System.Drawing.Size(256, 22)
        Me.ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem.Text = "Εκκίνηση Συνδέσεων με Κάμερες"
        '
        'ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem
        '
        Me.ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem.Name = "ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem"
        Me.ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem.Size = New System.Drawing.Size(256, 22)
        Me.ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem.Text = "Διακοπή Συνδέσεων με Κάμερες"
        '
        'ChangeConfigurationToolStripMenuItem
        '
        Me.ChangeConfigurationToolStripMenuItem.Name = "ChangeConfigurationToolStripMenuItem"
        Me.ChangeConfigurationToolStripMenuItem.Size = New System.Drawing.Size(256, 22)
        Me.ChangeConfigurationToolStripMenuItem.Text = "Αλλαγή Στοιχείων καμερών"
        '
        'ΥπολογισμόςΘέσηςToolStripMenuItem
        '
        Me.ΥπολογισμόςΘέσηςToolStripMenuItem.Name = "ΥπολογισμόςΘέσηςToolStripMenuItem"
        Me.ΥπολογισμόςΘέσηςToolStripMenuItem.Size = New System.Drawing.Size(256, 22)
        Me.ΥπολογισμόςΘέσηςToolStripMenuItem.Text = "Υπολογισμός Θέσης"
        '
        'ErrorLevel
        '
        Me.ErrorLevel.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllErrors, Me.ErrorsAndInfo, Me.OnlyErrors, Me.NoErrorLogging})
        Me.ErrorLevel.Name = "ErrorLevel"
        Me.ErrorLevel.Size = New System.Drawing.Size(256, 22)
        Me.ErrorLevel.Text = "Επίπεδο Καταγραφής/Logs"
        '
        'AllErrors
        '
        Me.AllErrors.Checked = True
        Me.AllErrors.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AllErrors.Name = "AllErrors"
        Me.AllErrors.Size = New System.Drawing.Size(262, 22)
        Me.AllErrors.Text = "Όλα τα συμβάντα της Εφαρμογής"
        '
        'ErrorsAndInfo
        '
        Me.ErrorsAndInfo.Name = "ErrorsAndInfo"
        Me.ErrorsAndInfo.Size = New System.Drawing.Size(262, 22)
        Me.ErrorsAndInfo.Text = "Σφάλματα και Πληροφορίες"
        '
        'OnlyErrors
        '
        Me.OnlyErrors.Name = "OnlyErrors"
        Me.OnlyErrors.Size = New System.Drawing.Size(262, 22)
        Me.OnlyErrors.Text = "Μόνο Σφάλματα"
        '
        'NoErrorLogging
        '
        Me.NoErrorLogging.Name = "NoErrorLogging"
        Me.NoErrorLogging.Size = New System.Drawing.Size(262, 22)
        Me.NoErrorLogging.Text = "Καθόλου συμβάντα"
        '
        'OnlineLogΕφαρμογήςToolStripMenuItem
        '
        Me.OnlineLogΕφαρμογήςToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.showOnlineLogs, Me.hideOnlineLogs, Me.ClearLogToolStripMenuItem})
        Me.OnlineLogΕφαρμογήςToolStripMenuItem.Name = "OnlineLogΕφαρμογήςToolStripMenuItem"
        Me.OnlineLogΕφαρμογήςToolStripMenuItem.Size = New System.Drawing.Size(256, 22)
        Me.OnlineLogΕφαρμογήςToolStripMenuItem.Text = "Online Log εφαρμογής"
        '
        'showOnlineLogs
        '
        Me.showOnlineLogs.Checked = True
        Me.showOnlineLogs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.showOnlineLogs.Name = "showOnlineLogs"
        Me.showOnlineLogs.Size = New System.Drawing.Size(139, 22)
        Me.showOnlineLogs.Text = "Εμφάνιση"
        '
        'hideOnlineLogs
        '
        Me.hideOnlineLogs.Name = "hideOnlineLogs"
        Me.hideOnlineLogs.Size = New System.Drawing.Size(139, 22)
        Me.hideOnlineLogs.Text = "Απόκρυψη"
        '
        'ClearLogToolStripMenuItem
        '
        Me.ClearLogToolStripMenuItem.Name = "ClearLogToolStripMenuItem"
        Me.ClearLogToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.ClearLogToolStripMenuItem.Text = "Καθάρισμα"
        '
        'CalibrateToolStripMenuItem
        '
        Me.CalibrateToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CalibrateDisabledToolStripMenuItem, Me.CalibrateEnabledToolStripMenuItem})
        Me.CalibrateToolStripMenuItem.Enabled = False
        Me.CalibrateToolStripMenuItem.Name = "CalibrateToolStripMenuItem"
        Me.CalibrateToolStripMenuItem.Size = New System.Drawing.Size(256, 22)
        Me.CalibrateToolStripMenuItem.Text = "Διαμόρφωση Καμερών"
        '
        'CalibrateDisabledToolStripMenuItem
        '
        Me.CalibrateDisabledToolStripMenuItem.Checked = True
        Me.CalibrateDisabledToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CalibrateDisabledToolStripMenuItem.Enabled = False
        Me.CalibrateDisabledToolStripMenuItem.Name = "CalibrateDisabledToolStripMenuItem"
        Me.CalibrateDisabledToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CalibrateDisabledToolStripMenuItem.Text = "Απενεργοποιημένη"
        '
        'CalibrateEnabledToolStripMenuItem
        '
        Me.CalibrateEnabledToolStripMenuItem.Enabled = False
        Me.CalibrateEnabledToolStripMenuItem.Name = "CalibrateEnabledToolStripMenuItem"
        Me.CalibrateEnabledToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CalibrateEnabledToolStripMenuItem.Text = "Ενεργοποιημένη"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ΒοήθειαToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.HelpToolStripMenuItem.Text = "Βοήθεια"
        '
        'ΒοήθειαToolStripMenuItem
        '
        Me.ΒοήθειαToolStripMenuItem.Name = "ΒοήθειαToolStripMenuItem"
        Me.ΒοήθειαToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.ΒοήθειαToolStripMenuItem.Text = "Βοήθεια"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.AboutToolStripMenuItem.Text = "Περί"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 20)
        '
        'txtInfo
        '
        Me.txtInfo.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.txtInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtInfo.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.txtInfo.Location = New System.Drawing.Point(0, 604)
        Me.txtInfo.Multiline = True
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ReadOnly = True
        Me.txtInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo.Size = New System.Drawing.Size(927, 130)
        Me.txtInfo.TabIndex = 0
        '
        'cmdCalculatePosition
        '
        Me.cmdCalculatePosition.Location = New System.Drawing.Point(728, 4)
        Me.cmdCalculatePosition.Name = "cmdCalculatePosition"
        Me.cmdCalculatePosition.Size = New System.Drawing.Size(166, 23)
        Me.cmdCalculatePosition.TabIndex = 1
        Me.cmdCalculatePosition.Text = "Υπολογισμός Τοποθεσίας"
        Me.cmdCalculatePosition.UseVisualStyleBackColor = True
        '
        'cmdMaxCameo1
        '
        Me.cmdMaxCameo1.Location = New System.Drawing.Point(14, 4)
        Me.cmdMaxCameo1.Name = "cmdMaxCameo1"
        Me.cmdMaxCameo1.Size = New System.Drawing.Size(232, 23)
        Me.cmdMaxCameo1.TabIndex = 4
        Me.cmdMaxCameo1.Text = "Μεγιστοποίηση Αριστερής Κάμερας"
        Me.cmdMaxCameo1.UseVisualStyleBackColor = True
        '
        'cmdDualView
        '
        Me.cmdDualView.Location = New System.Drawing.Point(490, 4)
        Me.cmdDualView.Name = "cmdDualView"
        Me.cmdDualView.Size = New System.Drawing.Size(232, 23)
        Me.cmdDualView.TabIndex = 5
        Me.cmdDualView.Text = "Επαναφορά σε διπλή όψη"
        Me.cmdDualView.UseVisualStyleBackColor = True
        '
        'AxCameo1
        '
        Me.AxCameo1.Enabled = True
        Me.AxCameo1.Location = New System.Drawing.Point(14, 36)
        Me.AxCameo1.Name = "AxCameo1"
        Me.AxCameo1.OcxState = CType(resources.GetObject("AxCameo1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxCameo1.Size = New System.Drawing.Size(464, 576)
        Me.AxCameo1.TabIndex = 2
        '
        'cmdMaxCameo2
        '
        Me.cmdMaxCameo2.Location = New System.Drawing.Point(252, 4)
        Me.cmdMaxCameo2.Name = "cmdMaxCameo2"
        Me.cmdMaxCameo2.Size = New System.Drawing.Size(232, 23)
        Me.cmdMaxCameo2.TabIndex = 5
        Me.cmdMaxCameo2.Text = "Μεγιστοποίηση Δεξιάς Κάμερας"
        Me.cmdMaxCameo2.UseVisualStyleBackColor = True
        '
        'AxCameo2
        '
        Me.AxCameo2.ContextMenuStrip = Me.CameoMenuStrip
        Me.AxCameo2.Enabled = True
        Me.AxCameo2.Location = New System.Drawing.Point(34, 55)
        Me.AxCameo2.Name = "AxCameo2"
        Me.AxCameo2.OcxState = CType(resources.GetObject("AxCameo2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxCameo2.Size = New System.Drawing.Size(464, 576)
        Me.AxCameo2.TabIndex = 3
        '
        'CameoMenuStrip
        '
        Me.CameoMenuStrip.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.CameoMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisconnectDataStream})
        Me.CameoMenuStrip.Name = "ContextMenuStrip1"
        Me.CameoMenuStrip.Size = New System.Drawing.Size(139, 26)
        '
        'DisconnectDataStream
        '
        Me.DisconnectDataStream.Font = New System.Drawing.Font("Verdana", 7.0!)
        Me.DisconnectDataStream.Name = "DisconnectDataStream"
        Me.DisconnectDataStream.Size = New System.Drawing.Size(138, 22)
        Me.DisconnectDataStream.Text = "Disconnect"
        '
        'tmrReconnectDevices
        '
        Me.tmrReconnectDevices.Interval = 600000
        '
        'tmrWaitForPanAnswer
        '
        Me.tmrWaitForPanAnswer.Interval = 10000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1118, 734)
        Me.Controls.Add(Me.SCMain)
        Me.Controls.Add(Me.TreeView1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.Text = "ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SCMain.Panel1.ResumeLayout(False)
        Me.SCMain.Panel1.PerformLayout()
        Me.SCMain.Panel2.ResumeLayout(False)
        Me.SCMain.Panel2.PerformLayout()
        Me.SCMain.ResumeLayout(False)
        Me.TVDevicesMenuStrip.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.AxCameo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxCameo2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CameoMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents SCMain As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tvDevices As System.Windows.Forms.TreeView
    Friend WithEvents AxCameo2 As Bosch.VideoSDK.AxCameoLib.AxCameo
    Friend WithEvents AxCameo1 As Bosch.VideoSDK.AxCameoLib.AxCameo
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ErrorLevel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllErrors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OnlyErrors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ErrorsAndInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NoErrorLogging As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtInfo As System.Windows.Forms.TextBox
    Friend WithEvents OnlineLogΕφαρμογήςToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents showOnlineLogs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents hideOnlineLogs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectOtherLicFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents selectOtherXmlFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectNewXmlFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CameoMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DisconnectDataStream As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ΚαθάρισμαΚάμεραςToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearAxCameo1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearAxCameo2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrReconnectDevices As System.Windows.Forms.Timer
    Friend WithEvents TVDevicesMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TVConnect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TVDisConnect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ΥπολογισμόςΘέσηςToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdCalculatePosition As System.Windows.Forms.Button
    Friend WithEvents tmrWaitForPanAnswer As System.Windows.Forms.Timer
    Friend WithEvents ΒοήθειαToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdDualView As System.Windows.Forms.Button
    Friend WithEvents cmdMaxCameo2 As System.Windows.Forms.Button
    Friend WithEvents cmdMaxCameo1 As System.Windows.Forms.Button
    Friend WithEvents CalibrateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CalibrateDisabledToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CalibrateEnabledToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreposReadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
