Public Class HomePage
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Menu_Master.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Menu_Transaksi.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Menu_Laporan.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim response As Integer
        response = MessageBox.Show("Anda yakin ingin Logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If response = vbYes Then
            Login.Show()
            Me.Hide()
        End If
    End Sub
End Class
