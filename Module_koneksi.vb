Imports System.Data.SqlClient

Module Module_Koneksi
    Public CONN As SqlConnection
    Public DA As SqlDataAdapter
    Public DS As DataSet
    Public CMD As SqlCommand
    Public DR As SqlDataReader
    Public DT As DataTable

    Public Sub koneksi()
        CONN = New SqlConnection("Data Source=LAPTOP-BOFMCSBB\TOKOBUKU;initial catalog=TokoBuku;integrated security=true")
        CONN.Open()
    End Sub
End Module
