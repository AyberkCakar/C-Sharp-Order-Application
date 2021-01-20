using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulProje
{
    public partial class OdemeGortuntule : Form
    {
        public OdemeGortuntule()
        {
            InitializeComponent();
        }
        public long id;
        private void OdemeGortuntule_Load(object sender, EventArgs e)
        {
            Siparis siparis = new Siparis();
            siparis.OdemeListeleCard(id);
            siparis.OdemeListeleCek(id);
            siparis.OdemeListeleKapida(id);
            foreach(Odeme o in siparis.OdemeList)
            {
                if(o.OdemeYontemi()==1) // Ödeme Listemiz İçinde Kontrol Ediyoruz Eğer Odemeyönetimi 1  yani Credi Kartı Bilgilerini listeden txt'lere dolduruyoruz
                {
                    txtAy.Text = (((o as CreditCard).KartAy).ToString());
                    txtYıl.Text = (((o as CreditCard).KartYıl).ToString());
                    txtKartOTutar.Text = (((o as CreditCard).Toplam()).ToString());
                    txtKartTip.Text = (((o as CreditCard).KartTip).ToString());
                    txtKartNo.Text = (((o as CreditCard).KartNo).ToString());
                    txtCVV.Text = (((o as CreditCard).KartCVV).ToString());
                }
                else if(o.OdemeYontemi()==2)  // Ödeme Listemiz İçinde Kontrol Ediyoruz Eğer Odemeyönetimi 2  yani Kapıda ödeme  Bilgilerini listeden txt'lere dolduruyoruz
                {
                    txtKapıdaOTutar.Text= (((o as KapidaOdeme).Toplam()).ToString());
                    txtKargoUcreti.Text= (((o as KapidaOdeme).KargoUcret).ToString());
                }
                else if (o.OdemeYontemi()==3) // Ödeme Listemiz İçinde Kontrol Ediyoruz Eğer Odemeyönetimi 3  yani Çek Bilgilerini listeden txt'lere dolduruyoruz
                {
                    txtBasımTarihi.Text = (((o as Cek).CBasimTarihi).ToString());
                    txtCekKime.Text = (((o as Cek).CekKime).ToString());
                    txtCekNo.Text = (((o as Cek).CekNo).ToString());
                    txtCekOTutar.Text = (((o as Cek).Toplam()).ToString());
                    txtCekSahip.Text = (((o as Cek).CekSahibi).ToString());
                    txtHesapNo.Text = (((o as Cek).HesapNo).ToString());
                    txtIBAN.Text = (((o as Cek).IBAN).ToString());
                    txtSube.Text = (((o as Cek).Sube).ToString());
                    txtBanka.Text = (((o as Cek).Banka).ToString());
                }
            }   
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
