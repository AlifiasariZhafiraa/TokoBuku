Imports System.Data.SqlClient
Imports System.Globalization

Public Class Transaksi_Pembelian

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim response As Integer
        response = MessageBox.Show("Anda yakin ingin keluar Aplikasi?", "Exit Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If response = vbYes Then
            Application.ExitThread()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        Menu_Transaksi.Show()
    End Sub

    Sub kondisiawal()
        txtjudullama.Text = ""
        txthargalama.Text = ""
        txtbmlama.Text = ""
        tanggal.Text = ""
        cbsupplier.Text = ""
        txtbmbaru.Text = ""
        txtidbaru.Text = ""
        txtjudulbaru.Text = ""
        txtpenulis.Text = ""
        txttahun.Text = ""
        txtpenerbit.Text = ""
        txtnmredisi.Text = ""
        txthargabaru.Text = ""
        txtidlama.Text = ""
        cbkategori.Text = ""
        Call IdOtomatis()
        Call tampilsupplier()
        Call tampilkategori()
    End Sub

    Private Sub Transaksi_Pembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
        Call BuatKolom1()
        Call BuatKolom2()
        Call IdOtomatis()
    End Sub

    Sub tampilsupplier()
        Call koneksi()
        CMD = New SqlCommand("select distinct nama_supplier from Data_Supplier", CONN)
        DR = CMD.ExecuteReader
        cbsupplier.Items.Clear()
        Do While DR.Read
            cbsupplier.Items.Add(DR.Item("nama_supplier"))
        Loop
        CONN.Close()
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
            Dim numericPart As String = maxIdString.Substring(1) 'Ambil bagian numerik setelah karakter pertama (misal : "001" dari "DB001"

            If Long.TryParse(numericPart, hitung) Then
                hitung += 1
                urutankode = "B" + hitung.ToString("00000")
            Else
                'handle kesalahan jika bagian numerik tidak dapat diubah menjadi Long MessageBox.Show("Kesalahan dalam mengonversi bagian numerik dari ID penjualan.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        txtidbaru.Text = urutankode
    End Sub

    Sub BuatKolom1()
        datagrid1.Columns.Clear()
        datagrid1.Columns.Add("nama_supplier", "Nama Suplier")
        datagrid1.Columns.Add("id_buku", "Id Judul")
        datagrid1.Columns.Add("judul", "Judul")
        datagrid1.Columns.Add("kategori", "Kategori")
        datagrid1.Columns.Add("penulis", "Penulis")
        datagrid1.Columns.Add("tahun", "Tahun Terbit")
        datagrid1.Columns.Add("penerbit", "penerbit")
        datagrid1.Columns.Add("nomor_edisi", "Nomor Edisi")
        datagrid1.Columns.Add("harga", "Harga")
        datagrid1.Columns.Add("barang_masuk", "Barang Masuk")
        datagrid1.Columns.Add("subtotal", "Subtotal")
    End Sub

    Sub BuatKolom2()
        datagrid2.Columns.Clear()
        datagrid2.Columns.Add("nama_supplier", "Nama Suplier")
        datagrid2.Columns.Add("id_buku", "Id Judul")
        datagrid2.Columns.Add("judul", "Judul")
        datagrid2.Columns.Add("kategori", "Kategori")
        datagrid2.Columns.Add("penulis", "Penulis")
        datagrid2.Columns.Add("tahun", "Tahun Terbit")
        datagrid2.Columns.Add("penerbit", "penerbit")
        datagrid2.Columns.Add("nomor_edisi", "Nomor Edisi")
        datagrid2.Columns.Add("harga", "Harga")
        datagrid2.Columns.Add("barang_masuk", "Barang Masuk")
        datagrid2.Columns.Add("subtotal", "Subtotal")
    End Sub

    Private Sub insertlama_Click(sender As Object, e As EventArgs) Handles insertlama.Click
        'If txtidbaru.Text = "" Or txtjudulbaru.Text = "" Or cbkategori.Text = "" Or txtpenulis.Text = "" Or txttahun.Text = "" Or txtpenerbit.Text = "" Or txtnmredisi.Text = "" Or txthargabaru.Text = " " Or txtbmbaru.Text = "" Then
        datagrid1.Rows.Add(New String() {cbsupplier.Text = "", txtidlama.Text, txtjudullama.Text, cbkategori.Text = "", txtpenulis.Text = "", txttahun.Text = "", txtpenerbit.Text = "", txtnmredisi.Text = "", txthargalama.Text, txtbmlama.Text, Val(txthargalama.Text.ToString) * Val(txtbmlama.Text.ToString)})
        'Else
        'DataGrid.Rows.Add(New String() {cbsupplier.Text, txtidbaru.Text, txtjudulbaru.Text, cbkategori.Text, txtpenulis.Text, txttahun.Text, txtpenerbit.Text, txtnmredisi.Text, txthargabaru.Text, txtbmbaru.Text, Val(txthargabaru.Text) * Val(txtbmbaru.Text)})
        'End If
        Call kondisiawal()
    End Sub

    Private Sub baru_Click(sender As Object, e As EventArgs) Handles baru.Click
        Call kondisiawal()
        Call IdOtomatis()
        datagrid1.Rows.Clear()
        txtidlama.Focus()
    End Sub

    Private Sub txtidlama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtidlama.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CMD = New SqlCommand("Select * from Data_Buku where id_buku='" & txtidlama.Text & "'", CONN)
            DR = CMD.ExecuteReader
            DR.Read()
            If Not DR.HasRows Then
                MsgBox("ID Barang Tidak Ditemukan")
            Else
                txtidlama.Text = DR.Item("id_buku")
                txtjudullama.Text = DR.Item("judul")
                txthargalama.Text = DR.Item("harga")
                'textjumlah.Text = DR.Item("jumlah")
            End If
        End If
    End Sub

    Private Sub deletelama_Click(sender As Object, e As EventArgs) Handles deletelama.Click
        Try
            If datagrid1.SelectedRows.Count > 0 Then
                Dim selectedRowIndex As Integer = datagrid1.SelectedRows(0).Index
                datagrid1.Rows.RemoveAt(selectedRowIndex)
                MsgBox("Data Berhasil Dihapus dari DataGridView!")
            Else
                MsgBox("Pilih baris yang ingin dihapus.")
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    Private Sub savelama_Click(sender As Object, e As EventArgs) Handles savelama.Click
        Try
            ' Pastikan ada baris di DataGridView
            If datagrid1.Rows.Count > 0 Then
                ' Buka koneksi
                Call koneksi()

                ' Loop melalui setiap baris di DataGridView
                For Each row As DataGridViewRow In datagrid1.Rows
                    ' Periksa apakah baris tersebut belum pernah disimpan
                    If row.IsNewRow = False Then
                        ' Ambil nilai dari setiap sel di baris
                        Dim namasupplier As String = row.Cells("nama_supplier").Value.ToString()
                        Dim idBuku As String = row.Cells("id_buku").Value.ToString()
                        Dim judul As String = row.Cells("judul").Value.ToString()
                        Dim kategori As String = row.Cells("kategori").Value.ToString()
                        Dim penulis As String = row.Cells("penulis").Value.ToString()
                        Dim tahunterbit As String = row.Cells("tahun").Value.ToString()
                        Dim penerbit As String = row.Cells("penerbit").Value.ToString()
                        Dim nomoredisi As String = row.Cells("nomor_edisi").Value.ToString()
                        Dim harga As String = row.Cells("harga").Value.ToString()
                        Dim barangmasuk As String = row.Cells("barang_masuk").Value.ToString()
                        Dim subtotal As String = row.Cells("subtotal").Value.ToString()
                        Dim tanggal As DateTime
                        ' Create SQL command to save data to the database
                        Dim saveCommand As String = "INSERT INTO Pembelian (nama_supplier, id_buku, judul, kategori, penulis, tahun, penerbit, nomor_edisi, harga, barang_masuk, subtotal) " &
                                                "VALUES ('" & namasupplier & "', '" & idBuku & "', '" & judul & "', '" & kategori & "', '" & penulis & "', '" & tahunterbit & "', '" & penerbit & "', '" & nomoredisi & "', '" & harga & "', '" & barangmasuk & "', '" & subtotal & "')"

                        ' Create SqlCommand object and execute the SQL command
                        Dim insertCommand As New SqlCommand(saveCommand, CONN)
                        insertCommand.ExecuteNonQuery()

                    End If
                Next

                ' Tutup koneksi
                CONN.Close()

                ' Beri pesan bahwa data berhasil disimpan
                MsgBox("Data berhasil disimpan ke database.")

                ' Reset DataGridView dan kondisi awal
                BuatKolom1()
                kondisiawal()
            Else
                MsgBox("Tidak ada data untuk disimpan.")
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    Private Sub insertbaru_Click(sender As Object, e As EventArgs) Handles insertbaru.Click
        datagrid2.Rows.Add(New String() {cbsupplier.Text, txtidbaru.Text, txtjudulbaru.Text, cbkategori.Text, txtpenulis.Text, txttahun.Text, txtpenerbit.Text, txtnmredisi.Text, txthargabaru.Text, txtbmbaru.Text, Val(txthargabaru.Text) * Val(txtbmbaru.Text)})
        Call kondisiawal()
    End Sub

    Private Sub deletebaru_Click(sender As Object, e As EventArgs) Handles deletebaru.Click
        Try
            If datagrid2.SelectedRows.Count > 0 Then
                Dim selectedRowIndex As Integer = datagrid2.SelectedRows(0).Index
                datagrid2.Rows.RemoveAt(selectedRowIndex)
                MsgBox("Data Berhasil Dihapus dari DataGridView!")
            Else
                MsgBox("Pilih baris yang ingin dihapus.")
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub

    Private Sub savebaru_Click(sender As Object, e As EventArgs) Handles savebaru.Click
        Try
            ' Pastikan ada baris di DataGridView
            If datagrid2.Rows.Count > 0 Then
                ' Buka koneksi
                Call koneksi()

                ' Loop melalui setiap baris di DataGridView
                For Each row As DataGridViewRow In datagrid2.Rows
                    ' Periksa apakah baris tersebut belum pernah disimpan
                    If row.IsNewRow = False Then
                        ' Ambil nilai dari setiap sel di baris
                        Dim namasupplier As String = row.Cells("nama_supplier").Value.ToString()
                        Dim idBuku As String = row.Cells("id_buku").Value.ToString()
                        Dim judul As String = row.Cells("judul").Value.ToString()
                        Dim kategori As String = row.Cells("kategori").Value.ToString()
                        Dim penulis As String = row.Cells("penulis").Value.ToString()
                        Dim tahunterbit As String = row.Cells("tahun").Value.ToString()
                        Dim penerbit As String = row.Cells("penerbit").Value.ToString()
                        Dim nomoredisi As String = row.Cells("nomor_edisi").Value.ToString()
                        Dim harga As String = row.Cells("harga").Value.ToString()
                        Dim barangmasuk As String = row.Cells("barang_masuk").Value.ToString()
                        Dim subtotal As String = row.Cells("subtotal").Value.ToString()
                        ' Create SQL command to save data to the database
                        Dim saveCommand As String = "INSERT INTO Pembelian (nama_supplier, id_buku, judul, kategori, penulis, tahun, penerbit, nomor_edisi, harga, barang_masuk, subtotal) " &
                                                "VALUES ('" & namasupplier & "', '" & idBuku & "', '" & judul & "', '" & kategori & "', '" & penulis & "', '" & tahunterbit & "', '" & penerbit & "', '" & nomoredisi & "', '" & harga & "', '" & barangmasuk & "', '" & subtotal & "')"

                        Dim savebuku As String = "INSERT INTO Data_Buku_Baru (id_buku, judul, kategori, penulis, tahun, penerbit, nomor_edisi, harga, stok)" &
                                                "VALUES ('" & idBuku & "', '" & judul & "', '" & kategori & "', '" & penulis & "', '" & tahunterbit & "', '" & penerbit & "', '" & nomoredisi & "', '" & harga & "', '" & barangmasuk & "')"

                        ' Create SqlCommand object and execute the SQL command
                        Dim insertCommand As New SqlCommand(saveCommand, CONN)
                        Dim insertbuku As New SqlCommand(savebuku, CONN)
                        insertCommand.ExecuteNonQuery()
                        insertbuku.ExecuteNonQuery()

                    End If
                Next

                ' Tutup koneksi
                CONN.Close()

                ' Beri pesan bahwa data berhasil disimpan
                MsgBox("Data berhasil disimpan ke database.")

                ' Reset DataGridView dan kondisi awal
                BuatKolom2()
                kondisiawal()
            Else
                MsgBox("Tidak ada data untuk disimpan.")
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message)
        End Try
    End Sub
End Class