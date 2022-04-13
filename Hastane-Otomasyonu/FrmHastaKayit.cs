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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl = new sqlBaglantisi();
        private void FrmHastaKayit_Load(object sender, EventArgs e)
        {
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTc,HastaTelefon,HastaSifre,HastaCinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",MskTC.Text);
            komut.Parameters.AddWithValue("@p4",MskTelefon.Text);
            komut.Parameters.AddWithValue("@p5",TxtSifre.Text);
            komut.Parameters.AddWithValue("@p6",CmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt İşlemi Başarıyla Tamamlandı");
              
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();

        }
    }
}
