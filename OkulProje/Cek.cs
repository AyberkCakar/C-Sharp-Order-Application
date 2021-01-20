using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulProje
{
    public class Cek:Odeme
    {
        public long Tutar { get; set; }
        public override long Toplam() //Ödeme eklerken Çek tutarını override ediyoruz.Kapıda ödemedeki tutarı kaydetmek için.
        {
            return Tutar;
        }
        public override int OdemeYontemi()// Kontrol için odeme yöntemini override ediyoruz.
        {
            return 3;
        }
        public long CekOdemeID { get; set; }
        public string Banka { get; set; }
        public string Sube { get; set; }
        public string CekSahibi { get; set; }
        public string HesapNo { get; set; }
        public string IBAN { get; set; }
        public string CekKime { get; set; }
        public string CBasimTarihi { get; set; }
        public string CekNo { get; set; }
    }
}
