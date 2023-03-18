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
    public partial class Siralamafrm : Form
    {
        public Siralamafrm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-0J6VBSNR;Initial Catalog=KütüphaneOtomasyonu;Integrated Security=True");
        DataSet dataset = new DataSet();

        private void Siralamafrm_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Uye order by okukitapsayisi desc", baglanti);
            adapter.Fill(dataset, "Uye");
            dataGridView1.DataSource = dataset.Tables["Uye"];
            baglanti.Close();
            label2.Text = "";
            label4.Text = "";
            label2.Text = dataset.Tables["Uye"].Rows[0]["adsoyad"].ToString()+" ";
            label2.Text += dataset.Tables["Uye"].Rows[0]["okukitapsayisi"].ToString()+" kitap";
            label4.Text = dataset.Tables["Uye"].Rows[dataGridView1.Rows.Count-2]["adsoyad"].ToString()+" ";
            label4.Text += dataset.Tables["Uye"].Rows[dataGridView1.Rows.Count-2]["okukitapsayisi"].ToString()+" kitap";
        }
    }
}
