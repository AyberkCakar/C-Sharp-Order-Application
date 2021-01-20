using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OkulProje
{
    public class CreditCard:Odeme
    {
        public long Tutar { get; set; }
        public override long Toplam()//Ödeme eklerken Credi Kartı tutarını override ediyoruz.Kapıda ödemedeki tutarı kaydetmek için.
        {
            return Tutar;
        }
        public override int OdemeYontemi()  // Kontrol için odeme yöntemini override ediyoruz.
        {
            return 1;
        }
        public long KartOdemeID { get; set; }
        public string KartTip { get; set; }
        public string KartNo { get; set; }
        public int KartAy { get; set; }    
        public int KartYıl { get; set; }
        public int KartCVV { get; set; }
    }
}
