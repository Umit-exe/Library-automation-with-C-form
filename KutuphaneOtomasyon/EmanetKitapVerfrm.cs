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
    public partial class EmanetKitapVerfrm : Form
    {
        public EmanetKitapVerfrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-0J6VBSNR;Initial Catalog=KütüphaneOtomasyonu;Integrated Security=True");
        DataSet dataset = new DataSet();
        private void EmanetKitapVerfrm_Load(object sender, EventArgs e)
        {
            sepetlistele();
            kitapsayisi();
        }
        private void kitapsayisi()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(kitapsayisi) from Sepet",baglanti);
            label16.Text = komut.ExecuteScalar().ToString();
            baglanti.Close();
        }
        private void sepetlistele()
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Sepet",baglanti);
            adapter.Fill(dataset, "Sepet");
            dataGridView1.DataSource = dataset.Tables["Sepet"];
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Sepet(barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
            komut.Parameters.AddWithValue("@barkodno", textBox5.Text);
            komut.Parameters.AddWithValue("@kitapadi", textBox6.Text);
            komut.Parameters.AddWithValue("@yazari", textBox7.Text);
            komut.Parameters.AddWithValue("@yayinevi", textBox8.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", textBox9.Text);
            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(textBox10.Text));
            komut.Parameters.AddWithValue("@teslimtarihi", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("@iadetarihi", dateTimePicker2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap(lar) sepete eklendi", "Ekleme İşlemi");
            dataset.Tables["Sepet"].Clear();
            sepetlistele();
            label16.Text = "";
            kitapsayisi();
            foreach(Control item in groupBox2.Controls)
            {
                if(item is TextBox && item!=textBox10)
                {
                    item.Text = "";
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Uye where tc like'"+textBox1.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["adsoyad"].ToString();
                textBox3.Text = read["yas"].ToString();
                textBox4.Text = read["telefon"].ToString();
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select sum(kitapsayisi) from EmanetKitaplar where tc='"+textBox1.Text+"'",baglanti);
            label14.Text = komut2.ExecuteScalar().ToString();
            baglanti.Close();
            if (textBox1.Text=="")
            {
                foreach (Control item in groupBox1.Controls)
                {
                    if(item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                label14.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kitap where barkodno like '"+textBox5.Text+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox6.Text = read["kitapadi"].ToString();
                textBox7.Text = read["yazari"].ToString();
                textBox8.Text = read["yayinevi"].ToString();
                textBox9.Text = read["sayfasayisi"].ToString();
            }
            baglanti.Close();
            if (textBox5.Text == "")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox && item != textBox10)
                    {
                        item.Text = "";
                    }
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Kayıt silinsin mi?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Sepet where barkodno ='" + dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString() + "'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme işlemi yapıldı!", "Silme İşlemi");
                dataset.Tables["Sepet"].Clear();
                sepetlistele();
                label16.Text = "";
                kitapsayisi();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label16.Text!="")
            {
                if (label14.Text==""&&int.Parse(label16.Text)<=3 ||label16.Text!=""&& int.Parse(label14.Text)+int.Parse(label16.Text)<=3)
                {
                    if (textBox1.Text!=""&&textBox2.Text!=""&&textBox3.Text!=""&&textBox4.Text!="")
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                        {
                            baglanti.Open();
                            SqlCommand komut = new SqlCommand("insert into EmanetKitaplar(tc,adsoyad,yas,telefon,barkodno,kitapadi,yazari,yayinevi,sayfasayisi,kitapsayisi,teslimtarihi,iadetarihi) values(@tc,@adsoyad,@yas,@telefon,@barkodno,@kitapadi,@yazari,@yayinevi,@sayfasayisi,@kitapsayisi,@teslimtarihi,@iadetarihi)", baglanti);
                            komut.Parameters.AddWithValue("@tc", textBox1.Text);
                            komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
                            komut.Parameters.AddWithValue("@yas", textBox3.Text);
                            komut.Parameters.AddWithValue("@telefon", textBox4.Text);
                            komut.Parameters.AddWithValue("@barkodno", dataGridView1.Rows[i].Cells["barkodno"].Value.ToString());
                            komut.Parameters.AddWithValue("@kitapadi", dataGridView1.Rows[i].Cells["kitapadi"].Value.ToString());
                            komut.Parameters.AddWithValue("@yazari", dataGridView1.Rows[i].Cells["yazari"].Value.ToString());
                            komut.Parameters.AddWithValue("@yayinevi", dataGridView1.Rows[i].Cells["yayinevi"].Value.ToString());
                            komut.Parameters.AddWithValue("@sayfasayisi", dataGridView1.Rows[i].Cells["sayfasayisi"].Value.ToString());
                            komut.Parameters.AddWithValue("@kitapsayisi", int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()));
                            komut.Parameters.AddWithValue("@teslimtarihi", dataGridView1.Rows[i].Cells["teslimtarihi"].Value.ToString());
                            komut.Parameters.AddWithValue("@iadetarihi", dataGridView1.Rows[i].Cells["iadetarihi"].Value.ToString());
                            komut.ExecuteNonQuery();
                            SqlCommand komut2 = new SqlCommand("update Uye set okukitapsayisi=okukitapsayisi+'"+int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString())+"' where tc='"+textBox1.Text+"'",baglanti);
                            komut2.ExecuteNonQuery();
                            SqlCommand komut3 = new SqlCommand("update Kitap set stoksayisi=stoksayisi-'" + int.Parse(dataGridView1.Rows[i].Cells["kitapsayisi"].Value.ToString()) + "' where barkodno='" + dataGridView1.Rows[i].Cells["barkodno"].Value.ToString() + "'", baglanti);
                            komut3.ExecuteNonQuery();
                            baglanti.Close();
                        }
                        baglanti.Open();
                        SqlCommand komut4 = new SqlCommand("delete from Sepet",baglanti);
                        komut4.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Kitap(lar) emanet edildi.");
                        dataset.Tables["Sepet"].Clear();
                        sepetlistele();
                        textBox1.Text = "";
                        label16.Text = "";
                        kitapsayisi();
                        label15.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Önce Üye ismi seçmeniz gerekir","Uyarı");
                    }
                }
                else
                {
                    MessageBox.Show("Emanet kitap sayısı 3 ten fazla olamaz!","Uyarı");
                }
            }
            else
            {
                MessageBox.Show("Önce sepete kitap eklemelisiniz!","Uyarı");
            }
        }
    }
}
