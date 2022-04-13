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
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }

        sqlBaglantisi bgl = new sqlBaglantisi();
        private void Duyurular_Load(object sender, EventArgs e)
        {
            //Duyuları Data Grid View'e Getiren Kod Bölümü
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Duyurular", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmGirisler frm = new FrmGirisler();
            frm.Show();
            this.Hide();
        }
    }
}
