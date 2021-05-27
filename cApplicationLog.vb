Imports System.Diagnostics

Public Class cApplicationLog
    Dim m_EventLog As New EventLog

    Public Sub New(ByVal EventSource As String)
        ' Check if event source already exists
        If Not CheckSource("FTA") Then
            'otherwise create a new event source
            Try
                EventLog.CreateEventSource("FTA", EventSource)
            Catch ex As Exception
                MsgBox("Υπήρξε σφάλμα κατά την δημιουργία του Event Logger" & vbCrLf & _
                      "Περιγραφή σφάλματος: " & ex.Source & vbCrLf & _
                     "Παρακαλώ όπως επικονωνήσετε με την εταιρεία Virtual Controls", MsgBoxStyle.Critical, EventSource & "Ο event logger δεν μπόρεσε να δημιουργηθεί")
            End Try
        End If
        m_EventLog.Log = EventSource
        m_EventLog.Source = "FTA"
    End Sub

    Private Function CheckSource(ByVal Source As String) As Boolean
        If EventLog.SourceExists(Source) Then
            CheckSource = True
        Else
            CheckSource = False
        End If
    End Function

    Public Sub WriteEntry(ByVal LogMessage As String, ByVal LogLevel As Integer)
        If ApplicationLogLevel >= LogLevel Then
            m_EventLog.WriteEntry(LogMessage)
        End If
    End Sub

    Public Sub WriteEntry(ByVal LogMessage As String, ByVal LogType As System.Diagnostics.EventLogEntryType, ByVal LogLevel As Integer)
        If ApplicationLogLevel >= LogLevel Then
            m_EventLog.WriteEntry(LogMessage, LogType)
        End If
    End Sub
End Class
