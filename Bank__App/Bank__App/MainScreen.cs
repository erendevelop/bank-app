using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank__App
{
    public partial class MainScreen : Form
    {
        string resimyedek;
        string dosyayol= "x";
        string bakiye;
        string soyad;
        int veri;
        string ad;
        string resimler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\resimler.txt";
        string adlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\adlar.txt";
        string soyadlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\soyadlar.txt";
        string epostalar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\epostalar.txt";
        string telefonlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\telefonlar.txt";
        string bakiyeler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\bakiyeler.txt";
        string sifreler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\sifreler.txt";
        public MainScreen()
        {
            InitializeComponent();
        }
        private void cardsShowInfos(int iValue, List<string> list)
        {
            if (!cardsFirstRun)
                MessageBox.Show($"{list[iValue]}", "ArıBank", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (cardsFirstRun) cardsFirstRun = false;
        }
        private void transactionsShowInfos(int iValue, List<string> list)
        {
            if (!transactionsFirstRun)
                MessageBox.Show($"{list[iValue]}", "ArıBank", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (transactionsFirstRun) transactionsFirstRun = false;
        }

        private bool cardsFirstRun = true;
        private bool transactionsFirstRun = true;
        public static List<string> transactions = new List<string> {};

        public void MainScreen_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            veri = LogInScreen.counter; 
            transactionsBox.DataSource = transactions;
            groupBox1.Text = "";
            nameLabel.Text = File.ReadLines(@adlar).Skip(veri-1).Take(1).First() + " " + File.ReadLines(@soyadlar).Skip(veri-1).Take(1).First();
            moneyLabel.Text = File.ReadLines(@bakiyeler).Skip(veri-1).Take(1).First() + " ₺";
            dosyayol = File.ReadLines(@resimler).Skip(veri - 1).Take(1).First();
            if (dosyayol == "x")
            {
                pictureBox1.ImageLocation = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\profilePicture.png";
            }
            else
            {
                dosyayol = File.ReadLines(@resimler).Skip(veri - 1).Take(1).First();
                pictureBox1.ImageLocation = dosyayol;
            }
            try
            {
                string urlDoviz = "https://www.tcmb.gov.tr/kurlar/202212/27122022.xml";
                WebClient wcDoviz = new WebClient();
                wcDoviz.Encoding = Encoding.UTF8;
                string htmlDoviz = wcDoviz.DownloadString(urlDoviz);
                MatchCollection mcKurlar = Regex.Matches(htmlDoviz, "<Currency.*?</Currency>", RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match mKur in mcKurlar)
                {
                    Match mKurIsim = Regex.Match(mKur.Value, "<Isim>.*?</Isim>");
                    string KurIsim = mKurIsim.Value.Replace("Isim", "").Replace("</Isim>", "").Replace("<>", "").Replace("</>", "");
                    listBox1.Items.Add(KurIsim);
                    comboBox1.Items.Add(KurIsim);
                    Match mKurAlis = Regex.Match(mKur.Value, "<ForexSelling>.*?</ForexSelling>");
                    string KurAlis = mKurAlis.Value.Replace("ForexSelling", "").Replace("</ForexSelling>", "").Replace("<>", "").Replace("</>", "");
                    listBox2.Items.Add(KurAlis);
                    Match mKurSatis = Regex.Match(mKur.Value, "<ForexBuying>.*?</ForexBuying>");
                    string KurSatis = mKurSatis.Value.Replace("ForexBuying", "").Replace("</ForexBuying>", "").Replace("<>", "").Replace("</>", "");
                    listBox3.Items.Add(KurSatis);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("İnternete bağlanılamadı.");
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                comboBox1.Items.Clear();
            }
        }

        private void nameLabel_Click(object sender, EventArgs e)
        {
        }

        private void profilePicture_Click(object sender, EventArgs e)
        {
        }

        private void transactionsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            transactionsShowInfos(transactionsBox.SelectedIndex, transactions);
        }

        private void sendMoneyButton_Click(object sender, EventArgs e)
        {
            try
            {
                string urlDoviz = "https://www.tcmb.gov.tr/kurlar/202212/27122022.xml";
                WebClient wcDoviz = new WebClient();
                wcDoviz.Encoding = Encoding.UTF8;
                string htmlDoviz = wcDoviz.DownloadString(urlDoviz);

                this.Hide();
                new SendMoney().Show();
            }
            catch (Exception ex) {
                MessageBox.Show("İnternet bağlantısı olmadığı için para gönderemezsiniz.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            new LogInScreen().Show();
        }

        private void greentingLabel_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Resim seçin.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dosyayol = dialog.FileName;
                pictureBox1.ImageLocation = dosyayol;
                string[] arrLine = File.ReadAllLines(@resimler);
                arrLine[veri-1] = dosyayol;
                File.WriteAllLines(@resimler, arrLine);
            }



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.SelectedIndex = listBox1.SelectedIndex;
            listBox3.SelectedIndex = listBox1.SelectedIndex;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox2.SelectedIndex;
            listBox3.SelectedIndex = listBox2.SelectedIndex;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = listBox3.SelectedIndex;
            listBox2.SelectedIndex = listBox3.SelectedIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int seciliKurIndexi = comboBox1.SelectedIndex;
            listBox1.SelectedIndex = seciliKurIndexi;
            listBox2.SelectedIndex = seciliKurIndexi;
            listBox3.SelectedIndex = seciliKurIndexi;
        }
    }
}
