Imports System.Xml

Module XMLVarious

    Public Function ReadDataFromXML(ByVal xmlSource As String, ByVal noOfFieldsToRead As Integer) As String(,)
        Dim aXmlData As String(,)
        Dim tempXmlDoc As New XmlDocument
        Try
            tempXmlDoc.Load(xmlSource)
            Dim vXmlNodeList As XmlNodeList = tempXmlDoc.SelectNodes("/FTA/Dome")
            If vXmlNodeList.Count > 0 Then
                ReDim aXmlData(vXmlNodeList.Count - 1, noOfFieldsToRead)
                For i = 0 To vXmlNodeList.Count - 1
                    For y = 0 To noOfFieldsToRead - 1
                        aXmlData(i, y) = vXmlNodeList.Item(i).ChildNodes(y).InnerText
                    Next
                    aXmlData(i, noOfFieldsToRead) = 0
                Next
            Else
                Return Nothing
            End If

            Return aXmlData

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Friend Sub WriteXML(ByVal XmlLocation As String, ByVal XMLDataToWrite As String(,))
        Try

            Dim writer As New XmlTextWriter(XmlLocation, System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("FTA")

            For i = 0 To XMLDataToWrite.GetLength(0) - 1
                writer.WriteStartElement("Dome")
                createNode("Mac", writer, XMLDataToWrite(i, 0))
                createNode("IP", writer, XMLDataToWrite(i, 1))
                createNode("Device", writer, XMLDataToWrite(i, 2))
                createNode("ChannelID", writer, XMLDataToWrite(i, 3))
                createNode("X-coord", writer, XMLDataToWrite(i, 4))
                createNode("Y-coord", writer, XMLDataToWrite(i, 5))
                writer.WriteEndElement()
            Next
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Σφάλμα κατά την δημιουργία του αρχείου στοιχείων καμερών.", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try

    End Sub

    Private Sub createNode(ByVal XmlTag As String, ByVal writer As XmlTextWriter, ByVal TagValue As String)
        writer.WriteStartElement(XmlTag)
        writer.WriteString(TagValue)
        writer.WriteEndElement()
    End Sub

End Module

