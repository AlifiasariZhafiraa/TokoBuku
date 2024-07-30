<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ListViewGroup2 As ListViewGroup = New ListViewGroup("ListViewGroup", HorizontalAlignment.Left)
        Label1 = New Label()
        ListView1 = New ListView()
        Idbuku = New ColumnHeader()
        Judulbuku = New ColumnHeader()
        Stoktoko = New ColumnHeader()
        Barangmasuk = New ColumnHeader()
        Barangkeluar = New ColumnHeader()
        StokAkhir = New ColumnHeader()
        GroupBox1 = New GroupBox()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.Location = New Point(460, 18)
        Label1.Name = "Label1"
        Label1.Size = New Size(188, 34)
        Label1.TabIndex = 0
        Label1.Text = "STOK BUKU"
        ' 
        ' ListView1
        ' 
        ListView1.BackColor = Color.DarkKhaki
        ListView1.Columns.AddRange(New ColumnHeader() {Idbuku, Judulbuku, Stoktoko, Barangmasuk, Barangkeluar, StokAkhir})
        ListViewGroup2.Header = "ListViewGroup"
        ListViewGroup2.Name = "ListViewGroup1"
        ListView1.Groups.AddRange(New ListViewGroup() {ListViewGroup2})
        ListView1.Location = New Point(525, 76)
        ListView1.Name = "ListView1"
        ListView1.Size = New Size(799, 458)
        ListView1.TabIndex = 1
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = View.Details
        ' 
        ' Idbuku
        ' 
        Idbuku.Text = "Id buku"
        Idbuku.Width = 110
        ' 
        ' Judulbuku
        ' 
        Judulbuku.Text = "Judul Buku"
        Judulbuku.Width = 150
        ' 
        ' Stoktoko
        ' 
        Stoktoko.Text = "Stok Toko"
        Stoktoko.Width = 120
        ' 
        ' Barangmasuk
        ' 
        Barangmasuk.Text = "Barang Masuk"
        Barangmasuk.Width = 120
        ' 
        ' Barangkeluar
        ' 
        Barangkeluar.Text = "Barang Keluar"
        Barangkeluar.Width = 120
        ' 
        ' StokAkhir
        ' 
        StokAkhir.Text = "Stok Akhir"
        StokAkhir.Width = 120
        ' 
        ' GroupBox1
        ' 
        GroupBox1.BackColor = Color.DarkKhaki
        GroupBox1.Location = New Point(8, 113)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(486, 343)
        GroupBox1.TabIndex = 2
        GroupBox1.TabStop = False
        GroupBox1.Text = "GroupBox1"
        ' 
        ' Form2
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.PaleGoldenrod
        ClientSize = New Size(1336, 546)
        Controls.Add(GroupBox1)
        Controls.Add(ListView1)
        Controls.Add(Label1)
        Name = "Form2"
        Text = "Form2"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ListView1 As ListView
    Friend WithEvents Idbuku As ColumnHeader
    Friend WithEvents Judulbuku As ColumnHeader
    Friend WithEvents Stoktoko As ColumnHeader
    Friend WithEvents Barangmasuk As ColumnHeader
    Friend WithEvents Barangkeluar As ColumnHeader
    Friend WithEvents StokAkhir As ColumnHeader
    Friend WithEvents GroupBox1 As GroupBox
End Class
