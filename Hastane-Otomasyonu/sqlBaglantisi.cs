using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hastane_Otomasyonu
{
    class sqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=MSAGLAM\\MSSQLSERVER1;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }

    }
}
