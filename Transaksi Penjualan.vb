Imports System.Data.SqlClient
Imports System.Globalization
'Imports System.Net.Mime.MediaTypeNames

Public Class Transaksi_Penjualan

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Menu_Transaksi.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim response As Integer
        response = MessageBox.Show("Anda yakin ingin keluar Aplikasi?", "Exit Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If response = vbYes Then
            Application.ExitThread()
        End If
    End Sub

    Sub KondisiAwal()
        Call IdOtomatis()
        txtidbuku.Text = ""
        txtjudul.Text = ""
        txtkategori.Text = ""
        txtharga.Text = ""
        txtjumlah.Text = ""
        txttotal.Text = ""
        txtharga.Text = ""
        txtkembalian.Text = ""
        txtjumlah.Enabled = False
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
        Call munculidbuku()
        Call IdOtomatis()
        Call BuatKolom()
        txtidbuku.Focus()
        txttotal.Text = "0"
    End Sub

    Sub munculidbuku()
        Call koneksi()
        CMD = New SqlCommand("select * from Data_Buku_Baru", CONN)
        DR = CMD.ExecuteReader
    End Sub

    Sub IdOtomatis()
        Call koneksi()
        CMD = New SqlCommand("SELECT MAX(id_transaksi) FROM Penjualan", CONN)
        Dim maxId As Object = CMD.ExecuteScalar()

        Dim urutankode As String
        Dim hitung As Long

        If maxId Is DBNull.Value Then
            urutankode = "TR001"
        Else
            Dim maxIdString As String = maxId.ToString()
            Dim numericPart As String = maxIdString.Substring(1) 'Ambil bagian numerik setelah karakter pertama (misal : "001" dari "TR001"

            If Long.TryParse(numericPart, hitung) Then
                hitung += 1
                urutankode = "TR" + hitung.ToString("000")
            Else
                'handle kesalahan jika bagian numerik tidak dapat diubah menjadi Long MessageBox.Show("Kesalahan dalam mengonversi bagian numerik dari ID penjualan.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        txtidtransaksi.Text = urutankode
    End Sub

    Sub BuatKolom()
        DataGrid.Columns.Clear()
        DataGrid.Columns.Add("id_transaksi", "ID Transaksi")
        DataGrid.Columns.Add("id_buku", "ID Buku")
        DataGrid.Columns.Add("judul", "Judul")
        DataGrid.Columns.Add("kategori", "Kategori")
        DataGrid.Columns.Add("harga", "Harga")
        DataGrid.Columns.Add("jumlah", "Jumlah")
        DataGrid.Columns.Add("tanggal_penjualan", "Tanggal")
        DataGrid.Columns.Add("subtotal", "Subtotal")
    End Sub

    Private Sub delete_Click(sender As Object, e As EventArgs) Handles delete.Click
        Try
            If DataGrid.SelectedRows.Count > 0 Then
                Dim selectedRowIndex As Integer = DataGrid.SelectedRows(0).Index
                DataGrid.Rows.RemoveAt(selectedRowIndex)
                MsgBox("Data Berhasil Dihapus dari DataGridView!")
            Else
                MsgBox("Pilih baris yang ingin dihapus.")
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    Private Sub insert_Click(sender As Object, e As EventArgs) Handles insert.Click
        If txtjudul.Text = "" Or txtkategori.Text = "" Or txtharga.Text = "" Or txtjumlah.Text = "" Then
            MsgBox("Silahkan Masukkan Jumlah Barang Terlebih Dahulu!")
        Else
            DataGrid.Rows.Add(New String() {txtidtransaksi.Text, txtidbuku.Text, txtjudul.Text, txtkategori.Text, txtharga.Text, txtjumlah.Text, Format(Date.Now.ToString("dd/MM/yyyy")), Val(txtharga.Text) * Val(txtjumlah.Text)})
            Call RumusTotal()
            Call KondisiAwal()
            txtidbuku.Focus()
        End If
    End Sub

    Private Sub textidbuku_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtidbuku.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CMD = New SqlCommand("Select * from Data_Buku_Baru where id_buku='" & txtidbuku.Text & "'", CONN)
            DR = CMD.ExecuteReader
            DR.Read()
            If Not DR.HasRows Then
                MsgBox("ID Barang Tidak Ditemukan")
            Else
                txtidbuku.Text = DR.Item("id_buku")
                txtjudul.Text = DR.Item("judul")
                txtkategori.Text = DR.Item("kategori")
                txtharga.Text = DR.Item("harga")
                'textjumlah.Text = DR.Item("jumlah")
                txtjumlah.Enabled = True
                txtjumlah.Focus()
                DR.Close()
            End If
        End If
    End Sub

    Sub RumusTotal()
        Dim hitung As Integer = 0
        For i As Integer = 0 To DataGrid.Rows.Count - 1
            hitung = hitung + DataGrid.Rows(i).Cells(7).Value
            txttotal.Text = hitung
        Next
    End Sub

    Private Sub txttotalbayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttotalbayar.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(txttotalbayar.Text) < Val(txttotal.Text) Then
                MsgBox("Uang Anda Tidak Cukup!")
            ElseIf Val(txttotalbayar.Text) = Val(txttotal.Text) Then
                txtkembalian.Text = 0
            ElseIf Val(txttotalbayar.Text) > Val(txttotal.Text) Then
                txtkembalian.Text = Val(txttotalbayar.Text) - Val(txttotal.Text)
                save.Focus()
            End If
        End If
    End Sub

    Private Sub baru_Click(sender As Object, e As EventArgs) Handles baru.Click
        Call KondisiAwal()
    End Sub

    Private Sub clear_Click(sender As Object, e As EventArgs) Handles clear.Click
        Call KondisiAwal()
    End Sub

    Private Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        Try
            ' Pastikan ada baris di DataGridView
            If DataGrid.Rows.Count > 0 Then
                ' Buka koneksi
                Call koneksi()

                ' Loop melalui setiap baris di DataGridView
                For Each row As DataGridViewRow In DataGrid.Rows
                    ' Periksa apakah baris tersebut belum pernah disimpan
                    If row.IsNewRow = False Then
                        ' Ambil nilai dari setiap sel di baris
                        Dim idTransaksi As String = row.Cells("id_transaksi").Value.ToString()
                        Dim idBuku As String = row.Cells("id_buku").Value.ToString()
                        Dim judul As String = row.Cells("judul").Value.ToString()
                        Dim kategori As String = row.Cells("kategori").Value.ToString()
                        Dim harga As String = row.Cells("harga").Value.ToString()
                        Dim jumlah As String = row.Cells("jumlah").Value.ToString()
                        Dim tanggalString As String = row.Cells("tanggal_penjualan").Value.ToString()
                        Dim subtotal As String = row.Cells("subtotal").Value.ToString()
                        Dim tanggal As DateTime
                        If DateTime.TryParseExact(tanggalString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, tanggal) Then
                            ' Format the date for SQL Server
                            Dim tanggalFormatted As String = tanggal.ToString("yyyy-MM-dd HH:mm:ss")

                            ' Create SQL command to save data to the database
                            Dim saveCommand As String = "INSERT INTO Penjualan (id_transaksi, id_buku, judul, kategori, harga, jumlah, tanggal_penjualan, subtotal) " &
                                                "VALUES ('" & idTransaksi & "', '" & idBuku & "', '" & judul & "', '" & kategori & "', '" & harga & "', '" & jumlah & "', '" & tanggalFormatted & "', '" & subtotal & "')"

                            ' Create SqlCommand object and execute the SQL command
                            Dim insertCommand As New SqlCommand(saveCommand, CONN)
                            insertCommand.ExecuteNonQuery()
                        Else
                            MsgBox("Invalid date format: " & tanggalString)
                        End If

                    End If
                Next

                ' Tutup koneksi
                CONN.Close()

                ' Beri pesan bahwa data berhasil disimpan
                MsgBox("Data berhasil disimpan ke database.")

                ' Reset DataGridView dan kondisi awal
                BuatKolom()
                KondisiAwal()
            Else
                MsgBox("Tidak ada data untuk disimpan.")
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    Private Sub txtjudul_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtjudul.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CMD = New SqlCommand("Select * from Data_Buku_Baru where judul='" & txtjudul.Text & "'", CONN)
            DR = CMD.ExecuteReader
            DR.Read()
            If Not DR.HasRows Then
                MsgBox("Judul Tidak Ditemukan")
            Else
                txtidbuku.Text = DR.Item("id_buku")
                txtjudul.Text = DR.Item("judul")
                txtkategori.Text = DR.Item("kategori")
                txtharga.Text = DR.Item("harga")
                'textjumlah.Text = DR.Item("jumlah")
                txtjumlah.Enabled = True
                txtjumlah.Focus()
                DR.Close()
            End If
        End If
    End Sub

End Class