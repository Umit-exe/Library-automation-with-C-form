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
    public partial class EmanetKitapİadefrm : Form
    {
        public EmanetKitapİadefrm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-0J6VBSNR;Initial Catalog=KütüphaneOtomasyonu;Integrated Security=True");
        DataSet dataset = new DataSet();

        private void EmanetListele()
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from EmanetKitaplar", baglanti);
            adapter.Fill(dataset, "EmanetKitaplar");
            dataGridView1.DataSource = dataset.Tables["EmanetKitaplar"];
            baglanti.Close();
        }

        private void EmanetKitapİadefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataset.Tables["EmanetKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from EmanetKitaplar where tc like '%"+textBox1.Text+"%'",baglanti);
            adapter.Fill(dataset,"EmanetKitaplar");
            baglanti.Close();
            if (textBox1.Text=="")
            {
                dataset.Tables["EmanetKitaplar"].Clear();
                EmanetListele();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataset.Tables["EmanetKitaplar"].Clear();
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from EmanetKitaplar where barkodno like '%" + textBox2.Text + "%'", baglanti);
            adapter.Fill(dataset, "EmanetKitaplar");
            baglanti.Close();
            if (textBox2.Text == "")
            {
                dataset.Tables["EmanetKitaplar"].Clear();
                EmanetListele();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from EmanetKitaplar where tc=@tc and barkodno=@barkodno",baglanti);
            komut.Parameters.AddWithValue("@tc",dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update Kitap set stoksayisi=stoksayisi+'"+dataGridView1.CurrentRow.Cells["kitapsayisi"].Value.ToString()+"' where barkodno=@barkodno",baglanti);
            komut2.Parameters.AddWithValue("@barkodno", dataGridView1.CurrentRow.Cells["barkodno"].Value.ToString());
            komut2.ExecuteNonQuery();
            MessageBox.Show("Kitap(lar) iade edildi");
            baglanti.Close();
            dataset.Tables["EmanetKitaplar"].Clear();
            EmanetListele();
        }
    }
}
