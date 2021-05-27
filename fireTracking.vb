Imports Bosch.VideoSDK.Device
Imports Bosch.VideoSDK
Imports System.Threading
Imports System

Public Class frmMain
    Private WithEvents CameraConnector As Bosch.VideoSDK.Device.DeviceConnector
    Private CameraProxy As Bosch.VideoSDK.Device.DeviceProxy
    Private ActiveCameo As Bosch.VideoSDK.AxCameoLib.AxCameo
    Private Cameo2Connect As Bosch.VideoSDK.AxCameoLib.AxCameo
    Private CameraControllerCameo1 As Bosch.VideoSDK.Live.CameraController = Nothing
    Private CameraControllerCameo2 As Bosch.VideoSDK.Live.CameraController = Nothing
    Private CameraStreamCameo1 As Bosch.VideoSDK.DataStream
    Private CameraStreamCameo2 As Bosch.VideoSDK.DataStream
    Private IP2Connect, vSelectedTreeIP As String
    Private Cameo1IP As String = ""
    Private Cameo2IP As String = ""
    Dim IVAArray() As IVAConnect.cIVA
    Private objDevices() As cDevices
    Dim MyThread, ReconnectThread As Thread
    Dim tvImageList As New ImageList()
    Dim numberOfFieldsForTheXML As Integer = 0
    Dim AxCameo1X1, AxCameo1X2 As Integer
    Dim AxCameo1Y1, AxCameo1Y2 As Integer
    Dim AxCameo2X1, AxCameo2X2 As Integer
    Dim AxCameo2Y1, AxCameo2Y2 As Integer
    Dim AxCameo1ReceivedPan As Boolean = False
    Dim AxCameo2ReceivedPan As Boolean = False
    Dim AxCameo1Reading, AxCameo2Reading As Integer
    Dim cameoArray(0) As Bosch.VideoSDK.AxCameoLib.AxCameo
    Const vCameraModel As String = "Bosch-AutoDome"
    Private vPTZMode As Bosch.VideoSDK.Live.PTZModeEnum = Live.PTZModeEnum.ptzNormalized
    Dim vCam1obj As Integer = -1
    Dim vCam2obj As Integer = -1
    Dim vCalibrateEnabled As Boolean = False
    

    Private Enum CameoInForm As Integer
        LeftCameo = 1
        RightCameo = 2
    End Enum

    Private Enum DialogType As Integer
        LicenseFile = 1
        XMLDataFile = 2
    End Enum

    Private Enum FileType As Integer
        License = 1
        XMLData = 2
    End Enum

    Private Enum ProjectType As Integer
        VCA = 1
        LPR = 2
        PTZ = 3
    End Enum

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        CloseApplication()
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not CameraProxy Is Nothing Then
            CameraProxy.Disconnect()
            CameraProxy = Nothing
        End If
    End Sub

    ' sto onload kathorizo ta 2 vasika arxia pou xriazete i efarmogi gia na treksi 
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ApplicationLogLevel = AppLogLevel.All
        ApplicationEventLog.WriteEntry("FTA Initiated at : " & Now, EventLogEntryType.Information)
        MakeInfoEntry("Ο διακομιστής ξεκίνησε.")
        ' Check existance of Licence Fiile
        If My.Settings.LicenseFile = String.Empty Then
            ApplicationEventLog.WriteEntry("No License file specified.", EventLogEntryType.Warning)
            MakeInfoEntry("Δεν υπάρχει προεπιλεγμένο αρχείο αδειών. Θα πρέπει να καθοριστεί ένα για να ξεκινήσει το σύστημα.")
            Dim LicDialog As DialogResult
            LicDialog = MessageBox.Show("Δεν έχει καθοριστεί αρχείο αδειών χρήσης. Θα Θέλατε να καθορίσετε ποιό αρχείο αδειών χρήσης χρησιμοποιει η εφαρμογή σας? Αν δεν έχετε αρχείο αδειών χρήσης θα πρέπει να επικοινωνήσετε με την εταιρεία Virtual Controls", "Δεν έχει καθοριστεί αρχείο αδειών χρήσης", _
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If LicDialog = Windows.Forms.DialogResult.Yes Then
                If Not ShowFileOpenDialog(DialogType.LicenseFile) Then
                    MessageBox.Show("Η εφαρμογή πρόκειτε να τερματιστεί λόγω έλλειψης άδειας χρήσης.")
                    ApplicationEventLog.WriteEntry("User has not specified a License file. Application exiting", EventLogEntryType.Error)
                    Application.Exit()
                Else
                    ProcessFile(FileType.License)
                End If
            Else
                MessageBox.Show("Η εφαρμογή πρόκειτε να τερματιστεί λόγω έλλειψης άδειας χρήσης.")
                ApplicationEventLog.WriteEntry("User has not specified a License file. Application exiting", EventLogEntryType.Error)
                Application.Exit()
            End If
        Else
            ProcessFile(FileType.License)
        End If

        ' Check existance of Xml Configuration File
        If My.Settings.XMLData = String.Empty Then
            ApplicationEventLog.WriteEntry("Δεν υπάρχει προεπιλεγμένο αρχείο στοιχείων καμερών.", EventLogEntryType.Warning)
            MakeInfoEntry("Δεν υπάρχει προεπιλεγμένο αρχείο στοιχείων καμερών.")
            Dim RegMacDialog As DialogResult
            RegMacDialog = MessageBox.Show("Δεν υπάρχει προεπιλεγμένο αρχείο στοιχείων καμερών. Θα θέλατε να επιλέξετε ένα ήδη υπάρχων αρχείο στοιχείων καμερών?", "Δεν υπάρχει προεπιλεγμένο αρχείο στοιχείων καμερών", _
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If RegMacDialog = Windows.Forms.DialogResult.Yes Then
                If Not ShowFileOpenDialog(DialogType.XMLDataFile) Then
                    MessageBox.Show("Η εφαρμογή πρόκειτε να τερματιστεί λόγω έλλειψης αρχείου στοιχείων καμερών.")
                    ApplicationEventLog.WriteEntry("User has not specified an Xml Configuration. Application exiting", EventLogEntryType.Error)
                    Application.Exit()
                Else
                    ProcessFile(FileType.XMLData)
                End If
            Else
                If MessageBox.Show("Θα θέλατε να δημιουργήσετε ένα καινούργιο αρχείο στοιχείων καμερών?", "Νέο αρχείο στοιχείων καμερών", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    ChangeConfigFile()
                Else
                    MessageBox.Show("Η εφαρμογή πρόκειτε να τερματιστεί λόγω έλλειψης αρχείου στοιχείων καμερών.")
                    ApplicationEventLog.WriteEntry("User has not specified an Xml Configuration. Application exiting", EventLogEntryType.Error)
                    Application.Exit()
                End If
            End If
        Else
            ProcessFile(FileType.XMLData)
        End If
        SetAndGetCameoFinalLocations()
        CreateControlsFinalPositions()
        StartFTAServer()
    End Sub

    Private Sub ChooseLicFile()
        Dim LicDialog As DialogResult
        LicDialog = MessageBox.Show("Θα Θέλατε να καθορίσετε κάποιο άλλο αρχείο αδειών χρήσης? Αν ναι αφού επιλεχτεί το καινούργιο αρχείο θα γίνει επανασύνδεση του συστήματος σε όλες τις συσκευές και θα γίνει εσωτερική επανεκκίνηση των στοιχείων της εφαρμογής.", "Καθορισμός αρχείου αδειών χρήσης", _
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If LicDialog = Windows.Forms.DialogResult.Yes Then
            If Not ShowFileOpenDialog(DialogType.LicenseFile) Then
                MessageBox.Show("Η εφαρμογή πρόκειτε να τερματιστεί λόγω έλλειψης άδειας χρήσης.")
                ApplicationEventLog.WriteEntry("User has not specified a License file. Application exiting", EventLogEntryType.Error)
                Application.Exit()
            Else
                ProcessFile(FileType.License)
            End If
        End If
    End Sub

    Private Sub ChooseXmlFile()
        Dim RegMacDialog As DialogResult
        RegMacDialog = MessageBox.Show("Θα Θέλατε να καθορίσετε κάποιο άλλο αρχείο στοιχείων καμερών? Αν ναι αφού επιλεχτεί το καινούργιο αρχείο θα γίνει επανασύνδεση του συστήματος σε όλες τις συσκευές και θα γίνει εσωτερική επανεκκίνηση των στοιχείων της εφαρμογής.", "Καθορισμός αρχείου στοιχείων καμερών", _
                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If RegMacDialog = Windows.Forms.DialogResult.Yes Then
            If Not ShowFileOpenDialog(DialogType.XMLDataFile) Then
                MessageBox.Show("Η εφαρμογή πρόκειτε να τερματιστεί λόγω έλλειψης αρχείου στοιχείων καμερών.")
                ApplicationEventLog.WriteEntry("User has not specified an Xml Configuration. Application exiting", EventLogEntryType.Error)
                Application.Exit()
            Else
                ProcessFile(FileType.XMLData)
            End If
        End If
    End Sub

    Private Sub SetAndGetCameoFinalLocations()
        AxCameo1.Visible = True
        AxCameo2.Visible = True
        'AxCameo1.BackgroundPicture = My.Resources.vc
        'AxCameo2.BackgroundPicture = My.Resources.vc
        buildCameos()
        AxCameo1X1 = tvDevices.Width + AxCameo1.Location.X + 10
        AxCameo1X2 = tvDevices.Width + AxCameo1.Location.X + 10 + AxCameo1.Size.Width
        AxCameo1Y1 = AxCameo1.Location.Y + 30
        AxCameo1Y2 = AxCameo1.Location.Y + 30 + AxCameo1.Size.Height
        AxCameo2X1 = tvDevices.Width + AxCameo2.Location.X + 10
        AxCameo2X2 = tvDevices.Width + AxCameo2.Location.X + 10 + AxCameo2.Size.Width
        AxCameo2Y1 = AxCameo2.Location.Y + 20
        AxCameo2Y2 = AxCameo2.Location.Y + 20 + AxCameo2.Size.Height
    End Sub

    Private Sub buildCameos()
        Dim vTotalwidth As Integer = SCMain.Panel2.Width - 20 ' gia na afisi ena mikro keno deksia
        Dim vMaxWidthPerCameo As Integer
        Dim drawCameo1, drawCameo2 As System.Drawing.Point
        vMaxWidthPerCameo = (vTotalwidth - 2 * 20) / 2 ' here we can change 2 with i for multiple cameos
        drawCameo1.X = 10
        drawCameo1.Y = 90
        drawCameo2.X = 30 + vMaxWidthPerCameo
        drawCameo2.Y = 90
        AxCameo1.Location = drawCameo1
        AxCameo2.Location = drawCameo2
        AxCameo1.Width = vMaxWidthPerCameo
        AxCameo1.Height = vMaxWidthPerCameo * 3 / 4
        AxCameo2.Width = vMaxWidthPerCameo
        AxCameo2.Height = vMaxWidthPerCameo * 3 / 4
    End Sub

    Private Sub CreateControlsFinalPositions()
        Dim txtInfoLocation As System.Drawing.Point
        Dim vTotalwidth As Integer = SCMain.Panel2.Width - 20
        tvDevices.Width = SCMain.Panel1.Width - 1
        'txtInfoLocation.Y = AxCameo1.Location.Y + 60 + (vTotalwidth * 3 / 4)
        txtInfoLocation.Y = 850
        txtInfoLocation.X = 15
        txtInfo.Location = txtInfoLocation

    End Sub

    Private Sub ChangeCameoSizes(ByVal cameoid As Integer, ByVal IsForMaximise As Boolean)
        If IsForMaximise Then
            MaximiseSpecificCameo(cameoid)
        Else
            SetAndGetCameoFinalLocations()
        End If
    End Sub


    Private Sub MaximiseSpecificCameo(ByVal cameoId As Integer)
        Dim vTotalwidth As Integer = SCMain.Panel2.Width - 20 ' gia na afisi ena mikro keno deksia
        Dim vMaxWidthPerCameo As Integer
        Dim drawCameo1, drawCameo2 As System.Drawing.Point
        vMaxWidthPerCameo = (vTotalwidth - 1 * 20) / 1 ' here we can change 2 with i for multiple cameos
        Select Case cameoId
            Case 1
                AxCameo1.Visible = True
                drawCameo1.X = 10
                drawCameo1.Y = 30
                AxCameo1.Location = drawCameo1
                AxCameo1.Width = vMaxWidthPerCameo
                AxCameo1.Height = vMaxWidthPerCameo * 3 / 4
                AxCameo1X1 = tvDevices.Width + AxCameo1.Location.X + 10
                AxCameo1X2 = tvDevices.Width + AxCameo1.Location.X + 10 + AxCameo1.Size.Width
                AxCameo1Y1 = AxCameo1.Location.Y + 30
                AxCameo1Y2 = AxCameo1.Location.Y + 30 + AxCameo1.Size.Height
                AxCameo2X1 = 1
                AxCameo2Y1 = 1
                AxCameo2X2 = 2
                AxCameo2Y2 = 2
                AxCameo2.Visible = False
            Case 2
                AxCameo2.Visible = True
                drawCameo2.X = 10
                drawCameo2.Y = 30
                AxCameo2.Location = drawCameo2
                AxCameo2.Width = vMaxWidthPerCameo
                AxCameo2.Height = vMaxWidthPerCameo * 3 / 4
                AxCameo2X1 = tvDevices.Width + AxCameo2.Location.X + 10
                AxCameo2X2 = tvDevices.Width + AxCameo2.Location.X + 10 + AxCameo2.Size.Width
                AxCameo2Y1 = AxCameo2.Location.Y + 20
                AxCameo2Y2 = AxCameo2.Location.Y + 20 + AxCameo2.Size.Height
                AxCameo1X1 = 1
                AxCameo1Y1 = 1
                AxCameo1X2 = 2
                AxCameo1Y2 = 2
                AxCameo1.Visible = False
        End Select
    End Sub

    Private Sub ResetServer()
        StopFTAServer()
        StartFTAServer()
    End Sub

    Private Sub UpdateTreeView()
        tvDevices.Nodes.Clear()
        tvDevices.ResetText()
        CreateImageList()
        tvDevices.ImageList = tvImageList
        CreateDeviceTV()
    End Sub

    Private Function ShowFileOpenDialog(ByVal FileDialogType As DialogType) As Boolean
        Dim UserFileDialog As New OpenFileDialog
        With UserFileDialog
            Select Case FileDialogType
                Case DialogType.XMLDataFile
                    .Filter = "XML Config files|*.xml"
                    .InitialDirectory = "C:\program Files\Virtual Controls\Fire Tracking Application"
                    .Title = "Open XML configuration file"
                    If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                        ApplicationEventLog.WriteEntry(.FileName & " is selected as the XML Configuration file", EventLogEntryType.Information)
                        MakeInfoEntry(.FileName & " επιλέχτηκε ως το αρχείο στοιχείων καμερών")
                        My.Settings.XMLData = .FileName
                        Return True
                    Else
                        If My.Settings.XMLData = String.Empty Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
                Case DialogType.LicenseFile
                    .Filter = "Αρχείο αδειών Χρήσης|*.lic"
                    .InitialDirectory = "C:\program Files\Virtual Controls\Fire Tracking Application"
                    .Title = "Επιλογή Αρχείου αδειών Χρήσης"
                    If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                        ApplicationEventLog.WriteEntry(.FileName & " is selected as License file", EventLogEntryType.Information)
                        MakeInfoEntry(.FileName & " επιλέχτηκε ως αρχείο αδειών χρήσης")
                        My.Settings.LicenseFile = .FileName
                        Return True
                    Else
                        If My.Settings.LicenseFile = String.Empty Then
                            Return False
                        Else
                            Return True
                        End If
                    End If
            End Select
        End With
    End Function

    Private Sub CreateDeviceTV()
        If Not arrCamConfig Is Nothing Then
            PopulateDeviceTree(ProjectType.PTZ)
            CreateTVDevices()
        End If
    End Sub

    Private Sub CreateTVDevices()
        For k = 0 To arrCamConfig.GetLength(0) - 1
            tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes.Add(arrCamConfig(k, 1), arrCamConfig(k, 2))
            tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(arrCamConfig(k, 1)).ImageKey = "1"
            tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(arrCamConfig(k, 1)).SelectedImageKey = "1"
            tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(arrCamConfig(k, 1)).ToolTipText = arrCamConfig(k, 1)
        Next
    End Sub

    Private Sub CreateImageList()
        tvImageList.Images.Add("1", My.Resources.Connection)
        tvImageList.Images.Add("2", My.Resources.NOConnection)
        tvImageList.Images.Add("3", My.Resources.encoder)
        tvImageList.Images.Add("4", My.Resources.start)
    End Sub

    Private Sub PopulateDeviceTree(ByVal ProjectTypeNode As ProjectType)
        With tvDevices
            Select Case ProjectTypeNode
                Case ProjectType.VCA
                    .Nodes.Add("VCA", "VCA Devices")
                Case ProjectType.LPR
                    .Nodes.Add("LPR", "LPR Devices")
                Case ProjectType.PTZ
                    .Nodes.Add("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ", "Σύστημα Εντοπισμού πυρκαγιάς")
            End Select
        End With
    End Sub

    Private Sub tvAddNode(ByVal vIP As String, ByVal vChannel As Integer, ByVal vImgkey As String)
        tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(vIP).Nodes.Add(vChannel, "Channel " & vChannel)
        tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(vIP).Nodes(CStr(vChannel)).ImageKey = vImgkey
        tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(vIP).Nodes(CStr(vChannel)).SelectedImageKey = vImgkey
    End Sub

    Private Sub ProcessFile(ByVal CurrentFileType As FileType)
        Select Case CurrentFileType
            Case FileType.License
                'do nothing
            Case FileType.XMLData
                ReadXmlConfigFile()
        End Select
    End Sub


    ' =============================== Delegates gia ta cross threading calls ===============================


    Delegate Sub SetTextCallback(ByVal [text] As String)

    Private Sub SetText(ByVal [text] As String)
        Me.txtInfo.Text = Now & " : " & [text] & vbCrLf & Me.txtInfo.Text
    End Sub

    Friend Sub UpdateDetailsSafely(ByRef outputString As String)
        Try

            Dim NewText As String = outputString

            If Me.txtInfo.InvokeRequired Then
                ' It's on a different thread, so use Invoke.
                Dim d As New SetTextCallback(AddressOf SetText)
                Me.Invoke(d, New Object() {[NewText]})
            Else
                ' It's on the same thread, no need for Invoke.
                Me.txtInfo.Text = Now & " : " & [NewText] & vbCrLf & Me.txtInfo.Text
            End If
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Delegate Sub SetImgCallback(ByVal vText As String, ByVal vDev As String)

    Private Sub SetTVImg(ByVal vText As String, ByVal vDev As String)
        tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(vDev).ImageKey = vText
        tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(vDev).SelectedImageKey = vText
    End Sub

    Friend Sub UpdateIMGDetailsSafely(ByVal outputString As String, ByVal OutPutDevice As String)
        Try

            Dim NewText As String = outputString
            Dim vDev As String = OutPutDevice
            ' Check if this method is running on a different thread
            ' than the thread that created the control.
            If Me.tvDevices.InvokeRequired Then
                ' It's on a different thread, so use Invoke.
                'Dim dMethod As New SetImgCallback(AddressOf SetTVImg)
                'Me.Invoke(dMethod, New Object() {[NewText], [vDev]})
                Dim del As SetImgCallback
                del = New SetImgCallback(AddressOf SetTVImg)
                Me.Invoke(del, New Object() {NewText, vDev})
            Else
                ' It's on the same thread, no need for Invoke.
                tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(vDev).ImageKey = NewText
                tvDevices.Nodes("ΣΥΣΤΗΜΑ ΕΝΤΟΠΙΣΜΟΥ ΠΥΡΚΑΓΙΑΣ").Nodes(vDev).SelectedImageKey = NewText
            End If
        Catch ex As Exception
            'do nothing
        End Try

    End Sub

    Delegate Sub EnableCalculationCallback()

    Private Sub EnableCalculation()
        cmdCalculatePosition.Enabled = True
    End Sub

    Friend Sub EnableCalculationSafely()
        Try

            ' Check if this method is running on a different thread
            ' than the thread that created the control.
            If Me.cmdCalculatePosition.InvokeRequired Then
                ' It's on a different thread, so use Invoke.
                'Dim dMethod As New SetImgCallback(AddressOf SetTVImg)
                'Me.Invoke(dMethod, New Object() {[NewText], [vDev]})
                Dim del As EnableCalculationCallback
                del = New EnableCalculationCallback(AddressOf EnableCalculation)
                Me.Invoke(del, New Object() {})
            Else
                ' It's on the same thread, no need for Invoke.
                cmdCalculatePosition.Enabled = True
            End If
        Catch ex As Exception
            'do nothing
        End Try

    End Sub

    ' =============================== FINISHED Delegates gia ta cross threading calls ===============================

    Private Sub StartFTAServer()
        Try
            tvDevices.Nodes.Clear()
            If Not arrCamConfig Is Nothing Then
                UpdateTreeView()
            Else
                Throw New Exception("Το αρχείο συσκευών δεν έχει καμία εγγραφή.")
            End If
            tmrReconnectDevices.Stop()
            tmrReconnectDevices.Start()
            MyThread = New Thread(AddressOf CreateConnectionToDevices)
            MyThread.Start()
        Catch ex As Exception
            ApplicationEventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
            MakeInfoEntry("Σφάλμα κατά την διαδικασία έναρξης του διακομιστή. Παρακαλώ επανεκκινήστε την διαδικασία.")
            MakeInfoEntry(ex.Message)
            MessageBox.Show("Σφάλμα κατά την διαδικασία έναρξης του διακομιστή. Το αρχείο συσκευών δεν έχει καμία εγγραφή.", "Γενικό σφάλμα", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CreateConnectionToDevices()
        Try
            ' create all the iva dll objects
            For k = 0 To arrCamConfig.GetLength(0) - 1
                Dim vCounter As Integer = 0
                CreateIvaObject(k, arrCamConfig(k, 1))
                MakeInfoEntry("Έναρξη σύνδεσης με την συσκευή " & arrCamConfig(k, 2) & " με ip : " & arrCamConfig(k, 1))
                AddDevice(arrCamConfig(k, 0), arrCamConfig(k, 1), arrCamConfig(k, 2), arrCamConfig(k, 3), arrCamConfig(k, 4), arrCamConfig(k, 5), k)
                MakeInfoEntry("Δημιουργία συσκευής " & arrCamConfig(k, 2) & " με ip :" & arrCamConfig(k, 1))
            Next
            ' create all the connections to the devices
            For i = 0 To IVAArray.Length - 1
                AddHandler IVAArray(i).ConnectionStatus, AddressOf ConnectionStateReceived
                AddHandler IVAArray(i).PanReading, AddressOf PositionReceived
                AddHandler IVAArray(i).PanPreposition99Reading, AddressOf Position99Received
                IVAArray(i).ConnectDevice(My.Settings.LicenseFile, arrCamConfig(i, 1))
                System.Threading.Thread.Sleep(200)
            Next

            ApplicationEventLog.WriteEntry("Finished Creating Connections to all the devices", EventLogEntryType.Information)

            ' create all the handlers to the devices
            For i = 0 To IVAArray.Length - 1
                If Not IVAArray(i) Is Nothing Then
                    AddHandler IVAArray(i).SocketErrorReceived, AddressOf SocketErrorReceived
                End If
            Next
            ApplicationEventLog.WriteEntry("Finished Creating Connections to devices.", EventLogEntryType.Information)
            MakeInfoEntry("πραγματοποιήθηκε με επιτυχία η σύνδεση σε όλες τις αδειοδοτημένες συσκευές")
        Catch ex As Exception
            ApplicationEventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
            MakeInfoEntry("Πρόβλημα κατά την σύνδεση με τις συσκευές")
            MakeInfoEntry(ex.Message)
            CleanAllCreatedObjects()
        End Try
    End Sub

    Private Sub AddDevice(ByVal vMac As String, ByVal vIP As String, ByVal vDeviceName As String, ByVal vChannel As String, ByVal Longtitude As String, ByVal Latitude As String, ByVal objId As Integer)
        Dim vDeviceExists As Boolean = False
        ' search if the device already exists 
        If objDevices Is Nothing Then
            vDeviceExists = False
        Else
            For Each devObject In objDevices
                If devObject.DevIPAddress = vIP And devObject.DevMacAddress = vMac Then
                    vDeviceExists = True
                End If
            Next
        End If

        ' if it does not exist create device object
        If vDeviceExists = False Then
            ReDim Preserve objDevices(objId)
            objDevices(objId) = New cDevices(vMac, vIP, vDeviceName, vChannel, Longtitude, Latitude, objId)
        End If
    End Sub

    Private Sub CleanAllCreatedObjects()
        DisconnectDevices()
        'ClearDevices()
    End Sub

    Private Sub DisconnectDevicesOLD()
        If Not IVAArray Is Nothing Then
            For Each device As IVAConnect.cIVA In IVAArray
                If Not device Is Nothing Then
                    device.DisconnectDevice()
                End If
            Next
        End If
    End Sub

    Private Sub DisconnectDevices()
        If Not IVAArray Is Nothing Then
            For i = 0 To IVAArray.Length - 1
                If Not IVAArray(i) Is Nothing Then
                    IVAArray(i).DisconnectDevice()
                End If
            Next
        End If
    End Sub

    Private Sub ClearDevices()
        If Not objDevices Is Nothing Then
            For Each virtualDevice As cDevices In objDevices
                virtualDevice = Nothing
            Next
        End If
    End Sub

    Private Sub CloseApplication()
        Dim myAnswer As DialogResult
        myAnswer = MessageBox.Show("Αν γίνει έξοδος από την εφαρμογή, θα χαθούν όλες οι συνδέσεις με τις συσκευές. Επιθυμείτε να γίνει έξοδος.", "Προσοχή", MessageBoxButtons.YesNo, MessageBoxIcon.Hand)
        If myAnswer = Windows.Forms.DialogResult.Yes Then
            ApplicationEventLog.WriteEntry("FTA Server Shut Down at : " & Now.ToString, EventLogEntryType.Information)
            CleanAllCreatedObjects()
            If Not MyThread Is Nothing Then
                MyThread.Abort()
                MyThread = Nothing
            End If
            Application.Exit()
        End If
    End Sub

    Private Sub CreateIvaObject(ByVal objID As Integer, ByVal vIP As String)
        ReDim Preserve IVAArray(objID)
        IVAArray(objID) = New IVAConnect.cIVA
        IVAArray(objID).SetDebugLevel(ApplicationLogLevel)
    End Sub

    Private Sub ReadXmlConfigFile()
        numberOfFieldsForTheXML = 6
        '6th field used for the connection 1= connected 2 = disconnected
        If Not My.Settings.XMLData = String.Empty Then
            arrCamConfig = ReadDataFromXML(My.Settings.XMLData, numberOfFieldsForTheXML)
        End If
    End Sub

    Private Sub ChangeConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeConfigurationToolStripMenuItem.Click
        ChangeConfigFile()
    End Sub

    Private Sub ChangeConfigFile()
        Dim fCamConfig As New CamConfiguration
        fCamConfig.ShowDialog()
        resetConfigFile()
    End Sub

    Private Sub tvDevices_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tvDevices.DoubleClick
        gSelectedIP = tvDevices.SelectedNode.Name
        Dim fDomeConfig As New DomeConfiguration
        fDomeConfig.ShowDialog()
    End Sub

    Private Sub resetConfigFile()
        CleanAllCreatedObjects()
        ProcessFile(FileType.XMLData)
        ResetServer()
    End Sub

    Private Function GetDeviceIPFromName(ByVal vDev As String) As String
        Dim vSelectedIP As String = ""
        Try
            For i = 0 To arrCamConfig.GetLength(0) - 1
                If arrCamConfig(i, 2) = Trim(vDev) Then
                    vSelectedIP = Trim(arrCamConfig(i, 1))
                End If
            Next
        Catch ex As Exception
            ' return keno string
            MakeInfoEntry("Η επιλεγμένη συσκευή δεν έχει διεύθυνση ΙΡ. Παρακαλώ όπως διορθώσετε το αρχείο στοιχείων καμερών.")
            ApplicationEventLog.WriteEntry(ex.Message & " No Ip For selected camera or problem with the array", EventLogEntryType.Error)
        End Try
        Return vSelectedIP
    End Function

    Private Function IsDeviceRegistered(ByVal vDev As String) As Boolean
        Dim isDevRegisterd As Boolean = False
        For i = 0 To arrCamConfig.GetLength(0) - 1
            If arrCamConfig(i, 2) = Trim(vDev) Then
                If arrCamConfig(i, 6) = 1 Then
                    isDevRegisterd = True
                End If
            End If
        Next
        Return True
        'Return isDevRegisterd
    End Function

    Private Sub BuildConnectionToDevice(ByVal vDevice As String, ByVal CameoToLoad As Integer)
        Try
            If IsDeviceRegistered(vDevice) Then
                ' Device is licensed, make a note of the IP to connect
                IP2Connect = Trim(GetDeviceIPFromName(vDevice))
                BuildConnectionObject(IP2Connect)
            Else
                Throw New Exception("Η επιλεγμένη συσκευή ή δεν έχει άδεια ή παρουσιάζει κάποιο τεχνικο/δικτυακό πρόβλημα.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Σφάλμα κατά την σύνδεση", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Private Sub StopFTAServer()
        CleanAllCreatedObjects()
        Me.txtInfo.Text = ""
        If Not MyThread Is Nothing Then
            MyThread.Abort()
            MyThread = Nothing
        End If
        ApplicationEventLog.WriteEntry("Server stopped by user", EventLogEntryType.Error)
    End Sub

    Private Sub SocketErrorReceived(ByVal vIPAddress As String)
        Try
            ApplicationEventLog.WriteEntry("Socket Exception για την συσκευή με IP: " & vIPAddress, EventLogEntryType.Error)
            For Each vDev As cDevices In objDevices
                If vDev.DevIPAddress = vIPAddress Then
                    IVAArray(vDev.GetObjId).DisconnectDevice()
                    Exit For
                End If
            Next
        Catch ex As Exception
            MakeInfoEntry("Γενικό σφάλμα κατά την διαδικασία dispose του προβληματικού socket. ")
            ApplicationEventLog.WriteEntry("general failure receiving the socket to dispose", EventLogEntryType.Error)
        End Try
    End Sub

    Private Sub ConnectSpecificDevice(ByVal vObj As Integer)
        Try
            If IVAArray(vObj) Is Nothing Then
                UpdateIMGDetailsSafely("1", arrCamConfig(vObj, 1))
                IVAArray(vObj) = New IVAConnect.cIVA
                IVAArray(vObj).SetDebugLevel(ApplicationLogLevel)
                AddHandler IVAArray(vObj).SocketErrorReceived, AddressOf SocketErrorReceived
                AddHandler IVAArray(vObj).ConnectionStatus, AddressOf ConnectionStateReceived
                AddHandler IVAArray(vObj).PanReading, AddressOf PositionReceived
                IVAArray(vObj).ConnectDevice(My.Settings.LicenseFile, arrCamConfig(vObj, 1))
            End If
        Catch ex As Exception
            MakeInfoEntry("Πρόβλημα κατά την επανασύνδεση με την συσκευή " & arrCamConfig(vObj, 2) & " με ip : " & arrCamConfig(vObj, 1))
            ApplicationEventLog.WriteEntry("Problem ReConnecting to device " & arrCamConfig(vObj, 1), EventLogEntryType.Error)
        End Try
    End Sub

    Private Sub DisConnectSpecificDevice(ByVal vObj As Integer)
        Try
            If Not IVAArray(vObj) Is Nothing Then
                RemoveHandler IVAArray(vObj).SocketErrorReceived, AddressOf SocketErrorReceived
                RemoveHandler IVAArray(vObj).ConnectionStatus, AddressOf ConnectionStateReceived
                IVAArray(vObj) = Nothing
                UpdateIMGDetailsSafely("2", arrCamConfig(vObj, 1))
                UpdateDetailsSafely("Αποσυνδέθηκε η συσκευή " & arrCamConfig(vObj, 2) & " με IP: " & arrCamConfig(vObj, 1))
            Else
                MakeInfoEntry("Η συσκευή " & arrCamConfig(vObj, 2) & " με IP " & arrCamConfig(vObj, 1) & " είναι ήδη αποσυνδεδεμένη.")
            End If
        Catch ex As Exception
            MakeInfoEntry("Προβλημα κατά την αποσύνδεση από την συσκευή " & arrCamConfig(vObj, 2) & " με IP: " & arrCamConfig(vObj, 1))
        End Try
    End Sub

    Private Sub ConnectionStateReceived(ByVal vIP As String, ByVal vState As Boolean)
        Try
            For Each vDev As cDevices In objDevices
                If vDev.DevIPAddress = vIP Then
                    If vState Then ' is for reconnecting to device only changes the color on the tree view
                        '  Debug.WriteLine("Created Connection to device " & vDev.DevIPAddress)
                        UpdateIMGDetailsSafely("4", vDev.DevIPAddress)
                        UpdateDetailsSafely("Πραγματοποιήθηκε σύνδεση με την συσκευή " & vDev.GetDeviceName & " με IP: " & vDev.DevIPAddress)
                        arrCamConfig(vDev.GetObjId, numberOfFieldsForTheXML) = 1
                        vDev.DevIsConnected = True
                        IVAArray(vDev.DevObjID).PTZPreposition99PanGet()
                    Else ' for disconnecting from a device
                        DisConnectSpecificDevice(vDev.GetObjId)
                        arrCamConfig(vDev.GetObjId, numberOfFieldsForTheXML) = 0
                        vDev.DevIsConnected = False
                    End If
                    Exit For
                End If
            Next
        Catch ex As Exception
            MakeInfoEntry("Γενικό σφάλμα κατά την αλλαγή κατάστασης μιας συσκευής.")
        End Try
    End Sub

    Private Sub SendDisconnectCommandToDevice(ByVal vObjID As Integer)
        IVAArray(vObjID).DisconnectDevice()
    End Sub

    Private Sub ReconnectToDevices()
        Try
            For i = 0 To arrCamConfig.GetLength(0) - 1
                If arrCamConfig(i, numberOfFieldsForTheXML) = 0 Then
                    ConnectSpecificDevice(i)
                End If
            Next
        Catch ex As Exception
            ApplicationEventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
            MakeInfoEntry("Σφάλμα κατά την διαδικασία αυτόματης επανασύνδεσης σε όλες τις εξουσιοδοτημένες συσκευές.")
        End Try
    End Sub

    Private Sub tmrReconnectDevices_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrReconnectDevices.Tick
        If Not ReconnectThread Is Nothing Then
            ReconnectThread.Abort()
            ReconnectThread = Nothing
        End If
        ReconnectThread = New Thread(AddressOf TimerReconnectToDevices)
        ReconnectThread.Start()
        tmrReconnectDevices.Stop()
        tmrReconnectDevices.Start()
        MakeInfoEntry("Ξεκίνησε η αυτόματη απανασύνδεση στις συσκευές που έχουν αποσυνδεθεί.")
    End Sub

    Private Sub TimerReconnectToDevices()
        ReconnectToDevices()
        UpdateDetailsSafely("Τελείωσε η αυτόματη επανασύνδεση στις συσκευές που έχουν αποσυνδεθεί.")
    End Sub

    Public Sub MakeInfoEntry(ByVal vText As String)
        UpdateDetailsSafely(vText)
    End Sub

    Private Sub CalculatePosition()
        Try
            Dim vFoundCameo1 As Boolean = False
            Dim vFoundCameo2 As Boolean = False

            cmdCalculatePosition.Enabled = False
            If Cameo1IP = "" Or Cameo2IP = "" Or Trim(Cameo1IP) = Trim(Cameo2IP) Then
                Throw New Exception("Πρέπει να υπάρχει εικόνα και στις δύο κάμερες και η εικόνα αυτή να είναι διαφορετική.")
            End If
            For Each vDevCameo1 As cDevices In objDevices
                If vDevCameo1.DevIPAddress = Cameo1IP Then
                    vCam1obj = vDevCameo1.GetObjId
                    vFoundCameo1 = True
                    Exit For
                End If
            Next
            For Each vDevCameo2 As cDevices In objDevices
                If vDevCameo2.DevIPAddress = Cameo2IP Then
                    vCam2obj = vDevCameo2.GetObjId
                    vFoundCameo2 = True
                    Exit For
                End If
            Next
            If vFoundCameo1 = False Or vFoundCameo2 = False Or vCam1obj = vCam2obj Then
                Throw New Exception("Παρουσιάστηκε πρόβλημα κατά την εύρεση των στοιχείων μιας κάμερας ή έχει επιλεχτεί η ίδια κάμερα δύο φορές.")
            End If

            If Not IVAArray(vCam1obj) Is Nothing Then
                If Not IVAArray(vCam2obj) Is Nothing Then
                    ' start a 10 sec timer w8ing for both devices to answer. if the answer has not been received an error is generated
                    tmrWaitForPanAnswer.Stop()
                    tmrWaitForPanAnswer.Start()
                    ' get pan reading from cameras
                    IVAArray(vCam1obj).PTZPanGet()
                    ApplicationEventLog.WriteEntry("Στάλθηκε εντολή στην κάμερα " & objDevices(vCam1obj).GetDeviceName & " να μας επιστρέψει το pan της. Object id : " & vCam1obj, EventLogEntryType.Information)
                    System.Threading.Thread.Sleep(500)
                    IVAArray(vCam2obj).PTZPanGet()
                    ApplicationEventLog.WriteEntry("Στάλθηκε εντολή στην κάμερα " & objDevices(vCam2obj).GetDeviceName & " να μας επιστρέψει το pan της. Object id : " & vCam2obj, EventLogEntryType.Information)
                Else
                    Throw New Exception("Η κάμερα με ΙΡ : " & objDevices(vCam2obj).DevIPAddress & " και ονομασία " & objDevices(vCam2obj).GetDeviceName & " αντιμετωπίζει κάποιο πρόβλημα επικοινωνίας. Επιλέξτε την από την λίστα με τις κάμερες, πατήστε δεξί κλίκ πάνω της και κάντε connect")
                End If
            Else
                Throw New Exception("Η κάμερα με ΙΡ : " & objDevices(vCam1obj).DevIPAddress & " και ονομασία " & objDevices(vCam1obj).GetDeviceName & " αντιμετωπίζει κάποιο πρόβλημα επικοινωνίας. Επιλέξτε την από την λίστα με τις κάμερες, πατήστε δεξί κλίκ πάνω της και κάντε connect")
            End If
        Catch ex As Exception
            MakeInfoEntry(ex.Message)
            cmdCalculatePosition.Enabled = True
        End Try

    End Sub

    Private Sub cmdCalculatePosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCalculatePosition.Click
        CalculatePosition()
    End Sub

    Private Sub Position99Received(ByVal vIP As String, ByVal vReading As String)
        Try
            ' MessageBox.Show("vIP: " & vIP & "   vReading: " & vReading)
            Dim vObjID As Integer = -1
            For Each vDev As cDevices In objDevices
                If vDev.DevIPAddress = Trim(vIP) Then
                    vObjID = vDev.GetObjId
                    Exit For
                End If
            Next

            If vObjID >= 0 Then
                If IsNumeric(vReading) Then
                    If CInt(vReading) >= 0 And CInt(vReading) < 62832 Then
                        ' epidi exi epistrafi i aristerostrofi gonia epilegoume thn deksiostrofi
                        objDevices(vObjID).Preset99Position = 62832 - vReading
                        ApplicationEventLog.WriteEntry("Η κάμερα " & objDevices(vObjID).GetDeviceName & ", επέστρεψε την γωνία Preposition99 με τίμή : " & objDevices(vObjID).Preset99Position, EventLogEntryType.Information)
                        MakeInfoEntry("Η κάμερα " & objDevices(vObjID).GetDeviceName & ", επέστρεψε Preposition99 με τίμή (σε rad) : " & objDevices(vObjID).Preset99Position)
                    Else
                        Throw New Exception("Η γωνία που επέστρεψε η κάμερα " & objDevices(vObjID).GetDeviceName & " δεν είναι μεταξύ 0 και 360 μοιρών.")
                    End If
                Else
                    AxCameo1ReceivedPan = False
                    Throw New Exception("Η γωνία που επέστρεψε η κάμερα " & objDevices(vObjID).GetDeviceName & " είναι λανθασμένη.")
                End If
            Else
                Throw New Exception("Το σύστημα έλαβε απάντηση από μια κάμερα που δεν υπάρχει στην λίστα/αρχείο καμερών. Η IP της κάμερας είναι η : " & vIP)
            End If

        Catch ex As Exception
            ApplicationEventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
            MakeInfoEntry("Σφάλμα κατά την διαδικασία λήψης της γωνίας του PREPOSITION 99 της κάμερας. Το σφάλμα είναι : " & ex.Message)
        End Try
    End Sub

    Private Sub GetPreposition99ForAllConnectedDevices()
        Try
            For Each vDev As cDevices In objDevices
                If vDev.DevIsConnected Then
                    IVAArray(vDev.DevObjID).PTZPreposition99PanGet()
                End If
            Next
        Catch ex As Exception
            ApplicationEventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
            MakeInfoEntry("Σφάλμα κατά την διαδικασία λήψης της γωνίας του PREPOSITION 99. Το σφάλμα είναι : " & ex.Message)
        End Try

    End Sub

    Private Function CalculateFinalCameoReading(ByVal vActualReading As Integer, ByVal v99PreposReading As Integer) As Integer
        Dim vCorrectedAngle As Integer = vActualReading
        ' den xriazonte elegxi gia tis gonies kathos exoun gini se proigoumena vimata
        If v99PreposReading > vActualReading Then
            vCorrectedAngle = 62832 - v99PreposReading + vActualReading
        ElseIf v99PreposReading < vActualReading Then
            vCorrectedAngle = vActualReading - v99PreposReading
            ' se periptosi pou ine akrivos idies apla epistrefoume to arxiko value tis metavlitis ara to vreading
        ElseIf v99PreposReading = vActualReading Then
            vCorrectedAngle = 0
        End If
        Return vCorrectedAngle
    End Function

    Private Sub PositionReceived(ByVal vIP As String, ByVal vReading As String)
        Try
            Dim vPanReading As Integer = -1
            ' MessageBox.Show("vIP: " & vIP & "   vReading: " & vReading)
            If vIP = objDevices(vCam1obj).DevIPAddress Then
                If IsNumeric(vReading) Then
                    If CInt(vReading) >= 0 And CInt(vReading) < 62832 Then

                        ApplicationEventLog.WriteEntry("H " & objDevices(vCam1obj).GetDeviceName & " επέστρεψε γωνία pan : " & vReading, EventLogEntryType.Information)
                        ' anti gia aristerostrofi gonia xrisimopioume tin deksiostrofi
                        vPanReading = 62832 - vReading
                        ' parakato ine i diadikasia meso tis opias diorthonoume to pan reading se sxesi me to preset 99.
                        ' An vgaloume tin parakato sub to sistima tha doulepsi opos douleve setin arxi me ton ipologismo gonias apo home position
                        If objDevices(vCam1obj).Preset99Position >= 0 Then
                            AxCameo1Reading = CalculateFinalCameoReading(vPanReading, objDevices(vCam1obj).Preset99Position)
                            ApplicationEventLog.WriteEntry("H " & objDevices(vCam1obj).GetDeviceName & ", από Home Position γωνία : " & Math.Round(360 * (vPanReading / (10000 * 2 * 3.14)), 2) & " deg ----  Pre 99 γωνία : " & Math.Round(360 * (objDevices(vCam1obj).Preset99Position / (10000 * 2 * 3.14)), 2), EventLogEntryType.Information)
                            ApplicationEventLog.WriteEntry("Η " & objDevices(vCam1obj).GetDeviceName & " είναι γυρισμένη " & Math.Round(360 * (AxCameo1Reading / (10000 * 2 * 3.14)), 2) & " μοίρες σε σχέση με τον βορρά.", EventLogEntryType.Information)
                            MakeInfoEntry("Η " & objDevices(vCam1obj).GetDeviceName & " είναι γυρισμένη " & Math.Round(360 * (AxCameo1Reading / (10000 * 2 * 3.14)), 2) & " μοίρες σε σχέση με τον βορρά.")
                            AxCameo1ReceivedPan = True
                        Else
                            Throw New Exception("Δεν έχει ληφθεί η θέση του prePosition 99 σωστά. Παρακαλώ αποσυνδέθείτε από την κάμερα " & objDevices(vCam1obj).GetDeviceName & " και επανασυνδεθείτε.")
                        End If
                        'AxCameo1Reading = vReading

                    Else
                        AxCameo1ReceivedPan = False
                        Throw New Exception("Η γωνία που επέστρεψε η " & objDevices(vCam1obj).GetDeviceName & " δεν είναι μεταξύ 0 και 360 μοιρών.")
                    End If
                Else
                    AxCameo1ReceivedPan = False
                    Throw New Exception("Η γωνία που επέστρεψε η " & objDevices(vCam1obj).GetDeviceName & " είναι λανθασμένη.")
                End If
            ElseIf vIP = objDevices(vCam2obj).DevIPAddress Then
                If IsNumeric(vReading) Then
                    If CInt(vReading) >= 0 And CInt(vReading) < 62832 Then

                        ApplicationEventLog.WriteEntry("H " & objDevices(vCam2obj).GetDeviceName & " επέστρεψε γωνία pan : " & vReading, EventLogEntryType.Information)
                        ' anti gia aristerostrofi gonia xrisimopioume tin deksiostrofi
                        vPanReading = 62832 - vReading
                        ' parakato ine i diadikasia meso tis opias diorthonoume to pan reading se sxesi me to preset 99.
                        ' An vgaloume tin parakato sub to sistima tha doulepsi opos douleve setin arxi me ton ipologismo gonias apo home position
                        If objDevices(vCam1obj).Preset99Position >= 0 Then
                            AxCameo2Reading = CalculateFinalCameoReading(vPanReading, objDevices(vCam2obj).Preset99Position)
                            ApplicationEventLog.WriteEntry("H " & objDevices(vCam2obj).GetDeviceName & ", από Home Position γωνία : " & Math.Round(360 * (vPanReading / (10000 * 2 * 3.14)), 2) & " deg ---- από Pre 99 γωνία : " & Math.Round(360 * (objDevices(vCam2obj).Preset99Position / (10000 * 2 * 3.14)), 2), EventLogEntryType.Information)
                            ApplicationEventLog.WriteEntry("Η " & objDevices(vCam2obj).GetDeviceName & " είναι γυρισμένη " & Math.Round(360 * (AxCameo2Reading / (10000 * 2 * 3.14)), 2) & " μοίρες σε σχέση με τον βορρά.", EventLogEntryType.Information)
                            MakeInfoEntry("Η " & objDevices(vCam2obj).GetDeviceName & " είναι γυρισμένη " & Math.Round(360 * (AxCameo2Reading / (10000 * 2 * 3.14)), 2) & " μοίρες σε σχέση με τον βορρά.")
                            AxCameo2ReceivedPan = True
                        Else
                            Throw New Exception("Δεν έχει ληφθεί η θέση του prePosition 99 σωστά. Παρακαλώ αποσυνδέθείτε από την κάμερα " & objDevices(vCam2obj).GetDeviceName & " και επανασυνδεθείτε.")
                        End If

                        ' AxCameo2Reading = vReading
                    Else
                        AxCameo2ReceivedPan = False
                        Throw New Exception("Η γωνία που επέστρεψε η " & objDevices(vCam2obj).GetDeviceName & " δεν είναι μεταξύ 0 και 360 μοιρών.")
                    End If
                Else
                    AxCameo2ReceivedPan = False
                    Throw New Exception("Η γωνία που επέστρεψε η " & objDevices(vCam2obj).GetDeviceName & " είναι λανθασμένη.")
                End If
            Else
                Throw New Exception("Η κάμερα που επέστρεψε την γωνία της, δεν είναι πλεον μια εκ των δύο επιλεγμένων από τον χρήστη καμερών.")
            End If

            If AxCameo1ReceivedPan = True AndAlso AxCameo2ReceivedPan = True Then
                tmrWaitForPanAnswer.Stop()
                If IsNumeric(AxCameo1Reading) And IsNumeric(AxCameo2Reading) Then
                    MathematicalCalculationOfX3Y3()
                Else
                    Throw New Exception("Γενικό σφάλμα κατά τον υπολογισμό της γωνίας.")
                End If
                AxCameo1ReceivedPan = False
                AxCameo2ReceivedPan = False
                EnableCalculationSafely()
            End If

        Catch ex As Exception
            ApplicationEventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
            MakeInfoEntry("Σφάλμα κατά την διαδικασία λήψης της γωνίας της κάμερας. Το σφάλμα είναι : " & ex.Message)
            AxCameo1ReceivedPan = False
            AxCameo2ReceivedPan = False
            EnableCalculationSafely()
        End Try
    End Sub

    Private Sub MathematicalCalculationOfX3Y3()

        Dim vAngleTheta1, vAngleTheta2 As Double
        Dim vX1, vX2, vY1, vY2, vX3, vY3 As Double
        Dim vA1, vA2, vB1, vB2 As Double
        Dim vTheta1ok As Boolean = False
        Dim vTheta2ok As Boolean = False
        Dim vStrX3, vStrY3 As String

        ' below angles are in radians
        vAngleTheta1 = AxCameo1Reading / 10000
        vAngleTheta2 = AxCameo2Reading / 10000

        'Initial condition check
        If vAngleTheta1 = vAngleTheta2 Then
            Throw New Exception("Δεν επιτρέπεται οι δύο κάμερες να βρίσκονται στην ακριβώς ίδια γωνία. Δεν θα υπάρξει ποτέ σημείο τομής.")
        End If

        vX1 = objDevices(vCam1obj).LongtitudeValue
        vX2 = objDevices(vCam2obj).LongtitudeValue
        vY1 = objDevices(vCam1obj).LatitudeValue
        vY2 = objDevices(vCam2obj).LatitudeValue

        ' ipologismos ton A1,A2 me vasi arithmitiko paradigma
        vA1 = 1 / Math.Tan(vAngleTheta1)
        vA2 = 1 / Math.Tan(vAngleTheta2)

        ' ipologismos ton B1,B2 me vasi arithmitiko paradigma
        vB1 = vY1 - (vX1 * vA1)
        vB2 = vY2 - (vX2 * vA2)

        ' Ipologismos ton x3 y3 me vasi arithmitiko paradigma

        vX3 = (vB1 - vB2) / (vA2 - vA1)
        vY3 = (vX3 * vA1) + vB1

        ' Elegxos apotelesmatos os pros theta1
        MakeInfoEntry("X1 : " & vX1 & " --- Y1 : " & vY1 & " --- Theta 1 : " & vAngleTheta1)
        MakeInfoEntry("X2 : " & vX2 & " --- Y2 : " & vY2 & " --- Theta 2 : " & vAngleTheta2)
        MakeInfoEntry("X0 : " & vX3 & " --- Y0 : " & vY3)
        ApplicationEventLog.WriteEntry("X1 : " & vX1 & " --- Y1 : " & vY1 & " --- Theta 1 : " & vAngleTheta1, EventLogEntryType.Information)
        ApplicationEventLog.WriteEntry("X2 : " & vX2 & " --- Y2 : " & vY2 & " --- Theta 2 : " & vAngleTheta2, EventLogEntryType.Information)
        ApplicationEventLog.WriteEntry("X0 : " & vX3 & " --- Y0 : " & vY3, EventLogEntryType.Information)


        Select Case vAngleTheta1
            Case Is < 0
                Throw New Exception("Μη αποδεκτή τιμή για την γωνία της " & objDevices(vCam1obj).GetDeviceName & ".")
            Case Is < 1.5708
                If vY3 > vY1 And vX3 >= vX1 Then
                    vTheta1ok = True
                End If
            Case Is < 3.1416
                If vY3 <= vY1 And vX3 > vX1 Then
                    vTheta1ok = True
                End If
            Case Is < 4.7124
                If vY3 < vY1 And vX3 <= vX1 Then
                    vTheta1ok = True
                End If
            Case Is < 6.2832
                If vY3 >= vY1 And vX3 < vX1 Then
                    vTheta1ok = True
                End If
            Case Else
                Throw New Exception("Μη αποδεκτή τιμή για την γωνία της " & objDevices(vCam1obj).GetDeviceName & ".")
        End Select


        ' Elegxos apotelesmatos os pros theta2
        Select Case vAngleTheta2
            Case Is < 0
                Throw New Exception("Μη αποδεκτή τιμή για την γωνία της " & objDevices(vCam2obj).GetDeviceName & ".")
            Case Is < 1.5708
                If vY3 > vY2 And vX3 >= vX2 Then
                    vTheta2ok = True
                End If
            Case Is < 3.1416
                If vY3 <= vY2 And vX3 > vX2 Then
                    vTheta2ok = True
                End If
            Case Is < 4.7124
                If vY3 < vY2 And vX3 <= vX2 Then
                    vTheta2ok = True
                End If
            Case Is < 6.2832
                If vY3 >= vY2 And vX3 < vX2 Then
                    vTheta2ok = True
                End If
            Case Else
                Throw New Exception("Μη αποδεκτή τιμή για την γωνία της " & objDevices(vCam2obj).GetDeviceName & ".")
        End Select

        If vTheta1ok Then
            If vTheta2ok Then
                ' epestrepse tis times ston xristi
                vStrX3 = Replace(Trim(CStr(vX3)), ",", ".")
                vStrY3 = Replace(Trim(CStr(vY3)), ",", ".")
                MakeInfoEntry("Το σημείο τομής των δύο καμερών έχει γεωγραφικό μήκος: " & vStrX3 & " και γεωγραφικό πλάτος : " & vStrY3)
                MessageBox.Show("Το σημείο τομής των δύο καμερών έχει γεωγραφικό μήκος: " & vStrX3 & " και γεωγραφικό πλάτος : " & vStrY3)
            Else
                Throw New Exception("Η " & objDevices(vCam2obj).GetDeviceName & " κοιτάει προς λάθος κατεύθυνση.")
            End If
        Else
            Throw New Exception("Η " & objDevices(vCam1obj).GetDeviceName & " κοιτάει προς λάθος κατεύθυνση.")
        End If

    End Sub

    Private Sub tmrWaitForPanAnswer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrWaitForPanAnswer.Tick
        tmrWaitForPanAnswer.Stop()
        If AxCameo1ReceivedPan = False Then
            If Not IVAArray(vCam1obj) Is Nothing Then
                IVAArray(vCam1obj).DisconnectDevice()
            End If
        End If
        If AxCameo2ReceivedPan = False Then
            If Not IVAArray(vCam2obj) Is Nothing Then
                IVAArray(vCam2obj).DisconnectDevice()
            End If
        End If
        AxCameo1ReceivedPan = False
        AxCameo2ReceivedPan = False
        ApplicationEventLog.WriteEntry("No answer from device for 15 seconds", EventLogEntryType.Error)
        MakeInfoEntry("Σφάλμα κατά την διαδικασία λήψης της γωνίας της κάμερας. Η συσκευή δεν απάντησε. Ξανασυνδεθείτε στην κάμερα που έχει πρόβλημα ή περιμέντε να γίνει η σύνδεση αυτόματα μεσα στα απόμενα 10 λεπτά.")
        cmdCalculatePosition.Enabled = True
    End Sub

    Private Sub cmdMaxCameo1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaxCameo1.Click
        ChangeCameoSizes(CameoInForm.LeftCameo, True)
    End Sub

    Private Sub cmdMaxCameo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaxCameo2.Click
        ChangeCameoSizes(CameoInForm.RightCameo, True)
    End Sub

    Private Sub cmdDualView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDualView.Click
        ChangeCameoSizes(CameoInForm.LeftCameo, False)
    End Sub

    Private Sub SCVideos_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        e.Graphics.DrawLine(Pens.Red, 0, 0, 600, 600)
    End Sub



    '================================= Menu Items ==================================================

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim vFrmAbout As New frmAbout
        vFrmAbout.ShowDialog()
    End Sub

    Private Sub ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ΕκκίνησηΔιακομιστήΣυνδέσεωνToolStripMenuItem.Click
        StartFTAServer()
    End Sub

    Private Sub ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ΣταμάτημαΣυνδέσεωνΚαμερώνToolStripMenuItem.Click
        StopFTAServer()
    End Sub

    ' epilogi error log epiepdou apo menu
    Private Sub AllErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllErrors.Click
        Me.AllErrors.Checked = True
        Me.ErrorsAndInfo.Checked = False
        Me.OnlyErrors.Checked = False
        Me.NoErrorLogging.Checked = False
        ApplicationLogLevel = AppLogLevel.All
    End Sub
    ' epilogi error log epiepdou apo menu
    Private Sub ErrorsAndInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ErrorsAndInfo.Click
        Me.AllErrors.Checked = False
        Me.ErrorsAndInfo.Checked = True
        Me.OnlyErrors.Checked = False
        Me.NoErrorLogging.Checked = False
        ApplicationLogLevel = AppLogLevel.ErrorsAndEvents
    End Sub
    ' epilogi error log epiepdou apo menu
    Private Sub OnlyErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlyErrors.Click
        Me.AllErrors.Checked = False
        Me.ErrorsAndInfo.Checked = False
        Me.OnlyErrors.Checked = True
        Me.NoErrorLogging.Checked = False
        ApplicationLogLevel = AppLogLevel.OnlyErrors
    End Sub
    ' epilogi error log epiepdou apo menu
    Private Sub NoErrorLogging_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoErrorLogging.Click
        Me.AllErrors.Checked = False
        Me.ErrorsAndInfo.Checked = False
        Me.OnlyErrors.Checked = False
        Me.NoErrorLogging.Checked = True
        ApplicationLogLevel = AppLogLevel.None
    End Sub
    ' emfanisi text box me leptomeries efarmoghs apo menu
    Private Sub showOnlineLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showOnlineLogs.Click
        Me.showOnlineLogs.Checked = True
        Me.hideOnlineLogs.Checked = False
        Me.txtInfo.Visible = True
    End Sub
    ' Apokripsi text box me leptomeries efarmoghs apo menu
    Private Sub hideOnlineLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hideOnlineLogs.Click
        Me.showOnlineLogs.Checked = False
        Me.hideOnlineLogs.Checked = True
        Me.txtInfo.Visible = False
    End Sub
    ' Katharisma Online log efarmogis
    Private Sub ClearLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearLogToolStripMenuItem.Click
        txtInfo.Text = ""
    End Sub
    'apo menu alagi license file
    Private Sub SelectOtherLicFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectOtherLicFile.Click
        ChooseLicFile()
    End Sub
    'apo menu alagi xml file
    Private Sub selectOtherXmlFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles selectOtherXmlFile.Click
        ChooseXmlFile()
    End Sub
    'dimiourgia kenourgiou xml file
    Private Sub SelectNewXmlFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectNewXmlFile.Click
        My.Settings.XMLData = ""
        arrCamConfig = Nothing
        ChangeConfigFile()
    End Sub

    Private Sub DisconnectDataStream_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectDataStream.Click
        DisconnectCameo(ActiveCameo)
    End Sub

    Private Sub ClearAxCameo1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAxCameo1.Click
        DisconnectCameo(AxCameo1)
    End Sub

    Private Sub ClearAxCameo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAxCameo2.Click
        DisconnectCameo(AxCameo2)
    End Sub

    Private Sub tvDevices_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvDevices.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If e.Node.Level = 1 Then
                vSelectedTreeIP = e.Node.Name
                TVDevicesMenuStrip.Show()
            End If
        End If
    End Sub

    Private Sub TVConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TVConnect.Click
        Try
            For Each vDev As cDevices In objDevices
                If Trim(vDev.DevIPAddress) = Trim(vSelectedTreeIP) Then
                    ConnectSpecificDevice(vDev.GetObjId)
                    Exit For
                End If
            Next
        Catch ex As Exception
            MakeInfoEntry("Σφάλμα κατά την σύνδεση με την συγκεκριμένη συσκευή.")
        End Try
    End Sub

    ' Preposition 99 Reading
    Private Sub PreposReadingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreposReadingToolStripMenuItem.Click
        Try
            For Each vDev As cDevices In objDevices
                If Trim(vDev.DevIPAddress) = Trim(vSelectedTreeIP) Then
                    If vDev.DevIsConnected Then
                        If Not IVAArray(vDev.DevObjID) Is Nothing Then
                            IVAArray(vDev.DevObjID).PTZPreposition99PanGet()
                        End If
                    End If
                    Exit For
                End If
            Next
        Catch ex As Exception
            MakeInfoEntry("Σφάλμα κατά την διαδικασί λήψης της γωνίας Preposition 99.")
        End Try

    End Sub

    Private Sub TVDisConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TVDisConnect.Click
        Try
            For Each vDev As cDevices In objDevices
                If Trim(vDev.DevIPAddress) = Trim(vSelectedTreeIP) Then
                    If Not IVAArray(vDev.GetObjId) Is Nothing Then
                        IVAArray(vDev.GetObjId).DisconnectDevice()
                    End If
                    Exit For
                End If
            Next
        Catch ex As Exception
            MakeInfoEntry("Σφάλμα κατά την αποσύνδεση από την συγκεκριμένη συσκευή. Παρακαλώ δοκιμάστε αργότερα.")
        End Try
    End Sub

    Private Sub ΥπολογισμόςΘέσηςToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ΥπολογισμόςΘέσηςToolStripMenuItem.Click
        CalculatePosition()
    End Sub

    Private Sub ΒοήθειαToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ΒοήθειαToolStripMenuItem.Click
        'MessageBox.Show(Application.StartupPath & "\FFT.chm")
        System.Windows.Forms.Help.ShowHelp(Me, Application.StartupPath & "\FFT.chm", HelpNavigator.TableOfContents)
    End Sub

    'Calibration Subs
    Private Sub CalibrateDisabledToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalibrateDisabledToolStripMenuItem.Click
        vCalibrateEnabled = False
        CalibrateDisabledToolStripMenuItem.Checked = True
        CalibrateEnabledToolStripMenuItem.Checked = False
        ' CalibrationLines()
    End Sub

    Private Sub CalibrateEnabledToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalibrateEnabledToolStripMenuItem.Click
        vCalibrateEnabled = True
        CalibrateDisabledToolStripMenuItem.Checked = False
        CalibrateEnabledToolStripMenuItem.Checked = True
        'CalibrationLines()
    End Sub

    Private Sub CalibrationLines()
        Dim g As Graphics = Graphics.FromHwnd(SCMain.Panel2.Handle)
        Dim Cameo1x, Cameo2x, Cameosy1, Cameosy2 As Integer
        Cameo1x = Math.Round(AxCameo1.Location.X + AxCameo1.Width / 2, 0)
        Cameo2x = Math.Round(AxCameo2.Location.X + AxCameo2.Width / 2, 0)
        Cameosy1 = Math.Round(AxCameo1.Location.Y - 40, 0)
        Cameosy2 = Math.Round(AxCameo1.Location.Y + AxCameo1.Height + 40, 0)
        If vCalibrateEnabled = True Then
            g.DrawLine(Pens.Magenta, Cameo1x, Cameosy1, Cameo1x, Cameosy2)
            g.DrawLine(Pens.Magenta, Cameo2x, Cameosy1, Cameo2x, Cameosy2)
            AxCameo1.SendToBack()
            AxCameo2.SendToBack()
        Else
            g.DrawLine(Pens.AliceBlue, Cameo1x, Cameosy1, Cameo1x, Cameosy2)
            g.DrawLine(Pens.AliceBlue, Cameo2x, Cameosy1, Cameo2x, Cameosy2)
        End If

    End Sub


    '================================= Finished Menu Items ==================================================

    '===========================   Tree view drag and Drop Handlers ========================='

    Private Sub tvDevices_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles tvDevices.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Public Sub tvDevices_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tvDevices.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Public Sub tvDevices_DragOver(ByVal sender As System.Object, ByVal e As DragEventArgs) Handles tvDevices.DragOver
        e.Effect = DragDropEffects.Move
    End Sub


    Private Sub SCMain_Panel2_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles SCMain.Panel2.DragDrop
        Dim vCursorX As Integer = Cursor.Position.X
        Dim vCursorY As Integer = Cursor.Position.Y
        If vCursorX >= AxCameo1X1 And vCursorX <= AxCameo1X2 And vCursorY >= AxCameo1Y1 And vCursorY <= AxCameo1Y2 Then
            CameoMakeSelection(AxCameo1)
            BuildConnectionToDevice(e.Data.GetData("System.Windows.Forms.TreeNode").Text, 1)
        ElseIf vCursorX >= AxCameo2X1 And vCursorX <= AxCameo2X2 And vCursorY >= AxCameo2Y1 And vCursorY <= AxCameo2Y2 Then
            CameoMakeSelection(AxCameo2)
            BuildConnectionToDevice(e.Data.GetData("System.Windows.Forms.TreeNode").Text, 2)
        End If

    End Sub

    Private Sub SCMain_Panel2_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles SCMain.Panel2.DragEnter
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            e.Effect = DragDropEffects.Move
        End If

    End Sub

    Private Sub SCMain_Panel2_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles SCMain.Panel2.DragOver
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            e.Effect = DragDropEffects.Move
        End If

    End Sub

    '===========================  Finished Tree view drag and Drop Handlers Finished ========================='


    '===========================   Bosch Cameo Functions and Subs ========================='

    Private Sub BuildConnectionObject(ByVal CameraIP As String)
        Try
            CameraConnector = New Bosch.VideoSDK.Device.DeviceConnectorClass()
            CameraConnector.DefaultTimeout = 10000
            CameraConnector.ConnectAsync(CameraIP, "")
        Catch ex As Exception
            MessageBox.Show("Πρόβλημα κατά την σύνδεση με την κάμερα " & CameraIP)
        End Try
    End Sub

    Private Sub ConnectStreamToCameo(ByRef CameraStream As Bosch.VideoSDK.DataStream)
        CameraStream.Multicast = True
        CameraStream.KeyFrameMode = 0
        ActiveCameo.Active = True
        ActiveCameo.EnableOverlay = True
        ActiveCameo.SetVideoStream(CameraStream)
    End Sub

    Private Sub CameraConnector_ConnectResult(ByVal ConnectResult As Bosch.VideoSDK.Device.ConnectResultEnum, ByVal URL As String, ByVal Proxy As Bosch.VideoSDK.Device.DeviceProxy) Handles CameraConnector.ConnectResult
        Try
            If ConnectResult = ConnectResultEnum.creConnected Or ConnectResult = ConnectResultEnum.creInitialized Then
                CameraProxy = Proxy
                If CameraProxy.VideoInputs.Count > 0 Then
                    Select Case ActiveCameo.Name
                        Case "AxCameo1"
                            CameraStreamCameo1 = CameraProxy.VideoInputs(1).Stream
                            CameraControllerCameo1 = CameraProxy.VideoInputs(1).CameraController
                            CameraControllerCameo1.SetController(vCameraModel, "", 1)
                            ConnectStreamToCameo(CameraStreamCameo1)
                            If CameraControllerCameo1.IsControllable Then
                                AxCameo1.EnableInWinPTZ = True
                            Else
                                AxCameo1.EnableInWinPTZ = False
                            End If
                            Cameo1IP = URL
                        Case "AxCameo2"
                            CameraStreamCameo2 = CameraProxy.VideoInputs(1).Stream
                            CameraControllerCameo2 = CameraProxy.VideoInputs(1).CameraController
                            CameraControllerCameo2.SetController(vCameraModel, "", 1)
                            ConnectStreamToCameo(CameraStreamCameo2)
                            If CameraControllerCameo2.IsControllable Then
                                AxCameo1.EnableInWinPTZ = True
                            Else
                                AxCameo1.EnableInWinPTZ = False
                            End If
                            Cameo2IP = URL
                    End Select
                End If
            End If
        Catch ex As Exception
            ApplicationEventLog.WriteEntry(ex.Message, EventLogEntryType.Error)
            MakeInfoEntry("Πρόβλημα κατά την σύνδεση με την συγκεκριμένη κάμερα.")
        End Try
    End Sub

    Private Sub CameoMouseAction(ByVal sender As System.Object, ByVal e As AxCameoLib._ICameoEvents_MouseActionEvent) Handles AxCameo1.MouseAction, AxCameo2.MouseAction
        Select Case e.evt
            Case CAMEOLib.MouseEventEnum.meeLButtonDown
                CameoMakeSelection(TryCast(sender, Bosch.VideoSDK.AxCameoLib.AxCameo))
            Case CAMEOLib.MouseEventEnum.meeRButtonUp
                ' do nothing at this point
            Case CAMEOLib.MouseEventEnum.meeLButtonDblClk
                Dim MYCOUNT As Integer
                MYCOUNT = TryCast(sender, Bosch.VideoSDK.AxCameoLib.AxCameo).Controls.Count
        End Select
    End Sub

    Private Sub CameoMakeSelection(ByVal sender As System.Object)
        If Not ActiveCameo Is Nothing Then
            ActiveCameo.Active = False
            ActiveCameo.EnableOverlay = False
        End If
        ActiveCameo = TryCast(sender, Bosch.VideoSDK.AxCameoLib.AxCameo)
        ActiveCameo.Active = True
        ActiveCameo.EnableOverlay = True
    End Sub

    Private Sub DisconnectCameo(ByRef cameo2Diconnect As Bosch.VideoSDK.AxCameoLib.AxCameo)
        cameo2Diconnect.SetVideoStream(Nothing)
        Select Case cameo2Diconnect.Name
            Case "AxCameo1"
                CameraStreamCameo1 = Nothing
            Case "AxCameo2"
                CameraStreamCameo2 = Nothing
        End Select
    End Sub

    '===========================   Finished Bosch Cameo Functions and Subs ========================='

    ' Parakato mia lista apo calibration subs
    'Private Sub SCVideos_Panel1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If vCalibrateEnabled Then
    '        Dim ms As New System.IO.MemoryStream(My.Resources.Saga3)
    '        Me.Cursor = New Cursor(ms)
    '    End If
    'End Sub

    'Private Sub SCVideos_Panel1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If vCalibrateEnabled Then
    '        Me.Cursor = Cursors.Arrow
    '    End If
    'End Sub

    'Private Sub SCMain_Panel2_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SCMain.Panel2.MouseEnter
    '    If vCalibrateEnabled Then
    '        Dim ms As New System.IO.MemoryStream(My.Resources.Saga3)
    '        Me.Cursor = New Cursor(ms)
    '    End If

    'End Sub

    'Private Sub SCMain_Panel2_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SCMain.Panel2.MouseLeave
    '    If vCalibrateEnabled Then
    '        Me.Cursor = Cursors.Arrow
    '    End If
    'End Sub


End Class


