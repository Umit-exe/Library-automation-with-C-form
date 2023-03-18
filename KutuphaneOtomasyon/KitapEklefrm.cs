using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyon
{
    public partial class KitapEklefrm : Form
    {
        public KitapEklefrm()
        {
            InitializeComponent();
        }
            SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-0J6VBSNR;Initial Catalog=KütüphaneOtomasyonu;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into kitap(barkodno,kitapadi,yazari,yayinevi,sayfasayisi,turu,stoksayisi,rafno,aciklama,kayittarihi) values(@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@turu,@stoksayisi,@rafno,@aciklama,@kayittarihi)", baglanti);
            komut.Parameters.AddWithValue("@barkodno",textBox1.Text);
            komut.Parameters.AddWithValue("@kitapadi", textBox2.Text);
            komut.Parameters.AddWithValue("@yazari", textBox3.Text);
            komut.Parameters.AddWithValue("@yayinevi", textBox4.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", textBox6.Text);
            komut.Parameters.AddWithValue("@turu", comboBox1.Text);
            komut.Parameters.AddWithValue("@stoksayisi", textBox7.Text);
            komut.Parameters.AddWithValue("@rafno", textBox8.Text);
            komut.Parameters.AddWithValue("@aciklama", textBox9.Text);
            komut.Parameters.AddWithValue("@kayittarihi", DateTime.Now.ToShortDateString());
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Kaydı yapıldı! ");
            foreach (Control item in Controls)
            {
                if (item is TextBox|| item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void KitapEklefrm_Load(object sender, EventArgs e)
        {

        }
    }
}
