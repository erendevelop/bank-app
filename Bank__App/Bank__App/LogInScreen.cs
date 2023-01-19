using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bank__App
{
   
    public partial class LogInScreen : Form
    {
        public static string genelHarcama;
        string ad;
        public static string eposta;
        string sifre;
        public static int counter;
        int gec2;
        // Change to your own location to run the program.

        string resimler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\resimler.txt";
        string adlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\adlar.txt";
        string soyadlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\soyadlar.txt";
        string epostalar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\epostalar.txt";
        string telefonlar = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\telefonlar.txt";
        string bakiyeler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\bakiyeler.txt";
        string sifreler = "C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\sifreler.txt";
        public LogInScreen()
        {
            InitializeComponent();
        }
        // Checking whether it's created or not.
        private void LogInScreen_Load(object sender, EventArgs e)
        {
            if (File.Exists(adlar))
            {

            }
            else
            {
                File.CreateText(adlar);
            }
            if (File.Exists(resimler))
            {

            }
            else
            {
                File.CreateText(resimler);
            }
            if (File.Exists(sifreler))
            {

            }
            else
            {
                File.CreateText(sifreler);
            }
            if (File.Exists(soyadlar))
            {

            }
            else
            {
                File.CreateText(soyadlar);
            }
            if (File.Exists(epostalar))
            {

            }
            else
            {
                File.CreateText(epostalar);
            }
            if (File.Exists(telefonlar))
            {

            }
            else
            {
                File.CreateText(telefonlar);
            }
            if (File.Exists(bakiyeler))
            {

            }
            else
            {
                File.CreateText(bakiyeler);
            }
        }
        // Logging in function.
        private void loginButton_Click(object sender, EventArgs e)
        {
            gec2 = 0;
            counter = 1;
            foreach (string line in System.IO.File.ReadLines(@epostalar))
            {
                if (line.Equals(loginTextBox.Text))
                {
                    // Triggering the codes below.
                    gec2 = 1;
                    eposta = line;
                    break;
                }
                counter++;
            }
            if(gec2 == 1)
            {
                sifre = File.ReadLines(@sifreler).Skip(counter - 1).Take(1).First();
                if(textBox1.Text == sifre)
                {
                    ad = File.ReadLines(@adlar).Skip(counter - 1).Take(1).First();
                    // Change to your own location to run the program.
                    string sonHarcamalar = $"C:\\Users\\HP\\Masaüstü\\Bank__App\\Bank__App\\bank\\sonHarcamalar\\{eposta}.txt";
                    genelHarcama = sonHarcamalar;
                    MessageBox.Show("Hoşgeldin " + ad);
                    this.Hide();
                    MainScreen.transactions.Clear(); 
                    for(int i = File.ReadAllLines(genelHarcama).ToList().Count-1; i >= 0; i--)
                        MainScreen.transactions.Add(File.ReadAllLines(genelHarcama).ToList()[i]);
                    new MainScreen().Show();
                    if (File.Exists(sonHarcamalar))
                    {

                    }
                    else
                    {
                        // Transaction file creation.
                        File.CreateText(sonHarcamalar);
                    }
                }
                else
                {
                    MessageBox.Show("Mail ya da şifre yanlış.");
                }
                
                
            }
            else
            {
                MessageBox.Show("Mail ya da şifre yanlış.");
            }
        }

        private void infoLabel_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void signInLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SignUp().Show();
        }

        private void loginTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
