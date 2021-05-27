Public Class DomeConfiguration

    Private Sub DomeConfiguration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadConfig()
    End Sub

    Private Sub LoadConfig()
        For i = 0 To arrCamConfig.GetLength(0) - 1
            If arrCamConfig(i, 1) = gSelectedIP Then
                Me.txtMac.Text = arrCamConfig(i, 0)
                Me.txtIP.Text = arrCamConfig(i, 1)
                Me.txtDev.Text = arrCamConfig(i, 2)
                Me.txtChann.Text = arrCamConfig(i, 3)
                Me.txtX.Text = arrCamConfig(i, 4)
                Me.txtY.Text = arrCamConfig(i, 5)
            End If
        Next
    End Sub


End Class