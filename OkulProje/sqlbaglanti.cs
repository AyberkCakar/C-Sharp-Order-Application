using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OkulProje
{
    public class sqlbaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-CCP65NT\SQLEXPRESS;Initial Catalog=OkulProje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}

