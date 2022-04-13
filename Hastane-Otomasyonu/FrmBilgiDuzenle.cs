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
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string Tcno;
        sqlBaglantisi bgl = new sqlBaglantisi();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            //Hastaya Ait Bilgilerin Otomatik Ekrana Gelmesini Sağlayan Kod Bölümü
            MskTC.Text = Tcno;
            SqlCommand komut = new SqlCommand("Select * From Tbl_Hastalar Where HastaTc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                MskTel.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();

            }
            bgl.baglanti().Close();

        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            //Hastaya Ait Bilgilerin Güncellendiği Kod Bölümü
            SqlCommand komut2 = new SqlCommand("update Tbl_Hastalar set HastaAd=@p1, HastaSoyad=@p2, HastaTelefon=@p3, HastaSifre=@p4, HastaCinsiyet=@p5 Where HastaTc=@p6",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",txtAd.Text);
            komut2.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3",MskTel.Text);
            komut2.Parameters.AddWithValue("@p4",txtSifre.Text);
            komut2.Parameters.AddWithValue("@p5",CmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6",MskTC.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Güncellendi");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
