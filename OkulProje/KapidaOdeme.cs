using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulProje
{
    public class KapidaOdeme : Odeme
    {
        public long KapidaOdemeID { get; set; }
        public long Tutar { get; set; }
        public long KargoUcret { get; set; }
        public override long Toplam() //Ödeme eklerken kapıda ödeme tutarını override ediyoruz.Kapıda ödemedeki tutarı kaydetmek için.
        {
            return Tutar;
        }
        public override int OdemeYontemi() // Kontrol için odeme yöntemini override ediyoruz.
        {
            return 2;
        }
        //Ağırlığa Göre Her Kargo şirketinin değişen tarifesini hesaplıyoruz.
        public double ArasHesapla(double kilo)
        {
            double odeme=0;
            if(kilo<0)
            { odeme = 0; }
            else if (kilo >= 0.1 && kilo <= 5)
            { odeme = 5;  }
            else if(kilo >= 5.1 && kilo <= 10)
            { odeme = 7.5; }
            else if (kilo >= 10.1 && kilo <= 24.9)
            { odeme = 10; }
            else if (kilo >= 25 && kilo <= 100)
            { for (int i = 1; i <= kilo - 25; i++)
                { odeme += 1.5; }
                odeme=odeme + 10;
            }
            else if(kilo>100)
            { odeme = 130; }
            return odeme;
        }
        public double YurticiHesapla(double kilo)
        {
            double odeme = 0;
            if (kilo < 0)
            { odeme = 0; }
            else if (kilo >= 0.1 && kilo <= 5)
            { odeme = 6; }
            else if (kilo >= 5.1 && kilo <= 10)
            { odeme = 7.5; }
            else if (kilo >= 10.1 && kilo <= 24.9)
            { odeme = 12; }
            else if (kilo >= 25 && kilo <= 100)
            {
                for (int i = 1; i <= kilo - 25; i++)
                { odeme += 1.2; }
                odeme = odeme + 12;
            }
            else if (kilo > 100)
            { odeme = 130; }
            return odeme;
        }
        public double UpsHesapla(double kilo)
        {
            double odeme = 0;
            if (kilo < 0)
            { odeme = 0; }
            else if (kilo >= 0.1 && kilo <= 5)
            { odeme = 10; }
            else if (kilo >= 5.1 && kilo <= 10)
            { odeme = 12; }
            else if (kilo >= 10.1 && kilo <= 24.9)
            { odeme = 15; }
            else if (kilo >= 25 && kilo <= 100)
            {
                for (int i = 1; i <= kilo - 25; i++)
                { odeme += 1.7; }
                odeme = odeme + 17;
            }
            else if (kilo > 100)
            { odeme = 170; }
            return odeme;
        }
        public double MngHesapla(double kilo)
        {
            double odeme = 0;
            if (kilo < 0)
            { odeme = 0; }
            else if (kilo >= 0.1 && kilo <= 5)
            { odeme = 7; }
            else if (kilo >= 5.1 && kilo <= 10)
            { odeme = 9; }
            else if (kilo >= 10.1 && kilo <= 24.9)
            { odeme = 12; }
            else if (kilo >= 25 && kilo <= 100)
            {
                for (int i = 1; i <= kilo - 25; i++)
                { odeme += 1.3; }
                odeme = odeme + 12;
            }
            else if (kilo > 100)
            { odeme = 130; }
            return odeme;
        }
    }
}
