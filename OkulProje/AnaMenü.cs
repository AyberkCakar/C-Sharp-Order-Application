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
    public partial class AnaMenü : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public AnaMenü()
        {
            InitializeComponent();
        }
        public string kullanıcı;
        CreditCard cc = new CreditCard();
        Siparis sip = new Siparis();
        DateTime dtime = DateTime.Today;
        Musteri msc = new Musteri();
        sqlbaglanti bg = new sqlbaglanti(); // sql bağlantı classımızdan türetiyoruz.
        Urun icerik = new Urun();
        KapidaOdeme kp = new KapidaOdeme();
        CreditCard cdc = new CreditCard();
        DataTable tablo,tablo1,tablo2;
        ////////////////////////////////////////////////////////MÜŞTERİ BÖLÜMÜ
        private void bt_Click(object sender, EventArgs e) // Hesap Bilgileri Butonumuz
        {
            xtraTabControl1.SelectedTabPageIndex = 1; // xtraTabPagenin 2. sayfasını açıyoruz.
        }
        void MusteriBilgiCek(string kullanıcı) // Müşterimizin Bilgilerini Kontrol Paneline Alıyoruz
        {
            msc.MusteriCek(kullanıcı); // Müşteri Classımızdan Kullanıcıya göre çekiyoruz
            if (kullanıcı == msc.KullanıcıID)
            {
                Siparis s = new Siparis();
                s.SahipAdSoyad = msc.Ad + " " + msc.Soyad;
                txtKAd.Text = msc.Ad;
                txtKSoyad.Text = msc.Soyad;
                lblKID.Text = msc.KullanıcıID;
                txtKSehir.Text = msc.Sehir;
                txtKIlce.Text = msc.Ilce;
                txtKSifre.Text = msc.Sifre;
                mskKTC.Text = msc.TC;
                mskKTelefon.Text = msc.Telefon;
                txtKMail.Text = msc.Email;
                button5.Text = "Sayın Müşterimiz, " + msc.Ad + " " + msc.Soyad;  // Üstteki Kırmızı butona Müşterimizin ismini soy ismini yazıyoruz.
            }
        }
        void MusteriBilgiGuncelle() // Müşterimizin Bilgi Güncellemesini Sağlıyoruz.
        {
            Musteri ms = new Musteri();
            ms.Ad = txtKAd.Text;
            ms.Soyad = txtKSoyad.Text;
            ms.Sehir = txtKSehir.Text;
            ms.Ilce = txtKIlce.Text;
            ms.Sifre = txtKSifre.Text;
            ms.TC = mskKTC.Text;
            ms.Telefon = mskKTelefon.Text;
            ms.Email = txtKMail.Text;
            ms.KullanıcıID = lblKID.Text;
            ms.MusteriGuncelle(); // Yeni bilgilerini Çekiyoruz.
            txtKSifre.UseSystemPasswordChar = false;
        }
        private void btnKGuncelle_Click(object sender, EventArgs e)//Bilgileri GÜncelle dersek güncellesin ve bilgileri tekrar çeksin
        {
            MusteriBilgiGuncelle();
            MusteriBilgiCek(kullanıcı);
        }
        private void button13_Click(object sender, EventArgs e) // Kullanıcı Güvenliği için şifre gizliyoruz
        {                                                       // Göster- Gizle İle gösterip gizliyoruz
            if (txtKSifre.UseSystemPasswordChar == false)
                txtKSifre.UseSystemPasswordChar = true;
            else if (txtKSifre.UseSystemPasswordChar == true)
                txtKSifre.UseSystemPasswordChar = false;
        }
        //////////////////////////////////////////////////////
        private void button3_Click(object sender, EventArgs e) // Anaformdaki Çıkış Butonumuz
        {
            this.Close();
        }

        ////////////////////////////////////////////////////// URUNLER
        private void button1_Click(object sender, EventArgs e)//ÜRÜN Butonumuz
        {
            UrunList();
            xtraTabControl1.SelectedTabPageIndex = 0;// xtraTabPagenin 1. sayfasını açıyoruz.
        }

        public void UrunList() // Bütün ürünleri listeliyoruz
        {
            listView1.Items.Clear();
            sqlbaglanti bgl = new sqlbaglanti();
            SqlCommand komut = new SqlCommand("Select * from Tbl_Urunler", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            { 
                string[] satir = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[8].ToString(),dr[9].ToString(), dr[6].ToString() };
                var ekle = new ListViewItem(satir);
                listView1.Items.Add(ekle);
            }
            bgl.baglanti().Close();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) //Listviev1 de satır seçerse ve eğer varsa urunler sayfasındaki bilgileri seçili ürünle doldursun
            {
                ListViewItem item = listView1.SelectedItems[0];
                lblUrunID.Text = item.SubItems[0].Text;
                lblUrunAD.Text = item.SubItems[1].Text;
                lblUrunMarka.Text = item.SubItems[2].Text;
                lblUrunModel.Text = item.SubItems[3].Text;
                lblUrunYıl.Text = item.SubItems[4].Text;
                lblUrunFiyat.Text = item.SubItems[5].Text;
                lblUrunToplam.Text = ((Convert.ToInt32(item.SubItems[5].Text))+(Convert.ToInt32(item.SubItems[5].Text) * 18 / 100)).ToString();
                lblUrunVergi.Text = (Convert.ToInt32(item.SubItems[5].Text)*18/100).ToString();
                lblUAgirlik.Text = item.SubItems[6].Text;
                lblStok.Text = item.SubItems[7].Text;
                rchUrunAciklama.Text=item.SubItems[8].Text;
            }
        }

        //////////////////////////////////////////////////////  SEPET
        private void button2_Click(object sender, EventArgs e)//SEPET Butonumuz
        {
            xtraTabControl1.SelectedTabPageIndex = 3;// xtraTabPagenin 4. sayfasını açıyoruz.
        }
        void SepetList()  //Listview tablolarını oluşturuyoruz
        {
            tablo1 = new DataTable();
            listView2.View = View.Details;
            listView2.Columns.Add("Urun", 425, HorizontalAlignment.Left);
            listView2.Columns.Add("Satis", 125, HorizontalAlignment.Left);
            listView2.Columns.Add("Adet", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("Toplam", 125, HorizontalAlignment.Left);        
            tablo1.Columns.Add("Urun", typeof(string));
            tablo1.Columns.Add("Fiyat", typeof(string));
            tablo1.Columns.Add("Adet", typeof(string));
            tablo1.Columns.Add("Toplam", typeof(string));

            tablo2 = new DataTable();
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

            tablo = new DataTable();
            listView1.View = View.Details;
            listView1.Columns.Add("Urun ID", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Urun Ad", 180, HorizontalAlignment.Left);
            listView1.Columns.Add("Marka", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Model", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Yıl", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Satis", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Ağırlık", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Adet", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Açıklama", 370, HorizontalAlignment.Left);
            tablo.Columns.Add("Urun ID", typeof(string));
            tablo.Columns.Add("Urun Ad", typeof(string));
            tablo.Columns.Add("Marka", typeof(string));
            tablo.Columns.Add("Model", typeof(string));
            tablo.Columns.Add("Yıl", typeof(string));
            tablo.Columns.Add("Fiyat", typeof(string));
            tablo.Columns.Add("Ağırlık", typeof(float));
            tablo.Columns.Add("Adet", typeof(float));
            tablo.Columns.Add("Aciklama", typeof(string));
        }   
        public void SepetEkle()
        {
            SiparisDetay detay = new SiparisDetay(); 
            Urun ur = new Urun();
            detay.Urun = ur; // SiparisDetay ve Urun arasında aggregation  bağ kuruyoruz
            detay.Urun.UrunAd=lblUrunAD.Text+" "+lblUrunMarka.Text+" "+lblUrunModel.Text; // Seçilen Ürünü Sepet Arayüzüne Ekle
            detay.Urun.BirimFiyat = Convert.ToInt32(lblUrunFiyat.Text);
            detay.Adet = Convert.ToInt16(cmdAdet.Text);
            detay.Agirlik = Convert.ToDouble(lblUAgirlik.Text);
            sip.KalemEkle(detay);   // Sepete Eklenen ürünü sipariş kalemi olarak liste ekle
            MessageBox.Show("Ürününüz Sepete Eklendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(lblStok.Text) >= Convert.ToInt32(cmdAdet.Text)) // Stok Kontrolü yapıyoruz, Seçilen üründen istenilen kadar yoksa stok yok hatası veriyoruz.
            {
                string[] satir = { lblUrunAD.Text + "  " + lblUrunMarka.Text + "  " + lblUrunModel.Text + "  " + lblUrunYıl.Text, lblUrunFiyat.Text, " X " + cmdAdet.Text, (Convert.ToInt32(lblUrunFiyat.Text) * Convert.ToInt32(cmdAdet.Text)).ToString() };
                var ekle = new ListViewItem(satir);
                listView2.Items.Add(ekle);
                if (lblUrunID.Text == "" && lblUrunAD.Text == "")
                { MessageBox.Show("Ürün Seçiniz...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else
                {
                    SepetEkle();
                    long deger,degerVergi;  
                    deger = sip.TutarHesapla();
                    degerVergi = sip.VergiHesapla();  //Ürünü sepete ekledikten sonra güncel Tutar, Vergi, Toplam ,Agirlik bilgilerini tekrar hesaplıyoruz.
                    lblAVTutar.Text = deger.ToString();
                    lblAVVergi.Text = degerVergi.ToString();
                    lblAVToplam.Text = (deger + degerVergi).ToString();
                    lblKargoAgirlik.Text = (sip.AgirlikHesapla()).ToString();
                    if (lblUrunID.Text != "")
                    {
                        sip.StokGuncelle(lblUrunID.Text, Convert.ToInt16(cmdAdet.Text));
                    }
                }
                cmdAdet.SelectedIndex = 0; //Adeti ürünü ekledikten sonra 1 e getiriyoruz.
            }
            else
            {
                MessageBox.Show("Seçtiğiniz Kadar Stok Yok...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //////////////////////////////////////////////////////  SİPARİS
        private void button6_Click(object sender, EventArgs e)// Sipariş Butonumuz
        {
            xtraTabControl1.SelectedTabPageIndex = 2; // xtraTabPagenin 3. sayfasını açıyoruz.
            SiparisList();
        }
        void SiparisList() // Siparişlerimizi listeliyoruz
        {
            listView3.Items.Clear(); //Her seferinde listview3 ü sıfırlıyoruz
            msc.SiparisListele(); // listimizi tekrar sqlden doldurup çağırıyoruz
            foreach (Siparis sp in msc.Siparisler)
            {
                string[] siparis = { sp.SiparisID.ToString(), sp.SahipAdSoyad, (Convert.ToInt32(sp.ToplamTutar)*100/118).ToString(), ( (Convert.ToInt32(sp.ToplamTutar) * 18/ 118)).ToString(),sp.ToplamTutar,sp.SiparisTarih,sp.SiparisDurum, sp.KargoSirket, sp.Agirlik, sp.Adres };
                var ekle = new ListViewItem(siparis);
                listView3.Items.Add(ekle);
            }
            msc.Siparisler = new List<Siparis>(); // Sipariş listesini sıfırlıyoruz.
        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0) //Listviev1 de satır seçerse ve eğer varsa urunler sayfasındaki bilgileri seçili ürünle doldursun
            {
                listSiparisIcerik.Items.Clear();
                ListViewItem item = listView3.SelectedItems[0];
                lblSipId.Text = item.SubItems[0].Text;
                lblAd.Text = item.SubItems[1].Text;
                lblSipTutar.Text = item.SubItems[2].Text;
                lblSipVergi.Text = item.SubItems[3].Text;
                lblSipToplam.Text = item.SubItems[4].Text;
                lblTarih.Text = item.SubItems[5].Text;
                lblSipKargo.Text = item.SubItems[7].Text;
                lblDurum.Text = item.SubItems[6].Text;
                lblSipAgirlik.Text = item.SubItems[8].Text;
                rchSipAdres.Text=item.SubItems[9].Text;
                sip.SipIcerikListele(Convert.ToInt32(item.SubItems[0].Text));
                foreach(SiparisDetay a in sip.SiparisKalem)
                {
                    listSiparisIcerik.Items.Add(a.Urun.UrunAd + "    " + a.Urun.BirimFiyat + " TL   " + " X " + a.Adet + " Adet  ");
                }
                sip.SiparisKalem.Clear();
            }
        }
        //////////////////////////////////////////////////////  ODEME
        private void button9_Click(object sender, EventArgs e)//Ödeme Butonumuz
        {
            xtraTabControl1.SelectedTabPageIndex = 4;// xtraTabPagenin 5. sayfasını açıyoruz.
        }
        private void btnKart_Click(object sender, EventArgs e)
        {
            xtraTabControl2.SelectedTabPageIndex = 0; //tabcontol2 1. sayfa (Kart)
            lblKargo.Visible = false; // Kargo Ödemeleri görünmez olsun
            lblKargoUcreti.Visible = false;
            AVTamamlaHesaplar();
            butonchech();
        }
        private void btnCek_Click(object sender, EventArgs e)
        {
            xtraTabControl2.SelectedTabPageIndex = 2;//tabcontol2 3. sayfa (Çek)
            lblKargo.Visible = false;// Kargo Ödemeleri görünmez olsun
            lblKargoUcreti.Visible = false;
            AVTamamlaHesaplar();
            butonchech();
        }
        
        private void btnOdeme_Click(object sender, EventArgs e)
        {
            string tarih;
            if (lblOdemeTutar.Text != "0" && lblOdemeTutar.Text != "00" && ( Convert.ToInt32(lblOdemeToplam.Text) == (Convert.ToInt32(txtCekPara.Text) + Convert.ToInt32(txtKartPara.Text) + Convert.ToInt32(txtKapıdaPara.Text))))
            {
                if(rchAdres.Text!=""&&lblKargoSirket.Text!="" )   //Eğer Gerekli Yerler Doluysa Devam et değilse eksik olan yerlerle ilgili hata ver.
                {
                    long Ido;
                    Siparis s = new Siparis();
                    Musteri msc = new Musteri();
                    Ido = s.IDOlustur(); // Yeni Sip id oluştur
                    tarih = dtime.Day + "." + dtime.Month + "." + dtime.Year; //Güncel Tarihi al
                    s.SiparisID = Ido;
                    s.SahipAdSoyad = txtKAd.Text+" "+txtKSoyad.Text;
                    s.KargoSirket = lblKargoSirket.Text;
                    s.SiparisTarih = tarih;
                    s.Adres = rchAdres.Text;
                    s.Agirlik = lblKargoAgirlik.Text;
                    s.ToplamTutar = lblOdemeToplam.Text;
                    msc.KullanıcıID = kullanıcı;
                    msc.SiparisEkle(s); // Siparişi liste ekle
                    msc.SiparisSqlEkle(); //Listi Sqle ekle
                    sip.KalemEklesql(Ido); // İçeriklerimizi Sqle ekle
                    Siparis odm = new Siparis();
                    if (checkKart.Checked == true) //  Ödemelerimizde CrediKart Ödeme seçili ise
                    {
                        if (mskKartNo.Text != "" && mskCVV.Text != "" && cmbAy.Text != "" && cmbYıl.Text != "" && cmbKartTipleri.Text != "") // Ve bilgileri tam doluysa
                        {
                            CreditCard cdco = new CreditCard();   // Yeni bir crediKartı Nesnesi oluştur ve bilgileri doldur.
                            cdco.Tutar = Convert.ToInt32(txtKartPara.Text);
                            cdco.KartOdemeID = Ido;
                            cdco.KartAy = Convert.ToInt16(cmbAy.Text);
                            cdco.KartCVV = Convert.ToInt16(mskCVV.Text);
                            cdco.KartNo = mskKartNo.Text;
                            cdco.KartTip = cmbKartTipleri.Text;
                            cdco.KartYıl = Convert.ToInt16(cmbYıl.Text);
                            odm.OdemeEkle(cdco); // Alınan Bilgileri sipariş içinde oluşturulmuş olan ödeme listesine CrediKart(class) Olarak ekle
                            KartTemizle();
                        }
                        else
                        {
                            MessageBox.Show("Eksik Yerleri Doldurunuz Yada Ödeme Kutularını Kontrol Ediniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    if (checkKapıda.Checked == true) //  Ödemelerimizde Kapıda Ödeme seçili ise
                    {
                        if (txtKapıdaPara.Text != "" )
                        {
                            KapidaOdeme kpo = new KapidaOdeme();   // Yeni bir KapıdaÖdeme Nesnesi oluştur ve bilgileri doldur.
                            kpo.Tutar = Convert.ToInt32(txtKapıdaPara.Text);
                            kpo.KapidaOdemeID = Ido;
                            kpo.KargoUcret = Convert.ToInt32(lblKargoUcreti.Text);
                            odm.OdemeEkle(kpo);  // Alınan Bilgileri sipariş içinde oluşturulmuş olan ödeme listesine KapıdaOdeme(class) Olarak ekle
                        }
                        else
                            MessageBox.Show("Eksik Yerleri Doldurunuz Yada Ödeme Kutularını Kontrol Ediniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (checkCek.Checked == true)  //  Ödemelerimizde Çek Ödeme seçili ise
                    {                                                                                     // Ve bilgileri tam doluysa
                        if (txtCekBanka.Text != "" && txtCekBasım.Text != "" && txtCekKime.Text != "" && txtCekSahip.Text != "" && txtCekSube.Text != "" && mskCekHesapNo.Text != "" && mskCekIBAN.Text != "" && mskCekNumarasi.Text != "")
                        {
                            Cek cek = new Cek();    // Yeni bir Çek Nesnesi oluştur ve bilgileri doldur.
                            cek.Tutar = Convert.ToInt32(txtCekPara.Text);
                            cek.CekOdemeID = Ido;
                            cek.Banka = txtCekBanka.Text;
                            cek.CBasimTarihi = txtCekBasım.Text;
                            cek.CekKime = txtCekKime.Text;
                            cek.CekNo = mskCekNumarasi.Text;
                            cek.CekSahibi = txtCekSahip.Text;
                            cek.HesapNo = mskCekHesapNo.Text;
                            cek.IBAN = mskCekIBAN.Text;
                            cek.Sube = txtCekSube.Text;
                            odm.OdemeEkle(cek);   // Alınan Bilgileri sipariş içinde oluşturulmuş olan ödeme listesine Cek(class) Olarak ekle
                        }
                        else
                            MessageBox.Show("Eksik Yerleri Doldurunuz Yada Ödeme Kutularını Kontrol Ediniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    odm.OdemeSqlEkle(); //Ödeme Bilgilerini Sql'e Ekle
                    AvTamamlaTemizle();
                    butonchech();     
                    MessageBox.Show("Siparişiniz Alınmıştır...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Lütfen Kargo Şirketinizi ve Adres Bilgilerinizi Doldurunuz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Odeme Yapılamaz...Lütfen Ürün Seçtiğinize Veya Ödeme Tutarlarınıza Dikkat Edin.....", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //////////////////////////////////////////////////////  Kargo
        void KargoEkle() //Kapıda Ödeme Seçilirse Kargo ücretini ekle
        {
            double yeniToplam = Convert.ToDouble(Convert.ToInt32(lblAVToplam.Text) + Kargoo);
            lblKapıdaToplam.Text = lblOdemeToplam.Text = yeniToplam.ToString();
        }
        private void btnNakit_Click(object sender, EventArgs e)//Kapıda Ödemede Kargo Şirketleri Gelsin.
        {
            lblKargo.Visible = true; // Ödeme Ücretleri Görünsün
            lblKargoUcreti.Visible = true;
            xtraTabControl2.SelectedTabPageIndex = 1;//TABCONTROL2 2. Sayfasını aç ( Kapıda Ödeme            
            AVTamamlaHesaplar();
            butonchech();
        }
        void butonchech()
        {
            rbAras.Checked = false;
            rbMng.Checked = false;
            rbUps.Checked = false;
            rbYurtici.Checked = false;
            lblKargoKapıda.Text = " ";
            lblKargoUcreti.Text = " ";
            lblKargoSirket.Text = " ";
        }
        private void rbAras_CheckedChanged(object sender, EventArgs e)
        { lblKargoSirket.Text = "Aras Kargo"; }

        private void rbMng_CheckedChanged(object sender, EventArgs e)
        {
            lblKargoSirket.Text = "MNG Kargo";
        }

        private void rbYurtici_CheckedChanged(object sender, EventArgs e)
        {
            lblKargoSirket.Text = "Yurtiçi Kargo";
        }

        private void rbUps_CheckedChanged(object sender, EventArgs e)
        {
            lblKargoSirket.Text = "Ups Kargo";
        }
        long Kargoo;
        
        private void button15_Click(object sender, EventArgs e)
        {
            Kargoo = Convert.ToInt32(kp.ArasHesapla(Convert.ToDouble(lblKargoAgirlik.Text)));   // Kapıda ödeme seçersem kargo ücretini ekle iptal edersem normal fiyatı getir.(Aras)
            rbAras.PerformClick();
            lblKargoUcreti.Text = lblKargoKapıda.Text = Kargoo.ToString(); lblUps.Visible = false;
            if (checkKapıda.Checked == true)
            {
                KargoEkle();
            }
            else if (checkKapıda.Checked == false)
            {
                AVTamamlaHesaplar();
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            Kargoo = Convert.ToInt32(kp.MngHesapla(Convert.ToDouble(lblKargoAgirlik.Text)));   // Kapıda ödeme seçersem kargo ücretini ekle iptal edersem normal fiyatı getir.(Mng)
            rbMng.PerformClick();
            lblKargoUcreti.Text = lblKargoKapıda.Text = Kargoo.ToString(); lblUps.Visible = false;
            if (checkKapıda.Checked == true)
            {
                KargoEkle();
            }
            else if (checkKapıda.Checked == false)
            {
                AVTamamlaHesaplar();
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            Kargoo = Convert.ToInt32(kp.YurticiHesapla(Convert.ToDouble(lblKargoAgirlik.Text)));   // Kapıda ödeme seçersem kargo ücretini ekle iptal edersem normal fiyatı getir.(Yurtiçi)
            rbYurtici.PerformClick();
            lblKargoUcreti.Text = lblKargoKapıda.Text = Kargoo.ToString(); ;
            lblUps.Visible = false;
            if (checkKapıda.Checked == true)
            {
                KargoEkle();
            }
            else if (checkKapıda.Checked == false)
            {
                AVTamamlaHesaplar();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Kargoo = Convert.ToInt32(kp.UpsHesapla(Convert.ToDouble(lblKargoAgirlik.Text)));  // Kapıda ödeme seçersem kargo ücretini ekle iptal edersem normal fiyatı getir.(Ups)
            rbUps.PerformClick();
            lblKargoUcreti.Text = lblKargoKapıda.Text = Kargoo.ToString();
            if (checkKapıda.Checked == true)
            {
                KargoEkle();
            }
            else if (checkKapıda.Checked == false) 
            {
                AVTamamlaHesaplar();

            }
            lblUps.Visible = true;
            lblUps.Text = "* Ups Kargo Seçeneğiyle Yurt Dışınada Kargo Gönderebilirsiniz.";
        }


        //////////////***********************************************************************/////


        private void button4_Click(object sender, EventArgs e)// Ana Sayfa Butonumuz
        {
            xtraTabControl1.SelectedTabPageIndex = 5;// xtraTabPagenin 9. sayfasını açıyoruz.
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SepetList();
            UrunList();
            xtraTabControl1.SelectedTabPageIndex = 5; // Programım Ana Sayfadan Başla
            MusteriBilgiCek(kullanıcı); // Müşteri Bilgilerini Getir.
        }
        void AvTamamlaTemizle() // ALIŞVERİŞ SONRASI FORMU TEMİZLE
        {
            Siparis s = new Siparis();
            s.SiparisKalem= new List<SiparisDetay>();
            listView2.Items.Clear();
            lblAVTutar.Text = "0";
            lblAVVergi.Text = "0";
            lblAVToplam.Text = "0";
            lblOdemeToplam.Text = "0";
            lblOdemeTutar.Text = "0";
            lblOdemeVergi.Text = "0";
            lblKargoSirket.Text = " ";
            lblKargoAgirlik.Text = " ";
            lblKargoUcreti.Text = " ";
            rchAdres.Text = "";
            txtCekBanka.Text = "";
            txtCekBasım.Text = "";
            txtCekKime.Text = "";
            txtCekSahip.Text = "";
            txtCekSube.Text = "";
            mskCekHesapNo.Text = "";
            mskCekIBAN.Text = "";
            mskCekNumarasi.Text = "";
        } 
        void KartTemizle()  // AlışVeriş Sonrası Kart Bilgileri SİL
        {
            mskCVV.Text = "";
            mskKartNo.Text = "";
            cmbYıl.Text = "";
            cmbAy.Text = "";
            cmbKartTipleri.Text = "";
        }
        void checkTemizle()
        {
            txtKapıdaPara.Text = "0";
            txtCekPara.Text = "0";
            txtKartPara.Text = "0";
        }

        private void checkKart_CheckedChanged(object sender, EventArgs e)
        {
            checkTemizle();
            if (checkKart.Checked==true)
            {
                lblKartPara.Visible = true;
                txtKartPara.Visible = true;
            }
            else
            {
                lblKartPara.Visible = false;
                txtKartPara.Visible = false;
            }
            
        }

        private void checkKapıda_CheckedChanged(object sender, EventArgs e)
        {
            checkTemizle();
            if (checkKapıda.Checked == true)
            {
                lblKapidaPara.Visible = true;
                txtKapıdaPara.Visible = true;
            }
            else
            {
                lblKapidaPara.Visible = false;
                txtKapıdaPara.Visible = false;
            }  
        }

        private void checkCek_CheckedChanged(object sender, EventArgs e)
        {
            checkTemizle();
            if (checkCek.Checked == true)
            {
                lblCekPara.Visible = true;
                txtCekPara.Visible = true;
            }
            else
            {
                lblCekPara.Visible = false;
                txtCekPara.Visible = false;
            }  
        }

        void AVTamamlaHesaplar() // Ödemede sayıları basamaklandır.
        {
            xtraTabControl1.SelectedTabPageIndex = 4; //Alışverişi Tamamla Deyince Ödeme Sayfasına geç
            lblKapıdaTutar.Text = lblOdemeTutar.Text = lblAVTutar.Text;
            lblKapıdaVergi.Text = lblOdemeVergi.Text = lblAVVergi.Text;
            lblKapıdaToplam.Text = lblOdemeToplam.Text = lblAVToplam.Text;
        }
        private void btnAVTamamla_Click_1(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 4; //Alışverişi Tamamla Deyince Ödeme Sayfasına geç
            AVTamamlaHesaplar();
        }   
    }
}
     
    
    

