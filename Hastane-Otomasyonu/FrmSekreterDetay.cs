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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl = new sqlBaglantisi();
        public string TcNo;
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = TcNo;
            // Sekreter Detay Sayfasına Ad Soyad Getiren Kod bölümü
            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter Where SekreterTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTc.Text);
            SqlDataReader dr1 = komut.ExecuteReader();
            while (dr1.Read())
            {
                LblAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            // Branş Listesini Data View'de Gösteren Kod bölümü
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar",bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;
            bgl.baglanti().Close();

            //Doktorları Data View'de Gösteren Kod Bölümü
            DataTable dt2 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar',DoktorBrans From Tbl_Doktorlar",bgl.baglanti());
            da1.Fill(dt2);
            dataGridView2.DataSource = dt2;
            bgl.baglanti().Close();

            //Doktoralara Ait Branşı CheckBoxa Getiren Kod Bölümü
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();


        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //Randevu Oluşturma Kod Bölümü
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,HastaTc) values (@r1,@r2,@r3,@r4,@r5)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1",MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2",MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3",CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4",CmbDoktor.Text);
            komutkaydet.Parameters.AddWithValue("@r5",MskTc.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Branşa Tıklandığında ilgili doktorun gelmesini göstere Kod Bölümü
            CmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close(); 
        }

        private void BtnDuyuOluştur_Click(object sender, EventArgs e)
        {
            //Duyuruları Oluşturan Kod Bölümü
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",richTextBox1.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frmBrans = new FrmBrans();
            frmBrans.Show();
        }

        private void BtnRandevuListele_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frl = new FrmRandevuListesi();
            frl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmd = new FrmDuyurular();
            frmd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
