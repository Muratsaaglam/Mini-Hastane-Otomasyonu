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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlBaglantisi bgl = new sqlBaglantisi();
        public string TCNO;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = TCNO;
            //Doktor Bilgilerini Güncelleyen Kod Bölümü
            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar where DoktorTc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text =dr[3].ToString();    
                TxtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();

        }

        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            //Doktor Bilgierini Güncelleyen Kod Bölümü
            SqlCommand komut = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 where DoktorTC=@p5", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",CmbBrans.Text);
            komut.Parameters.AddWithValue("@p4",TxtSifre.Text);
            komut.Parameters.AddWithValue("@p5",MskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Güncelleme İşlemi Başarıyla Tamamlandı");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
