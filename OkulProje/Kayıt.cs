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
    public partial class Kayıt : Form
    {
        public Kayıt()
        {
            InitializeComponent();
        }

        private void btnMusteri_Click(object sender, EventArgs e)
        {
            lblPozisyon.Visible = false;
            txtPozisyon.Visible = false;
            txtPozisyon.Text = " ";
            btnKayıtTur.Text = "Müşteri Kayıt";
        }

        private void btnYonetici_Click(object sender, EventArgs e)
        {
            lblPozisyon.Visible = true;
            txtPozisyon.Visible = true;
            btnKayıtTur.Text = "Yönetici Kayıt";
        }
        void txtSifirla() // Kayıt sonrası text leri sıfırlıyoruz.
        {
            txtAd.Text = "";
            txtID.Text = "";
            txtIlce.Text = "";
            txtMail.Text = "";
            txtPozisyon.Text = "";
            txtSehir.Text = "";
            txtSifre.Text = "";
            txtSoyad.Text = "";
            mskTC.Text = "";
            mskTelefon.Text = "";
        }
        sqlbaglanti baglanti = new sqlbaglanti();
        private void btnKayır_Click(object sender, EventArgs e) // Müşteri Kayıdı Yapıyoruz
        {
            if (lblPozisyon.Visible == false)
            {
                if (txtID.Text != "" && txtAd.Text != "" && txtIlce.Text != "" && txtMail.Text != ""&& txtSehir.Text != "" && txtSifre.Text != "" && txtSoyad.Text != "" && mskTC.Text != "" && mskTelefon.Text != "")
                {
                    Musteri ms = new Musteri();
                    ms.Ad=txtAd.Text;
                    ms.Email = txtMail.Text;
                    ms.Ilce = txtIlce.Text;
                    ms.KullanıcıID = txtID.Text;
                    ms.Sehir = txtSehir.Text;
                    ms.Sifre = txtSifre.Text;
                    ms.Soyad = txtSoyad.Text;
                    ms.TC = mskTC.Text;
                    ms.Telefon = mskTelefon.Text;
                    ms.MusteriKayit();
                    txtSifirla();
                }
                else
                {
                    MessageBox.Show("Eksik Yerleri Doldurunuz!...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (lblPozisyon.Visible == true) // Yönetici Seçildiyse lblpozisyon görünür olur / lblpozisyon görünürse yönetici kaydı yapılıyor
            {
                if (txtID.Text != "" && txtAd.Text != "" && txtIlce.Text != "" && txtMail.Text != "" && txtPozisyon.Text != "" && txtSehir.Text != "" && txtSifre.Text != "" && txtSoyad.Text != "" && mskTC.Text != "" && mskTelefon.Text != "")
                {
                    SqlCommand yon = new SqlCommand("insert into Tbl_Yonetici (YID,YSifre,YAd,YSoyad,YPozisyon,YTC,YTelefon,YMail,YSehir,YIlce) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglanti.baglanti());
                    yon.Parameters.AddWithValue("@a1", txtID.Text);
                    yon.Parameters.AddWithValue("@a2", txtSifre.Text);
                    yon.Parameters.AddWithValue("@a6", mskTC.Text);
                    yon.Parameters.AddWithValue("@a7", mskTelefon.Text);
                    yon.Parameters.AddWithValue("@a8", txtMail.Text);
                    yon.Parameters.AddWithValue("@a9", txtSehir.Text);
                    yon.Parameters.AddWithValue("@a10", txtIlce.Text);
                    yon.Parameters.AddWithValue("@a3", txtAd.Text);
                    yon.Parameters.AddWithValue("@a4", txtSoyad.Text);
                    yon.Parameters.AddWithValue("@a5", txtPozisyon.Text);
                    yon.ExecuteNonQuery();
                    baglanti.baglanti().Close();
                    MessageBox.Show("Yönetici Kaydınız Yapılmıştır!...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSifirla();
                }
                else
                {
                    MessageBox.Show("Eksik Yerleri Doldurunuz!...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }        
        }
        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
