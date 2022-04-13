using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Otomasyonu
{
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl = new sqlBaglantisi();

        private void FrmBrans_Load(object sender, EventArgs e)
        {
            //Bransların Data Gride Yüklenmesi Sağlayan Kod Bölümü
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            //Branş Ekleme Kod Bölümü
            SqlCommand komut = new SqlCommand("insert into Tbl_Branslar (Bransad) values (@b1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@b1",TxtBrans.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Eklendi");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Data Grid View'e Bir Kez Tıklanınca Verilerin Text'Boxa Gelmesini Sağlayan Kod Bölümü
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            // Seçilen Branşı Silme İşlemi Yapan Kod Bölümü
            SqlCommand komut = new SqlCommand("Delete from Tbl_Branslar Where Bransid=@b1",bgl.baglanti());
            komut.Parameters.AddWithValue("@b1",TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            //Seçilen Branşı Güncelleme İşlemi Yapan Kod Bölümü
            SqlCommand komut = new SqlCommand("Update Tbl_Branslar set Bransad=@p1 where bransid=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",TxtBrans.Text);
            komut.Parameters.AddWithValue("@p2",TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Güncellendi");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
