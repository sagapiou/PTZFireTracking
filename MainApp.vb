Imports System.Net

Module MainApp
    Public Enum AppLogLevel As Integer
        None = 0
        OnlyErrors = 1
        ErrorsAndEvents = 2
        All = 4
    End Enum
    Public ApplicationEventLog As cApplicationLog = New cApplicationLog("FTA")
    Public ContactDetails As String = "Please contact Virtual Controls"
    Public ApplicationLogLevel As Integer
    Public gSelectedIP As String = ""
    Public arrCamConfig As String(,)
    Public IsForEdit As Boolean = False
    Public regExIpPattern As String = "^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$"
    Public regExMacPattern As String = "^([0-9a-fA-F]{2}[:\-]){5}[0-9a-fA-F]{2}"

    Public Function MacStringToMac48String(ByVal MAC As String) As String
        Dim FormatedMAC As String
        If (MAC.Length < 12) Then Throw New ArgumentException("Invalid MAC string.  This application uses MAC-48 which consists of 6 address bytes.")
        FormatedMAC = MAC.Insert(2, "-")
        FormatedMAC = FormatedMAC.Insert(5, "-")
        FormatedMAC = FormatedMAC.Insert(8, "-")
        FormatedMAC = FormatedMAC.Insert(11, "-")
        FormatedMAC = FormatedMAC.Insert(14, "-")
        Return FormatedMAC
    End Function
End Module
