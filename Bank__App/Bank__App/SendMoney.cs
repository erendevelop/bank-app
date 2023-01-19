using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank__App
{
    public partial class SendMoney : Form
    {
        string eposta;
        string adlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\adlar.txt";
        string soyadlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\soyadlar.txt";
        string epostalar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\epostalar.txt";
        string telefonlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\telefonlar.txt";
        string bakiyeler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\bakiyeler.txt";
        string resimler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\resimler.txt";
        string sifreler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\sifreler.txt";
        string harcamalar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\harcamalar.txt";
        int a;
        int c;
        int d;
        int d2;
        int d3;
        string d4;
        int d5;
        int d6;
        int number = 0;
        List<string> list2 = new List<string>();
        public SendMoney()
        {
            InitializeComponent();
        }

        private void addToTransactions(string amountOfMoney, string whom) { 
            MainScreen.transactions.Add(amountOfMoney + " ₺ - " + whom);
        }
        private void SendMoney_Load(object sender, EventArgs e)
        {
            a = LogInScreen.counter;
            eposta = File.ReadLines(@epostalar).Skip(a - 1).Take(1).First();
            List<string> list = new List<string>();
            list = File.ReadAllLines(@epostalar).ToList();
            list.Remove(eposta);
            c = list.Count();
            for (int i = 0; i < c; i++)
            {
                comboBox1.Items.Add(list[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MainScreen().Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text.Trim(), out number) & textBox1.Text != null & this.comboBox1.GetItemText(this.comboBox1.SelectedItem) != null)
            {
                // Entered amount of money.

                d = Int32.Parse(textBox1.Text);
                // Sender's amount of money.

                d2 = Int32.Parse(File.ReadLines(@bakiyeler).Skip(a - 1).Take(1).First());
                // New money amount after sending.

                d3 = d2 - d;
                // Receiver of the money.

                d4 = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
                // Getting all emails.

                list2 = File.ReadAllLines(@epostalar).ToList();
                d5 = list2.IndexOf(d4) + 1;
                d6 = d + Int32.Parse(File.ReadLines(@bakiyeler).Skip(d5 - 1).Take(1).First());
                if (d <= d2)
                {
                    string[] arrLine = File.ReadAllLines(@bakiyeler);
                    arrLine[a - 1] = d3.ToString();
                    File.WriteAllLines(@bakiyeler, arrLine);

                    string[] arrLine2 = File.ReadAllLines(@bakiyeler);
                    arrLine[d5 - 1] = d6.ToString();
                    File.WriteAllLines(@bakiyeler, arrLine);

                    addToTransactions(d.ToString(), d4);
                    StreamWriter harcamaWriter = new StreamWriter(LogInScreen.genelHarcama, true);
                    harcamaWriter.WriteLine(d + " ₺ - " + d4);
                    harcamaWriter.Close();
                    // Adding transactions to transactionBox's data source.

                    MainScreen.transactions.Clear();
                    for (int i = File.ReadAllLines(LogInScreen.genelHarcama).ToList().Count - 1; i >= 0; i--)
                        MainScreen.transactions.Add(File.ReadAllLines(LogInScreen.genelHarcama).ToList()[i]);

                    MessageBox.Show("Para gönderildi.");
                    this.Close();
                    new MainScreen().Show();
                }
                else
                {
                    MessageBox.Show("Yeterli paranız yok.");
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir miktar giriniz ve hesap seçtiğinizden emin olunuz.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}