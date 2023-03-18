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
    public partial class EmanetKitapListelefrm : Form
    {
        public EmanetKitapListelefrm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-0J6VBSNR;Initial Catalog=KütüphaneOtomasyonu;Integrated Security=True");
        DataSet dataset = new DataSet();

        private void EmanetKitapListelefrm_Load(object sender, EventArgs e)
        {
            EmanetListele();
            comboBox1.SelectedIndex = 0;
        }

        private void EmanetListele()
        {
            baglanti.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from EmanetKitaplar", baglanti);
            adapter.Fill(dataset, "EmanetKitaplar");
            dataGridView1.DataSource = dataset.Tables["EmanetKitaplar"];
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataset.Tables["EmanetKitaplar"].Clear();
            if (comboBox1.SelectedIndex==0)
            {
                EmanetListele();
            }
            else if (comboBox1.SelectedIndex==1)
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from EmanetKitaplar where '"+DateTime.Now.ToShortDateString()+"'>iadetarihi", baglanti);
                adapter.Fill(dataset, "EmanetKitaplar");
                dataGridView1.DataSource = dataset.Tables["EmanetKitaplar"];
                baglanti.Close();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                baglanti.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from EmanetKitaplar where '" + DateTime.Now.ToShortDateString() + "'<=iadetarihi", baglanti);
                adapter.Fill(dataset, "EmanetKitaplar");
                dataGridView1.DataSource = dataset.Tables["EmanetKitaplar"];
                baglanti.Close();
            }
        }
    }
}
