Public Class cDevices

    Private vMacAddress As String
    Private vIPAddress As String
    Private vIsConnected As Boolean = False
    Private vConnectionComment As String = ""
    Private vChannelID As Integer = 0
    Private vRegisterName As String = ""
    Private voID As Integer = -1
    Private vLatitude As Double = 0
    Private vLongtitude As Double = 0
    Private vDeviceName As String = ""
    Private vPreset99Position As Integer = -1


    Public Sub New(ByVal MacAddress As String, ByVal Ipaddress As String, ByVal DeviceName As String, ByVal ChannelId As String, ByVal Longtitude As String, ByVal Latitude As String, ByVal objID As Integer)
        vIPAddress = Ipaddress
        vMacAddress = MacAddress
        vChannelID = Trim(ChannelId)
        vLatitude = CDbl(Latitude)
        vLongtitude = CDbl(Longtitude)
        vDeviceName = Trim(DeviceName)
        voID = objID
    End Sub

    Public ReadOnly Property DevObjID() As Integer
        Get
            Return voID
        End Get
    End Property

    Public Property DevIPAddress() As String
        Get
            Return vIPAddress
        End Get
        Set(ByVal value As String)
            vIPAddress = value
        End Set
    End Property

    Public Property DevMacAddress() As String
        Get
            Return vMacAddress
        End Get
        Set(ByVal value As String)
            vMacAddress = value
        End Set
    End Property

    Public Property DevIsConnected() As Boolean
        Get
            Return vIsConnected
        End Get
        Set(ByVal value As Boolean)
            vIsConnected = value
        End Set
    End Property

    Public Property DevConnectionComment() As String
        Get
            Return vConnectionComment
        End Get
        Set(ByVal value As String)
            vConnectionComment = Trim(value)
        End Set
    End Property

    Public Property ChannelID() As String
        Get
            Return vChannelID
        End Get
        Set(ByVal value As String)
            vChannelID = value
        End Set
    End Property

    Public Property LongtitudeValue() As String
        Get
            Return vLongtitude
        End Get
        Set(ByVal value As String)
            vLongtitude = Trim(value)
        End Set
    End Property

    Public Property LatitudeValue() As String
        Get
            Return vLatitude
        End Get
        Set(ByVal value As String)
            vLatitude = Trim(value)
        End Set
    End Property


    Public ReadOnly Property GetObjId() As Integer
        Get
            Return voID
        End Get
    End Property

    Public ReadOnly Property GetDeviceName() As String
        Get
            Return vDeviceName
        End Get
    End Property

    Public Property Preset99Position() As Integer
        Get
            Return vPreset99Position
        End Get
        Set(ByVal value As Integer)
            vPreset99Position = value
        End Set
    End Property

End Class
