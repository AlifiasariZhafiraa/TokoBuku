﻿Public Class Laporan_Pembelian
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Menu_Laporan.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim response As Integer
        response = MessageBox.Show("Anda yakin ingin keluar Aplikasi?", "Exit Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If response = vbYes Then
            Application.ExitThread()
        End If
    End Sub
End Class