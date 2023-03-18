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
    public partial class KitapListelefrm : Form
    {
        public KitapListelefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-0J6VBSNR;Initial Catalog=KütüphaneOtomasyonu;Integrated Security=True");
        DataSet dataset = new DataSet();
        private void kitaplistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Kitap", baglanti);
            adtr.Fill(dataset, "Kitap");
            dataGridView1.DataSource = dataset.Tables["Kitap"];
            baglanti.Close();
        }

        private void KitapListelefrm_Load(object sender, EventArgs e)
        {
            kitaplistele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu kaydı silmek mi istiyorsunuz? ", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Kitap where barkodno=@barkodno", baglanti);
                komut.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme işlemi başarılı.");
                dataset.Tables["Kitap"].Clear();
                kitaplistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox || item is ComboBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Kitap set kitapadi=@kitapadi,yazari=@yazari,yayinevi=@yayinevi,sayfasayisi=@sayfasayisi,turu=@turu,stoksayisi=@stoksayisi,rafno=@rafno,aciklama=@aciklama where barkodno=@barkodno", baglanti);
            komut.Parameters.AddWithValue("@barkodno", textBox1.Text);
            komut.Parameters.AddWithValue("@kitapadi", textBox2.Text);
            komut.Parameters.AddWithValue("@yazari", textBox3.Text);
            komut.Parameters.AddWithValue("@yayinevi", textBox4.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", textBox6.Text);
            komut.Parameters.AddWithValue("@turu", comboBox1.Text);
            komut.Parameters.AddWithValue("@stoksayisi", textBox7.Text);
            komut.Parameters.AddWithValue("@rafno", textBox8.Text);
            komut.Parameters.AddWithValue("@aciklama", textBox9.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi gerçekleşti.");
            dataset.Tables["Kitap"].Clear();
            kitaplistele();
            foreach (Control item in Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            dataset.Tables["Kitap"].Clear();
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Kitap where barkodno like '%" + textBox5.Text + "%'", baglanti);
            adapter.Fill(dataset, "Kitap");
            dataGridView1.DataSource = dataset.Tables["Kitap"];
            baglanti.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kitap where barkodno like'" + textBox1.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["kitapadi"].ToString();
                textBox3.Text = read["yazari"].ToString();
                textBox4.Text = read["yayinevi"].ToString();
                textBox6.Text = read["sayfasayisi"].ToString();
                comboBox1.Text = read["turu"].ToString();
                textBox7.Text = read["stoksayisi"].ToString();
                textBox8.Text = read["rafno"].ToString();
                textBox9.Text = read["aciklama"].ToString();
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
