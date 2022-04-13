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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl = new sqlBaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //Form Load'ında Doktorları DataTable'da Gösteren Kod Bölümü
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            bgl.baglanti().Close();

            //Doktoralara Ait Branşı CheckBoxa Getiren Kod Bölümü
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            // Doktor Ekleme Kod Bölümü
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",TxtAd.Text);
            komut.Parameters.AddWithValue("@d2",TxtSoyad.Text);
            komut.Parameters.AddWithValue("@d3",CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4",MskTc.Text);
            komut.Parameters.AddWithValue("@d5",TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt İşlmi Başarılı");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Data Grid View'de Herhangi Bir Hücreye Sol Tıklandığında Verileri İlgili Alanlara Göndere Kod Bölümü
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTc.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();



        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //Doktorlara Ait Bilgilerin Silinmesini Sağlayan Kod Bölümü
            SqlCommand komut = new SqlCommand("Delete From Tbl_Doktorlar where DoktorTc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTc.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            //Doktorlara Ait Bilgilerin Güncellenmesini Sağlayan Kod Bölümü
            SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d5 where DoktorTC=@d4", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4", MskTc.Text);
            komut.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme İşlmi Başarılı");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
