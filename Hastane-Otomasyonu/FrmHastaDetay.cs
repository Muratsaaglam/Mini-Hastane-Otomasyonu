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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlBaglantisi bgl = new sqlBaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = tc;

            //Ad Soyad Verileri Çeken Kod Bölümü
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad From Tbl_Hastalar where HastaTc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişini Çeken Kod Bölümü
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where HastaTc="+tc,bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşların Verilerini Çeken Kod Bölümü
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2=komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Branş ile Eşleşen Doktorların Gelmesini Sağlayan Kod Bölümü
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]+ " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Doktorun Randevularını Getiren Kod Bölümü
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular Where RandevuBrans='"+CmbBrans.Text+"'" + "and RandevuDoktor='"+CmbDoktor.Text+"' and RandevuDurum=0",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Bilgileri Düzenleme Kod Bölümü
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.Tcno = LblTc.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Data Grid View'den Sol Tıklandığında Verilerin Box'lara Otomatik Getirmesini Sağlayan Kod Bölümü
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            //Randevuyu Almaya Yarayan Kod Bölümü
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular set RandevuDurum=1,HastaTc=@p1,HastaSikayet=@p2 where Randevuid=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTc.Text);
            komut.Parameters.AddWithValue("@p2",RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3",Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
