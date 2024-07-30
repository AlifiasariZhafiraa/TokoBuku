Imports System.Data.SqlClient

Public Class Data_Buku

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
        cbkategori.Text = "Pilih"
        txtjudul.Clear()
        txtpenulis.Clear()
        txttahun.Clear()
        txtpenerbit.Clear()
        txtnmredisi.Clear()
        txtharga.Clear()
        txtstok.Clear()
    End Sub

    Sub ketemu()
        On Error Resume Next
        txtid.Text = DR.Item("id_buku")
        txtjudul.Text = DR.Item("judul")
        cbkategori.Text = DR.Item("kategori")
        txtpenulis.Text = DR.Item("penulis")
        txttahun.Text = DR.Item("tahun")
        txtpenerbit.Text = DR.Item("penerbit")
        txtnmredisi.Text = DR.Item("nomor_edisi")
        txtharga.Text = DR.Item("harga")
        txtstok.Text = DR.Item("stok")
    End Sub

    Sub tampilkategori()
        Call koneksi()
        CMD = New SqlCommand("select distinct kategori from Data_Kategori", CONN)
        DR = CMD.ExecuteReader
        cbkategori.Items.Clear()
        Do While DR.Read
            cbkategori.Items.Add(DR.Item("kategori"))
        Loop
        CONN.Close()
    End Sub

    Sub tampilgrid()
        Call koneksi()
        DA = New SqlDataAdapter("select * from Data_Buku_Baru", CONN)
        DS = New DataSet
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
        DataGridView1.ReadOnly = True
    End Sub

    Sub carikode()
        Call koneksi()
        CMD = New SqlCommand("select * from Data_Buku_Baru where id_buku ='" & txtid.Text & "'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
    End Sub

    Sub kondisiawal()
        Call tampilgrid()
        Call tampilkategori()
        Call kosongkan()
        Call IdOtomatis()
    End Sub

    Sub IdOtomatis()
        Call koneksi()
        CMD = New SqlCommand("SELECT MAX(id_buku) FROM Data_Buku_Baru", CONN)
        Dim maxId As Object = CMD.ExecuteScalar()

        Dim urutankode As String
        Dim hitung As Long

        If maxId Is DBNull.Value Then
            urutankode = "B00001"
        Else
            Dim maxIdString As String = maxId.ToString()
            Dim numericPart As String = maxIdString.Substring(1)

            If Long.TryParse(numericPart, hitung) Then
                hitung += 1
                urutankode = "B" + hitung.ToString("00000")
            Else
                'handle kesalahan jika bagian numerik tidak dapat diubah menjadi Long MessageBox.Show("Kesalahan dalam mengonversi bagian numerik dari ID penjualan.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        txtid.Text = urutankode
    End Sub

    Private Sub Data_Buku_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
    End Sub

    Private Sub databaru_Click(sender As Object, e As EventArgs) Handles databaru.Click
        Call kondisiawal()
        txtjudul.Focus()
    End Sub

    Private Sub txtid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtid.KeyPress
        If e.KeyChar = Chr(13) Then
            Call carikode()
            If DR.HasRows Then
                Call ketemu()
                txtid.Focus()
            Else
                cbkategori.Focus()
            End If
        End If
    End Sub

    Private Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        Call carikode()
        If Not DR.HasRows Then
            Call koneksi()
            Dim save As String = "insert into Data_Buku_Baru values ('" & txtid.Text & "', '" & txtjudul.Text & "', '" & cbkategori.Text & "', '" & txtpenulis.Text & "', 
                                '" & txttahun.Text & "', '" & txtpenerbit.Text & "', '" & txtnmredisi.Text & "', '" & txtharga.Text & "', '" & txtstok.Text & "')"
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
        Dim update As String = "update Data_Buku_Baru set judul = '" & txtjudul.Text & "', kategori = '" & cbkategori.Text & "', penulis = '" & txtpenulis.Text & "', tahun = 
                            '" & txttahun.Text & "', penerbit = '" & txtpenerbit.Text & "', nomor_edisi = '" & txtnmredisi.Text & "', harga = '" & txtharga.Text & "', stok = 
                            '" & txtstok.Text & "' where id_buku = '" & txtid.Text & "'"
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
            Dim delete As String = "delete Data_Buku_Baru where id_buku ='" & txtid.Text & "'"
            CMD = New SqlCommand(delete, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data berhasil dihapus")
            CONN.Close()
            Call kondisiawal()
            txtid.Focus()
        End If
    End Sub

End Class