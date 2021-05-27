'Imports System
'Imports System.Net
'Imports System.Xml
'Imports System.Threading
'Imports AutoDwxRuntimeLib

'Public Class frmMain

'    Private Enum DialogType As Integer
'        DataWorXConfig = 0
'        LicenseFile = 1
'        RegMacFile = 2
'    End Enum

'    Private Enum ReplyPacketType As Integer
'        PacketNotRelevant = -1
'        AutodetectDevice = 0
'        VCA_Alarm = 1
'        TimeoutWarning = 2
'        Reply = 3
'    End Enum

'    Private Enum AppLogLevel As Integer
'        None = 0
'        OnlyErrors = 1
'        ErrorsAndEvents = 2
'        All = 4
'    End Enum

'    Private Enum MessageType As Integer
'        UDP_Message = 0
'        TCP_Message = 1
'    End Enum

'    Private Enum FileType As Integer
'        DataWorX = 0
'        License = 1
'        RegMac = 2
'    End Enum

'    Private Enum ProjectType As Integer
'        VCA = 1
'        LPR = 2
'    End Enum

'    Private Enum AlarmFlagsMask As Integer
'        MOTION = 32768                  ' Bit01 - motion flag
'        GLOBAL_CHANGE = 16384           ' Bit02 - global change flag
'        SIGNAL_BRIGHT = 8192            ' Bit03 - signal too bright flag
'        SIGNAL_DARK = 4096              ' Bit04 - signal too dark flag
'        SIGNAL_NOISY = 2048             ' Bit05 - signal too noisy flag
'        IMG_BLURRY = 1024               ' Bit06 - image too blurry flag
'        SIGNAL_LOSS = 512               ' Bit07 - signal loss flag
'        REF_IMG_CHK = 256               ' Bit08 - reference image check failed flag
'        INV_CONF_FLAG = 128             ' Bit09 - invalid configuration flag
'        RETURN_NORMAL = 0               ' ALARM TO NORMAL
'    End Enum

'    Private Enum IRegisterTypes As Integer
'        MOTION = 0                  ' Bit01 - motion flag
'        GLOBAL_CHANGE = 1           ' Bit02 - global change flag
'        SIGNAL_BRIGHT = 2            ' Bit03 - signal too bright flag
'        SIGNAL_DARK = 3              ' Bit04 - signal too dark flag
'        SIGNAL_NOISY = 4             ' Bit05 - signal too noisy flag
'        IMG_BLURRY = 5               ' Bit06 - image too blurry flag
'        SIGNAL_LOSS = 6               ' Bit07 - signal loss flag
'        REF_IMG_CHK = 7               ' Bit08 - reference image check failed flag
'        INV_CONF_FLAG = 8             ' Bit09 - invalid configuration flag
'    End Enum

'    Dim Doc2Process As New XmlDocument()
'    Dim m_UDPPORT As Integer = 1757
'    Dim m_UDPREPLYPORT As Int16
'    Dim m_SequenceNumber As Integer
'    Private WithEvents objSocket As cSocket
'    Private m_DeviceMACAddress As String
'    Private m_DeviceIPAddress As String
'    Private Shared connectDone As New ManualResetEvent(False)
'    Private m_ClientID As Short
'    Private objDevices() As cDevices
'    Private objRegisters(,) As IRegister
'    Dim arrMacReg As String(,)
'    Private WithEvents objtest As IVAConnect.cIVA
'    Dim objRegister As DwxRuntime
'    Dim IVAArray() As IVAConnect.cIVA
'    'Dim TesterArray()
'    'Dim MyThread As New Thread(AddressOf CreateConnectionToDevices)
'    Dim MyThread As Thread
'    Dim vServerLogLevel As AppLogLevel
'    Dim vDllsLogLevel As AppLogLevel
'    Dim vArrOfIPs(0) As String
'    Dim tvImageList As New ImageList()
'    Dim VIDOSSocket As VirtualTCPSocketClient
'    Dim VIDOSAddress As IPAddress
'    Dim vVIDOSMonitor As String

'    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        'TODO: This line of code loads data into the 'Moreas.tblAlarms' table. You can move, or remove it, as needed.
'        Me.TblAlarmsTableAdapter.Fill(Me.Moreas.tblAlarms)
'        ' Set up connection to the event logger
'        ApplicationEventLog = New cApplicationLog("IVA")
'        ApplicationEventLog.WriteEntry("DataWorx - IVA connectivity is starting.", EventLogEntryType.Information)
'        objRegister = New DwxRuntime
'        ' Check existance of DWX xml file
'        If My.Settings.DWXConfig = String.Empty Then
'            ApplicationEventLog.WriteEntry("No DataWorX configuration file specified.", EventLogEntryType.Warning)
'            Dim DWXConfigDialog As DialogResult
'            DWXConfigDialog = MessageBox.Show("Do you want to specify a DataWorX configuration file?", _
'                                            "No DataWorX configuration file specified.", MessageBoxButtons.YesNo, _
'                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification)
'            If DWXConfigDialog = Windows.Forms.DialogResult.Yes Then
'                If Not ShowFileOpenDialog(DialogType.DataWorXConfig) Then
'                    MessageBox.Show("Application is exiting. No IVA connectivity has been established")
'                    ApplicationEventLog.WriteEntry("User has not specified a DataWorX configuration file. Application exiting", EventLogEntryType.Error)
'                    Application.Exit()
'                End If
'            Else
'                MessageBox.Show("Application is exiting. No IVA connectivity has been established")
'                ApplicationEventLog.WriteEntry("User has not specified a DataWorX configuration file. Application exiting", EventLogEntryType.Error)
'                Application.Exit()
'            End If
'        Else
'            txtDWXConfig.Text = My.Settings.DWXConfig
'            ApplicationEventLog.WriteEntry("Using " & My.Settings.DWXConfig & " as DataWorX configuration file", _
'                                           EventLogEntryType.Information)
'            ' Examine XML file contents and verify correct structure
'        End If

'        ' Check existance of Licence Fiile
'        If My.Settings.LicenseFile = String.Empty Then
'            ApplicationEventLog.WriteEntry("No License file specified.", EventLogEntryType.Warning)
'            Dim LicDialog As DialogResult
'            LicDialog = MessageBox.Show("Do you want to specify a valid Licence file?", "No License file specified", _
'                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
'            If LicDialog = Windows.Forms.DialogResult.Yes Then
'                If Not ShowFileOpenDialog(DialogType.LicenseFile) Then
'                    MessageBox.Show("Application is exiting. No IVA connectivity has been established")
'                    ApplicationEventLog.WriteEntry("User has not specified a License file. Application exiting", EventLogEntryType.Error)
'                    Application.Exit()
'                Else
'                    txtLicense.Text = My.Settings.LicenseFile
'                    ProcessFile(FileType.License)
'                End If
'            Else
'                MessageBox.Show("Application is exiting. No IVA connectivity has been established")
'                ApplicationEventLog.WriteEntry("User has not specified a License file. Application exiting", EventLogEntryType.Error)
'                Application.Exit()
'            End If
'        Else
'            txtLicense.Text = My.Settings.LicenseFile
'            ProcessFile(FileType.License)
'        End If

'        ' Check existance of Register - Mac Xml Connection File
'        If My.Settings.RegDev = String.Empty Then
'            ApplicationEventLog.WriteEntry("No register Mac Connection file specified.", EventLogEntryType.Warning)
'            Dim RegMacDialog As DialogResult
'            RegMacDialog = MessageBox.Show("Do you want to specify an existing Register-Mac Interconnection file?", "No Register-Mac Interconnection file specified", _
'                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
'            If RegMacDialog = Windows.Forms.DialogResult.Yes Then
'                If Not ShowFileOpenDialog(DialogType.RegMacFile) Then
'                    MessageBox.Show("Application is exiting. No Reg Mac interconnection has been established")
'                    ApplicationEventLog.WriteEntry("User has not specified a Reg Mac interconnection. Application exiting", EventLogEntryType.Error)
'                    Application.Exit()
'                Else
'                    txtMacReg.Text = My.Settings.RegDev
'                    ProcessFile(FileType.RegMac)
'                End If
'            Else
'                If MessageBox.Show("Do you want to create a new Register Mac InterConnection File?", "New Reg Mac File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
'                    Dim frmInterconn As New frmInterConnect
'                    Me.Hide()
'                    frmInterconn.Show()
'                Else
'                    MessageBox.Show("Application is exiting. No Reg Mac interconnection has been established.")
'                    ApplicationEventLog.WriteEntry("User has not specified a Reg Mac interconnection. Application exiting", EventLogEntryType.Error)
'                    Application.Exit()
'                End If
'            End If
'        Else
'            txtMacReg.Text = My.Settings.RegDev
'            ProcessFile(FileType.RegMac)
'        End If
'        vServerLogLevel = AppLogLevel.OnlyErrors
'        vServerLogLevel = AppLogLevel.OnlyErrors
'        Me.cbxServerLog.SelectedIndex = 1
'        Me.cbxDllLog.SelectedIndex = 1
'        CreateImageList()
'        tvDevices.ImageList = tvImageList
'        cbxVidosMonitor.Text = My.Settings.VidosMonitor
'        vVIDOSMonitor = My.Settings.VidosMonitor
'        cbxVidosMonitor.Enabled = False
'    End Sub

'    Private Sub CreateImageList()
'        tvImageList.Images.Add("1", My.Resources.Connection)
'        tvImageList.Images.Add("2", My.Resources.NOConnection)
'        tvImageList.Images.Add("3", My.Resources.encoder)
'        tvImageList.Images.Add("4", My.Resources.start)
'    End Sub

'    Private Sub btnDWXConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDWXConfig.Click
'        If Not ShowFileOpenDialog(DialogType.DataWorXConfig) Then
'            MessageBox.Show("Application is exiting. No IVA connectivity has been established")
'            If vServerLogLevel >= EventLogEntryType.Error Then
'                ApplicationEventLog.WriteEntry("User has not specified a DataWorX configuration file. Application exiting", EventLogEntryType.Error)
'            End If
'            Application.Exit()
'        End If
'    End Sub

'    Private Sub cmdMacReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMacReg.Click
'        If Not ShowFileOpenDialog(DialogType.RegMacFile) Then
'            MessageBox.Show("Application is exiting. No IVA Reg - Mac connectivity has been established")
'            If vServerLogLevel >= EventLogEntryType.Error Then
'                ApplicationEventLog.WriteEntry("User has not specified a Reg - Mac configuration file. Application exiting", EventLogEntryType.Error)
'            End If
'            Application.Exit()
'        End If
'    End Sub

'    Private Function ShowFileOpenDialog(ByVal FileDialogType As DialogType) As Boolean
'        Dim UserFileDialog As New OpenFileDialog

'        With UserFileDialog
'            Select Case FileDialogType
'                Case DialogType.DataWorXConfig
'                    .Filter = "DWX Config files|*.xml"
'                    .InitialDirectory = "c:\virtualcontrols"
'                    .Title = "Open DataWorX configuration file"
'                    If .ShowDialog() = Windows.Forms.DialogResult.OK Then
'                        txtDWXConfig.Text = .FileName
'                        ApplicationEventLog.WriteEntry(.FileName & " is selected as DataWorX Configuration file", _
'                                                           EventLogEntryType.Information)
'                        My.Settings.DWXConfig = .FileName
'                        Return True
'                    Else
'                        If My.Settings.DWXConfig = String.Empty Then
'                            Return False
'                        Else
'                            Return True
'                        End If
'                    End If
'                Case DialogType.RegMacFile
'                    .Filter = "Reg - Mac Config files|*.xml"
'                    .InitialDirectory = "c:\virtualcontrols"
'                    .Title = "Open Reg - Mac configuration file"
'                    If .ShowDialog() = Windows.Forms.DialogResult.OK Then
'                        txtMacReg.Text = .FileName
'                        ApplicationEventLog.WriteEntry(.FileName & " is selected as Reg - Mac Configuration file", _
'                                                       EventLogEntryType.Information)
'                        My.Settings.RegDev = .FileName
'                        Return True
'                    Else
'                        If My.Settings.RegDev = String.Empty Then
'                            Return False
'                        Else
'                            Return True
'                        End If
'                    End If
'                Case DialogType.LicenseFile
'                    .Filter = "License files|*.lic"
'                    .InitialDirectory = "c:\virtualcontrols"
'                    .Title = "Open Licence file"
'                    If .ShowDialog() = Windows.Forms.DialogResult.OK Then
'                        txtLicense.Text = .FileName
'                        ApplicationEventLog.WriteEntry(.FileName & " is selected as License file", _
'                                                       EventLogEntryType.Information)
'                        My.Settings.LicenseFile = .FileName
'                        Return True
'                    Else
'                        If My.Settings.LicenseFile = String.Empty Then
'                            Return False
'                        Else
'                            Return True
'                        End If
'                    End If
'            End Select
'        End With
'    End Function

'    Private Sub btnLicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLicense.Click
'        If Not ShowFileOpenDialog(DialogType.LicenseFile) Then
'            MessageBox.Show("Application is exiting. No IVA connectivity has been established")
'            If vServerLogLevel >= EventLogEntryType.Error Then
'                ApplicationEventLog.WriteEntry("User has not specified a License file. Application exiting", EventLogEntryType.Error)
'            End If
'            Application.Exit()
'        End If
'    End Sub

'    Private Sub PopulateDeviceTree(ByVal ProjectTypeNode As ProjectType)
'        With tvDevices
'            Select Case ProjectTypeNode
'                Case ProjectType.VCA
'                    .Nodes.Add("VCA", "VCA Devices")
'                Case ProjectType.LPR
'                    .Nodes.Add("LPR", "LPR Devices")
'            End Select
'        End With
'    End Sub

'    Private Sub tvAddNode(ByVal vIP As String, ByVal vChannel As Integer, ByVal vImgkey As String)
'        tvDevices.Nodes("VCA").Nodes(vIP).Nodes.Add(vChannel, "Channel " & vChannel)
'        tvDevices.Nodes("VCA").Nodes(vIP).Nodes(CStr(vChannel)).ImageKey = vImgkey
'        tvDevices.Nodes("VCA").Nodes(vIP).Nodes(CStr(vChannel)).SelectedImageKey = vImgkey
'    End Sub

'    Private Sub MakeEntries(ByVal Description As String)
'        UpdateDetailsSafely(Description)
'        If vServerLogLevel >= EventLogEntryType.Warning Then
'            ApplicationEventLog.WriteEntry(Description, EventLogEntryType.Information)
'        End If
'    End Sub

'    Private Sub ProcessFile(ByVal CurrentFileType As FileType)
'        Select Case CurrentFileType
'            Case FileType.License
'                ' process license file
'            Case FileType.DataWorX
'                ' process Dataworx file
'            Case FileType.RegMac
'                ' process Reg-Mac file
'                GetRelationRegMacChannel()
'        End Select
'    End Sub

'    Private Function AutodetectRequest(ByVal ReplyPort As Int16) As Byte()
'        Dim RequestPacket(11) As Byte

'        Dim rand As New Random
'        m_SequenceNumber = rand.Next(30000)

'        RequestPacket(0) = 153      ' 0x99
'        RequestPacket(1) = 57       ' 0x39
'        RequestPacket(2) = 164      ' 0xA4
'        RequestPacket(3) = 39       ' 0x27
'        If BitConverter.IsLittleEndian Then
'            BitConverter.GetBytes(Net.IPAddress.HostToNetworkOrder(m_SequenceNumber)).CopyTo(RequestPacket, 4)        ' This sequence number must be a randomized number.
'        Else
'            BitConverter.GetBytes(m_SequenceNumber).CopyTo(RequestPacket, 4)        ' This sequence number must be a randomized number.
'        End If
'        RequestPacket(8) = 0        ' 0x00
'        RequestPacket(9) = 0        ' 0x00
'        If BitConverter.IsLittleEndian Then
'            BitConverter.GetBytes(Net.IPAddress.HostToNetworkOrder(ReplyPort)).CopyTo(RequestPacket, 10)            ' Specifies the reply UDP port
'        Else
'            BitConverter.GetBytes(ReplyPort).CopyTo(RequestPacket, 10)              ' Specifies the reply UDP port
'        End If
'        Return RequestPacket
'    End Function

'    Delegate Sub SetTextCallback(ByVal [text] As String)

'    Private Sub SetText(ByVal [text] As String)
'        Me.txtDetails.Text = [text] & Me.txtDetails.Text
'    End Sub

'    ' If the calling thread is different from the thread that
'    ' created the TextBox control, this method passes in the
'    ' the SetText method to the SetTextCallback delegate and 
'    ' passes in the delegate to the Invoke method.

'    Friend Sub UpdateDetailsSafely(ByRef outputString As String)

'        Dim NewText As String = outputString

'        ' Check if this method is running on a different thread
'        ' than the thread that created the control.
'        If Me.txtDetails.InvokeRequired Then
'            ' It's on a different thread, so use Invoke.
'            Dim d As New SetTextCallback(AddressOf SetText)
'            Me.Invoke(d, New Object() {[NewText] + vbCrLf})
'        Else
'            ' It's on the same thread, no need for Invoke.
'            Me.txtDetails.Text = [NewText] + vbCrLf & Me.txtDetails.Text
'            If vServerLogLevel >= EventLogEntryType.Information Then
'                ApplicationEventLog.WriteEntry([NewText], EventLogEntryType.Information)
'            End If
'        End If
'    End Sub

'    ' This method is passed in to the SetTextCallBack delegate
'    ' to set the Text property of textBox1.

'    Delegate Sub SetImgCallback(ByVal [text] As String, ByVal [vIP] As String)

'    Private Sub SetTVImg(ByVal [text] As String, ByVal [vIP] As String)
'        tvDevices.Nodes("VCA").Nodes([vIP]).ImageKey = [text]
'        tvDevices.Nodes("VCA").Nodes([vIP]).SelectedImageKey = [text]
'    End Sub

'    ' If the calling thread is different from the thread that
'    ' created the TextBox control, this method passes in the
'    ' the SetText method to the SetTextCallback delegate and 
'    ' passes in the delegate to the Invoke method.

'    Private Sub UpdateIMGDetailsSafely(ByRef outputString As String, ByVal OutPutIP As String)

'        Dim NewText As String = outputString
'        Dim vIP As String = OutPutIP
'        ' Check if this method is running on a different thread
'        ' than the thread that created the control.
'        If tvDevices.InvokeRequired Then
'            ' It's on a different thread, so use Invoke.
'            Dim d As New SetImgCallback(AddressOf SetTVImg)
'            Me.Invoke(d, New Object() {[NewText], [vIP]})
'        Else
'            ' It's on the same thread, no need for Invoke.
'            tvDevices.Nodes("VCA").Nodes([vIP]).ImageKey = [NewText]
'            tvDevices.Nodes("VCA").Nodes([vIP]).SelectedImageKey = [NewText]
'            If vServerLogLevel >= EventLogEntryType.Information Then
'                ApplicationEventLog.WriteEntry([NewText], EventLogEntryType.Information)
'            End If
'        End If
'    End Sub

'    Private Sub StartIvaServer()
'        Try
'            MyThread = New Thread(AddressOf CreateConnectionToDevices)
'            If Not arrMacReg Is Nothing Then
'                PopulateDeviceTree(ProjectType.VCA)
'                CreateTVDevices()
'                For i = 0 To arrMacReg.GetLength(0) - 1
'                    AddDevice(arrMacReg(i, 0), arrMacReg(i, 1), Integer.Parse(arrMacReg(i, 3)), arrMacReg(i, 2), i)
'                    tvAddNode(arrMacReg(i, 1), Integer.Parse(arrMacReg(i, 3)), "3")
'                    ReDim Preserve objRegisters(8, i)
'                    Try
'                        objRegisters(IRegisterTypes.MOTION, i) = objRegister.GetRegister(objDevices(i).AlarmIregMotionDetection)
'                        objRegisters(IRegisterTypes.GLOBAL_CHANGE, i) = objRegister.GetRegister(objDevices(i).AlarmIregGlobalChange)
'                        objRegisters(IRegisterTypes.IMG_BLURRY, i) = objRegister.GetRegister(objDevices(i).AlarmIregImgBlurry)
'                        objRegisters(IRegisterTypes.INV_CONF_FLAG, i) = objRegister.GetRegister(objDevices(i).AlarmIregCfgFlag)
'                        objRegisters(IRegisterTypes.REF_IMG_CHK, i) = objRegister.GetRegister(objDevices(i).AlarmIregRefImgChk)
'                        objRegisters(IRegisterTypes.SIGNAL_BRIGHT, i) = objRegister.GetRegister(objDevices(i).AlarmIregSignalBright)
'                        objRegisters(IRegisterTypes.SIGNAL_DARK, i) = objRegister.GetRegister(objDevices(i).AlarmIregSignalDark)
'                        objRegisters(IRegisterTypes.SIGNAL_LOSS, i) = objRegister.GetRegister(objDevices(i).AlarmIregVideoLoss)
'                        objRegisters(IRegisterTypes.SIGNAL_NOISY, i) = objRegister.GetRegister(objDevices(i).AlarmIregSignalNoisy)
'                    Catch ex As Exception
'                        MakeEntries("Invalid Register Declared in XML File : " & arrMacReg(i, 2))
'                        ' dikse sto tree view to device disconnected
'                    End Try
'                Next
'            Else
'                Throw New Exception("No Valid XML File Specified")
'            End If
'            MyThread.Start()
'        Catch ex As Exception
'            MakeEntries(ex.Message)
'        End Try
'    End Sub

'    Private Sub CreateArrOfIPs()
'        Dim vExists As Boolean
'        Dim vIP As String = ""
'        Try
'            If arrMacReg Is Nothing Then
'                Throw New Exception("No Data in the Mac - Reg interconnection file.")
'            End If
'            vArrOfIPs(0) = arrMacReg(0, 1)
'            If arrMacReg.Length > 1 Then
'                For i = 1 To arrMacReg.GetLength(0) - 1
'                    vExists = False
'                    vIP = Trim(arrMacReg(i, 1))
'                    For j = 0 To vArrOfIPs.Length - 1
'                        If vArrOfIPs(j) = arrMacReg(i, 1) Then
'                            vExists = True
'                        End If
'                    Next
'                    If vExists = False Then
'                        If vIP <> "" Then
'                            ReDim Preserve vArrOfIPs(vArrOfIPs.Length)
'                            vArrOfIPs(vArrOfIPs.Length - 1) = vIP
'                        End If
'                    End If
'                    vExists = True
'                Next
'            End If
'        Catch ex As Exception
'            MakeEntries(ex.Message)
'        End Try
'    End Sub

'    Private Sub CreateTVDevices()
'        For k = 0 To vArrOfIPs.Length - 1
'            tvDevices.Nodes("VCA").Nodes.Add(vArrOfIPs(k), vArrOfIPs(k))
'            tvDevices.Nodes("VCA").Nodes(vArrOfIPs(k)).ImageKey = "1"
'            tvDevices.Nodes("VCA").Nodes(vArrOfIPs(k)).SelectedImageKey = "1"
'        Next
'    End Sub

'    Private Sub CreateConnectionToDevices()
'        Try
'            ' create all the iva dll objects
'            For k = 0 To vArrOfIPs.Length - 1
'                Try
'                    Dim vCounter As Integer = 0
'                    CreateIvaObject(k, vArrOfIPs(k))
'                Catch ex As Exception
'                    MakeEntries("Problem during Connection to device " & vArrOfIPs(k))
'                    ' dikse sto tree view to device disconnected
'                End Try
'            Next
'            ' create all the connections to the devices
'            For i = 0 To IVAArray.Length - 1
'                IVAArray(i).ConnectDevice(My.Settings.LicenseFile, vArrOfIPs(i))
'            Next
'            UpdateDetailsSafely("Finished Creating Connections to all the devices")
'            'MessageBox.Show("Finished Creating Connections")
'            ' update the tv so it shows correctly the connection statuses
'            UpdateTVWithConnections()
'            ' create all the handlers for the alarms received from the connections
'            For i = 0 To IVAArray.Length - 1
'                AddHandler IVAArray(i).AlarmReceived, AddressOf IVA_AlarmReceived
'            Next
'            UpdateDetailsSafely("Finished preparing handlers")
'            'MessageBox.Show("Finished adding handlers")
'            ' Create Connection To Vidos
'            ConnectVIDOS()
'        Catch ex As Exception
'            MakeEntries(ex.Message)
'        End Try
'    End Sub

'    Private Sub AutodiscoverDevices()
'        Try
'            ' Broadcast and auto detect devices
'            Dim rand As New Random
'            objSocket = New cSocket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Dgram, Net.Sockets.ProtocolType.Udp)
'            m_UDPREPLYPORT = rand.Next(m_UDPPORT, Short.MaxValue)
'            objSocket.BindLocalInterface(m_UDPREPLYPORT)
'            objSocket.ConnectRemoteInterface(m_UDPPORT)
'            objSocket.SendUDP_Packet(AutodetectRequest(m_UDPREPLYPORT))
'        Catch ex As Exception
'            MessageBox.Show(ex.Message)
'        End Try
'    End Sub

'    Private Sub AddDevice(ByVal vMac As String, ByVal vIP As String, ByVal vChannelId As Integer, ByVal vregister As String, ByVal objId As Integer)
'        Dim vDeviceExists As Boolean = False
'        ' search if the device already exists 
'        If objDevices Is Nothing Then
'            ReDim objDevices(0)
'            objDevices(0) = New cDevices(vMac, vIP, vChannelId, vregister, objId)
'            vDeviceExists = True
'            UpdateDetailsSafely("Created Device with IP : " & vIP & " Mac Address :" & vMac & " Channel:" & vChannelId)
'        Else
'            For Each devObject In objDevices
'                If devObject.DevIPAddress = vIP And devObject.DevMacAddress = vMac And devObject.ChannelID = vChannelId Then
'                    vDeviceExists = True
'                    'do nothing at the moment
'                End If
'            Next
'        End If

'        ' if it does not exist vreate device object
'        If vDeviceExists = False Then
'            If objDevices.Length = Nothing Then
'                Throw New Exception("frmMain / AddDevice : Unexpected Failure for object Array Length")
'            Else
'                ReDim Preserve objDevices(objDevices.Length)
'                objDevices(objDevices.Length - 1) = New cDevices(vMac, vIP, vChannelId, vregister, objId)
'                UpdateDetailsSafely("Created Device with IP : " & vIP & " Mac Address :" & vMac & " Channel:" & vChannelId)
'            End If
'        End If
'    End Sub

'    Private Sub m_UDPClient_DatagramReceived(ByRef Message() As Byte) Handles objSocket.DatagramReceived
'        Dim ReplyType As ReplyPacketType
'        Dim vForUpdate As Boolean = True
'        Try
'            ' Analyze header of reply message
'            ReplyType = AnalyzeMessageHeader(Message, MessageType.UDP_Message)
'            Select Case ReplyType
'                ' Incoming message is response to device autodetect request
'                Case ReplyPacketType.AutodetectDevice
'                    ' Retrieve device's MAC address
'                    m_DeviceMACAddress = ByteToHexString(Message(8)) & ByteToHexString(Message(9)) & _
'                                        ByteToHexString(Message(10)) & ByteToHexString(Message(11)) & _
'                                        ByteToHexString(Message(12)) & ByteToHexString(Message(13))
'                    ' Retrieve device's IP address
'                    m_DeviceIPAddress = Message(16).ToString & "." & Message(17).ToString & "." & _
'                                        Message(18).ToString & "." & Message(19).ToString
'                    'm_DeviceConnected = True
'            End Select
'            ' Signal that the connection has been made.
'            connectDone.Set()
'        Catch ex As Exception
'            If vServerLogLevel >= EventLogEntryType.Error Then
'                ApplicationEventLog.WriteEntry("frmMain : " & ex.Message, EventLogEntryType.Error)
'            End If
'        End Try
'    End Sub

'    Private Function AnalyzeMessageHeader(ByRef ReceivedMessage() As Byte, ByVal CurrentMessageType As MessageType) As ReplyPacketType
'        Dim SeqNum(3) As Byte
'        Select Case CurrentMessageType
'            Case MessageType.UDP_Message
'                ' Check if reply to autodetect device message
'                If ReceivedMessage(0) = 153 AndAlso ReceivedMessage(1) = 57 AndAlso ReceivedMessage(2) = 164 AndAlso ReceivedMessage(3) = 39 Then
'                    ' Verify that message received contains the correct sequence number
'                    SeqNum = BitConverter.GetBytes(Net.IPAddress.NetworkToHostOrder(m_SequenceNumber))
'                    If SeqNum(0) = ReceivedMessage(4) AndAlso _
'                    SeqNum(1) = ReceivedMessage(5) AndAlso _
'                    SeqNum(2) = ReceivedMessage(6) AndAlso _
'                    SeqNum(3) = ReceivedMessage(7) Then
'                        AnalyzeMessageHeader = ReplyPacketType.AutodetectDevice
'                    Else
'                        AnalyzeMessageHeader = ReplyPacketType.PacketNotRelevant
'                    End If
'                Else
'                    AnalyzeMessageHeader = ReplyPacketType.PacketNotRelevant
'                End If
'            Case MessageType.TCP_Message
'                'do nothing
'        End Select
'    End Function

'    Private Sub DisconnectDevices()
'        If Not IVAArray Is Nothing Then
'            For Each device As IVAConnect.cIVA In IVAArray
'                device.DisconnectDevice()
'            Next
'        End If
'    End Sub

'    Private Sub CloseApplication()
'        Dim myAnswer As DialogResult
'        myAnswer = MessageBox.Show("If you exit now all alarms produced by devices will not be saved to the Database. Do you still want to Exit?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Hand)
'        If myAnswer = Windows.Forms.DialogResult.Yes Then
'            ApplicationEventLog.WriteEntry("IVA Server was Shut Down by the user at : " & Now.ToString, EventLogEntryType.Information)
'            DisconnectDevices()
'            If Not MyThread Is Nothing Then
'                MyThread.Abort()
'            End If
'            Application.Exit()
'        End If
'    End Sub

'    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
'        CloseApplication()
'    End Sub

'    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
'        Dim aboutForm As frmAbout = New frmAbout
'        aboutForm.ShowDialog()
'    End Sub

'    Private Sub GetRelationRegMacChannel()
'        If Not My.Settings.RegDev = String.Empty Then
'            arrMacReg = ReadIPMacDataFromXML(My.Settings.RegDev)
'            MakeEntries("Registers - Mac - Channel XML has been Loaded Successfully")
'            ReDim vArrOfIPs(0)
'            CreateArrOfIPs()
'        End If
'    End Sub

'    Private Sub LoadInterConnectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadInterConnectionToolStripMenuItem.Click
'        GetRelationRegMacChannel()
'    End Sub

'    'Private Sub CreateIvaObject(ByVal objID As Integer, ByVal vIP As String)
'    '    ReDim Preserve IVAArray(objID)
'    '    IVAArray(objID) = New IVAConnect.cIVA
'    '    'IVAArray(objID).SetDebugLevel(vDllsLogLevel)
'    '    IVAArray(objID).SetDebugLevel(AppLogLevel.All)
'    '    AddHandler IVAArray(objID).AlarmReceived, AddressOf IVA_AlarmReceived
'    '    IVAArray(objID).ConnectDevice(My.Settings.LicenseFile, vIP)
'    'End Sub

'    Private Sub CreateIvaObject(ByVal objID As Integer, ByVal vIP As String)
'        ReDim Preserve IVAArray(objID)
'        IVAArray(objID) = New IVAConnect.cIVA
'        IVAArray(objID).SetDebugLevel(vDllsLogLevel)
'    End Sub

'    Private Function MaskVCA(ByVal AlarmMask As UInt32) As AlarmFlagsMask
'        Return (AlarmMask And 4294901760) >> 16 ' Bitmasking with 0xFFFF 0000 and right shift 16 bits
'    End Function

'    Private Sub IVA_AlarmReceived_Initial(ByVal Mac As String, ByVal channel As Integer, ByVal Alarm As UInt32) 'Handles Tester.AlarmReceived
'        Try
'            Select Case MaskVCA(Alarm)
'                Case AlarmFlagsMask.MOTION
'                    Dim testBin As String
'                    testBin = Convert.ToString(Alarm And 65535, 2)
'                    If testBin.Length > 16 Then
'                        Throw New Exception("Packet Failure")
'                    End If
'                    While testBin.Length < 16
'                        testBin = "0" & testBin
'                    End While
'                    For Each vDev As cDevices In objDevices
'                        If LCase(vDev.DevMacAddress) = Mac And vDev.ChannelID = channel Then
'                            If Mid(testBin, 1, 1) = "1" Then
'                                vDev.AlarmSplitObject = True
'                                vDev.AlarmSplitObjectTime = Now()
'                                If Mid(testBin, 2, 15) <> "000000000000000" Then
'                                    objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Value = Alarm And 65535
'                                    Me.TblAlarmsTableAdapter.Insert(Mac, Now(), testBin, channel)
'                                    If vServerLogLevel >= EventLogEntryType.Warning Then
'                                        ApplicationEventLog.WriteEntry(Now.ToString & " > MAC : " & Mac & " Channel : " & channel.ToString & " Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                                        UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & " Alarm mask : " & Alarm.ToString & " - > " & testBin & vbCrLf)
'                                    End If
'                                End If
'                                Exit Sub
'                            End If
'                            If Mid(testBin, testBin.Length - 1, 1) = "1" Or Mid(testBin, testBin.Length - 2, 1) = "1" Then
'                                If Math.Abs(DateDiff(DateInterval.Second, Now, vDev.AlarmSplitObjectTime)) < 10 Then
'                                    If vDev.AlarmSplitObject = False Then
'                                        objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Value = Alarm And 65535
'                                        Me.TblAlarmsTableAdapter.Insert(Mac, Now(), testBin, channel)
'                                        If vServerLogLevel >= EventLogEntryType.Warning Then
'                                            ApplicationEventLog.WriteEntry(Now.ToString & " > MAC : " & Mac & " Channel : " & channel.ToString & " Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                                            UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & " Alarm mask : " & Alarm.ToString & " - > " & testBin & vbCrLf)
'                                        End If
'                                    End If
'                                Else
'                                    objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Value = Alarm And 65535
'                                    Me.TblAlarmsTableAdapter.Insert(Mac, Now(), testBin, channel)
'                                    If vServerLogLevel >= EventLogEntryType.Warning Then
'                                        ApplicationEventLog.WriteEntry(Now.ToString & " > MAC : " & Mac & " Channel : " & channel.ToString & " Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                                        UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & " Alarm mask : " & Alarm.ToString & " - > " & testBin & vbCrLf)
'                                    End If
'                                End If
'                                Exit Sub
'                            End If
'                            ' for all the other cases 
'                            objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Value = Alarm And 65535
'                            Me.TblAlarmsTableAdapter.Insert(Mac, Now(), testBin, channel)
'                            If vServerLogLevel >= EventLogEntryType.Warning Then
'                                ApplicationEventLog.WriteEntry(Now.ToString & " > MAC : " & Mac & " Channel : " & channel.ToString & " Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                                UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & " Alarm mask : " & Alarm.ToString & " - > " & testBin & vbCrLf)
'                            End If
'                            Exit Sub
'                        End If
'                    Next
'                Case AlarmFlagsMask.RETURN_NORMAL
'                    For Each vDev As cDevices In objDevices
'                        If LCase(vDev.DevMacAddress) = Mac And vDev.ChannelID = channel Then

'                            For i = 0 To 8
'                                If i = 0 Then
'                                    objRegisters(i, vDev.GetObjId).Value = Alarm And 65535
'                                Else
'                                    objRegisters(i, vDev.GetObjId).Value = 0
'                                End If
'                            Next
'                            Exit Sub
'                        End If
'                    Next
'                Case Else
'                    If vServerLogLevel >= EventLogEntryType.Warning Then
'                        ApplicationEventLog.WriteEntry(" Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                        UpdateDetailsSafely(" Alarm mask : " & Alarm.ToString & vbCrLf)
'                    End If
'            End Select
'        Catch ex As Exception
'            ApplicationEventLog.WriteEntry(Now.ToString & "Problem for device: " & Mac & "-" & channel & ". Alarm received was : " & Alarm.ToString & "Error Message: " & ex.Message, EventLogEntryType.Warning)
'            UpdateDetailsSafely(Now.ToString & " Exception Raised. There is a problem for the device: " & Mac & "-" & channel & ". Alarm received was : " & Alarm.ToString & ex.Message & vbCrLf)
'        End Try
'    End Sub

'    Private Sub IVA_AlarmReceived(ByVal Mac As String, ByVal channel As Integer, ByVal Alarm As UInt32) 'Handles Tester.AlarmReceived
'        ' Function that inserts a filtered alarm to dataworks and a db called moreas. 
'        Try
'            Select Case MaskVCA(Alarm)
'                Case AlarmFlagsMask.MOTION
'                    Dim testBin As String
'                    testBin = Convert.ToString(Alarm And 65535, 2)
'                    If testBin.Length > 16 Then
'                        Throw New Exception("Packet Failure")
'                    End If
'                    While testBin.Length < 16
'                        testBin = "0" & testBin
'                    End While
'                    For Each vDev As cDevices In objDevices
'                        If LCase(vDev.DevMacAddress) = Mac And vDev.ChannelID = channel Then
'                            If Mid(testBin, 1, 1) = "1" Then
'                                vDev.AlarmSplitObject = True
'                                vDev.AlarmSplitObjectTime = Now()
'                                If vServerLogLevel >= EventLogEntryType.Information Then
'                                    UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & " Alarm mask : " & Alarm.ToString & " - > " & testBin & vbCrLf)
'                                End If
'                                If Mid(testBin, 2, 15) <> "000000000000000" Then
'                                    objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Value = Alarm And 65535
'                                    Me.TblAlarmsTableAdapter.Insert(Mac, Now(), testBin, channel)
'                                    If vServerLogLevel >= EventLogEntryType.Warning Then
'                                        ApplicationEventLog.WriteEntry(Now.ToString & " > MAC : " & Mac & " Channel : " & channel.ToString & " Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                                        UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & " Alarm mask : " & Alarm.ToString & " - > " & testBin & vbCrLf)
'                                    End If
'                                    SendConnectionMessageToVidos(vDev.DevIPAddress, vDev.ChannelID)
'                                End If
'                                System.Threading.Thread.Sleep(400)
'                                Exit Sub
'                            End If
'                            If Mid(testBin, testBin.Length - 1, 1) = "1" Or Mid(testBin, testBin.Length - 2, 1) = "1" Then
'                                If (Math.Abs(DateDiff(DateInterval.Second, Now, vDev.AlarmSplitObjectTime)) < 10) And (Math.Abs(DateDiff(DateInterval.Second, Now, vDev.AlarmSplitObjectTime)) >= 5) Then
'                                    If vDev.AlarmSplitObject = True Then
'                                        objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Value = Alarm And 65535
'                                        Me.TblAlarmsTableAdapter.Insert(Mac, Now(), testBin, channel)
'                                        If vServerLogLevel >= EventLogEntryType.Warning Then
'                                            ApplicationEventLog.WriteEntry(Now.ToString & " > MAC : " & Mac & " Channel : " & channel.ToString & " Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                                            UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & " Alarm mask : " & Alarm.ToString & " - > " & testBin & vbCrLf)
'                                        End If
'                                        SendConnectionMessageToVidos(vDev.DevIPAddress, vDev.ChannelID)
'                                        System.Threading.Thread.Sleep(400)
'                                    End If
'                                Else
'                                    If vServerLogLevel >= EventLogEntryType.Information Then
'                                        ApplicationEventLog.WriteEntry(Now.ToString & " > MAC : " & Mac & " Channel : " & channel.ToString & "Alarm not inserted because of NO low speed before it ")
'                                        UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & "Alarm not inserted because of NO low speed before it ")
'                                    End If
'                                End If
'                                vDev.AlarmSplitObject = False
'                                Exit Sub
'                            End If
'                            ' for all the other cases 
'                            objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Value = Alarm And 65535
'                            Me.TblAlarmsTableAdapter.Insert(Mac, Now(), testBin, channel)
'                            If vServerLogLevel >= EventLogEntryType.Warning Then
'                                ApplicationEventLog.WriteEntry(Now.ToString & " > MAC : " & Mac & " Channel : " & channel.ToString & " Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                                UpdateDetailsSafely("Device : " & objRegisters(IRegisterTypes.MOTION, vDev.GetObjId).Name & " Alarm mask : " & Alarm.ToString & " - > " & testBin & vbCrLf)
'                            End If
'                            SendConnectionMessageToVidos(vDev.DevIPAddress, vDev.ChannelID)
'                            System.Threading.Thread.Sleep(400)
'                            Exit Sub
'                        End If
'                    Next
'                Case AlarmFlagsMask.RETURN_NORMAL
'                    For Each vDev As cDevices In objDevices
'                        If LCase(vDev.DevMacAddress) = Mac And vDev.ChannelID = channel Then

'                            For i = 0 To 8
'                                If i = 0 Then
'                                    objRegisters(i, vDev.GetObjId).Value = Alarm And 65535
'                                Else
'                                    objRegisters(i, vDev.GetObjId).Value = 0
'                                End If
'                            Next
'                            Exit Sub
'                        End If
'                    Next
'                Case Else
'                    If vServerLogLevel >= EventLogEntryType.Information Then
'                        ApplicationEventLog.WriteEntry(" Alarm mask : " & Alarm.ToString, EventLogEntryType.Warning)
'                        UpdateDetailsSafely(" Alarm mask : " & Alarm.ToString & vbCrLf)
'                    End If
'            End Select
'        Catch ex As Exception
'            ApplicationEventLog.WriteEntry(Now.ToString & "Problem for device: " & Mac & "-" & channel & ". Alarm received was : " & Alarm.ToString & "Error Message: " & ex.Message, EventLogEntryType.Warning)
'            UpdateDetailsSafely(Now.ToString & " Exception Raised. There is a problem for the device: " & Mac & "-" & channel & ". Alarm received was : " & Alarm.ToString & ex.Message & vbCrLf)
'        End Try


'    End Sub

'    Private Sub StartApplicationServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartApplicationServerToolStripMenuItem.Click
'        StartIvaServer()
'        Me.StartApplicationServerToolStripMenuItem.Enabled = False
'        Me.LoadInterConnectionToolStripMenuItem.Enabled = False
'    End Sub

'    Private Sub BroadCastToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BroadCastToolStripMenuItem.Click
'        AutodiscoverDevices()
'    End Sub

'    Private Sub cbxServerLog_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxServerLog.SelectedIndexChanged
'        If cbxServerLog.SelectedIndex > -1 Then
'            Select Case cbxServerLog.SelectedIndex
'                Case 0
'                    vServerLogLevel = AppLogLevel.None
'                Case 1
'                    vServerLogLevel = AppLogLevel.OnlyErrors
'                Case 2
'                    vServerLogLevel = AppLogLevel.ErrorsAndEvents
'                Case 3
'                    vServerLogLevel = AppLogLevel.All
'            End Select
'        End If
'    End Sub

'    Private Sub cbxDllLog_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxDllLog.SelectedIndexChanged
'        If cbxDllLog.SelectedIndex > -1 Then
'            Select Case cbxDllLog.SelectedIndex
'                Case 0
'                    vDllsLogLevel = AppLogLevel.None
'                Case 1
'                    vDllsLogLevel = AppLogLevel.OnlyErrors
'                Case 2
'                    vDllsLogLevel = AppLogLevel.ErrorsAndEvents
'                Case 3
'                    vDllsLogLevel = AppLogLevel.All
'            End Select
'        End If
'    End Sub

'    Private Sub StopIVAServer()
'        DisconnectDevices()
'        If MyThread Is Nothing Then
'        Else
'            MyThread.Abort()
'            MyThread = Nothing
'        End If
'        If vServerLogLevel >= EventLogEntryType.Warning Then
'            ApplicationEventLog.WriteEntry("Server Restarted by user", EventLogEntryType.Warning)
'        End If
'        tvDevices.Nodes.Clear()
'        txtDetails.Text = "Application server Stopped"
'        Me.StartApplicationServerToolStripMenuItem.Enabled = True
'        Me.LoadInterConnectionToolStripMenuItem.Enabled = True
'        StopVidosConnectivity()
'    End Sub

'    Private Sub StopVidosConnectivity()
'        If Not VIDOSSocket Is Nothing Then
'            VIDOSSocket.CloseTCP()
'            VIDOSSocket = Nothing
'        End If
'        pbVidosStatus.Image = My.Resources.redLight
'    End Sub

'    Private Sub StopApplicationServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopApplicationServerToolStripMenuItem.Click
'        StopIVAServer()
'    End Sub

'    Private Sub CLearTextLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CLearTextLogToolStripMenuItem.Click
'        txtDetails.Text = ""
'    End Sub

'    Private Sub CreateNewInterConnectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewInterConnectionToolStripMenuItem.Click
'        Dim frmInterConnection As New frmInterConnect
'        OpenFormFor = TypeOfAction.Insert
'        frmInterConnection.ShowDialog()
'    End Sub

'    Private Sub EditInterConnectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditInterConnectionToolStripMenuItem.Click
'        Dim frmInterConnection As New frmInterConnect
'        OpenFormFor = TypeOfAction.Edit
'        frmInterConnection.ShowDialog()
'    End Sub

'    Private Sub UpdateTVWithConnections()
'        For i = 0 To IVAArray.Length - 1
'            If IVAArray(i).IsConnected Then
'                UpdateIMGDetailsSafely("4", vArrOfIPs(i))
'                MakeEntries(Now.ToString & " > Device Connected " & vArrOfIPs(i))
'            Else
'                UpdateIMGDetailsSafely("2", vArrOfIPs(i))
'            End If
'        Next
'    End Sub

'    Private Sub ShowStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowStatusToolStripMenuItem.Click
'        UpdateTVWithConnections()
'    End Sub

'    Private Sub frmMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
'        StopIVAServer()
'    End Sub

'    Private Sub ConnectVIDOS()
'        Try
'            If ParseSettings(My.Settings.VidosIP, My.Settings.TCPPort, My.Settings.UserName, My.Settings.Password) Then
'                StopVidosConnectivity()
'                VIDOSSocket = New VirtualTCPSocketClient(My.Settings.VidosIP, My.Settings.TCPPort)
'                If VIDOSSocket.ConnectionStatus Then
'                    VIDOSSocket.SendMessage("login " & My.Settings.UserName & " " & My.Settings.Password)
'                    VIDOSSocket.SendMessage("subscribe connection")
'                End If
'            End If
'            UpdateDetailsSafely("Finished Creating Connection to the Local Vidos Client")
'            ApplicationEventLog.WriteEntry("VIDOS: Finished Creating Connection to the Local Vidos Client", EventLogEntryType.Information)
'            pbVidosStatus.Image = My.Resources.greenLight
'        Catch ex As Exception
'            ApplicationEventLog.WriteEntry("VIDOS: Problem with starting the TCP listener thread. No events from VIDOS server will be monitored.", EventLogEntryType.Error)
'            UpdateDetailsSafely("VIDOS: Problem with starting the TCP listener thread. No events from VIDOS server will be monitored.")
'        End Try
'    End Sub

'    Private Sub SendConnectionMessageToVidos(ByVal vIp As String, ByVal vChannel As Integer)
'        If Not VIDOSSocket Is Nothing Then
'            VIDOSSocket.SendMessage("connect(""" & vIp & "/camera/" & vChannel & """,""#" & Trim(vVIDOSMonitor) & """);")
'        End If
'    End Sub

'    Private Sub StartVidosMonitorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartVidosMonitorToolStripMenuItem.Click
'        ConnectVIDOS()
'    End Sub

'    Private Sub StopVidosMonitorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopVidosMonitorToolStripMenuItem.Click
'        StopVidosConnectivity()
'    End Sub

'    Private Sub ChangeMonitorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeMonitorToolStripMenuItem.Click
'        cbxVidosMonitor.Enabled = True
'    End Sub

'    Private Sub cbxVidosMonitor_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxVidosMonitor.SelectedValueChanged
'        vVIDOSMonitor = Trim(cbxVidosMonitor.Text)
'        My.Settings.VidosMonitor = vVIDOSMonitor
'        My.Settings.Save()
'        cbxVidosMonitor.Enabled = False
'    End Sub

'End Class