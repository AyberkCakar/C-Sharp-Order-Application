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

namespace OkulProje
{
    public partial class Yonetici : Form
    {
        public Yonetici()
        {
            InitializeComponent();
        }
        public string kullanici;

        List<Musteri> musteri = new List<Musteri>();  //Yeni müşteri için listemiz
        public void MusteriEkle(Musteri mus)
        {
            musteri.Add(mus);
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAna_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;
        }

        private void btnHesabim_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
        }
        DataTable tablo, tbMs;
        public void MusteriListele()  //Sistemimizdeki bütün müşteri bilglerini sqlden alıyoruz
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Musteriler", bagla.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Musteri ms = new Musteri();
                ms.Ad = dr[0].ToString();
                ms.Soyad = dr[1].ToString();
                ms.TC = dr[2].ToString();
                ms.Telefon = dr[3].ToString();
                ms.Email = dr[4].ToString();
                ms.Sehir = dr[5].ToString();
                ms.Ilce = dr[6].ToString();
                ms.KullanıcıID = dr[7].ToString();
                ms.Sifre = dr[8].ToString();
                MusteriEkle(ms);
            }
            bagla.baglanti().Close();
        }
        void UrunListele()  //Listview ürün tablolarını oluşturuyoruz.
        {
            tablo = new DataTable();
            listView1.View = View.Details;
            listView1.Columns.Add("Urun ID", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Urun Ad", 180, HorizontalAlignment.Left);
            listView1.Columns.Add("Marka", 140, HorizontalAlignment.Left);
            listView1.Columns.Add("Model", 130, HorizontalAlignment.Left);
            listView1.Columns.Add("Yıl", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Alış", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Satış", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Ağırlık", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Adet", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Açıklama", 400, HorizontalAlignment.Left);
            tablo.Columns.Add("Urun ID", typeof(string));
            tablo.Columns.Add("Urun Ad", typeof(string));
            tablo.Columns.Add("Marka", typeof(string));
            tablo.Columns.Add("Model", typeof(string));
            tablo.Columns.Add("Yıl", typeof(string));
            tablo.Columns.Add("Alış", typeof(string));
            tablo.Columns.Add("Satış", typeof(string));
            tablo.Columns.Add("Ağırlık", typeof(float));
            tablo.Columns.Add("Adet", typeof(float));
            tablo.Columns.Add("Aciklama", typeof(string));
        }
        void MusteriListesi()
        {
            tbMs = new DataTable();   //Listview müşteri tablolarını oluşturuyoruz.
            listView2.View = View.Details;
            listView2.Columns.Add("Müşteri AD", 200, HorizontalAlignment.Left);
            listView2.Columns.Add("Soyad", 150, HorizontalAlignment.Left);
            listView2.Columns.Add("TC", 150, HorizontalAlignment.Left);
            listView2.Columns.Add("Telefon", 150, HorizontalAlignment.Left);
            listView2.Columns.Add("E-Mail", 250, HorizontalAlignment.Left);
            listView2.Columns.Add("Sehir", 150, HorizontalAlignment.Left);
            listView2.Columns.Add("Ilçe", 150, HorizontalAlignment.Left);
            listView2.Columns.Add("ID", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("Şifre", 100, HorizontalAlignment.Left);
            tbMs.Columns.Add("Müşteri AD", typeof(string));
            tbMs.Columns.Add("Soyad", typeof(string));
            tbMs.Columns.Add("TC", typeof(string));
            tbMs.Columns.Add("Telefon", typeof(string));
            tbMs.Columns.Add("E-Mail", typeof(string));
            tbMs.Columns.Add("Sehir", typeof(string));
            tbMs.Columns.Add("Ilçe", typeof(float));
            tbMs.Columns.Add("ID", typeof(string));
            tbMs.Columns.Add("Şifre", typeof(string));

        }
        void SilTamamla() //Ürün Sildikten sonra txt leri sıfırla
        {
            txtUrunAdet.Text = "";
            txtUrunAd.Text = "";
            txtUrunAgirlik.Text = "";
            txtUrunAlis.Text = "";
            txtUrunID.Text = "";
            txtUrunMarka.Text = "";
            txtUrunModel.Text = "";
            txtUrunSatis.Text = "";
            txtUrunToplam.Text = "";
            txtUrunVergi.Text = "";
            txtUrunYil.Text = "";
            rchUrunAciklama.Text = "";
        }
        private void btnUrunler_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 3;  // Ürünler butonuna tıklayınca bütün ürünleri sqlden al
            listView1.Items.Clear();
            sqlbaglanti bgl = new sqlbaglanti();
            SqlCommand komut = new SqlCommand("Select * from Tbl_Urunler", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                string[] satir = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[7].ToString(), dr[5].ToString(), dr[8].ToString(), dr[9].ToString(), dr[6].ToString() };
                var ekle = new ListViewItem(satir);
                listView1.Items.Add(ekle);
            }
            bgl.baglanti().Close();
        }
        void Istatistikler()
        {
            int musterisay = 0,UrunSay = 0;
            MusteriListele();
            foreach (Musteri m in musteri)
            {
                musterisay++;
            }
            lblMsuteriSay.Text = musterisay.ToString();
            sqlbaglanti bgl = new sqlbaglanti();
            SqlCommand komut = new SqlCommand("Select * from Tbl_Urunler", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                UrunSay++;
            }
            lblTopUrun.Text = UrunSay.ToString();
            bgl.baglanti().Close();
            musteri.Clear();
        }
        private void btnUrunEkle_Click_1(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 4;
        }
        Musteri mst = new Musteri();
        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();   //Müsterileri listten listview e doldur
            xtraTabControl1.SelectedTabPageIndex = 5;
            MusteriListele();
            foreach (Musteri m in musteri)
            {
                string[] satir1 = { m.Ad, m.Soyad, m.TC, m.Telefon, m.Email, m.Sehir, m.Ilce, m.KullanıcıID, m.Sifre };
                var muster = new ListViewItem(satir1);
                listView2.Items.Add(muster);
            }
            musteri.Clear();
        }
        void ListViewTablo()
        {
            DataTable tablo2 = new DataTable();  //Listview sipariş tablolarını oluşturuyoruz.
            listView3.View = View.Details;
            listView3.Columns.Add("SiparisID", 125, HorizontalAlignment.Left);
            listView3.Columns.Add("Sahip", 250, HorizontalAlignment.Left);
            listView3.Columns.Add("Fiyat", 75, HorizontalAlignment.Left);
            listView3.Columns.Add("Vergi", 75, HorizontalAlignment.Left);
            listView3.Columns.Add("Toplam", 75, HorizontalAlignment.Left);
            listView3.Columns.Add("Tarih", 125, HorizontalAlignment.Left);
            listView3.Columns.Add("Durum", 250, HorizontalAlignment.Left);
            listView3.Columns.Add("Kargo", 200, HorizontalAlignment.Left);
            listView3.Columns.Add("Ağırlık", 75, HorizontalAlignment.Left);
            listView3.Columns.Add("Adres", 150, HorizontalAlignment.Left);
            tablo2.Columns.Add("SiparisID", typeof(int));
            tablo2.Columns.Add("Sahip", typeof(string));
            tablo2.Columns.Add("Fiyat", typeof(string));
            tablo2.Columns.Add("Vergi", typeof(string));
            tablo2.Columns.Add("Toplam", typeof(string));
            tablo2.Columns.Add("Tarih", typeof(string));
            tablo2.Columns.Add("Durum", typeof(string));
            tablo2.Columns.Add("Kargo", typeof(string));
            tablo2.Columns.Add("Ağırlık", typeof(string));
            tablo2.Columns.Add("Adres", typeof(string));
        }
        void SiparisList()  //Verilen bütün siparişlerimizi Musteri classımızın içindeki Sipariş listesine alıp listview e dolduruyoruz.
        {
            Musteri msc = new Musteri();
            listView3.Items.Clear();
            msc.YoneticiSiparisListesi();
            foreach (Siparis sp in msc.Siparisler)
            {
                string[] siparis = { sp.SiparisID.ToString(), sp.SahipAdSoyad, (Convert.ToInt32(sp.ToplamTutar) * 100 / 118).ToString(), ((Convert.ToInt32(sp.ToplamTutar) * 18 / 118)).ToString(), sp.ToplamTutar, sp.SiparisTarih, sp.SiparisDurum, sp.KargoSirket, sp.Agirlik, sp.Adres };
                var ekle = new ListViewItem(siparis);
                listView3.Items.Add(ekle);
            }
            msc.Siparisler = new List<Siparis>();

        }
        private void btnIstatikler_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 6;
            Istatistikler();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 2;
            SiparisList();

        }
        sqlbaglanti bagla = new sqlbaglanti();
        void YoneticiCek(string kullanici)  //Yöneticimizin bilgilerini çekiyoruz.
        {
            SqlCommand komut = new SqlCommand("Select * from Tbl_Yonetici where YID= @p1", bagla.baglanti());
            komut.Parameters.AddWithValue("@p1", kullanici);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblYID.Text = dr[0].ToString();
                txtYSifre.Text = dr[1].ToString();
                txtYAd.Text = dr[2].ToString();
                txtYSoyad.Text = dr[3].ToString();
                txtYPozisyon.Text = dr[4].ToString();
                mskYTC.Text = dr[5].ToString();
                mskYTelefon.Text = dr[6].ToString();
                txtYMail.Text = dr[7].ToString();
                txtYSehir.Text = dr[8].ToString();
                txtYIlce.Text = dr[9].ToString();
            }
            bagla.baglanti().Close();

            button5.Text = "Sayın " + txtYPozisyon.Text + " , " + txtYAd.Text + " " + txtYSoyad.Text;  // Üstteki Kırmızı butona Müşterimizin ismini soy ismini yazıyoruz.
        }
        public void YoneticiGuncelle() //Yönetici Bilgilerini Güncelliyoruz.
        {
            SqlCommand komut1 = new SqlCommand("update Tbl_Yonetici set YIlce=@p10,YSifre=@p2,YAd=@p3,YSoyad=@p4,YPozisyon=@p5,YTC=@p6,YTelefon=@p7,YMail=@p8,YSehir=@p9 where YID=@p1", bagla.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblYID.Text);
            komut1.Parameters.AddWithValue("@p2", txtYSifre.Text);
            komut1.Parameters.AddWithValue("@p3", txtYAd.Text);
            komut1.Parameters.AddWithValue("@p4", txtYSoyad.Text);
            komut1.Parameters.AddWithValue("@p5", txtYPozisyon.Text);
            komut1.Parameters.AddWithValue("@p6", mskYTC.Text);
            komut1.Parameters.AddWithValue("@p7", mskYTelefon.Text);
            komut1.Parameters.AddWithValue("@p8", txtYMail.Text);
            komut1.Parameters.AddWithValue("@p9", txtYSehir.Text);
            komut1.Parameters.AddWithValue("@p10", txtYIlce.Text);
            komut1.ExecuteNonQuery();
            MessageBox.Show("Hesap Bilgileriniz Güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bagla.baglanti().Close();
        }
        int OnayBek = 0, Onayli = 0, Teslim = 0,SipSay=0;
        long SipTutar = 0, SipVergi = 0, SipToplam = 0;
        private void Yonetici_Load_1(object sender, EventArgs e)
        {
            YoneticiCek(kullanici);
            UrunListele();
            MusteriListesi();
            ListViewTablo();
            Musteri m = new Musteri();
            m.YoneticiSiparisListesi();
            foreach (Siparis sp in m.Siparisler)
            {
                SipSay++;
                SipToplam += Convert.ToInt32(sp.ToplamTutar);
                SipVergi += ((Convert.ToInt32(sp.ToplamTutar) * 18 / 118));   //Siparişlerimizden gelen
                SipTutar += (Convert.ToInt32(sp.ToplamTutar) * 100 / 118);    // Tutar,Vergi,Toplam değerlerini buluyoruz,
                if (sp.SiparisDurum == "Onay Bekliyor.")                       //Onay bekleyen, Onaylanan,Teslim Edilen sipariş sayılarını buluyoruz.
                    OnayBek++;                                                  
                else if (sp.SiparisDurum == "Onaylandı.")
                    Onayli++;
                else if (sp.SiparisDurum == "Teslim Edildi.")
                    Teslim++;
            }
            double yeniTutar = Convert.ToDouble(SipTutar);
            double yeniVergi = Convert.ToDouble(SipVergi);
            double yeniToplam = Convert.ToDouble(Convert.ToInt32(SipToplam));
            string Tuyeni = string.Format("{0:0,0}", yeniTutar).Replace(",", ".");
            string Vyeni = string.Format("{0:0,0}", yeniVergi).Replace(",", ".");
            string ToYeni = string.Format("{0:0,0}", yeniToplam).Replace(",", ".");
            m.Siparisler = new List<Siparis>();
            lblOnayBekleyen.Text = OnayBek.ToString();
            lblOnaylanmis.Text = Onayli.ToString();
            lblTeslimEdilmis.Text = Teslim.ToString();
            TopSatıs.Text = Tuyeni.ToString();
            lblTopVergi.Text = Vyeni.ToString();
            lblTopToplam.Text =ToYeni.ToString();
            lblToplamSiparis.Text = SipSay.ToString();
            chartControl1.Series["Series 1"].Points.AddPoint("Onaylanan", Onayli);         //Ve Chartlarımıza dolduruyoruz
            chartControl1.Series["Series 1"].Points.AddPoint("Onay Bekleyen", OnayBek);
            chartControl1.Series["Series 1"].Points.AddPoint("Teslim Edilen", Teslim);
            chartControl2.Series["Series 1"].Points.AddPoint("Tutar", SipTutar);
            chartControl2.Series["Series 1"].Points.AddPoint("Vergi", SipVergi);
            chartControl2.Series["Series 1"].Points.AddPoint("Toplam", SipToplam);
        }
        private void btnKGuncelle_Click(object sender, EventArgs e)
        {
            YoneticiGuncelle();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtYSifre.UseSystemPasswordChar == false)
                txtYSifre.UseSystemPasswordChar = true;
            else if (txtYSifre.UseSystemPasswordChar == true)
                txtYSifre.UseSystemPasswordChar = false;
        }
        void urunSifirla()
        {
            txtUAd.Text = "";
            txtAlisFiyat.Text = "";
            txtSatisFiyat.Text = "";
            txtUAdet.Text = "";
            txtUAgirlik.Text = "";
            txtUMarka.Text = "";
            txtUModel.Text = "";
            rchAciklama.Text = "";
            cmbUYıl.Text = "";
        }
        private void btnUEkle_Click(object sender, EventArgs e) // Ürün ekleme yapıyoruz
        {
            if (txtUAgirlik.Text != "" && txtUMarka.Text != "" && txtUAd.Text != "" && txtUModel.Text != "" && cmbUYıl.Text != "" && rchAciklama.Text != "" && txtAlisFiyat.Text != "" && txtSatisFiyat.Text != "" && txtUAdet.Text != "")
            {
                SqlCommand ek = new SqlCommand("insert into Tbl_Urunler (Ad,Marka,Model,Yıl,Satis,Acıklama,Alis,Agirlik,Adet) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", bagla.baglanti());
                ek.Parameters.AddWithValue("@a1", txtUAd.Text);
                ek.Parameters.AddWithValue("@a2", txtUMarka.Text);
                ek.Parameters.AddWithValue("@a3", txtUModel.Text);
                ek.Parameters.AddWithValue("@a4", cmbUYıl.Text);
                ek.Parameters.AddWithValue("@a5", txtSatisFiyat.Text);
                ek.Parameters.AddWithValue("@a6", rchAciklama.Text);
                ek.Parameters.AddWithValue("@a7", txtAlisFiyat.Text);
                ek.Parameters.AddWithValue("@a8", txtUAgirlik.Text);
                ek.Parameters.AddWithValue("@a9", txtUAdet.Text);
                ek.ExecuteNonQuery();
                bagla.baglanti().Close();
                MessageBox.Show("Ürününüz Eklenmiştir!...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                urunSifirla();
            }
            else
            {
                MessageBox.Show("Ürün Bilgilerini Doldurunuz!...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUGuncelle_Click(object sender, EventArgs e) // Ürün güncelleme yapıyoruz.
        {
            if (txtUrunID.Text != "")
            {
                SqlCommand gn = new SqlCommand("Update Tbl_Urunler set Ad=@a1,Marka=@a2,Model=@a3,Yıl=@a4,Satis=@a5,Acıklama=@a6,Alis=@a7,Agirlik=@a8,Adet=@a9 where UrunId=@a10", bagla.baglanti());
                gn.Parameters.AddWithValue("@a10", txtUrunID.Text);
                gn.Parameters.AddWithValue("@a1", txtUrunAd.Text);
                gn.Parameters.AddWithValue("@a2", txtUrunMarka.Text);
                gn.Parameters.AddWithValue("@a3", txtUrunModel.Text);
                gn.Parameters.AddWithValue("@a4", txtUrunYil.Text);
                gn.Parameters.AddWithValue("@a5", txtUrunSatis.Text);
                gn.Parameters.AddWithValue("@a6", rchUrunAciklama.Text);
                gn.Parameters.AddWithValue("@a7", txtUrunAlis.Text);
                gn.Parameters.AddWithValue("@a8", Convert.ToDouble(txtUrunAgirlik.Text));
                gn.Parameters.AddWithValue("@a9", Convert.ToInt16(txtUrunAdet.Text));
                gn.ExecuteNonQuery();
                bagla.baglanti().Close();
                MessageBox.Show("Ürününüz Güncellenmiştir!...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SilTamamla();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OdemeGortuntule og = new OdemeGortuntule();
            og.id = Convert.ToInt32(lblSipId.Text);
            og.Show();
            
        }

        private void btnUSil_Click(object sender, EventArgs e) // Ürün Siliyoruz
        {
            SqlCommand sil = new SqlCommand("Delete From Tbl_Urunler where UrunId = @p1", bagla.baglanti());
            sil.Parameters.AddWithValue("@p1", txtUrunID.Text);
            sil.ExecuteNonQuery();
            bagla.baglanti().Close();
            MessageBox.Show("Ürün Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            SilTamamla();
        }
        Siparis sip = new Siparis();


        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0) //Listviev3 de satır seçerse ve eğer varsa urunler sayfasındaki bilgileri seçili ürünle doldursun
            {

                listSiparisIcerik.Items.Clear();//Her seferinde listi sıfırla
                ListViewItem item = listView3.SelectedItems[0];
                lblSipId.Text = item.SubItems[0].Text;
                lblAd.Text = item.SubItems[1].Text;
                lblSipTutar.Text = item.SubItems[2].Text;
                lblSipVergi.Text = item.SubItems[3].Text;
                lblSipToplam.Text = item.SubItems[4].Text;
                lblTarih.Text = item.SubItems[5].Text;
                lblSipKargo.Text = item.SubItems[7].Text;
                if (item.SubItems[6].Text == "Onay Bekliyor.")
                { cmbDurum.SelectedIndex = 0; }
                else if (item.SubItems[6].Text == "Onaylandı.")
                {
                    cmbDurum.SelectedIndex = 1;
                }
                else if (item.SubItems[6].Text == "Teslim Edildi.")
                {
                    cmbDurum.SelectedIndex = 2;
                }
                lblSipAgirlik.Text = item.SubItems[8].Text;
                richTextBox1.Text = item.SubItems[9].Text;
                sip.SipIcerikListele(Convert.ToInt32(item.SubItems[0].Text));
                foreach (SiparisDetay a in sip.SiparisKalem)
                {
                    listSiparisIcerik.Items.Add(a.Urun.UrunAd + "    " + a.Urun.BirimFiyat + " TL   " + " X " + a.Adet + " Adet  " + a.Agirlik + " Kg");
                }
                sip.SiparisKalem.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0) //Listviev3de satır seçerse o satırın sipariş durumunu bana getirsin
            {
                ListViewItem item = listView3.SelectedItems[0];
                lblSipId.Text = item.SubItems[0].Text;
            }
            Musteri ms = new Musteri();
            Siparis sip = new Siparis();
            sip.SiparisDurum = cmbDurum.Text;  // Yeni durumu  Siparişimizin Durumuna Atıyoruz.
            sip.SiparisID = Convert.ToInt32(lblSipId.Text);
            ms.SiparisEkle(sip);  // sipariş id ve durumu liste ekliyoruz
            ms.SiparisGuncelle();  //Sipariş listesinden sipariş durumunu güncelliyoruz.
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) //Listviev1 de satır seçerse ve eğer varsa urunler sayfasındaki bilgileri seçili ürünle doldursun
            {
                ListViewItem item = listView1.SelectedItems[0];
                txtUrunID.Text = item.SubItems[0].Text;
                txtUrunAd.Text = item.SubItems[1].Text;
                txtUrunMarka.Text = item.SubItems[2].Text;
                txtUrunModel.Text = item.SubItems[3].Text;
                txtUrunYil.Text = item.SubItems[4].Text;
                txtUrunAlis.Text = item.SubItems[5].Text;
                txtUrunSatis.Text = item.SubItems[6].Text;
                txtUrunToplam.Text = ((Convert.ToInt32(item.SubItems[5].Text)) + (Convert.ToInt32(item.SubItems[5].Text) * 18 / 100)).ToString();
                txtUrunVergi.Text = (Convert.ToInt32(item.SubItems[5].Text) * 18 / 100).ToString();
                txtUrunAgirlik.Text = item.SubItems[7].Text;
                txtUrunAdet.Text = item.SubItems[8].Text;
                rchUrunAciklama.Text = (item.SubItems[9].Text);
            }
        }
    }
}
