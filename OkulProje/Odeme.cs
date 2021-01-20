using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OkulProje
{
    public abstract class Odeme
    {
        public abstract long Toplam();  // Ödeme Abstract class tır ve CrediKartı,KapıdaÖdeme,Çek sınıfları Ödeme sınıfından kalıtım alır.
        public abstract int OdemeYontemi();

    }
}
