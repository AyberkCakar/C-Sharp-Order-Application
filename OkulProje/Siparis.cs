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
using System.Collections;

namespace OkulProje
{
    public class Siparis
    {
        public long  SiparisID { get; set; }
        public string SiparisTarih { get; set; }
        public string SiparisDurum { get; set; }
        public Siparis() //constructor ile her siparişin ilk durumu "Onay Bekliyor." olacaktır
        {
            this.SiparisDurum = "Onay Bekliyor.";
        }
        public string Adres { get; set; }
        public string SahipAdSoyad { get; set; }
        public string KargoSirket { get; set; }
        public string Agirlik { get; set; }
        public string ToplamTutar { get; set; }
        public List<SiparisDetay> SiparisKalem = new List<SiparisDetay>(); // Sipariş Kalemlerimiz
        public List<Odeme> OdemeList = new List<Odeme>(); // Odeme Kalemlerimiz
        public void KalemEkle(SiparisDetay k)// Sipariş Kalemi Ekleme Metodumuz
        {
            SiparisKalem.Add(k);
        }
        sqlbaglanti bgl = new sqlbaglanti();
        public void OdemeEkle(Odeme o) // Sipariş Ödeme Ekleme Metodumuz
        {
            OdemeList.Add(o);
        }
        public long IDOlustur() // Random Sipariş ID oluşturuyoruz.
        {
            long random;
            Random rd = new Random();
            random = rd.Next(00000, 99999);
            SqlCommand komut = new SqlCommand("Select * from Tbl_Siparisler", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                SiparisID = Convert.ToInt32(dr[0]);
                if (SiparisID == random) // Bu id'den var mı kontrol et
                {
                    random = rd.Next(00000, 99999);
                    SiparisID = random;
                }
                else
                    SiparisID = random;     
            }return random;
        }
        public void OdemeSqlEkle() // Ödemeyi Sql'e Ekle
        {
            foreach (Odeme ode in OdemeList)
            {
                if (ode.OdemeYontemi()==1)
                {
                    SqlCommand kmcc = new SqlCommand("insert into Tbl_Kartlar (CCTip,CCNo,CCCVV,CCAy,CCYıl,CCOdemeTutar,CCOdemeID) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7)", bgl.baglanti());
                    kmcc.Parameters.AddWithValue("@a1", ((ode as CreditCard).KartTip));
                    kmcc.Parameters.AddWithValue("@a2", ((ode as CreditCard).KartNo));
                    kmcc.Parameters.AddWithValue("@a3", ((ode as CreditCard).KartCVV));
                    kmcc.Parameters.AddWithValue("@a4", ((ode as CreditCard).KartAy));
                    kmcc.Parameters.AddWithValue("@a5", ((ode as CreditCard).KartYıl));
                    kmcc.Parameters.AddWithValue("@a6", ((ode as CreditCard).Toplam()));
                    kmcc.Parameters.AddWithValue("@a7", ((ode as CreditCard).KartOdemeID));
                    kmcc.ExecuteNonQuery();
                    bgl.baglanti().Close();
                }
                else if(ode.OdemeYontemi()==2)
                {
                    SqlCommand kmko = new SqlCommand("insert into Tbl_KapıdaOdeme (KapidaOdemeTutar,KapidaKargoUcret,KapidaOdemeID) values (@a1,@a2,@a3)", bgl.baglanti());
                    kmko.Parameters.AddWithValue("@a1", ((ode as KapidaOdeme).Tutar));
                    kmko.Parameters.AddWithValue("@a2", ((ode as KapidaOdeme).KargoUcret));
                    kmko.Parameters.AddWithValue("@a3", ((ode as KapidaOdeme).KapidaOdemeID));
                    kmko.ExecuteNonQuery();
                    bgl.baglanti().Close();
                }
                else if(ode.OdemeYontemi()==3)
                {
                    SqlCommand kmck = new SqlCommand("insert into Tbl_Cek (CBanka,CSube,CekSahip,CHesapNo,CIBAN,CekKime,CBasimTarih,CekNo,CekOdemeTutar,CekOdemeID) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", bgl.baglanti());
                    kmck.Parameters.AddWithValue("@a1", (ode as Cek).Banka);
                    kmck.Parameters.AddWithValue("@a2", (ode as Cek).Sube);
                    kmck.Parameters.AddWithValue("@a3", (ode as Cek).CekSahibi);
                    kmck.Parameters.AddWithValue("@a4", (ode as Cek).HesapNo);
                    kmck.Parameters.AddWithValue("@a5", (ode as Cek).IBAN);
                    kmck.Parameters.AddWithValue("@a6", (ode as Cek).CekKime);
                    kmck.Parameters.AddWithValue("@a7", (ode as Cek).CBasimTarihi);
                    kmck.Parameters.AddWithValue("@a8", (ode as Cek).CekNo);
                    kmck.Parameters.AddWithValue("@a9", (ode as Cek).Toplam());
                    kmck.Parameters.AddWithValue("@a10", (ode as Cek).CekOdemeID);
                    kmck.ExecuteNonQuery();
                    bgl.baglanti().Close();
                }
            }
            OdemeList.Clear();
        }
        public void OdemeListeleCard(long SipId) // Sql den Seçilen Sipariş İd' ye Göre Credi Kartı Bilgilerini Getiriyoruz.Ve Odeme Listemize ekliyoruz.
        {
            SqlCommand komutC = new SqlCommand("Select * from Tbl_Kartlar where CCOdemeID= @p1", bgl.baglanti());
            komutC.Parameters.AddWithValue("@p1", SipId);
            SqlDataReader drC = komutC.ExecuteReader();
            while (drC.Read())
            {
                CreditCard cdco = new CreditCard();
                cdco.Tutar = Convert.ToInt32(drC[5]);
                cdco.KartAy = Convert.ToInt32(drC[3]);
                cdco.KartCVV = Convert.ToInt32(drC[2]);
                cdco.KartNo = drC[1].ToString();
                cdco.KartTip = drC[0].ToString();
                cdco.KartYıl = Convert.ToInt32(drC[4]);
                OdemeEkle(cdco);
            }
            bgl.baglanti().Close();
        }
        public void OdemeListeleKapida(long SipId1) // Sql den Seçilen Sipariş İd' ye Göre Kapıda Ödeme Bilgilerini Getiriyoruz.Ve Odeme Listemize ekliyoruz.
        {
            SqlCommand komutK = new SqlCommand("Select * from Tbl_KapıdaOdeme where KapidaOdemeID=@p1", bgl.baglanti());
            komutK.Parameters.AddWithValue("@p1", SipId1);
            SqlDataReader drK = komutK.ExecuteReader();
            while (drK.Read())
            {
                KapidaOdeme kpo = new KapidaOdeme();
                kpo.KargoUcret = Convert.ToInt32(drK[1].ToString());
                kpo.Tutar = Convert.ToInt32(drK[0]);
                OdemeEkle(kpo);
            }
            bgl.baglanti().Close();
        }
        public void OdemeListeleCek(long SipId2) // Sql den Seçilen Sipariş İd' ye Göre Çek Bilgilerini Getiriyoruz.Ve Odeme Listemize ekliyoruz.
        {
            SqlCommand komutCC = new SqlCommand("Select * from Tbl_Cek where CekOdemeID= @p1", bgl.baglanti());
            komutCC.Parameters.AddWithValue("@p1", SipId2);
            SqlDataReader drCC = komutCC.ExecuteReader();
            while (drCC.Read())
            {
                Cek ckc = new Cek();
                ckc.CekKime = drCC[5].ToString();
                ckc.HesapNo = drCC[3].ToString();
                ckc.CekSahibi = drCC[2].ToString();
                ckc.Sube = drCC[1].ToString();
                ckc.Banka = drCC[0].ToString();
                ckc.IBAN = drCC[4].ToString();
                ckc.CBasimTarihi = drCC[6].ToString();
                ckc.CekNo = drCC[7].ToString();
                ckc.Tutar = Convert.ToInt32(drCC[8]);
                OdemeEkle(ckc);
            }
            bgl.baglanti().Close();
        }
        public void KalemEklesql(long id) // Sipariş Kalemlerini Sipariş id ye göre sql e yaz.
        {
            foreach(SiparisDetay sd in SiparisKalem)
            {
                SqlCommand km = new SqlCommand("insert into TBL_SiparisDetay (SipDetId,UrunAd,UToplam,UAdet,UAgirlik) values (@a1,@a2,@a3,@a4,@a5)", bgl.baglanti());
                km.Parameters.AddWithValue("@a1", id);
                km.Parameters.AddWithValue("@a2", sd.Urun.UrunAd);
                km.Parameters.AddWithValue("@a3", (sd.Urun.BirimFiyat*sd.Adet));
                km.Parameters.AddWithValue("@a4", sd.Adet);
                km.Parameters.AddWithValue("@a5", sd.Agirlik);
                km.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
        }
        public long TutarHesapla() //Sipariş Kalemine Ekleneri Hesapla
        {
            long deger = 0;
            foreach (SiparisDetay sis in SiparisKalem)
            {
                deger += (sis.Adet * sis.Urun.BirimFiyat);
            }
            return deger;
        }
        public long VergiHesapla()//Sipariş Kalemine Eklenenlerin Vergisini Hesapla
        {
            long Vergi=0;
            Vergi=TutarHesapla();
            Vergi = Vergi * 18 / 100;
            return Vergi;
        }
        public double AgirlikHesapla() //Sipariş Kalemine Eklenenlerin Ağırlığını Hesapla
        {
            double agirlikTop = 0;
            foreach (SiparisDetay sis in SiparisKalem)
            {
                agirlikTop += (sis.Adet * sis.Agirlik);
            }
            return agirlikTop;
        }
        public void StokGuncelle(string id,int adet) // Satılan ürünün stoğunu güncelle
        {
            SqlCommand gnc = new SqlCommand("Update Tbl_Urunler set Adet=Adet-@a1 where UrunId=@a2", bgl.baglanti());
            gnc.Parameters.AddWithValue("@a1", adet);
            gnc.Parameters.AddWithValue("@a2", id);
            gnc.ExecuteNonQuery();
            bgl.baglanti().Close();
        }
        public void SipIcerikListele(long SipId) //Verilen Siparişin sql'den içeriklerini liste çekiyoruz.
        {
            SqlCommand komutI = new SqlCommand("Select * from Tbl_SiparisDetay where SipDetId= @p1", bgl.baglanti());
            komutI.Parameters.AddWithValue("@p1", SipId);
            SqlDataReader drI = komutI.ExecuteReader();
            while (drI.Read())
            {
                SiparisDetay sdd = new SiparisDetay();
                Urun ur = new Urun();
                sdd.Urun = ur;
                sdd.Urun.UrunAd = drI[1].ToString();
                sdd.Urun.BirimFiyat = Convert.ToInt32(drI[2].ToString());
                sdd.Adet = Convert.ToInt16(drI[3].ToString());
                sdd.Agirlik = Convert.ToDouble(drI[4].ToString());
                KalemEkle(sdd);
            }
            bgl.baglanti().Close();
        }  
    }
}
