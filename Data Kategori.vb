Imports System.Data.SqlClient
Imports System.Runtime.DesignerServices

Public Class Data_Kategori

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
        txtkategori.Clear()
    End Sub

    Sub ketemu()
        On Error Resume Next
        txtid.Text = DR.Item("id_kategori")
        txtkategori.Text = DR.Item("kategori")
    End Sub

    Sub tampilgrid()
        Call koneksi()
        DA = New SqlDataAdapter("select * from Data_Kategori", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub

    Sub carikode()
        Call koneksi()
        CMD = New SqlCommand("select * from Data_Kategori where id_kategori ='" & txtid.Text & "'", CONN)
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
        CMD = New SqlCommand("SELECT MAX(id_kategori) FROM Data_Kategori", CONN)
        Dim maxId As Object = CMD.ExecuteScalar()

        Dim urutankode As String
        Dim hitung As Long

        If maxId Is DBNull.Value Then
            urutankode = "KB0001"
        Else
            Dim maxIdString As String = maxId.ToString()
            Dim numericPart As String = maxIdString.Substring(1)

            If Long.TryParse(numericPart, hitung) Then
                hitung += 1
                urutankode = "KB" + hitung.ToString("0000")
            Else
                'handle kesalahan jika bagian numerik tidak dapat diubah menjadi Long MessageBox.Show("Kesalahan dalam mengonversi bagian numerik dari ID penjualan.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        txtid.Text = urutankode
    End Sub

    Private Sub databaru_Click(sender As Object, e As EventArgs) Handles databaru.Click
        Call kondisiawal()
        txtkategori.Focus()
    End Sub

    Private Sub Data_Kategori_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
    End Sub

    Private Sub txtid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtid.KeyPress
        If e.KeyChar = Chr(13) Then
            Call carikode()
            If DR.HasRows Then
                Call ketemu()
                txtid.Focus()
            Else
                txtkategori.Focus()
            End If
        End If
    End Sub

    Private Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        Call carikode()
        If Not DR.HasRows Then
            Call koneksi()
            Dim save As String = "insert into Data_Kategori values ('" & txtid.Text & "', '" & txtkategori.Text & "')"
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
        Dim update As String = "update Data_Kategori set kategori = '" & txtkategori.Text & "' where id_kategori = '" & txtid.Text & "'"
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
            Dim delete As String = "delete Data_Kategori where id_kategori ='" & txtid.Text & "'"
            CMD = New SqlCommand(delete, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data berhasil dihapus")
            CONN.Close()
            Call kondisiawal()
            txtid.Focus()
        End If
    End Sub

End Class