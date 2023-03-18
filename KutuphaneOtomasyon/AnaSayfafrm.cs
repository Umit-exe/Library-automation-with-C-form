using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyon
{
    public partial class AnaSayfafrm : Form
    {
        public AnaSayfafrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UyeEklefrm uyeekle = new UyeEklefrm();
            uyeekle.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UyeListelemefrm uyelistele = new UyeListelemefrm();
            uyelistele.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            KitapEklefrm kitapekle = new KitapEklefrm();
            kitapekle.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KitapListelefrm kitaplistele = new KitapListelefrm();
            kitaplistele.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EmanetKitapVerfrm emanetver = new EmanetKitapVerfrm();
            emanetver.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EmanetKitapListelefrm listele = new EmanetKitapListelefrm();
            listele.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EmanetKitapİadefrm iade = new EmanetKitapİadefrm();
            iade.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Siralamafrm sirala = new Siralamafrm();
            sirala.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            grafikfrm grafik = new grafikfrm();
            grafik.ShowDialog();
        }
    }
}
