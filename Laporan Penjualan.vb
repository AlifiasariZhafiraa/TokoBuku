Imports System.Data.SqlClient

Public Class Laporan_Penjualan
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

    Private Sub cetak_Click(sender As Object, e As EventArgs) Handles cetak.Click
        Call koneksi()
        CMD = New SqlCommand("Select * from Transaksi_Penjualan_Baru_Lagi where tanggal_penjualan between @date1 and @date2 order By tanggal_penjualan Desc", CONN)
        CMD.Parameters.Add("date1", SqlDbType.DateTime).Value = DateTimePicker1.Value
        CMD.Parameters.Add("date2", SqlDbType.DateTime).Value = DateTimePicker2.Value
        DA.SelectCommand = CMD
        DT.Clear()
        DA.Fill(DT)
        DataGridView1.DataSource = DT
        CONN.Close()
    End Sub

End Class