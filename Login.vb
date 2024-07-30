Imports System.Data.SqlClient

Public Class Login

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()
        CMD = New SqlCommand("select * from Data_Admin_Baru_Lagi where id_admin = '" & txtid.Text &
                             "'and password ='" & txtpassword.Text & "'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        If DR.HasRows Then
            MessageBox.Show("Login Berhasil")
            HomePage.Show()
            Me.Hide()
            txtid.Clear()
            txtpassword.Clear()
        Else
            MessageBox.Show("ID Admin atau Password Anda Salah")
            txtid.Focus()
        End If
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpassword.UseSystemPasswordChar = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            txtpassword.UseSystemPasswordChar = False
        Else
            txtpassword.UseSystemPasswordChar = True
        End If
        txtpassword.Focus()
    End Sub
End Class