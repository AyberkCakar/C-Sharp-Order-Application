using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OkulProje
{
    public class Musteri
    {
        sqlbaglanti bgl = new sqlbaglanti();
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TC { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string KullanıcıID { get; set; }
        public string Sifre { get; set; }
        public List<Siparis> Siparisler = new List<Siparis>(); // Siparişler müşteriye aittir bu yüzden siparişler Müşteri Classı Siparis(Class) türünde bir listedir
        public void SiparisEkle(Siparis sip)
        {
            Siparisler.Add(sip);
        }
        public void MusteriKayit() // Yeni Müşteri Kaydı Yapıyoruz.
        {
            SqlCommand mus = new SqlCommand("insert into Tbl_Musteriler (MAd,MSoyad,MTC,MTelefon,MMail,MSehir,MIlce,MId,MSifre) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", bgl.baglanti());
            mus.Parameters.AddWithValue("@a2", Soyad);
            mus.Parameters.AddWithValue("@a1", Ad);
            mus.Parameters.AddWithValue("@a3", TC);
            mus.Parameters.AddWithValue("@a4", Telefon);
            mus.Parameters.AddWithValue("@a5", Email);
            mus.Parameters.AddWithValue("@a6", Sehir);
            mus.Parameters.AddWithValue("@a7", Ilce);
            mus.Parameters.AddWithValue("@a8", KullanıcıID);
            mus.Parameters.AddWithValue("@a9", Sifre);
            mus.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Kaydınız Yapılmıştır!...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void SiparisSqlEkle() // Verilen siparişi listten çekerek sql e ekle
        {
            foreach (Siparis si in Siparisler)
            {
                SqlCommand km = new SqlCommand("insert into Tbl_Siparisler (SipId,SipAdSoyad,SipDurum,SipTarih,SipKargo,SipAdres,SipAgirlik,SipTutar,SipKullanici) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", bgl.baglanti());
                km.Parameters.AddWithValue("@a1", si.SiparisID);
                km.Parameters.AddWithValue("@a2", si.SahipAdSoyad);
                km.Parameters.AddWithValue("@a3", si.SiparisDurum);
                km.Parameters.AddWithValue("@a4", si.SiparisTarih);
                km.Parameters.AddWithValue("@a5", si.KargoSirket);
                km.Parameters.AddWithValue("@a6", si.Adres);
                km.Parameters.AddWithValue("@a7", si.Agirlik);
                km.Parameters.AddWithValue("@a8", si.ToplamTutar);
                km.Parameters.AddWithValue("@a9", KullanıcıID);
                km.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
        }
        public void SiparisListele() // verilen siparişlerini sqlden liste çekerek müşteriye listele
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Siparisler where SipKullanici=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", KullanıcıID);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Siparis si = new Siparis();
                si.SiparisID = Convert.ToInt32(dr[0]);
                si.SahipAdSoyad = dr[1].ToString();
                si.SiparisDurum = dr[2].ToString();
                si.SiparisTarih = dr[3].ToString();
                si.KargoSirket = dr[4].ToString();
                si.Adres = dr[5].ToString();
                si.Agirlik = dr[6].ToString();
                si.ToplamTutar = dr[7].ToString();
                SiparisEkle(si);
            }
        }
        public void YoneticiSiparisListesi() //Verilen bütün siparişleri liste alarak Yöneticiye Gösteriyoruz.
        {
            SqlCommand komut1 = new SqlCommand("Select * from Tbl_Siparisler", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                Siparis si = new Siparis();
                si.SiparisID = Convert.ToInt32(dr1[0]);
                si.SahipAdSoyad = dr1[1].ToString();
                si.SiparisDurum = dr1[2].ToString();
                si.SiparisTarih = dr1[3].ToString();
                si.KargoSirket = dr1[4].ToString();
                si.Adres = dr1[5].ToString();
                si.Agirlik = dr1[6].ToString();
                si.ToplamTutar = dr1[7].ToString();
                SiparisEkle(si);
            }
        }
        public void SiparisGuncelle() //Yönetici Siparişi Kontrol Edip Sipariş Durumunu güncelliyor.
        {
            foreach(Siparis gez in Siparisler)
            {
                SqlCommand komuts = new SqlCommand("update Tbl_Siparisler set SipDurum=@a1 where SipId=@a2", bgl.baglanti());
                komuts.Parameters.AddWithValue("@a1", gez.SiparisDurum);
                komuts.Parameters.AddWithValue("@a2", gez.SiparisID);
                komuts.ExecuteNonQuery();
                MessageBox.Show("Sipariş Bilgileriniz Güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bgl.baglanti().Close();
            }
        }
        public void MusteriCek(string kullanıcılar) // Müşterinin Kendi Bilgilerini Çek
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Musteriler where MId= @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", kullanıcılar);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Ad = dr[0].ToString();
                Soyad = dr[1].ToString();
                TC = dr[2].ToString();
                Telefon = dr[3].ToString();
                Email = dr[4].ToString();
                Sehir = dr[5].ToString();
                Ilce = dr[6].ToString();
                KullanıcıID = dr[7].ToString();
                Sifre = dr[8].ToString();
            }bgl.baglanti().Close();     
        }
        public void MusteriGuncelle() // Müşteri Bilgilerini Güncelleme
        {
            SqlCommand komut1 = new SqlCommand("update Tbl_Musteriler set MAd=@p9,MSoyad=@p2,MTC=@p3,MTelefon=@p4,MMail=@p5,MSehir=@p6,MIlce=@p7,MSifre=@p8 where MId=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p9", Ad);
            komut1.Parameters.AddWithValue("@p2", Soyad);
            komut1.Parameters.AddWithValue("@p3", TC);
            komut1.Parameters.AddWithValue("@p4", Telefon);
            komut1.Parameters.AddWithValue("@p5", Email);
            komut1.Parameters.AddWithValue("@p6", Sehir);
            komut1.Parameters.AddWithValue("@p7", Ilce);
            komut1.Parameters.AddWithValue("@p8", Sifre);
            komut1.Parameters.AddWithValue("@p1", KullanıcıID);
            komut1.ExecuteNonQuery();
            MessageBox.Show("Hesap Bilgileriniz Güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close();           
        }
        
    }
}
