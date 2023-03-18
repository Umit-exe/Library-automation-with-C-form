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

namespace KutuphaneOtomasyon
{
    public partial class UyeListelemefrm : Form
    {
        public UyeListelemefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-0J6VBSNR;Initial Catalog=KütüphaneOtomasyonu;Integrated Security=True");
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Uye where tc like'"+textBox1.Text+"'",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["adsoyad"].ToString();
                textBox3.Text = read["yas"].ToString();
                comboBox1.Text = read["cinsiyet"].ToString();
                textBox5.Text = read["telefon"].ToString();
                textBox6.Text = read["adres"].ToString();
                textBox7.Text = read["email"].ToString();
                textBox4.Text = read["okukitapsayisi"].ToString();
            }
            baglanti.Close();
        }
        DataSet dataset = new DataSet();
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            dataset.Tables["Uye"].Clear();
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Uye where tc like '%"+textBox8.Text+"%'",baglanti);
            adapter.Fill(dataset,"Uye");
            dataGridView1.DataSource = dataset.Tables["Uye"];
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu kaydı silmek mi istiyorsunuz? ","Sil",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from Uye where tc=@tc", baglanti);
                komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme işlemi başarılı.");
                dataset.Tables["Uye"].Clear();
                uyelistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox || item is ComboBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }
        private void uyelistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from Uye",baglanti);
            adtr.Fill(dataset, "Uye");
            dataGridView1.DataSource = dataset.Tables["Uye"];
            baglanti.Close();
        }

        private void UyeListelemefrm_Load(object sender, EventArgs e)
        {
            uyelistele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Uye set adsoyad=@adsoyad,yas=@yas,cinsiyet=@cinsiyet,telefon=@telefon,adres=@adres,email=@email,okukitapsayisi=@okukitapsayisi where tc=@tc", baglanti);
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
            komut.Parameters.AddWithValue("@yas", textBox3.Text);
            komut.Parameters.AddWithValue("@cinsiyet", comboBox1.Text);
            komut.Parameters.AddWithValue("@telefon", textBox5.Text);
            komut.Parameters.AddWithValue("@adres", textBox6.Text);
            komut.Parameters.AddWithValue("@email", textBox7.Text);
            komut.Parameters.AddWithValue("@okukitapsayisi", int.Parse(textBox4.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi gerçekleşti.");
            dataset.Tables["Uye"].Clear();
            uyelistele();
            foreach (Control item in Controls)
            {
                if (item is TextBox || item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }
    }
}
