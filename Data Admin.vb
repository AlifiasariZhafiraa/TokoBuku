Imports System.Data.SqlClient

Public Class Data_Admin

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Menu_Master.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim response As Integer
        response = MessageBox.Show("Anda yakin ingin keluar Aplikasi?", "Exit Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If response = vbYes Then
            Application.ExitThread()
        End If
    End Sub

    Sub kosongkan()
        txtid.Clear()
        txtnama.Clear()
        txtpassword.Clear()
        txttelp.Clear()
    End Sub

    Sub ketemu()
        On Error Resume Next
        txtid.Text = DR.Item("id_admin")
        txtnama.Text = DR.Item("nama_admin")
        txtpassword.Text = DR.Item("password")
        txttelp.Text = DR.Item("telp_admin")
    End Sub

    Sub tampilgrid()
        Call koneksi()
        DA = New SqlDataAdapter("select * from Data_Admin_Baru_Lagi", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub

    Sub carikode()
        Call koneksi()
        CMD = New SqlCommand("select * from Data_Admin_Baru_Lagi where id_admin ='" & txtid.Text & "'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
    End Sub

    Sub kondisiawal()
        Call tampilgrid()
        Call kosongkan()
        Call IdOtomatis()
    End Sub

    Sub IdOtomatis()
        Call koneksi()
        CMD = New SqlCommand("SELECT MAX(id_admin) FROM Data_Admin_Baru_Lagi", CONN)
        Dim maxId As Object = CMD.ExecuteScalar()

        Dim urutankode As String
        Dim hitung As Long

        If maxId Is DBNull.Value Then
            urutankode = "A001"
        Else
            Dim maxIdString As String = maxId.ToString()
            Dim numericPart As String = maxIdString.Substring(1) 'Ambil bagian numerik setelah karakter pertama (misal : "001" dari "TR001"

            If Long.TryParse(numericPart, hitung) Then
                hitung += 1
                urutankode = "A" + hitung.ToString("000")
            Else
                'handle kesalahan jika bagian numerik tidak dapat diubah menjadi Long MessageBox.Show("Kesalahan dalam mengonversi bagian numerik dari ID penjualan.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        txtid.Text = urutankode
    End Sub

    Private Sub Data_Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
    End Sub

    Private Sub txtid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtid.KeyPress
        If e.KeyChar = Chr(13) Then
            Call carikode()
            If DR.HasRows Then
                Call ketemu()
                txtid.Focus()
            Else
                txtnama.Focus()
            End If
        End If
    End Sub

    Private Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        Call carikode()
        If Not DR.HasRows Then
            Call koneksi()
            Dim save As String = "insert into Data_Admin_Baru_Lagi values ('" & txtid.Text & "', '" & txtnama.Text & "', '" & txtpassword.Text & "', '" & txttelp.Text & "')"
            CMD = New SqlCommand(save, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data berhasil disimpan")
            CONN.Close()
            Call kondisiawal()
            txtid.Focus()
        End If
    End Sub

    Private Sub update_Click(sender As Object, e As EventArgs) Handles update.Click
        Call koneksi()
        Dim update As String = "update Data_Admin_Baru_Lagi set nama_admin = '" & txtnama.Text & "', password = '" & txtpassword.Text & "', telp_admin = '" & txttelp.Text & "' where id_admin = '" & txtid.Text & "'"
        CMD = New SqlCommand(update, CONN)
        CMD.ExecuteNonQuery()
        MsgBox("Data berhasil diupdate")
        CONN.Close()
        Call kondisiawal()
        txtid.Focus()
    End Sub

    Private Sub delete_Click(sender As Object, e As EventArgs) Handles delete.Click
        Call koneksi()
        If MessageBox.Show("Yakin data akan dihapus?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Dim delete As String = "delete Data_Admin_Baru_Lagi where id_admin ='" & txtid.Text & "'"
            CMD = New SqlCommand(delete, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data berhasil dihapus")
            CONN.Close()
            Call kondisiawal()
            txtid.Focus()
        End If
    End Sub

    Private Sub databaru_Click(sender As Object, e As EventArgs) Handles databaru.Click
        Call kondisiawal()
        txtnama.Focus()
    End Sub
End Class