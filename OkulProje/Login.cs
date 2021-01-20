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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;
            txtid.Focus();
            txtTemizle();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            txtTemizle();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        void txtTemizle()
        {
            txtid.Text = "";
            txtSifre.Text = "";
            txtYid.Text = "";
            txtYSifre.Text = "";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if(xtraTabControl1.SelectedTabPageIndex==0)
            {
                SqlCommand komut = new SqlCommand("Select * From Tbl_Musteriler where MId =@p1 and MSifre = @p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    AnaMenü fr = new AnaMenü();
                    fr.kullanıcı = txtid.Text;
                    fr.Show();
                    txtTemizle();
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre.... ", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtid.Focus();
                }
                bgl.baglanti().Close();
            }
            else if (xtraTabControl1.SelectedTabPageIndex==1)
            {
                SqlCommand komut1 = new SqlCommand("Select * From Tbl_Yonetici where YId =@p1 and YSifre = @p2", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", txtYid.Text);
                komut1.Parameters.AddWithValue("@p2", txtYSifre.Text);
                SqlDataReader dr1 = komut1.ExecuteReader();
                if (dr1.Read())
                {
                    Yonetici yo = new Yonetici();
                    yo.kullanici = txtYid.Text;
                    yo.Show();
                    txtTemizle();
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre.... ", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtid.Focus();
                }
                bgl.baglanti().Close();
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Kayıt kayıt = new Kayıt();
            kayıt.Show();
        }
    }
}
